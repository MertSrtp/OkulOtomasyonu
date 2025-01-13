using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace OkulOtomasyonu
{
    public partial class Devamsizlik_Ogrenci : Form
    {
        public Devamsizlik_Ogrenci()
        {
            InitializeComponent();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            string connectionString = "Server=DESKTOP-I71FBSL\\SQLEXPRESS;Database=DB_Okul_Otomasyon;Trusted_Connection=True;";


            string query = "SELECT o.Ad AS Isim, o.Soyad AS Soyisim, d.OzurluDevamsizlik, d.OzursuzDevamsizlik, d.FaaliyetDevamsizlik " +
                           "FROM Devamsizlik d " +
                           "JOIN Ogrenci o ON d.OgrenciNumara = o.OgrenciNumara " +
                           "WHERE d.OgrenciNumara = @OgrenciNumara";

            string numara = textEdit1.Text.Trim();

            if (string.IsNullOrEmpty(numara))
            {
                MessageBox.Show("Lütfen geçerli bir öğrenci numarası giriniz.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                using (SqlConnection baglanti = new SqlConnection(connectionString))
                {
                    baglanti.Open();
                    SqlCommand command = new SqlCommand(query, baglanti);
                    command.Parameters.AddWithValue("@OgrenciNumara", numara);

                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        labelControl7.Text = reader["Isim"].ToString();
                        labelControl8.Text = reader["Soyisim"].ToString();
                        labelControl9.Text = reader["OzurluDevamsizlik"].ToString();
                        labelControl10.Text = reader["OzursuzDevamsizlik"].ToString();
                        labelControl11.Text = reader["FaaliyetDevamsizlik"].ToString();
                    }
                    else
                    {
                        MessageBox.Show("Girilen öğrenci bulunamadı.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Bir hata oluştu: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            labelControl7.Text = "";
            labelControl8.Text = "";
            labelControl9.Text = "";
            labelControl10.Text = "";
            labelControl11.Text = "";
            textEdit1.Text = "";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
    }
}
