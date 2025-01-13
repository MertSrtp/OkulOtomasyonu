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
    public partial class Gelir_Guncelle : Form
    {
        string connectionString = "Server=DESKTOP-I71FBSL\\SQLEXPRESS;Database=DB_Okul_Otomasyon;Trusted_Connection=True;";

        public Gelir_Guncelle()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void button2_Click(object sender, EventArgs e)
        {

            string query = "SELECT * FROM Gelir WHERE KurumAdi = @KurumAdi";
            string kurumadi = textBox1.Text.Trim();

            if (string.IsNullOrEmpty(kurumadi))
            {
                MessageBox.Show("Lütfen geçerli bir Kurum Adı giriniz.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            SqlConnection baglanti = new SqlConnection(connectionString);
            baglanti.Open();
            SqlCommand command = new SqlCommand(query, baglanti);
            command.Parameters.AddWithValue("@KurumAdi", kurumadi);
            SqlDataReader reader = command.ExecuteReader();
            if (reader.Read())
            {
                
                textBox2.Text = reader["GelirTipi"].ToString();
                textBox3.Text = reader["Miktar"].ToString();
                textBox4.Text = reader["Taksit"].ToString();
                comboBox1.Text = reader["OdemeTuru"].ToString();
                dateTimePicker1.Text = reader["OdemeTarihi"].ToString();

            }
            else
            {
                MessageBox.Show("Girilen Kurum bulunamadı.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            baglanti.Close();

        }

        private void button3_Click(object sender, EventArgs e)
        {
            string query = "UPDATE Gelir " +
               "SET GelirTipi = @GelirTipi, Miktar = @Miktar, Taksit = @Taksit, OdemeTuru = @OdemeTuru, OdemeTarihi = @OdemeTarihi " +
               "WHERE KurumAdi = @KurumAdi";

            string kurumAdi = textBox1.Text.Trim();
            string gelirTipi = textBox2.Text.Trim();
            string miktar = textBox3.Text.Trim();
            string taksit = textBox4.Text.Trim();
            string odemeTuru = comboBox1.Text.Trim();
            


            if (string.IsNullOrEmpty(gelirTipi) || string.IsNullOrEmpty(miktar) ||
                string.IsNullOrEmpty(taksit) || string.IsNullOrEmpty(odemeTuru) ||
                 string.IsNullOrEmpty(kurumAdi))
            {
                MessageBox.Show("Lütfen tüm alanları doldurunuz.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            try
            {
                SqlConnection baglanti = new SqlConnection(connectionString);
                baglanti.Open();
                SqlCommand command = new SqlCommand(query, baglanti);
                command.Parameters.AddWithValue("@KurumAdi", kurumAdi);
                command.Parameters.AddWithValue("@GelirTipi", gelirTipi);
                command.Parameters.AddWithValue("@Miktar", miktar);
                command.Parameters.AddWithValue("@Taksit", taksit);
                command.Parameters.AddWithValue("@OdemeTuru", odemeTuru);
                command.Parameters.AddWithValue("@OdemeTarihi", dateTimePicker1.Value);

                int rowsAffected = command.ExecuteNonQuery();

                if (rowsAffected > 0)
                {
                    MessageBox.Show("Güncelleme başarılı!", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Gelir güncellenemedi. Lütfen Kurum Adını kontrol edin.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            comboBox1.SelectedIndex = -1;
            dateTimePicker1.Value = DateTime.Now;
        }
    }
}
