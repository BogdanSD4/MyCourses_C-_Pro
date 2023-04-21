

using System.IO;
using System;
using System.Text.Json;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using Lessons;
using ClassesOfLesson23;
using System.Xml;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization.Formatters.Soap;

namespace Lessons.LessonBody
{
    public class Lesson23 : ILesson
    {
        string path = FileManager.TasksPath;
        public void Open()
        {
            SerializationTypes();
            DefaultToAttributes();
            DeserializationTypes();
        }

        private void SerializationTypes()
        {
            Person person = new Person();

            string dirPath = $"{path}/XML_Lesson23";
            if (!Directory.Exists(dirPath)) Directory.CreateDirectory(dirPath);

            SerializeBinary($"{dirPath}/SerializeBinary.bin", person);
            SerializeJson($"{dirPath}/SerializeJSON.json", person);
            SerializeXml($"{dirPath}/SerializeXML.xml", person);
            SerializeSoap($"{dirPath}/SerializeSOAP.soap", person);
            SerializeDataContract($"{dirPath}/SerializeDataContract.txt", person);

            void SerializeBinary(string filePath, Person person)
            {
                BinaryFormatter formatter = new BinaryFormatter();
                using (var stream = CreatePath(filePath))
                {
                    formatter.Serialize(stream, person);
                }
            }
            void SerializeJson(string filePath, Person person)
            {
                using (var stream = CreatePath(filePath))
                {
                    JsonSerializer.Serialize<Person>(stream, person);
                }
            }
            void SerializeXml(string filePath, Person person)
            {
                XmlSerializer serializer = new XmlSerializer(typeof(Person));
                using (var stream = CreatePath(filePath))
                {
                    serializer.Serialize(stream, person);
                }
            }
            void SerializeSoap(string filePath, Person person)
            {
                SoapFormatter formatter = new SoapFormatter();
                using (var stream = CreatePath(filePath))
                {
                    formatter.Serialize(stream, person);
                }      
            }
            void SerializeDataContract(string filePath, Person person)
            {
                DataContractSerializer serializer = new DataContractSerializer(typeof(Person));
                using (var stream = CreatePath(filePath))
                {
                    serializer.WriteObject(stream, person);
                }
            }
        }

        private void DefaultToAttributes()
        {
            Person person = new Person();

            string nameXml = "Person.xml";
            string nameXml_Attr = "Person_Attr.xml";

            string dirPath = $"{path}/XML_Lesson23";
            if (!Directory.Exists(dirPath)) Directory.CreateDirectory(dirPath);
            nameXml = $"{dirPath}/{nameXml}";
            if (!File.Exists(nameXml)) using (File.Create(nameXml)) { }
            nameXml_Attr = $"{dirPath}/{nameXml_Attr}";
            if (!File.Exists(nameXml_Attr)) using (File.Create(nameXml_Attr)) { }

            var defaultSerializer = new XmlSerializer(typeof(Person));
            using (var writer = new StreamWriter(nameXml))
            {
                defaultSerializer.Serialize(writer, person);
            }

            using (var reader = new StreamReader(nameXml))
            {
                person = (Person)defaultSerializer.Deserialize(reader);
            }

            var overrides = new XmlAttributeOverrides();
            var attributes = new XmlAttributes { XmlAttribute = new XmlAttributeAttribute() };
            overrides.Add(typeof(Person), "FirstName", attributes);
            overrides.Add(typeof(Person), "LastName", attributes);
            overrides.Add(typeof(Person), "Age", attributes);
            var attrSerializer = new XmlSerializer(typeof(Person), overrides);

            using (var writer = new StreamWriter(nameXml_Attr))
            {
                attrSerializer.Serialize(writer, person);
            }

            XmlDocument xmlDocument = new XmlDocument();
            using (var reader = new StreamReader(nameXml_Attr))
            {
                xmlDocument.Load(reader);
                Console.WriteLine();

                foreach (XmlNode item in xmlDocument.ChildNodes)
                {
                    if (item.Attributes != null && item.Attributes.Count > 0)
                    {
                        foreach (XmlAttribute attr in item.Attributes)
                        {
                            Console.WriteLine($"{attr.Name}: {attr.Value}");
                        }
                    }
                }
            }
        }

        private void DeserializationTypes()
        {
            Console.WriteLine();

            string dirPath = $"{path}/XML_Lesson23";
            if (!Directory.Exists(dirPath)) Directory.CreateDirectory(dirPath);

            DeserializeBinary($"{dirPath}/SerializeBinary.bin");
            DeserializeJson($"{dirPath}/SerializeJSON.json");
            DeserializeXml($"{dirPath}/SerializeXML.xml");
            DeserializeSoap($"{dirPath}/SerializeSOAP.soap");
            DeserializeDataContract($"{dirPath}/SerializeDataContract.txt");

            void DeserializeBinary(string filePath)
            {
                Console.WriteLine(new string('-', 30)+ "\nBinary");
                BinaryFormatter formatter = new BinaryFormatter();
                using (var stream = CreatePath(filePath))
                {
                    ((Person)formatter.Deserialize(stream)).ShowInfo();
                }
            }
            void DeserializeJson(string filePath)
            {
                Console.WriteLine(new string('-', 30) + "\nJson");
                using (var stream = CreatePath(filePath))
                {
                    JsonSerializer.Deserialize<Person>(stream).ShowInfo();
                }
            }
            void DeserializeXml(string filePath)
            {
                Console.WriteLine(new string('-', 30) + "\nXml");
                XmlSerializer serializer = new XmlSerializer(typeof(Person));
                using (var stream = CreatePath(filePath))
                {
                    ((Person)serializer.Deserialize(stream)).ShowInfo();
                }
            }
            void DeserializeSoap(string filePath)
            {
                Console.WriteLine(new string('-', 30) + "\nSoap");
                SoapFormatter formatter = new SoapFormatter();
                using (var stream = CreatePath(filePath))
                {
                    ((Person)formatter.Deserialize(stream)).ShowInfo();
                }
            }
            void DeserializeDataContract(string filePath)
            {
                Console.WriteLine(new string('-', 30) + "\nDataContract");
                DataContractSerializer serializer = new DataContractSerializer(typeof(Person));
                using (var stream = CreatePath(filePath))
                {
                    ((Person)serializer.ReadObject(stream)).ShowInfo();
                }
            }
        }

        Stream CreatePath(string filePath)
        {
            return new FileStream(filePath, FileMode.OpenOrCreate, FileAccess.ReadWrite);
        }
    }
}
namespace ClassesOfLesson23
{
    [Serializable]
    [DataContract]
    public class Person
    {
        public Person()
        {
            FirstName = Lesson_Instruments.GetSomeText(10, ' ', '\n');
            LastName = Lesson_Instruments.GetSomeText(10, ' ', '\n');
            Age = Random.Shared.Next(10, 60);
        }
        //[XmlAttribute("FirstName")]
        [DataMember]
        public string FirstName { get; set; }
        //[XmlAttribute("LastName")]
        [DataMember]
        public string LastName { get; set; }
        //[XmlAttribute("Age")]
        [DataMember]
        public int Age { get; set; }

        public void ShowInfo()
        {
            Console.WriteLine($"First name: {FirstName}");
            Console.WriteLine($"Last name: {LastName}");
            Console.WriteLine($"Age: {Age}");
        }
    }
}
