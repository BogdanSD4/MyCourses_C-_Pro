using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Lessons.LessonBody
{
    public class Lesson25 : ILesson
    {
        public void Open()
        {
            //Asynchron();
            //PLINQ();
            Parallel();
        }

        private void Asynchron()
        {
            Console.WriteLine();
            int filesCount = (int)ILesson.Read<uint>("Input files quantity(<40): ", inline: true);
            SemaphoreSlim semaphoreSlim = new SemaphoreSlim(1, 1);

            int yMain = Console.CursorTop;
            Console.WriteLine(new string('\n', 2));

            Console.CursorVisible = false;
            int x = Console.CursorLeft;
            int y = Console.CursorTop;
            Random random = new Random();
            int downloadFiles = 0;

            Action<int, Action> downdoad = (row, callback) =>
            {
                object block = new object();

                lock (block)
                {
                    int fileWeight = random.Next(15, 400);
                    int load = 0;
                    int isDowload = 50;

                    while (Interlocked.Increment(ref load) <= isDowload)
                    {
                        semaphoreSlim.Wait();
                        Console.SetCursorPosition(x, row);
                        Console.Write($" {fileWeight}");
                        Console.SetCursorPosition(x + 5, row);
                        Console.Write('[');
                        for (int i = 1; i <= isDowload; Interlocked.Increment(ref i))
                        {
                            if (load < i)
                            {
                                Console.Write(' ');
                            }
                            else Console.Write('=');
                        }
                        Console.Write(']');

                        if (load < isDowload) Console.Write(new string(' ', 30));
                        else
                        {
                            Console.Write(" Download");
                            callback.Invoke();
                        }

                        semaphoreSlim.Release();
                        Thread.Sleep((int)(100 + fileWeight));
                    }
                }
            };

            List<Thread> threads = new List<Thread>();

            int count = 0;
            while(count != filesCount)
            {
                int current = count;
                threads.Add(new Thread(() =>
                {
                    Console.WriteLine(count);
                    downdoad.Invoke(y + current, () => { Interlocked.Increment(ref downloadFiles); });
                }));
                count++;
            }

            foreach (Thread thread in threads)
            {
                thread.Start();
            }

            while (true)
            {
                semaphoreSlim.Wait();
                Console.SetCursorPosition(x, yMain + 1);
                Console.Write($"> Main process: {random.Next(1000, 10000)}{new string(' ', 50)}");
                if (downloadFiles == filesCount)
                {
                    Console.SetCursorPosition(x, y + filesCount);
                    break;
                }
                semaphoreSlim.Release();
                Thread.Sleep(1000);
            }
        }
        private void PLINQ()
        {
            Random random = new Random();
            int[] ints = new int[random.Next(1000000)];

            for (int i = 0; i < ints.Length; i++)
            {
                ints[i] = random.Next(1000);
            }

            var query = ints.AsParallel().Where(x => x % 2 == 1).Select(x => $" {x} ").ToArray();
        }
        private void Parallel()
        {
            Console.WriteLine();
            Console.CursorVisible = false;
            int x = Console.CursorLeft;
            int y = Console.CursorTop;

            SemaphoreSlim semaphoreSlim = new SemaphoreSlim(1,1);
            TaskFactory factory = new TaskFactory();

            Action method1 = () =>
            {
                Draw("Method 1 ", y, 70);
            };

            Action method2 = () =>
            {
                Draw("Method 2 ", y + 1, 50);
            };

            factory.StartNew(method1);
            factory.StartNew(method2);

            while (true)
            {
                if (Console.KeyAvailable)
                {
                    var sym = Console.ReadKey(true);

                    if (sym.Key == ConsoleKey.Enter)
                    {
                        break;
                    }
                }

                Draw("Main process ", y + 2, 10);
            }

            Console.WriteLine();
            Console.SetCursorPosition(x, y + 5);
            Console.WriteLine("Complete");


            void Draw(string text, int row, int speed)
            {
                StringBuilder sb = new StringBuilder();
                while (sb.ToString().Length < Console.WindowWidth - 2 - text.Length)
                {
                    semaphoreSlim.Wait();
                    Console.SetCursorPosition(x, row);
                    Console.Write($"{text}[");
                    Console.Write(sb.ToString() + '>');
                    int empty = Console.WindowWidth - sb.ToString().Length - 3 - text.Length;
                    for (int i = 0; i < empty; i++)
                    {
                        Console.Write(' ');
                    }
                    Console.Write("]");
                    semaphoreSlim.Release();
                    Thread.Sleep(speed);
                    sb.Append("=");
                }
            }
        }
    }
}
