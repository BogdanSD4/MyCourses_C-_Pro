using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lessons.LessonBody
{
    public class Lesson20 : ILesson
    {
        string path = FileManager.TasksPath;
        public void Open()
        {
            CreateFolder();
            Writer();
            TaskFileManager();
        }

        private void CreateFolder()
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            string[] result = new string[51];
            for (int i = 0; i < result.Length; i++) 
            {
                string name = path + $"/Folder_{i}";
                var info = Directory.CreateDirectory(name);
                result[i] =
                    $"Disk: {info.Root}\n" +
                    $"Name: {info.Name}\n" +
                    $"FullName: {info.FullName}\n" +
                    $"Create time: {info.CreationTime}";
                Directory.Delete(name);
            }

            Directory.Delete(path);

            Lesson_Instruments.InlineWriter(5, result);
        }
        private void Writer()
        {
            Console.WriteLine();
            string name = path + "newFile.txt";
            using (StreamWriter writer = File.CreateText(name))
            {
                writer.WriteLine(Lesson_Instruments.someText);
            }

            using (StreamReader reader = new StreamReader(name))
            {
                StringBuilder builder = new StringBuilder();
                while (!reader.EndOfStream)
                {
                    builder.Append(reader.ReadLine());
                }
                Console.WriteLine(builder.ToString());
            }

            File.Delete(name);
        }
        private void TaskFileManager()
        {
            Lesson_Instruments.OpenWPF("fileManager");
        }
    }
}
