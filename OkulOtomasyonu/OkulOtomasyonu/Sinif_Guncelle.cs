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
    public partial class Sinif_Guncelle : Form
    {
        string connectionString = "Server=DESKTOP-I71FBSL\\SQLEXPRESS;Database=DB_Okul_Otomasyon;Trusted_Connection=True;";

        public Sinif_Guncelle()
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
            textBox4.Text = "";
            comboBox1.Text = "";

        }

        private void button2_Click(object sender, EventArgs e)
        {
            string query = "SELECT * FROM Sinif WHERE SinifAdi = @SinifAdi";
            string sinifAdi = textBox1.Text.Trim();

            if (string.IsNullOrEmpty(sinifAdi))
            {
                MessageBox.Show("Lütfen geçerli bir Sınıf Adı giriniz.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            SqlConnection baglanti = new SqlConnection(connectionString);
            baglanti.Open();
            SqlCommand command = new SqlCommand(query, baglanti);
            command.Parameters.AddWithValue("@SinifAdi", sinifAdi);
            SqlDataReader reader = command.ExecuteReader();
            if (reader.Read())
            {
                textBox2.Text = reader["OgrenciSayisi"].ToString();
                textBox3.Text = reader["DersAdi"].ToString();
                textBox4.Text = reader["SinifOgretmeni"].ToString();
                comboBox1.Text = reader["AlanTuru"].ToString();
            }
            else
            {
                MessageBox.Show("Girilen Sınıf Adı bulunamadı.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            baglanti.Close();

        }

        private void button3_Click(object sender, EventArgs e)
        {

            string query = "UPDATE Sinif " +
                           "SET  OgrenciSayisi = @OgrenciSayisi, DersAdi = @DersAdi, SinifOgretmeni = @SinifOgretmeni, AlanTuru = @AlanTuru " +
                           "WHERE SinifAdi = @SinifAdi";

            string sinifAdi = textBox1.Text.Trim();
            string ogrenciSayisi = textBox2.Text.Trim();
            string dersAdi = textBox3.Text.Trim();
            string sinifOgretmeni = textBox4.Text.Trim();
            string alanTuru = comboBox1.Text.Trim();


            if (string.IsNullOrEmpty(sinifAdi) || string.IsNullOrEmpty(ogrenciSayisi) ||
                string.IsNullOrEmpty(dersAdi) || string.IsNullOrEmpty(sinifOgretmeni) ||
                string.IsNullOrEmpty(alanTuru))
            {
                MessageBox.Show("Lütfen tüm alanları doldurunuz.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            try
            {
                SqlConnection baglanti = new SqlConnection(connectionString);
                baglanti.Open();
                SqlCommand command = new SqlCommand(query, baglanti);
                command.Parameters.AddWithValue("@SinifAdi", sinifAdi);
                command.Parameters.AddWithValue("@OgrenciSayisi", ogrenciSayisi);
                command.Parameters.AddWithValue("@DersAdi", dersAdi);
                command.Parameters.AddWithValue("@SinifOgretmeni", sinifOgretmeni);
                command.Parameters.AddWithValue("@AlanTuru", alanTuru);

                int rowsAffected = command.ExecuteNonQuery();

                if (rowsAffected > 0)
                {
                    MessageBox.Show("Güncelleme başarılı!", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Sınıf güncellenemedi. Lütfen Sınıf Adını kontrol edin.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Bir hata oluştu: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
