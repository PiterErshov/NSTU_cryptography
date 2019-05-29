using System;
using System.Collections.Generic;
using System.IO;

namespace WindowsFormsApp1
{
    class Encoder
    {
        public void Write(string fileName, string[] outw) //Запись входной строки и ключа в файл
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

        public bool ver(string key, string alph) //проверка на отсутствие символа алфавита в словах-ключе
        {
            bool flag = false;

            for (int i = 0; i < key.Length; i++)
            {
                if (key[i].ToString() == alph)
                    flag = true;
            }
            return flag;
        }
        public string[,] Key_Gen(string[] key, string[] alph) //генерация ключа кодирования
        {
            int o = 0;
            string kbuf = "", buf = "";
            string[,] G = new string[5, 5];
            for (int i = 0; i < 2; i++)
                for (int j = 0; j < 5; j++)
                {
                    G[i, j] = key[i][j].ToString();
                }

            kbuf += key[0] + key[1];

            for (int i = 0; i < 25; i++)
            {
                if (!ver(kbuf, alph[i]))
                    buf += alph[i];
            }

            for (int i = 2; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    if (o < buf.Length)
                    {
                        G[i, j] = buf[o].ToString();
                        o++;
                    }
                }
            }
            return G;
        }

        public int[] IJCord(string inw, string[,] A)//поиск координат символа в матрице-ключе
        {
            int[] ij = new int[2];

            for (int i = 0; i < 5; i++)
                for (int j = 0; j < 5; j++)
                {
                    if (inw == A[i, j])
                    {
                        ij[0] = i;
                        ij[1] = j;
                    }
                }
            return ij;
        }

        public string[] Code(string[] inw, string[,] G)//кодирование
        {
            string[] cinw = new string[inw.Length];
            string buf = "", bf = "";
            int[] ij1 = new int[2];
            int[] ij2 = new int[2];
            int o = 0;

            for (int i = 0; i < inw.Length; i++)
            {
                buf = "";
                int j = 0;
                if (inw[i].Length % 2 == 0)
                {
                    bf = inw[i];
                }
                else
                {
                    bf = inw[i];
                    bf += "X";
                }
                o = 0;
                while(o < bf.Length)
                {
                    if (bf[o].ToString() == "J")
                    {
                        buf += "I";
                        o++;
                    }
                    else
                    {
                        buf += bf[o].ToString();
                        o++;
                    }
                }


                while (j < buf.Length)
                {
                    ij1 = IJCord(buf[j].ToString(), G);
                    ij2 = IJCord(buf[j + 1].ToString(), G);

                    if (ij1[0] > ij2[0] && ij1[1] != ij2[1])
                    {
                        cinw[i] += G[ij2[0], ij1[1]];
                        cinw[i] += G[ij1[0], ij2[1]];
                    }

                    if (ij1[0] < ij2[0] && ij1[1] != ij2[1])
                    {
                        cinw[i] += G[ij2[0], ij1[1]];
                        cinw[i] += G[ij1[0], ij2[1]];
                    }

                    if (ij1[0] == ij2[0] && ij1[1] != ij2[1])
                    {
                        if (ij1[1] == 4)
                        {
                            cinw[i] += G[ij1[0], 0];
                            cinw[i] += G[ij2[0], ij2[1] + 1];
                        }
                        else
                        {
                            if (ij2[1] == 4)
                            {
                                cinw[i] += G[ij1[0], ij1[1] + 1];
                                cinw[i] += G[ij1[0], 0];
                            }
                            else
                            {
                                cinw[i] += G[ij1[0], ij1[1] + 1];
                                cinw[i] += G[ij2[0], ij2[1] + 1];
                            }
                        }
                    }
                    if (ij1[1] == ij2[1] && ij1[0] != ij2[0])
                    {
                        if (ij1[0] == 4)
                        {
                            cinw[i] += G[0, ij1[1]];
                            cinw[i] += G[ij2[0] + 1, ij2[1]];
                        }
                        else
                        {
                            if (ij2[0] == 4)
                            {
                                cinw[i] += G[ij1[0] + 1, ij1[1]];
                                cinw[i] += G[0, ij1[1]];
                            }
                            else
                            {
                                cinw[i] += G[ij1[0] + 1, ij1[1]];
                                cinw[i] += G[ij2[0] + 1, ij2[1]];
                            }
                        }
                    }
                    if (buf[j] == buf[j + 1])
                    {
                        cinw[i] += buf[j];
                        cinw[i] += buf[j + 1];
                    }
                    j += 2;
                }
            }
            return cinw;
        }

        public string[] Decode(string[] inw, string[,] G)//декодирование
        {
            string[] cinw = new string[inw.Length];
            string[] coutw = new string[inw.Length];
            string buf = "";
            int[] ij1 = new int[2];
            int[] ij2 = new int[2];

            for (int i = 0; i < inw.Length; i++)
            {
                buf = "";
                int j = 0;
                if (inw[i].Length % 2 == 0)
                {
                    buf = inw[i];
                }

                while (j < buf.Length)
                {
                    ij1 = IJCord(buf[j].ToString(), G);
                    ij2 = IJCord(buf[j + 1].ToString(), G);
                   
                    if (ij1[0] < ij2[0] && ij1[1] != ij2[1])
                    {
                        cinw[i] += G[ij2[0], ij1[1]];
                        cinw[i] += G[ij1[0], ij2[1]];
                    }
                    
                    if (ij1[0] > ij2[0] && ij1[1] != ij2[1])
                    {
                        cinw[i] += G[ij2[0], ij1[1]];
                        cinw[i] += G[ij1[0], ij2[1]];
                    }
                    if (ij1[0] == ij2[0] && ij1[1] != ij2[1])
                    {
                        if (ij1[1] == 0)
                        {
                            cinw[i] += G[ij1[0], 4];
                            cinw[i] += G[ij2[0], ij2[1] - 1];
                        }
                        else
                        {
                            if (ij2[1] == 0)
                            {
                                cinw[i] += G[ij1[0], ij1[1] - 1];
                                cinw[i] += G[ij1[0], 4];
                            }
                            else
                            {
                                cinw[i] += G[ij1[0], ij1[1] - 1];
                                cinw[i] += G[ij2[0], ij2[1] - 1];
                            }
                        }
                    }
                    if (ij1[1] == ij2[1] && ij1[0] != ij2[0])
                    {
                        if (ij1[0] == 0)
                        {
                            cinw[i] += G[4, ij1[1]];
                            cinw[i] += G[ij2[0] - 1, ij2[1]];
                        }
                        else
                        {
                            if (ij2[0] == 0)
                            {
                                cinw[i] += G[ij1[0] - 1, ij1[1]];
                                cinw[i] += G[4, ij1[1]];
                            }
                            else
                            {
                                cinw[i] += G[ij1[0] - 1, ij1[1]];
                                cinw[i] += G[ij2[0] - 1, ij2[1]];
                            }
                        }
                    }
                    if (buf[j] == buf[j + 1])
                    {
                        cinw[i] += buf[j];
                        cinw[i] += buf[j + 1];
                    }
                    j += 2;
                }
            }
            
            for(int i = 0; i < cinw.Length; i++)
            {
                buf = "";
                if (cinw[i][cinw[i].Length - 1].ToString() == "X")
                {
                    for (int j = 0; j < cinw[i].Length - 1; j++)
                        buf += cinw[i][j].ToString();
                }
                else
                {
                    for (int j = 0; j < cinw[i].Length; j++)
                        buf += cinw[i][j].ToString();
                }
                coutw[i] = buf;
            }
            return coutw;
        }
    }
}
