﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Projekt_2_Grafika_Podejscie_2
{
    public partial class Form1 : Form
    {
        Scena scena;

        public Form1()
        {
            InitializeComponent();

            /*
            // Dane inicjalizujące
            Figura figura = new Figura();

            // Przód
            figura.dodajTrojkat(new Trojkat(new Punkt(0, 0, 0), new Punkt(0, 1, 0), new Punkt(1, 1, 0)));
            figura.dodajTrojkat(new Trojkat(new Punkt(0, 0, 0), new Punkt(1, 1, 0), new Punkt(1, 0, 0)));

            // Tył
            figura.dodajTrojkat(new Trojkat(new Punkt(1, 0, 1), new Punkt(1, 1, 1), new Punkt(0, 1, 1)));
            figura.dodajTrojkat(new Trojkat(new Punkt(1, 0, 1), new Punkt(0, 1, 1), new Punkt(0, 0, 1)));

            // Prawa
            figura.dodajTrojkat(new Trojkat(new Punkt(1, 0, 0), new Punkt(1, 1, 0), new Punkt(1, 1, 1)));
            figura.dodajTrojkat(new Trojkat(new Punkt(1, 0, 0), new Punkt(1, 1, 1), new Punkt(1, 0, 1)));

            // Lewa
            figura.dodajTrojkat(new Trojkat(new Punkt(0, 0, 1), new Punkt(0, 1, 1), new Punkt(0, 1, 0)));
            figura.dodajTrojkat(new Trojkat(new Punkt(0, 0, 1), new Punkt(0, 1, 0), new Punkt(0, 0, 0)));

            // Góra
            figura.dodajTrojkat(new Trojkat(new Punkt(0, 1, 0), new Punkt(0, 1, 1), new Punkt(1, 1, 1)));
            figura.dodajTrojkat(new Trojkat(new Punkt(0, 1, 0), new Punkt(1, 1, 1), new Punkt(1, 1, 0)));

            // Dół
            figura.dodajTrojkat(new Trojkat(new Punkt(1, 0, 1), new Punkt(0, 0, 1), new Punkt(0, 0, 0)));
            figura.dodajTrojkat(new Trojkat(new Punkt(1, 0, 1), new Punkt(0, 0, 0), new Punkt(1, 0, 0)));
            */

            this.scena = new Scena(900, 700, this.pictureBox1);
            scena.dodajFigure(Figura.generujProstopadloscian(new Punkt(-0.5, 0.5, -1.0),1,0.3,2));
            scena.dodajFigure(Figura.generujProstopadloscian(new Punkt(-0.3, 0.2, -0.3), 0.6, 0.3, 0.6));
            scena.dodajFigure(Figura.generujProstopadloscian(new Punkt(-0.05, 0.3, -0.6), 0.1, 0.1, 0.4));

            //this.label2.Text = this.scena.oswietlenie[0] + "; " + this.scena.oswietlenie[1] + "; " + this.scena.oswietlenie[2];
            //this.scena.dodajFigure(Figura.generujProstopadloscian(new Punkt(0.5, 0.5, -1.75),1,1,1));
            //this.scena.dodajFigure(Figura.generujProstopadloscian(new Punkt(0.5, 0.5, -0.5), 1, 1, 1));
            //this.scena.dodajFigure(Figura.generujProstopadloscian(new Punkt(0.5, 0.5, 1.25), 1, 1, 1));
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //this.scena.Przesun(0.0, 0.0, 2.0);
            //this.scena.Obroc(5.0);     
            //this.scena.Przesun(0.0, 0.0,2.0);
            this.scena.ObrocY(-10.0);
            //this.scena.ObrocX(30.0);
            //this.scena.ObrocX(10.0);
            //this.scena.Obroc(5.0, 90.0, 0.0);
            //this.scena.Skaluj(2.0,2.0,2.0);
            this.scena.Renderuj();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            //this.scena.Przesun(0.0, 0.0, -2.0);
            //this.scena.ObrocX(2.0);
            //this.scena.ObrocY(1.5);
            //this.scena.Przesun(0.0, 0.0, -2.0);
            //this.scena.Obroc(3.0, 2.0, 1.0);
            //this.scena.ObrocZ(1.5);
            //this.scena.ObrocX(2.0);
            //this.scena.Przesun(0.0, 0.0,2.0);

            //this.scena.Renderuj();
        }

        private void Form1_MouseWheel(object sender, MouseEventArgs e)
        {
            if (e.Delta > 0){
                this.scena.Skaluj(1.1, 1.1, 1.1);
                this.scena.Renderuj();
            }
            else {
                this.scena.Skaluj(0.91, 0.91, 0.91);
                this.scena.Renderuj();
            }
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode) {
                case Keys.Left:
                    this.scena.Przesun(0.1, 0.0, 0.0);
                    this.scena.Renderuj();
                    break;
                case Keys.Right:
                    this.scena.Przesun(-0.1, 0.0, 0.0);
                    this.scena.Renderuj();
                    break;
                case Keys.Up:
                    this.scena.Przesun(0.0, 0.0, -0.1);
                    this.scena.Renderuj();
                    break;
                case Keys.Down:
                    this.scena.Przesun(0.0, 0.0, 0.1);
                    this.scena.Renderuj();
                    break;
                case Keys.W:
                    this.scena.ObrocX(5.0);
                    this.scena.Renderuj();
                    break;
                case Keys.S:
                    this.scena.ObrocX(-5.0);
                    this.scena.Renderuj();
                    break;
                case Keys.A:
                    this.scena.ObrocY(5.0);
                    this.scena.Renderuj();
                    break;
                case Keys.D:
                    this.scena.ObrocY(-5.0);
                    this.scena.Renderuj();
                    break;
                case Keys.Q:
                    this.scena.ObrocZ(5.0);
                    this.scena.Renderuj();
                    break;
                case Keys.E:
                    this.scena.ObrocZ(-5.0);
                    this.scena.Renderuj();
                    break;
            }
        }
    }
}
