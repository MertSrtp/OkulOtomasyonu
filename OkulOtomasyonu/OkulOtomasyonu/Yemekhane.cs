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
    public partial class Yemekhane : Form
    {
        string connectionString = "Server=DESKTOP-I71FBSL\\SQLEXPRESS;Database=DB_Okul_Otomasyon;Trusted_Connection=True;";

        string query = "SELECT * FROM Menu";
        public Yemekhane()
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
            int selectedID = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["MenuID"].Value);
            string query = "DELETE FROM Menu WHERE MenuID = @MenuID";
            SqlConnection baglanti = new SqlConnection(connectionString);
            baglanti.Open();
            SqlCommand command = new SqlCommand(query, baglanti);
            command.Parameters.AddWithValue("@MenuID", selectedID);
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
            using(Menu_Ekle ekle = new Menu_Ekle())
            {
                ekle.ShowDialog();

            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            using(Menu_Guncelle guncelle = new Menu_Guncelle())
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
    }
}
