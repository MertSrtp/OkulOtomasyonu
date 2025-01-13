using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OkulOtomasyonu
{
    public partial class AnaSayfa : Form
    {
        
        public AnaSayfa()
        {

            InitializeComponent();
        }


        private void button1_Click(object sender, EventArgs e)
        {
            this.Dispose();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (Form1.Giris_Tipi == "Müdür")
            {
                using (Ogrenci_Kayit ogr = new Ogrenci_Kayit())
                {
                    ogr.ShowDialog();

                }
            }
            else
            {
                MessageBox.Show("Erişim Engeli");
            }

        }

        private void button4_Click(object sender, EventArgs e)
        {
            using (Siniflar sin = new Siniflar())
            {
                sin.ShowDialog();

            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (Form1.Giris_Tipi == "Müdür")
            {
                using (Personel personel = new Personel())
                {

                    personel.ShowDialog();
                }
            }
            else
            {
                MessageBox.Show("Erişim Engeli");
            }
        }

        private void button17_Click(object sender, EventArgs e)
        {
            if (Form1.Giris_Tipi == "Müdür")
            {
                using (Masraflar masraflar = new Masraflar())
                {

                    masraflar.ShowDialog();
                }
            }
            else
            {
                MessageBox.Show("Erişim Engeli");
            }
        }

        private void button15_Click(object sender, EventArgs e)
        {
            using (Devamsizlik devamsizlik = new Devamsizlik())
            {
                devamsizlik.ShowDialog();

            }
        }

        private void button14_Click(object sender, EventArgs e)
        {
            if (Form1.Giris_Tipi == "Müdür")
            {
                using (Dersler dersler = new Dersler())
                {


                    dersler.ShowDialog();
                }
            }
            else
            {
                MessageBox.Show("Erişim Engeli");
            }
        }

        private void button16_Click(object sender, EventArgs e)
        {
            if (Form1.Giris_Tipi == "Müdür")
            {
                using (Kulupler kulupler = new Kulupler())
                {

                    kulupler.ShowDialog();
                }
            }
            else
            {
                MessageBox.Show("Erişim Engeli");
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (Form1.Giris_Tipi == "Müdür")
            {
                using (Gelirler gelirler = new Gelirler())
                {

                    gelirler.ShowDialog();
                }
            }
            else
            {
                MessageBox.Show("Erişim Engeli");
            }
        }

        private void button12_Click(object sender, EventArgs e)
        {
            using(Yemekhane yemekhane = new Yemekhane())
            {
                yemekhane.ShowDialog();
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            if (Form1.Giris_Tipi == "Müdür")
            {
                using (Ogretmen ogretmen = new Ogretmen())
                {

                    ogretmen.ShowDialog();
                }
            }
            else
            {
                MessageBox.Show("Erişim Engeli");
            }
        }

        private void button19_Click(object sender, EventArgs e)
        {
            using(Duyurular duyurular = new Duyurular())
            {
                duyurular.ShowDialog();
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            using (Sinavlar sinavlar = new Sinavlar())
            {
                sinavlar.ShowDialog();
            }
        }

        private void button10_Click(object sender, EventArgs e)
        {
            using(Quiz quiz = new Quiz())
            {
                quiz.ShowDialog();
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (Form1.Giris_Tipi == "Müdür")
            {
                using (Ders_Programi ders_Programi = new Ders_Programi())
                {

                    ders_Programi.ShowDialog();
                }
            }
            else
            {
                MessageBox.Show("Erişim Engeli");
            }
        }

        private void button11_Click(object sender, EventArgs e)
        {
            using(Gorus_Oneri gorus_Oneri = new Gorus_Oneri())
            {
                gorus_Oneri.ShowDialog();
            }
        }
    }
}
