using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using Newtonsoft.Json;

namespace OkulOtomasyonu
{
    public partial class Ders_Programi : Form
    {
        private string secilenDosyaYolu = "";
        private string jsonDosyaYolu = Path.Combine(Application.StartupPath, "dersprogramlari.json");
        private string hedefKlasor = Path.Combine(Application.StartupPath, "DersProgramiDosyalari");
        private List<DersProgramiYolu> dosyaBilgileri = new List<DersProgramiYolu>();

        public Ders_Programi()
        {
            InitializeComponent();
            if (!Directory.Exists(hedefKlasor))
            {
                Directory.CreateDirectory(hedefKlasor);
            }

            DosyaBilgileriniYukle();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Title = "Dosya Seç",
                Filter = "Tüm Dosyalar (*.*)|*.*"
            };

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                secilenDosyaYolu = openFileDialog.FileName;
                string dosyaAdi = Path.GetFileName(secilenDosyaYolu);

                button2.Text = dosyaAdi;
                button2.Image = Icon.ExtractAssociatedIcon(secilenDosyaYolu)?.ToBitmap();
                button2.ImageAlign = ContentAlignment.MiddleLeft;
                button2.TextAlign = ContentAlignment.MiddleRight;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(secilenDosyaYolu))
            {
                string dosyaAdi = Path.GetFileName(secilenDosyaYolu);
                string hedefDosyaYolu = Path.Combine(hedefKlasor, dosyaAdi);

                try
                {
                    File.Copy(secilenDosyaYolu, hedefDosyaYolu, true);

                    DersProgramiYolu yeniDosya = new DersProgramiYolu
                    {
                        DosyaAdi = dosyaAdi,
                        DosyaYolu = hedefDosyaYolu,
                        YuklemeTarihi = DateTime.Now.ToString("dd.MM.yyyy HH:mm:ss")
                    };

                    dosyaBilgileri.Add(yeniDosya);
                    DosyaBilgileriniKaydet();

                    listBox1.Items.Add(yeniDosya);

                    MessageBox.Show("Dosya başarıyla yüklendi.");
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Dosya yükleme sırasında bir hata oluştu: {ex.Message}");
                }
            }
            else
            {
                MessageBox.Show("Lütfen önce bir dosya seçin!");
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            secilenDosyaYolu = "";
            button2.Text = "Dosya Ekle";
            button2.Image = null;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedItem != null)
            {
                DersProgramiYolu silinecekDosya = listBox1.SelectedItem as DersProgramiYolu;

                if (silinecekDosya != null)
                {
                    try
                    {
                        if (File.Exists(silinecekDosya.DosyaYolu))
                        {
                            File.Delete(silinecekDosya.DosyaYolu);
                        }

                        dosyaBilgileri.Remove(silinecekDosya);

                        DosyaBilgileriniKaydet();

                        listBox1.Items.Remove(silinecekDosya);

                        MessageBox.Show("Dosya başarıyla silindi.");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Dosya silinirken bir hata oluştu: {ex.Message}");
                    }
                }
                else
                {
                    MessageBox.Show("Seçili dosya JSON listesinde bulunamadı!");
                }
            }
            else
            {
                MessageBox.Show("Lütfen bir dosya seçin!");
            }
        }

        private void DosyaBilgileriniYukle()
        {
            if (File.Exists(jsonDosyaYolu))
            {
                string json = File.ReadAllText(jsonDosyaYolu);
                dosyaBilgileri = JsonConvert.DeserializeObject<List<DersProgramiYolu>>(json) ?? new List<DersProgramiYolu>();

                foreach (var dosya in dosyaBilgileri)
                {
                    listBox1.Items.Add(dosya);
                }
            }
        }

        private void DosyaBilgileriniKaydet()
        {
            string json = JsonConvert.SerializeObject(dosyaBilgileri, Formatting.Indented);
            File.WriteAllText(jsonDosyaYolu, json);
        }

        private void FizikselDosyalariKontrolEt()
        {
            List<DersProgramiYolu> silinecekDosyalar = new List<DersProgramiYolu>();

            foreach (var dosya in dosyaBilgileri)
            {
                if (!File.Exists(dosya.DosyaYolu))
                {
                    silinecekDosyalar.Add(dosya);
                }
            }

            foreach (var dosya in silinecekDosyalar)
            {
                dosyaBilgileri.Remove(dosya);
            }

            if (silinecekDosyalar.Count > 0)
            {
                DosyaBilgileriniKaydet();
                ListBoxGuncelle();
            }
        }

        private void ListBoxGuncelle()
        {
            listBox1.Items.Clear();
            foreach (var dosya in dosyaBilgileri)
            {
                listBox1.Items.Add(dosya);
            }
        }

        private void Ders_Programi_Load(object sender, EventArgs e)
        {
            FizikselDosyalariKontrolEt();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
