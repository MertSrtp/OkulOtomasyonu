using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using Newtonsoft.Json;

namespace OkulOtomasyonu
{
    public partial class Quiz_Ogrenci : Form
    {
        private string jsonDosyaYolu = Path.Combine(Application.StartupPath, "quizler.json");
        private List<DosyaBilgisi> quizDosyalari = new List<DosyaBilgisi>();

        public Quiz_Ogrenci()
        {
            InitializeComponent();
            QuizDosyalariniYukle(); // JSON dosyasından quiz dosyalarını yükler
        }

        private void QuizDosyalariniYukle()
        {
            if (File.Exists(jsonDosyaYolu))
            {
                string json = File.ReadAllText(jsonDosyaYolu);
                quizDosyalari = JsonConvert.DeserializeObject<List<DosyaBilgisi>>(json) ?? new List<DosyaBilgisi>();

                foreach (var dosya in quizDosyalari)
                {
                    listBoxControl1.Items.Add($"{dosya.DosyaAdi} - {dosya.YuklemeTarihi}");
                }
            }
            else
            {
                MessageBox.Show("Quiz JSON dosyası bulunamadı!");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            if (listBoxControl1.SelectedItem != null)
            {
                string secilenKayit = listBoxControl1.SelectedItem.ToString();
                string dosyaAdi = secilenKayit.Split('-')[0].Trim();
                DosyaBilgisi secilenDosya = quizDosyalari.Find(d => d.DosyaAdi == dosyaAdi);

                if (secilenDosya != null && File.Exists(secilenDosya.DosyaYolu))
                {
                    SaveFileDialog saveFileDialog = new SaveFileDialog
                    {
                        FileName = secilenDosya.DosyaAdi,
                        Filter = "Tüm Dosyalar (*.*)|*.*"
                    };

                    if (saveFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        try
                        {
                            File.Copy(secilenDosya.DosyaYolu, saveFileDialog.FileName, true);
                            MessageBox.Show("Dosya başarıyla indirildi.");
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show($"Dosya indirme sırasında bir hata oluştu: {ex.Message}");
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Seçilen dosya bulunamadı!");
                }
            }
            else
            {
                MessageBox.Show("Lütfen bir dosya seçin!");
            }
        }
    }
}
