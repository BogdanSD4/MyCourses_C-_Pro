using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lessons
{
    [Serializable]
    public class UserInfo
    {
        public string firstName { get; set; }
        public string lastName { get; set; }
        public int password { get; set; }

        public int currentLesson { get; set; }
    }
}
