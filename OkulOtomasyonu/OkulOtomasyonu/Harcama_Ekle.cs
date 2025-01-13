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
    public partial class Harcama_Ekle : Form
    {
        string connectionString = "Server=DESKTOP-I71FBSL\\SQLEXPRESS;Database=DB_Okul_Otomasyon;Trusted_Connection=True;";

        string query = "INSERT INTO Harcama (HarcamaTipi, KurumAdi, Miktar, OdemeTuru, Taksit, OdemeTarihi) " +
                           "VALUES (@HarcamaTipi, @KurumAdi, @Miktar, @OdemeTuru, @Taksit, @OdemeTarihi)";
        public Harcama_Ekle()
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
            dateTimePicker1.Value = DateTime.Now;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            SqlConnection baglanti = new SqlConnection(connectionString);
            baglanti.Open();
            SqlCommand command = new SqlCommand(query, baglanti);
            command.Parameters.AddWithValue("@HarcamaTipi", textBox1.Text);
            command.Parameters.AddWithValue("@KurumAdi", textBox2.Text);
            command.Parameters.AddWithValue("@Miktar", int.Parse(textBox3.Text));
            command.Parameters.AddWithValue("@Taksit", int.Parse(textBox4.Text));
            command.Parameters.AddWithValue("@OdemeTuru", comboBox1.Text);
            command.Parameters.AddWithValue("@OdemeTarihi", dateTimePicker1.Value);

            int rowsAffected = command.ExecuteNonQuery();
            baglanti.Close();
        }
    }
}
