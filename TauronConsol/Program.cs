using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Text.RegularExpressions;

namespace TauronConsol
{
    class Program
    {
        static void Main(string[] args)
        {
            List<List<string>> punkty = new List<List<string>>();
            string[] linie = File.ReadAllLines(@"D:\plik.txt");
            for (int i = 0; i < linie.Length; ++i)
            {
                punkty.Add(new List<string>());
                foreach (string slowo in Regex.Split(linie[i], @"\s+"))
                {
                    punkty[i].Add(slowo);
                }
            }
            Console.WriteLine(punkty[0][0]);
            Console.WriteLine(punkty[0][2]);
            Console.WriteLine(punkty[0][3]);
            Console.WriteLine(punkty[0][1]);
            Console.WriteLine(punkty[1][0]);
            Console.WriteLine(punkty[1][2]);
            Console.WriteLine(punkty[1][3]);
            Console.WriteLine(punkty[1][1]); //przykładowe użycie

            FileStream fs = new FileStream("D:\\plik2.txt", FileMode.Create, FileAccess.Write);
            try
            {
                StreamWriter sw = new StreamWriter(fs);
                for (int i = 0; i < linie.Length; i++)
                {
                    sw.WriteLine(punkty[i][0]+" "+ punkty[i][0] + " " + punkty[i][0] + " " + punkty[i][0]);
                                    }
                sw.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            








            Console.ReadKey();
        }
    }
}
