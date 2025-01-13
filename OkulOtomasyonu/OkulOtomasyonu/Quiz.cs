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
    public partial class Quiz : Form
    {
        private string hedefKlasor = Path.Combine(Application.StartupPath, "Quizler");
        private string jsonDosyaYolu = Path.Combine(Application.StartupPath, "quizler.json");
        private List<DosyaBilgisi> dosyaBilgileri = new List<DosyaBilgisi>();
        private string secilenDosyaYolu = "";
        public Quiz()
        {
            InitializeComponent();

            if (!Directory.Exists(hedefKlasor))
            {
                Directory.CreateDirectory(hedefKlasor);
            }

            DosyaBilgileriniYukle(); // Uygulama başlarken JSON'dan dosya bilgilerini yükle
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Dispose();
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

                // Dosya adını ve iconu göster
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

                    // Yeni dosya bilgisini oluştur ve listeye ekle
                    DosyaBilgisi yeniDosya = new DosyaBilgisi
                    {
                        DosyaAdi = dosyaAdi,
                        DosyaYolu = hedefDosyaYolu,
                        YuklemeTarihi = DateTime.Now.ToString("dd.MM.yyyy HH:mm:ss")
                    };

                    dosyaBilgileri.Add(yeniDosya);
                    DosyaBilgileriniKaydet();

                    // ListBox'a ekle
                    listBox1.Items.Add($"{yeniDosya.DosyaAdi} - {yeniDosya.YuklemeTarihi}");

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

            // Dosya seçimi temizlenir
            secilenDosyaYolu = "";
            button2.Text = "Dosya Ekle";
            button2.Image = null;
        }

        private void button5_Click(object sender, EventArgs e)
        {

            if (listBox1.SelectedItem != null)
            {
                string secilenKayit = listBox1.SelectedItem.ToString();
                string dosyaAdi = secilenKayit.Split('-')[0].Trim();
                DosyaBilgisi silinecekDosya = dosyaBilgileri.Find(d => d.DosyaAdi == dosyaAdi);

                if (silinecekDosya != null)
                {
                    try
                    {
                        // Dosyayı fiziksel olarak sil
                        if (File.Exists(silinecekDosya.DosyaYolu))
                        {
                            File.Delete(silinecekDosya.DosyaYolu);
                        }

                        // Listeden ve JSON'dan kaldır
                        dosyaBilgileri.Remove(silinecekDosya);
                        DosyaBilgileriniKaydet();

                        // ListBox'tan kaldır
                        listBox1.Items.Remove(secilenKayit);

                        MessageBox.Show("Dosya başarıyla silindi.");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Dosya silinirken bir hata oluştu: {ex.Message}");
                    }
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
                dosyaBilgileri = JsonConvert.DeserializeObject<List<DosyaBilgisi>>(json) ?? new List<DosyaBilgisi>();

                foreach (var dosya in dosyaBilgileri)
                {
                    listBox1.Items.Add($"{dosya.DosyaAdi} - {dosya.YuklemeTarihi}");
                }
            }
        }

        private void DosyaBilgileriniKaydet()
        {
            string json = JsonConvert.SerializeObject(dosyaBilgileri, Formatting.Indented);
            File.WriteAllText(jsonDosyaYolu, json);
        }
    }
}
