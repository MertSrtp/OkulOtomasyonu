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
    public partial class Ogrenci_Sayfa : Form
    {
        public Ogrenci_Sayfa()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void simpleButton8_Click(object sender, EventArgs e)
        {
            using (Devamsizlik_Ogrenci devamsizlik_Ogrenci = new Devamsizlik_Ogrenci())
            {
                devamsizlik_Ogrenci.ShowDialog();
            }
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            using(Mesaj_Gonder_Ogrenci mesaj_Gonder_Ogrenci = new Mesaj_Gonder_Ogrenci())
            {
                mesaj_Gonder_Ogrenci.ShowDialog();
            }
        }

        private void simpleButton5_Click(object sender, EventArgs e)
        {
            using(Quiz_Ogrenci quiz_Ogrenci = new Quiz_Ogrenci())
            {
                quiz_Ogrenci.ShowDialog();
            }
        }

        private void simpleButton7_Click(object sender, EventArgs e)
        {
            using(Yemekhane_Ogrenci yemekhane_Ogrenci = new Yemekhane_Ogrenci())
            {
                yemekhane_Ogrenci.ShowDialog();
            }
        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            using(DersProgrami_Ogrenci dersProgrami_Ogrenci = new DersProgrami_Ogrenci())
            {
                dersProgrami_Ogrenci.ShowDialog();
            }
        }

        private void simpleButton6_Click(object sender, EventArgs e)
        {
            using(Gorus_Oneri_Ogrenci gorus_Oneri_Ogrenci = new Gorus_Oneri_Ogrenci())
            {
                gorus_Oneri_Ogrenci.ShowDialog();
            }
        }
    }
}
