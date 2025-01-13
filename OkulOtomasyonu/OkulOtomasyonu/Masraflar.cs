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
    public partial class Masraflar : Form
    {
        string connectionString = "Server=DESKTOP-I71FBSL\\SQLEXPRESS;Database=DB_Okul_Otomasyon;Trusted_Connection=True;";

        string query = "SELECT * FROM Harcama";
        public Masraflar()
        {
            InitializeComponent();
        }

        private void VeriYukle()
        {
            try
            {
                SqlConnection baglanti = new SqlConnection(connectionString);
                baglanti.Open();
                SqlDataAdapter da = new SqlDataAdapter(query, baglanti);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dataGridView1.DataSource = dt;
                baglanti.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Veri yüklenirken bir hata oluştu: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void VeriSil()
        {
            try
            {
                if (dataGridView1.SelectedRows.Count > 0)
                {
                    int selectedID = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["HarcamaID"].Value);
                    string query = "DELETE FROM Harcama WHERE HarcamaID = @HarcamaID";
                    SqlConnection baglanti = new SqlConnection(connectionString);
                    baglanti.Open();
                    SqlCommand command = new SqlCommand(query, baglanti);
                    command.Parameters.AddWithValue("@HarcamaID", selectedID);
                    int rowsAffected = command.ExecuteNonQuery();
                    VeriYukle();
                    baglanti.Close();

                    MessageBox.Show(rowsAffected > 0 ? "Veri başarıyla silindi." : "Silinecek veri bulunamadı.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Lütfen silmek istediğiniz bir satırı seçin.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Veri silinirken bir hata oluştu: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            using(Harcama_Ekle ekle = new Harcama_Ekle())
            {
                ekle.ShowDialog();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            using(Harcama_Guncelle guncelle = new Harcama_Guncelle())
            {
                guncelle.ShowDialog();
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                VeriYukle();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Veriler yüklenirken bir hata oluştu: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                VeriSil();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Veri silinirken bir hata oluştu: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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
                        
                        // Harcama ID'ye göre arama yapan SQL sorgusu
                        string query = "SELECT * FROM Harcama WHERE HarcamaID = @harcamaId";
                        
                        using (SqlCommand cmd = new SqlCommand(query, conn))
                        {
                            cmd.Parameters.AddWithValue("@harcamaId", textBox1.Text);
                            
                            // Sonuçları DataTable'a aktar
                            SqlDataAdapter da = new SqlDataAdapter(cmd);
                            DataTable dt = new DataTable();
                            da.Fill(dt);
                            
                            // DataGridView'e sonuçları göster
                            dataGridView1.DataSource = dt;

                            if (dt.Rows.Count == 0)
                            {
                                MessageBox.Show("Bu Harcama ID'ye ait kayıt bulunamadı.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                MessageBox.Show("Lütfen bir Harcama ID giriniz.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
