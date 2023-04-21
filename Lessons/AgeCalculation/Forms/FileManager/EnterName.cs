using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AgeCalculation.Forms
{
    public partial class EnterName : Form
    {
        public EnterName(string text)
        {
            labelFilePath.Text = text;
            InitializeComponent();
        }
    }
}
