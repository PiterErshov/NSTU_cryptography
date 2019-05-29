using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace WindowsFormsApp1
{
    class Encoder
    {

        public void Write(string fileName, string[] outw) //Запись входной строки и алфавита в файл
        {
            using (StreamWriter sw = new StreamWriter(fileName))
            {
                foreach (string str in outw)
                    sw.WriteLine(str);
            }
        }
        public double[] ReadArray(string fileName) //чтение распределения из файла 
        {
            List<double> list = new List<double>();
            double tmp;
            using (StreamReader reader = new StreamReader(fileName))
            {
                while (!reader.EndOfStream)
                {
                    string str = reader.ReadLine();
                    if (double.TryParse(str, out tmp))
                        list.Add(tmp);
                }
            }
            return list.ToArray();
        }

        public string[] Read(string fileName) //чтение строки и алфавита
        {
            List<string> list = new List<string>();
            using (StreamReader reader = new StreamReader(fileName))
            {
                while (!reader.EndOfStream)
                {
                    string str = reader.ReadLine();
                    list.Add(str);
                }
            }
            return list.ToArray();
        }

        public int Conform(string arr, string[] alp, int m) // поиск нужного индекса для кодирования
        {
            int id = 0;
            for (int i = 0; i < m; i++)
            {
                if (arr == alp[i])
                {
                    id = i;
                }
            }
            return id;
        }
        public double[] Cumul_Prob(double[] p, int m)//поиск кумулятивной вероятности
        {
            double[] q = new double[m];
            for (int i = 0; i < m - 1; i++)
                for (int j = 0; j < i - 1; j++)
                    q[i] += p[j];
            return q;
        }
        public double[] Alphabet(int m, double[] p, double[] q) //десятичная форма алфавита для кодирования
        {
            double[] sigm = new double[m];

            for (int i = 0; i < m; i++)
                sigm[i] = q[i] + p[i] / 2;

            return sigm;
        }

        public int[] Words_len(double[] p, int m) // создание массива с длиной кода для кажной буквы алфавита
        {
            int[] wl = new int[m];
            double b = 0;
            for (int i = 0; i < m; i++)
            {
                b = Math.Log(2 / p[i], 2);
                wl[i] = (int)Math.Ceiling(b);
            }
            return wl;
        }

        public bool Kraft(int[] wl, int m)//проверка неравентсва Крафта
        {
            double k = 0;
            for (int i = 0; i < m; i++)
                k += 1 / Math.Pow(2, wl[i]);

            if (k <= 1)
                return true;
            else
                return false;
        }


        public double Entropy(double[] p, int m) //поиск энтропии
        {
            double H = 0;
            for (int i = 0; i < m; i++)
                H += (-p[i] * Math.Log(p[i], 2));
            return H;
        }

        public double Average_length(int[] wl, double[] p, int m)// поиск средней длины
        {
            double L = 0;
            for (int i = 0; i < m; i++)
                L += wl[i] * p[i];

            return L;
        }
        public double Redundancy(double L, double H) // поиск избыточности
        {
            double r = 0;
            r = L - H;

            return r;
        }
        public string[] TenToBin(int[] wl, double[] sigm, int m) //перевод в двоичную систему
        {
            string r;
            double b = 0;
            int c = 0;
            string[] alph = new string[m];

            for (int i = 0; i < m; i++)
            {
                b = sigm[i];
                for (int j = 0; j < wl[i]; j++)
                {
                    b = b * 2;
                    c = (int)b;
                    r = c.ToString();
                    alph[i] += r;
                    if (b >= 1)
                        b -= 1;
                }
            }
            return alph;
        }

        public string Encode(string[] en, string[] str, string[] alp) //кодирование
        {
            string s = "";

            int sum = 0;
            for (int i = 0; i < str.Length; i++)
                sum += str[i].Length;
            sum /= 2;
            string[] strr = new string[sum];
            int flag = 1, j = 0, l = 0, k = 0;

            while (j < strr.Length)
            {
                while (l < str.Length)
                {
                    k = 0;
                    while (k < str[l].Length)
                    {

                        strr[j] += str[l][k];
                        if (flag == 2)
                        {
                            flag = 0;
                            j++;
                        }
                        flag++;
                        k++;
                    }
                    l++;
                }
            }

            for (int i = 0; i < strr.Length; i++)
            { 
                s += en[Conform(strr[i], alp, alp.Length)];
                s += " ";
            }

            return s;
        }

        public string Decode(string[] en, string[] str, string[] alp) //Декодирование
        {
            string s = "";
            for (int i = 0; i < en.Length; i++)
            {
                s += en[Conform(str[i], alp, alp.Length)];
                s += " ";
            }

            return s;
        }
    }
}
