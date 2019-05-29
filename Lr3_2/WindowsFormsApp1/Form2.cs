using System;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }
    
    private void button1_Click(object sender, EventArgs e)
    {
            var IP = new Encoder();

            int m = 7; // число символов в алфавите
            const int N = 32;
            //алфавит слов
            string[] alph = new string[N] { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12", "13", "14", "15", "16", "17", "18", "19", "20", "21", "22", "23", "24", "25", "26", "27", "28", "29", "30", "31" };
            //двоичная форма алфавита
            string[] bin = new string[N] { "00000", "00001", "00010", "00011", "00100", "00101", "00110", "00111", "01000", "01001", "01010", "01011", "01100", "01101", "01110", "01111", "10000", "10001", "10010", "10011", "10100", "10101", "10110", "10111", "11000", "11001", "11010", "11011", "11100", "11101", "11110", "11111" };
            //порождающая матрица
            int[,] G = new int[5, 9] { {1, 0, 0, 0, 0,   0, 1, 0, 1},
                                       {0, 1, 0, 0, 0,   1, 0, 1, 1},
                                       {0, 0, 1, 0, 0,   1, 1, 0, 0},
                                       {0, 0, 0, 1, 0,   0, 1, 1, 0},
                                       {0, 0, 0, 0, 1,   0, 0, 1, 1}};
            //транспонированная проверочная матрица
            int[,] Ht = new int[9, 4] { {0, 1, 0, 1 },
                                        {1, 0, 1, 1 },
                                        {1, 1, 0, 0 },
                                        {0, 1, 1, 0 },
                                        {0, 0, 1, 1 },
                                        {1, 0, 0, 0 },
                                        {0, 1, 0, 0 },
                                        {0, 0, 1, 0 },
                                        {0, 0, 0, 1 },};

            string[] erp = new string[9] { "0101", "1011", "1100", "0110", "0011", "1000", "0100", "0010", "0001" };
            string[] inw = new string[m]; //входное слово
            string[] binalph = new string[m];//бинарный алфавит кодовых слов
            int[] dh = new int[m];
            string bs = "";
            string fl = "";

            binalph = IP.Bicode(bin, G);//получение бинарного алфавита кодовых слов

            fl = textBox1.Text;
            var sep = new char[] { ' ' };
            var st1 = fl.Split(sep, StringSplitOptions.RemoveEmptyEntries);
            
            IP.Write("Input.txt", st1);
            inw = IP.Read("Input.txt");

            bs = IP.Encode(binalph, inw, alph);
            textBox2.Text = bs;
            binalph = IP.Bicode(bin, G);

            var st2 = bs.Split(sep, StringSplitOptions.RemoveEmptyEntries);

            dh = IP.DistHamm(st2);
            //bf = IP.Fix(erp, rtes, Ht);
            //bs = IP.Decode(alph, bf, binalph);
            //fl = IP.Errors(alph, dtes, binalph);

            for (int i = 0; i < binalph.Length; i++)
                richTextBox2.Text += alph[i] + ": " + bin[i] + ": " + binalph[i] + Environment.NewLine;

            int hi = 0;
            for (int i = 0; i < st2.Length; i++)
            {
                int j = 0;
                while (j < st2.Length)
                {
                    if (i != j)
                    {
                        richTextBox1.Text += st2[i] + " " + st2[j] + " " + dh[hi].ToString() + Environment.NewLine;
                        hi++;
                    }
                    j++;
                }
            }

            textBox4.Text = "1";
            textBox5.Text = "16";
            double dp = 0;
            dp = 5 * (Math.Pow(2, 4) / (Math.Pow(2, 5) - 1));
            textBox6.Text = dp.ToString();
            if (dh.Length > 0)
                textBox7.Text = IP.MinDH(dh);
            else
                textBox7.Text = "0";

            var arr = bs.Split(sep, StringSplitOptions.RemoveEmptyEntries);

            IP.Write("DInput.txt", arr);
        }
    }      
}
 