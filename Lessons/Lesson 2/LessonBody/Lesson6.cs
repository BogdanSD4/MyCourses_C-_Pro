using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lessons.LessonBody
{
    class Lesson6 : ILesson
    {
        public void Open()
        {
            CreateAndPrintArray();
            CreateAndPrintArrayRevers();
            EnterAndWriteArray();
            WordRevers();
            CountProfit();
            ArrayAction();
            CutArray();
            IncreaseArraySize();
            ArrayTable();
        }

        private void CreateAndPrintArray()
        {
            object[] arr = new object[5];
            string text = "";
            int length = 5;
            for (int i = 0; i < length; i++)
            {
                GetElement(out arr[i]);
                text += arr[i];
                if (i != length - 1) text += ", ";
            }
            Console.WriteLine("\nYour array: " + text);

            void GetElement(out object obj)
            {
                Random random = new Random();
                switch (random.Next(0, 3))
                {
                    case 0:
                        obj = random.Next(0, 1000);
                        break;
                    case 1:
                        obj = (float)Math.Round(random.NextDouble() * 1000, 3);
                        break;
                    case 2:
                        string[] symbol = new string[] { "q", "w", "r", "y", "t", "e", "u", "i", "o", "s", "a", "p", "g", "f", "d" };
                        int count = random.Next(1, 5);
                        string result = "";

                        for (int i = 0; i < count; i++)
                        {
                            result += symbol[random.Next(0, symbol.Length - 1)];
                        }

                        obj = result;
                        break;
                    default:
                        obj = "Error";
                        break;
                }
            }
        }
        private void CreateAndPrintArrayRevers()
        {
            object[] arr = new object[5];
            string text = "";
            int length = 5;
            for (int i = length - 1; i >= 0; i--)
            {
                GetElement(out arr[i]);
                text += arr[i];
                if (i != 0) text += ", ";
            }
            Console.WriteLine("\nYour array: " + text);

            void GetElement(out object obj)
            {
                Random random = new Random();
                switch (random.Next(0, 3))
                {
                    case 0:
                        obj = random.Next(0, 1000);
                        break;
                    case 1:
                        obj = (float)Math.Round(random.NextDouble() * 1000, 3);
                        break;
                    case 2:
                        string[] symbol = new string[] { "q", "w", "r", "y", "t", "e", "u", "i", "o", "s", "a", "p", "g", "f", "d" };
                        int count = random.Next(1, 5);
                        string result = "";

                        for (int i = 0; i < count; i++)
                        {
                            result += symbol[random.Next(0, symbol.Length - 1)];
                        }

                        obj = result;
                        break;
                    default:
                        obj = "Error";
                        break;
                }
            }
        }
        private void EnterAndWriteArray()
        {
            Console.WriteLine("Enter array: ");
            int length = 4;
            string arrB = "";
            for (int i = 0; i < length; i++)
            {
                arrB = $"Array B[{length - 1 - i}]: " + ILesson.Read($"Array A[{i}]: ") + "\n" + arrB;
            }
            Console.WriteLine("\n" + arrB);
        }
        private void WordRevers()
        {
            string text = "";
            Console.Write("Enter word: ");
            text = Console.ReadLine();
            Console.WriteLine("Word revers: " + text.ReversThis());
        }
        private void CountProfit()
        {
            string key = ILesson.ReadKey("Enter profit during 12 month (\"R\" - random | \"M\" - manually): ", (ref ConsoleKey res) =>
            {
                if (res == ConsoleKey.M || res == ConsoleKey.R) return true;
                else return false;
            });

            float[] profitMonth = new float[12];

            switch (key)
            {
                case "R":
                    Random random = new Random();
                    for (int i = 0; i < profitMonth.Length; i++)
                    {
                        profitMonth[i] = random.Next(-500, 5000);
                    }
                    break;
                case "M":
                    Console.WriteLine();
                    for (int i = 0; i < profitMonth.Length; i++)
                    {
                        profitMonth[i] = ILesson.Read<float>($"Month_{i+1}: ");
                    }
                    break;
            }

            int serchMonthFirst = 0;
            int serchMonthLast = 0;

            ILesson.Read("\nSearch range (Example: 3 - 6): ", (ref string res) =>
            {
                string current = "";
                for (int i = 0; i < res.Length; i++)
                {
                    if (res[i] == ' ') continue;
                    else if (res[i] == '-')
                    {
                        try
                        {
                            serchMonthFirst = int.Parse(current);
                            if (serchMonthFirst > 12 || serchMonthFirst < 1) return false;
                            current = "";
                        }
                        catch (Exception)
                        {
                            return false;
                        }
                    }
                    else current += res[i];

                    if (i == res.Length - 1)
                    {
                        try
                        {
                            serchMonthLast = int.Parse(current);
                            if (serchMonthLast > 12 || serchMonthLast < 1) return false;
                            else if (serchMonthFirst > serchMonthLast) return false;
                            return true;
                        }
                        catch (Exception)
                        {
                            return false;
                        }
                    }
                }
                return false;
            });

            float min = int.MaxValue;
            float max = int.MinValue;
            int minMonth = 0;
            int maxMonth = 0;

            for (int i = serchMonthFirst; i < serchMonthLast; i++)
            {
                if (profitMonth[i] > max)
                {
                    max = profitMonth[i];
                    maxMonth = i;
                }
                else if (profitMonth[i] < min)
                {
                    min = profitMonth[i];
                    minMonth = i;
                }
            }

            Console.WriteLine(
                $"Max profit:\n==> month_{maxMonth}\n==> profit: {max}\n" +
                $"Min profit:\n==> month_{minMonth}\n==> profit: {min}");
        }
        private void ArrayAction()
        {
            int arrLength = ILesson.Read<int>("Array length: ", (ref string res) => { return res.Contains("-") ? false : true; });

            int max = int.MinValue;
            int min = int.MaxValue;
            int sum = 0;
            float middleValue = 0;
            string oddValue = "[ ";

            Random random = new Random();
            int[] arr = new int[arrLength];

            for (int i = 0; i < arrLength; i++)
            {
                arr[i] = random.Next(-5000, 5000);

                sum += arr[i];

                if (arr[i] % 2 != 0)
                {
                    oddValue += arr[i] + ", ";
                }

                if (arr[i] > max) max = arr[i];
                else if (arr[i] < min) min = arr[i];
            }

            oddValue += "]";
            middleValue = sum / arrLength;

            Console.WriteLine(
                $"Max value => {max}" +
                $"\nMin value => {min}" +
                $"\nArray sum => {sum}" +
                $"\nArithmetic mean => {middleValue}" +
                $"\nOdd value => {oddValue}");
        }
        private void CutArray()
        {
            List<int> list = new List<int>();

            Console.WriteLine("Enter array (Enter \"exit\" to continue): ");
            for (int i = 0; i < int.MaxValue; i++)
            {
                int num = 0;
                try
                {
                    num = int.Parse(ILesson.Read($"Argumetrt {i + 1}: ", (ref string res) =>
                    {
                        if (res == "exit")
                        {
                            return true;
                        }
                        else
                        {
                            try
                            {
                                int.Parse(res);
                                return true;
                            }
                            catch (Exception)
                            {

                                return false;
                            }
                        }
                    }));
                    list.Add(num);
                }
                catch (Exception)
                {
                    break;
                }
            }

            int index = ILesson.Read<int>("Enter index: ", (ref string res) =>
            {
                if (res.Contains("-")) return false;
                try
                {
                    int id = int.Parse(res);
                    if (id > list.Count - 1) return false;
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            });
            int count = ILesson.Read<int>("Enter count: ", (ref string res) =>
            {
                return res.Contains("-") ? false : true;
            });

            string result = "";
            NewArray(list.ToArray(), index, count);

            Console.WriteLine("New array: " + result);

            void NewArray(int[] arr, int id, int ct)
            {
                for (int i = 0; i < arr.Length; i++)
                {
                    if (i >= id - 1)
                    {
                        if (ct-- > 0)
                        {
                            result += arr[i];
                            if (i != arr.Length - 1 && ct != 0)
                            {
                                result += ", ";
                            }
                        }
                    }
                }
            }
        }
        private void IncreaseArraySize()
        {
            List<int> list = new List<int>();

            Console.WriteLine("Enter array (Enter \"exit\" to continue): ");
            for (int i = 0; i < int.MaxValue; i++)
            {
                int num = 0;
                try
                {
                    num = int.Parse(ILesson.Read($"Argumetrt {i + 1}: ", (ref string res) =>
                    {
                        if (res == "exit")
                        {
                            return true;
                        }
                        else
                        {
                            try
                            {
                                int.Parse(res);
                                return true;
                            }
                            catch (Exception)
                            {

                                return false;
                            }
                        }
                    }));
                    list.Add(num);
                }
                catch (Exception)
                {
                    break;
                }
            }

            int value = ILesson.Read<int>("Enter value: ");

            Console.WriteLine("New array: " + NewArray(list.ToArray(), value));

            string NewArray(int[] arr, int value)
            {
                int[] newArr = new int[arr.Length + 1];
                newArr[0] = value;

                for (int i = 0; i < arr.Length; i++)
                {
                    newArr[i + 1] = arr[i];
                }

                return newArr.ArrayTo();
            }
        }
        private void ArrayTable()
        {
            Console.WriteLine("\nWARNING: if X != Y diagonal sum not found!!!");
            int langX = ILesson.Read<int>("Enter array length X: ", (ref string res) => { return res.Contains("-") ? false : true; });
            int langY = ILesson.Read<int>("Enter array length Y: ", (ref string res) => { return res.Contains("-") ? false : true; });
            

            int[][] arr = Task.Run<int[][]>(() =>
            {
                List<int[]> list = new List<int[]>();
                Random random = new Random();
                int diagonal = 0;
                int horrSum = 0;
                int[] vertSum = new int[langX + 1];

                for (int i = 0; i < langY; i++)
                {
                    int[] lt = new int[langX + 1];
                    for (int j = 0; j < langX; j++)
                    {
                        lt[j] = random.Next(-50, 50);

                        horrSum += lt[j];
                        if (j == langX - 1) lt[langX] = horrSum;

                        vertSum[j] += lt[j];
                        if (i == j && langX == langY) diagonal += lt[j];
                        if (i == langY - 1 && j == langX - 1) vertSum[vertSum.Length - 1] = diagonal;
                    }
                    list.Add(lt);
                    if (i == langY - 1)
                    {
                        list.Add(vertSum);
                    }
                }

                return list.ToArray();

            }).Result;
            int maxValueSize = Task.Run<int>(() => 
            {
                int max = 0;
                for (int i = 0; i < arr.Length; i++)
                {
                    for (int j = 0; j < arr[i].Length; j++)
                    {
                        int size = arr[i][j].ToString().Length;
                        if (size > max) max = size;
                    }
                }
                return max;
            }).Result;

            CreateTable(arr);

            var repeat = ILesson.ReadKey("Repeat last task? (Y - yes | N - no): ", (ref ConsoleKey res) => 
            {
                if (res == ConsoleKey.Y || res == ConsoleKey.M) return true;
                else return false;
            });
            if (repeat == "Y") ArrayTable();

            void CreateTable(int[][] arr)
            {
                char across = '+';
                char horr = '-';
                char vert = '|';

                for (int i = 0; i < arr.Length; i++)
                {
                    HorrizontalLine(arr[i].Length);
                    for (int j = 0; j < arr[i].Length; j++)
                    {
                        Console.Write(vert + " ");

                        string value = arr[i][j].ToString();
                        int lang = value.Length;

                        for (int space = 0; space < maxValueSize - lang; space++) Console.Write(" ");
                        if(j == i && langX == langY) 
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.Write(value + " ");
                            Console.ForegroundColor = ConsoleColor.Green;
                        }
                        else if(j == arr[i].Length - 1 || i == arr.Length - 1)
                        {
                            Console.ForegroundColor = ConsoleColor.White;
                            Console.Write(value + " ");
                            Console.ForegroundColor = ConsoleColor.Green;
                        }
                        else
                        {
                            Console.Write(value + " ");
                        }

                        if (j == arr[i].Length - 1) Console.WriteLine(vert);
                    }
                    if(i == arr.Length - 1) HorrizontalLine(arr[i].Length);
                }

                void HorrizontalLine(int count)
                {
                    for (int repeat = 0; repeat < count; repeat++)
                    {
                        Console.Write(across);
                        for (int h = 0; h < maxValueSize + 2; h++)
                        {
                            Console.Write(horr);
                        }
                    }
                    Console.WriteLine(across);
                }
            }
        }
    }
}
