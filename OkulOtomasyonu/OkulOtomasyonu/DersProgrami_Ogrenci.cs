using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using Newtonsoft.Json;

namespace OkulOtomasyonu
{
    public partial class DersProgrami_Ogrenci : Form
    {
        private string jsonDosyaYolu = Path.Combine(Application.StartupPath, "dersprogramlari.json");
        private List<DersProgramiYolu> dersProgramlari = new List<DersProgramiYolu>();

        public DersProgrami_Ogrenci()
        {
            InitializeComponent();
            DersProgramlariniYukle(); // JSON dosyasından ders programlarını yükler
        }

        private void DersProgramlariniYukle()
        {
            if (File.Exists(jsonDosyaYolu))
            {
                string json = File.ReadAllText(jsonDosyaYolu);
                dersProgramlari = JsonConvert.DeserializeObject<List<DersProgramiYolu>>(json) ?? new List<DersProgramiYolu>();

                foreach (var program in dersProgramlari)
                {
                    listBoxControl1.Items.Add(program);
                }
            }
            else
            {
                MessageBox.Show("Ders programları JSON dosyası bulunamadı!");
            }
        }

        private void FizikselDosyalariTemizle()
        {
            List<DersProgramiYolu> eksikDosyalar = new List<DersProgramiYolu>();

            foreach (var dosya in dersProgramlari)
            {
                if (!File.Exists(dosya.DosyaYolu))
                {
                    eksikDosyalar.Add(dosya);
                }
            }

            foreach (var eksik in eksikDosyalar)
            {
                dersProgramlari.Remove(eksik);
            }

            if (eksikDosyalar.Count > 0)
            {
                DosyaBilgileriniKaydet();
                ListBoxGuncelle();
                MessageBox.Show("Eksik dosyalar temizlendi.");
            }
        }

        private void ListBoxGuncelle()
        {
            listBoxControl1.Items.Clear();
            foreach (var dosya in dersProgramlari)
            {
                listBoxControl1.Items.Add(dosya);
            }
        }

        private void DosyaBilgileriniKaydet()
        {
            string json = JsonConvert.SerializeObject(dersProgramlari, Formatting.Indented);
            File.WriteAllText(jsonDosyaYolu, json);
        }

        private void DersProgrami_Ogrenci_Load(object sender, EventArgs e)
        {
            FizikselDosyalariTemizle();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            if (listBoxControl1.SelectedItem != null)
            {
                DersProgramiYolu secilenDosya = listBoxControl1.SelectedItem as DersProgramiYolu;

                if (secilenDosya != null)
                {
                    if (File.Exists(secilenDosya.DosyaYolu))
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
                        MessageBox.Show($"Dosya yolu bulunamadı: {secilenDosya.DosyaYolu}");
                        FizikselDosyalariTemizle();
                    }
                }
                else
                {
                    MessageBox.Show("Seçilen dosya JSON listesinde bulunamadı!");
                }
            }
            else
            {
                MessageBox.Show("Lütfen bir dosya seçin!");
            }
        }
    }
}
