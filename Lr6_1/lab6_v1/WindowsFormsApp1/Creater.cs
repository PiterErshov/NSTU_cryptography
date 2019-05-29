using System;
using System.Collections.Generic;
using System.IO;
using System.Diagnostics;

namespace WindowsFormsApp1
{
    class Creater
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

        public string[] Creat_seq(int N) //создание простого числа
        {
            string[] seq = new string[4];
            bool Result = false;
            bool isPrime = false;
            int num = -1;
            double t = 0;
            double result_chance = 0;
            double local_chance = 1;
            int k = 0;
            int i = 0;
            int MinNum = (int)Math.Pow(10, (N - 1));
            int MaxNum = (int)(Math.Pow(10, N) - 1);

            Stopwatch time = new Stopwatch();
            time.Start();
            
            Random rand = new Random();

            while (Result == false)
            {
                num = rand.Next(MinNum, MaxNum);
                isPrime = false;
                k = 0;
                result_chance = 0;
                local_chance = 1;
                while (isPrime == false && k < 3)
                {
                    int a = rand.Next(1, num);
                    t = Math.Pow(a, (num - 1) / 2) % num;

                    if (t != 1)
                        if (t - num != -1)
                            isPrime = true;

                    //if (t == 1 || t - num == -1)
                    //{
                        local_chance *= 0.5;
                        result_chance = 1 - local_chance;
                    //}

                    k++;
                }
                if (isPrime == false && k == 3)
                    Result = true;
                i++;
            }
            time.Stop();

            seq[0] = num.ToString();
            seq[1] = time.Elapsed.ToString();
            seq[2] = i.ToString();
            seq[3] = result_chance.ToString();
            return seq;
        }        
    }
}
