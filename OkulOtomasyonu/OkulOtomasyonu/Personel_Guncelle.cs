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
    public partial class Personel_Guncelle : Form
    {
        string connectionString = "Server=DESKTOP-I71FBSL\\SQLEXPRESS;Database=DB_Okul_Otomasyon;Trusted_Connection=True;";

        public Personel_Guncelle()
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
            textBox7.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            textBox6.Text = "";
            comboBox1.Text = "";
            maskedTextBox1.Text = "";
        }

        private void button6_Click(object sender, EventArgs e)
        {


            string query = "SELECT * FROM Personel WHERE TcNo = @TcNo";
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
                //textBox5.Text = reader["DogumYeri"].ToString();
                textBox6.Text = reader["Maas"].ToString();
                textBox7.Text = reader["Meslek"].ToString();
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
                MessageBox.Show("Girilen öğrenci numarası bulunamadı.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            baglanti.Close();
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

        private void button3_Click(object sender, EventArgs e)
        {
            string query = "UPDATE Personel " +
    "SET  Ad = @Ad, Soyad = @Soyad, Cinsiyet = @Cinsiyet, Yas = @Yas, TelefonNo = @TelefonNo, Maas = @Maas,Meslek = @Meslek, Resim = @Resim " +
                               "WHERE TcNo = @TcNo";



            SqlConnection baglanti = new SqlConnection(connectionString);
            baglanti.Open();
            byte[] resimBytes = null;
            if (pictureBox1.Image != null)
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    pictureBox1.Image.Save(ms, pictureBox1.Image.RawFormat);
                    resimBytes = ms.ToArray();
                }
            }
            SqlCommand command = new SqlCommand(query, baglanti);
            command.Parameters.AddWithValue("@TcNo", textBox1.Text);
            command.Parameters.AddWithValue("@Ad", textBox2.Text);
            command.Parameters.AddWithValue("@Soyad", textBox3.Text);
            command.Parameters.AddWithValue("@Yas", int.Parse(textBox4.Text));
            command.Parameters.AddWithValue("@Cinsiyet", comboBox1.Text);
            //command.Parameters.AddWithValue("@DogumYeri", textBox5.Text);
            command.Parameters.AddWithValue("@Maas", int.Parse(textBox6.Text));
            command.Parameters.AddWithValue("@Meslek", textBox7.Text);
            command.Parameters.AddWithValue("@TelefonNo", maskedTextBox1.Text);
            command.Parameters.AddWithValue("@Resim", (object)resimBytes ?? DBNull.Value);

            int rowsAffected = command.ExecuteNonQuery();
            baglanti.Close();

        }
    }  
}
