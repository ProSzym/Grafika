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
        //Te wartości przekształcam
        public double rzeczywistyX, rzeczywistyY, rzeczywistyZ;

        //Tu są zrzutowane wartości
        public double rzeczywistyZX, rzeczywistyZY, rzeczywistyZZ;

        //Tu są zeskalowanie i zamienione na int - wartości przetłumaczone na punkty na bitmapie
        public int zrzutowanyX, zrzutowanyY, zrzutowanyZ;

        public Punkt(double x, double y, double z)
        {
            this.rzeczywistyX = x;
            this.rzeczywistyZX = x;
            this.rzeczywistyY = y;
            this.rzeczywistyZX = y;
            this.rzeczywistyZ = z;
            this.rzeczywistyZX = z;
            this.zrzutowanyX = (int)x;
            this.zrzutowanyY = (int)y;
            this.zrzutowanyZ = (int)z;
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
                this.rzeczywistyZX = wynik[0, 0];
                this.rzeczywistyZY = wynik[0, 1];
                this.rzeczywistyZZ = wynik[0, 2];
            }
        }

        public void skalujPunkt(int szerokosc, int wysokosc)
        {
            this.zrzutowanyX = (int)((this.rzeczywistyZX + 1.0) * 0.5 * (double)szerokosc);
            this.zrzutowanyY = (int)((this.rzeczywistyZY + 1.0) * 0.5 * (double)wysokosc);
        }

        public override string ToString()
        {
            string toString = "Wartości rzeczywiste: x = " + rzeczywistyX + ", y = " + rzeczywistyY + ", z = " + rzeczywistyZ + " \n";
            toString += "Wartości zrzutowane: x = "+rzeczywistyZX+", y = "+rzeczywistyZY+", z = "+rzeczywistyZZ+" \n";
            toString += "Wartości zrzutowane w int: x = " + zrzutowanyX + ", y = " + zrzutowanyY + ", z = " + zrzutowanyZ + " \n";

            return toString;
        }

        public int CompareTo(Punkt other)
        {
            return this.zrzutowanyY.CompareTo(other.zrzutowanyY);
        }
    }
}
