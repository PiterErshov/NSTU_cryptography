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

            int m = 7; 
            //матрица-ключ
            string[,] G = new string[5, 5];/* { {"A", "B", "C", "D", "E" },
                                             {"F", "G", "H", "I", "K" },
                                             {"L", "M", "N", "O", "P" },
                                             {"Q", "R", "S", "T", "U" },
                                             {"V", "W", "X", "Y", "Z" }, };*/
            string[] alph = new string[25] { "A", "B", "C", "D", "E", "F", "G", "H", "I", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z" };

            string[] inw = new string[m]; //входное слово
            string[] key = new string[m]; //ключ
            string[] outw = new string[m];//выходное слово


            inw = IP.Read("DInput.txt");
            for (int i = 0; i < inw.Length; i++)
                textBox1.Text += inw[i] + " ";

            key = IP.Read("Key.txt");
            G = IP.Key_Gen(key, alph);
            outw = IP.Decode(inw, G);
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                    richTextBox1.Text += G[i, j] + " ";
                richTextBox1.Text += Environment.NewLine;
            }

            for (int i = 0; i < outw.Length; i++)
                textBox2.Text += outw[i] + " ";                        
        }
    }    
}
