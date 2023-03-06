using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lessons.LessonBody
{
    partial class Lesson4 : ILesson
    {
        public void Open()
        {
            TestCycle();
            MultiplicationTable();
            DivideResult();
            DepositAmount();
            Factorial();
            MaxCanEat();
            SquareDraw(() => { Console.WriteLine("Input parameters, if you want exit input 0"); });
            NumFibonacci();
            SumInteval();
            MathOperations(() => { Console.WriteLine($"Input value (a ? b = x)"); });
            Algoritm();
            TernaryOperation();
            AngleName();
            ILesson.UserRequest();
        }

        private void TestCycle()
        {
            Console.WriteLine();
            int num = 1;
            while (num != 6)
            {
                Console.Write(num + " ");
                int checkNum = num + 1;
                do
                {
                    Console.Write(num + " ");
                    for(int i = 0; i < 1; i++)
                    {
                        Console.WriteLine(num + " ");
                        num++;
                    }
                }
                while (num != checkNum);
            }
        }
        private void MultiplicationTable()
        {
            Console.WriteLine();

            int num = 31;
            int line = 0;
            Lesson_Instruments.FillSpaceArray(num);

            for (int i = 0; i < num; i++)
            {
                if (line == num) break;
                
                if(line == 0)
                {
                    Console.Write(Lesson_Instruments.GetSpace(i, i) + i + "  ");
                    if (i == num - 1)
                    {
                        line++;
                        i = 0;
                        Console.Write("\n");
                        Console.Write(Lesson_Instruments.GetSpace(i, line) + line + "  ");
                    }
                }
                else if(i != 0)
                {
                    Console.Write(Lesson_Instruments.GetSpace(i, i * line)+ i * line + "  ");
                    if (i == num - 1)
                    {
                        line++;
                        i = 0;
                        if (line != num)
                        {
                            Console.Write("\n");
                            Console.Write(Lesson_Instruments.GetSpace(i, line) + line + "  ");
                        }
                        else Console.WriteLine();
                    }
                }
            }
        }
        private void DivideResult()
        {
            Console.WriteLine();

            for (int i = 20; i < 50; i++)
            {
                if(i % 3 == 0 && i % 5 != 0)
                {
                    Console.Write(i+" ");
                }
            }
        }
        private void DepositAmount()
        {
            try
            {
                Console.WriteLine();
                Console.Write("Input percent: ");
                var percent = float.Parse(Console.ReadLine());
                Console.Write("Input deposit: ");
                var deposit = float.Parse(Console.ReadLine());
                Console.Write("Input term (years): ");
                var term = float.Parse(Console.ReadLine());

                float result = deposit * (float)Math.Pow((1 + (percent/100)), term);

                Console.Write("Result: " + result);
            }
            catch (Exception)
            {
                Console.WriteLine("Invalid value");
                DepositAmount();
            }
        }
        private void Factorial()
        {
            try
            {
                Console.WriteLine();

                Console.Write("Input value: ");
                var value = uint.Parse(Console.ReadLine());

                int result = 0;

                for (int i = 1; i <= value; i++)
                {
                    result += i;
                }

                Console.Write("Result: " + result);
            }
            catch (Exception)
            {
                Console.WriteLine("Invalid value");
                Factorial();
            }
        }
        private void MaxCanEat()
        {
            try
            {
                Console.WriteLine();

                Console.Write("Input money have: ");
                var money = float.Parse(Console.ReadLine());
                if(money < 0)
                {
                    Console.WriteLine("Invalid value");
                    Factorial();
                    return;
                }
                Console.Write("Input ice-cream price: ");
                var price = float.Parse(Console.ReadLine());
                if (price < 0)
                {
                    Console.WriteLine("Invalid value");
                    Factorial();
                    return;
                }

                int iceEat = 0;

                while(money > price)
                {
                    money -= price;
                    iceEat++;
                }

                Console.Write("Money left: " + money);
                Console.Write("\nAmout of eaten ice-cream: " + iceEat);
            }
            catch (Exception)
            {
                Console.WriteLine("Invalid value");
                Factorial();
            }
        }
        private void SquareDraw(Action start = null)
        {
            Console.WriteLine();
            if (start != null) start.Invoke();
            try
            {
                Console.Write("Input width: ");
                var width = uint.Parse(Console.ReadLine());
                if (width < 0)
                {
                    Console.WriteLine("Invalid value");
                    Factorial();
                    return;
                }
                else if (width == 0) return;
                Console.Write("Input height: ");
                var height = uint.Parse(Console.ReadLine());
                if (height < 0)
                {
                    Console.WriteLine("Invalid value");
                    Factorial();
                    return;
                }
                Console.Write("Input fill square yes(1) | no(0): ");
                bool fill;
                switch (Console.ReadLine())
                {
                    case "0": fill = false;
                        break;
                    case "1": fill = true;
                        break;
                    default:
                        Console.WriteLine("Invalid value");
                        SquareDraw();
                        return;
                }
                Console.WriteLine();

                uint line = 0;
                for (int i = 0; i < width; i++)
                {
                    if (fill)
                    {
                        Console.Write("* ");
                    }
                    else
                    {
                        if (line == 0 || line == height - 1 || i == 0)
                        {
                            Console.Write("* ");
                        }
                        else if (i == width - 1)
                        {
                            string res = "";
                            for (int j = 0; j < width - 2; j++)
                            {
                                res += "  ";
                            }
                            Console.Write(res+"* ");
                        }
                    }

                    if(i == width - 1)
                    {
                        i = -1;
                        line++;
                        Console.WriteLine();
                    }

                    if (line == height) break;
                }
                SquareDraw();
            }
            catch (Exception)
            {
                Console.WriteLine("Invalid value");
                SquareDraw();
            }
        }
        private void NumFibonacci()
        {
            try
            {
                Console.WriteLine();

                Console.Write("Input count: ");
                var count = uint.Parse(Console.ReadLine());
                if(count == 0)
                {
                    Console.WriteLine("Count must be more then 0");
                    NumFibonacci();
                }

                string result = "";
                int last = 1;
                int next = 0;

                for (int i = 0; i < count; i++)
                {
                    next = next + last;
                    result += next + " ";
                    last = next - last;
                }

                Console.Write("Result: " + result);
            }
            catch (Exception)
            {
                Console.WriteLine("Invalid value");
                NumFibonacci();
            }
        }
        private void SumInteval()
        {
            try
            {
                Console.WriteLine();

                Console.Write("Input A and B (A < B)\n");
                Console.Write("Input A: ");
                var a = int.Parse(Console.ReadLine());
                Console.Write("Input B: ");
                var b = int.Parse(Console.ReadLine());
                if (a > b)
                {
                    Console.WriteLine("Invalid value");
                    SumInteval();
                    return;
                }

                int sum = 0;
                string odd = "";
                for (int i = a; i < b; i++)
                {
                    sum += i;
                    if(i % 2 != 0)
                    {
                        odd += $"{i} ";
                    }
                }
                Console.Write("Sum: " + sum + "\nOdd numbers: " + odd);
            }
            catch (Exception)
            {
                Console.WriteLine("Invalid value");
                SumInteval();
            }
        }
        private void MathOperations(Action start = null)
        {
            Console.WriteLine();
            if (start != null) start.Invoke();
            try
            {
                Console.Write($"Input a: ");
                var a = float.Parse(Console.ReadLine());
                Console.Write($"Input b: ");
                var b = float.Parse(Console.ReadLine());
                if(b == 0)
                {
                    Console.Write("You can't divide by zero");
                    MathOperations();
                    return;
                }
                Console.Write("Input sign(?) ( + | - | / | * )(0 - exit): ");
                string sign = Console.ReadLine();
                var result = "";
                switch (sign)
                {
                    case "+":
                        result = $"x = {a + b}";
                        break;
                    case "-":
                        result = $"x = {a - b}";
                        break;
                    case "/":
                        result = $"x = {a / b}";
                        break;
                    case "*":
                        result = $"x = {a * b}";
                        break;
                    case "0":
                        return;
                    default:
                        Console.WriteLine("Sign not correct");
                        MathOperations();
                        return;
                }

                Console.WriteLine("Result: "+result);
                MathOperations();
            }
            catch (Exception)
            {
                Console.WriteLine("Invalid value");
                MathOperations();
            }
        }
        private void Algoritm()
        {
            Console.WriteLine();
            try
            {
                Console.Write($"Input x: ");
                var x = double.Parse(Console.ReadLine());
                double y;

                if (x < -20)
                {
                    y = 3 * Math.Pow(x, 3);
                }
                else if (x <= 30)
                {
                    y = Math.Abs(x);
                }
                else y = 30;

                Console.Write("Result: x = " + y);
            }
            catch (Exception)
            {
                Console.WriteLine("Invalid value");
                Algoritm();
            }
        }
        private void TernaryOperation()
        {
            Console.WriteLine();
            try
            {
                Console.Write($"Input circle radius: ");
                var radius = float.Parse(Console.ReadLine());
                if (radius < 0)
                {
                    Console.Write("It can't be lass then zero");
                    TernaryOperation();
                    return;
                }
                Console.Write($"Input square side length: ");
                var side = float.Parse(Console.ReadLine());
                if (side < 0)
                {
                    Console.Write("It can't be lass then zero");
                    TernaryOperation();
                    return;
                }

                float squareArea = side * side;
                float circleArea = (float)Math.PI * radius * radius;

                Console.WriteLine("Result: " + 
                    (circleArea > squareArea? 
                    circleArea + "\nCircle area is greater then area of square" :
                    squareArea + "\nSquare area is greater then area of circle"));
            }
            catch (Exception)
            {
                Console.WriteLine("Invalid value");
                TernaryOperation();
            }
        }
        private void AngleName()
        {
            Console.WriteLine();
            try
            {
                Console.Write($"Input angle (0 - 360): ");
                var angle = float.Parse(Console.ReadLine());
                if (angle < 0 || angle > 360)
                {
                    Console.Write("Invalid value");
                    AngleName();
                    return;
                }

                var result = "";
                switch (angle)
                {
                    case 90:
                        result = "Angle is streight";
                        break;
                    case > 90:
                        result = "Angle is blunt";
                        break;
                    case < 90:
                        result = "Angle is sharp";
                        break;
                }

                Console.Write(result);
            }
            catch (Exception)
            {
                Console.WriteLine("Invalid value");
                AngleName();
            }
        }
    }
}
