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
    public partial class Sinav_Ekle : Form
    {
        string connectionString = "Server=DESKTOP-I71FBSL\\SQLEXPRESS;Database=DB_Okul_Otomasyon;Trusted_Connection=True;";

        public Sinav_Ekle()
        {
            InitializeComponent();
        }


        private void DersleriYukle()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "SELECT DISTINCT SinifAdi FROM Sinif";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            comboBox4.Items.Clear();
                            while (reader.Read())
                            {
                                comboBox4.Items.Add(reader["SinifAdi"].ToString());
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Dersler yüklenirken hata oluştu: " + ex.Message);
            }
        }

        private void OgretmenleriYukle()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = @"SELECT PersonelID, 
                                      CAST(PersonelID AS VARCHAR) + ' - ' + Ad + ' ' + Soyad AS AdSoyad 
                                   FROM Personel 
                                   WHERE Meslek = 'Öğretmen'
                                   ORDER BY PersonelID";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        comboBox3.Items.Clear();
                        while (dt.Rows.Count > 0)
                        {
                            comboBox3.Items.Add(dt.Rows[0]["AdSoyad"].ToString());
                            dt.Rows.RemoveAt(0);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Öğretmenler yüklenirken hata oluştu: " + ex.Message);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                // Gerekli alanların dolu olup olmadığını kontrol et
                if (string.IsNullOrEmpty(textBox1.Text) || string.IsNullOrEmpty(textBox2.Text) ||
                    comboBox1.SelectedIndex == -1 || comboBox2.SelectedIndex == -1 ||
                    comboBox3.SelectedIndex == -1 || comboBox4.SelectedIndex == -1)
                {
                    MessageBox.Show("Lütfen tüm alanları doldurunuz!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string insertQuery = @"
                        INSERT INTO Sinav (
                            SinavKodu,
                            SinavAdi,
                            Donem,
                            SinifBilgisi,
                            SinavYeri,
                            GozetmenHoca
                        ) VALUES (
                            @SinavKodu,
                            @SinavAdi,
                            @Donem,
                            @SinifBilgisi,
                            @SinavYeri,
                            @GozetmenHoca
                        )";

                    using (SqlCommand cmd = new SqlCommand(insertQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@SinavKodu", textBox1.Text);
                        cmd.Parameters.AddWithValue("@SinavAdi", textBox2.Text);
                        cmd.Parameters.AddWithValue("@Donem", comboBox1.SelectedItem.ToString());
                        cmd.Parameters.AddWithValue("@SinifBilgisi", comboBox2.SelectedItem.ToString());
                        cmd.Parameters.AddWithValue("@SinavYeri", comboBox4.SelectedItem.ToString());
                        cmd.Parameters.AddWithValue("@GozetmenHoca", comboBox3.SelectedItem.ToString());
                        int result = cmd.ExecuteNonQuery();
                        if (result > 0)
                        {
                            MessageBox.Show("Sınav başarıyla eklendi!", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            FormuTemizle();
                        }
                        else
                        {
                            MessageBox.Show("Sınav eklenirken bir hata oluştu!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            catch (FormatException)
            {
                MessageBox.Show("Lütfen puanı sayısal bir değer olarak giriniz!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ekleme sırasında bir hata oluştu: " + ex.Message);
            }
        }

        private void FormuTemizle()
        {
            textBox1.Clear();
            textBox2.Clear();
            comboBox1.SelectedIndex = -1;
            comboBox2.SelectedIndex = -1;
            comboBox3.SelectedIndex = -1;
            comboBox4.SelectedIndex = -1;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            FormuTemizle();
        }

        private void Sinav_Ekle_Load(object sender, EventArgs e)
        {

            DersleriYukle();
            OgretmenleriYukle();
        }
    }
}
