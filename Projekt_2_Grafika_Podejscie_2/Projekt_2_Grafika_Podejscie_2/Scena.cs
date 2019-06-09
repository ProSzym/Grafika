using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Projekt_2_Grafika_Podejscie_2
{
    class Scena
    {
        public PictureBox pictureBox;
        public Bitmap bitmap;
        public List<Figura> figury;
        public double[,] macierzRzutowania;
        public double [] oswietlenie;

        public Scena(int szerokosc, int wyskokosc, PictureBox pictureBox)
        {
            double rzutnia1 = 0.1;
            double rzutnia2 = 1000.0;
            double katWidzenia = 90.0;
            double proporcjeSceny = (double)wyskokosc / (double)szerokosc;
            double poleWidzenia = 1.0 / Math.Tan(0.5 * katWidzenia / 180.0 * Math.PI);

            this.pictureBox = pictureBox;
            this.bitmap = new Bitmap(szerokosc, wyskokosc);
            this.figury = new List<Figura>();

            this.macierzRzutowania = Macierze.getMacierzRzutowania((double)wyskokosc / (double)szerokosc,
                1.0 / Math.Tan(Macierze.Radiany(0.5 * katWidzenia)), rzutnia1, rzutnia2);

            //this.oswietlenie = new double[] { 0, 0.0, -5.0};
            this.oswietlenie = new double[] { 10.0, 0.0, 5.0 };
        }

        public void dodajFigure(Figura figura)
        {
            this.figury.Add(figura);
        }

        public void ObrocX(double katObrotu)
        {
            double[,] macierzObrotu = Macierze.getMacierzObrotuX(katObrotu);
            foreach (Figura figura in figury)
            {
                figura.ObrocFigure(macierzObrotu);
            }
        }

        public void ObrocY(double katObrotu)
        {
            double[,] macierzObrotu = Macierze.getMacierzObrotuY(katObrotu);
            foreach (Figura figura in figury)
            {
                figura.ObrocFigure(macierzObrotu);
            }
        }

        public void ObrocZ(double katObrotu)
        {
            double[,] macierzObrotu = Macierze.getMacierzObrotuZ(katObrotu);
            foreach (Figura figura in figury)
            {
                figura.ObrocFigure(macierzObrotu);
            }
        }

        public void Obroc(double katObrotuX, double katObrotuY, double katObrotuZ) {
            double[,] macierzObrotu = Macierze.MnozMacierze(Macierze.getMacierzObrotuX(katObrotuX), Macierze.getMacierzObrotuY(katObrotuY));
            macierzObrotu = Macierze.MnozMacierze(Macierze.getMacierzObrotuZ(katObrotuZ), macierzObrotu);
            foreach (Figura figura in figury)
            {
                figura.ObrocFigure(macierzObrotu);
            }
        }

        public void Przesun(double x, double y, double z)
        {
            double[,] macierzPrzesuniecia = Macierze.getMacierzPrzesuniecia(x, y, z);
            foreach (Figura figura in figury)
            {
                figura.PrzesunFigure(macierzPrzesuniecia);
            }
        }

        public void Skaluj(double x, double y, double z) {
            double[,] macierzSkalowania = Macierze.getMacierzSkalowania(x, y, z);
            foreach (Figura figura in figury)
            {
                figura.PrzesunFigure(macierzSkalowania);
            }
        }

        // Zmienione, ma figurach nie działał malarz
        public void Renderuj()
        {
            List <Trojkat> doWyswietlenia = new List<Trojkat>(); 
            this.bitmap = new Bitmap(this.bitmap.Width, this.bitmap.Height);
            this.pictureBox.Image = (Image)this.bitmap;
            foreach (Figura figura in figury)
            {
                foreach (Trojkat trojkat in figura.trojkaty)
                {
                    trojkat.RzutujTrojkat(pictureBox, bitmap, macierzRzutowania);
                    double[] normalna = trojkat.LiczNormalna();
                    if (normalna[0] * (trojkat.P1.rzeczywistyZX) +
                        normalna[1] * (trojkat.P1.rzeczywistyZY) +
                        normalna[2] * (trojkat.P1.rzeczywistyZZ) < 0)
                    {
                        doWyswietlenia.Add(trojkat);
                    }
                }
                //figura.RysujFigure(this.pictureBox, this.bitmap, this.macierzRzutowania, this.oswietlenie);
            }
            doWyswietlenia.Sort();
            doWyswietlenia.Reverse();
            double[] oswietlenieZnormalizowane = new double[3];
            double dlugosc = Math.Sqrt(Math.Pow(this.oswietlenie[0],2) + Math.Pow(this.oswietlenie[1],2) + Math.Pow(this.oswietlenie[2],2));
            oswietlenieZnormalizowane[0] = (oswietlenie[0] / dlugosc);
            oswietlenieZnormalizowane[1] = (oswietlenie[1] / dlugosc);
            oswietlenieZnormalizowane[2] = (oswietlenie[2] / dlugosc);

            foreach (Trojkat trojkat in doWyswietlenia)
            {
                //MessageBox.Show(""+trojkat.maxZ());
                double[] normalna = trojkat.LiczNormalna();
                double oswietlenie = normalna[0] * oswietlenieZnormalizowane[0] + normalna[1] * oswietlenieZnormalizowane[1] + normalna[2] * oswietlenieZnormalizowane[2];
                int[] kolor = new int[] { 0, 0, 150 + (int)(105 * oswietlenie) };
                trojkat.RysujTrojkat(pictureBox, bitmap, macierzRzutowania, kolor);
            }
        }
    }
}
