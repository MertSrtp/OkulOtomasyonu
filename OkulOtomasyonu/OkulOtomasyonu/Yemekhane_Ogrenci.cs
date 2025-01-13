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
    public partial class Yemekhane_Ogrenci : Form
    {
        string connectionString = "Server=DESKTOP-I71FBSL\\SQLEXPRESS;Database=DB_Okul_Otomasyon;Trusted_Connection=True;";

        string query = "SELECT * FROM Menu";

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
        public Yemekhane_Ogrenci()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void Yemekhane_Ogrenci_Load(object sender, EventArgs e)
        {
            VeriYukle();
        }
    }
}
