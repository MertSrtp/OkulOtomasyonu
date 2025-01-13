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
    public partial class Menu_Ekle : Form
    {
        string connectionString = "Server=DESKTOP-I71FBSL\\SQLEXPRESS;Database=DB_Okul_Otomasyon;Trusted_Connection=True;";

        string query = "INSERT INTO Menu (Tarih, Cesit1, Cesit2, Cesit3, Cesit4, Ekstra) " +
                           "VALUES (@Tarih, @Cesit1, @Cesit2, @Cesit3, @Cesit4, @Ekstra)";

        public Menu_Ekle()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            dateTimePicker1.Value = DateTime.Now;
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            textBox5.Text = "";
        }

        private void button3_Click(object sender, EventArgs e)
        {
            SqlConnection baglanti = new SqlConnection(connectionString);
            baglanti.Open();

            SqlCommand command = new SqlCommand(query, baglanti);
            command.Parameters.AddWithValue("@Cesit1", textBox1.Text);
            command.Parameters.AddWithValue("@Cesit2", textBox2.Text);
            command.Parameters.AddWithValue("@Cesit3", textBox3.Text);
            command.Parameters.AddWithValue("@Cesit4", textBox4.Text);
            command.Parameters.AddWithValue("@Ekstra", textBox5.Text);
            command.Parameters.AddWithValue("@Tarih", dateTimePicker1.Value);

            int rowsAffected = command.ExecuteNonQuery();
            baglanti.Close();

        }
    }
}
