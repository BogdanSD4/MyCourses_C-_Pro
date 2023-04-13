using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;
using System.Windows.Forms;
using LessonEntity.EntityFramework;
using LessonEntity.EntityFramework.CodeFirst;
using LessonEntity.EntityFramework.DatabaseFirst;
using LessonEntity.EntityFramework.ModelFirst;
using Newtonsoft.Json;

namespace AgeCalculation
{
    public partial class DB_Grid : Form
    {
        private List<(string, object)> _databaseData = new List<(string, object)>();
        private List<TextBox> inputList = new List<TextBox>();
        private DbSet currentTable;
        private DbContext currentDB;
        private bool IsDatabaseConnect;

        private string path = "D:\\Fork\\MyCourses_C-_Pro\\Lessons\\AgeCalculation\\Database\\";
        private string databaseFirst = "DatabaseFirst.txt";
        private string modelFirst = "ModelFirst.txt";
        private string codeFirst = "CodeFirst.txt";
        public DB_Grid()
        {
            InitializeComponent();
        }

        private void LoadDatabase<T, V>(Func<T, DbSet<V>> func, bool useCache = true) where T : DbContext where V : class
        {
            Predicate<string> dataExists = (name) =>
            {
                if (_databaseData != null)
                {
                    foreach (var set in _databaseData)
                    {
                        if (set.Item1 == typeof(T).Name)
                        {
                            dataGridView1.DataSource = set.Item2;
                            var db = (T)Activator.CreateInstance(typeof(T));
                            currentDB = db;
                            return true;
                        }
                    }
                }
                return false;
            };

            if (!useCache)
            {
                LoadNew();
                return;
            }
            if (!dataExists.Invoke(typeof(T).Name))
            {
                LoadNew();
            }

            DrawUI<T>();
            void LoadNew()
            {
                var db = (T)Activator.CreateInstance(typeof(T));
                var table = func.Invoke(db);
                
                currentTable = table;
                currentDB = db;

                var result = from res in table
                             select res;

                var grid = result.ToArray();
                _databaseData.Add((typeof(T).Name, grid));

                dataGridView1.DataSource = grid;
            }
        }


        private void LoadDatabase<T, V>(string databaseName) where T : DbContext where V : class
        {
            var db = (T)Activator.CreateInstance(typeof(T));
            currentDB = db;

            var json = File.ReadAllText(path + databaseName);
            var result = JsonConvert.DeserializeObject<List<V>>(json);

            dataGridView1.DataSource = result.ToArray();

            DrawUI<T>();
        }
        private void SaveDatabese<T>(string databaseName, List<T> tableList = null) where T : class
        {
            var list = default(List<T>);
            if (tableList == null)
            {
                list = Activator.CreateInstance<List<T>>();
            }
            else
            {
                list = tableList;
            }
             
            var json = JsonConvert.SerializeObject(list);
            File.WriteAllText(path + databaseName, json);
        }
        private void AddToDatabase<T>(string databaseName, T table) where T : class
        {
            var json = File.ReadAllText(path + databaseName);
            var result = JsonConvert.DeserializeObject<List<T>>(json);
            result.Add(table);

            dataGridView1.DataSource = result.ToArray();

            SaveDatabese<T>(databaseName, result);
        }
        private void AddToDatabase<T>(string databaseName, List<T> listTable, T table) where T : class
        {
            listTable.Add(table);

            dataGridView1.DataSource = listTable.ToArray();

            SaveDatabese<T>(databaseName, listTable);
        }
        private void DelOutDatabase<T>(string databaseName, T table, params int[] index)where T : class
        {
            var json = File.ReadAllText(path + databaseName);
            var result = JsonConvert.DeserializeObject<List<T>>(json);
            for (int i = 0; i < index.Length; i++)
            {
                result.RemoveAt(index[i]);
            }

            dataGridView1.DataSource = result.ToArray();

            SaveDatabese<T>(databaseName, result);
        }
        private void DelOutDatabase<T>(string databaseName, List<T> listTable, params int[] index) where T : class
        {
            for (int i = 0; i < index.Length; i++)
            {
                listTable.RemoveAt(index[i]);
            }

            dataGridView1.DataSource = listTable.ToArray();

            SaveDatabese<T>(databaseName, listTable);
        }


        private void DrawUI<T>()
        {
            var type = typeof(T);
            string[] arr = new string[] { };
            if (type == typeof(MyDataBase_DatabaseFirstEntities))
            {
                arr = new string[] { "Name", "Price", "Discount", "Quantity" };
            }
            else if(type == typeof(MyDataBase_ModelFirstEntities))
            {
                arr = new string[] { "Name", "Phone", "CreditCard(last 4)"};
            }
            else if(type == typeof(MyDataBase_CodeFirstEntities))
            {
                arr = new string[] { "Name", "Price", "Quantity" };
            }

            Draw();

            void Draw()
            {
                var size = panel1.Size.Width;
                var distance = size / arr.Length;
                var middle = panel1.Size.Height / 2;

                var startX = 5;
                var startY = 0;

                inputList.Clear();
                panel1.Controls.Clear();

                for (int i = 0; i < arr.Length; i++)
                {
                    var lable = new Label();
                    lable.Text = arr[i];
                    lable.Font = new Font(new FontFamily("Times"), 12, FontStyle.Regular);
                    lable.Location = new Point(startX, middle - lable.Size.Height);
                    lable.Size = new Size(130, lable.Size.Height);
                    panel1.Controls.Add(lable);

                    var box = new TextBox();
                    box.Click += new EventHandler((a, args) => 
                    {
                        labelError.Text = "";
                    }); 
                    box.Location = new Point(startX, middle);
                    box.Size = new Size(distance - 10, box.Height);
                    panel1.Controls.Add(box);

                    inputList.Add(box);
                    startX += distance;
                }
            }
        }
        private void Error(string message)
        {
            labelError.Text = message;
        }
        private void DB_Grid_Load(object sender, EventArgs e)
        {
            
            MyDataBase_DatabaseFirstEntities db = new MyDataBase_DatabaseFirstEntities();
            if (db.Database.Exists())
            {
                IsDatabaseConnect = true;
                LoadDatabase<MyDataBase_DatabaseFirstEntities, TableProduct>(x => x.TableProduct);
            }
            else
            {
                IsDatabaseConnect = false;
                
                if (!File.Exists(path + databaseFirst)) SaveDatabese<TableProduct>(databaseFirst);
                if (!File.Exists(path + modelFirst)) SaveDatabese<Client>(modelFirst);
                if (!File.Exists(path + codeFirst)) SaveDatabese<Drinks>(codeFirst);
            }

            string[] items = new string[] { "DatabaseFirst", "ModelFirst", "CodeFirst" };
            comboBox1.Items.AddRange(items);
            comboBox1.SelectedIndex = 0;
            dataGridView1.CellClick += new DataGridViewCellEventHandler((a, args) =>
            {
                labelError.Text = "";
            });
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!IsDatabaseConnect)
            {
                switch (comboBox1.SelectedIndex)
                {
                    case 0:
                        LoadDatabase<MyDataBase_DatabaseFirstEntities, TableProduct>(databaseFirst);
                        break;
                    case 1:
                        LoadDatabase<MyDataBase_ModelFirstEntities, Client>(modelFirst);
                        break;
                    case 2:
                        LoadDatabase<MyDataBase_CodeFirstEntities, Drinks>(codeFirst);
                        break;
                }
            }
            else
            {
                switch (comboBox1.SelectedIndex)
                {
                    case 0:
                        LoadDatabase<MyDataBase_DatabaseFirstEntities, TableProduct>(x => x.TableProduct);
                        break;
                    case 1:
                        LoadDatabase<MyDataBase_ModelFirstEntities, Client>(x => x.ClientSet);
                        break;
                    case 2:
                        LoadDatabase<MyDataBase_CodeFirstEntities, Drinks>(x => x.DrinkSet);
                        break;
                }
            }
        }

        private async void button3_Click(object sender, EventArgs e)
        {
            string symbol = "/?.,';:\"*-+)(=_";
            string number = "0123456789";
            List<string> errorList = new List<string>();

            T CorrectNum<T>(string num, string errorName) where T : struct
            {
                if (num.Length == 0)
                {
                    errorList.Add(errorName);
                    return default(T);
                }
                if (!number.Contains(num[0]))
                {
                    errorList.Add(errorName);
                    return default(T);
                }

                string result = $"{num[0]}";

                for (int i = 1; i < num.Length; i++)
                {
                    if (num[i] == '.')
                    {
                        result += ',';
                        continue;
                    }
                    if (num[i] != ',' && !number.Contains(num[i]))
                    {
                        errorList.Add(errorName);
                        return default(T);
                    }

                    result += num[i];
                }

                if(result.Count(x => x == ',') > 1)
                {
                    errorList.Add(errorName);
                    return default(T);
                }

                double res = double.Parse(result);
                return (T)Convert.ChangeType(res, typeof(T));
            }

            if (!IsDatabaseConnect)
            {
                switch (currentDB.GetType().Name)
                {
                    case "MyDataBase_DatabaseFirstEntities":
                        {
                            var json = File.ReadAllText(path + databaseFirst);
                            var result = JsonConvert.DeserializeObject<List<TableProduct>>(json);

                            var list = result.ToArray();

                            int id = list.Length == 0 ? 1 : list.Max(x => x.Id) + 1;
                            string productName = inputList[0].Text;
                            decimal price = CorrectNum<decimal>(inputList[1].Text, "[Price]");
                            float discount = CorrectNum<float>(inputList[2].Text, "[Discount]");
                            int quantity = CorrectNum<int>(inputList[3].Text, "[Quantity]");

                            if (errorList.Count > 0)
                            {
                                Error($"Invalid value: {errorList[0]}");
                                return;
                            }

                            TableProduct table = new TableProduct()
                            {
                                Id = id,
                                Product = productName,
                                Price = price,
                                Discount = discount,
                                QuantityOfProduct = quantity,
                            };
                            table.PriceWithDiscount = (table.Price * (decimal)(1 - (table.Discount / 100)));

                            AddToDatabase<TableProduct>(databaseFirst, result, table);

                            dataGridView1.DataSource = result.ToArray();
                        }
                        break;
                    case "MyDataBase_ModelFirstEntities":
                        {
                            var json = File.ReadAllText(path + modelFirst);
                            var result = JsonConvert.DeserializeObject<List<Client>>(json);

                            var clientJson = File.ReadAllText(path + databaseFirst);
                            var tableProduct = JsonConvert.DeserializeObject<List<TableProduct>>(clientJson);
                            Random random = new Random();
                            decimal? sum = 0;

                            var list = result.ToArray();

                            int id = list.Length == 0 ? 1 : list.Max(x => x.Id) + 1;
                            string clientName = inputList[0].Text;
                            string phone = await Task.Run<string>(() =>
                            {
                                var text = inputList[1].Text;
                                if (text.Length != 10)
                                {
                                    errorList.Add("[Phone (Exumple: 0962553952)]");
                                    return null;
                                }
                                for (int i = 0; i < text.Length; i++)
                                {
                                    if (!number.Contains(text[i]))
                                    {
                                        errorList.Add("[Phone (Exumple: 0962553952)]");
                                        return null;
                                    }
                                }
                                return text;
                            });
                            string products = await Task.Run<string>(() =>
                            {
                                string res = "";
                                var count = random.Next(1, 10);
                                for (int i = 0; i < count; i++)
                                {
                                    var item = tableProduct[random.Next(0, tableProduct.Count)];
                                    res += $"{item.Id},";
                                    sum += item.PriceWithDiscount;
                                }
                                res = res.Remove(res.Length - 1);
                                return res;
                            });
                            string credit = await Task.Run<string>(() =>
                            {
                                var text = inputList[2].Text;
                                if (text.Length != 4)
                                {
                                    errorList.Add("[CreditCard (Exumple: 8671)]");
                                    return null;
                                }
                                for (int i = 0; i < text.Length; i++)
                                {
                                    if (!number.Contains(text[i]))
                                    {
                                        errorList.Add("[CreditCard (Exumple: 8671)]");
                                        return null;
                                    }
                                }
                                return text;
                            });

                            if (errorList.Count > 0)
                            {
                                Error($"Invalid value: {errorList[0]}");
                                return;
                            }

                            Client table = new Client()
                            {
                                Id = id,
                                Name = clientName,
                                CheckAmount = sum.ToString(),
                                Phone = phone.ToString(),
                                Products = products,
                                CreditCard = credit,
                            };


                            AddToDatabase<Client>(modelFirst, result, table);

                            dataGridView1.DataSource = result.ToArray();
                        }
                        break;
                    case "MyDataBase_CodeFirstEntities":
                        {
                            var json = File.ReadAllText(path + codeFirst);
                            var result = JsonConvert.DeserializeObject<List<Drinks>>(json);

                            int id = result.Count == 0 ? 1 : result.Max(x => x.Id) + 1;
                            string productName = inputList[0].Text;
                            decimal price = CorrectNum<decimal>(inputList[1].Text, "[Price]");
                            int quantity = CorrectNum<int>(inputList[2].Text, "[Quantity]");

                            if (errorList.Count > 0)
                            {
                                Error($"Invalid value: {errorList[0]}");
                                return;
                            }

                            Drinks table = new Drinks()
                            {
                                Id = id,
                                Name = productName,
                                Price = price,
                                Quantity = quantity,
                            };

                            AddToDatabase<Drinks>(codeFirst, result, table);

                            dataGridView1.DataSource = result.ToArray();
                        }
                        break;
                }
            }
            else
            {
                switch (currentDB.GetType().Name)
                {
                    case "MyDataBase_DatabaseFirstEntities":
                        {
                            var db = (MyDataBase_DatabaseFirstEntities)currentDB;

                            var list = db.TableProduct.ToArray();

                            int id = list.Length == 0 ? 1 : list.Max(x => x.Id) + 1;
                            string productName = inputList[0].Text;
                            decimal price = CorrectNum<decimal>(inputList[1].Text, "[Price]");
                            float discount = CorrectNum<float>(inputList[2].Text, "[Discount]");
                            int quantity = CorrectNum<int>(inputList[3].Text, "[Quantity]");

                            if (errorList.Count > 0)
                            {
                                Error($"Invalid value: {errorList[0]}");
                                return;
                            }

                            TableProduct table = new TableProduct()
                            {
                                Id = id,
                                Product = productName,
                                Price = price,
                                Discount = discount,
                                QuantityOfProduct = quantity,
                            };
                            table.PriceWithDiscount = (table.Price * (decimal)(1 - (table.Discount / 100)));

                            db.TableProduct.Add(table);

                            await db.SaveChangesAsync();

                            var result = from data in db.TableProduct
                                         select data;

                            var arr = result.ToArray();

                            for (int i = 0; i < _databaseData.Count; i++)
                            {
                                var name = _databaseData[i].Item1;
                                if (name == db.GetType().Name)
                                {
                                    _databaseData[i] = (name, arr);
                                }
                            }

                            dataGridView1.DataSource = arr;

                        }
                        break;
                    case "MyDataBase_ModelFirstEntities":
                        {
                            var db = (MyDataBase_ModelFirstEntities)currentDB;

                            var productDB = new MyDataBase_DatabaseFirstEntities();
                            var tableProduct = productDB.TableProduct.ToArray();
                            Random random = new Random();
                            decimal? sum = 0;

                            var list = db.ClientSet.ToArray();

                            int id = list.Length == 0 ? 1 : list.Max(x => x.Id) + 1;
                            string clientName = inputList[0].Text;
                            string phone = await Task.Run<string>(() =>
                            {
                                var text = inputList[1].Text;
                                if (text.Length != 10)
                                {
                                    errorList.Add("[Phone (Exumple: 0962553952)]");
                                    return null;
                                }
                                for (int i = 0; i < text.Length; i++)
                                {
                                    if (!number.Contains(text[i]))
                                    {
                                        errorList.Add("[Phone (Exumple: 0962553952)]");
                                        return null;
                                    }
                                }
                                return text;
                            });
                            string products = await Task.Run<string>(() =>
                            {
                                var res = "";
                                var count = random.Next(1, 10);
                                for (int i = 0; i < count; i++)
                                {
                                    var item = tableProduct[random.Next(0, tableProduct.Length)];
                                    res += $"{item.Id},";
                                    sum += item.PriceWithDiscount;
                                }
                                res = res.Remove(res.Length - 1);
                                return res;
                            });
                            string credit = await Task.Run<string>(() =>
                            {
                                var text = inputList[2].Text;
                                if (text.Length != 4)
                                {
                                    errorList.Add("[Phone (Exumple: 8671)]");
                                    return null;
                                }
                                for (int i = 0; i < text.Length; i++)
                                {
                                    if (!number.Contains(text[i]))
                                    {
                                        errorList.Add("[Phone (Exumple: 8671)]");
                                        return null;
                                    }
                                }
                                return text;
                            });

                            if (errorList.Count > 0)
                            {
                                Error($"Invalid value: {errorList[0]}");
                                return;
                            }

                            Client table = new Client()
                            {
                                Id = id,
                                Name = clientName,
                                CheckAmount = sum.ToString(),
                                Phone = phone.ToString(),
                                Products = products,
                                CreditCard = credit,
                            };


                            db.ClientSet.Add(table);

                            await db.SaveChangesAsync();

                            var result = from data in db.ClientSet
                                         select data;

                            var arr = result.ToArray();

                            for (int i = 0; i < _databaseData.Count; i++)
                            {
                                var name = _databaseData[i].Item1;
                                if (name == db.GetType().Name)
                                {
                                    _databaseData[i] = (name, arr);
                                }
                            }

                            dataGridView1.DataSource = arr;

                        }
                        break;
                    case "MyDataBase_CodeFirstEntities":
                        {
                            var db = (MyDataBase_CodeFirstEntities)currentDB;
                            var list = db.DrinkSet.ToArray();

                            int id = list.Length == 0 ? 1 : list.Max(x => x.Id) + 1;
                            string productName = inputList[0].Text;
                            decimal price = CorrectNum<decimal>(inputList[1].Text, "[Price]");
                            int quantity = CorrectNum<int>(inputList[2].Text, "[Quantity]");

                            if (errorList.Count > 0)
                            {
                                Error($"Invalid value: {errorList[0]}");
                                return;
                            }

                            Drinks table = new Drinks()
                            {
                                Id = id,
                                Name = productName,
                                Price = price,
                                Quantity = quantity,
                            };

                            db.DrinkSet.Add(table);

                            await db.SaveChangesAsync();

                            var result = from data in db.DrinkSet
                                         select data;

                            var arr = result.ToArray();

                            for (int i = 0; i < _databaseData.Count; i++)
                            {
                                var name = _databaseData[i].Item1;
                                if (name == db.GetType().Name)
                                {
                                    _databaseData[i] = (name, arr);
                                }
                            }

                            dataGridView1.DataSource = arr;

                        }
                        break;
                }
            }

            
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            if (!IsDatabaseConnect)
            {
                if (dataGridView1.SelectedRows.Count > 0)
                {
                    switch (currentDB.GetType().Name)
                    {
                        case "MyDataBase_DatabaseFirstEntities":
                            {
                                var json = File.ReadAllText(path + databaseFirst);
                                var result = JsonConvert.DeserializeObject<List<TableProduct>>(json);

                                int[] indexes = new int[dataGridView1.SelectedRows.Count];
                                for (int i = 0; i < dataGridView1.SelectedRows.Count; i++)
                                {
                                    indexes[i] = dataGridView1.SelectedRows[i].Index;
                                }

                                DelOutDatabase<TableProduct>(databaseFirst, result, indexes);

                                dataGridView1.DataSource = result.ToArray();
                            }
                            break;
                        case "MyDataBase_ModelFirstEntities":
                            {
                                var json = File.ReadAllText(path + modelFirst);
                                var result = JsonConvert.DeserializeObject<List<Client>>(json);

                                int[] indexes = new int[dataGridView1.SelectedRows.Count];
                                for (int i = 0; i < dataGridView1.SelectedRows.Count; i++)
                                {
                                    indexes[i] = dataGridView1.SelectedRows[i].Index;
                                }

                                DelOutDatabase<Client>(modelFirst, result, indexes);

                                dataGridView1.DataSource = result.ToArray();
                            }
                            break;
                        case "MyDataBase_CodeFirstEntities":
                            {
                                var json = File.ReadAllText(path + codeFirst);
                                var result = JsonConvert.DeserializeObject<List<Drinks>>(json);

                                int[] indexes = new int[dataGridView1.SelectedRows.Count];
                                for (int i = 0; i < dataGridView1.SelectedRows.Count; i++)
                                {
                                    indexes[i] = dataGridView1.SelectedRows[i].Index;
                                }

                                DelOutDatabase<Drinks>(codeFirst, result, indexes);

                                dataGridView1.DataSource = result.ToArray();
                            }
                            break;
                    }
                }
                else
                {
                    labelError.Text = "Choose some row";
                }
            }
            else
            {
                if (dataGridView1.SelectedRows.Count > 0)
                {
                    switch (currentDB.GetType().Name)
                    {
                        case "MyDataBase_DatabaseFirstEntities":
                            {
                                var db = (MyDataBase_DatabaseFirstEntities)currentDB;

                                for (int i = 0; i < dataGridView1.SelectedRows.Count; i++)
                                {
                                    int row = dataGridView1.SelectedRows[i].Index;
                                    int counter = 0;
                                    int tableIndex = 0;
                                    foreach (var table in db.TableProduct)
                                    {
                                        if (row == counter)
                                        {
                                            tableIndex = table.Id;
                                            break;
                                        }
                                        else counter++;
                                    }

                                    var res = from table in db.TableProduct
                                              where table.Id == tableIndex
                                              select table;
                                    db.TableProduct.RemoveRange(res.ToArray());
                                }

                                await db.SaveChangesAsync();

                                var result = from data in db.TableProduct
                                             select data;

                                var arr = result.ToArray();

                                for (int i = 0; i < _databaseData.Count; i++)
                                {
                                    var name = _databaseData[i].Item1;
                                    if (name == db.GetType().Name)
                                    {
                                        _databaseData[i] = (name, arr);
                                    }
                                }

                                dataGridView1.DataSource = arr;
                            }
                            break;
                        case "MyDataBase_ModelFirstEntities":
                            {
                                var db = (MyDataBase_ModelFirstEntities)currentDB;

                                for (int i = 0; i < dataGridView1.SelectedRows.Count; i++)
                                {
                                    int row = dataGridView1.SelectedRows[i].Index;
                                    int counter = 0;
                                    int tableIndex = 0;
                                    foreach (var table in db.ClientSet)
                                    {
                                        if (row == counter)
                                        {
                                            tableIndex = table.Id;
                                            break;
                                        }
                                        else counter++;
                                    }

                                    var res = from table in db.ClientSet
                                              where table.Id == tableIndex
                                              select table;
                                    db.ClientSet.RemoveRange(res.ToArray());
                                }

                                await db.SaveChangesAsync();

                                var result = from data in db.ClientSet
                                             select data;

                                var arr = result.ToArray();

                                for (int i = 0; i < _databaseData.Count; i++)
                                {
                                    var name = _databaseData[i].Item1;
                                    if (name == db.GetType().Name)
                                    {
                                        _databaseData[i] = (name, arr);
                                    }
                                }

                                dataGridView1.DataSource = arr;
                            }
                            break;
                        case "MyDataBase_CodeFirstEntities":
                            {
                                var db = (MyDataBase_CodeFirstEntities)currentDB;

                                for (int i = 0; i < dataGridView1.SelectedRows.Count; i++)
                                {
                                    int row = dataGridView1.SelectedRows[i].Index;
                                    int counter = 0;
                                    int tableIndex = 0;
                                    foreach (var table in db.DrinkSet)
                                    {
                                        if (row == counter)
                                        {
                                            tableIndex = table.Id;
                                            break;
                                        }
                                        else counter++;
                                    }

                                    var res = from table in db.DrinkSet
                                              where table.Id == tableIndex
                                              select table;
                                    db.DrinkSet.RemoveRange(res.ToArray());
                                }

                                await db.SaveChangesAsync();

                                var result = from data in db.DrinkSet
                                             select data;

                                var arr = result.ToArray();

                                for (int i = 0; i < _databaseData.Count; i++)
                                {
                                    var name = _databaseData[i].Item1;
                                    if (name == db.GetType().Name)
                                    {
                                        _databaseData[i] = (name, arr);
                                    }
                                }

                                dataGridView1.DataSource = arr;
                            }
                            break;
                    }
                }
                else
                {
                    labelError.Text = "Choose some row";
                }
            }
        }
    }
}
