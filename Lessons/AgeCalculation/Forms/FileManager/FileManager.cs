using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Security.AccessControl;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Shapes;

namespace AgeCalculation.Forms
{
    public partial class FileManager : Form
    {
        public FileManager()
        {
            InitializeComponent();
        }

        public object BoxDataSource { get { return listBox1.DataSource; }
            set { 
                listBox1.DataSource = value;
                listBox1.ClearSelected();
            }
        }

        string parent = "";
        string currentPath = "";
        int selectIndex;
        WorkMode workMode = WorkMode.Search;

        private void FileManager_Load(object sender, EventArgs e)
        {
            listBox1.SelectionMode = SelectionMode.One;
            BoxDataSource = DriveInfo.GetDrives().Select(x => x.Name).ToArray();

            buttonCreate.Visible = false;
            buttonSave.Visible = false;
            buttonCompress.Visible = false;
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (selectIndex != listBox1.SelectedIndex)
            {
                if (listBox1.SelectedItem != null)
                {
                    selectIndex = listBox1.SelectedIndex;
                    currentPath = listBox1.SelectedItem.ToString();
                }
            }
        }

        private void buttonBack_Click(object sender, EventArgs e)
        {
            switch (workMode)
            {
                case WorkMode.Search:
                    {
                        if (currentPath != "")
                        {
                            var disks = DriveInfo.GetDrives().Select(x => x.Name);
                            if (disks.Contains(parent))
                            {
                                buttonBack.Visible = false;
                                buttonCreate.Visible = false;

                                BoxDataSource = disks.ToArray();
                                return;
                            }

                            string dir = Directory.GetParent(parent).FullName;
                            var list =
                                Directory.GetDirectories(dir).
                                Concat(Directory.GetFiles(dir)).ToArray();

                            if (list != null)
                            {
                                parent = dir;
                                BoxDataSource = list;
                            }
                        }
                    }
                    break;
                case WorkMode.DocEdit:
                    {
                        textBoxDoc.Visible = false;
                        buttonSave.Visible = false;
                        buttonOpen.Visible = true;
                        buttonCreate.Visible = true;

                        buttonSave.Text = "Save";
                        workMode = WorkMode.Search;
                    }
                    break;
                default:
                    break;
            }
        }

        private void listBox1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (selectIndex == listBox1.SelectedIndex)
            {
                if(!OpenListBoxItem()) return;
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (timer1.Enabled)
            {
                errorLable.Text = "";
                timer1.Stop();
            }
        }

        private void Error(object message)
        {
            errorLable.Text = message.ToString();
            timer1.Enabled = true;
            timer1.Start();
        }

        private bool OpenListBoxItem()
        {
            if(listBox1.SelectedItem == null)
            {
                Error("Choose file or directory");
                return false;
            }

            currentPath = listBox1.SelectedItem.ToString();

            if (currentPath != "")
            {
                if (!buttonBack.Visible) buttonBack.Visible = true;
                if (!buttonCreate.Visible) buttonCreate.Visible = true;
                if (!buttonCompress.Visible) buttonCompress.Visible = true;

                if (Directory.Exists(currentPath))
                {
                    try
                    {
                        var list =
                        Directory.GetDirectories(currentPath).
                        Concat(Directory.GetFiles(currentPath)).ToArray();
                        if (list != null)
                        {
                            parent = currentPath;
                            BoxDataSource = list;
                            return true;
                        }
                    }
                    catch (Exception)
                    {
                        Error("Access denied");
                        return false;
                    }
                }
                else if (File.Exists(currentPath))
                {
                    
                    string ext = new FileInfo(currentPath).Extension;

                    if (ext == ".txt")
                    {
                        var file = new StringBuilder(File.ReadAllText(currentPath));
                        textBoxDoc.Visible = true;
                        textBoxDoc.Text = file.ToString();
                        workMode = WorkMode.DocEdit;

                        buttonSave.Visible = true;
                        buttonOpen.Visible = false;
                        buttonCreate.Visible = false;
                        buttonCompress.Visible = false;

                        return true;
                    }
                    else
                    {
                        Error("This format not supported");
                        return false;
                    }
                }
            }

            return false;
        }

        private void buttonOpen_Click(object sender, EventArgs e)
        {
            if (!OpenListBoxItem()) return;
        }

        private void buttonCreate_Click(object sender, EventArgs e)
        {
            EnterName enterName = new EnterName("File path");

            enterName.buttonEnter.MouseDown += (send, n) => 
            {
                TryCreate();
            };
            enterName.textBox1.KeyDown += (send, n) => 
            {
                if (n.KeyCode == Keys.Enter) TryCreate();
            };

            enterName.StartPosition = FormStartPosition.CenterScreen;
            enterName.ShowDialog();

            void TryCreate()
            {
                try
                {
                    string path = parent + enterName.textBox1.Text;
                    var info = new DirectoryInfo(path);
                    if(info.Extension == "")
                    {
                        Directory.CreateDirectory(info.FullName);
                    }
                    else
                    {
                        Directory.CreateDirectory(info.Parent.FullName);
                    }
                    File.Create(path);
                }
                catch (Exception)
                {
                    Error("Invalid name");
                }
                finally
                {
                    var list =
                        Directory.GetDirectories(parent).
                        Concat(Directory.GetFiles(parent)).ToArray();
                    if (list != null)
                    {
                        BoxDataSource = list;
                    }
                    enterName.Close();
                }
            }
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            if(workMode == WorkMode.DocEdit)
            {
                File.WriteAllText(currentPath, textBoxDoc.Text);
                buttonSave.Text = "Save";
            }
        }

        private void textBoxDoc_KeyDown(object sender, KeyEventArgs e)
        {
            if(workMode == WorkMode.DocEdit)
            {
                buttonSave.Text = "Save*";
            }
        }

        private void buttonCompress_Click(object sender, EventArgs e)
        {
            if(listBox1.SelectedItem != null)
            {
                var info = new DirectoryInfo(currentPath);

                Task.Run(() => 
                {
                    var lastParent = parent;
                    int repeat = 0;
                    var path = System.IO.Path.Combine(lastParent, 
                        info.Name + (repeat == 0? "" : $"_{repeat}") + ".zip");
                    while (File.Exists(path))
                    {
                        repeat++;
                        path = System.IO.Path.Combine(lastParent,
                        info.Name + (repeat == 0 ? "" : $"_{repeat}") + ".zip");
                    }

                    ZipFile.CreateFromDirectory(currentPath, path, CompressionLevel.Optimal, false);

                    if(lastParent == parent)
                    {
                        var list =
                        Directory.GetDirectories(parent).
                        Concat(Directory.GetFiles(parent)).ToArray();
                        if (list != null)
                        {
                            parent = currentPath;
                            BoxDataSource = list;
                        }
                    }
                });
            }
        }
    }
}
enum WorkMode
{
    Search,
    DocEdit,
}
