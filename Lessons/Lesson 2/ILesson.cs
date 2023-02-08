using System.IO;
using System;


namespace Lessons
{
    public interface ILesson
    {
        public abstract void Open();
        public static void Hello()
        {
            try
            {
                var name = File.ReadAllText(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile)+"/userName");
                if(name != null)
                {
                    Console.WriteLine("Hello " + name);
                }
            }
            catch (Exception) { }
        }
    }
}
