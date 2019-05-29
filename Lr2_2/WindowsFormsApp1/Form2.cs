using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;

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
            int dw = 5; // число символов в слове
            double[] p1 = new double[m]; //распределение
            double[] p = new double[m]; //распределение для создания строки
            double[] q = new double[m]; //кумулятивная вероятность
            double[] sigm = new double[m]; //кодировка в десятичной форме
            string[] alp = new string[m]; //алфавит слов
            string[] alph = new string[m]; //кодировка в двоичной форме
            string[] en = new string[m]; // алфавит кодов
            string[] inw = new string[dw]; //входное слово
            int[] wl = new int[m];
            string bf = "";
            string fl = "";
            double A_l = 0; // Средняя длина
            double Entr = 0; // Энтропия
            
           

            fl = textBox1.Text;
            var sep = new char[] { ' ' };
            var st1 = fl.Split(sep, StringSplitOptions.RemoveEmptyEntries);

            string[] arr = new string[st1.Length];
            for (int i = 0; i < st1.Length; i++)
                arr[i] = st1[i];

            IP.Write("Input.txt", arr);
            //Form2 f = new Form2();

            if (radioButton1.Checked)
                p1 = IP.ReadArray("P1.txt");
            if (radioButton2.Checked)
                p1 = IP.ReadArray("P2.txt");
            if (radioButton3.Checked)
                p1 = IP.ReadArray("Rav.txt");


            // p1 = IP.ReadArray("P1.txt");
            alp = IP.Read("ALP.txt");

            //ВЕСЬ КОД НИЖЕ РАБОТАЕТ !!!ВМЕСТО!!! ВВОДА СЛОВ ВРУЧНУЮ. ОН САМ СОЗДАЁТ ПОСЛЕДОВАТЕЛЬНОСТЬ СИМВОЛОВ ДЛЯ КОДИРОВАНИЯ
            //ДОБАВЬ КНОПКИ ДЛЯ ВЫБОРА РАСПРЕДЕЛЕНИЯ ДЛЯ ФОРМИРОВАНИЯ СТРОКИ 
            if (radioButton6.Checked)
                p = IP.ReadArray("P1.txt");
            if (radioButton5.Checked)
                p = IP.ReadArray("P2.txt");
            if (radioButton4.Checked)
                p = IP.ReadArray("Rav.txt");

            string str_buf = "";
            double p_i;
            int pb;
            int simv = 0;
            for (int i = 0; i < 7; i++)
            {
                p_i = 1000 * p[i];
                pb = (int)p_i;
                simv += pb;
                for (int j = 0; j < p_i; j++)
                {
                    str_buf += alp[i];
                    str_buf += " ";
                }
            }
            textBox8.Text = simv.ToString();

            textBox1.Text = str_buf;

            var st3 = str_buf.Split(sep, StringSplitOptions.RemoveEmptyEntries);

            string[] ar = new string[st3.Length];
            for (int i = 0; i < st3.Length; i++)
                ar[i] = st3[i];//МАССИВ AR НУЖНО ЗАПИСАТЬ В ФАЙЛ. 

            IP.Write("Input.txt", ar);

            /////////////////////////////////////////////////////

            inw = IP.Read("Input.txt");

            q = IP.Cumul_Prob(p1, m);
            sigm = IP.Alphabet(m, p1, q);
            wl = IP.Words_len(p1, m);
            alph = IP.TenToBin(wl, sigm, m);
            bf = IP.Encode(alph, inw, alp);
            textBox2.Text = bf;

            textBox6.Clear();
            // Вывод закодированного алфавита
            for (int j = 0; j < m; j++)
                textBox6.Text += alp[j].ToString() + ": " + alph[j].ToString() + Environment.NewLine;

            textBox5.Clear();
            if (IP.Kraft(wl, m))  // Проверка неравенства Крафта
                textBox5.Text = "Выполняется";
            else
                textBox5.Text = "Не выполняется";

            
            A_l = IP.Average_length(wl, p1, m);
            textBox3.Clear();
            textBox3.Text = A_l.ToString(); // Средняя длина

            Entr = IP.Entropy(p1, m);
            textBox7.Clear();
            textBox7.Text = IP.Entropy(p1, m).ToString(); // Энтропия

            textBox4.Clear();
            textBox4.Text = IP.Redundancy(A_l, Entr).ToString(); // Избыточность 

            var st2 = bf.Split(sep, StringSplitOptions.RemoveEmptyEntries);
            
            string[] t = new string[st2.Length];
            for (int i = 0; i < st2.Length; i++)
                t[i] = st2[i];
            IP.Write("DInput.txt", t);
        }
    }

      
}
 