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
        List<Trojkat> trojkaty;

        public Figura()
        {
            this.trojkaty = new List<Trojkat>();
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
        public void RysujFigure(PictureBox pictureBox, Bitmap bitMap, double[,] macierzRzutowania)
        {
            foreach (Trojkat trojkat in trojkaty)
            {
                if (trojkat.LiczNormalna()[2] < 0)
                {
                    trojkat.RysujTrojkat(pictureBox, bitMap, macierzRzutowania);
                }
            }
        }
    }
}
