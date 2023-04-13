using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace LessonEntity
{
    public static class DBController
    {
        public static T GetDatabase<T>() where T : DbContext
        {
            return (T)Activator.CreateInstance(typeof(T));
        }
    }
}
