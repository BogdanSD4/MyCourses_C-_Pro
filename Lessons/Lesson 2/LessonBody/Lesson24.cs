using ClassesOfLesson24;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Lessons.LessonBody
{
    public class Lesson24 : ILesson
    {
        int width;
        public void Open()
        {
            width = Console.WindowWidth;
            RecursionMatrix();
            ThreadMatrix();
        }

        private void RecursionMatrix()
        {
            Console.WriteLine();
            Console.WindowWidth = 70;

            float speed = 50;
            int lineCount = (int)ILesson.Read<uint>("Input line count (> 1000): ");
            Console.WriteLine("(Input \"Enter\" to exit)");
            Console.CursorVisible = false;

            bool exit = false;
            int x = Console.CursorLeft;
            int y = Console.CursorTop;

            Thread thread = new Thread(() => 
            {
                int lineCountMax = 1000;
                Random random = new Random();

                var signs = Lesson_Instruments.numbers.ToCharArray().Concat(Lesson_Instruments.alphabet.ToCharArray()).ToArray();

                Recursion(lineCount);

                while (lineCount > 0)
                {
                    FrameHandler.action.Invoke();
                    Thread.Sleep((int)(1000 / speed));
                }

                void Recursion(int lineLeft)
                {
                    if (exit) return;
                    
                    int buffer = Console.WindowWidth - 1;
                    int length = random.Next(3, 10);
                    Line line = new Line(random.Next(buffer), y);

                    int index = 1;
                    if (FrameHandler.action != null)
                    {
                        index = FrameHandler.action.GetInvocationList().Length;
                    }

                    Action action = () =>
                    {
                        char sym;
                        if (length-- > 0)
                        {
                            sym = signs[random.Next(signs.Length)];
                        }
                        else sym = default;

                        if (!line.AddCharAndDraw(sym))
                        {
                            var list = FrameHandler.action.GetInvocationList();
                            if (index < list.Length)
                            {
                                FrameHandler.action -= (Action)list[index];
                                lineCount--;
                            }
                        }
                    };

                    FrameHandler.FrameAction += action;
                    FrameHandler.action.Invoke();

                    Thread.Sleep((int)(1000 / speed));

                    if (lineLeft > 0) Recursion(--lineLeft);
                }
            });
            thread.Start();

            while (!exit)
            {
                if (!Console.KeyAvailable) continue;
                var sym = Console.ReadKey(true);

                if (sym.Key == ConsoleKey.Enter)
                {
                    exit = true;
                    lineCount = 0;
                }
            }

            while (!exit && lineCount > 0) { }

            thread.Join();

            EndMethod(x, y);
        }
        private void ThreadMatrix()
        {
            Console.WriteLine();
            Console.WindowWidth = 70;
            SemaphoreSlim semaphoreSlim = new SemaphoreSlim(1, 1);

            int lineCount = (int)ILesson.Read<uint>("Input line count (> 1000): ");
            Console.WriteLine("(Input \"Enter\" to exit)");

            Random random = new Random();
            var signs = Lesson_Instruments.numbers.ToCharArray().Concat(Lesson_Instruments.alphabet.ToCharArray()).ToArray();

            int x = Console.CursorLeft;
            int y = Console.CursorTop;

            List<Thread> threads = new List<Thread>();

            for (int i = 0; i < lineCount; i++)
            {
                int buffer = Console.WindowWidth - 1;
                int length = random.Next(3, 10);
                Line line = new Line(random.Next(buffer), y);
                object block = new object();

                Thread thread = new Thread(() =>
                {
                    lock (block)
                    {
                        bool goOut = false;
                        int lg = length;
                        while (!goOut)
                        {
                            semaphoreSlim.Wait();
                            char sym;
                            if (lg-- > 0)
                            {
                                sym = signs[random.Next(signs.Length)];
                            }
                            else sym = default;

                            if (!line.AddCharAndDraw(sym))
                            {
                                goOut = true;
                            }
                            semaphoreSlim.Release();

                            Thread.Sleep(100);
                        }
                    }
                });

                threads.Add(thread);
            }

            foreach (Thread thread in threads)
            {
                thread.Start();
                Thread.Sleep(100);
            }

            while (true)
            {
                foreach (Thread thread in threads)
                {
                    thread.Join();
                }
                break;
            }

            EndMethod(x, y);
        }

        private void EndMethod(int x, int y)
        {
            Thread.Sleep(500);
            Lesson_Instruments.Clear(width - y, y);

            Console.SetCursorPosition(0, 0);
            Console.SetCursorPosition(x, y);

            Console.CursorVisible = true;
            Console.ForegroundColor = ConsoleColor.Green;
        }
    }
}
namespace ClassesOfLesson24
{
    public class FrameHandler
    {
        public static Action action = default;
        public static event Action FrameAction 
        { add { action += value; } remove { action -= value; } }
    }
    public class Line
    {
        public int x;
        public int y;

        public List<char> line = new List<char>();

        public Line(int X, int Y)
        {
            x = X;
            y = Y;
        }

        public bool AddCharAndDraw(char newChar = default, Action action = null)
        {
            if (newChar != default)
            {
                line.Add(newChar);
            }

            return Draw();
        }

        public bool Draw()
        {
            int selfY = y + line.Count;
            if (y >= Console.WindowHeight) return false;

            for (int i = 0; i < line.Count; i++)
            {
                if (selfY >= Console.WindowHeight)
                {
                    Interlocked.Decrement(ref selfY);
                    continue;
                }

                Console.SetCursorPosition(x, selfY);
                if (i == 0)
                {
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write(line[i]);
                }
                else if (i == 1)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write(line[i]);
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.DarkGreen;
                    Console.Write(line[i]);
                }

                Interlocked.Decrement(ref selfY);

                if(i == line.Count - 1)
                {
                    Console.SetCursorPosition(x, selfY);
                    Console.Write(' ');
                }
            }

            y++;

            return true;
        }
    }
}
