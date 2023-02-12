using System.IO;
using System;
using System.Diagnostics;

namespace Lessons
{
    public interface ILesson
    {
        static int lesson = 2;
        public abstract void Open();
        public static void Hello()
        {
            try
            {
                var name = File.ReadAllText(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile)+"/userName");
                if(name != null)
                {
                    Console.WriteLine("Hello " + name);
                    Console.WriteLine($"Current lesson: {lesson}");
                }
            }
            catch (Exception) { }
        }

        public static void ReadLine()
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
                    ReadLine();
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

                ILesson currentLesson = (ILesson)obj;
                lesson = num;
                currentLesson.Open();
            }
            catch(Exception) 
            {
                Console.Write("This lesson doesn't exist");
                ChooseLesson();
            }
        }
    }
}
