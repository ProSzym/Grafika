﻿using System;
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

        public void ObrocXZ(double katObrotuX, double katObrotuZ) {
            double[,] macierzObrotu = Macierze.MnozMacierze(Macierze.getMacierzObrotuX(katObrotuX),Macierze.getMacierzObrotuZ(katObrotuZ));
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

        public void Renderuj()
        {
            this.bitmap = new Bitmap(this.bitmap.Width, this.bitmap.Height);
            this.pictureBox.Image = (Image)this.bitmap;
            foreach (Figura figura in figury)
            {
                figura.RysujFigure(this.pictureBox, this.bitmap, this.macierzRzutowania);
            }
        }
    }
}
