using System;
using System.Diagnostics;

namespace Lessons
{
    class Program
    {
        static void Main(string[] args)
        {
            ILesson currentLesson = new Lesson2();
            currentLesson.Open();
            Console.ReadLine();
        }
    }
}
