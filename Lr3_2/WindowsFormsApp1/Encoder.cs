using System;
using System.Collections.Generic;
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
        public string[] Read(string fileName) //чтение строки
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
            int id = -1;
            for (int i = 0; i < m; i++)
            {
                if (arr == alp[i])
                {
                    id = i;
                }
            }
            return id;
        }        
        public string[] Bicode(string[] binw, int[,] G) // Кодирование в двоичную форму
        {
            int[] bw = new int[5];
            int[] bv = new int[4];
            int[] ver = new int[20];
            int[] buf = new int[4];
            int k = 0;
            int t = 5;
            int bufb = 0;

            string[] outw = new string[binw.Length];
            for (int b = 0; b < binw.Length; b++)
            {
                for (int j = 0; j < 5; j++)
                {
                    bw[j] = 0;
                }
                for (int j = 0; j < 4; j++)
                {
                    bv[j] = 0;
                }

                k = 0;
                for (int i = 0; i < 5; i++)
                {
                    for (int j = 0; j < 5; j++)
                    {
                        bufb = binw[b][i] - '0';
                        bw[j] += bufb * G[i, j];
                    }
                }
                for (int i = 0; i < 5; i++)
                {
                    t = 5;
                    k = 0;
                    while (t < 9)
                    {
                        bufb = binw[b][i] - '0';
                        bv[k] += bufb * G[i, t];
                        k++;
                        t++;
                    }
                }
                for (int i = 0; i < 5; i++)
                {
                    outw[b] += bw[i];
                }
                for (int i = 0; i < 4; i++)
                {
                    if(bv[i] % 2 == 0)
                        outw[b] += "0";
                    else
                        outw[b] += "1";
                }
            }
            
            return outw;
        }
        public string Encode(string[] en, string[] str, string[] alp) // Кодирование
        {
            string s = "";
            int b = 0;
            for (int i = 0; i < str.Length; i++)
            {
                b = Conform(str[i], alp, alp.Length);
                if (b != -1)
                {
                    s += en[b];
                    s += " ";
                }
            }

            return s;
        }

        /*
        public int Encode(int[,] G) // Кодирование
        {
            string s= "";
            int it = 0;
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    s = G[i, j].ToString();
                    it += int.Parse(s);
                }
            }            
            
            return it;
        } */
        
        public string Decode(string[] en, string[] str, string[] alp) //Декодирование
        {
            string s = "";
            int b = 0;
            for (int i = 0; i < str.Length; i++)
            {
                b = Conform(str[i], alp, alp.Length);
                if (b != -1)
                {
                    s += en[b];
                    s += " ";
                }
            }

            return s;
        }
        public string Errors(string[] en, string[] str, string[] alp) //Поиск ошибок
        {
            string s = "";
            int b = 0;
            for (int i = 0; i < str.Length; i++)
            {
                b = Conform(str[i], alp, alp.Length);
                if (b == -1)
                {
                    s += i;
                    s += " ";
                }
            }
            return s;
        }
        public string[] Rebuild(string[] binw, int[,] H) // исправление ошибок
        {
            int[] bw = new int[4];
            string[] outw = new string[binw.Length];
            int bufb = 0;
            for (int b = 0; b < binw.Length; b++)
            {
                for (int j = 0; j < 4; j++)
                {
                    bw[j] = 0;
                }

                for (int i = 0; i < 9; i++)
                {
                    for (int j = 0; j < 4; j++)
                    {
                        bufb = binw[b][i] - '0';
                        bw[j] += bufb * H[i, j];
                    }
                }

                for (int i = 0; i < 4; i++)
                {
                    if (bw[i] % 2 == 0)
                        outw[b] += "0";
                    else
                        outw[b] += "1";

                }
            }

            return outw;
        }
        public string[] Fix(string[] erp, string[] str, int[,] H) // Восстановление правильной входной последовательности
        {
            int[] bw = new int[4];
            string[] outw = new string[str.Length];
            string[] sind = new string[4];
            string er = "";
            string buf = "";
            var sep = new char[] { ' ' };
            int sn = 0;
            var sterr = er.Split(sep, StringSplitOptions.RemoveEmptyEntries);
            int[] err = new int[sterr.Length];
            for (int i = 0; i < sterr.Length; i++)
                err[i] = int.Parse(sterr[i]);

            sind = Rebuild(str, H);

            for(int i = 0; i < str.Length; i++)
            {
                buf = "";
                if (sind[i] != "0000")
                {

                    sn = Conform(sind[i], erp, erp.Length);
                    for (int j = 0; j < str[i].Length; j++)
                    {
                        if (j != sn)
                            buf += str[i][j];
                        else
                        {
                            if (str[i][sn].ToString() == "1")
                                buf += "0";
                            else
                                buf += "1";
                        }
                    }
                }
                else
                    buf = str[i];

                outw[i] = buf;
            }
            return outw;
        }
        public int[] DistHamm(string[] str) // Расстояние Хэмминга
        {
            int[] h = new int[(str.Length - 1) * str.Length];
            int j = 0, hi = 0;
            for (int i = 0; i < str.Length; i++)
            {
                j = 0;
                while(j < str.Length)
                {
                    if (i != j)
                    {
                        for (int g = 0; g < 4; g++)
                        {
                            if (str[i][g] != str[j][g])
                                h[hi] += 1;
                        }
                        hi++;
                    }
                    j++;
                }
            }
            
            return h;
        }
        public string MinDH(int[] str) // Поиск d0
        {
            string buf = "";
            int min = 0;
            min = str[0];
            for (int i = 1; i < str.Length; i++)
            {
                if (min > str[i])
                    min = str[i];
            }
            buf = min.ToString();
            return buf;
        }
    }
}
