using AgeCalculation.Forms;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace AgeCalculation
{
    class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            var form = new Form();

            var projectName = Process.GetCurrentProcess().ProcessName;
            var filePath = Directory.GetParent(Directory.GetCurrentDirectory());

            while (filePath.Name != projectName)
            {
                filePath = filePath.Parent;
            }

            string path = "D:\\Fork\\MyCourses_C-_Pro\\Lessons\\Lesson 2\\Files\\Settings.xml";
            XmlDocument document = new XmlDocument();
            document.Load(path);

            var node = document.SelectSingleNode("//Name");
            string settings = node.InnerText;


            switch (settings)
            {
                case "calculator":
                    form = new AgeCalculator();
                    break;
                case "timer":
                    form = new Timer();
                    break;
                case "database":
                    form = new DB_Grid();
                    break;
                case "fileManager":
                    form = new FileManager();
                    break;
                case "xmlForm":
                    form = new XmlForm();
                    break;
            }

            form.StartPosition = FormStartPosition.CenterScreen;
            Application.Run(form);
            
        }
    }
}
