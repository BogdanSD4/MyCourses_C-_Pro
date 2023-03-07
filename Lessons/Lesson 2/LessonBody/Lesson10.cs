using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ClassesOfLesson10;

namespace Lessons.LessonBody
{
    class Lesson10 : ILesson
    {
        public void Open()
        {
            Calculate();
            Temperature();
            StringCounter();
            ArraySort();
            Coordinates();
            ILesson.UserRequest();
        }

        private void Calculate()
        {
            Console.CursorVisible = false;
            int x = Console.CursorLeft;
            int y = Console.CursorTop;
            bool exit = false;
            string result = "";
            char lastSym = ' ';
            bool punktAllow = true;
            bool isPunkt = false;

            while (!exit)
            {
                Console.SetCursorPosition(x, y);
                Console.WriteLine("\n(\"Enter\" - result | \"E\" - exit | \"C\" - clear)\n");
                Console.Write("> " + result + Lesson_Instruments.emptyString);
                string[] allowNum = new string[] { "1", "2", "3", "4", "5", "6", "7", "8", "9", "0"};
                string[] allowSym = new string[] { "+", "-", "/", "*", "." };

                var sym = Console.ReadKey(true);

                if (allowSym.Contains($"{sym.KeyChar}") || allowNum.Contains($"{sym.KeyChar}"))
                {
                    switch (sym.KeyChar)
                    {
                        case '+' or '-' or '/' or '*':
                            if (allowNum.Contains($"{lastSym}"))
                            {
                                result += " " + sym.KeyChar + " ";
                                lastSym = sym.KeyChar;
                                punktAllow = true;
                            }
                            break;
                        case '.':
                            if (punktAllow && allowNum.Contains($"{lastSym}"))
                            {
                                punktAllow = false;
                                isPunkt = true;
                                result += sym.KeyChar;
                                lastSym = sym.KeyChar;
                            }
                            break;
                        default:
                            result += sym.KeyChar;
                            lastSym = sym.KeyChar;
                            break;
                    }
                }
                else if (sym.Key == ConsoleKey.Backspace)
                {
                    if (result != "")
                    {
                        do
                        {
                            if (result[result.Length - 1] == '.')
                            {
                                punktAllow = true;
                                isPunkt = false;
                            }
                            if(allowNum.Contains($"{result[result.Length - 1]}"))
                            {
                                result = result.Remove(result.Length - 1);
                                if (result != "")
                                {
                                    lastSym = result[result.Length - 1];
                                }
                                else lastSym = ' ';
                                break;
                            }
                            else if(allowSym.Contains($"{result[result.Length - 1]}"))
                            {
                                result = result.Remove(result.Length - 1);
                                if (result != "")
                                {
                                    lastSym = result[result.Length - 1];
                                }
                                else lastSym = ' ';
                                if (isPunkt) punktAllow = false;
                            }
                            else
                            {
                                result = result.Remove(result.Length - 1);
                            }

                            if (result == "")
                            {
                                lastSym = ' ';
                                break;
                            }
                            lastSym = result[result.Length - 1];
                        }
                        while (!allowNum.Contains($"{result[result.Length - 1]}"));
                    }
                }
                else if(sym.Key == ConsoleKey.Enter)
                {
                    while(!allowNum.Contains($"{result[result.Length - 1]}"))
                    {
                        result = result.Remove(result.Length - 1);
                    }
                    string res = result.Calculate().ToString().Replace(',','.');
                    result = res;
                    lastSym = result[result.Length - 1];
                    if (result.Contains(".")) punktAllow = false;
                    else punktAllow = true;
                }
                else if (sym.Key == ConsoleKey.E)
                {
                    exit = true;
                    Console.WriteLine("\n");
                }
                else if (sym.Key == ConsoleKey.C)
                {
                    result = "";
                }
            }

            Console.CursorVisible = true;
        }
        private void Temperature()
        {
            Console.CursorVisible = false;
            int x = Console.CursorLeft;
            int y = Console.CursorTop;
            bool exit = false;

            string arrow = "==> ";
            string arrowEmpty = "   ";
            string space = "                                 ";
            int vertical = 0;
            float celsius = 0;
            float fahrenheit = 0;

            float koefBase = 0.1f;
            float koef = 0.1f;

            float gameTimer = 0;
            float timer = 0;
            float timerLimit = 5;
            float multiplyKoef = 10;
            float multiplyLimit = 30;
                
            while (!exit)
            {
                Console.SetCursorPosition(x, y);
                Console.WriteLine("\nControl - \"arrow button\" | \"C\" - to zero | \"E\" - exit\n");
                Console.WriteLine((vertical == 0 ? arrow : arrowEmpty) + "Celsius: " + celsius + space);
                Console.WriteLine((vertical == 1 ? arrow : arrowEmpty) + "Fahrenheit: " + fahrenheit + space);

                if (Console.KeyAvailable)
                {
                    var sym = Console.ReadKey(true).Key;

                    switch (sym)
                    {
                        case ConsoleKey.LeftArrow:
                            if (vertical == 0)
                            {
                                fahrenheit = Convector.CelsiusToFahrenheit((celsius -= koef).ToString());
                                celsius = MathF.Round(celsius, 2);
                            }
                            else if (vertical == 1)
                            {
                                celsius = Convector.FahrenheitToCelsius((fahrenheit -= koef).ToString());
                                fahrenheit = MathF.Round(fahrenheit, 2);
                            }
                            break;
                        case ConsoleKey.RightArrow:
                            if (vertical == 0)
                            {
                                fahrenheit = Convector.CelsiusToFahrenheit((celsius += koef).ToString());
                                celsius = MathF.Round(celsius, 2);
                            }
                            else if (vertical == 1)
                            {
                                celsius = Convector.FahrenheitToCelsius((fahrenheit += koef).ToString());
                                fahrenheit = MathF.Round(fahrenheit, 2);
                            }
                            break;
                        case ConsoleKey.UpArrow:
                            if (--vertical < 0) vertical = 1;
                            break;
                        case ConsoleKey.DownArrow:
                            if (++vertical > 1) vertical = 0;
                            break;
                        case ConsoleKey.C:
                            fahrenheit = 0;
                            celsius = 0;
                            break;
                        case ConsoleKey.E:
                            exit = true;
                            break;
                    }

                    if (timer >= timerLimit)
                    {
                        koef *= multiplyKoef;
                        timerLimit *= multiplyLimit;
                    }

                    timer += koef;
                    gameTimer = 0;
                }
                else
                {
                    if(gameTimer > 1500)
                    {
                        if(timer != 0)
                        {
                            timer = 0;
                            koef = koefBase;
                            timerLimit = 5;
                        }
                    }

                    gameTimer += 1;
                }
            }

            Console.WriteLine();
            Console.CursorVisible = true;
        }
        private void StringCounter()
        {
            Console.WriteLine();
            string text = ILesson.Read("Input text (min 4 symbol): ", (ref string res) => 
            {
                if (res.Length >= 4) return true;
                else return false;
            });
            uint index = ILesson.Read<uint>("Input start index: ");
            Console.WriteLine("Result: " + text.StrCount((int)index));
        }
        private void ArraySort()
        {
            Console.WriteLine();
            uint num = ILesson.Read<uint>("Input array length: ");

            var numbers = Extantions.GetArray((int)num);
            Console.Write("Array: ");
            for (int i = 0; i < numbers.Length; i++)
            {
                Console.Write($"[{numbers[i]}] ");
            }

            numbers = numbers.ArrayFilter();
            Console.Write("\nSort arrat: ");
            for (int i = 0; i < numbers.Length; i++)
            {
                Console.Write($"[{numbers[i]}] ");
            }
            Console.WriteLine();
        }
        private void Coordinates()
        {
            Console.WriteLine();
            uint count = ILesson.Read<uint>("Input points count (2 - 10): ", (ref string res) => 
            {
                int num = int.Parse(res);
                if (num >= 2 && num <= 10) return true;
                return false;
            });

            List<Point> points = new List<Point>();
            string[] arrInfo = new string[count];

            for (int i = 0; i < count; i++)
            {
                Point point = new Point();
                arrInfo[i] = point.ToString();
                points.Add(point);
            }

            InlineWriter(Console.CursorLeft, Console.CursorTop, arrInfo);

            Console.WriteLine("\n> Operator \"+\":");
            Point[] pointsCopy = new Point[points.Count];
            points.CopyTo(pointsCopy);

            for (int i = 1; i < pointsCopy.Length; i++)
            {
                 pointsCopy[0] += pointsCopy[i];
            }
            Console.WriteLine(pointsCopy[0].ToString());

            Console.WriteLine("\n> Operator \"-\":");
            points.CopyTo(pointsCopy);

            for (int i = 1; i < pointsCopy.Length; i++)
            {
                pointsCopy[0] -= pointsCopy[i];
            }
            Console.WriteLine(pointsCopy[0].ToString());

            Console.WriteLine("\n> Operator \"*\":");
            points.CopyTo(pointsCopy);

            for (int i = 1; i < pointsCopy.Length; i++)
            {
                pointsCopy[0] *= pointsCopy[i];
            }
            Console.WriteLine(pointsCopy[0].ToString());

            Console.WriteLine("\n> Operator \"/\":");
            points.CopyTo(pointsCopy);

            for (int i = 1; i < pointsCopy.Length; i++)
            {
                pointsCopy[0] /= pointsCopy[i];
            }
            Console.WriteLine(pointsCopy[0].ToString());

            Console.WriteLine("\n> Operator \"++\":");
            points.CopyTo(pointsCopy);
            arrInfo = new string[count];

            for (int i = 0; i < pointsCopy.Length; i++)
            {
                arrInfo[i] = pointsCopy[i]++.ToString();
            }
            InlineWriter(Console.CursorLeft, Console.CursorTop, arrInfo);

            Console.WriteLine("\n> Operator \"--\":");
            points.CopyTo(pointsCopy);
            arrInfo = new string[count];

            for (int i = 0; i < pointsCopy.Length; i++)
            {
                arrInfo[i] = pointsCopy[i]--.ToString();
            }
            InlineWriter(Console.CursorLeft, Console.CursorTop, arrInfo);

            void InlineWriter(int startX, int startY, params string[] text)
            {
                int maxLength = 0;

                for (int i = 0; i < text.Length; i++)
                {
                    
                    string res = "";
                    int indexY = startY;

                    for (int j = 0; j < text[i].Length; j++)
                    {
                        if (text[i][j] == '\n' || j == text[i].Length - 1)
                        {
                            if(j == text[i].Length - 1) res += text[i][j];
                            Console.SetCursorPosition(startX + maxLength, indexY);
                            Console.WriteLine(res);
                            res = "";
                            indexY++;
                            continue;
                        }
                        res += text[i][j];
                    }

                    maxLength += Task.Run<int>(() => 
                    {
                        int num = 0;
                        for (int j = 0; j < text[i].Length; j++)
                        {
                            if(text[i][j] == '\n')
                            {
                                num = 0;
                                continue;
                            }
                            num++;
                        }
                        return num;
                    }).Result;
                }
            }
        }
    }
}
namespace ClassesOfLesson10
{
    static class Calculator
    {
        private static DataTable dataTable = new DataTable(); 
        public static double Calculate(this string res)
        {
            double num = Convert.ToDouble(dataTable.Compute(res, string.Empty));
            return num;
        }
    }

    static class Convector
    {
        public static float CelsiusToFahrenheit(string temperatureCelsius)
        {
            return MathF.Round((float.Parse(temperatureCelsius) * 1.8f + 32), 2);
        }
        public static float FahrenheitToCelsius(string temperatureFahrenheit)
        {
            return MathF.Round((float.Parse(temperatureFahrenheit) - 30) / 1.8f, 2);
        }
    }

    static class StringExtension
    {
        public static int StrCount(this string str, int startIndex = 0)
        {
            return str.Length - startIndex;
        }
    }

    static class Extantions
    {
        public static int[] ArrayFilter(this int[] res)
        {
            List<int> newArr = new List<int>();
            int leng = res.Length;
       
            while(leng != newArr.Count)
            {
                int min = int.MaxValue;
                int index = 0;

                for (int i = 0; i < res.Length; i++)
                {
                    if(res[i] < min)
                    {
                        min = res[i];
                        index = i;
                    }
                }
                res[index] = int.MaxValue;

                newArr.Add(min);
            }

            return newArr.ToArray();
        } 

        public static int[] GetArray(int length)
        {
            string[] numbers = new string[] { "1", "2", "3", "4", "5", "6", "7", "8", "9", "0" };

            Random random = new Random();
            List<int> arr = new List<int>();

            for (int i = 0; i < length; i++)
            {
                int numLeng = random.Next(1, 5);
                string num = "";
                for (int j = 0; j < numLeng; j++)
                {
                    num += numbers[random.Next(0, numbers.Length - 1)];
                }
                arr.Add(int.Parse(num));
            }

            return arr.ToArray();
        }
    }

    class Point
    {
        public Point()
        {
            Random random = new Random();
            x = random.Next(-100, 100);
            y = random.Next(-100, 100);
            z = random.Next(-100, 100);
            hashcode = ++pointAmount;
        }
        public Point(float X, float Y, float Z)
        {
            x = X;
            y = Y;
            z = Z;
            hashcode = ++pointAmount;
        }
        public float x;
        public float y;
        public float z;
        private int hashcode;
        private static int pointAmount;

        public override string ToString()
        {
            string line = "---------------- ";
            string result = 
                $"{line}" +
                $"\nPoint hash: {hashcode}" +
                $"\n> x: {x}" +
                $"\n> y: {y}" +
                $"\n> z: {z}" +
                $"\n{line}";
            return result;
        }
        public override bool Equals(object obj)
        {
            try
            {
                var point = (Point)obj;

                if (point.x != x) return false;
                if (point.y != y) return false;
                if (point.z != z) return false;

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public override int GetHashCode()
        {
            return hashcode;
        }
        public static Point operator +(Point point1, Point point2)
        {
            var point = new Point(point1.x + point2.x, point1.y + point2.y, point1.z + point2.z);
            return point;
        }
        public static Point operator -(Point point1, Point point2)
        {
            var point = new Point(point1.x - point2.x, point1.y - point2.y, point1.z - point2.z);
            return point;
        }
        public static Point operator *(Point point1, Point point2)
        {
            var point = new Point(point1.x * point2.x, point1.y * point2.y, point1.z * point2.z);
            return point;
        }
        public static Point operator /(Point point1, Point point2)
        {
            var point = new Point(point1.x / point2.x, point1.y / point2.y, point1.z / point2.z);
            return point;
        }
        public static Point operator ++(Point point1)
        {
            var point = new Point(point1.x++, point1.y++, point1.z++);
            return point;
        }
        public static Point operator --(Point point1)
        {
            var point = new Point(point1.x--, point1.y--, point1.z--);
            return point;
        }
    }
}
