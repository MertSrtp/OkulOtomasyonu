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
    public partial class Personel_Ekle : Form
    {
        string connectionString = "Server=DESKTOP-I71FBSL\\SQLEXPRESS;Database=DB_Okul_Otomasyon;Trusted_Connection=True;";

        string query = "INSERT INTO Personel (TcNo, Ad, Soyad, Cinsiyet, Yas, TelefonNo, Maas, Meslek, Resim) " +
                           "VALUES (@TcNo, @Ad, @Soyad, @Cinsiyet, @Yas, @TelefonNo, @Maas, @Meslek, @Resim)";

        public Personel_Ekle()
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
            command.Parameters.AddWithValue("@TcNo",textBox1.Text);
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
