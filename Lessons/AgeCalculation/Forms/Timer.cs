using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AgeCalculation
{
    public partial class Timer : Form
    {
        public Timer()
        {
            InitializeComponent();
        }

        private float time;
        private float Time { get { return time; }
            set 
            {
                time = value;
                label2.Text = CountTime(time); 
            }
        }

        private string[] points = new string[] { };

        private void AddPoint(string item) 
        {
            int count = points.Length >= 5 ? 5 : points.Length; 
            string[] list = new string[count + 1];
            list[0] = item;
            for (int i = 0; i < count; i++)
            {
                list[i + 1] = points[i];
            }
            points = list;
            DrawPointList();
        }
        public void DrawPointList()
        {
            string res = "";
            for (int i = 0; i < points.Length; i++)
            {
                res += $"{i + 1}) {points[i]}\n";
            }
            label1.Text = res;
        }
        

        private string CountTime(float millisec)
        {
            TimeSpan span = TimeSpan.FromMilliseconds(millisec);
            string stringSpan = span.Hours.ToString();
            string hour = stringSpan.Length == 1 ? "0" + span.Hours : stringSpan;
            stringSpan = span.Minutes.ToString();
            string minuts = stringSpan.Length == 1 ? "0" + span.Minutes : stringSpan;
            stringSpan = span.Seconds.ToString();
            string seconds = stringSpan.Length == 1 ? "0" + span.Seconds : stringSpan;
            stringSpan = span.Milliseconds.ToString();
            string milliseconds = "";
            if (stringSpan == "0") milliseconds = "00";
            else milliseconds = stringSpan.Length == 2 ? "0" + stringSpan[0] : $"{stringSpan[0]}{stringSpan[1]}";

            return $"{hour}:{minuts}:{seconds}:{milliseconds}";
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Time += timer1.Interval;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            timer1.Enabled = true;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            timer1.Enabled = false;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            label1.Text = "";
            label2.Text = "00:00:00:00";
            timer1.Enabled = false;
            points = new string[] { };
            time = 0;
            DrawPointList();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (time > 0)
            {
                AddPoint(CountTime(time));
            }
        }
    }
}
