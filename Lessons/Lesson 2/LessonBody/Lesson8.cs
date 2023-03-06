using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using ClassesOfLesson8;

namespace Lessons.LessonBody
{
    class Lesson8 : ILesson
    {
        public void Open()
        {
            TailOfAnimal();
            ChestFilling();
            SomeClass();
            GropeOfPerson();
            ILesson.UserRequest();
        }

        private void TailOfAnimal()
        {
            var animal = new Dog(new Tail(10, "short"), "black", 5, "Vikki");
            Console.WriteLine("\n" + animal.GetInfo());
        }
        private void ChestFilling()
        {
            float boxSize = ILesson.Read<float>("Input box size: ", (ref string res) =>
            {
                return res.Contains("-") ? false : true;
            });

            Box chest = new Box(boxSize);
            bool boxIsEmpty = true;

            do
            {
                string figure = ILesson.ReadKey(
                "Choose shape (\"P\" - Pyramid | \"C\" - Cylinder | \"B\" - Ball): ",
                (ref ConsoleKey res) =>
                {
                    if (res == ConsoleKey.P || res == ConsoleKey.B || res == ConsoleKey.C) return true;
                    else return false;
                });

                var shape = Shape.GetFigure(figure);

                Console.WriteLine("Volume: " + shape.GetVolume() + "\n");
                boxIsEmpty = chest.Add(shape);
                Console.Write("Box filling: ");

                if (boxIsEmpty) Console.ForegroundColor = ConsoleColor.White;
                else Console.ForegroundColor = ConsoleColor.Red;

                Console.WriteLine($"{boxSize - chest.DrawerVolume} / {boxSize}");
                Console.ForegroundColor = ConsoleColor.Green;
            } 
            while (boxIsEmpty);

            Console.WriteLine("Box is full");
            string answer = ILesson.ReadKey("(\"E\" - exit | \"C\" - create new box)", (ref ConsoleKey res) =>
            {
                if (res == ConsoleKey.E || res == ConsoleKey.C) return true;
                return false;
            });
            if (answer == "C")
            {
                Console.WriteLine();
                ChestFilling();
            }
        }
        private void SomeClass()
        {
            int pupilsAmount = ILesson.Read<int>("How many pupils in your class? (2 - 30): ", (ref string res) =>
                {
                    int count = int.Parse(res);
                    if (count >= 2 && count <= 30) return true;
                    return false;
                });

            List<Pupil> pupils = new List<Pupil>();
            for (int i = 0; i < pupilsAmount; i++)
            {
                pupils.Add(Pupil.GetRandomPupil());
            }

            ClassRoom classRoom = new ClassRoom(pupils.ToArray());
            classRoom.GetPupilInfo();
        }
        private void GropeOfPerson()
        {
            uint teachersCount = ILesson.Read<uint>("Input teachers count: ");
            uint studentsCount = ILesson.Read<uint>("Input students count: ");

            List<Person> people = new List<Person>();

            for (int i = 0; i < teachersCount; i++)
            {
                Person person = new Teacher((int)studentsCount);
                people.Add(person);
            }
            Console.WriteLine();

            PersonInfo.ShowPersonsAfterFiltration();
        }
    }
}
namespace ClassesOfLesson8
{
    using Lessons;
    public class Tail
    {
        public Tail(int length, string tailType)
        {
            _length = length;
            _tailType = tailType;
        }

        private int _length;
        private string _tailType;

        public int TailLength { get { return _length; } set { _length = value; } }
        public string TailType { get { return _tailType; } set { _tailType = value; } }

        public string GetInfo()
        {
            return Lesson_Instruments.CreateVerticalList(
                ("TailLength: ", TailLength), ("TailType: ", TailType));
        }
    }
    public class AnimlaWithTail
    {
        private Tail _tail;
        private string _color;
        private int _age;
        public Tail Tail { get { return _tail; } set { _tail = value; } }
        public string Color { get { return _color; } set { _color = value; } }
        public int Age { get { return _age; } set { _age = value; } }

        public virtual string GetInfo()
        {
            return Tail.GetInfo() + Lesson_Instruments.CreateVerticalList(
                ("Color: ", Color), ("Age: ", Age));
        }
    }
    public class Dog : AnimlaWithTail
    {
        public Dog(Tail tail, string color, int age, string alias)
        {
            Tail = tail;
            Color = color;
            Age = age;
            Alias = alias;
        }
        private string _alias;
        public string Alias { get { return _alias; } set { _alias = value; } }

        public override string GetInfo()
        {
            return base.GetInfo() + Lesson_Instruments.CreateVerticalList(("Alias: ", Alias));
        }
    }


    public abstract class Shape
    {
        
        private double _volume;

        public virtual double GetVolume()
        {
            return _volume = FindVolume();
        }
        protected abstract double FindVolume();

        public static Shape GetFigure(string figureType)
        {
            figureType = figureType[0].ToString().ToUpper();
            switch (figureType)
            {
                case "P":
                    return new Pyramid();
                case "C":
                    return new Cylinder();
                case "B":
                    return new Ball();
                default:
                    throw new Exception("Shape not exist");
            }
        }
    }
    public class Pyramid : Shape
    {
        public Pyramid()
        {
            string answer = ILesson.ReadKey("Use random parameters? (\"Y\" - yes | \"N\" - no): ", (ref ConsoleKey res) =>
            {
                if (res == ConsoleKey.Y || res == ConsoleKey.N) return true;
                else return false;
            });

            if(answer == "Y")
            {
                Random random = new Random();
                Console.WriteLine("Side 1: " + Math.Round((_side1 = (float)(random.NextDouble() * 100)), 2));
                Console.WriteLine("Side 2: " + Math.Round((_side2 = (float)(random.NextDouble() * 100)), 2));
                Console.WriteLine("Heigth: " + Math.Round((_height = (float)(random.NextDouble() * 100)), 2));
            }
            else
            {
                _side1 = ILesson.Read<float>("Input side 1: ", (ref string res) =>
                {
                    return res.Contains("-") ? false : true;
                });
                _side2 = ILesson.Read<float>("Input side 2: ", (ref string res) =>
                {
                    return res.Contains("-") ? false : true;
                });
                _height = ILesson.Read<float>("Input height: ", (ref string res) =>
                {
                    return res.Contains("-") ? false : true;
                });
            }
        }
        public Pyramid(float side1, float side2, float height)
        {
            _side1 = side1;
            _side2 = side2;
            _height = height;
        }

        private float _side1;
        private float _side2;
        private float _height;
        protected override double FindVolume()
        {
            return Math.Round((1f / 3f) * _side1 * _side2 * _height, 2);
        }
    }
    public class Cylinder : Shape
    {
        public Cylinder()
        {
            string answer = ILesson.ReadKey("Use random parameters? (\"Y\" - yes | \"N\" - no): ", (ref ConsoleKey res) =>
            {
                if (res == ConsoleKey.Y || res == ConsoleKey.N) return true;
                else return false;
            });

            if (answer == "Y")
            {
                Random random = new Random();
                Console.WriteLine("Radius: " + Math.Round((_radius = (float)(random.NextDouble() * 100)), 2));
                Console.WriteLine("Heigth: " + Math.Round((_height = (float)(random.NextDouble() * 100)), 2));
            }
            else
            {
                _radius = ILesson.Read<float>("Input radius: ", (ref string res) =>
                {
                    return res.Contains("-") ? false : true;
                });
                _height = ILesson.Read<float>("Input height: ", (ref string res) =>
                {
                    return res.Contains("-") ? false : true;
                });
            }
        }
        public Cylinder(float radius, float height)
        {
            _radius = radius;
            _height = height;
        }

        private float _radius;
        private float _height;
        protected override double FindVolume()
        {
            return Math.Round(Math.PI * MathF.Pow(_radius, 2) * _height);
        }
    }
    public class Ball : Shape
    {
        public Ball()
        {
            string answer = ILesson.ReadKey("Use random parameters? (\"Y\" - yes | \"N\" - no): ", (ref ConsoleKey res) =>
            {
                if (res == ConsoleKey.Y || res == ConsoleKey.N) return true;
                else return false;
            });

            if (answer == "Y")
            {
                Random random = new Random();
                Console.WriteLine("Radius: " + Math.Round((_radius = (float)(random.NextDouble() * 100)), 2));
            }
            else
            {
                _radius = ILesson.Read<float>("Input radius: ", (ref string res) =>
                {
                    return res.Contains("-") ? false : true;
                });
            }
        }
        public Ball(float radius)
        {
            _radius = radius;
        }

        private float _radius;
        protected override double FindVolume()
        {
            return Math.Round((4f / 3f) * Math.PI * MathF.Pow(_radius, 3));
        }
    }
    public class Box
    {
        public Box(double volume)
        {
            _drawerVolume = volume;
        }
        private double _drawerVolume;
        public double DrawerVolume { get { return _drawerVolume; } set { _drawerVolume = value; } }

        public bool Add(Shape shape)
        {
            if ((DrawerVolume -= shape.GetVolume()) <= 0) return false;
            return true;
        }
    }


    public class ClassRoom
    {
        private Pupil[] _pupils;
        public ClassRoom(Pupil pupil_1, Pupil pupil_2, Pupil pupil_3 = null, Pupil pupil_4 = null)
        {
            _pupils = new Pupil[] { pupil_1, pupil_2, pupil_3, pupil_4 };
        }
        public ClassRoom(params Pupil[] pupils)
        {
            _pupils = pupils;
        }

        public void GetPupilInfo()
        {
            int counter = 1;
            Console.WriteLine();
            foreach (var pupil in _pupils)
            {
                Console.WriteLine($"Student_{counter}");
                Console.WriteLine("Student type: " + pupil.GetType().Name);
                pupil.Progress();
                Console.WriteLine("--------------------------");
                counter++;
            }
        }
    }
    public class Pupil
    {
        protected int study { get; set; }
        protected int read { get; set; }
        protected int write { get; set; }
        protected int relax { get; set; }
        private void Study() => Console.WriteLine("Study: "+study);
        private void Read() => Console.WriteLine("Read: "+ read);
        private void Write() => Console.WriteLine("Write: " + write);
        private void Relax() => Console.WriteLine("Relax: " + relax);
        public void Progress()
        {
            Study();
            Read();
            Write();
            Relax();
        }

        public static Pupil GetRandomPupil()
        {
            Random random = new Random();
            int num = random.Next(0, 3);
            switch (num)
            {
                case 0: return new ExcelentPupil();
                case 1: return new GoodPupil();
                case 2: return new BadPupil();
                default: return null;
            }
        }
    }
    public class ExcelentPupil : Pupil
    {
        public ExcelentPupil()
        {
            Random random = new Random();
            study = random.Next(8, 11);
            read = random.Next(8, 11);
            write = random.Next(8, 11);
            relax = random.Next(0, 4);
        }
    }
    public class GoodPupil : Pupil
    {
        public GoodPupil()
        {
            Random random = new Random();
            study = random.Next(4, 8);
            read = random.Next(4, 8);
            write = random.Next(4, 8);
            relax = random.Next(4, 8);
        }
    }
    public class BadPupil : Pupil
    {
        public BadPupil()
        {
            Random random = new Random();
            study = random.Next(0, 5);
            read = random.Next(0, 5);
            write = random.Next(0, 5);
            relax = random.Next(8, 11);
        }
    }


    public class PersonInfo
    {
        private static Person[] _peopleArray = new Person[] { };
        public static Person[] GetPeopleArray { get { return _peopleArray; } }
        public static Person SetPeopleArray
        {
            set
            {
                var arr = new Person[_peopleArray.Length + 1];
                _peopleArray.CopyTo(arr, 0);
                arr[arr.Length - 1] = value;
                _peopleArray = arr;
            }
        }

        public static void ShowPersonsAfterFiltration()
        {
            Person[] people = new Person[_peopleArray.Length];
            Person min = null;
            int couter = 0;
            List<Person> newArr = _peopleArray.ToList();

            while(couter != _peopleArray.Length)
            {
                int dateMin = int.MaxValue;
                int index = 0;

                for (int i = 0; i < newArr.Count; i++)
                {
                    int year = newArr[i].BirthYear;
                    if(year < dateMin)
                    {
                        dateMin = year;
                        min = newArr[i];
                        index = i;
                    }
                }

                newArr.RemoveAt(index);
                people[couter] = min;
                couter++;
            }

            Console.WriteLine();
            for (int i = 0; i < people.Length; i++)
            {
                people[i].ShowInfo();
                Console.WriteLine("---------------------");
            }
        }
    }
    public class Person
    {
        public Person()
        {
            FirstName = GetSomeName();
            LastName = GetSomeName();
            PersonInfo.SetPeopleArray = this;
        }

        private int _birthYear;
        private string _firstName;
        private string _lastName;

        private static string[] _names = new string[] { "Abraham", "Adam", "Adrian", "Albert", "Alexander", "Alfred", "Anderson", "Andrew", "Anthony", "Arnold", "Arthur", "Ashley", "Austen", "Benjamin", "Bernard", "Brian", "Caleb", "Calvin", "Carl", "Chad", "Charles", "Christian", "Christopher", "Clayton", "Clifford", "Clinton", "Corey", "Daniel", "Darren", "David", "Derek", "Dirk", "Donald", "Douglas", "Dwight", "Earl", "Edgar", "Edmund", "Edward", "Edwin", "Elliot", "Eric", "Ernest", "Ethan", "Ezekiel", "Felix", "Franklin", "Frederick", "Gabriel", "Gareth", "Geoffrey", "Gerald", "Graham", "Grant", "Gregory", "Harold", "Harry", "Henry", "Herbert", "Horace", "Hubert", "Hugh", "Ian", "Jack", "Jacob", "James", "Jason", "Jasper", "Jerome", "Jesse", "John", "Jonathan", "Joseph", "Joshua", "Julian", "Keith", "Kenneth", "Kevin", "Kurt", "Kyle", "Lawrence", "Leonard", "Lester", "Louis", "Lucas", "Malcolm", "Marcus", "Marshall", "Martin", "Matthew", "Maximilian", "Michael", "Miles", "Nathan", "Neil", "Nicholas", "Norman", "Oliver", "Oscar", "Oswald", "Samuel", "Scott", "Sebastian", "Shayne", "Sigmund", "Simon", "Steven", "Sylvester", "Terence", "Thomas", "Timothy", "Tobias", "Travis", "Tristan", "Tyler", "Valentine", "Victor", "Vincent", "Walter", "Wayne", "Wilfred", "William", "Winston", "Zachary", "Agat", "Agne", "Adelaid", "Id", "Iri", "Alic", "Amand", "Ameli", "Anastasi", "Angelin", "An", "Arie", "Ary", "Barbar", "Beatric", "Bridge", "Britne", "Batt", "Valer", "Vaness", "Wend", "Veronic", "Vivia", "Victori", "Viol", "Gabrie", "Gwe", "Gwinnet", "Glori", "Grac", "Debr", "Julie", "Jan", "Janic", "Jenn", "Jennife", "Jessi", "Jessic", "Gil", "Gin", "Joa", "Jodi", "Joyc", "Jocely", "Jud", "Juli", "Jun", "Dian", "Doroth", "Ev", "Jacquelin", "Jane", "Josephin", "Zar", "Zo", "Iv", "Isabell", "Irm", "Iren", "Camill", "Carolin", "Kare", "Cassandr", "Katherin", "Kimberl", "Constanc", "Christin", "Kell", "Cand", "Laur", "Leil", "Leon", "Lesle", "Lydi", "Lillia", "Lind", "Louis", "Luc", "Madelin", "Margare", "Mari", "Marci", "Meliss", "Maria", "Mirand", "Mi", "Moll", "Mon", "Monic", "Maggi", "Madiso", "Ma", "Mand", "Mar", "Murie", "Naom", "Natal", "Nicol", "Nor", "Norm", "Nanc", "Audre", "Olivi", "Pamel", "Patrici", "Paul", "Pegg", "Pag", "Penn", "Poll", "Priscill", "Rebecc", "Regin", "Rache", "Rosemar", "Ros", "Rut", "Sabrin", "Sall", "Samanth", "Sandr", "Sar", "Selen", "Sand", "Ceci", "Scarle", "Sophi", "Stac", "Stell", "Susa", "Susann", "Teres", "Tin", "Tiffan", "Trac", "Floranc", "Heathe", "Chlo", "Charlott", "Sheil", "Cheri", "Sharo", "Sherr", "Shirle", "Abigayl", "Evely", "Ediso", "Edit", "Aver", "Eleano", "Elizabet", "Ell", "Emil", "Emm", "Este", "Ashley"};

        public int BirthYear { get { return _birthYear; } set { _birthYear = value; } }
        public string FirstName { get { return _firstName; } set { _firstName = value; } }
        public string LastName { get { return _lastName; } set { _lastName = value; } }

        public virtual void ShowInfo()
        {
            Console.WriteLine($"==| {this.GetType().Name} |==");
            Console.WriteLine($"BirthYear: {BirthYear}");
            Console.WriteLine($"First name: {FirstName}");
            Console.WriteLine($"Last name: {LastName}");
        }

        public static string GetSomeName()
        {
            Random random = new Random();
            return _names[random.Next(0, _names.Length)];
        }
    }
    public class Student : Person
    {
        public Student()
        {
            Random random = new Random();
            int count = random.Next(0, 3);
            for (int i = 0; i <= count; i++)
            {
                SetStudyCourses = GetSomeCourse();
            }

            BirthYear = random.Next(1998, 2010);
        }
        private string[] _studyCourses = new string[] { };
        public string[] GetStudyCourses { get { return _studyCourses; } }
        public string SetStudyCourses
        {
            set
            {
                if (_studyCourses.Length == 3) return;
                else
                {
                    var arr = new string[_studyCourses.Length + 1];
                    _studyCourses.CopyTo(arr, 0);
                    arr[arr.Length - 1] = value;
                    _studyCourses = arr;
                }
            }
        }
        public void DisplayStudyСourses()
        {
            Console.WriteLine("Courses: ");
            foreach (var item in _studyCourses)
            {
                Console.WriteLine("==> " + item);
            }
        }

        public override void ShowInfo()
        {
            base.ShowInfo();
            DisplayStudyСourses();
        }

        private static string[] _cources = new string[] { "Algebra", "Astronomia", "Biologia", "Chemistry", "Computer tecnologies", "Ecology", "English", "Foreign", "French", "Geography", "Geometry", "German", "History", "Literature", "Mathematic", "Music", "Physical", "Physic", "Reading", "Scienc", "Technology", "Technical", "Zoology" };

        public static string GetSomeCourse()
        {
            Random random = new Random();
            return _cources[random.Next(0, _cources.Length)];
        }
    }
    public class Teacher : Person
    {
        public Teacher(int studentsCount)
        {
            Random random = new Random();
            for (int i = 0; i < studentsCount; i++)
            {
                SetStudentsArray = new Student();
            }

            BirthYear = random.Next(1965, 1990);
        }

        private Student[] _studentsArray = new Student[] { };
        public Student[] GetStudentsArray { get { return _studentsArray; } }
        public Student SetStudentsArray
        {
            set
            {
                var arr = new Student[_studentsArray.Length + 1];
                _studentsArray.CopyTo(arr, 0);
                arr[arr.Length - 1] = value;
                _studentsArray = arr;
            }
        }

        private void ShowStudents()
        {
            Console.WriteLine("Students: ");
            foreach (var item in _studentsArray)
            {
                Console.WriteLine("==> " + item.FirstName + " " + item.LastName);
            }
        }

        public override void ShowInfo()
        {
            base.ShowInfo();
            ShowStudents();
        }
    }
}
