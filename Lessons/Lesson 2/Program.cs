global using Extensions.Mine; 
using System;
using System.Runtime.InteropServices;

namespace Lessons
{
    class Program
    {
        [DllImport("kernel32.dll", SetLastError = true)]
        static extern IntPtr GetConsoleWindow();

        [DllImport("user32.dll", SetLastError = true)]
        static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int X, int Y, int cx, int cy, uint uFlags);
        static void Main(string[] args)
        {
            StartSettings();
            ILesson.Hello();

            Type type = Type.GetType($"Lessons.LessonBody.Lesson{ILesson.lesson}");
            object obj = Activator.CreateInstance(type);

            ILesson currentLesson = (ILesson)obj;
            currentLesson.Open();
            ILesson.UserRequest();

            Console.ReadLine();
        }

        private static void StartSettings()
        {
            IntPtr consoleWindow = GetConsoleWindow();

            int screenWidth = Console.LargestWindowWidth;
            int screenHeight = Console.LargestWindowHeight;

            SetWindowPos(consoleWindow, IntPtr.Zero, 0, 0, screenWidth / 2, screenHeight / 2, 0);

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

            Console.SetCursorPosition(0, 0);
            Console.ForegroundColor = ConsoleColor.Green;
            FileManager.SAVE_PATH = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + FileManager.SAVE_PATH;
        }
    }
}
