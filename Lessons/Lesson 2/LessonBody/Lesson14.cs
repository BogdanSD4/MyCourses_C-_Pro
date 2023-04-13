using ClassesOfLesson14;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Lessons.LessonBody
{
    class Lesson14 : ILesson
    {
        public void Open()
        {
            Library();
            Factory();
            Calculator();
            DataTimes();
        }

        private void Library()
        {
            Console.WriteLine();

            var bookCount = (int)ILesson.Read<uint>("How many books in your library? ");

            string[] infoArr = new string[bookCount];

            for (int i = 0; i < infoArr.Length; i++)
            {
                string T = Lesson_Instruments.GetRandomStructType().FullName;
                var type = Type.GetType($"ClassesOfLesson14.Book`1[{T}]");
                var books = Activator.CreateInstance(type);
                infoArr[i] = books.GetType().GetMethod("Show").Invoke(books, null).ToString();
            }


            Lesson_Instruments.InlineWriter(3, infoArr);
        }
        private void Factory()
        {
            Console.WriteLine();
            Console.WriteLine("Don't hold key button");

            Console.CursorVisible = false;

            int x = Console.CursorLeft;
            int y = Console.CursorTop;

            int[] cell = new int[] { 3, 3, 1, 1 };
            int distance = 20;
            float speed = 5;
            int baseWidth = 8;

            int controllerH = 0;
            int controllerV = 0;

            bool keyActiv = false;
            bool exit = false;

            Storage storage = new Storage();
            FactoryLine factoryLine = new FactoryLine(storage, baseWidth, distance);

            Factorio<Gear> factorioGear = new Factorio<Gear>(factoryLine);
            Factorio<Weapon> factorioWeapon = new Factorio<Weapon>(factoryLine);
            Factorio<Clothes> factorioClothes = new Factorio<Clothes>(factoryLine);

            Func<int, string> space = Lesson_Instruments.GetSpace;

            DrawBase();
            DrawUI();

            Task.Run(() =>
            {
                while (!exit)
                {
                    DrawProduct();
                    Thread.Sleep((int)(1000 / speed));
                }
            });

            while (!exit) 
            {
                if (Console.KeyAvailable)
                {
                    ControllerUI();
                }
            }

            Console.CursorVisible = true;

            void DrawProduct()
            {
                if(factoryLine.StorageSize > 0)
                {
                    var product = factoryLine.storage[0];
                    var logo = product.Logo;
                    var dis = factoryLine.Finish - (product.PosX + product.Logo.Length) - 1;
                    Console.SetCursorPosition(x + product.PosX, y + 1);
                    if (dis < 0)
                    {
                        dis *= -1;
                        for (int i = 0; i < dis; i++)
                        {
                            if (logo.Length > 0)
                            {
                                logo = logo.Remove(logo.Length - 1);
                            }
                        }
                        Console.Write(" " + logo + " ");
                    }
                    else
                    {
                        Console.Write(" " + product.Logo + " ");
                    }
                    product.PosX++;
                    if (factoryLine.Finish == product.PosX) DrawBase();
                }
            }
            void DrawBase()
            {
                Console.SetCursorPosition(x, y);

                int num1 = factoryLine.StorageSize;
                int num2 = storage.StorageSize;
                int num1Lang = num1.ToString().Length;
                int num2Lang = num1.ToString().Length;
                string baseDistance = space.Invoke(distance);

                string drawFabricBase = "+------+";
                string drawFabricCenter_1 = "|" + $" {num1}{space.Invoke(5 - num1Lang)}" + "|";
                string drawFabricCenter_2 = "|" + $" {num2}{space.Invoke(5 - num2Lang)}" + "|";

                Console.WriteLine(drawFabricBase + baseDistance + " "+  drawFabricBase + "     \n" + drawFabricCenter_1);

                Console.SetCursorPosition(x + distance + baseWidth, y + 1);
                Console.WriteLine($" {drawFabricCenter_2}     ");

                Console.WriteLine(drawFabricBase + baseDistance + " " + drawFabricBase + "     ");

                Console.Write(Lesson_Instruments.emptyString);
            }
            void ControllerUI()
            {
                var key = Console.ReadKey(true);

                switch (key.Key)
                {
                    case ConsoleKey.LeftArrow:
                        if (--controllerH < 0) controllerH = cell[controllerV] - 1;
                        break;
                    case ConsoleKey.RightArrow:
                        if (++controllerH >= cell[controllerV]) controllerH = 0;
                        break;
                    case ConsoleKey.UpArrow:
                        if (--controllerV < 0) controllerV = FindVerticalIndex() - 1;
                        break;
                    case ConsoleKey.DownArrow:
                        if (++controllerV >= FindVerticalIndex()) controllerV = 0;
                        break;
                    case ConsoleKey.Enter:
                        keyActiv = true;
                        break;
                }

                DrawUI();

                int FindVerticalIndex()
                {
                    int res = 0;
                    for (int i = 0; i < cell.Length; i++)
                    {
                        if(cell[i] - 1 >= controllerH)
                        {
                            res++;
                        }
                    }
                    return res;
                }
            }
            void DrawUI()
            {
                Console.SetCursorPosition(x, y + 4);

                Action final = null;

                Console.WriteLine("Product   Settings");
                Write("[Gears]   ", 0, 0, () =>
                {
                    factoryLine.AddProduct(factorioGear.CreateProduct());
                    DrawBase();
                });
                Write("[+]", 1, 0, () =>
                {
                    if (speed <= 50)
                    {
                        speed++;
                    }
                    else speed = 10;
                });
                Write("[-]", 2, 0, () =>
                {
                    if (speed >= 0)
                    {
                        speed--;
                    }
                    else speed = 0;
                });
                Console.WriteLine(" Speed: " + MathF.Round(speed, 1));
                Write("[Weapon]  ", 0, 1, () =>
                {
                    factoryLine.AddProduct(factorioWeapon.CreateProduct());
                    DrawBase();
                });
                Write("[+]", 1, 1, () =>
                {
                    if (distance <= 80)
                    {
                        distance++;
                        factoryLine.Finish++;
                    }
                    else distance = 80;
                    DrawBase();
                });
                Write("[-]", 2, 1, () =>
                {
                    if (distance >= 10)
                    {
                        distance--;
                        factoryLine.Finish--;
                    }
                    else distance = 10;
                    DrawBase();
                });
                Console.WriteLine(" Distance: " + distance);
                Write("[Clothes]\n", 0, 2, () =>
                {
                    factoryLine.AddProduct(factorioClothes.CreateProduct());
                    DrawBase();
                });
                Write("<Exit>\n", 0, 3, () => { exit = true; });

                if (keyActiv && final != null)
                {
                    final.Invoke();
                    keyActiv = false;
                }

                void Write(string text, int posX, int posY, Action action)
                {
                    if (posX == controllerH && posY == controllerV)
                    {
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.Write(text);
                        Console.ForegroundColor = ConsoleColor.Green;
                        final = action;
                    }
                    else
                    {
                        Console.Write(text);
                    }
                }
            }
        }
        private void Calculator()
        {
            Console.WriteLine();

            Console.WriteLine(
                "<int, double>   " + Calculator<int, double>.Sum(10, 15.4896435) + "\n" +
                "<float, int>   " + Calculator<float, int>.Sub(10.486f, 15) + "\n" +
                "<int, decimal>   " + Calculator<int, decimal>.Mult(10, 15.4896M) + "\n" +
                "<float, decimal>   " + Calculator<float, decimal>.Div(10.7894f, 14.96435M));
        }
        private void DataTimes()
        {
            Console.WriteLine("DateTime time = null / Exception: it is a non-nullable value type");
            Console.WriteLine("DateTime time = default / Correct");
        }
    }
}
namespace ClassesOfLesson14
{
    public class Book<T> where T : struct
    {
        public Book()
        {
            name = Lessons.Lesson_Instruments.GetSomeText(10, ' ');
            Random random = new Random();
            object obj = random.NextDouble() * 1000;
            price = (T)Convert.ChangeType(obj, typeof(T));
        }
        public Book(string baseName, T basePrice)
        {
            name = baseName;
            price = basePrice;
        }
        private string name;
        private T price;

        public string Name { get { return name; } set { name = value; } }
        public T Price { get { return price; } set { price = value; } }

        public string Show()
        {
            string res = $"Name: {Name}\nPrice: {Price}\nType: {typeof(T)}";
            return res;
        }
    }


    public interface IFactory 
    {
        public FactoryLine FactoryLine { get; set; }
        public string Logo { get; set; }
        public int PosX { get; set; }
    }
    public class Factorio<T> where T : IFactory
    {
        public Factorio(FactoryLine line)
        {
            factoryLine = line;
        }

        private FactoryLine factoryLine;

        public T CreateProduct()
        {
            return (T)Activator.CreateInstance(typeof(T)); 
        }
    }
    public class Storage
    {
        public List<IFactory> storage = new List<IFactory>();
        public int StorageSize => storage.Count;

        public virtual void AddProduct(IFactory prodict)
        {
            storage.Add(prodict);
        }
    }
    public class FactoryLine : Storage
    {
        public FactoryLine(Storage stor,int start, int distance)
        {
            collect = stor;
            lineStart = start;
            lineEnd = start + distance;
        }

        Storage collect;
        int lineStart;
        int lineEnd;

        public int Finish { get { return lineEnd; } set { lineEnd = value; } }
        public override void AddProduct(IFactory product)
        {
            base.AddProduct(product);
            product.FactoryLine = this;
            product.PosX = lineStart;
        }

        public void ProductToStorage()
        {
            collect.AddProduct(storage[0]);
            storage.RemoveAt(0);
        }
    }
    public class Gear : IFactory
    {
        private string logo = "[Gears]";
        private int posX = 0;
        public string Logo { get { return logo; } set { logo = value; } }
        public int PosX { get { return posX; } set 
            {
                posX = value;
                if (posX == FactoryLine.Finish) FactoryLine.ProductToStorage();
            }
        }
        public FactoryLine FactoryLine { get; set; }
    }
    public class Weapon : IFactory
    {
        private string logo = "[Weapon]";
        private int posX = 0;
        public string Logo { get { return logo; } set { logo = value; } }
        public int PosX
        {
            get { return posX; }
            set
            {
                posX = value;
                if (posX == FactoryLine.Finish) FactoryLine.ProductToStorage();
            }
        }
        public FactoryLine FactoryLine { get; set; }
    }
    public class Clothes : IFactory
    {
        private string logo = "[Clothes]";
        private int posX = 0;
        public string Logo { get { return logo; } set { logo = value; } }
        public int PosX
        {
            get { return posX; }
            set
            {
                posX = value;
                if (posX == FactoryLine.Finish) FactoryLine.ProductToStorage();
            }
        }
        public FactoryLine FactoryLine { get; set; }
    }


    public class Calculator<T1, T2> where T1 : struct where T2 : struct
    {
        public static double Sum(T1 arg1, T2 arg2)
        {
            double r1 = Double.Parse(arg1.ToString());
            double r2 = Double.Parse(arg2.ToString());
            return r1 + r2;
        }
        public static double Sub(T1 arg1, T2 arg2)
        {
            double r1 = Double.Parse(arg1.ToString());
            double r2 = Double.Parse(arg2.ToString());
            return r1 - r2;
        }
        public static double Mult(T1 arg1, T2 arg2)
        {
            double r1 = Double.Parse(arg1.ToString());
            double r2 = Double.Parse(arg2.ToString());
            return r1 * r2;
        }
        public static double Div(T1 arg1, T2 arg2)
        {
            double r1 = Double.Parse(arg1.ToString());
            double r2 = Double.Parse(arg2.ToString());
            return r1 / r2;
        }
    }
}
