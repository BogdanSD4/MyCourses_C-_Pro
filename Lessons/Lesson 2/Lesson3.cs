using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Lessons
{
    class Lesson3 : ILesson
    {
        public void Open()
        {
            MoreLess();
            DiscountCounter();
            FindQuarterOfAnHour();
            NumIsEven();
            Quadratic();
            IntercalaryYear();
            MathOperations();
            FindNumberInterval();
            TextTranslator();
            PremiumCounter();
            MathOperationsResultConvertToByte();
            FindCircleRadiusAndSquare();
            ILesson.UserRequest();
        }

        private void MoreLess()
        {
            try
            {
                Console.Write("Input your value: ");
                float value;
                value = float.Parse(Console.ReadLine());
                var result = "";
                switch (value)
                {
                    case > 10: result = $"{value} > 10: False";
                        break;
                    case < 10: result = $"{value} < 10: True";
                        break;
                    case 10: result = $"{value} is equal to 10";
                        break;
                }
                Console.WriteLine(result);
            }
            catch (Exception) 
            { 
                Console.WriteLine("Invalid value");
                MoreLess();
            }
        }
        private void DiscountCounter()
        {
            try
            {
                Console.Write("Input price: ");
                float price = float.Parse(Console.ReadLine());
                Console.Write("Input goods count: ");
                int goodsCount = int.Parse(Console.ReadLine());
                float discount;
                switch (goodsCount)
                {
                    case > 0 and <= 3:
                        discount = 10;
                        break;
                    case > 3 and <= 7:
                        discount = 20;
                        break;
                    case > 7: 
                        discount = 25;
                        break;
                    default: 
                        Console.Write("Error...Try again");
                        DiscountCounter();
                        return;

                }
                Console.WriteLine($"Your discount is {(goodsCount*price)*(discount/100)}");
            }
            catch (Exception)
            {
                Console.WriteLine("Invalid value");
                DiscountCounter();
            }
        }
        private void FindQuarterOfAnHour()
        {
            try
            {
                Console.Write("Input time (0 - 59): ");
                float min;
                min = float.Parse(Console.ReadLine());
                int quarter;
                switch (min)
                {
                    case >= 0 and < 15:
                        quarter = 1;
                        break;
                    case >= 15 and < 30:
                        quarter = 2;
                        break;
                    case >= 30 and < 45:
                        quarter = 3;
                        break;
                    case >= 45 and <= 59:
                        quarter = 4;
                        break;
                    default:
                        Console.WriteLine("Error...Try again");
                        FindQuarterOfAnHour();
                        return;
                }
                Console.WriteLine($"Quarter: {quarter}");
            }
            catch (Exception)
            {
                Console.WriteLine("Invalid value");
                FindQuarterOfAnHour();
            }
        }
        private void NumIsEven()
        {
            try
            {
                Console.Write("Input num (Intager): ");
                int value = int.Parse(Console.ReadLine());
                var result = "";

                if(value % 2 == 0) result += "Num is even";
                else result += "Num is odd";

                if(value % 3 == 0) result += "\nNum is divisible by 3";
                else result += "\nNum isn't divisible by 3";

                if (value % 6 == 0) result += "\nNum is divisible by 6";
                else result += "\nNum isn't divisible by 6";

                Console.WriteLine(result);
            }
            catch (Exception)
            {
                Console.WriteLine("Invalid value");
                NumIsEven();
            }
        }
        private void Quadratic()
        {
            try
            {
                Console.Write("Input a: ");
                float a = float.Parse(Console.ReadLine());
                Console.Write("Input b: ");
                float b = float.Parse(Console.ReadLine());
                Console.Write("Input c: ");
                float c = float.Parse(Console.ReadLine());
                Console.WriteLine($"Quadratic: {a}x^2 + {b}x + {c} = 0");

                var D = b*b - 4 * a * c;
                var result = "";

                if (D > 0)
                {
                    var sqrt = Math.Sqrt(D);
                    result = $"x1 = {(-b + sqrt) / (2 * a)}\nx2 = {(-b - sqrt) / (2 * a)}";
                }
                else if (D == 0)
                {
                    var sqrt = Math.Sqrt(D);
                    result = $"x = {-b / (2 * a)}";
                }
                else result = "No roots";

                Console.WriteLine(result);
            }
            catch (Exception)
            {
                Console.WriteLine("Invalid value");
                Quadratic();
            }
        }
        private void IntercalaryYear()
        {
            try
            {
                Console.Write("Input year: ");
                uint a = uint.Parse(Console.ReadLine());

                var result = false;

                if(a % 4 == 0)
                {
                    result = true;
                    if(a % 100 == 0)
                    {
                        result = false;
                        if (a % 400 == 0) result = true;
                    }
                }

                Console.WriteLine("IntercalaryYear: " + result);
            }
            catch (Exception)
            {
                Console.WriteLine("Invalid value: ");
                IntercalaryYear();
            }
        }
        private void MathOperations()
        {
            try
            {
                var random = new Random();
                var operand1 = (float)random.Next(0, 1000);
                var operand2 = (float)random.Next(0, 1000);
                Console.WriteLine($"{operand1} ? {operand2} = x");
                Console.Write("Input sign(?) ( + | - | / | * ): ");
                string a = Console.ReadLine();
                var result = "";
                switch (a)
                {
                    case "+": 
                        result = $"x = {operand1 + operand2}";
                        break;
                    case "-":
                        result = $"x = {operand1 - operand2}";
                        break;
                    case "/":
                        result = $"x = {operand1 / operand2}";
                        break;
                    case "*":
                        result = $"x = {operand1 * operand2}";
                        break;
                    default:
                        Console.WriteLine("Sign not correct");
                        MathOperations();
                        return;
                }

                Console.WriteLine(result);
            }
            catch (Exception)
            {
                Console.WriteLine("Invalid value");
                MathOperations();
            }
        }
        private void FindNumberInterval()
        {
            try
            {
                Console.Write("Input value");
                float a = float.Parse(Console.ReadLine());
                var result = "";
                switch (a)
                {
                    case >= 0 and <= 14:
                        result = "Interval: [0 - 14]";
                        break;
                    case >= 15 and <= 35:
                        result = "Interval: [15 - 35]";
                        break;
                    case >= 36 and <= 50:
                        result = "Interval: [36 - 50]";
                        break;
                    case >= 51 and <= 100:
                        result = "Interval: [50 - 100]";
                        break;
                    default:
                        Console.WriteLine("Value is out of intervals");
                        FindNumberInterval();
                        return;
                }

                Console.WriteLine(result);
            }
            catch (Exception)
            {
                Console.WriteLine("Invalid value");
                FindNumberInterval();
            }
        }
        private void TextTranslator()
        {
            try
            {
                Console.WriteLine("Translator: " +
                    "\nI translate any word or phrase on english" +
                    "\nIf you want out of translator write \"exit\" in \"Your request\"");

                var lang = false;
                while (!lang)
                {
                    Console.Write("Choose language: ua(1) | ru(0):");
                    var language = Console.ReadLine();
                    switch (language)
                    {
                        case "1":
                            Translator.Language = "ukrainian";
                            lang = true;
                            break;
                        case "0":
                            Translator.Language = "russian";
                            lang = true;
                            break;
                        default:
                            Console.WriteLine("Num is not correct");
                            break;
                    }
                }
                

                var exit = "";
                while (exit != "/exit")
                {
                    Console.Write("Your request: ");
                    string text = Console.ReadLine();

                    if (text == "exit")
                    {
                        exit = "/exit";
                        continue;
                    }

                    var result = Translator.Translate(text);
                    Console.WriteLine(result);
                }
            }
            catch (Exception)
            {
                Console.WriteLine("Invalid value");
                FindNumberInterval();
            }
        }
        private void PremiumCounter()
        {
            try
            {
                Console.Write("Input salary: ");
                float salary = float.Parse(Console.ReadLine());
                Console.Write("Years of service: ");
                int years = int.Parse(Console.ReadLine());
                var premium = 0f;
                switch (years)
                {
                    case > 0 and < 5:
                        premium = 10;
                        break;
                    case >= 5 and < 10:
                        premium = 15;
                        break;
                    case >= 10 and < 15:
                        premium = 25;
                        break;
                    case >= 15 and < 20:
                        premium = 35;
                        break;
                    case >= 20 and < 25:
                        premium = 45;
                        break;
                    case >= 25:
                        premium = 50;
                        break;
                    default:
                        Console.WriteLine("Value is out of intervals");
                        PremiumCounter();
                        return;
                }

                Console.WriteLine($"Worker premium is {salary * (premium / 100)}");
            }
            catch (Exception)
            {
                Console.WriteLine("Invalid value");
                PremiumCounter();
            }
        }
        private void MathOperationsResultConvertToByte()
        {
            byte a = 10;
            byte b = 20;

            var result =
                $"{a} + {b} = {a + b}" +
                $"\n{a} - {b} = {a - b}" +
                $"\n{a} * {b} = {a * b}" +
                $"\n{a} / {b} = {a / b}" +
                $"\n{a} % {b} = {a % b}";

            Console.WriteLine(result);
        }
        private void FindCircleRadiusAndSquare()
        {
            try
            {
                Console.Write("Input L: ");
                float length = float.Parse(Console.ReadLine());

                var radius = length / (2 * Math.PI);
                var square = Math.PI * (radius * radius);

                var result =
                    $"Radius = {radius}" +
                    $"\nSquare = {square}";

                Console.WriteLine(result);
            }
            catch (Exception)
            {
                Console.WriteLine("Invalid value");
                FindCircleRadiusAndSquare();
            }
        }
    }
}
