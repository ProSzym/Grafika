using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekt_2_Grafika_Podejscie_2
{
    public static class Macierze
    {
        // Zamienia stopnie na radiany
        public static double Radiany(double katStopnie) {
            return (katStopnie / 180.00) * Math.PI;
        }

        public static double[,] MnozMacierze(double[,] Macierz1, double[,] Macierz2)
        {
            int skladoweMacierz1Wiersze = Macierz1.GetLength(0);
            int skladoweMacierz1Kolumny = Macierz1.GetLength(1);

            int skladoweMacierz2Wiersze = Macierz2.GetLength(0);
            int skladoweMacierz2Kolumny = Macierz2.GetLength(1);

            if (skladoweMacierz1Kolumny != skladoweMacierz2Wiersze)
            {
                throw new IndexOutOfRangeException("Liczba kolumn 1-szej macierzy musi się równać ilości wierszy 2-giej macierzy");
            }
            else
            {
                double[,] nowaMacierz = new double[skladoweMacierz1Wiersze, skladoweMacierz2Kolumny];

                for (int i = 0; i < skladoweMacierz1Wiersze; i++)
                {
                    for (int j = 0; j < skladoweMacierz2Kolumny; j++)
                    {
                        for (int k = 0; k < skladoweMacierz1Kolumny; k++)
                        {
                            nowaMacierz[i, j] += (Macierz1[i, k] * Macierz2[k, j]);
                        }
                    }
                }
                return nowaMacierz;
            }
        }

        public static double[,] NormalizujWektor(double[,] Wektor)
        {
            if (Wektor[0, 3] != 0)
            {
                Wektor[0, 0] /= Wektor[0, 3];
                Wektor[0, 1] /= Wektor[0, 3];
                Wektor[0, 2] /= Wektor[0, 3];
            }
            return Wektor;    
        }

        public static double[,] MnozWektorMacierzNormalizacja(double[,] Wektor, double[,] Macierz)
        {
            int skladoweMacierz1Wiersze = Wektor.GetLength(0);
            int skladoweMacierz1Kolumny = Wektor.GetLength(1);

            int skladoweMacierz2Wiersze = Macierz.GetLength(0);
            int skladoweMacierz2Kolumny = Macierz.GetLength(1);

            if (skladoweMacierz1Kolumny != skladoweMacierz2Wiersze)
            {
                throw new IndexOutOfRangeException("Liczba kolumn 1-szej macierzy musi się równać ilości wierszy 2-giej macierzy");
            }
            else
            {
                double[,] nowyWektor = new double[skladoweMacierz1Wiersze, skladoweMacierz2Kolumny];

                for (int i = 0; i < skladoweMacierz1Wiersze; i++)
                {
                    for (int j = 0; j < skladoweMacierz2Kolumny; j++)
                    {
                        for (int k = 0; k < skladoweMacierz1Kolumny; k++)
                        {
                            nowyWektor[i, j] += (Wektor[i, k] * Macierz[k, j]);
                        }
                    }
                }
                return NormalizujWektor(nowyWektor);
            }
        }

        public static double[,] getMacierzPrzesuniecia(double x, double y, double z)
        {
            return new double[,] {
                { 1.0, 0.0, 0.0, 0.0},
                { 0.0, 1.0, 0.0, 0.0},
                { 0.0, 0.0, 1.0, 0.0},
                { x, y, z, 1.0}
            };
        }

        public static double[,] getMacierzObrotuX(double katObrotu)
        {
            katObrotu = Radiany(katObrotu);
            return new double[,] {
                { 1.0, 0.0, 0.0, 0.0},
                { 0.0, Math.Cos(katObrotu), Math.Sin(katObrotu), 0.0},
                { 0.0, -Math.Sin(katObrotu), Math.Cos(katObrotu), 0.0},
                { 0.0, 0.0, 0.0, 1.0}
            };
        }

        public static double[,] getMacierzObrotuY(double katObrotu)
        {
            katObrotu = Radiany(katObrotu);
            return new double[,] {
                { Math.Cos(katObrotu), 0.0, -Math.Sin(katObrotu), 0.0},
                { 0.0, 1.0, 0.0, 0.0},
                { Math.Sin(katObrotu), 0.0, Math.Cos(katObrotu), 0.0},
                { 0.0, 0.0, 0.0, 1.0}
            };
        }

        public static double[,] getMacierzObrotuZ(double katObrotu)
        {
            katObrotu = Radiany(katObrotu);
            return new double[,] {
                { Math.Cos(katObrotu), Math.Sin(katObrotu), 0.0, 0.0},
                { -Math.Sin(katObrotu), Math.Cos(katObrotu), 0.0, 0.0},
                { 0.0, 0.0, 1.0, 0.0},
                { 0.0, 0.0, 0.0, 1.0}
            };
        }

        public static double[,] getMacierzSkalowania(double x, double y, double z) {
            return new double[,] {
                { x, 0.0, 0.0, 0.0},
                { 0.0, y, 0.0, 0.0},
                { 0.0, 0.0, z, 0.0},
                { 0.0, 0.0, 0.0, 1.0}
            };
        }

        public static double[,] getMacierzRzutowania(double proporcjeSceny, double poleWidzenia, double rzutnia1, double rzutnia2) {
            return new double[4, 4] {
                { proporcjeSceny * poleWidzenia, 0.0, 0.0, 0.0},
                { 0.0, poleWidzenia, 0.0, 0.0},
                { 0.0, 0.0, rzutnia2 / (rzutnia2 - rzutnia1), 1.0},
                { 0.0, 0.0, (-rzutnia2 * rzutnia1) / (rzutnia2 - rzutnia1), 0.0}
            };
        }
    }
}
