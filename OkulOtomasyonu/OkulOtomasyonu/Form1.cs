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
    public partial class Form1 : Form
    {
        public Form1()
        {
            
            InitializeComponent();
        }

        public static string Giris_Tipi;
        
        private void button1_Click(object sender, EventArgs e)
        {

            string rol = comboBox1.SelectedItem?.ToString();
            string sifre = textBox1.Text;
            
            if (rol == "Öğretmen" || rol == "Müdür")
            {
                if (comboBox1.Text == "Öğretmen" && textBox1.Text == "Fırat Üniversitesi")
                {
                    using (AnaSayfa ana = new AnaSayfa())
                    {
                        Giris_Tipi = "Öğretmen";
                        ana.ShowDialog();

                    }
                }
                else if(rol == "Müdür" && textBox1.Text == "123456")
                {
                    using (AnaSayfa ana = new AnaSayfa())
                    {
                        Giris_Tipi = "Müdür";

                        ana.ShowDialog();

                    }
                }
                else
                {
                    MessageBox.Show("Hatalı Şifre Girdiniz");
                }
            }
            else if(rol == "Öğrenci")
            {
                if (textBox1.Text == "147258")
                {
                    using (Ogrenci_Sayfa ogrenci_Sayfa = new Ogrenci_Sayfa())
                    {
                        ogrenci_Sayfa.ShowDialog();
                    }
                }
                else
                {
                    MessageBox.Show("Hatalı Şifre Girdiniz");
                }
            }

        }

        private void checkButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkButton1.Checked)
            {
                textBox1.UseSystemPasswordChar = false;
            }
            else
            {
                textBox1.UseSystemPasswordChar = true;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            textBox1.UseSystemPasswordChar = true;
        }
    }
}
