using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Projekt_2_Grafika_Podejscie_2
{
    public static class Projekt1
    {
        public static void LiniaPrzyrostowa(PictureBox p, Bitmap b, int x0, int y0, int x1, int y1)
        {
            float dx, dy, m;

            // Zamiana zmiennych
            if (x0 > x1 && Math.Abs(x1 - x0) > Math.Abs(y1 - y0))
            {
                int tmp = x0;
                x0 = x1;
                x1 = tmp;

                tmp = y0;
                y0 = y1;
                y1 = tmp;
            }
            else if (y0 > y1 && Math.Abs(x1 - x0) < Math.Abs(y1 - y0))
            {
                int tmp = x0;
                x0 = x1;
                x1 = tmp;

                tmp = y0;
                y0 = y1;
                y1 = tmp;
            }
            dy = y1 - y0;
            dx = x1 - x0;

            // Rysowanie po X
            if (dy < dx)
            {

                m = dy / dx;
                float y = y0;
                for (int x = x0; x <= x1; x++)
                {
                    b.SetPixel(x, (int)Math.Floor(y + 0.5), Color.Black);
                    y += m;
                }
            }
            // Rysowanie po Y
            else
            {
                m = dx / dy;
                float x = x0;
                for (int y = y0; y <= y1; y++)
                {
                    b.SetPixel((int)Math.Floor(x + 0.5), y, Color.Black);
                    x += m;
                }
            }

            // Wyświetl
            p.Image = (Image)b;
        }

        public static void LiniaSrodek(PictureBox p, Bitmap b, int x0, int y0, int x1, int y1)
        {
            // zmienne pomocnicze
            int d, dx, dy, ai, bi, xi, yi;
            int x = x0, y = y0;
            // ustalenie kierunku rysowania
            if (x0 < x1)
            {
                xi = 1;
                dx = x1 - x0;
            }
            else
            {
                xi = -1;
                dx = x0 - x1;
            }
            // ustalenie kierunku rysowania
            if (y0 < y1)
            {
                yi = 1;
                dy = y1 - y0;
            }
            else
            {
                yi = -1;
                dy = y0 - y1;
            }
            // Piksel początkowy
            if (x > 0 && y > 0 && x < b.Width && y < b.Height) b.SetPixel(x, y, Color.Blue);

            // Rysowanie po X
            if (dx > dy)
            {
                ai = (dy - dx) * 2;
                bi = dy * 2;
                d = bi - dx;

                while (x != x1)
                {
                    // test współczynnika
                    if (d >= 0)
                    {
                        x += xi;
                        y += yi;
                        d += ai;
                    }
                    else
                    {
                        d += bi;
                        x += xi;
                    }
                    if (x > 0 && y > 0 && x < b.Width && y < b.Height) b.SetPixel(x, y, Color.Blue);
                }
            }

            // Rysowanie po Y
            else
            {
                ai = (dx - dy) * 2;
                bi = dx * 2;
                d = bi - dy;

                while (y != y1)
                {
                    // test współczynnika
                    if (d >= 0)
                    {
                        x += xi;
                        y += yi;
                        d += ai;
                    }
                    else
                    {
                        d += bi;
                        y += yi;
                    }
                    if (x > 0 && y > 0 && x < b.Width && y < b.Height) b.SetPixel(x, y, Color.Blue);
                }
            }
            // Wyświetl
            p.Image = (Image)b;
        }

        public static void CzyscBitmape(Bitmap b, PictureBox p)
        {
            p.Image = (Image)b;
        }
    }
}
