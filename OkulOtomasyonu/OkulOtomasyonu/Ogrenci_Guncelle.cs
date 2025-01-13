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
    public partial class Ogrenci_Guncelle : Form
    {
        string connectionString = "Server=DESKTOP-I71FBSL\\SQLEXPRESS;Database=DB_Okul_Otomasyon;Trusted_Connection=True;";
        
        public Ogrenci_Guncelle()
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
            comboBox1.SelectedIndex = -1;
            textBox6.Text = "";
            textBox7.Text = "";
            textBox8.Text = "";
            maskedTextBox1.Text = "";
            textBox10.Text = "";
            textBox11.Text = "";
            dateTimePicker1.Value = DateTime.Now;
            pictureBox1.Image = null;

        }

        private void button6_Click(object sender, EventArgs e)
        {
            string query = "SELECT * FROM Ogrenci WHERE OgrenciNumara = @OgrenciNumara";
            string OgrenciNo = textBox2.Text.Trim();

            if (string.IsNullOrEmpty(OgrenciNo))
            {
                MessageBox.Show("Lütfen geçerli bir öğrenci numarası giriniz.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (SqlConnection baglanti = new SqlConnection(connectionString))
            {
                try
                {
                    baglanti.Open();
                    using (SqlCommand command = new SqlCommand(query, baglanti))
                    {
                        command.Parameters.AddWithValue("@OgrenciNumara", OgrenciNo);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                // Diğer alanları doldur
                                textBox1.Text = reader["Sinif"].ToString();
                                textBox3.Text = reader["Ad"].ToString();
                                textBox4.Text = reader["Soyad"].ToString();
                                comboBox1.Text = reader["Cinsiyet"].ToString();
                                textBox6.Text = reader["Yas"].ToString();
                                textBox7.Text = reader["AnneAdi"].ToString();
                                textBox8.Text = reader["BabaAdi"].ToString();
                                maskedTextBox1.Text = reader["TelNo"].ToString();
                                textBox10.Text = reader["Mail"].ToString();
                                textBox11.Text = reader["DogumYeri"].ToString();
                                dateTimePicker1.Text = reader["DogumTarihi"].ToString();

                                // Resmi güvenli bir şekilde yükle
                                if (reader["Resim"] != DBNull.Value)
                                {
                                    try
                                    {
                                        byte[] resimBytes = (byte[])reader["Resim"];
                                        using (var ms = new MemoryStream(resimBytes))
                                        {
                                            // Önce mevcut resmi temizle
                                            if (pictureBox1.Image != null)
                                            {
                                                var temp = pictureBox1.Image;
                                                pictureBox1.Image = null;
                                                temp.Dispose();
                                            }

                                            // Yeni resmi yükle
                                            using (var image = Image.FromStream(ms))
                                            {
                                                pictureBox1.Image = new Bitmap(image);
                                            }
                                        }
                                    }
                                    catch (Exception ex)
                                    {
                                        MessageBox.Show("Resim yüklenirken bir hata oluştu: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                        pictureBox1.Image = null;
                                    }
                                }
                                else
                                {
                                    pictureBox1.Image = null;
                                }
                            }
                            else
                            {
                                MessageBox.Show("Girilen öğrenci numarası bulunamadı.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Veri yüklenirken bir hata oluştu: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string query = "UPDATE Ogrenci " +
                "SET [Sinif] = @Sinif, Ad = @Ad, Soyad = @Soyad, Cinsiyet = @Cinsiyet, Yas = @Yas, " +
                "AnneAdi = @AnneAdi, BabaAdi = @BabaAdi, TelNo = @TelNo, Mail = @Mail, " +
                "DogumYeri = @DogumYeri, DogumTarihi = @DogumTarihi, Resim = @Resim " +
                "WHERE OgrenciNumara = @OgrenciNumara";

            using (SqlConnection baglanti = new SqlConnection(connectionString))
            {
                try
                {
                    baglanti.Open();
                    byte[] resimBytes = null;

                    // Eğer PictureBox içinde resim varsa
                    if (pictureBox1.Image != null)
                    {
                        using (MemoryStream ms = new MemoryStream())
                        {
                            // Resmi PNG formatında kaydet
                            pictureBox1.Image.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                            resimBytes = ms.ToArray();
                        }
                    }

                    using (SqlCommand command = new SqlCommand(query, baglanti))
                    {
                        command.Parameters.AddWithValue("@Sinif", textBox1.Text);
                        command.Parameters.AddWithValue("@OgrenciNumara", textBox2.Text);
                        command.Parameters.AddWithValue("@Ad", textBox3.Text);
                        command.Parameters.AddWithValue("@Soyad", textBox4.Text);
                        command.Parameters.AddWithValue("@Cinsiyet", comboBox1.Text);
                        command.Parameters.AddWithValue("@Yas", int.Parse(textBox6.Text));
                        command.Parameters.AddWithValue("@AnneAdi", textBox7.Text);
                        command.Parameters.AddWithValue("@BabaAdi", textBox8.Text);
                        command.Parameters.AddWithValue("@TelNo", maskedTextBox1.Text);
                        command.Parameters.AddWithValue("@Mail", textBox10.Text);
                        command.Parameters.AddWithValue("@DogumYeri", textBox11.Text);
                        command.Parameters.AddWithValue("@DogumTarihi", dateTimePicker1.Value);
                        command.Parameters.AddWithValue("@Resim", (object)resimBytes ?? DBNull.Value);

                        int rowsAffected = command.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Güncelleme başarılı!", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show("Güncelleme yapılamadı!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Güncelleme sırasında bir hata oluştu: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
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
    }
}
