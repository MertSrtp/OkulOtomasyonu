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
    public partial class Ogrenci_Ekle : Form
    {
        string connectionString = "Server=DESKTOP-I71FBSL\\SQLEXPRESS;Database=DB_Okul_Otomasyon;Trusted_Connection=True;";

        string query = "INSERT INTO Ogrenci (Sinif, OgrenciNumara, Ad, Soyad, Cinsiyet, Yas, AnneAdi, BabaAdi, TelNo, Mail, DogumYeri, DogumTarihi, Resim) " +
                           "VALUES (@Sinif, @OgrenciNumara, @Ad, @Soyad, @Cinsiyet, @Yas, @AnneAdi, @BabaAdi, @TelNo, @Mail, @DogumYeri, @DogumTarihi, @Resim)";

        public Ogrenci_Ekle()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Dispose();

        }

        private void button3_Click(object sender, EventArgs e)
        {

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
            command.Parameters.AddWithValue("@Sinif", textBox1.Text);
            command.Parameters.AddWithValue("@OgrenciNumara", textBox2.Text);
            command.Parameters.AddWithValue("@Ad", textBox3.Text);
            command.Parameters.AddWithValue("@Soyad", textBox4.Text);
            command.Parameters.AddWithValue("@Cinsiyet", comboBox1.SelectedItem);
            command.Parameters.AddWithValue("@Yas", int.Parse(textBox6.Text));
            command.Parameters.AddWithValue("@AnneAdi", textBox7.Text);
            command.Parameters.AddWithValue("@BabaAdi", textBox8.Text);
            command.Parameters.AddWithValue("@TelNo", maskedTextBox1.Text);
            command.Parameters.AddWithValue("@Mail", textBox10.Text);
            command.Parameters.AddWithValue("@DogumYeri", textBox11.Text);
            command.Parameters.AddWithValue("@DogumTarihi", dateTimePicker1.Value);
            command.Parameters.AddWithValue("@Resim", (object)resimBytes ?? DBNull.Value);

            int rowsAffected = command.ExecuteNonQuery();
            baglanti.Close();

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
