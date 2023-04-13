using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ClassesOfLesson11;
using System.IO;
using System.Reflection;

namespace Lessons.LessonBody
{
    class Lesson11 : ILesson
    {
        public void Open()
        {
            Writer();
            TrainSchedule();
            ClassAndStructDifference();
            ColorText();
            OfficeWorkers();
        }

        private void Writer()
        {
            Console.WriteLine();
            uint count = ILesson.Read<uint>("Input notebook count(1 - 10): ", (ref string res) =>
            {
                int num = int.Parse(res);
                if(num >= 1 && num <= 10) return true;
                return false;
            }, true);

            Console.WriteLine();

            List<string> notes = new List<string>();
            for (int i = 0; i < count; i++)
            {
                var obj = new Notebook(true);
                notes.Add(obj.ShowParam());
            }

            Lesson_Instruments.InlineWriter(space: 5, text: notes.ToArray());
        }
        private void TrainSchedule()
        {
            Console.WriteLine();
            string sym = ILesson.ReadKey("(\"D\" - default train count(5) \"Q\" - your count)", (ref ConsoleKey res) =>
            {
                if (res == ConsoleKey.D || res == ConsoleKey.Q) return true;
                return false;
            }, false, false);

            uint count;

            switch (sym)
            {
                case "Q":
                    count = ILesson.Read<uint>("Input train count: ");
                    break;
                default:
                    count = 5;
                    break;
            }

            int line = count < 200? (count < 10? (int)count : 10): 25;
            Train[] trains = new Train[count];
            bool fillRandom = false;

            Console.WriteLine("\nTrain number comprise of 4 number (Example: \"1234\")");
            Console.WriteLine("(\"r\" - random current | \"-r\" - random all left)\n");
            int finalY = 0;

            for (int i = 0; i < trains.Length; i++)
            {
                if (fillRandom)
                {
                    Random random = new Random();
                    trains[i] = new Train(random.Next(1000, 9999));
                    Console.SetCursorPosition(0, Console.CursorTop);
                    Console.Write($"Train #{i+1}: " + trains[i].trainNumber);
                }
                else
                {
                    bool isCommand = false;
                    uint number = ILesson.Read<uint>($"Train #{i+1}: ", (ref string res) =>
                        {
                            if (res == "r")
                            {
                                Random random = new Random();
                                res = random.Next(1000, 9999).ToString();
                                isCommand = true;
                                return true;
                            }
                            else if (res == "-r")
                            {
                                Random random = new Random();
                                res = random.Next(1000, 9999).ToString();
                                fillRandom = true;
                                isCommand = true;
                                return true;
                            }

                            if (res.Length == 4) return true;
                            return false;
                        }, true, false);
                    trains[i] = new Train((int)number);

                    if (isCommand)
                    {
                        Console.SetCursorPosition(Console.CursorLeft, Console.CursorTop - 1);
                        Console.WriteLine($"Train #{i+1}: " + trains[i].trainNumber);
                    }
                }
            }

            Console.WriteLine();
            Console.CursorVisible = false;
            Console.WriteLine("(\"Arrow buttons\" - control | \"Enter\" - accept | \"E\" - exit)\n");

            int controler = 0;
            int row = Task.Run<int>(() => 
            {
                int leng = trains.Length;
                int num = 1;
                while(true)
                {
                    if (leng > line)
                    {
                        leng -= line;
                        num++;
                    }
                    else
                    {
                        break;
                    }
                }
                return num;
            }).Result;

            int cursorX = Console.CursorLeft;
            int cursorY = Console.CursorTop;

            while (true)
            {
                Console.SetCursorPosition(cursorX, cursorY);
                int counter = 0;

                for (int i = 0; i < trains.Length; i++)
                {
                    if (trains[i].trainNumber == default) continue;
                    if(i == controler)
                    {
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                    Console.Write("["+trains[i].trainNumber + "] ");
                    Console.ForegroundColor = ConsoleColor.Green;

                    counter++;
                    if (counter != 0 && counter % line == 0)
                    {
                        Console.WriteLine();
                    }
                }

                if (finalY == 0) finalY = Console.CursorTop + 1;

                var key = Console.ReadKey(true).Key;
                if (key == ConsoleKey.E) break;
                switch (key)
                {
                    case ConsoleKey.LeftArrow:
                        if (controler-- % line == 0) controler += line;
                        break;
                    case ConsoleKey.RightArrow:
                        if (++controler % line == 0) controler -= line;
                        if(controler == trains.Length)
                        {
                            controler -= controler % line;
                        }
                        break;
                    case ConsoleKey.UpArrow:
                        if ((controler -= line) < 0) controler += (row * line);
                        break;
                    case ConsoleKey.DownArrow:
                        int arg = row * line;
                        if ((controler += line) >= arg)
                        {
                            controler -= arg;
                        }
                        break;
                    case ConsoleKey.Enter:
                        Console.WriteLine("\n");
                        Console.WriteLine(trains[controler].GetInfo());
                        finalY = Console.CursorTop + 1; 
                        break;
                }
                if(controler > trains.Length - 1)
                {
                    controler = trains.Length - 1;
                }
            }

            Console.SetCursorPosition(0, finalY);
            Console.CursorVisible = true;
        }
        private void ClassAndStructDifference()
        {
            MyClass newClass = new MyClass { change = "not change" };
            MyStruct newStruct = new MyStruct { change = "not change" };
            
            Console.WriteLine("Class:"+"\n>Hash: " + newClass.GetHashCode() + "\nChange: "+newClass.change+"\n");
            Console.WriteLine("Struct:" + "\n>Hash: " + newStruct.GetHashCode() + "\nChange: " + newStruct.change+"\n");

            ClassTaker(newClass);
            StructTaker(newStruct);

            Console.WriteLine("Class:" + "\n>Hash: " + newClass.GetHashCode() + "\nChange: " + newClass.change + "\n");
            Console.WriteLine("Class preserves links on value");
            Console.WriteLine("Struct:" + "\n>Hash: " + newStruct.GetHashCode() + "\nChange: " + newStruct.change + "\n");
            Console.WriteLine("Struct preserves value");
        }
        private void ColorText()
        {
            Console.WriteLine();
            Console.WriteLine("(\"Arrow button\" - control | \"Enter\" - choose)");
            Console.WriteLine("(Text: \"exit\" - to exit)\n");

            int vertical = 0;
            string clientInput = "";

            int x = Console.CursorLeft;
            int y= Console.CursorTop;

            Random random = new Random();
            var colors = Enum.GetNames(typeof(ConsoleColor));
            int colorIndex = random.Next(0, colors.Length);

            string arrow = "==> ";
            string arrowEmpy = "   ";
            string freeSpace = Lesson_Instruments.emptyString;

            bool exit = false;

            while (!exit)
            {
                Console.SetCursorPosition(x, y);
                Console.WriteLine((vertical == 0 ? arrow : arrowEmpy) + $"Color: <{colors[colorIndex]}>" + freeSpace);
                Console.WriteLine((vertical == 1 ? arrow : arrowEmpy) + $"Text: {clientInput}" + freeSpace);

                var key = Console.ReadKey(true);

                switch (key.Key)
                {
                    case ConsoleKey.LeftArrow:
                        if(vertical == 0)
                        {
                            if (--colorIndex < 0) colorIndex = colors.Length - 1;
                        }
                        break;
                    case ConsoleKey.RightArrow:
                        if (vertical == 0)
                        {
                            if (++colorIndex == colors.Length) colorIndex = 0;
                        }
                        break;
                    case ConsoleKey.UpArrow:
                        if (--vertical < 0) vertical = 1;
                        break;
                    case ConsoleKey.DownArrow:
                        if (++vertical > 1) vertical = 0;
                        break;
                    case ConsoleKey.Backspace:
                        if(vertical == 1 && clientInput != "")
                        {
                            clientInput = clientInput.Remove(clientInput.Length - 1);
                        }
                        break;
                    case ConsoleKey.Enter:
                        if (clientInput == "exit")
                        {
                            exit = true;
                            Console.WriteLine();
                            break;
                        }
                        Text.Print(clientInput + freeSpace, colorIndex);
                        break;
                    default:
                        if(vertical == 1)
                        {
                            clientInput += key.KeyChar;
                        }
                        break;
                }
            }
        }
        private void OfficeWorkers()
        {
            Console.WriteLine();

            var list = Enum.GetValues<Post>();

            for (int i = list.Length - 1; i >= 0; i--)
            {
                var worker = new Accauntant(list[i]);
                Console.WriteLine(worker.ShowBonus()+"\n");
            }
        }


        private static void ClassTaker(MyClass myClass)
        {
            myClass.change = "change";
        }
        private static void StructTaker(MyStruct myStruct)
        {
            myStruct.change = "change";
        }
    }
}
namespace ClassesOfLesson11
{
    public struct Notebook
    {
        public Notebook(bool rand)
        {
            Random random = new Random();
            model = "#" + Lessons.Lesson_Instruments.GetSomeText(10, ' ');
            producer = "@" + Lessons.Lesson_Instruments.GetSomeText(10, ' ');
            price = random.Next(100, 1500);
        }
        public Notebook(string Model, string Producer, float Price)
        {
            Random random = new Random();
            if (Model == default) model = "#" + Lessons.Lesson_Instruments.GetSomeText(10, ' ');
            else model = Model;

            if (Producer == default) producer = "@" + Lessons.Lesson_Instruments.GetSomeText(10, ' ');
            else producer = Producer;

            if (Price == default) price = random.Next(100, 1500);
            else price = Price;
        }
        public string model;
        public string producer;
        public float price;

        public string ShowParam()
        {
            string result = 
                $"Model: {model}" +
                $"\nProducer: {producer}" +
                $"\nPrice: {price}";
            return result;
        }
    }

    public struct Train
    {
        public Train(int num)
        {
            Random random = new Random();
            destination = Lessons.Lesson_Instruments.GetSomeText(10, ' ', ',', '.');
            trainNumber = num.ToString();
            string hour = random.Next(0, 24).ToString();
            string minute = random.Next(0, 60).ToString();

            shippingTime = $"{(hour.Length == 1? "0"+hour : hour)}:{(minute.Length == 1 ? "0" + minute : minute)}";
        }

        public string destination;
        public string trainNumber;
        public string shippingTime;

        public string GetInfo()
        {
            string res =
                $"Destination: {destination}{Lessons.Lesson_Instruments.emptyString}" +
                $"\nNumber: {trainNumber}" +
                $"\nShipping time: {shippingTime}";
            return res;
        }
    }

    public class MyClass
    {
        public string change;
    }
    public struct MyStruct
    {
        public string change;
    }

    public static class Text
    {
        public static void Print(string line, int color)
        {
            var list = Enum.GetValues(typeof(ConsoleColor));
            ConsoleColor[] colors = new ConsoleColor[list.Length];
            list.CopyTo(colors, 0);

            for (int i = 0; i < colors.Length; i++)
            {
                if((int)colors[i] == color)
                {
                    Console.ForegroundColor = colors[i];
                    Console.WriteLine("Result: " + line);
                    Console.ForegroundColor = ConsoleColor.Green;
                }
            }
        }
    }

    public class Accauntant
    {
        public Accauntant(Post post)
        {
            Random random = new Random();
            currentPost = post;
            workTime = random.Next((int)currentPost - 40, (int)currentPost + 40);
        }
        public Accauntant()
        {
            Random random = new Random();
            var list = Enum.GetValues<Post>();
            currentPost = list[random.Next(0, list.Length - 1)];
            workTime = random.Next((int)currentPost - 40, (int)currentPost + 40);
        }
        Post currentPost;
        int bonusThisMonth = 0;
        int workTime = 0;
        public bool AskForBonus(Post post, int hour)
        {
            int time = hour - (int)post;
            int payPerHour = 10;

            switch (time)
            {
                case >= 5 and < 10:
                    payPerHour = 20;
                    break;
                case >= 10 and < 25:
                    payPerHour = 30;
                    break;
                case >= 25:
                    payPerHour = 50;
                    break;
            }

            if (time > 0)
            {
                bonusThisMonth = time * payPerHour;
                return true;
            }
            else return false;
        }

        public  string ShowBonus()
        {
            AskForBonus(currentPost, workTime);
            string res =
                $"Post: {currentPost}\n" +
                $"Work time: {workTime}/{(int)currentPost}\n" +
                $"Bonus: {bonusThisMonth}$";
            return res;
        }
    }
    public enum Post
    {
        CEO = 160,
        DeputyDirector = 150,
        Manager = 145,
        ChiefDispatcher = 140,
        Lawyer = 135,
        Dispatcher = 130,
        GeneralManagerSecretary = 128,
        DepartmentSecretary = 125,
        Receptionist = 120,
    }
}
