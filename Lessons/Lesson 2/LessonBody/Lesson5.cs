using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lessons
{
    class Lesson5 : ILesson
    {
        public void Open()
        {
            MiddleValue();
            GetMaxValue();
            UserAge();
            Calculator();
            Convertation();
            MultiplyByTen();
            RecursionCicle();
            RecursionCicleSum();
            NumberRate();
            ILesson.UserRequest();
        }

        private void MiddleValue()
        {
            try
            {
                char[] numbers = new char[] { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };
                Console.Write("Enter numbers separated by space: ");
                string result = Console.ReadLine();

                int sum = 0;
                int count = 0;
                string currentNum = "";

                for (int i = 0; i < result.Length; i++)
                {
                    if (result[i] == ' ') result = result.Remove(0);
                    else break;
                }

                for (int i = 0; i < result.Length; i++)
                {
                    if (result[i] == ' ') continue;
                    else if (result[i] == '-')
                    {
                        if (i != result.Length - 1)
                        {
                            if (numbers.Contains(result[i + 1]))
                            {
                                currentNum += result[i];
                            }
                            else
                            {
                                Console.WriteLine("Invalid value");
                                GetMaxValue();
                                return;
                            }
                        }
                        else
                        {
                            break;
                        }
                    }
                    else if (numbers.Contains(result[i]))
                    {
                        currentNum += result[i];
                        if(i != result.Length - 1)
                        {
                            if (result[i + 1] == ' ')
                            {
                                sum += int.Parse(currentNum);
                                currentNum = "";
                                count++;
                            }
                        }
                        else
                        {
                            sum += int.Parse(currentNum);
                            count++;
                        }
                    }
                    else
                    {
                        Console.WriteLine("Invalid value");
                        MiddleValue();
                        return;
                    }
                }
                Console.WriteLine("Result: "+ sum / count);
            }
            catch (Exception)
            {
                Console.WriteLine("Invalid value");
                MiddleValue();
            }
        }
        private void GetMaxValue()
        {
            try
            {
                char[] numbers = new char[] { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };
                Console.Write("Enter numbers separated by space (Use \",\" with float value): ");
                string result = Console.ReadLine();

                float max = int.MinValue;
                float min = int.MaxValue;
                string currentNum = "";

                for (int i = 0; i < result.Length; i++)
                {
                    if (result[i] == ' ') result = result.Remove(0);
                    else break;
                }

                for (int i = 0; i < result.Length; i++)
                {
                    if (result[i] == ' ') continue;
                    else if (result[i] == ',' || result[i] == '-')
                    {
                        if (i != result.Length - 1)
                        {
                            if (numbers.Contains(result[i + 1]))
                            {
                                currentNum += result[i];
                            }
                            else
                            {
                                Console.WriteLine("Invalid value");
                                GetMaxValue();
                                return;
                            }
                        }
                        else
                        {
                            break;
                        }
                    }
                    else if (numbers.Contains(result[i]))
                    {
                        currentNum += result[i];
                        if (i != result.Length - 1)
                        {
                            if (result[i + 1] == ' ')
                            {
                                float num = float.Parse(currentNum);
                                if (num > max) max = num;
                                else if (num < min) min = num;

                                currentNum = "";
                            }
                        }
                        else
                        {
                            float num = float.Parse(currentNum);
                            if (num > max) max = num;
                            else if (num < min) min = num;
                        }
                    }
                    else
                    {
                        Console.WriteLine("Invalid value");
                        GetMaxValue();
                        return;
                    }
                }
                Console.WriteLine("Max: " + max + "\nMin: " + min);
            }
            catch (Exception)
            {
                Console.WriteLine("Invalid value");
                GetMaxValue();
            }
        }
        private void UserAge()
        {
            string date = ILesson.Read("Enter date of your birthday (Format DD.MM.YY): ", (ref string res) => 
            {
                try
                {
                    var date = DateTime.Parse(res);

                    Console.WriteLine($"You born {(DateTime.Now.Year - date.Year) - 1} years ago");
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            });
            
        }
        private void Calculator()
        {
            int arg1 = ILesson.Read<int>("Argument 1: ");
            char sign = ILesson.Read<char>("Sign: ", (ref string res) => 
            {
                char[] signs = new char[] { '-', '+', '/', '*'};
                if (!signs.Contains(res[0])) return false;
                else return true;
            });
            int arg2 = ILesson.Read<int>("Argument 2: ");

            switch (sign)
            {
                case '*': 
                    Multiply();
                    break;
                case '+':
                    Plus();
                    break;
                case '-':
                    Minus();
                    break;
                case '/':
                    Divide();
                    break;
                default:
                    break;
            }

            void Plus()
            {
                Console.WriteLine("Result: " + (arg1 + arg2));
            }
            void Minus()
            {
                Console.WriteLine("Result: " + (arg1 - arg2));
            }
            void Multiply()
            {
                Console.WriteLine("Result: " + (arg1 * arg2));
            }
            void Divide()
            {
                Console.WriteLine("Result: " + (arg1 - arg2));
            }
        }
        private void Convertation()
        {
            Console.WriteLine("Convert UAN to USD");
            float rate = ILesson.Read<float>("Exchange rate: ", (ref string res) => { return res.Contains('-') ? false : true; });
            float cash = ILesson.Read<float>("Cash in UAN: ", (ref string res) => { return res.Contains('-') ? false : true; });

            Console.WriteLine("Result: " + (cash / rate) + " USD");
        }
        private void MultiplyByTen()
        {
            List<int> list = new List<int>();

            Console.WriteLine("Multiplyer (Enter \"exit\" to get result): ");
            for (int i = 0; i < int.MaxValue; i++)
            {
                int num = 0;
                try
                {
                    num = int.Parse(ILesson.Read($"Argumetrt {i + 1}: ", (ref string res) =>
                    {
                        if (res == "exit")
                        {
                            Multiply(ref list);
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

            void Multiply(ref List<int> list)
            {
                Console.Write("Result: ");
                for (int i = 0; i < list.Count; i++)
                {
                    Console.Write(list[i] * 10 + " ");
                }
                Console.ReadLine();
            }
        }
        private void FindAreaAndPerimeter()
        {
            float side1 = ILesson.Read<float>("Enter side1: ", (ref string res) => { return res.Contains('-') ? false : true; });
            float side2 = ILesson.Read<float>("Enter side2: ", (ref string res) => { return res.Contains('-') ? false : true; });

            float perimeter = side1 * 2 + side2 * 2;
            float area = side1 * side2;

            Console.WriteLine($"Perimeter: {perimeter}\nArea: {area}");
        }
        private void RecursionCicle()
        {
            int N = ILesson.Read<int>("Enter repeat amount: ", (ref string res) => { return res.Contains('-') ? false : true; });
            int num = 0;

            Recursion();

            Console.WriteLine(num);

            void Recursion()
            {
                num++;
                if (num >= N) return;
                Recursion();
            }
        }
        private void RecursionCicleSum()
        {
            int N = ILesson.Read<int>("Enter value: ", (ref string res) => { return res.Contains('-') ? false : true; });
            int num = 0;

            Recursion(1);

            Console.WriteLine(num);

            void Recursion(int number)
            {
                num += number++;
                if (number >= N) return;
                Recursion(number);
            }
        }
        private void NumberRate()
        {
            int N = Math.Abs(ILesson.Read<int>("Enter value: "));
            Console.WriteLine("Rate: "+N.ToString().Length);
        }
    }
}
