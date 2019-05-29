using System;
using System.Windows.Forms;


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
            const int N = 32;
            string[] alph = new string[N] { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12", "13", "14", "15", "16", "17", "18", "19", "20", "21", "22", "23", "24", "25", "26", "27", "28", "29", "30", "31" };
            string[] bin = new string[N] { "00000", "00001", "00010", "00011", "00100", "00101", "00110", "00111", "01000", "01001", "01010", "01011", "01100", "01101", "01110", "01111", "10000", "10001", "10010", "10011", "10100", "10101", "10110", "10111", "11000", "11001", "11010", "11011", "11100", "11101", "11110", "11111" };
            string[] erp = new string[9] { "0101", "1011", "1100", "0110", "0011", "1000", "0100", "0010", "0001" };
            int[,] G = new int[5, 9] { {1, 0, 0, 0, 0,   0, 1, 0, 1},
                                       {0, 1, 0, 0, 0,   1, 0, 1, 1},
                                       {0, 0, 1, 0, 0,   1, 1, 0, 0},
                                       {0, 0, 0, 1, 0,   0, 1, 1, 0},
                                       {0, 0, 0, 0, 1,   0, 0, 1, 1}};

            int[,] Ht = new int[9, 4] { {0, 1, 0, 1 },
                                        {1, 0, 1, 1 },
                                        {1, 1, 0, 0 },
                                        {0, 1, 1, 0 },
                                        {0, 0, 1, 1 },
                                        {1, 0, 0, 0 },
                                        {0, 1, 0, 0 },
                                        {0, 0, 1, 0 },
                                        {0, 0, 0, 1 },};

            string[] inw = new string[dw]; //входное слово
            string[] fxw = new string[dw]; //исправленное входное слово

            var sep = new char[] { ' ' };
            string[] binalph = new string[m];
            string bs = "";
            string fl = "";

            binalph = IP.Bicode(bin, G);

            inw = IP.Read("DInput.txt");
            
            fl = IP.Errors(alph, inw, binalph);

            fxw = IP.Fix(erp, inw, Ht);

            bs = IP.Decode(alph, fxw, binalph);

            for (int i = 0; i < binalph.Length; i++)           
                richTextBox1.Text += alph[i] + ": " + bin[i] + ": " + binalph[i] + Environment.NewLine;

            for (int i = 0; i < inw.Length; i++)
                textBox1.Text += inw[i] + " ";

            textBox2.Text = bs;
            textBox3.Text = fl;
            for (int i = 0; i < fxw.Length; i++)
                textBox4.Text += fxw[i] + " ";
        }
    }    
}
