using ClassesOfLesson10;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Lessons.LessonBody
{
    internal class Lesson19 : ILesson
    {
        public void Open()
        {
            Temperature();
            Reflector();
        }

        private void Temperature()
        {
            Console.CursorVisible = false;
            int x = Console.CursorLeft;
            int y = Console.CursorTop;
            bool exit = false;

            string arrow = "==> ";
            string arrowEmpty = "   ";
            int vertical = 0;

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

                var cel = Type.GetType("ClassesOfLesson19.Converter").GetField("celsius").GetValue(null);
                var fah = Type.GetType("ClassesOfLesson19.Converter").GetField("fahrenheit").GetValue(null);

                Console.WriteLine((vertical == 0 ? arrow : arrowEmpty) + "Celsius: " + cel + new string(' ', 30));
                Console.WriteLine((vertical == 1 ? arrow : arrowEmpty) + "Fahrenheit: " + fah + new string(' ', 30));

                if (Console.KeyAvailable)
                {
                    var sym = Console.ReadKey(true).Key;

                    switch (sym)
                    {
                        case ConsoleKey.LeftArrow:
                            if (vertical == 0)
                            {
                                var method = Type.GetType("ClassesOfLesson19.Converter").GetMethod("CelsiusToFahrenheit");
                                method.Invoke(null, new object[] { koef, false });
                            }
                            else if (vertical == 1)
                            {
                                var method = Type.GetType("ClassesOfLesson19.Converter").GetMethod("FahrenheitToCelsius");
                                method.Invoke(null, new object[] { koef, false });
                            }
                            break;
                        case ConsoleKey.RightArrow:
                            if (vertical == 0)
                            {
                                var method = Type.GetType("ClassesOfLesson19.Converter").GetMethod("CelsiusToFahrenheit");
                                method.Invoke(null, new object[] { koef, true });
                            }
                            else if (vertical == 1)
                            {
                                var method = Type.GetType("ClassesOfLesson19.Converter").GetMethod("FahrenheitToCelsius");
                                method.Invoke(null, new object[] { koef, true });
                            }
                            break;
                        case ConsoleKey.UpArrow:
                            if (--vertical < 0) vertical = 1;
                            break;
                        case ConsoleKey.DownArrow:
                            if (++vertical > 1) vertical = 0;
                            break;
                        case ConsoleKey.C:
                            var met = Type.GetType("ClassesOfLesson19.Converter").GetMethod("Reset");
                            met.Invoke(null, null);
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
                    if (gameTimer > 1500)
                    {
                        if (timer != 0)
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
        private void Reflector()
        {
            Console.WriteLine();

            Assembly assembly = Assembly.GetExecutingAssembly();
            Console.WriteLine("Assembly Name: {0}", assembly.FullName);
            Console.WriteLine("Assembly Version: {0}", assembly.GetName().Version);

            Type[] types = assembly.GetTypes();
            foreach (Type type in types)
            {
                Console.WriteLine("Type Name: {0}", type.Name);
            }
        }
    }
}
namespace ClassesOfLesson19
{
    public static class Converter
    {
        public static float celsius = 0;
        public static float fahrenheit = 0;

        public static void CelsiusToFahrenheit(float koef, bool plus)
        {
            var num = 0f;
            if (plus)
            {
                num = (celsius += koef);
            }
            else num = (celsius -= koef);

            fahrenheit = MathF.Round((num * 1.8f) + 32, 2);
            celsius = MathF.Round(celsius, 2);
        }
        public static void FahrenheitToCelsius(float koef, bool plus)
        {
            var num = 0f;
            if (plus)
            {
                num = (fahrenheit += koef);
            }
            else num = (fahrenheit -= koef);

            celsius = MathF.Round((num - 30) / 1.8f, 2);
            fahrenheit = MathF.Round(fahrenheit, 2);
        }

        public static void Reset()
        {
            celsius = 0;
            fahrenheit = 0;
        }
    }
}
