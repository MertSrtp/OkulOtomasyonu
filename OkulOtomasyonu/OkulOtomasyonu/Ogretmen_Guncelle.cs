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
using System.IO;

namespace OkulOtomasyonu
{
    public partial class Ogretmen_Guncelle : Form
    {
        string connectionString = "Server=DESKTOP-I71FBSL\\SQLEXPRESS;Database=DB_Okul_Otomasyon;Trusted_Connection=True;";
        
        public Ogretmen_Guncelle()
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
            textBox6.Text = "";
            comboBox1.Text = "";
            maskedTextBox1.Text = "";
            pictureBox1.Image = null;

        }

        private void button2_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Title = "Resim Seç";
            openFileDialog.Filter = "Resim Dosyaları|*.jpg;*.jpeg;*.png;*.bmp";

            // Eğer kullanıcı bir dosya seçtiyse
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                // Seçilen dosyayı PictureBox içine yükle
                pictureBox1.ImageLocation = openFileDialog.FileName;
            }
        }

        private void button6_Click_1(object sender, EventArgs e)
        {


            string query = "SELECT * FROM Personel WHERE TcNo = @TcNo AND Meslek = 'Öğretmen'";
            string tcNo = textBox1.Text.Trim();

            if (string.IsNullOrEmpty(tcNo))
            {
                MessageBox.Show("Lütfen geçerli bir tc kimlik numarası giriniz.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            SqlConnection baglanti = new SqlConnection(connectionString);
            baglanti.Open();
            SqlCommand command = new SqlCommand(query, baglanti);
            command.Parameters.AddWithValue("@TcNo", tcNo);
            SqlDataReader reader = command.ExecuteReader();
            if (reader.Read())
            {
                textBox2.Text = reader["Ad"].ToString();
                textBox3.Text = reader["Soyad"].ToString(); ;
                textBox4.Text = reader["Yas"].ToString();
                comboBox1.Text = reader["Cinsiyet"].ToString();
                textBox6.Text = reader["Maas"].ToString();
                //textBox7.Text = reader["Meslek"].ToString();
                maskedTextBox1.Text = reader["TelefonNo"].ToString(); ;
                // Resmi PictureBox'a yükle
                if (reader["Resim"] != DBNull.Value)
                {
                    byte[] resimBytes = (byte[])reader["Resim"]; // Resmi binary olarak al
                    using (MemoryStream ms = new MemoryStream(resimBytes))
                    {
                        pictureBox1.Image = Image.FromStream(ms); // Resmi PictureBox'a yükle
                    }
                }
                else
                {
                    pictureBox1.Image = null; // Eğer resim yoksa PictureBox'ı temizle
                }


            }
            else
            {
                MessageBox.Show("Girilen öğretmen numarası bulunamadı.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            baglanti.Close();
        }


        private void button3_Click_1(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    
                    // Resmi byte dizisine çevir
                    byte[] resimBytes = null;
                    if (pictureBox1.Image != null)
                    {
                        using (MemoryStream ms = new MemoryStream())
                        {
                            pictureBox1.Image.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                            resimBytes = ms.ToArray();
                        }
                    }

                    // Sadece Personel tablosunu güncelle (Meslek = 'Öğretmen' olan kayıtlar için)
                    string updateQuery = @"
                        UPDATE Personel 
                        SET Ad = @Ad,
                            Soyad = @Soyad,
                            Yas = @Yas,
                            Cinsiyet = @Cinsiyet,
                            TelefonNo = @TelefonNo,
                            Maas = @Maas,
                            Resim = @Resim
                        WHERE TcNo = @TcNo 
                        AND Meslek = 'Öğretmen'";

                    using (SqlCommand cmd = new SqlCommand(updateQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@TcNo", textBox1.Text);
                        cmd.Parameters.AddWithValue("@Ad", textBox2.Text);
                        cmd.Parameters.AddWithValue("@Soyad", textBox3.Text);
                        cmd.Parameters.AddWithValue("@Yas", int.Parse(textBox4.Text));
                        cmd.Parameters.AddWithValue("@Cinsiyet", comboBox1.Text);
                        cmd.Parameters.AddWithValue("@Maas", int.Parse(textBox6.Text));
                        cmd.Parameters.AddWithValue("@TelefonNo", maskedTextBox1.Text);
                        cmd.Parameters.AddWithValue("@Resim", (object)resimBytes ?? DBNull.Value);

                        int result = cmd.ExecuteNonQuery();
                        if (result > 0)
                        {
                            MessageBox.Show("Öğretmen bilgileri başarıyla güncellendi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            this.Close();
                        }
                        else
                        {
                            MessageBox.Show("Güncelleme yapılamadı. Kayıt bulunamadı veya seçilen kişi öğretmen değil.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Güncelleme sırasında bir hata oluştu: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
