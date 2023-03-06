using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lessons.LessonBody
{
    class Lesson7 : ILesson
    {
        public void Open()
        {
            LessonClassRealisation();
            ILesson.UserRequest();
        }

        public void LessonClassRealisation()
        {
            Console.WriteLine();

            Adress adress = new Adress("Ukraine", "Kyiv", "Nauky", "80", index: "03083");
            Console.WriteLine(adress.GetFullAdress());

            Console.WriteLine();

            PussyCat pussyCat = new PussyCat("Foxy", 5, "Gray", "Green");
            Console.WriteLine(pussyCat.Voice(3));

            Console.WriteLine();

            BankAccount bankAccount = new BankAccount(DateTime.Parse("05.09.2006"), 15, 956000.45f);
            Console.WriteLine("Account exist: " + bankAccount.AccountExistsTime() + " days");
            Console.WriteLine("Deposit: " + bankAccount.ShowDeposit());
            Console.WriteLine("Deposit after 25 years: " + bankAccount.PercentCalculating(25) + " USD");

            Console.WriteLine();

            Triangle triangle = Triangle.RandParemeters;
            triangle.ShowSides();
            triangle.ShowPerimeterAndArea();

            Console.WriteLine();

            Figure.Triangle.PrintReult();
            Figure.Rectangle.PrintReult();
            Figure.Cube.PrintReult();

            Console.WriteLine();
        }

        class Adress
        {
            public Adress(
                string country = null,
                string city = null,
                string street = null,
                string house = null,
                string flat = null,
                string index = null)
            {
                Country = country;
                City = city;
                Street = street;
                House = house;
                Flat = flat;
                Index = index;
            }
            public string Country { get; private set; }
            public string City { get; private set; }
            public string Street { get; private set; }
            public string House { get; private set; }
            public string Flat { get; private set; }
            public string Index { get; private set; }

            public string GetFullAdress()
            {
                string result =
                    $"Country: {Country}\n"+
                    $"City: {City}\n"+
                    $"Street: {Street}\n"+
                    $"House: {House}\n"+
                    $"Flat: {Flat}\n"+
                    $"Index: {Index}";
                return result;
            }
        }
        class PussyCat
        {
            public PussyCat(
                string name = null,
                int age = 0,
                string colorWool = null,
                string colorEyes = null)
            {
                Name = name;
                Age = age;
                ColorWool = colorWool;
                ColorEyes = colorEyes;
            }
            public string Name { get; private set; }
            public float Age { get; private set; }
            public string ColorWool { get; private set; }
            public string ColorEyes { get; private set; }

            public string Voice(int count = 1)
            {
                string result = "";
                for (int i = 1; i <= count; i++)
                {
                    result += "Myu";
                    if (i != count) result += '-';
                }
                return result;
            }
        }
        class BankAccount
        {
            public BankAccount(
                DateTime accountOpen,
                int interestRate,
                float deposit)
            {
                AccountOpen = accountOpen;
                InterestRate = interestRate;
                Deposit = deposit;
            }
            public DateTime AccountOpen { get; private set; }
            public float InterestRate { get; private set; }
            public float Deposit { get; private set; }

            public int AccountExistsTime()
            {
                var time = DateTime.Now - AccountOpen;
                return time.Days;
            }
            public float PercentCalculating(int years)
            {
                return (float)(Deposit * Math.Pow((1 + (InterestRate / 100)), years));
            }
            public string ShowDeposit() => Deposit.ToString() + "USD";
        }
        class Triangle
        {
            public Triangle( float side1, float side2, float side3)
            {
                a = side1;
                b = side2;
                c = side3;
            }
            public float a { get; set; }
            public float b { get; set; }
            public float c { get; set; }

            public static Triangle RandParemeters { get 
                {
                    Random random = new Random();
                    float a = (float)random.Next(1, 500) * (float)random.NextDouble();
                    float b = (float)random.Next(1, 500) * (float)random.NextDouble();
                    float c = (float)random.Next(1, (int)MathF.Round(a + b)) * (float)random.NextDouble();

                    return new Triangle(a, b, c);
                }
            }
            public float Perimeter()
            {
                float result = (a + b + c) / 2;
                return result;
            }
            public static float Perimeter(float side1, float side2, float side3)
            {
                float result = (side1 + side2 + side3) / 2;
                return result;
            }

            public float Area()
            {
                float p = Perimeter() / 2;
                float result = MathF.Sqrt(p * (p - a) * (p - b) * (p - c));
                return result;
            }

            public string ShowSides()
            {
                string result =
                    $"A: {a}" +
                    $"\nB: {b}" +
                    $"\nC: {c}";
                return result;
            }
            public string ShowPerimeterAndArea()
            {
                string result =
                    $"Perimeter: {Perimeter()}\n" +
                    $"Area: {Area()}";
                return result;
            }
        }

        class Vector2
        {
            public Vector2(float posX, float posY, string vectorName = null)
            {
                x = posX;
                y = posY;
                name = vectorName;
            }

            private float x;
            private float y;
            private string name;

            public float X { get { return x; } set { x = value; } }
            public float Y { get { return y; } set { y = value; } }
            public string Name { get { return name; } set { name = value; } }

            public static Vector2 random { get 
                {
                    Random rand = new Random();
                    return new Vector2(
                        (float)rand.Next(-50, 50), 
                        (float)rand.Next(-50, 50));
                }
            }
        }
        class Figure
        {
            public Figure() { }
            public Figure(Vector2 arg1, Vector2 arg2, Vector2 arg3)
            {
                vectors = new Vector2[] { arg1, arg2, arg3};
                Name = "Triangle";
            }
            public Figure(Vector2 arg1, Vector2 arg2, Vector2 arg3, Vector2 arg4, string name = null)
            {
                vectors = new Vector2[] { arg1, arg2, arg3, arg4};
                if (name == null) Name = "Rectangle";
                else Name = name;
            }
            Vector2[] vectors;
            public string Name { get; private set; }

            public static Figure Triangle 
            {
                get 
                {
                    return new Figure(
                        Vector2.random,
                        Vector2.random,
                        Vector2.random);
                }
            }
            public static Figure Rectangle
            {
                get
                {
                    return new Figure(
                        Vector2.random,
                        Vector2.random,
                        Vector2.random,
                        Vector2.random);
                }
            }
            public static Figure Cube
            {
                get
                {
                    var vct = Vector2.random;
                    return new Figure(
                        vct,
                        new Vector2(vct.X, -vct.Y),
                        new Vector2(-vct.X, vct.Y),
                        new Vector2(-vct.X, -vct.Y),
                        "Cube");
                }
            }

            public float SideLenght(Vector2 side1, Vector2 side2)
            {
                return MathF.Sqrt(
                    (float)Math.Pow((side2.X - side1.X), 2) + 
                    (float)Math.Pow((side2.Y - side1.X), 2));
            }
            public float Perimeter()
            {
                float result = 0;
                for (int i = 0; i < vectors.Length; i++)
                {
                    if (i == vectors.Length - 1)
                    {
                        result += SideLenght(vectors[i], vectors[0]);
                    }
                    else
                    {
                        result += SideLenght(vectors[i], vectors[i + 1]);
                    }
                }
                return result;
            }

            public void PrintReult()
            {
                Console.WriteLine(
                $"Name: {Name}\n" +
                $"Perimeter: {Perimeter()}");
            }
        }
    }
}
