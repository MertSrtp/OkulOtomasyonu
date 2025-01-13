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
    public partial class Siniflar : Form
    {
        string connectionString = "Server=DESKTOP-I71FBSL\\SQLEXPRESS;Database=DB_Okul_Otomasyon;Trusted_Connection=True;";

        string query = "SELECT * FROM Sinif";
        public Siniflar()
        {
            InitializeComponent();
        }

        private void VeriYukle()
        {
            SqlConnection baglanti = new SqlConnection(connectionString);
            baglanti.Open();
            SqlDataAdapter da = new SqlDataAdapter(query, baglanti);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            baglanti.Close();
        }

        private void VeriSil()
        {
            int selectedID = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["SinifID"].Value);
            string query = "DELETE FROM Sinif WHERE SinifID = @SinifID";
            SqlConnection baglanti = new SqlConnection(connectionString);
            baglanti.Open();
            SqlCommand command = new SqlCommand(query, baglanti);
            command.Parameters.AddWithValue("@SinifID", selectedID);
            int rowsAffected = command.ExecuteNonQuery();
            VeriYukle();
            baglanti.Close();


        }
        private void button1_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            using(Sinif_Ekle ders = new Sinif_Ekle())
            {
                ders.ShowDialog();

            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            using (Sinif_Guncelle guncelle = new Sinif_Guncelle())
            {
                guncelle.ShowDialog();
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            VeriYukle();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            VeriSil();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            string sinifAdi = textBox1.Text.Trim();

            // Eğer sınıf kodu girilmemişse uyarı ver
            if (string.IsNullOrEmpty(sinifAdi))
            {
                MessageBox.Show("Lütfen bir sınıf adı giriniz.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // SQL sorgusu - sınıf koduna göre arama
            string query = "SELECT * FROM Sinif " +
                           "WHERE SinifAdi = @SinifAdi";

            try
            {
                // Veritabanı bağlantısı
                SqlConnection baglanti = new SqlConnection(connectionString);
                
                    baglanti.Open();
                // SQL komutu ve parametre ekleme
                SqlCommand command = new SqlCommand(query, baglanti);
                command.Parameters.AddWithValue("@SinifAdi", sinifAdi);

                // DataTable kullanarak sonuçları doldur
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);
                // Verileri okuyucu ile al
                SqlDataReader reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        dataGridView1.DataSource = dataTable;
                }
                    else
                    {
                        // Eğer sınıf bulunamazsa bilgi mesajı göster
                        MessageBox.Show("Girilen sınıf bulunamadı.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                
            }
            catch (Exception ex)
            {
                // Hata durumunda mesaj göster
                MessageBox.Show("Bir hata oluştu: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
