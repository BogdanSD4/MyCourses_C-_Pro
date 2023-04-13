using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Lessons.LessonBody
{
    class Lesson18 : ILesson
    {
        public void Open()
        {
            DataBaseGrid();
        }

        private void DataBaseGrid()
        {
            Console.WriteLine("\nAll tasks of lesson 18" +
                "\npresented in this WinForm");

            Lesson_Instruments.OpenWPF("database");
        }
    }
}
