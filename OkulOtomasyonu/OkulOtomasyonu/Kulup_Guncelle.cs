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
    public partial class Kulup_Guncelle : Form
    {
        string connectionString = "Server=DESKTOP-I71FBSL\\SQLEXPRESS;Database=DB_Okul_Otomasyon;Trusted_Connection=True;";

        public Kulup_Guncelle()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            comboBox1.Text = "";
            textBox5.Text = "";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string query = "SELECT * FROM Kulupler WHERE KulupKodu = @KulupKodu";
            string dersKodu = textBox1.Text.Trim();

            if (string.IsNullOrEmpty(dersKodu))
            {
                MessageBox.Show("Lütfen geçerli bir Kulüp Kodu giriniz.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            SqlConnection baglanti = new SqlConnection(connectionString);
            baglanti.Open();
            SqlCommand command = new SqlCommand(query, baglanti);
            command.Parameters.AddWithValue("@KulupKodu", dersKodu);
            SqlDataReader reader = command.ExecuteReader();
            if (reader.Read())
            {
                textBox2.Text = reader["KulupAdi"].ToString();
                textBox3.Text = reader["KulupBaskani"].ToString();
                comboBox1.Text = reader["DanismanHoca"].ToString();
                textBox5.Text = reader["KulupTuru"].ToString();

            }
            else
            {
                MessageBox.Show("Girilen Kulüp Kodu bulunamadı.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            baglanti.Close();

        }

        private void button3_Click(object sender, EventArgs e)
        {
            string query = "UPDATE Kulupler " +
               "SET KulupAdi = @KulupAdi, KulupBaskani = @KulupBaskani, DanismanHoca = @DanismanHoca, KulupTuru = @KulupTuru " +
               "WHERE KulupKodu = @KulupKodu";

            string kulupKodu = textBox1.Text.Trim();
            string kulupAdi = textBox2.Text.Trim();
            string kulupBaskani = textBox3.Text.Trim();
            string danismanHoca = comboBox1.Text.Trim();
            string kulupTuru = textBox5.Text.Trim();


            if (string.IsNullOrEmpty(kulupKodu) || string.IsNullOrEmpty(kulupAdi) ||
                string.IsNullOrEmpty(kulupBaskani) || string.IsNullOrEmpty(danismanHoca) ||
                string.IsNullOrEmpty(kulupTuru))
            {
                MessageBox.Show("Lütfen tüm alanları doldurunuz.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            try
            {
                SqlConnection baglanti = new SqlConnection(connectionString);
                baglanti.Open();
                SqlCommand command = new SqlCommand(query, baglanti);
                command.Parameters.AddWithValue("@KulupKodu", kulupKodu);
                command.Parameters.AddWithValue("@KulupAdi", kulupAdi);
                command.Parameters.AddWithValue("@KulupBaskani", kulupBaskani);
                command.Parameters.AddWithValue("@DanismanHoca", danismanHoca);
                command.Parameters.AddWithValue("@KulupTuru", kulupTuru);

                int rowsAffected = command.ExecuteNonQuery();

                if (rowsAffected > 0)
                {
                    MessageBox.Show("Güncelleme başarılı!", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Ders güncellenemedi. Lütfen Ders Kodunu kontrol edin.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Bir hata oluştu: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ComboBoxDoldur()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {


                connection.Open();

                // Veritabanı sorgusu
                string query = "SELECT CONCAT(Ad, ' ', Soyad) AS AdSoyad FROM Personel WHERE Meslek = 'Öğretmen'";

                SqlCommand command = new SqlCommand(query, connection);

                // Verileri okumak için SqlDataReader kullanıyoruz
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        // ComboBox'a ekleme
                        comboBox1.Items.Add(reader["AdSoyad"].ToString());
                    }
                }

            }
        }
        private void Kulup_Guncelle_Load(object sender, EventArgs e)
        {
            ComboBoxDoldur();
        }
    }
}
