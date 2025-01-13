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
    public partial class Sinif_Ekle : Form
    {
        string connectionString = "Server=DESKTOP-I71FBSL\\SQLEXPRESS;Database=DB_Okul_Otomasyon;Trusted_Connection=True;";

        string query = "INSERT INTO Sinif (SinifAdi, OgrenciSayisi, DersAdi, SinifOgretmeni, AlanTuru) " +
                           "VALUES (@SinifAdi, @OgrenciSayisi, @DersAdi, @SinifOgretmeni, @AlanTuru)";

        public Sinif_Ekle()
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

        private void button3_Click(object sender, EventArgs e)
        {

            SqlConnection baglanti = new SqlConnection(connectionString);
            baglanti.Open();

            SqlCommand command = new SqlCommand(query, baglanti);
            command.Parameters.AddWithValue("@SinifAdi", textBox1.Text);
            command.Parameters.AddWithValue("@OgrenciSayisi", textBox2.Text);
            command.Parameters.AddWithValue("@DersAdi", textBox3.Text);
            command.Parameters.AddWithValue("@SinifOgretmeni", textBox4.Text);
            command.Parameters.AddWithValue("@AlanTuru", comboBox1.Text);

            int rowsAffected = command.ExecuteNonQuery();
            baglanti.Close();

        }
    }
}
