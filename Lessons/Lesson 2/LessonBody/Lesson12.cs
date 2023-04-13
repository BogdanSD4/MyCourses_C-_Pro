using ClassesOfLesson12;
using Lessons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lessons.LessonBody
{
    class Lesson12 : ILesson
    {
        public void Open()
        {
            AnotherCalculator();
            WorkerReviews();
            FoodMarket();
        }

        private void AnotherCalculator()
        {
            Console.WriteLine("\n(\"Enter\" - to get result)");
            string num1 = "";
            string num2 = "";
            string[] buttons = new string[] { "Add", "Sub", "Mul", "Div", "Exit" };
            string currentButton = "";

            int x = Console.CursorLeft;
            int y = Console.CursorTop;
            int vertical = 0;
            int buttHoriz = 0;

            string numbers = "0123456789";
            string arrow = "==> ";
            string arrowEmpty = "   ";
            int lastLine = 0;

            bool exit = false;

            while (!exit)
            {
                Console.SetCursorPosition(x, y);

                Console.WriteLine((vertical == 0? arrow : arrowEmpty) + (num1 == ""? "0" : num1) + Lesson_Instruments.emptyString);
                Console.WriteLine((vertical == 1 ? arrow : arrowEmpty) + (num2 == "" ? "0" : num2) + Lesson_Instruments.emptyString);
                Console.Write(vertical == 2 ? arrow : arrowEmpty);
                for (int i = 0; i < buttons.Length; i++)
                {
                    if(buttHoriz == i)
                    {
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.Write("[" + buttons[i] + "] ");
                        currentButton = buttons[i];
                        Console.ForegroundColor = ConsoleColor.Green;
                    }
                    else
                    {
                        Console.Write("[" + buttons[i] + "] ");
                    }
                }
                Console.WriteLine();

                if (lastLine == 0) lastLine = Console.CursorTop;

                var sym = Console.ReadKey(true);
                if (numbers.Contains(sym.KeyChar))
                {
                    if (vertical == 0)
                    {
                        num1 += sym.KeyChar;
                        if (num1 == "0") num1 = "";
                    }
                    else if (vertical == 1)
                    {
                        num2 += sym.KeyChar;
                        if (num2 == "0") num2 = "";
                    }
                    continue;
                }

                switch (sym.Key)
                {
                    case ConsoleKey.LeftArrow:
                        if(vertical == 2)
                        {
                            if (--buttHoriz < 0) buttHoriz = buttons.Length - 1;
                        }
                        break;
                    case ConsoleKey.UpArrow:
                        if (--vertical < 0) vertical = 2;
                        break;
                    case ConsoleKey.RightArrow:
                        if (vertical == 2)
                        {
                            if (++buttHoriz > buttons.Length - 1) buttHoriz = 0;
                        }
                        break;
                    case ConsoleKey.DownArrow:
                        if (++vertical > 2) vertical = 0;
                        break;
                    case ConsoleKey.Backspace:
                        if(vertical == 0 && num1 != "")
                        {
                            num1 = num1.Remove(num1.Length - 1);
                        }
                        else if(vertical == 1 && num2 != "")
                        {
                            num2 = num2.Remove(num2.Length - 1);
                        }
                        break;
                    case ConsoleKey.Enter:
                        if(currentButton == "Exit")
                        {
                            exit = true;
                            break;
                        }
                        
                        var calcul = new Calculator(
                            float.Parse(num1 == ""? "0" : num1), 
                            float.Parse(num2 == ""? "0" : num2));
                        calcul.GetType().GetMethod(currentButton).Invoke(calcul, null);
                        lastLine = Console.CursorTop;
                        Lesson_Instruments.Clear(1);

                        break;
                }
            }
            Console.SetCursorPosition(0, lastLine + 1);
        }
        private void WorkerReviews()
        {
            Console.WriteLine("Our company based in 1988 year\n");

            int counter = 0;
            int workersCount = 5;
            List<Worker> workers = new List<Worker>(workersCount);
            
            bool exit = false;
            
            int x = Console.CursorLeft;
            int y = Console.CursorTop;

            while (!exit)
            {
                Console.SetCursorPosition(x, y);
                Console.WriteLine($"Review #{counter + 1}" + Lesson_Instruments.emptyString);
                int clearPoint = Console.CursorTop;
                Lesson_Instruments.Clear(6);

                var wr = new Worker();
                wr.FillReview();
                workers.Add(wr);
                
                counter++;
                if(counter == workersCount)
                {
                    exit = true;
                    workers = workers.ToArray().OrderBy(x => x.lastName).ToList();
                    Lesson_Instruments.Clear(6, clearPoint - 1);
                    continue;
                }
            }

            string[] allReviews = new string[workers.Count];
            List<string> invalidReview = new List<string>();
            for (int i = 0; i < workers.Count; i++)
            {
                allReviews[i] = workers[i].Info();
                if(workers[i].yearOfEmployment == 0)
                {
                    invalidReview.Add(allReviews[i]);
                }
            }

            Console.SetCursorPosition(x, y);
            Console.WriteLine("==> All workers: ");
            Lesson_Instruments.InlineWriter(text: allReviews, space: 5);
            Console.WriteLine("\n==> Workers with invalid review");
            Lesson_Instruments.InlineWriter(typeof(ReviewException), 5, invalidReview.ToArray());

            int Filter(Worker worker)
            {
                int res = 0;
                string currentWorker = worker.lastName;

                for (int i = 0; i < workers.Count; i++)
                {
                    string name = workers[i].lastName;
                    for (int j = 0; j < name.Length; j++)
                    {
                        if (j > currentWorker.Length - 1) break;
                        int w1 = AlphabetIndex(currentWorker[j]);
                        int w2 = AlphabetIndex(name[j]);

                        if (w1 == w2) continue;
                        else if (w1 > w2) break;
                        else
                        {
                            return i;
                        }
                    }
                    res++;
                }

                return res;

                int AlphabetIndex(char sym)
                {
                    string alpha = Lesson_Instruments.alphabet;
                    for (int i = 0; i < alpha.Length; i++)
                    {
                        if(sym == alpha[i])
                        {
                            return i;
                        }
                    }
                    return 0;
                }
            }
        }
        private void FoodMarket()
        {
            Console.WriteLine("Welcome to Food&Fruit\n");

            int counter = 0;
            int priceCount = 2;
            List<Price> prices = new List<Price>(priceCount);

            bool exit = false;

            int x = Console.CursorLeft;
            int y = Console.CursorTop;

            while (!exit)
            {
                Console.SetCursorPosition(x, y);
                Console.WriteLine($"Product #{counter + 1}" + Lesson_Instruments.emptyString);
                int clearPoint = Console.CursorTop;
                Lesson_Instruments.Clear(6);

                var wr = new Price();
                wr.FillReview();
                prices.Add(wr);

                counter++;
                if (counter == priceCount)
                {
                    exit = true;
                    prices = prices.ToArray().OrderBy(x => x.marketName).ToList();
                    Lesson_Instruments.Clear(6, clearPoint - 1);
                    continue;
                }
            }

            string[] allReviews = new string[prices.Count];
            List<string> invalidReview = new List<string>();
            for (int i = 0; i < prices.Count; i++)
            {
                allReviews[i] = prices[i].Info();
                if (prices[i].price == -1)
                {
                    invalidReview.Add(allReviews[i]);
                }
            }

            Console.SetCursorPosition(x, y);
            Console.WriteLine("\n==> All workers: ");
            Lesson_Instruments.InlineWriter(text: allReviews, space: 5);
            Console.WriteLine("\n==> Workers with invalid review");
            Lesson_Instruments.InlineWriter(typeof(PriceException), 5, invalidReview.ToArray());
        }
    }
}
namespace ClassesOfLesson12
{
    public class Calculator
    {
        private float arg1;
        private float arg2;
        public Calculator(float num1, float num2)
        {
            arg1 = num1;
            arg2 = num2;
        }
        public void Add()
        {
            Console.WriteLine("Add: " + (arg1 + arg2) + Lessons.Lesson_Instruments.emptyString);
        }
        public void Sub()
        {
            Console.WriteLine("Subtract: " + (arg1 - arg2) + Lessons.Lesson_Instruments.emptyString);
        }
        public void Mul()
        {
            Console.WriteLine("Multiply: " + (arg1 * arg2) + Lessons.Lesson_Instruments.emptyString);
        }
        public void Div()
        {
            if (arg2 == 0) new MyException("You can't divide by zero");
            Console.WriteLine("Divide: " + (arg1 / arg2) + Lessons.Lesson_Instruments.emptyString);
        }
    }
    public class MyException : Exception
    {
        public MyException(string excption)
        {
            Console.WriteLine(excption);
        } 
    }

    public struct Worker
    {
        public string lastName;
        public string initials;
        public string post;
        public int yearOfEmployment;

        public void FillReview()
        {
            lastName = ILesson.Read("Last Name: ");
            initials = ILesson.Read("Initials (BD): ");
            post = ILesson.Read("Post: ");
            Console.Write("Year of employment: ");
            try
            {
                yearOfEmployment = int.Parse(Console.ReadLine());
                if(yearOfEmployment < 1988 || yearOfEmployment > DateTime.Now.Year)
                {
                    yearOfEmployment = 0;
                }
            }
            catch (Exception)
            {
                yearOfEmployment = 0;
            }
        }
        public string Info()
        {
            string init = "";
            try
            {
                init = $"{initials[0]}.{initials[1]}.";
            }
            catch (Exception)
            {
                init = "---"; 
            }
            string res =
                $"Name: {lastName} {init}" +
                $"\nPost: {post}" +
                $"\nYear of employment: {(yearOfEmployment == 0 ? "invalid" : yearOfEmployment)}";
            return res;
        }
    }
    public class ReviewException : Exception
    {
        public ReviewException()
        {
            Console.WriteLine("Exception: Failed");
        }
    }

    public struct Price
    {
        public string name;
        public string marketName;
        public int price;

        public void FillReview()
        {
            name = ILesson.Read("Product name: ");
            marketName = ILesson.Read("Market name: ");
            Console.Write("Price: ");
            try
            {
                price = int.Parse(Console.ReadLine());
            }
            catch (Exception)
            {
                price = -1;
            }
        }
        public string Info()
        {
            string res =
                $"Product name: {name}" +
                $"\nMarket name: {marketName}" +
                $"\nPrice: {(price == -1 ? "invalid" : price)}";
            return res;
        }
    }
    public class PriceException : Exception
    {
        public PriceException()
        {
            Console.WriteLine("Exception: price in not correct");
        }
    }
}
