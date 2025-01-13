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
    public partial class Devamsizlik_Guncelle : Form
    {
        string connectionString = "Server=DESKTOP-I71FBSL\\SQLEXPRESS;Database=DB_Okul_Otomasyon;Trusted_Connection=True;";

        public Devamsizlik_Guncelle()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string query = "UPDATE Devamsizlik " +
                   "SET OzurluDevamsizlik = @OzurluDevamsizlik, OzursuzDevamsizlik = @OzursuzDevamsizlik, FaaliyetDevamsizlik = @FaaliyetDevamsizlik " +
                   "WHERE OgrenciNumara = @OgrenciNumara";

            string ogrenciNo = textBox1.Text.Trim();
            string ozurluD = textBox4.Text.Trim();
            string ozursuzD = textBox5.Text.Trim();
            string faaliyetD = textBox6.Text.Trim();

            if (string.IsNullOrEmpty(ogrenciNo) || string.IsNullOrEmpty(ozurluD) ||
                string.IsNullOrEmpty(ozursuzD) || string.IsNullOrEmpty(faaliyetD))
            {
                MessageBox.Show("Lütfen tüm alanları doldurunuz.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                using (SqlConnection baglanti = new SqlConnection(connectionString))
                {
                    baglanti.Open();
                    SqlCommand command = new SqlCommand(query, baglanti);
                    command.Parameters.AddWithValue("@OgrenciNumara", ogrenciNo);
                    command.Parameters.AddWithValue("@OzurluDevamsizlik", ozurluD);
                    command.Parameters.AddWithValue("@OzursuzDevamsizlik", ozursuzD);
                    command.Parameters.AddWithValue("@FaaliyetDevamsizlik", faaliyetD);

                    int rowsAffected = command.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Güncelleme başarılı!", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Güncelleme başarısız. Lütfen öğrenci numarasını kontrol edin.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Bir hata oluştu: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {

            string query = "SELECT o.Ad AS Isim, o.Soyad AS Soyisim, d.OzurluDevamsizlik, d.OzursuzDevamsizlik, d.FaaliyetDevamsizlik " +
                           "FROM Devamsizlik d " +
                           "JOIN Ogrenci o ON d.OgrenciNumara = o.OgrenciNumara " +
                           "WHERE d.OgrenciNumara = @OgrenciNumara";

            string numara = textBox1.Text.Trim();

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
                        Isim.Text = reader["Isim"].ToString();
                        Soyisim.Text = reader["Soyisim"].ToString();
                        textBox4.Text = reader["OzurluDevamsizlik"].ToString();
                        textBox5.Text = reader["OzursuzDevamsizlik"].ToString();
                        textBox6.Text = reader["FaaliyetDevamsizlik"].ToString();
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

        private void button4_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
            textBox4.Text = "";
            textBox5.Text = "";
            textBox6.Text = "";
            Isim.Text = "";
            Soyisim.Text = "";
        }
    }
}
