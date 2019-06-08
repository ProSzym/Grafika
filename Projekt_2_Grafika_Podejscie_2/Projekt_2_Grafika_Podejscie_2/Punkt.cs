using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Projekt_2_Grafika_Podejscie_2
{
    public class Punkt :IComparable<Punkt>
    {
        // stałe składowe, nie zmieniam ich
        protected int bazowyX, bazowyY, bazowyZ;

        // składowe po operacjach, to będę rzutował na ekran i w tym przechowywał zrzutowane wartości
        public double rzeczywistyX, rzeczywistyY, rzeczywistyZ, 
            zrzutowanyX, zrzutowanyY, zrzutowanyZ;

        public Punkt(int x, int y, int z)
        {
            this.bazowyX = x;
            this.rzeczywistyX = this.bazowyX;

            this.bazowyY = y;
            this.rzeczywistyY = this.bazowyY;

            this.bazowyZ = z;
            this.rzeczywistyZ = this.bazowyZ;

            this.zrzutowanyX = 0.0;
            this.zrzutowanyY = 0.0;
            this.zrzutowanyZ = 0.0;
        }

        public Punkt(double x, double y, double z)
        {
            this.zrzutowanyX = x;
            this.zrzutowanyY = y;
            this.zrzutowanyZ = z;

            this.bazowyX = (int) x;
            this.rzeczywistyX = x;

            this.bazowyY = (int)y;
            this.rzeczywistyY = y;

            this.bazowyZ = (int)z;
            this.rzeczywistyZ = z;
        }

        public int getbazowyX
        {
            get
            {
                return this.bazowyX;
            }
        }

        public int getbazowyY
        {
            get
            {
                return this.bazowyY;
            }
        }

        public int getbazowyZ
        {
            get
            {
                return this.bazowyZ;
            }
        }

        public double[,] getWektor
        {
            get
            {
                return new double[,] {
                    { (double)this.getbazowyX, (double)this.getbazowyY , (double)this.getbazowyZ, 1.0 }
                };
            }
        }

        public double[,] getWektorRzeczywisty
        {
            get
            {
                return new double[,] {
                    { this.rzeczywistyX, this.rzeczywistyY , this.rzeczywistyZ, 1.0 }
                };
            }
        }

        public void PrzesunPunkt(double[,] macierzPrzesuniecia)
        {
            double[,] wynik = Macierze.MnozWektorMacierzNormalizacja(this.getWektorRzeczywisty, macierzPrzesuniecia);

            if (wynik.GetLength(0) != 1 || wynik.GetLength(1) != 4)
            {
                throw new Exception("Wektor po obróceniu ma złe wymiary 1 != " + wynik.GetLength(0) + " lub 4 != " + wynik.GetLength(1));
            }
            else
            {
                this.rzeczywistyX = wynik[0, 0];
                this.rzeczywistyY = wynik[0, 1];
                this.rzeczywistyZ = wynik[0, 2];
            }
        }

        public void ObrocPunkt(double[,] macierzObrotu)
        {
            double[,] wynik = Macierze.MnozWektorMacierzNormalizacja(this.getWektorRzeczywisty, macierzObrotu);

            if (wynik.GetLength(0) != 1 || wynik.GetLength(1) != 4)
            {
                throw new Exception("Wektor po obróceniu ma złe wymiary 1 != " + wynik.GetLength(0) + " lub 4 != " + wynik.GetLength(1));
            }
            else
            {             
                this.rzeczywistyX = wynik[0, 0];
                this.rzeczywistyY = wynik[0, 1];
                this.rzeczywistyZ = wynik[0, 2];             
            }
            //MessageBox.Show(this.ToString());
        }

        public void RzutujPunkt(double[,] macierzRzutowania)
        {
            this.rzeczywistyZ += 3;
            double[,] wynik = Macierze.MnozWektorMacierzNormalizacja(this.getWektorRzeczywisty, macierzRzutowania);
            this.rzeczywistyZ -= 3;

            if (wynik.GetLength(0) != 1 || wynik.GetLength(1) != 4)
            {
                throw new Exception("Wektor po rzutowaniu ma złe wymiary 1 != " + wynik.GetLength(0) + " lub 4 != " + wynik.GetLength(1));
            }
            else
            {
                this.zrzutowanyX = wynik[0, 0];
                this.zrzutowanyY = wynik[0, 1];
                this.zrzutowanyZ = wynik[0, 2];
            }
        }

        public void skalujPunkt(int szerokosc, int wysokosc)
        {
            this.zrzutowanyX = (int)((this.zrzutowanyX + 1.0) * 0.5 * (double)szerokosc);
            this.zrzutowanyY = (int)((this.zrzutowanyY + 1.0) * 0.5 * (double)wysokosc);
        }

        public override string ToString()
        {
            string toString = "Wartości bazowe: x = "+ getbazowyX + ", y = "+getbazowyY+", z = "+getbazowyZ+" \n";
            toString += "Wartości rzeczywiste: x = "+rzeczywistyX+", y = "+rzeczywistyY+", z = "+rzeczywistyZ+" \n";
            return toString;
        }

        public int CompareTo(Punkt other)
        {
            return this.zrzutowanyX.CompareTo(other.zrzutowanyY);
        }
    }
}
