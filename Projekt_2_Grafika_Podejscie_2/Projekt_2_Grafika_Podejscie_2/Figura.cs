using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Projekt_2_Grafika_Podejscie_2
{
    class Figura
    {
        public List<Trojkat> trojkaty;

        public Figura()
        {
            this.trojkaty = new List<Trojkat>();
        }

        public static Figura generujProstopadloscian(Punkt lewyDolny, double szerX, double wysY, double dlugZ) {
            Figura figura = new Figura();
            double x = lewyDolny.rzeczywistyX;
            double y = lewyDolny.rzeczywistyY;
            double z = lewyDolny.rzeczywistyZ;

            double x1 = x + szerX;
            double y1 = y + wysY;
            double z1 = z + dlugZ;
            // Przód
            figura.dodajTrojkat(new Trojkat(new Punkt(x, y, z), new Punkt(x, y1, z), new Punkt(x1, y1, z)));
            figura.dodajTrojkat(new Trojkat(new Punkt(x, y, z), new Punkt(x1, y1, z), new Punkt(x1, y, z)));

            // Tył
            figura.dodajTrojkat(new Trojkat(new Punkt(x1, y, z1), new Punkt(x1, y1, z1), new Punkt(x, y1, z1)));
            figura.dodajTrojkat(new Trojkat(new Punkt(x1, y, z1), new Punkt(x, y1, z1), new Punkt(x, y, z1)));

            // Prawa
            figura.dodajTrojkat(new Trojkat(new Punkt(x1, y, z), new Punkt(x1, y1, z), new Punkt(x1, y1, z1)));
            figura.dodajTrojkat(new Trojkat(new Punkt(x1, y, z), new Punkt(x1, y1, z1), new Punkt(x1, y, z1)));

            // Lewa
            figura.dodajTrojkat(new Trojkat(new Punkt(x, y, z1), new Punkt(x, y1, z1), new Punkt(x, y1, z)));
            figura.dodajTrojkat(new Trojkat(new Punkt(x, y, z1), new Punkt(x, y1, z), new Punkt(x, y, z)));

            // Góra
            figura.dodajTrojkat(new Trojkat(new Punkt(x, y1, z), new Punkt(x, y1, z1), new Punkt(x1, y1, z1)));
            figura.dodajTrojkat(new Trojkat(new Punkt(x, y1, z), new Punkt(x1, y1, z1), new Punkt(x1, y1, z)));

            // Dół
            figura.dodajTrojkat(new Trojkat(new Punkt(x1, y, z1), new Punkt(x, y, z1), new Punkt(x, y, z)));
            figura.dodajTrojkat(new Trojkat(new Punkt(x1, y, z1), new Punkt(x, y, z), new Punkt(x1, y, z)));

            return figura;
        }

        public void dodajTrojkat(Trojkat trojkat)
        {
            this.trojkaty.Add(trojkat);
        }

        public void PrzesunFigure(double[,] macierzPrzesuniecia)
        {
            foreach (Trojkat trojkat in trojkaty)
            {
                trojkat.PrzesunTrojkat(macierzPrzesuniecia);
            }
        }

        public void ObrocFigure(double[,] macierzObrotu)
        {
            foreach (Trojkat trojkat in trojkaty)
            {
                trojkat.ObrocTrojkat(macierzObrotu);
            }
        }

        /*
        public void RysujFigure(PictureBox pictureBox, Bitmap bitMap, double[,] macierzRzutowania, double[] oswietlenieZnormalizowane)
        {
            foreach (Trojkat trojkat in trojkaty)
            {
                trojkat.RzutujTrojkat(pictureBox, bitMap, macierzRzutowania);
                double[] normalna = trojkat.LiczNormalna(); 
                if (normalna[0] * (trojkat.P1.rzeczywistyZX) +
                    normalna[1] * (trojkat.P1.rzeczywistyZY) +
                    normalna[2] * (trojkat.P1.rzeczywistyZZ) < 0)
                {
                    double oswietlenie = normalna[0] * oswietlenieZnormalizowane[0] + normalna[1] * oswietlenieZnormalizowane[1] + normalna[2] * oswietlenieZnormalizowane[2];
                    int[] kolor = new int[] { 0, 0, 55 + (int)(200 * oswietlenie) };
                    trojkat.RysujTrojkat(pictureBox, bitMap, macierzRzutowania, kolor);
                }
            }
        }
        */
    }
}
