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
    public partial class Ders_Guncelle : Form
    {
        string connectionString = "Server=DESKTOP-I71FBSL\\SQLEXPRESS;Database=DB_Okul_Otomasyon;Trusted_Connection=True;";



        public Ders_Guncelle()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string query = "SELECT * FROM Ders WHERE DersKodu = @DersKodu";
            string dersKodu = textBox1.Text.Trim();

            if (string.IsNullOrEmpty(dersKodu))
            {
                MessageBox.Show("Lütfen geçerli bir Ders Kodu giriniz.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            SqlConnection baglanti = new SqlConnection(connectionString);
            baglanti.Open();
            SqlCommand command = new SqlCommand(query, baglanti);
            command.Parameters.AddWithValue("@DersKodu", dersKodu);
            SqlDataReader reader = command.ExecuteReader();
            if (reader.Read())
            {
                textBox2.Text = reader["DersAdi"].ToString();
                textBox3.Text = reader["Derslikler"].ToString();
                comboBox1.SelectedItem = reader["OgretimGorevlisi"].ToString();
                textBox4.Text = reader["HaftalikDersSaati"].ToString();

            }
            else
            {
                MessageBox.Show("Girilen Ders Kodu bulunamadı.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            baglanti.Close();

        }

        private void button3_Click(object sender, EventArgs e)
        {
            string query = "UPDATE Ders " +
                           "SET DersAdi = @DersAdi, Derslikler = @Derslikler, OgretimGorevlisi = @OgretimGorevlisi, HaftalikDersSaati = @HaftalikDersSaati " +
                           "WHERE DersKodu = @DersKodu";

            string dersKodu = textBox1.Text.Trim();
            string dersAdi = textBox2.Text.Trim();
            string derslik = textBox3.Text.Trim();
            string ogretimGorevlisi = comboBox1.Text.Trim();
            string haftalikDersSaati = textBox4.Text.Trim();


            if (string.IsNullOrEmpty(dersKodu) || string.IsNullOrEmpty(dersAdi) ||
                string.IsNullOrEmpty(derslik) || string.IsNullOrEmpty(ogretimGorevlisi) ||
                string.IsNullOrEmpty(haftalikDersSaati))
            {
                MessageBox.Show("Lütfen tüm alanları doldurunuz.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            try
            {
                SqlConnection baglanti = new SqlConnection(connectionString);
                baglanti.Open();
                SqlCommand command = new SqlCommand(query, baglanti);
                command.Parameters.AddWithValue("@DersKodu", dersKodu);
                command.Parameters.AddWithValue("@DersAdi", dersAdi);
                command.Parameters.AddWithValue("@Derslikler", derslik);
                command.Parameters.AddWithValue("@OgretimGorevlisi", ogretimGorevlisi);
                command.Parameters.AddWithValue("@HaftalikDersSaati", haftalikDersSaati);

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
            catch(Exception ex)
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
        private void Ders_Guncelle_Load(object sender, EventArgs e)
        {
            ComboBoxDoldur();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            comboBox1.SelectedIndex = -1;
        }
    }
}
