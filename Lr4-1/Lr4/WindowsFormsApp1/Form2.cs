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

            int m = 7;

            //матрица-ключ
            string[,] G = new string[5, 5]; /* { {"A", "B", "C", "D", "E" },
                                             {"F", "G", "H", "I", "K" },
                                             {"L", "M", "N", "O", "P" },
                                             {"Q", "R", "S", "T", "U" },
                                             {"V", "W", "X", "Y", "Z" }, };*/


            string[] alph = new string[25] { "A", "B", "C", "D", "E", "F", "G", "H", "I", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z" };

            string[] inw = new string[m]; //входное слово
            string[] key = new string[m]; //ключ
            string[] outw = new string[m];//выходное слово
            int[] dh = new int[2];
            string bs = "";
            string fl = "";
            fl = textBox1.Text;
            bs = textBox3.Text;
            var sep = new char[] { ' ' };
            var sep1 = new char[] { '_' };
            var st1 = fl.Split(sep, StringSplitOptions.RemoveEmptyEntries);
            IP.Write("Key.txt", st1);
            var st2 = bs.Split(sep1, StringSplitOptions.RemoveEmptyEntries);
            IP.Write("Input.txt", st2);

            inw = IP.Read("Input.txt");
            key = IP.Read("Key.txt");
            G = IP.Key_Gen(key, alph);
            outw = IP.Code(inw, G);
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                    richTextBox2.Text += G[i, j] + " ";
                richTextBox2.Text += Environment.NewLine;
            }

            for (int i = 0; i < inw.Length; i++)
                textBox2.Text += outw[i]+ " ";

            IP.Write("DInput.txt", outw);
            
        }
    }      
}
 