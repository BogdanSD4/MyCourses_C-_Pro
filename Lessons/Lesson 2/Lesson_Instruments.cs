using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;

namespace Lessons
{
    class Lesson_Instruments
    {
        static int[] space = new int[] { };
        public static string emptyString = "                                                    ";
        public static string alphabet = "abcdefghijklmnopqrstuvwxyz";
        public static string someText 
        { 
            get 
            { return "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Egestas erat imperdiet sed euismod nisi porta. Posuere sollicitudin aliquam ultrices sagittis orci a scelerisque. Blandit massa enim nec dui nunc mattis. Dignissim enim sit amet venenatis urna cursus eget. Sit amet massa vitae tortor condimentum. Morbi enim nunc faucibus a pellentesque sit amet porttitor eget. Tempor orci eu lobortis elementum. Adipiscing elit duis tristique sollicitudin nibh sit amet. Tellus at urna condimentum mattis pellentesque id nibh tortor id. Diam phasellus vestibulum lorem sed risus ultricies tristique nulla aliquet. Leo vel fringilla est ullamcorper.Feugiat scelerisque varius morbi enim nunc. Arcu ac tortor dignissim convallis aenean et.Adipiscing elit duis tristique sollicitudin nibh sit amet commodo.Mauris sit amet massa vitae tortor condimentum.At ultrices mi tempus imperdiet nulla malesuada pellentesque elit eget. Feugiat vivamus at augue eget arcu dictum varius duis at. Erat imperdiet sed euismod nisi porta lorem.Fames ac turpis egestas sed tempus urna et pharetra.Fringilla urna porttitor rhoncus dolor purus non enim praesent elementum. Nunc vel risus commodo viverra maecenas accumsan."; 
            }
        }
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
        public static string GetSpace(int count)
        {
            string result = "";
            for (int i = 0; i < count; i++)
            {
                result += ' ';
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

        public static void Clear(int lineCount, int startLine = default)
        {
            if(startLine == default)
            {
                startLine = Console.CursorTop;
            }
            string clearH = new string(' ', Console.LargestWindowWidth);
            for (int i = 0; i < lineCount; i++)
            {
                Console.SetCursorPosition(0, startLine + i);
                Console.Write(clearH);
            }
            Console.SetCursorPosition(0, startLine);
        }

        /// <summary>
        /// Default parameters "length" return random value (10 - 50)
        /// </summary>
        /// <param name="length"></param>
        /// <param name="dellChar"></param>
        /// <returns></returns>
        public static string GetSomeText(int length = -1, params char[] dellChar)
        {
            Random random = new Random();
            if (length == -1) length = random.Next(10, 50);
            int startIndex = random.Next(0, someText.Length / 2);
            string result = "";

            for (int i = startIndex; i < someText.Length; i++)
            {
                if (dellChar.Contains(someText[i])) continue;

                result += someText[i];
                if (--length == 0) break;
            }

            return result;
        }

        public static void InlineWriter(int space = default, params string[] text)
        {
            int maxLength = 0;

            int row = 0;
            for (int i = 0; i < text.Length; i++)
            {
                int newLine = 0;
                for (int j = 0; j < text[i].Length; j++)
                {
                    if (text[i][j] == '\n') newLine++;
                }
                if (newLine > row) row = newLine;
            }
            row += 2;

            int startX = Console.CursorLeft;
            int startY = Console.CursorTop;

            int lineY = 0;
            for (int i = 0; i < text.Length; i++)
            {
                string res = "";
                int indexY = startY + lineY;

                for (int j = 0; j < text[i].Length; j++)
                {
                    if (text[i][j] == '\n' || j == text[i].Length - 1)
                    {
                        if (j == text[i].Length - 1) res += text[i][j];

                        Console.SetCursorPosition(startX + maxLength, indexY);
                        Console.WriteLine(res);
                        res = "";
                        indexY++;
                        continue;
                    }
                    res += text[i][j];
                }

                int newLeng = Task.Run<int>(() =>
                {
                    int result = 0;
                    int num = 0;
                    for (int j = 0; j < text[i].Length; j++)
                    {
                        if (text[i][j] == '\n' || j == text[i].Length - 1)
                        {
                            if (num > result) result = num;
                            num = 0;
                            continue;
                        }
                        num++;
                    }
                    return result + space;
                }).Result;

                if (startX + maxLength + (newLeng * 2) > Console.BufferWidth)
                {
                    lineY += row;
                    maxLength = 0;
                }
                else
                {
                    maxLength += newLeng;
                }
            }
        }
        public static void InlineWriter(Type exception, int space = default, params string[] text)
        {
            int maxLength = 0;

            int row = 0;
            for (int i = 0; i < text.Length; i++)
            {
                int newLine = 0;
                for (int j = 0; j < text[i].Length; j++)
                {
                    if (text[i][j] == '\n') newLine++;
                }
                if (newLine > row) row = newLine;
            }
            row += 2;

            int startX = Console.CursorLeft;
            int startY = Console.CursorTop;

            int lineY = 0;
            for (int i = 0; i < text.Length; i++)
            {
                string res = "";
                int indexY = startY + lineY;

                for (int j = 0; j < text[i].Length; j++)
                {
                    if (text[i][j] == '\n' || j == text[i].Length - 1)
                    {
                        if (j == text[i].Length - 1) res += text[i][j];

                        Console.SetCursorPosition(startX + maxLength, indexY);
                        Console.WriteLine(res);
                        res = "";
                        indexY++;
                        continue;
                    }
                    res += text[i][j];
                }

                if (exception.IsSubclassOf(typeof(Exception)))
                {
                    Console.SetCursorPosition(startX + maxLength, indexY);
                    Activator.CreateInstance(exception);
                }

                int newLeng = Task.Run<int>(() =>
                {
                    int result = 0;
                    int num = 0;
                    for (int j = 0; j < text[i].Length; j++)
                    {
                        if (text[i][j] == '\n' || j == text[i].Length - 1)
                        {
                            if (num > result) result = num;
                            num = 0;
                            continue;
                        }
                        num++;
                    }
                    return result + space;
                }).Result;

                if (startX + maxLength + (newLeng * 2) > Console.BufferWidth)
                {
                    lineY += row;
                    maxLength = 0;
                }
                else
                {
                    maxLength += newLeng;
                }
            }
        }
        public static void InlineWriter(int startX, int startY, int space = default, params string[] text)
        {
            int maxLength = 0;

            int row = 0;
            for (int i = 0; i < text.Length; i++)
            {
                int newLine = 0;
                for (int j = 0; j < text[i].Length; j++)
                {
                    if (text[i][j] == '\n') newLine++;
                }
                if (newLine > row) row = newLine;
            }
            row += 2;

            if (startX == default) startX = Console.CursorLeft;
            if (startY == default) startY = Console.CursorTop;

            int lineY = 0;
            for (int i = 0; i < text.Length; i++)
            {
                string res = "";
                int indexY = startY + lineY;

                for (int j = 0; j < text[i].Length; j++)
                {
                    if (text[i][j] == '\n' || j == text[i].Length - 1)
                    {
                        if (j == text[i].Length - 1) res += text[i][j];

                        Console.SetCursorPosition(startX + maxLength, indexY);
                        Console.WriteLine(res);
                        res = "";
                        indexY++;
                        continue;
                    }
                    res += text[i][j];
                }

                int newLeng = Task.Run<int>(() =>
                {
                    int result = 0;
                    int num = 0;
                    for (int j = 0; j < text[i].Length; j++)
                    {
                        if (text[i][j] == '\n' || j == text[i].Length - 1)
                        {
                            if (num > result) result = num;
                            num = 0;
                            continue;
                        }
                        num++;
                    }
                    return result + space;
                }).Result;

                if (startX + maxLength + (newLeng * 2) > Console.BufferWidth)
                {
                    lineY += row;
                    maxLength = 0;
                }
                else
                {
                    maxLength += newLeng;
                }
            }
        }

        public static Type GetRandomStructType()
        {
            string[] types = new string[]
            { "System.Int32", "System.Decimal", "System.Double", "System.Single" };

            Random random = new Random();
            return Type.GetType(types[random.Next(0, types.Length)]);
        }
        public static void OpenWPF(string processName)
        {
            var rootPath = FileManager.PathToRootDir();

            File.WriteAllText(rootPath + "Settings.txt", processName);

            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.FileName = "AgeCalculation.exe";
            startInfo.WorkingDirectory = rootPath;
            startInfo.UseShellExecute = true;
            Process process = new Process();
            process.StartInfo = startInfo;
            process.Start();
        }
    }
}
