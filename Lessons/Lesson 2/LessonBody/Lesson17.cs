using ClassesOfLesson15;
using ClassesOfLesson17;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Lessons.LessonBody
{
    class Lesson17 : ILesson
    {
        public void Open()
        {
            _55Calculator();
            AutoService();
            Sequence();
            Fitness();
            Vokabulary();
        }

        private void _55Calculator()
        {
            Console.WriteLine();
            int x = Console.CursorLeft;
            int y = Console.CursorTop;
            string space = Lesson_Instruments.emptyString;

            while (true)
            {
                Console.SetCursorPosition(x, y);
                Lesson_Instruments.Clear(10, y);
                int count = (int)ILesson.Read<uint>("Input quantity of arguments (1 < x < 500): ", (ref string res) =>
                {
                    int num = int.Parse(res);
                    if (num > 0 && num <= 500) return true;
                    return false;
                }, true);

                Type someType = Lesson_Instruments.GetRandomStructType();

                Type myListGenericType = typeof(MyList<>);
                Type myListType = myListGenericType.MakeGenericType(someType);
                dynamic list = Activator.CreateInstance(myListType);

                Random random = new Random();
                

                for (int i = 0; i < count; i++)
                {
                    double num = random.NextDouble() * 100;
                    dynamic res = Convert.ChangeType(num, someType);
                    list.Add(res);
                }
                
                Type type = typeof(Calculator);
                MethodInfo methodInfo = type.GetMethod("Sum").MakeGenericMethod(someType);

                var result = methodInfo.Invoke(null, new object[] { list.array });

                Console.WriteLine("> Type: " + someType);
                Console.WriteLine("> Numbers count: " + count);
                Console.WriteLine("> Result Sum: " + GetResult("Sum"));
                Console.WriteLine("> Result Sub: " + GetResult("Sub"));
                Console.WriteLine("> Result Mul: " + GetResult("Mul"));
                Console.WriteLine("> Result Div: " + GetResult("Div"));

                var key = ILesson.ReadKey("\n> \"R\" - rest | \"Enter\" - exit", (ref ConsoleKey res) =>
                {
                    if (res == ConsoleKey.R || res == ConsoleKey.Enter) return true;
                    return false;
                }, showError: false);

                if (key == "Enter") break; 

                dynamic GetResult(string methodName)
                {
                    Type type = typeof(Calculator);
                    MethodInfo methodInfo = type.GetMethod(methodName).MakeGenericMethod(someType);
                    return methodInfo.Invoke(null, new object[] { list.array });
                }
            }
        }
        private void AutoService()
        {
            List<dynamic> autos = new List<dynamic>();
            List<dynamic> clients = new List<dynamic>();
            int clientsCount = 10;
            int controllerV = 0;

            bool exit = false;
            bool openResult = false;

            Console.WriteLine();
            
            var autoQuantity = ILesson.Read<uint>("Input auto quantity: ", inline: true);
            Random random = new Random();

            Console.WriteLine("\"E\" - exit\n");

            int x = Console.CursorLeft;
            int y = Console.CursorTop;

            for (int i = 0; i < autoQuantity; i++)
            {
                autos.Add(new 
                {
                    price = random.Next(5000, 125000),
                    brand = Lesson_Instruments.GetSomeText(10, ' '),
                    model = random.Next(1000, 9999),
                    year = random.Next(1942, 2022),
                    color = Lesson_Instruments.GetSomeText(8, ' ', ';', '.', ','),
                });
            }
            for (int i = 0; i < clientsCount; i++)
            {
                int match = random.Next(1, 4);
                int[] model = new int[match];
                int sum = 0;

                for (int j = 0; j < model.Length; j++)
                {
                    var car = autos[random.Next(0, autos.Count)];
                    model[j] = car.model;
                    sum += car.price;
                }

                var client = new
                {
                    model = model,
                    name = Lesson_Instruments.GetSomeText(10, ' '),
                    number = "096" + random.Next(1000000, 9999999),
                    carsQuantity = match,
                    costAllCars = sum,
                };

                clients.Add(client);
            }

            DrawUI();
            Console.CursorVisible = false;

            while (!exit)
            {
                Console.SetCursorPosition(x, y);
                var key = Console.ReadKey(true);

                switch (key.Key)
                {
                    case ConsoleKey.UpArrow:
                        if (--controllerV < 0) controllerV = clientsCount - 1;
                        break;
                    case ConsoleKey.DownArrow:
                        if (++controllerV >= clientsCount) controllerV = 0;
                        break;
                    case ConsoleKey.Enter:
                        openResult = true;
                        break;
                    case ConsoleKey.E:
                        exit = true;
                        break;
                }

                DrawUI();
                if (openResult)
                {
                    DrawResult();
                    openResult = false;
                }
                
            }

            Console.CursorVisible = true;

            void DrawUI()
            {
                for (int i = 0; i < clientsCount; i++)
                {
                    string text = $"[{clients[i].name}]"+ new string(' ', 100);
                    if(i == controllerV)
                    {
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.WriteLine(text);
                        Console.ForegroundColor = ConsoleColor.Green;
                    }
                    else
                    {
                        Console.WriteLine(text);
                    }
                }
                Lesson_Instruments.Clear(10);
            }
            void DrawResult()
            {
                List<string> result = new List<string>();
                string res = "";

                for (int i = 0; i < clients[controllerV].model.Length; i++)
                {
                    var join = from car in autos
                               join customer in clients on car.model equals clients[controllerV].model[i]
                               where customer.name == clients[controllerV].name
                               select new
                               {
                                   clientName = customer.name,
                                   clientPhone = customer.number,
                                   clientCarsCount = customer.carsQuantity,
                                   clientCostAllCars = customer.costAllCars,
                                   carPrice = car.price,
                                   carBrand = car.brand,
                                   carModel = car.model,
                                   carYear = car.year,
                                   carColor = car.color,
                               };
                    foreach (var item in join)
                    {
                        if(i == 0)
                        {
                            res += 
                                $"Name: {item.clientName}" +
                                $"\nPhone: {item.clientPhone}" +
                                $"\nCars quantyty: {item.clientCarsCount}" +
                                $"\nCost of all cars: {item.clientCostAllCars} $";
                        }
                        else
                        {
                            res += new string('\n', 3);
                        }

                        res += $"\n"+ new string('-', 25) +
                        $"\nAuto price: {item.carPrice} $" +
                        $"\nAuto brand: {item.carBrand}" +
                        $"\nAuto model: {item.carModel}" +
                        $"\nAuto year: {item.carYear}" +
                        $"\nAuto color: {item.carColor}";
                    }
                    
                    result.Add(res);
                    res = "";
                }
                
                Console.SetCursorPosition(x + 15, y);

                Lesson_Instruments.InlineWriter(5, result.ToArray());
            }
        }
        private void Sequence()
        {
            Random random = new Random();
            var order = Enumerable.Range(1, 30).Select(i => random.Next(-100, 100)).ToArray();

            Console.Write("\n> Number row: ");
            for (int i = 0; i < order.Length; i++)
            {
                Console.Write($"[{order[i]}] ");
            }

            int min = order.FirstOrDefault(x => x > 0);
            int max = order.LastOrDefault(x => x < 0);

            Console.WriteLine("\n> Min: "+min);
            Console.WriteLine("> Max: "+max);
        }
        private void Fitness()
        {
            List<dynamic> clients = new List<dynamic>();
            Random random = new Random();
            List<string> result = new List<string>();

            Console.WriteLine();
            int count = (int)ILesson.Read<uint>("How many clients in the fitnes center: ", inline: true);
            Console.WriteLine();

            for (int i = 0; i < count; i++)
            {
                clients.Add(new 
                {
                    id = random.Next(10000000, 99999999),
                    year = random.Next(1970, 2010),
                    month = random.Next(1, 13),
                    duration = random.Next(20, 200),
                });
            }

            int minValue = clients.Min(x => x.duration);
            var minimals = clients.Where(x => x.duration == minValue).ToArray();
            var last = minimals[minimals.Length - 1];

            
            for (int i = 0; i < minimals.Length; i++)
            {
                result.Add(
                    $"ID: {minimals[i].id}" +
                    $"\nYear: {minimals[i].year}" +
                    $"\nMonth: {minimals[i].month}" +
                    $"\nDuration: {minimals[i].duration}");
            }
            Console.WriteLine("> All clients\n" + new string('-', 30));
            Lesson_Instruments.InlineWriter(5, result.ToArray());
            Console.WriteLine(new string('-', 30));

            Console.WriteLine(
                $"\n> Last client\n" + new string('-', 15) +
                $"\nID: {last.id}" +
                $"\nYear: {last.year}" +
                $"\nMonth: {last.month}" +
                $"\nDuration: {last.duration}" +
                $"\n"+ new string('-', 15));
        }
        private void Vokabulary()
        {
            dynamic[] vokabulary = new[]
            {
                new { native = "яблуко", english = "apple" },
                new { native = "сонце", english = "sun" },
                new { native = "кіт", english = "cat" },
                new { native = "музика", english = "music" },
                new { native = "будівля", english = "building" },
                new { native = "книга", english = "book" },
                new { native = "дерево", english = "tree" },
                new { native = "море", english = "sea" },
                new { native = "гроші", english = "money" },
                new { native = "планета", english = "planet" },
                new { native = "чай", english = "tea" },
                new { native = "пісок", english = "sand" },
                new { native = "здоров'я", english = "health" },
                new { native = "небо", english = "sky" },
                new { native = "ресторан", english = "restaurant" },
                new { native = "мова", english = "language" },
                new { native = "місяць", english = "moon" },
                new { native = "комп'ютер", english = "computer" },
                new { native = "подорож", english = "travel" },
                new { native = "дощ", english = "rain" },
            };
            int controllerV = 0;

            Console.WriteLine("\nVokabulary (\"E\" - exit)\n");

            int x = Console.CursorLeft;
            int y = Console.CursorTop;

            do
            {
                Console.SetCursorPosition(x, y);
                DrawUI();

                var key = Console.ReadKey(true);
                if (key.Key == ConsoleKey.E) break;

                switch (key.Key)
                {
                    case ConsoleKey.UpArrow:
                        if (--controllerV < 0) controllerV = vokabulary.Length - 1;
                        break;
                    case ConsoleKey.DownArrow:
                        if (++controllerV >= vokabulary.Length) controllerV = 0;
                        break;
                }

            } while (true);

            void DrawUI()
            {
                for (int i = 0; i < vokabulary.Length; i++)
                {
                    string text = vokabulary[i].native;
                    if (i == controllerV)
                    {
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.WriteLine($"[{text}] [{vokabulary[i].english}]");
                        Console.ForegroundColor = ConsoleColor.Green;
                    }
                    else
                    {
                        Console.WriteLine($"[{text}]"+new string(' ', 30));
                    }
            }
            }
        }
    }
}
namespace ClassesOfLesson17
{
    public class Calculator
    {
        public static dynamic Sum<T>(params T[] arguments) where T : struct
        {
            if (arguments == null) throw new NullReferenceException();
            dynamic sum = default(T);
            foreach (dynamic arg in arguments)
            {
                sum += arg;
            }
            return sum;
        }
        public static dynamic Sub<T>(params T[] arguments) where T : struct
        {
            if (arguments == null) throw new NullReferenceException();
            dynamic sum = default(T);
            foreach (dynamic arg in arguments)
            {
                sum -= arg;
            }
            return sum;
        }
        public static dynamic Mul<T>(params T[] arguments) where T : struct
        {
            if (arguments == null) throw new NullReferenceException();
            dynamic sum = arguments[0];
            bool cont = false;
            foreach (dynamic arg in arguments)
            {
                if (!cont)
                {
                    cont = true;
                    continue;
                }
                try
                {
                    sum *= arg;
                }
                catch(Exception) { return "error"; }
            }
            return sum;
        }
        public static dynamic Div<T>(params T[] arguments) where T : struct
        {
            if (arguments == null) throw new NullReferenceException();
            dynamic sum = arguments[0];
            bool cont = false;
            foreach (dynamic arg in arguments)
            {
                if (!cont)
                {
                    cont = true;
                    continue;
                }
                try
                {
                    sum /= arg;
                }
                catch (Exception) { return "error"; }
            }
            return sum;
        }
    } 
}
