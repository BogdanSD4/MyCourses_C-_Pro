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
        public abstract void Open();
        public static bool TT(string a)
        {
            return false;
        }
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

                info.firstName = Write("First name: ");
                info.lastName = Write("Last name: ");
                info.password = Write<int>("Password(4 - 16): ", (ref string res) =>
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
                Console.Write($"Input lesson number or \"Y\" to open current: ");
                string number = Console.ReadLine();

                if (number == "Y") return;
                else
                {
                    try
                    {
                        int numInt = int.Parse(number);
                        if (Type.GetType($"Lessons.Lesson{numInt}") == null)
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
        }

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
        public delegate bool ActionEvent<T>(ref T text);
        public static string Write(string text, ActionEvent<string> paremeters = null)
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
        public static T Write<T>(string text, ActionEvent<string> paremeters = null)
        {
            var result = Activator.CreateInstance<T>();
            string res;
            bool exit;
            do
            {
                Request();

                if (paremeters != null)
                {
                    exit = paremeters.Invoke(ref res);
                    if(!exit) Console.WriteLine("Invalid value");
                    else
                    {
                        try
                        {
                            result = (T)Convert.ChangeType(res, result.GetType());
                        }
                        catch (Exception)
                        {
                            exit = false;
                        }
                    }
                }
                else
                {
                    try
                    {
                        result = (T)Convert.ChangeType(res, result.GetType());
                        exit = true;
                    }
                    catch (Exception)
                    {
                        exit = false;
                    }
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
    }
}
