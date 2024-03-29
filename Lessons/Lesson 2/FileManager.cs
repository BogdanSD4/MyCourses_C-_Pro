﻿using System;
using System.Buffers;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Xml;

namespace Lessons
{
    class FileManager
    {
        public static string SAVE_PATH;
        public const string FILE_EXTANSIONS_TXT = ".txt";
        public const string TasksPath = "../../../Files";
        public static T GetData<T>(string name, string path = null, string extension = FILE_EXTANSIONS_TXT)
        {
            var type = typeof(T);
            if (path == null) path = SAVE_PATH;
            path = Path.Combine(path, name + extension);

            if (File.Exists(path))
            {
                string json = File.ReadAllText(path);
                return JsonSerializer.Deserialize<T>(json);
            }
            else return Activator.CreateInstance<T>();
        }

        public static void SaveData(object value, string name, string path = null, string extension = FILE_EXTANSIONS_TXT)
        {
            if (path == null) path = SAVE_PATH;
            path = Path.Combine(path, name + extension);
            
            string json = JsonSerializer.Serialize(value);
            File.WriteAllText(path, json);
        }

        public static bool Exists(string name, string path = null, string extension = FILE_EXTANSIONS_TXT)
        {
            if (path == null) path = SAVE_PATH;
            path = Path.Combine(path, name + extension);

            return File.Exists(path);
        }

        private void FileReader()
        {
            string path = "D:\\Fork\\MyCourses_C-_Pro\\Lessons\\Lesson 2\\Names.txt";
            StreamReader stringReader = new StreamReader(path);
            string result = "";
            int repeat = 0;
            while (!stringReader.EndOfStream)
            {
                string line = stringReader.ReadLine();
                if (repeat-- == 0)
                {
                    string name = "";

                    for (int i = 0; i < line.Length; i++)
                    {
                        if (line[i] == ' ' || i == line.Length - 1)
                        {
                            result += $"\"{name}\",";

                            break;
                        }

                        name += line[i];
                    }

                    if (repeat <= 0) repeat = 5;
                }
            }

            Console.WriteLine(result);
        }

        public static string PathToRootDir()
        {
            var projectName = Process.GetCurrentProcess().ProcessName;
            var filePath = Directory.GetParent(Directory.GetCurrentDirectory());

            while (filePath.Name != projectName)
            {
                filePath = filePath.Parent;
            }

            return filePath + "\\AgeCalculation\\bin\\Debug\\";
        }

        public static void SetXmlWinFormSettings(string settings)
        {
            if (!Directory.Exists(TasksPath))
            {
                Directory.CreateDirectory(TasksPath);
            }
            string filePath = $"{TasksPath}/Settings.xml";
            if (!File.Exists(filePath))
            {
                File.WriteAllText(filePath, "");
            }
            
            XmlDocument xmlDoc = new XmlDocument();

            XmlElement root = xmlDoc.CreateElement("Settings");
            xmlDoc.AppendChild(root);

            XmlElement contact = xmlDoc.CreateElement("Name");
            contact.InnerText = settings;

            root.AppendChild(contact);

            xmlDoc.Save(filePath);
        }

        public static void GetXmlWinFormSettings()
        {

        }
    }
}
