using ClassesOfLesson22;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;

namespace Lessons.LessonBody
{
    public class Lesson22 : ILesson
    {
        public void Open()
        {
            ObsoleteMethod();
            AccessTree();
        }

        private void ObsoleteMethod()
        {
            Console.WriteLine();
            Person person = new Person(new DateTime(1990, 1, 1), "John", "Doe");

            int age = person.GetAge();
            Console.WriteLine("Age: " + age);

            //string name = person.GetName();
            string name = person.GetFullName();
            Console.WriteLine("Name: " + name);
        }
        private void AccessTree()
        {
            Console.WriteLine();
            var manager = new Manager();
            var programmer = new Programmer();
            var director = new Director();

            try
            {
                manager.AccessProtectedSection();
                programmer.AccessProtectedSection();
                director.AccessProtectedSection();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
namespace ClassesOfLesson22
{
    class Person
    {
        private string firstName { get; set; }
        private string lastName;
        private DateTime dateOfBirth;

        public Person(DateTime dateOfBirth, string firstName, string lastName)
        {
            this.firstName = firstName;
            this.lastName = lastName;
            this.dateOfBirth = dateOfBirth;
        }

        [Obsolete("This method is obsolete. Please use GetAgeInYears() instead.")]
        public int GetAge()
        {
            DateTime today = DateTime.Today;
            int age = today.Year - dateOfBirth.Year;
            if (dateOfBirth > today.AddYears(-age)) age--;
            return age;
        }

        [Obsolete("This method is obsolete. Please use GetFullName() instead.", true)]
        public string GetName()
        {
            return firstName + " " + lastName;
        }

        public int GetAgeInYears()
        {
            DateTime today = DateTime.Today;
            int age = today.Year - dateOfBirth.Year;
            if (dateOfBirth > today.AddYears(-age)) age--;
            return age;
        }

        public string GetFullName()
        {
            return firstName + " " + lastName;
        }
    }
    [AttributeUsage(AttributeTargets.Field)]
    public class MyDefaultValueAttribute : DefaultValueAttribute
    {
        public string Description { get; set; }

        public MyDefaultValueAttribute(string defaultValue) : base(defaultValue)
        {
            Description = "No description available.";
        }

        public MyDefaultValueAttribute(string defaultValue, string description) : base(defaultValue)
        {
            Description = description;
        }
    }


    [AttributeUsage(AttributeTargets.Class)]
    public class AccessLevelAttribute : Attribute
    {
        public int AccessLevel { get; set; }

        public AccessLevelAttribute(int accessLevel)
        {
            AccessLevel = accessLevel;
        }
    }
    public class CompanyEmployees
    {
        public void AccessProtectedSection()
        {
            CheckAccessLevel();
            Console.WriteLine($"Access granted to {GetType().Name}");
        }

        private void CheckAccessLevel()
        {
            var accessLevel = (AccessLevelAttribute)Attribute.GetCustomAttribute(this.GetType(), typeof(AccessLevelAttribute));
            if (accessLevel.AccessLevel < 1)
            {
                throw new Exception("Access denied");
            }
        }
    }
    [AccessLevel(1)]
    public class Manager : CompanyEmployees { }

    [AccessLevel(2)]
    public class Programmer : CompanyEmployees { }

    [AccessLevel(3)]
    public class Director : CompanyEmployees { }
}
