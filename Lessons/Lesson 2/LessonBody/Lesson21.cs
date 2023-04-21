using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace Lessons.LessonBody
{
    public class Lesson21 : ILesson
    {
        string path = FileManager.TasksPath;
        public void Open()
        {
            XmlInput();
            XmlOutput();
            OutputPhone();
            XmlSave();
        }

        private void XmlInput()
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            if (!File.Exists($"{path}/TelephoneBook.xml"))
            {
                File.WriteAllText($"{path}/TelephoneBook.xml", "");
            }

            Console.WriteLine();
            uint count = ILesson.Read<uint>("Input clients count: ");
            if (count == 0) count = 1;

            Random random = new Random();
            XmlDocument xmlDoc = new XmlDocument();

            XmlElement root = xmlDoc.CreateElement("MyContacts");
            xmlDoc.AppendChild(root);

            for (int i = 0; i < count; i++)
            {
                XmlElement contact = xmlDoc.CreateElement("Contact");
                contact.SetAttribute("TelephoneNumber", 
                    $"{random.Next(100, 1000)}-{random.Next(100, 1000)}-{random.Next(1000, 10000)}");

                XmlElement name = xmlDoc.CreateElement("Name");
                name.InnerText = Lesson_Instruments.GetSomeText(10, ' ', '\n');
                contact.AppendChild(name);

                root.AppendChild(contact);
            }

            xmlDoc.Save($"{path}/TelephoneBook.xml");
        }
        private void XmlOutput()
        {
            XmlDocument xmlDocument = new XmlDocument();
            
            xmlDocument.Load($"{path}/TelephoneBook.xml");
            
            Console.WriteLine();
            StringBuilder builder = new StringBuilder();

            ShowNode(xmlDocument.FirstChild);

            bool ShowNode(XmlNode node)
            {
                builder.Append(" ");
                Console.Write($"{builder.ToString()}{node.Name}");
                if(node.NodeType == XmlNodeType.Text) Console.WriteLine($"  {node.InnerText.Trim()}");
                else Console.WriteLine();

                if (node.Attributes != null)
                {
                    foreach (XmlAttribute item in node.Attributes)
                    {
                        Console.WriteLine($"{builder.ToString()} {item.Name}: {item.Value}");
                    }
                }

                XmlNodeList nodes = node.ChildNodes;
                if (nodes.Count == 0) return false;

                foreach (XmlNode Xnode in nodes)
                {
                    if (!ShowNode(Xnode))
                    {
                        builder.Remove(builder.Length - 1, 1);
                    }
                }

                builder.Remove(builder.Length - 1, 1);
                return true;
            }
        }
        private void OutputPhone()
        {
            XmlDocument xmlDocument = new XmlDocument();

            xmlDocument.Load($"{path}/TelephoneBook.xml");

            Console.WriteLine();

            ShowNode(xmlDocument.FirstChild);

            bool ShowNode(XmlNode node)
            {
                if (node.Attributes != null && node.Attributes.Count > 0)
                {
                    Console.WriteLine(node.Attributes["TelephoneNumber"].Value);
                }

                XmlNodeList nodes = node.ChildNodes;
                if (nodes.Count == 0) return false;

                foreach (XmlNode Xnode in nodes)
                {
                    if (!ShowNode(Xnode))
                    {

                    }
                }

                return true;
            }
        }
        private void XmlSave()
        {
            Lesson_Instruments.OpenWPF("xmlForm");
        }
    }
}
