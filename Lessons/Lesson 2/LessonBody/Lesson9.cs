using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ClassesOfLesson9;

namespace Lessons.LessonBody
{
    class Lesson9 : ILesson
    {
        public void Open()
        {
            ShowMagazines();
            Recorder();
            Documentation();
            ActionsWithDocument();
            ILesson.UserRequest();
        }

        private void ShowMagazines()
        {
            uint amountBook = ILesson.Read<uint>("Input book amount: ");
            uint amountJournal = ILesson.Read<uint>("Input journal amount: ");
            Console.WriteLine();

            for (int i = 0; i < amountBook; i++)
            {
                var book = new Book(); 
            }
            for (int i = 0; i < amountJournal; i++)
            {
                var journal = new Journal();
            }

            Magazine.Magazines(typeof(Book));
            Magazine.Magazines(typeof(Journal));
        }
        private void Recorder()
        {
            Player player = new Player();
            Console.WriteLine();
            Console.CursorVisible = false;
            int cursorX = Console.CursorLeft;
            int cursorY = Console.CursorTop;

            var sym = ILesson.ReadKey("Commands (\"R\" - record | \"P\" - play | \"E\" - exit)" + Player.clearH, (ref ConsoleKey res) => 
            {
                Console.Write("\n");
                switch (res)
                {
                    case ConsoleKey.R:
                        Console.SetCursorPosition(cursorX, cursorY);
                        Console.WriteLine("Commands (\"S\" - stop | \"P\" - pause)"+Player.clearH);
                        player.Record();
                        Lessons.Lesson_Instruments.Clear(cursorY + 2, 3);
                        break;
                    case ConsoleKey.P:
                        Console.SetCursorPosition(cursorX, cursorY);
                        Console.WriteLine("Choose record (\"<-- | -->\" - control | \"Enter\" - choose | \"D\" - delete | \"E\" - exit)" + Player.clearH);
                        player.Play();
                        Lessons.Lesson_Instruments.Clear(cursorY + 1, 3);
                        break;
                    case ConsoleKey.E:
                        return true;
                }
                return false;
            }, true, false, cursorX, cursorY);
        }
        private void Documentation()
        {
            Console.CursorVisible = false;

            int x = Console.CursorLeft;
            int y = Console.CursorTop;
            int num = 0;
            bool exit = false;
            int lastX = x;
            int lastY = y;

            while (!exit)
            {
                Console.SetCursorPosition(x, y);
                Console.WriteLine("\nYour document was generate (\"E\" - exit)");
                Console.Write("Types: ");

                var types = Assembly.GetAssembly(typeof(Document)).GetTypes().Where(type => type.IsSubclassOf(typeof(Document))).ToList();
                for (int i = 0; i < types.Count; i++)
                {
                    if (num == i)
                    {
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                    Console.Write($"[{types[i].Name}] ");
                    Console.ForegroundColor = ConsoleColor.Green;
                }
                Console.WriteLine();

                if (Console.KeyAvailable)
                {
                    var sym = Console.ReadKey(true).Key;

                    switch (sym)
                    {
                        case ConsoleKey.LeftArrow:
                            if(--num < 0)
                            {
                                num = types.Count - 1;
                            }
                            break;
                        case ConsoleKey.RightArrow:
                            if(++num > types.Count - 1) {
                                num = 0;
                            }
                            break;
                        case ConsoleKey.Enter:
                            var doc = Activator.CreateInstance(types[num]);
                            var met = doc.GetType().GetMethod("GetDocument");
                            Console.WriteLine(met.Invoke(doc, null));
                            lastX = Console.CursorLeft;
                            lastY = Console.CursorTop;
                            break;
                        case ConsoleKey.E:
                            exit = true;
                            break;
                    }
                }

                Thread.Sleep(50);
            }

            Console.SetCursorPosition(lastX, lastY);
            Console.CursorVisible = true;
        }
        private void ActionsWithDocument()
        {
            Console.CursorVisible = false;
            int x = Console.CursorLeft;
            int y = Console.CursorTop;
            Console.WriteLine();

            string[] actions = new string[] { "[Open]", "[Create]", "[Change]", "[Save]" };
            var types = Task.Run<AbstractHandler[]>(() => 
            {
                var res = Assembly.GetAssembly(typeof(AbstractHandler)).GetTypes().Where(type => type.IsSubclassOf(typeof(AbstractHandler))).ToArray();

                var result = new List<AbstractHandler>();

                for (int i = 0; i < res.Length; i++)
                {
                    var activ = Activator.CreateInstance(res[i]);
                    result.Add((AbstractHandler)activ);
                }
                return result.ToArray();
            }).Result;

            bool exit = false;
            int verticalNum = 0;
            int docTypeNum = 0;
            int docActionNum = 0;

            while (!exit)
            {
                Console.SetCursorPosition(x, y);
                Console.WriteLine("\nManagement takes place with help of \"Arrows button\"");
                Console.WriteLine("(\"Enter\" - accept | \"E\" - exit)\n");

                Console.Write("Document type: ");
                for (int i = 0; i < types.Length; i++)
                {
                    if(docTypeNum == i)
                    {
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                    Console.Write($"[{types[i].GetExtention()}] ");
                    Console.ForegroundColor = ConsoleColor.Green;
                }
                if (verticalNum == 0) Console.Write(" <==");
                else Console.Write("     ");
                Console.WriteLine();

                Console.Write("Actions: ");
                for (int i = 0; i < actions.Length; i++)
                {
                    if (docActionNum == i)
                    {
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                    Console.Write(actions[i] + " ");
                    Console.ForegroundColor = ConsoleColor.Green;
                }
                if (verticalNum == 1) Console.Write(" <==");
                else Console.Write("     ");

                var sym = Console.ReadKey(true).Key;

                switch (sym)
                {
                    case ConsoleKey.LeftArrow:
                        if(verticalNum == 0)
                        {
                            if (--docTypeNum < 0) docTypeNum = types.Length - 1;
                        }
                        else if(verticalNum == 1)
                        {
                            if (--docActionNum < 0) docActionNum = actions.Length - 1;
                        }
                        break;
                    case ConsoleKey.RightArrow:
                        if (verticalNum == 0)
                        {
                            if (++docTypeNum > types.Length - 1) docTypeNum = 0;
                        }
                        else if (verticalNum == 1)
                        {
                            if (++docActionNum > actions.Length - 1) docActionNum = 0;
                        }
                        break;
                    case ConsoleKey.UpArrow:
                        if (--verticalNum < 0) verticalNum = 1;
                        break;
                    case ConsoleKey.DownArrow:
                        if (++verticalNum > 1) verticalNum = 0;
                        break;
                    case ConsoleKey.Enter:
                        types[docTypeNum].Action(actions[docActionNum]);
                        break;
                    case ConsoleKey.E:
                        exit = true;
                        Console.WriteLine("\n");
                        break;
                }
            }
            Console.CursorVisible = true;
        }
    }
}
namespace ClassesOfLesson9
{
    interface Printable
    {
        public void Print();
    }
    class Book : Magazine
    {
        public Book()
        {
            Name = "#" + Name;
        }
    }
    class Journal : Magazine
    {
        public Journal()
        {
            Name = "@" + Name;
        }
    }
    class Magazine : Printable
    {
        public Magazine()
        {
            var arr = new Printable[_printables.Length + 1];
            _printables.CopyTo(arr, 0);
            arr[arr.Length - 1] = this;
            _printables = arr;
            CreateName();
        }
        private string[] names = new string[] { "q","w","e","r","t","y","u","i","o","p","a","s","d","f","g","h","j","k","l","z","x","c","v","b","n","m" };
        public static Printable[] _printables = new Printable[] { };
        public string Name { get; set; }
        private void CreateName()
        {
            Random random = new Random();
            int chars = random.Next(5, 10);
            for (int i = 0; i < chars; i++)
            {
                Name += names[random.Next(0, names.Length)];
            }
        }
        public static void Magazines(Type type)
        {
            Console.WriteLine(type.Name);
            Console.WriteLine("--------------------");
            foreach (var item in _printables)
            {
                if(item.GetType() == type)
                {
                    item.Print();
                }
            }
            Console.WriteLine("--------------------\n");
        }
        public void Print()
        {
            Console.WriteLine("==> " + Name);
        }
    }


    public interface IPlayable
    {
        void Play();
        void Pause();
        void Stop();
    }
    public interface IRecordable
    {
        void Record();
        void Pause();
        void Stop();
    }
    public class Player : IPlayable, IRecordable
    {
        public List<List<int>> records = new List<List<int>>();
        public static string clearH = "                                                   ";

        public void Record()
        {
            List<int> rec = new List<int>();
            int bit = NewBit();
            int currentBit = 1;
            
            int x = Console.CursorLeft;
            int y = Console.CursorTop;

            string line = "";
            string sym = "";
            string status = "";

            while (sym != "S")
            {
                Console.SetCursorPosition(x, y);
                string st = "";
                for (int i = 0; i < currentBit; i++)
                {
                    st += "|";
                }
                line = st+clearH;

                if (currentBit > bit) currentBit--;
                else if (currentBit < bit) currentBit++;
                else bit = NewBit();
                
                Console.Write("Recording: "+ line);
                Console.Write("\n==> " + status);
                if (Console.KeyAvailable)
                {
                    sym = Console.ReadKey(true).Key.ToString();
                    if(sym == "P")
                    {
                        status = "Pause";
                        Console.SetCursorPosition(x, y);
                        Console.Write("Recording: " + line);
                        Console.Write("\n==> " + status);

                        string newSym = "";
                        while(newSym != "P" && newSym != "S")
                        {
                            if (Console.KeyAvailable)
                            {
                                newSym = Console.ReadKey(true).Key.ToString();
                                if (newSym == "S")
                                {
                                    sym = newSym;
                                }
                                else if (newSym == "P") status = clearH;
                            }

                            Thread.Sleep(30);
                        }
                    }
                }

                Thread.Sleep(30);
            }

            records.Add(rec);
            Console.SetCursorPosition(x, y);
            Console.Write($"(Record save) => name: [{rec.Count}s]" + clearH);

            int NewBit()
            {
                Random random = new Random();
                int num = random.Next(1, 25);
                rec.Add(num);
                return num;
            }
        }
        public void Play()
        {
            int x = Console.CursorLeft;
            int y = Console.CursorTop;

            int currentRec = 0;

            ConsoleKey sym;
            do
            {
                Console.SetCursorPosition(x, y);
                Console.Write("Records: ");
                if (records.Count > 0)
                {
                    for (int i = 0; i < records.Count; i++)
                    {
                        if (i == currentRec)
                        {
                            Console.ForegroundColor = ConsoleColor.White;
                        }
                        Console.Write("[" + records[i].Count + "s" + "] ");
                        Console.ForegroundColor = ConsoleColor.Green;
                    }
                }
                else
                {
                    Console.Write("No Records");
                }
                Console.Write(clearH);
                sym = Console.ReadKey(true).Key;
                switch (sym)
                {
                    case ConsoleKey.LeftArrow:
                        if (currentRec > 0) currentRec--;
                        break;
                    case ConsoleKey.RightArrow:
                        if (currentRec < records.Count - 1) currentRec++;
                        break;
                    case ConsoleKey.Enter:
                        if (records.Count == 0) break;
                        Console.SetCursorPosition(x, y - 1);
                        Console.WriteLine("Commands (\"S\" - stop | \"P\" - pause | \"R\" - repeat)" + clearH);

                        int currentBit = 1;
                        string line = "";
                        string status = "";
                        int count = 0;
                        int bit = NextBit();

                        while (sym != ConsoleKey.S)
                        {
                            Console.SetCursorPosition(x, y);
                            string st = "";
                            for (int i = 0; i < currentBit; i++)
                            {
                                st += "|";
                            }
                            line = st + clearH;

                            if (bit == -1)
                            {
                                line = "end" + clearH;
                                Console.Write("Playing: " + line);
                                Console.Write("\n==> " + status);

                                while (sym != ConsoleKey.S && sym != ConsoleKey.R)
                                {
                                    sym = Console.ReadKey(true).Key;
                                }

                                if(sym == ConsoleKey.R)
                                {
                                    count = 0;
                                    currentBit = 1;
                                    bit = NextBit();
                                    sym = ConsoleKey.Clear;
                                }
                                else if(sym == ConsoleKey.S)
                                {
                                    Console.SetCursorPosition(x, y - 1);
                                    Console.WriteLine("Choose record (\"<-- | -->\" - control | \"Enter\" - choose | \"E\" - exit)" + clearH);
                                    Lessons.Lesson_Instruments.Clear(y, 2);
                                }

                                continue;
                            }

                            if (currentBit > bit) currentBit--;
                            else if (currentBit < bit) currentBit++;
                            else bit = NextBit();

                            Console.Write("Playing: " + line);
                            Console.Write("\n==> " + status);
                            if (Console.KeyAvailable)
                            {
                                sym = Console.ReadKey(true).Key;
                                if (sym == ConsoleKey.P)
                                {
                                    status = "Pause";
                                    Console.SetCursorPosition(x, y);
                                    Console.Write("Playing: " + line);
                                    Console.Write("\n==> " + status);

                                    ConsoleKey newSym = ConsoleKey.Clear;
                                    while (newSym != ConsoleKey.P && newSym != ConsoleKey.S)
                                    {
                                        if (Console.KeyAvailable)
                                        {
                                            newSym = Console.ReadKey(true).Key;
                                            if (newSym == ConsoleKey.S)
                                            {
                                                sym = newSym;
                                                Console.SetCursorPosition(x, y - 1);
                                                Console.WriteLine("Choose record (\"<-- | -->\" - control | \"Enter\" - choose | \"E\" - exit)" + clearH);
                                                Lessons.Lesson_Instruments.Clear(y, 2);
                                            }
                                            else if (newSym == ConsoleKey.P) status = clearH;
                                        }

                                        Thread.Sleep(30);
                                    }
                                }
                                else if(sym == ConsoleKey.S)
                                {
                                    Console.SetCursorPosition(x, y - 1);
                                    Console.WriteLine("Choose record (\"<-- | -->\" - control | \"Enter\" - choose | \"E\" - exit)" + clearH);
                                    Lessons.Lesson_Instruments.Clear(y, 2);
                                }
                            }

                            Thread.Sleep(30);
                        }
                        break;

                        int NextBit()
                        {
                            int num = -1;
                            if (count < records[currentRec].Count - 1)
                            {
                                num = records[currentRec][count];
                                count++;
                            }
                            return num;
                        }
                    case ConsoleKey.D:
                        if (records.Count > 0)
                        {
                            records.RemoveAt(currentRec);
                            if (currentRec > records.Count - 1) currentRec--;
                        }
                        break;
                }
            }
            while (sym != ConsoleKey.E);

        }
        public void Stop()
        {
            throw new NotImplementedException();
        }
        public void Pause()
        {
            throw new NotImplementedException();
        }
    }


    public abstract class Document
    {
        public Document()
        {
            headLine = GetText(10, 20);
            documentContext = GetText(200, 400);
            footer = GetText(80, 100);
            
            string GetText(int lengthMin, int lengthMax)
            {
                string text = Lessons.Lesson_Instruments.someText;

                Random random = new Random();
                int startNum = random.Next(0, text.Length / 2);
                int length = random.Next(lengthMin, lengthMax);
                string res = "";

                for (int i = startNum; i < text.Length; i++)
                {
                    res += text[i];
                    if (res.Length == length) break;
                }

                return res;
            }
        }
        public string headLine { get; set; }
        public string documentContext { get; set; }
        public string footer { get; set; }

        private string line = "-------------------------";
        private int lineLength = 30;
        void HeadLine()
        {
            Console.WriteLine(line);
            Console.WriteLine("==> HeaderLine");
            Console.WriteLine(headLine);
        }
        void DocumentContext()
        {
            Console.WriteLine(line);
            Console.WriteLine("==> DocumentContext");
            string res = "";
            for (int i = 0; i < documentContext.Length; i++)
            {
                res += documentContext[i];
                if (i == 0) continue;
                if(i % lineLength == 0 || i == documentContext.Length - 1)
                {
                    Console.WriteLine(res);
                    res = "";
                }
            }
            
        }
        void Footer()
        {
            Console.WriteLine(line);
            Console.WriteLine("==> Footer");
            string res = "";
            for (int i = 0; i < footer.Length; i++)
            {
                res += footer[i];
                if (i == 0) continue;
                if (i % lineLength == 0 || i == footer.Length - 1)
                {
                    Console.WriteLine(res);
                    res = "";
                }
            }
        }
        public void GetDocument()
        {
            int y = Console.CursorTop + 1;
            Lessons.Lesson_Instruments.Clear(y, 30);
            Console.SetCursorPosition(0, y);
            Console.WriteLine("Document type: " + this.GetType().Name);
            HeadLine();
            DocumentContext();
            Footer();
            Console.WriteLine(line);
        }
    }
    public class DOC : Document { }
    public class PDF : Document { }
    public class TXT : Document { }


    public class AbstractHandler
    {
        protected string extention;
        public void Action(string methodName)
        {
            if (methodName.Contains("Open")) Open();
            else if (methodName.Contains("Create")) Create();
            else if (methodName.Contains("Change")) Change();
            else if (methodName.Contains("Save")) Save();
        }
        void Open()
        {
            Console.WriteLine("\nOpen File." + extention + Player.clearH);
        }
        void Create()
        {
            Console.WriteLine("\nCreate File." + extention + Player.clearH);
        }
        void Change()
        {
            Console.WriteLine("\nChange File." + extention + Player.clearH);
        }
        void Save()
        {
            Console.WriteLine("\nSave File." + extention + Player.clearH);
        }
        public string GetExtention() => extention;
        
    }
    public class XMLHendler : AbstractHandler
    {
        public XMLHendler()
        {
            extention = "xml";
        }
    }
    public class TXTHendler : AbstractHandler
    {
        public TXTHendler()
        {
            extention = "txt";
        }
    }
    public class DOCHendler : AbstractHandler 
    {
        public DOCHendler()
        {
            extention = "doc";
        }
    }
}
