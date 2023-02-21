using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Lessons
{
    class Lesson2 : ILesson
    {
        public void Open()
        {
            ConvertMeterToCentimeter();
            FindCircleSquare();
            FindHypotenuse();
            FindSinOfAngle();
            MathOperation();
            FindSale();
            AgeCalculation();
            CalculationExample();
            CalculateSquareAndVolume();
            ILesson.UserRequest();
        }

        private static void CalculateSquareAndVolume()
        {
            try
            {
                var pi = Math.PI;
                Console.Write("Input radius: ");
                var radius = float.Parse(Console.ReadLine());
                Console.Write("Input height: ");
                var height = float.Parse(Console.ReadLine());
                Console.WriteLine($"Square = " +
                    $"{2 * pi * radius * (radius + height)}");
                Console.WriteLine($"Volume = " +
                    $"{pi * (radius * radius) * height}\n");
            }
            catch (Exception)
            {
                Console.WriteLine("Invalid input");
                CalculateSquareAndVolume();
            }
        }
        private static void CalculationExample()
        {
            var x = 10;
            var y = 12;
            var z = 3;
            Console.WriteLine(
                $"x = {x}" +
                $"\ny = {y}" +
                $"\nz = {z}" +
                $"\n\n(x += y - x++ * z) x = {x += y - x++ * z}" +
                $"\n(z = --x – y * 5) z = {z = --x - y * 5}" +
                $"\n(y /= x + 5 % z) y = {y /= x + 5 % z}" +
                $"\n(z = x++ + y * 5) z = {z = x++ + y * 5}" +
                $"\n(x = y - x++ * z) x = {x = y - x++ * z}\n");
        }
        private static void AgeCalculation()
        {
            var projectName = Process.GetCurrentProcess().ProcessName;
            var filePath = Directory.GetParent(Directory.GetCurrentDirectory());

            while(filePath.Name != projectName)
            {
                filePath = filePath.Parent;
            }

            var path = filePath+"\\AgeCalculation\\bin\\Debug\\AgeCalculation.exe";
            Process.Start(path);
        }
        private static void FindSale()
        {
            try
            {
                Console.Write("Input price: ");
                var price = float.Parse(Console.ReadLine());
                Console.Write("Input sale(%): ");
                var sale = float.Parse(Console.ReadLine());
                Console.WriteLine($"Sale = " +
                    $"{(sale / 100) * price}\n");
            }
            catch (Exception)
            {
                Console.WriteLine("Invalid input");
                FindSale();
            }
        }
        private static void MathOperation()
        {
            var random = new Random();
            int arg1 = random.Next(1, 1000);
            int arg2 = random.Next(1, 1000);
            Console.Write($"Argument 1: {arg1}\nArgument 2: {arg2}\n");
            Console.WriteLine(
                $"\n{arg1} + {arg2} = {arg1 + arg2}" +
                $"\n{arg1} - {arg2} = {arg1 - arg2}" +
                $"\n{arg1} * {arg2} = {arg1 * arg2}" +
                $"\n{arg1} / {arg2} = {arg1 / arg2}" +
                $"\n{arg1} % {arg2} = {arg1 % arg2}\n");
        }
        private static void FindSinOfAngle()
        {
            try
            {
                Console.Write("Input angle: ");
                var angle = float.Parse(Console.ReadLine());
                Console.WriteLine($"Sin = " +
                    $"{Math.Sin(angle)}\n");
            }
            catch (Exception)
            {
                Console.WriteLine("Invalid input");
                FindSinOfAngle();
            }
        }
        private static void FindHypotenuse()
        {
            try
            {
                Console.Write("Input cathetus 1: ");
                var cathetus1 = float.Parse(Console.ReadLine());
                Console.Write("Input cathetus 2: ");
                var cathetus2 = float.Parse(Console.ReadLine());
                Console.WriteLine($"Hypotenuse = " +
                    $"{Math.Sqrt(cathetus1 * cathetus1 + cathetus2 * cathetus2)}\n");
            }
            catch (Exception)
            {
                Console.WriteLine("Invalid input");
                FindHypotenuse();
            }
        }
        private static void ConvertMeterToCentimeter()
        {
            Console.Write("Input meters count: ");
            var meters = 0.0f;
            try
            {
                meters = float.Parse(Console.ReadLine());
                Console.WriteLine($"It's {meters * 100} centimeter\n");
            }
            catch (Exception)
            {
                Console.WriteLine("Invalid input");
                ConvertMeterToCentimeter();
            }
        }
        private static void FindCircleSquare()
        {
            Console.Write("Input radius: ");
            var radius = 0.0f;
            var pi = Math.PI;
            try
            {
                radius = float.Parse(Console.ReadLine());
                Console.WriteLine($"Squere = {Math.Pow(pi * radius, 2)}\n");
            }
            catch (Exception)
            {
                Console.WriteLine("Invalid input");
                FindCircleSquare();
            }
        }
    }
}
