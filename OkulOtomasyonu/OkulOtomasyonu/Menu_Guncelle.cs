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
    public partial class Menu_Guncelle : Form
    {
        string connectionString = "Server=DESKTOP-I71FBSL\\SQLEXPRESS;Database=DB_Okul_Otomasyon;Trusted_Connection=True;";

        public Menu_Guncelle()
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

        private void button2_Click(object sender, EventArgs e)
        {
            string query = "SELECT * FROM Menu WHERE Tarih = @Tarih";
            

            if (string.IsNullOrEmpty(dateTimePicker1.Text))
            {
                MessageBox.Show("Lütfen geçerli bir Tarih giriniz.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            SqlConnection baglanti = new SqlConnection(connectionString);
            baglanti.Open();
            SqlCommand command = new SqlCommand(query, baglanti);
            command.Parameters.AddWithValue("@Tarih", dateTimePicker1.Value);
            SqlDataReader reader = command.ExecuteReader();
            if (reader.Read())
            {
                textBox1.Text = reader["Cesit1"].ToString();
                textBox2.Text = reader["Cesit2"].ToString();
                textBox3.Text = reader["Cesit3"].ToString();
                textBox4.Text = reader["Cesit4"].ToString();
                textBox5.Text = reader["Ekstra"].ToString();

            }
            else
            {
                MessageBox.Show("Girilen Tarihte herhangi bir Menü bulunamadı.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            baglanti.Close();

        }

        private void button3_Click(object sender, EventArgs e)
        {

            string query = "UPDATE Menu " +
                           "SET Cesit1 = @Cesit1, Cesit2 = @Cesit2, Cesit3 = @Cesit3, Cesit4 = @Cesit4, Ekstra = @Ekstra " +
                           "WHERE Tarih = @Tarih";

            string cesit1 = textBox1.Text.Trim();
            string cesit2 = textBox2.Text.Trim();
            string cesit3 = textBox3.Text.Trim();
            string cesit4 = textBox4.Text.Trim();
            string ekstra = textBox5.Text.Trim();


            if (string.IsNullOrEmpty(cesit1) || string.IsNullOrEmpty(cesit2) ||
                string.IsNullOrEmpty(cesit3) || string.IsNullOrEmpty(cesit4) ||
                string.IsNullOrEmpty(ekstra))
            {
                MessageBox.Show("Lütfen tüm alanları doldurunuz.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            try
            {
                SqlConnection baglanti = new SqlConnection(connectionString);
                baglanti.Open();
                SqlCommand command = new SqlCommand(query, baglanti);
                command.Parameters.AddWithValue("@Tarih", dateTimePicker1.Value);
                command.Parameters.AddWithValue("@Cesit1", cesit1);
                command.Parameters.AddWithValue("@Cesit2", cesit2);
                command.Parameters.AddWithValue("@Cesit3", cesit3);
                command.Parameters.AddWithValue("@Cesit4", cesit4);
                command.Parameters.AddWithValue("@Ekstra", ekstra);

                int rowsAffected = command.ExecuteNonQuery();

                if (rowsAffected > 0)
                {
                    MessageBox.Show("Güncelleme başarılı!", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Menü güncellenemedi. Lütfen Tarihi kontrol edin.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Bir hata oluştu: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
