
namespace Extensions.Mine
{
    static class Extensions
    {
        public static string ReversThis(this object obj)
        {
            string revers = obj.ToString();
            string result = "";

            foreach (char res in revers)
            {
                result = res + result;
            }

            return result;
        }

        public static string ArrayTo(this int[] arr, string separates = ", ") 
        {
            string result = "";
            for (int i = 0; i < arr.Length; i++)
            {
                result += arr[i];
                if (i != arr.Length - 1) result += separates;
            }
            return result;
        }

        public static bool Contains(this char[] arr, char sym)
        {
            for (int i = 0; i < arr.Length; i++)
            {
                if (arr[i] == sym) return true;
            }
            return false;
        }
    }
}
