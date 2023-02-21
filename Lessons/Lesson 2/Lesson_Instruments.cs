namespace Lessons
{
    class Lesson_Instruments
    {
        static int[] space = new int[] { };

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
    }
}
