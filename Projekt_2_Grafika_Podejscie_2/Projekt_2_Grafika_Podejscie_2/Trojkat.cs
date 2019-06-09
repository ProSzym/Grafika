using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Projekt_2_Grafika_Podejscie_2
{
    public class Trojkat :IComparable<Trojkat>
    {
        Punkt p1, p2, p3;

        public Punkt[] SortujPunktyPoY()
        {
            Punkt[] posortowane = new Punkt[] { p1, p2, p3 };
            Array.Sort(posortowane);
            return posortowane;
        }

        public Punkt P1 { get { return this.p1; } set { this.p1 = value; } }
        public Punkt P2 { get { return this.p2; } set { this.p2 = value; } }
        public Punkt P3 { get { return this.p3; } set { this.p3 = value; } }

        public Trojkat(Punkt p1, Punkt p2, Punkt p3)
        {
            this.p1 = p1;
            this.p2 = p2;
            this.p3 = p3;
        }

        public void ObrocTrojkat(double[,] macierzObrotu)
        {
            p1.ObrocPunkt(macierzObrotu);
            p2.ObrocPunkt(macierzObrotu);
            p3.ObrocPunkt(macierzObrotu);
        }

        public void PrzesunTrojkat(double[,] macierzPrzesuniecia)
        {
            p1.PrzesunPunkt(macierzPrzesuniecia);
            p2.PrzesunPunkt(macierzPrzesuniecia);
            p3.PrzesunPunkt(macierzPrzesuniecia);
        }

        public double[] LiczNormalna() {
            double[] linia1 = new double[] { p2.rzeczywistyX - p1.rzeczywistyX, p2.rzeczywistyY - p1.rzeczywistyY, p2.rzeczywistyZ - p1.rzeczywistyZ };
            double[] linia2 = new double[] { p3.rzeczywistyX - p1.rzeczywistyX, p3.rzeczywistyY - p1.rzeczywistyY, p3.rzeczywistyZ - p1.rzeczywistyZ };

            double[] normalna = new double[] {
                linia1[1] * linia2[2] - linia1[2] * linia2[1],
                linia1[2] * linia2[0] - linia1[0] * linia2[2],
                linia1[0] * linia2[1] - linia1[1] * linia2[0]
            };

            double dlugosc = Math.Sqrt(Math.Pow(normalna[0], 2) + Math.Pow(normalna[1], 2) + Math.Pow(normalna[2], 2));
            normalna[0] /= dlugosc;
            normalna[1] /= dlugosc;
            normalna[2] /= dlugosc;

            return normalna;
        }

        // Wypełnianie trójkąta
        // Posortowane tmp1.y <= tmp2.y <= tmp3.y
        public void WypelnijOdDolu(PictureBox pictureBox, Bitmap bitmap, Punkt tmp1, Punkt tmp2, Punkt tmp3, Color kolor)
        {
            double invslope1 = (double)((double)(tmp2.zrzutowanyX - tmp1.zrzutowanyX) / (double)(tmp2.zrzutowanyY - tmp1.zrzutowanyY));
            double invslope2 = (double)((double)(tmp3.zrzutowanyX - tmp1.zrzutowanyX) / (double)(tmp3.zrzutowanyY - tmp1.zrzutowanyY));

            double curx1 = tmp1.zrzutowanyX;
            double curx2 = tmp1.zrzutowanyX;

            for (int scanlineY = tmp1.zrzutowanyY; scanlineY <= tmp2.zrzutowanyY; scanlineY++)
            {
                Projekt1.LiniaSrodek(pictureBox, bitmap, (int)curx1, scanlineY, (int)curx2, scanlineY, kolor);
                curx1 += invslope1;
                curx2 += invslope2;
            }
        }

        public void WypelnijOdGory(PictureBox pictureBox, Bitmap bitmap, Punkt tmp1, Punkt tmp2, Punkt tmp3, Color kolor)
        {
            double invslope1 = (double)((double)(tmp3.zrzutowanyX - tmp1.zrzutowanyX) / (double)(tmp3.zrzutowanyY - tmp1.zrzutowanyY));
            double invslope2 = (double)((double)(tmp3.zrzutowanyX - tmp2.zrzutowanyX) / (double)(tmp3.zrzutowanyY - tmp2.zrzutowanyY));

            double curx1 = (double)tmp3.zrzutowanyX;
            double curx2 = (double)tmp3.zrzutowanyX;

            for (int scanlineY = tmp3.zrzutowanyY; scanlineY > tmp1.zrzutowanyY; scanlineY--)
            {
                Projekt1.LiniaSrodek(pictureBox, bitmap, (int)curx1, scanlineY, (int)curx2, scanlineY, kolor);
                curx1 -= invslope1;
                curx2 -= invslope2;
            }
        }

        public void Wypelnij(PictureBox pictureBox, Bitmap bitMap, int[] kolor) {
            Punkt[] posortowane = this.SortujPunktyPoY();
            Punkt Tmp1 = posortowane[0];
            Punkt Tmp2 = posortowane[1];
            Punkt Tmp3 = posortowane[2];
            Color kolorFromRgb = Color.FromArgb(kolor[0], kolor[1], kolor[2]); ;
            if (Tmp2.zrzutowanyY == Tmp3.zrzutowanyY)
            {
                WypelnijOdDolu(pictureBox, bitMap, Tmp1, Tmp2, Tmp3, kolorFromRgb);
            }
            else if (Tmp1.zrzutowanyY == Tmp2.zrzutowanyY)
            {
                WypelnijOdGory(pictureBox, bitMap, Tmp1, Tmp2, Tmp3, kolorFromRgb);
            }
            else {
                Punkt Tmp4 = new Punkt(
                        (int)((double)Tmp1.zrzutowanyX + ((double)(Tmp2.zrzutowanyY - Tmp1.zrzutowanyY) / (double)(Tmp3.zrzutowanyY - Tmp1.zrzutowanyY)) * (double)(Tmp3.zrzutowanyX - Tmp1.zrzutowanyX)),
                        Tmp2.zrzutowanyY, 
                        0
                    );
                //Projekt1.LiniaSrodek(pictureBox, bitMap, Tmp2.zrzutowanyX, Tmp2.zrzutowanyY, Tmp4.zrzutowanyX, Tmp4.zrzutowanyY, kolorFromRgb);
                WypelnijOdDolu(pictureBox, bitMap, Tmp1, Tmp2, Tmp4, kolorFromRgb);
                WypelnijOdGory(pictureBox, bitMap, Tmp2, Tmp4, Tmp3, kolorFromRgb);
            }
        }

        public void RzutujTrojkat(PictureBox pictureBox, Bitmap bitMap, double[,] macierzRzutowania) {
            this.p1.RzutujPunkt(macierzRzutowania);
            this.p2.RzutujPunkt(macierzRzutowania);
            this.p3.RzutujPunkt(macierzRzutowania);

            p1.skalujPunkt(bitMap.Width, bitMap.Height);
            p2.skalujPunkt(bitMap.Width, bitMap.Height);
            p3.skalujPunkt(bitMap.Width, bitMap.Height);
        }

        public void RysujTrojkat(PictureBox pictureBox, Bitmap bitMap, double[,] macierzRzutowania, int[] kolor)
        {
            //Projekt1.LiniaSrodek(pictureBox, bitMap, p1.zrzutowanyX, p1.zrzutowanyY, p2.zrzutowanyX, p2.zrzutowanyY);
            //Projekt1.LiniaSrodek(pictureBox, bitMap, p2.zrzutowanyX, p2.zrzutowanyY, p3.zrzutowanyX, p3.zrzutowanyY);
            //Projekt1.LiniaSrodek(pictureBox, bitMap, p1.zrzutowanyX, p1.zrzutowanyY, p3.zrzutowanyX, p3.zrzutowanyY);
            this.Wypelnij(pictureBox, bitMap, kolor);
        }

        public double maxZ() {
            double maxZ = this.P1.rzeczywistyZ;
            if (this.P2.rzeczywistyZZ > maxZ) maxZ = this.P2.rzeczywistyZZ;
            if (this.P3.rzeczywistyZZ > maxZ) maxZ = this.P3.rzeczywistyZZ;
            return maxZ;
        }

        public int CompareTo(Trojkat other)
        {
            return this.maxZ().CompareTo(other.maxZ());
        }
    }
}
