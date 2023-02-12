using System;
using System.Diagnostics;
using System.Reflection;

namespace Lessons
{
    class Program
    {
        static void Main(string[] args)
        {
            Type type = Type.GetType($"Lessons.Lesson{ILesson.lesson}");
            object obj = Activator.CreateInstance(type);

            ILesson currentLesson = (ILesson)obj;
            currentLesson.Open();
            Console.ReadLine();
        }
    }
}
