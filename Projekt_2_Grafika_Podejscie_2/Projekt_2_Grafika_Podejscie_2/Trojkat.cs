using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Projekt_2_Grafika_Podejscie_2
{
    public class Trojkat
    {
        Punkt p1, p2, p3;

        public Punkt[] SortujPunktyPoY()
        {
            Punkt[] posortowane = new Punkt[] { p1, p2, p3 };
            Array.Sort(posortowane);
            return posortowane;
        }

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

        // Posortowane tmp1.y <= tmp2.y <= tmp3.y
        public void WypelnijOdDolu(PictureBox pictureBox, Bitmap bitmap, Punkt tmp1, Punkt tmp2, Punkt tmp3)
        {
            double invslope1 = (tmp2.zrzutowanyX - tmp1.zrzutowanyX) / (tmp2.zrzutowanyY - tmp1.zrzutowanyY);
            double invslope2 = (tmp3.zrzutowanyX - tmp1.zrzutowanyX) / (tmp3.zrzutowanyY - tmp1.zrzutowanyY);

            double curx1 = tmp1.zrzutowanyX;
            double curx2 = tmp1.zrzutowanyX;

            for (int scanlineY =(int) tmp1.zrzutowanyY; scanlineY <= (int) tmp2.zrzutowanyY; scanlineY++)
            {
                Projekt1.LiniaSrodek(pictureBox, bitmap, (int)curx1, scanlineY, (int)curx2, scanlineY);
                curx1 += invslope1;
                curx2 += invslope2;
            }
        }

        public void WypelnijOdGory(PictureBox pictureBox, Bitmap bitmap, Punkt tmp1, Punkt tmp2, Punkt tmp3)
        {
            double invslope1 = (tmp3.zrzutowanyX - tmp1.zrzutowanyX) / (tmp3.zrzutowanyY - tmp1.zrzutowanyY);
            double invslope2 = (tmp3.zrzutowanyX - tmp2.zrzutowanyX) / (tmp3.zrzutowanyY - tmp2.zrzutowanyY);

            double curx1 = tmp3.zrzutowanyX;
            double curx2 = tmp3.zrzutowanyX;

            for (int scanlineY = (int)tmp3.zrzutowanyY; scanlineY > (int)tmp1.zrzutowanyY; scanlineY--)
            {
                Projekt1.LiniaSrodek(pictureBox, bitmap, (int)curx1, scanlineY, (int)curx2, scanlineY);
                curx1 -= invslope1;
                curx2 -= invslope2;
            }
        }

        // http://www.sunshine2k.de/coding/java/TriangleRasterization/TriangleRasterization.html?fbclid=IwAR0ypsoe3L16vNtGcICtqOWIvbuk4nO3xJst6R2yPINn4aQ4tvLvv-2b3lI
        public void Wypelnij(PictureBox pictureBox, Bitmap bitMap) {
            Punkt[] posortowane = this.SortujPunktyPoY();
            Punkt tmp1 = posortowane[0];
            Punkt tmp2 = posortowane[1];
            Punkt tmp3 = posortowane[2];

            if (tmp2.zrzutowanyY == tmp3.zrzutowanyY)
            {
                WypelnijOdDolu(pictureBox, bitMap, tmp1, tmp2, tmp3);
            }
            else if (tmp1.zrzutowanyY == tmp2.zrzutowanyY)
            {
                WypelnijOdGory(pictureBox, bitMap, tmp1, tmp2, tmp3);
            }
            
            else {
                Punkt tmp4 = new Punkt(
                    (int)tmp1.zrzutowanyX + ((tmp2.zrzutowanyY - tmp1.zrzutowanyY)/(tmp3.zrzutowanyY - tmp1.zrzutowanyY)) * (tmp3.zrzutowanyX - tmp1.zrzutowanyX),
                    tmp2.zrzutowanyY, 
                    0.0
                    );
                WypelnijOdDolu(pictureBox, bitMap, tmp1, tmp2, tmp4);
                WypelnijOdGory(pictureBox, bitMap, tmp2, tmp4, tmp3);
            }
            
        }

        public void RysujTrojkat(PictureBox pictureBox, Bitmap bitMap, double[,] macierzRzutowania)
        {
            this.p1.RzutujPunkt(macierzRzutowania);
            this.p2.RzutujPunkt(macierzRzutowania);
            this.p3.RzutujPunkt(macierzRzutowania);

            p1.skalujPunkt(bitMap.Width, bitMap.Height);
            p2.skalujPunkt(bitMap.Width, bitMap.Height);
            p3.skalujPunkt(bitMap.Width, bitMap.Height);

            Projekt1.LiniaSrodek(pictureBox, bitMap, (int)p1.zrzutowanyX, (int)p1.zrzutowanyY, (int)p2.zrzutowanyX, (int)p2.zrzutowanyY);
            Projekt1.LiniaSrodek(pictureBox, bitMap, (int)p2.zrzutowanyX, (int)p2.zrzutowanyY, (int)p3.zrzutowanyX, (int)p3.zrzutowanyY);
            Projekt1.LiniaSrodek(pictureBox, bitMap, (int)p1.zrzutowanyX, (int)p1.zrzutowanyY, (int)p3.zrzutowanyX, (int)p3.zrzutowanyY);

            this.Wypelnij(pictureBox, bitMap);
        }
    }
}
