using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Projekt_2_Grafika_Podejscie_2;
using Projekt_2_Grafika_Podejscie_2;
namespace Test
{
    class Program
    {
        static void Main(string[] args)
        {
            double rzutnia1 = 0.1;
            double rzutnia2 = 1000.0;
            double katWidzenia = 90.0;
            double proporcjeSceny = 800.0 / 1000.0;
            double poleWidzenia = 1.0 / Math.Tan(Macierze.Radiany(0.5 * katWidzenia));

            Punkt p1 = new Punkt(0, 2, 0);
            Punkt p2 = new Punkt(0, 3, 0);
            Punkt p3 = new Punkt(10, 1, 0);

            Trojkat trojkat = new Trojkat(p1, p2, p3);
            Punkt[] posortowane= trojkat.SortujPunktyPoY();

            foreach (var item in posortowane)
            {
                Console.WriteLine(item.ToString());
            }
            //p1.PrzesunPunkt(Macierze.getMacierzPrzesuniecia(0.0,-10.0,10.0));
            //p1.ObrocPunkt(Macierze.getMacierzObrotuY(10));
            /*
            for (int i = 0; i < 10; i++)
            {
                p1.ObrocPunkt(Macierze.getMacierzObrotuX(10*i));
                p2.ObrocPunkt(Macierze.getMacierzObrotuX(10*i));
                Console.WriteLine(p1.ToString());
                Console.WriteLine(p2.ToString());
            }
            */
            //p1.ObrocPunkt(Macierze.getMacierzObrotuX(10));
            //p2.ObrocPunkt(Macierze.getMacierzObrotuX(10));
            //p1.RzutujPunkt(Macierze.getMacierzRzutowania(proporcjeSceny, poleWidzenia, rzutnia1, rzutnia2));
            //Console.WriteLine(p1.ToString());
            //Console.WriteLine(p2.ToString());
            /*
            double[,] wynik = Macierze.MnozMacierze(new double[,] { { 0,1,2 } }, new double[,] { { 0,1,2 }, { 1,2,3 }, { 2,3,4 }, } );
            wynik = Macierze.MnozWektorMacierzNormalizacja(p2.getWektor, Macierze.getMacierzObrotuX(20));
            for (int i = 0; i < wynik.GetLength(0); i++)
            {
                for (int j = 0; j < wynik.GetLength(1); j++)
                {
                    Console.Write(wynik[i,j]+"; ");
                }
                Console.WriteLine();
            }
            */
            Console.ReadKey();
        }
    }
}
