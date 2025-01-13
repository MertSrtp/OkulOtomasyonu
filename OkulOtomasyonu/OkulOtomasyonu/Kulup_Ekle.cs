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
    public partial class Kulup_Ekle : Form
    {
        string connectionString = "Server=DESKTOP-I71FBSL\\SQLEXPRESS;Database=DB_Okul_Otomasyon;Trusted_Connection=True;";

        string query = "INSERT INTO Kulupler (KulupKodu, KulupAdi, KulupBaskani, DanismanHoca, KulupTuru) " +
                           "VALUES (@KulupKodu, @KulupAdi, @KulupBaskani, @DanismanHoca, @KulupTuru)";

        public Kulup_Ekle()
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

        private void button3_Click(object sender, EventArgs e)
        {

            SqlConnection baglanti = new SqlConnection(connectionString);
            baglanti.Open();
            SqlCommand command = new SqlCommand(query, baglanti);
            command.Parameters.AddWithValue("@KulupKodu", textBox1.Text);
            command.Parameters.AddWithValue("@KulupAdi", textBox2.Text);
            command.Parameters.AddWithValue("@KulupBaskani", textBox3.Text);
            command.Parameters.AddWithValue("@DanismanHoca", comboBox1.SelectedItem);
            command.Parameters.AddWithValue("@KulupTuru", textBox5.Text);

            int rowsAffected = command.ExecuteNonQuery();
            baglanti.Close();

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

        private void Kulup_Ekle_Load(object sender, EventArgs e)
        {
            ComboBoxDoldur();
        }
    }
}
