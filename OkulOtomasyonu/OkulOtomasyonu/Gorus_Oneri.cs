using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OkulOtomasyonu
{
    public partial class Gorus_Oneri : Form
    {
        public Gorus_Oneri()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
        private void GorusleriYukle()
        {
            var gorusler = JsonHelper.Oku();
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = gorusler;
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            GorusleriYukle();
        }
    }
}
