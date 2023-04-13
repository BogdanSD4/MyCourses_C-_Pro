using ClassesOfLesson15;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lessons.LessonBody
{
    class Lesson15 : ILesson
    {
        public void Open()
        {
            MyListTest();
            ConvertToArray();
            SortAndShow();
        }

        private void MyListTest()
        {
            Console.WriteLine();
            Console.WriteLine("\"E\" - exit | \"Enter - repeat\"");
            do
            {
                Draw();

                var key = ILesson.ReadKey("\n> ");
                if (key == "E") break;

            } while (true);

            Console.WriteLine();

            void Draw()
            {
                var list = new MyList<int>();
                Random random = new Random();

                Console.Write("> Add: ");
                Console.SetCursorPosition(30, Console.CursorTop);
                for (int i = 0; i < 20; i++)
                {
                    int num = random.Next(0, 11);
                    Console.Write(num + " ");
                    list.Add(num);
                }

                int remove = random.Next(0, 11);
                Console.Write($"\n> Remove ({remove}): ");
                Console.SetCursorPosition(30, Console.CursorTop);
                list.Remove(remove);
                for (int i = 0; i < list.Count; i++)
                {
                    Console.Write(list[i] + " ");
                }

                int removeAt = random.Next(0, 20);
                Console.Write($"\n> RemoveAt ({removeAt}): ");
                Console.SetCursorPosition(30, Console.CursorTop);
                list.RemoveAt(removeAt);
                for (int i = 0; i < list.Count; i++)
                {
                    Console.Write(list[i] + " ");
                }

                Predicate<int> removeAll = (x) => x % 2 != 0;
                Console.Write($"\n> RemoveAll (x % 2 != 0): ");
                Console.SetCursorPosition(30, Console.CursorTop);
                list.RemoveAll(removeAll);
                for (int i = 0; i < list.Count; i++)
                {
                    Console.Write(list[i] + " ");
                }
            }
        }
        private void ConvertToArray()
        {
            var list = new MyList<int>();
            Random random = new Random();
            for (int i = 0; i < 20; i++)
            {
                int num = random.Next(0, 1000);
                list.Add(num);
            }

            var arr = list.GetArray();
            Console.Write("> Array: ");
            for (int i = 0; i < arr.Length; i++)
            {
                Console.Write(arr[i] + " ");
            }

            Console.WriteLine();
        }
        private void SortAndShow()
        {
            int count = (int)ILesson.Read<uint>("Input list count (5 < x < 50): ", (ref string res) => 
            {
                int num = int.Parse(res);
                if (num >= 5 && num <= 50) return true;
                return false;
            });

            SortedList<int, string> valuePairs = new SortedList<int, string>();
            Random random = new Random();

            List<string> result = new List<string>();

            string res = "Base";
            for (int i = 0; i < count; i++)
            {
                int key = random.Next(1000, 10000);
                string value = Lesson_Instruments.GetSomeText(10, ' ');
                res += $"\n{key} | {value}";
                valuePairs.Add(key, value);
            }
            result.Add(res);

            var list = valuePairs.ToList();

            res = "OrderBy";
            list = list.OrderBy(value => value.Value).ToList();
            foreach (var item in list)
            {
                res += $"\n{item.Key} | {item.Value}";
            }
            result.Add(res);

            res = "Revers";
            list.Reverse();
            foreach (var item in list)
            {
                res += $"\n{item.Key} | {item.Value}";
            }
            result.Add(res);

            Lesson_Instruments.InlineWriter(5, result.ToArray());
        }
    }
}
namespace ClassesOfLesson15
{
    public interface IMyList<T>
    {
        public T[] array { get; set; }
        public void Add(T item);
        public T this[int index] { get; }
        public int Count { get; }
        public void Clear();
        public bool Remove(T item);
        public int RemoveAll(Predicate<T> match);
        public void RemoveAt(int index);
        public bool Contains(T item);
    }
    public class MyList<T> : IMyList<T>
    {
        public MyList()
        {
            array = new T[] { };
        }
        public MyList(int count)
        {
            array = new T[count];
        }
        public T[] array { get; set; }

        public T this[int index] => array[index];

        public int Count => array.Length;

        public void Add(T item)
        {
            T[] newArr = new T[array.Length + 1];
            for (int i = 0; i < array.Length; i++)
            {
                newArr[i] = array[i];
            }
            newArr[array.Length] = item;
            array = newArr;
        }

        public void Clear()
        {
            array = new T[] { };
        }

        public bool Contains(T item)
        {
            for (int i = 0; i < array.Length; i++)
            {
                object arg1 = array[i];
                object arg2 = item;
                if (arg1 == arg2) return true;
            }
            return false;
        }

        public bool Remove(T item)
        {
            bool res = false;

            T[] newArr = new T[] { };
            object arg2 = item;

            for (int i = 0; i < array.Length; i++)
            {
                if (!res)
                {
                    object arg1 = array[i];
                    if (arg1 == arg2)
                    {
                        res = true;
                        continue;
                    }
                }

                T[] arr = new T[newArr.Length + 1];
                for (int j = 0; j < newArr.Length; j++)
                {
                    arr[j] = newArr[j];
                }
                arr[newArr.Length] = array[i];
                newArr = arr;
            }

            array = newArr;
            return res;
        }

        public void RemoveAt(int index)
        {
            if(index < 0 || index > array.Length - 1)
            {
                throw new IndexOutOfRangeException();
            }

            T[] newArr = new T[array.Length - 1];
            int arrIndex = 0;

            for (int i = 0; i < array.Length; i++)
            {
                if (i == index)
                {
                    continue;
                }
                newArr[arrIndex] = array[i];
                arrIndex++;
            }
            array = newArr;
        }

        public int RemoveAll(Predicate<T> match)
        {
            int res = 0;
            T[] newArr = new T[] { };

            for (int i = 0; i < array.Length; i++)
            {
                if (!match.Invoke(array[i]))
                {
                    T[] arr = new T[newArr.Length + 1];
                    for (int j = 0; j < newArr.Length; j++)
                    {
                        arr[j] = newArr[j];
                    }
                    arr[newArr.Length] = array[i];
                    newArr = arr;
                }
            }
            array = newArr;
            return res;
        }
    }
    public static class MyLINQ
    {
        public static T[] GetArray<T>(this IMyList<T> list)
        {
            T[] arr = new T[list.Count];
            for (int i = 0; i < arr.Length; i++)
            {
                arr[i] = list[i];
            }
            return arr;
        }
        public static MyList<T> GetList<T>(this IMyList<T> list)
        {
            MyList<T> arr = new MyList<T>(list.Count);
            for (int i = 0; i < list.Count; i++)
            {
                arr.Add(list[i]);
            }
            return arr;
        }
    }
}
