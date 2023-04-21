using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace AgeCalculation.Forms
{
    public partial class XmlForm : Form
    {
        public XmlForm()
        {
            InitializeComponent();
        }

        KnownColor[] colors = new KnownColor[] { };
        FontFamily[] fontFamilies = new FontFamily[]{ };
        FontStyle[] fontStyles = new FontStyle[] { };
        BorderStyle[] borderStyles = new BorderStyle[] { };

        private int colorBG;
        private int colorFont;
        private int font;
        private int fontSize = 5;
        private int fontStyle;
        private int boxStyle;

        const string pathDir = "D:\\Fork\\MyCourses_C-_Pro\\Lessons\\Lesson 2\\Files\\";
        const string fileName = "XmlFormPreferance.xml";
        string fullName = $"{pathDir}{fileName}";
        private void XmlForm_Load(object sender, EventArgs e)
        {
            StartSettings();

            if (File.Exists(fullName))
            {
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(fullName);

                colorBG = int.Parse(xmlDoc.SelectSingleNode("//ColorBG").InnerText);
                colorFont = int.Parse(xmlDoc.SelectSingleNode("//ColorFont").InnerText);
                font = int.Parse(xmlDoc.SelectSingleNode("//Font").InnerText);
                fontSize = int.Parse(xmlDoc.SelectSingleNode("//FontSize").InnerText);
                fontStyle = int.Parse(xmlDoc.SelectSingleNode("//FontStyle").InnerText);
                boxStyle = int.Parse(xmlDoc.SelectSingleNode("//BoxStyle").InnerText);

                 textBox1.BackColor = Color.FromKnownColor(colors[colorBG]);
                textBox1.ForeColor = Color.FromKnownColor(colors[colorFont]);
                textBox1.Font = new Font(textBox1.Font.FontFamily, fontSize);
                textBox1.Font = new Font(fontFamilies[font], textBox1.Font.Size);
                textBox1.Font = new Font(textBox1.Font, fontStyles[fontStyle]);
                textBox1.BorderStyle = borderStyles[boxStyle];
            }
            else
            {
                textBox1.BackColor = Color.FromKnownColor(colors[colorBG]);
                textBox1.ForeColor = Color.FromKnownColor(colors[colorFont]);
                textBox1.Font = new Font(textBox1.Font.FontFamily, fontSize);
                textBox1.Font = new Font(fontFamilies[font], textBox1.Font.Size);
                textBox1.Font = new Font(textBox1.Font, fontStyles[fontStyle]);
                textBox1.BorderStyle = borderStyles[boxStyle];
            }

            void StartSettings()
            {
                var arr = Enum.GetValues(typeof(KnownColor));
                colors = new KnownColor[arr.Length];
                for (int i = 0; i < colors.Length; i++)
                {
                    colors[i] = (KnownColor)arr.GetValue(i);
                }

                arr = Enum.GetValues(typeof(FontStyle));
                fontStyles = new FontStyle[arr.Length];
                for (int i = 0; i < fontStyles.Length; i++)
                {
                    fontStyles[i] = (FontStyle)arr.GetValue(i);
                }

                arr = Enum.GetValues(typeof(BorderStyle));
                borderStyles = new BorderStyle[arr.Length];
                for (int i = 0; i < borderStyles.Length; i++)
                {
                    borderStyles[i] = (BorderStyle)arr.GetValue(i);
                }

                fontFamilies = FontFamily.Families;
            }
        }

        private void buttonBGColor_Click(object sender, EventArgs e)
        {
            if(++colorBG >= colors.Length) colorBG = 0;

            textBox1.BackColor = Color.FromKnownColor(colors[colorBG]);
        }

        private void buttonFont_Click(object sender, EventArgs e)
        {
            if(++colorFont >= colors.Length) colorFont = 0;

            textBox1.ForeColor = Color.FromKnownColor(colors[colorFont]);
        }

        private void buttonSizeFont_Click(object sender, EventArgs e)
        {
            if (++fontSize >= 40) fontSize = 5;

            textBox1.Font = new Font(textBox1.Font.FontFamily, fontSize);
        }

        private void buttonFontName_Click(object sender, EventArgs e)
        {
            if (++font >= fontFamilies.Length) font = 0;

            textBox1.Font = new Font(fontFamilies[font], textBox1.Font.Size);
        }

        private void buttonFontStyle_Click(object sender, EventArgs e)
        {
            if(++fontStyle >= fontStyles.Length) fontStyle = 0;

            textBox1.Font = new Font(textBox1.Font, fontStyles[fontStyle]);
        }

        private void buttonBoxStyle_Click(object sender, EventArgs e)
        {
            if(++boxStyle >= borderStyles.Length) boxStyle = 0;

            textBox1.BorderStyle = borderStyles[boxStyle];
        }

        private void XmlForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            
        }

        private void XmlForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (!Directory.Exists(pathDir)) Directory.CreateDirectory(pathDir);
            if (!File.Exists(fullName)) File.WriteAllText(fullName, "");

            XmlDocument document = new XmlDocument();

            XmlElement xml = document.CreateElement("Preferance");
            document.AppendChild(xml);

            XmlElement elementColorBg = document.CreateElement("ColorBG");
            elementColorBg.InnerText = colorBG.ToString();
            xml.AppendChild(elementColorBg);

            XmlElement elementColorFont = document.CreateElement("ColorFont");
            elementColorFont.InnerText = colorFont.ToString();
            xml.AppendChild(elementColorFont);

            XmlElement elementFont = document.CreateElement("Font");
            elementFont.InnerText = font.ToString();
            xml.AppendChild(elementFont);

            XmlElement elementFontSize = document.CreateElement("FontSize");
            elementFontSize.InnerText = fontSize.ToString();
            xml.AppendChild(elementFontSize);

            XmlElement elementFontStyle = document.CreateElement("FontStyle");
            elementFontStyle.InnerText = fontStyle.ToString();
            xml.AppendChild(elementFontStyle);

            XmlElement elementBoxStyle = document.CreateElement("BoxStyle");
            elementBoxStyle.InnerText = boxStyle.ToString();
            xml.AppendChild(elementBoxStyle);

            document.Save(fullName);
        }
    }
}
