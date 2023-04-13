using ClassesOfLesson15;
using ClassesOfLesson16;
using Lessons;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Lessons.LessonBody
{
    class Lesson16 : ILesson
    {
        public void Open()
        {
            LessonController();
        }

        private void LessonController()
        {
            string text =
                "Controll of Lesson 16 occurs with the help of [NUM PAD numbers]" +
                "\n[1] - Task 1"+
                "\n[2] - Task 2"+
                "\n[3] - Task 3"+
                "\n[4] - Task 4"+
                "\n[0] - Exit\n" +
                "\n<Choose task>";

            Console.WriteLine(text);
            Console.CursorVisible = false;

            var callback = new Callback();
            callback.Update();

            Console.CursorVisible = true;
        }
    }
}
namespace ClassesOfLesson16
{
    public class Callback
    {
        public Callback()
        {
            coursorPosX = Console.CursorLeft;
            coursorPosY = Console.CursorTop - 1;

            action += (clear) => { Lesson_Instruments.Clear(clearAmount, coursorPosY); };

            action += (key) => 
            {
                if (key != ConsoleKey.NumPad1) return;
                
                Random random = new Random();
                int num1 = random.Next(10, 1000);
                int num2 = random.Next(10, 1000);
                int num3 = random.Next(10, 1000);
                ArithmeticMean arithmetic = delegate (int a, int b, int c) { return (a + b + c) / 3; };
                Console.WriteLine(
                    $"> Task 1: Num1 = {num1}, Num2 = {num2}, Num3 = {num3}\n" +
                    $"> Result: {arithmetic(num1, num2, num3)}");

                isInvoked = true;
            };
            action += (key) => 
            {
                if (key != ConsoleKey.NumPad2) return;

                Random random = new Random();
                int controllerH = 0;
                bool activeAction = false;
                float result = 0;
                bool taskExit = false;

                float num1 = random.Next(10, 10000);
                float num2 = random.Next(10, 10000);

                (string, Action)[] button = new (string, Action)[] 
                  {("[Sum]", () => 
                  {
                      Func<float, float, float> func = (x, y) => { return x + y; };
                      result = func.Invoke(num1, num2); 
                  }),
                   ("[Sub]", () =>
                  {
                      Func<float, float, float> func = (x, y) => { return x - y; };
                      result = func.Invoke(num1, num2);
                  }),
                   ("[Mult]", () =>
                  {
                      Func<float, float, float> func = (x, y) => { return x * y; };
                      result = func.Invoke(num1, num2);
                  }),
                   ("[Div]", () =>
                  {
                      Func<float, float, float> func = (x, y) => { return x / y; };
                      result = func.Invoke(num1, num2);
                  }), 
                   ("[Exit]", () => 
                   {
                       activeAction = false;
                       taskExit = true; 
                   })};               

                Console.WriteLine($"> Task 2: Num1 = {num1}, Num2 = {num2}" + Lessons.Lesson_Instruments.emptyString);
                DrawUI();

                while (!taskExit)
                {
                    var sign = Console.ReadKey(true);
                    switch (sign.Key)
                    {
                        case ConsoleKey.LeftArrow:
                            if (--controllerH < 0) controllerH = button.Length - 1;
                            break;
                        case ConsoleKey.RightArrow:
                            if (++controllerH >= button.Length) controllerH = 0;
                            break;
                        case ConsoleKey.Enter:
                            activeAction = true;
                            break;
                    }
                    Console.SetCursorPosition(coursorPosX, coursorPosY + 1);
                    DrawUI();
                    if (activeAction)
                    {
                        Console.WriteLine($"\n> Result: {result}");
                        activeAction = false;
                    }
                }

                void DrawUI()
                {
                    for (int i = 0; i < button.Length; i++)
                    {
                        Write(button[i].Item1, i, button[i].Item2);
                    }
                    void Write(string text, int index, Action action)
                    {
                        if(index == controllerH)
                        {
                            Console.ForegroundColor = ConsoleColor.White;
                            Console.Write(text+" ");
                            Console.ForegroundColor = ConsoleColor.Green;
                            if(activeAction && action != null)
                            {
                                action.Invoke();
                            }
                        }
                        else
                        {
                            Console.Write(text+" ");
                        }
                    }
                }
            };
            action += (key) =>
            {
                if (key != ConsoleKey.NumPad3) return;

                Lesson_Instruments.Clear(5);

                int count = (int)ILesson.Read<uint>("> Task 3: Input delegates count (0 < x < 10000): ", (ref string res) => 
                {
                    int num = int.Parse(res);
                    if (num > 0 && num <= 10000) return true;
                    return false;
                }, true);
                Thread.Sleep(100);

                int posY = Console.CursorTop;
                Random random = new Random();
                MyList<ReturnAction<int>> valueList = new MyList<ReturnAction<int>>(); 
                for (int i = 0; i < count; i++)
                {
                    Console.SetCursorPosition(0, posY);
                    int num1 = random.Next(10, 100000);
                    int num2 = random.Next(10, 100000);
                    int num3 = random.Next(10, 100000);
                    Console.Write(
                    $"> ({i+1}): Num1 = {num1}, Num2 = {num2}, Num3 = {num3}" + Lesson_Instruments.emptyString);
                    Thread.Sleep(1);
                    valueList.Add(delegate () 
                    {
                        ArithmeticMean mean = delegate (int a, int b, int c) { return (a + b + c) / 3; };
                        return mean.Invoke(num1, num2, num3);
                    });
                }

                Func<IMyList<ReturnAction<int>>, int> result = delegate (IMyList<ReturnAction<int>> arithmetics) 
                {
                    int result = 0;

                    for (int i = 0; i < arithmetics.Count; i++)
                    {
                        result += arithmetics[i].Invoke();
                    }
                    return result / arithmetics.Count;
                };

                Console.WriteLine($"\n> Result: " + result.Invoke(valueList));

                isInvoked = true;
            };
            action += (key) =>
            {
                if (key != ConsoleKey.NumPad4) return;

                Lesson_Instruments.OpenWPF("timer");

                isInvoked = true;
            };

            action += (key) => 
            {
                if (key != ConsoleKey.NumPad0) return;

                systemExit = true;
                isInvoked = true;
            };
        }
        private Action<ConsoleKey> action = default;
        private bool systemExit = false;
        private bool isInvoked = false;
        private int coursorPosX;
        private int coursorPosY;
        private const int clearBase = 5;
        private int clearAmount = clearBase;
        public void Update()
        {
            while (!systemExit)
            {
                Console.SetCursorPosition(coursorPosX, coursorPosY);
                action.Invoke(Console.ReadKey(true).Key);
                if (!isInvoked)
                {
                    Lesson_Instruments.Clear(clearAmount, coursorPosY);
                    Console.WriteLine("<Choose task>");
                    clearAmount = clearBase;
                }
                else
                {
                    isInvoked = false;
                }
            }
        }
    }

    public delegate int ArithmeticMean(int arg1, int arg2, int arg3);
    public delegate T ReturnAction<T>();
}
