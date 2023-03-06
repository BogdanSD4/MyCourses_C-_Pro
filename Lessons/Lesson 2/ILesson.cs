﻿
using System.IO;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Linq;


namespace Lessons
{
    public interface ILesson
    {
        public static int lesson = 5;
        public delegate bool ActionEvent<T>(ref T text);
        public abstract void Open();
        public static void Hello()
        {
            UserInfo info = new UserInfo();

            if (FileManager.Exists("userInfo"))
            {
                info = FileManager.GetData<UserInfo>("userInfo");
                lesson = info.currentLesson;
            }
            else
            {
                Console.WriteLine("Registration:");

                info.firstName = Read("First name: ");
                info.lastName = Read("Last name: ");
                info.password = Read<int>("Password(4 - 16): ", (ref string res) =>
                {
                    char[] numbers = new char[] { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };

                    int leng = res.Length;
                    if (leng < 4 || leng > 16) return false;

                    for (int i = 0; i < res.Length; i++)
                    {
                        if (!numbers.Contains(res[i]))
                        {
                            return false;
                        }
                    }

                    Console.WriteLine("\n(Don't forget that)\n");
                    return true;
                });
                info.currentLesson = lesson;

                FileManager.SaveData(info, "userInfo");
            }

            Console.WriteLine(
                $"Hello {info.firstName} {info.lastName}" +
                $"\nCurrent lesson: {lesson}");

            LessonNumRequest();

            FileManager.SaveData(info, "userInfo");

            void LessonNumRequest()
            {
                Console.Write($"Open current lesson press - \"Y\"\n" +
                    $"Choose specific press - \"Enter\"");
                var key = Console.ReadKey().Key;
                if (key == ConsoleKey.Y)
                {
                    Console.WriteLine();
                    return;
                }

                Console.Write("\nInput lesson number: ");
                string number = Console.ReadLine();

                try
                {
                    int numInt = int.Parse(number);
                    if (Type.GetType($"Lessons.LessonBody.Lesson{numInt}") == null)
                    {
                        Console.WriteLine("Invalid number");
                        LessonNumRequest();
                    }
                    else
                    {
                        lesson = info.currentLesson = numInt;
                    }
                }
                catch (Exception)
                {
                    Console.WriteLine("Invalid number");
                    LessonNumRequest();
                }
            }
        }

        /// <summary>
        /// Show command list
        /// </summary>
        public static void UserRequest()
        {
            Console.WriteLine($"Command list:" +
                $"\nchoose - choose spacific lesson" +
                $"\nexit - out of program");
            var text = Console.ReadLine();

            switch (text)
            {
                case "choose":
                    ChooseLesson();
                    return;
                case "exit":
                    Process.GetCurrentProcess().Close();
                    return;
                default:
                    UserRequest();
                    return;
            }
        }

        private static void ChooseLesson()
        {
            Console.Write("Input lesson number: ");
            var num = int.Parse(Console.ReadLine());

            try
            {
                Type type = Type.GetType($"Lessons.Lesson{num}");
                object obj = Activator.CreateInstance(type);

                var data = FileManager.GetData<UserInfo>("userInfo");
                ILesson currentLesson = (ILesson)obj;
                lesson = data.currentLesson = num;
                FileManager.SaveData(data, "userInfo");

                Console.Clear();
                currentLesson.Open();
            }
            catch (Exception)
            {
                Console.Write("This lesson doesn't exist");
                ChooseLesson();
            }
        }    
        public static string Read(string text, ActionEvent<string> paremeters = null)
        {
            string result;
            bool exit;
            do
            {
                Request();

                if (paremeters != null)
                {
                    exit = paremeters.Invoke(ref result);
                    if(!exit) Console.WriteLine("Invalid value");
                }
                else
                {
                    exit = true;
                }
            } 
            while (!exit);

            return result;

            void Request()
            {
                Console.Write(text);
                result = Console.ReadLine();
            }
        }
        public static T Read<T>(string text, ActionEvent<string> paremeters = null)
        {
            var result = Activator.CreateInstance<T>();
            string res;
            bool exit;
            do
            {
                Request();

                try
                {
                    result = (T)Convert.ChangeType(res, result.GetType());
                    exit = true;
                }
                catch (Exception)
                {
                    exit = false;
                }

                if (paremeters != null)
                {
                    exit = paremeters.Invoke(ref res);
                    if(!exit) Console.WriteLine("Invalid value");
                }
            }
            while (!exit);

            return result;

            void Request()
            {
                Console.Write(text);
                res = Console.ReadLine();
            }
        }

        public static string ReadKey(string text, ActionEvent<ConsoleKey> paremeters = null, bool printKey = false, bool showError = true, int pointerX = -1, int pointerY = -1)
        {
            ConsoleKey result;
            bool exit;
            do
            {
                Request();

                if (paremeters != null)
                {
                    exit = paremeters.Invoke(ref result);
                    if (!exit && showError) Console.WriteLine("\nInvalid value");
                }
                else
                {
                    exit = true;
                }
            }
            while (!exit);

            Console.WriteLine();
            return result.ToString();

            void Request()
            {
                if(pointerX != -1 && pointerY != -1)
                {
                    int x = pointerX == -1 ? 0 : pointerX;
                    int y = pointerY == -1 ? 0 : pointerY;
                    Console.SetCursorPosition(x, y);
                }
                Console.Write(text);
                result = Console.ReadKey(printKey).Key;
            }
        }
    }
}
