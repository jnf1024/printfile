using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Printing;
using System.IO;

namespace PrintFile
{
    public partial class Form1 : Form
    {
        private string s;
        
        public Form1()
        {
            InitializeComponent();

            printDocument1.PrintPage +=new PrintPageEventHandler(printDocument1_PrintPage);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.Trim().Length > 0)
            {
                string[] files = Directory.GetFiles(textBox1.Text, "*.txt");
                foreach (string file in files)
                {
                    StreamReader reader = new StreamReader(new FileStream(file, FileMode.Open),Encoding.Default);
                    s = reader.ReadToEnd();
                    reader.Close();
                    printDocument1.Print();
                }
            }
        }

        private void printDocument1_PrintPage(object sender, PrintPageEventArgs e)
        {
            int i =0;
            int l = 0;
            
            Font ValueFont = new Font("宋体", 13, GraphicsUnit.Pixel);

            e.Graphics.MeasureString(s, ValueFont, e.MarginBounds.Size, StringFormat.GenericTypographic, out i, out l);
            e.Graphics.DrawString(s, ValueFont, Brushes.Black, e.MarginBounds, StringFormat.GenericTypographic);
            s = s.Substring(i);
            e.HasMorePages = (l == 65);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            folderBrowserDialog1.ShowDialog();
            textBox1.Text = folderBrowserDialog1.SelectedPath;
        }
    }
}
