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

            Type type = Type.GetType($"Lessons.LessonBody.Lesson{ILesson.lesson}");
            object obj = Activator.CreateInstance(type);

            ILesson currentLesson = (ILesson)obj;
            currentLesson.Open();
            Console.ReadLine();
        }

        private static void StartSettings()
        {
            
            try
            {
                Console.SetBufferSize(Console.LargestWindowWidth, Console.LargestWindowHeight * 10);
                Console.SetWindowSize(Console.LargestWindowWidth - 10, Console.LargestWindowHeight - 10);
            }
            catch (Exception)
            {
                Console.SetWindowSize(1, 1);
                Console.SetBufferSize(Console.LargestWindowWidth, Console.LargestWindowHeight * 10);
                Console.SetWindowSize(Console.LargestWindowWidth - 10, Console.LargestWindowHeight - 10);
            }
            

            Console.ForegroundColor = ConsoleColor.Green;
            FileManager.SAVE_PATH = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + FileManager.SAVE_PATH;
        }

        private static string Test(string task)
        {
            string result = "";
            for (int i = 0; i < task.Length; i++)
            {
                switch (task[i])
                {
                    case '1' or '2' or '3' or '4' or '5' or '6' or '7' or '8' or '9':
                        string res = task.Substring(i + 2);
                        while(res[res.Length - 1] != ']')
                        {
                            res = res.Remove(res.Length - 1);
                        }
                        res = res.Remove(res.Length - 1);
                        string sym = Test(res);
                        int count = int.Parse($"{task[i]}");
                        for (int j = 0; j < count; j++)
                        {
                            result += sym;
                        }
                        i += res.Length + 1;
                        break;
                    case '[' or ']':
                        break;
                    default: 
                        result += task[i];
                        break;
                }
                
            }
            return result;
        }
    }
}
