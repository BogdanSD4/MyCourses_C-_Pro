using System;

namespace Lessons
{
    class Lesson_Instruments
    {
        static int[] space = new int[] { };
        public static string emptyString = "                                                    ";
        public static void FillSpaceArray(int value) 
        {
            space = new int[value];

            for (int i = 0; i < space.Length; i++)
            {
                if (i == 0)
                {
                    space[i] = ((value) * 1).ToString().Length;
                }
                else
                {
                    space[i] = ((value) * i).ToString().Length;
                }
            }
        }
        public static string GetSpace(int index, int value)
        {
            if (index > space.Length) return "";
                
            string result = "";
            int size = space[index] - value.ToString().Length;

            if (size >= 0)
            {
                for (int j = 0; j < size; j++)
                {
                    result += " ";
                }
            }
            return result;
        }

        public static string CreateVerticalList(params (string, object)[] obj)
        {
            string result = "";
            for (int i = 0; i < obj.Length; i++)
            {
                result += obj[i].Item1 + obj[i].Item2 + "\n";
            }
            return result;
        }

        public static void Clear(int startLine, int lineCount)
        {
            string clearH = "                                                           ";
            for (int i = 0; i < lineCount; i++)
            {
                Console.SetCursorPosition(0, startLine + i);
                Console.Write(clearH);
            }
        }
    }
}
