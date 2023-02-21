global using Extensions.Mine; 
using System;

namespace Lessons
{
    class Program
    {
        static void Main(string[] args)
        {
            StartSettings();
            ILesson.Hello();

            Type type = Type.GetType($"Lessons.Lesson{ILesson.lesson}");
            object obj = Activator.CreateInstance(type);

            ILesson currentLesson = (ILesson)obj;
            currentLesson.Open();
            Console.ReadLine();
        }

        private static void StartSettings()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            FileManager.SAVE_PATH = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + FileManager.SAVE_PATH;
        }
    }
}
