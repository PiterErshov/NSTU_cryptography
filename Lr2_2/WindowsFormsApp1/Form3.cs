using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;


namespace WindowsFormsApp1
{
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var IP = new Encoder();

            int m = 7; // число символов в алфавите
            int dw = 5; // число символов в слове
            double[] p1 = new double[m]; //распределение
            double[] q = new double[m]; //кумулятивная вероятность
            double[] sigm = new double[m]; //кодировка в десятичной форме
            string[] alp = new string[m]; //алфавит слов
            string[] alph = new string[m]; //кодировка в двоичной форме
            string[] en = new string[m]; // алфавит кодов
            string[] inw = new string[dw]; //входное слово
            int[] wl = new int[m];
            string bf = "";
            string buf = "";
            var sep = new char[] { ' ' };

            if (radioButton1.Checked)
                p1 = IP.ReadArray("P1.txt");
            if (radioButton2.Checked)
                p1 = IP.ReadArray("P2.txt");
            if (radioButton3.Checked)
                p1 = IP.ReadArray("Rav.txt");

            alp = IP.Read("ALP.txt");
            inw = IP.Read("DInput.txt");
            for(int i = 0; i < inw.Length; i++)
            {
                buf += inw[i];
                buf += " ";
            }
            textBox1.Text = buf;
           
            q = IP.Cumul_Prob(p1, m);
            sigm = IP.Alphabet(m, p1, q);
            wl = IP.Words_len(p1, m);
            alph = IP.TenToBin(wl, sigm, m);
           
            bf = IP.Decode(alp, inw, alph);
            var st1 = bf.Split(sep, StringSplitOptions.RemoveEmptyEntries);

            string[] t = new string[st1.Length];
            for (int i = 0; i < st1.Length; i++)
                t[i] = st1[i];
            IP.Write("DOutput.txt", t);
            textBox2.Clear();
            textBox2.Text = bf;

            textBox6.Clear();
            for (int j = 0; j < m; j++)
                // Вывод закодированного алфавита
                textBox6.Text += alp[j].ToString() + ": " + alph[j].ToString() + Environment.NewLine; 


        }
    }
    
    
}
