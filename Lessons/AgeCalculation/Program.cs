using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace AgeCalculation
{
    class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            var form = new Form();

            var projectName = Process.GetCurrentProcess().ProcessName;
            var filePath = Directory.GetParent(Directory.GetCurrentDirectory());

            while (filePath.Name != projectName)
            {
                filePath = filePath.Parent;
            }

            var settings = File.ReadAllText(filePath.FullName + "\\bin\\Debug\\Settings.txt");
            
            switch (settings)
            {
                case "calculator":
                    form = new AgeCalculator();
                    break;
                case "timer":
                    form = new Timer();
                    break;
                case "database":
                    form = new DB_Grid();
                    break;
            }

            form.StartPosition = FormStartPosition.CenterScreen;
            Application.Run(form);
            
        }
    }
}
