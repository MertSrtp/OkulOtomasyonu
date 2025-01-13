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
    public partial class Ogrenci_Kayit : Form
    {

        string connectionString = "Server=DESKTOP-I71FBSL\\SQLEXPRESS;Database=DB_Okul_Otomasyon;Trusted_Connection=True;";

        string query = "SELECT * FROM Ogrenci";

        public Ogrenci_Kayit()
        {
            InitializeComponent();
        }

        private void VeriYukle()
        {
            SqlConnection baglanti = new SqlConnection(connectionString);
            baglanti.Open();
            SqlDataAdapter da = new SqlDataAdapter(query, baglanti);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            baglanti.Close();
        }

        private void VeriSil()
        {
            int selectedID = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["OgrenciID"].Value);
            string query = "DELETE FROM Ogrenci WHERE OgrenciID = @OgrenciID";
            SqlConnection baglanti = new SqlConnection(connectionString);
            baglanti.Open();
            SqlCommand command = new SqlCommand(query, baglanti);
            command.Parameters.AddWithValue("@OgrenciID", selectedID);
            int rowsAffected = command.ExecuteNonQuery();
            VeriYukle();
            baglanti.Close();


        }
        private void button1_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            using (Ogrenci_Ekle ekle = new Ogrenci_Ekle())
            {
                ekle.ShowDialog();

            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            using(Ogrenci_Guncelle guncelle = new Ogrenci_Guncelle())
            {
                guncelle.ShowDialog();
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {

            VeriYukle();

        }

        private void button4_Click(object sender, EventArgs e)
        {
            VeriSil();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            // TextBox boş değilse arama yap
            if (!string.IsNullOrEmpty(textBox1.Text))
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    try
                    {
                        conn.Open();
                        
                        // Öğrenci numarasına göre arama yapan SQL sorgusu
                        string query = "SELECT * FROM Ogrenci WHERE OgrenciNumara = @ogrenciNumara";
                        
                        using (SqlCommand cmd = new SqlCommand(query, conn))
                        {
                            cmd.Parameters.AddWithValue("@ogrenciNumara", textBox1.Text);
                            
                            // Sonuçları DataTable'a aktar
                            SqlDataAdapter da = new SqlDataAdapter(cmd);
                            DataTable dt = new DataTable();
                            da.Fill(dt);
                            
                            // DataGridView'e sonuçları göster
                            dataGridView1.DataSource = dt;

                            if (dt.Rows.Count == 0)
                            {
                                MessageBox.Show("Bu Öğrenci Numarasına ait kayıt bulunamadı.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Arama sırasında hata oluştu: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Lütfen bir Öğrenci Numarası giriniz.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
