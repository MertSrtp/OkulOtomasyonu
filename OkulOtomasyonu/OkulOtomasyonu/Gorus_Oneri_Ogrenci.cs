using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using Newtonsoft.Json;

namespace OkulOtomasyonu
{
    public partial class Gorus_Oneri_Ogrenci : Form
    {
        public Gorus_Oneri_Ogrenci()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            string metin = textBox1.Text.Trim();

            if (string.IsNullOrEmpty(metin))
            {
                MessageBox.Show("Lütfen bir görüş veya öneri girin.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var gorus = new GorusOneri
            {
                Metin = metin,
                Tarih = DateTime.Now
            };

            var mevcutGorusler = JsonHelper.Oku();
            mevcutGorusler.Add(gorus);
            JsonHelper.Yaz(mevcutGorusler);

            MessageBox.Show("Görüş kaydedildi!", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
            textBox1.Clear();
        }
    }
}
