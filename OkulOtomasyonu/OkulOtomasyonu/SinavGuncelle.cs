using System;
using System.Data;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace OkulOtomasyonu
{
    public partial class SinavGuncelle : Form
    {
        string connectionString = "Server=DESKTOP-I71FBSL\\SQLEXPRESS;Database=DB_Okul_Otomasyon;Trusted_Connection=True;";

        public SinavGuncelle()
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

        private void button2_Click(object sender, EventArgs e)
        {
            // Sınav Ara butonu
            if (!string.IsNullOrEmpty(textBox1.Text))
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    try
                    {
                        conn.Open();
                        string query = @"SELECT * FROM Sinav WHERE SinavKodu = @SinavKodu";
                        using (SqlCommand cmd = new SqlCommand(query, conn))
                        {
                            cmd.Parameters.AddWithValue("@SinavKodu", textBox1.Text);
                            using (SqlDataReader reader = cmd.ExecuteReader())
                            {
                                if (reader.Read())
                                {
                                    textBox2.Text = reader["SinavAdi"].ToString();
                                    comboBox1.SelectedItem = reader["Donem"].ToString();
                                    comboBox2.SelectedItem = reader["SinifBilgisi"].ToString();
                                    comboBox4.SelectedItem = reader["SinavYeri"].ToString();
                                    comboBox3.SelectedItem = reader["GozetmenHoca"].ToString();
                                }
                                else
                                {
                                    MessageBox.Show("Sınav bulunamadı!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Sınav aranırken hata oluştu: " + ex.Message);
                    }
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
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
                    string updateQuery = @"
                        UPDATE Sinav 
                        SET SinavAdi = @SinavAdi,
                            Donem = @Donem,
                            SinifBilgisi = @SinifBilgisi,
                            SinavYeri = @SinavYeri,
                            GozetmenHoca = @GozetmenHoca
                        WHERE SinavKodu = @SinavKodu";

                    using (SqlCommand cmd = new SqlCommand(updateQuery, conn))
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
                            MessageBox.Show("Sınav başarıyla güncellendi!", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            FormuTemizle();
                        }
                        else
                        {
                            MessageBox.Show("Güncelleme yapılamadı!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Güncelleme sırasında bir hata oluştu: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        private void SinavGuncelle_Load(object sender, EventArgs e)
        {
            DersleriYukle();
            OgretmenleriYukle();
        }
    }
}
