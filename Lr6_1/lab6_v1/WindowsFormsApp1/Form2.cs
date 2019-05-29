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
            var CR = new Creater();

            int m = 1;
            string[] inw = new string[m];
            string[] n = new string[1];
            int N = Convert.ToInt32(textBox1.Text);
            n[0] = N.ToString();

            CR.Write("Input.txt", n);

            n = CR.Read("Input.txt");

            N = Convert.ToInt32(n[0]);

            inw = CR.Creat_seq(N);

            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
            textBox5.Clear();

            textBox2.Text += inw[0];
            textBox3.Text += inw[2];
            textBox4.Text += inw[1];
            textBox5.Text += inw[3];
        }
    }      
}
 