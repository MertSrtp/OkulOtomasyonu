using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Net.Mail;

namespace OkulOtomasyonu
{
    public partial class Duyurular : Form
    {
        // Sabit gönderici mail bilgileri (örneğin kurumsal bir e-posta adresi)
        private const string SenderEmail = "saritopmert7@gmail.com"; // Gönderen kurumsal mail adresi
        private const string SenderPassword = "jxby eats dwjv igcd"; // Şifrenizi buraya yazın
        private const string SmtpServer = "smtp.gmail.com"; // SMTP sunucusu (Gmail için)
        private const int SmtpPort = 587; // SMTP portu (Gmail için)
        public Duyurular()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void MailGonder()
        {
            try
            {
                // Formdan bilgileri al
                string recipientEmail = textBox1.Text; // Alıcı Mail kısmı
                string subject = textBox2.Text; // Başlık kısmı
                string body = textBox3.Text; // Mesaj kısmı
                string filePath = textBox4.Text; // Dosya yolu

                // MailMessage oluştur
                MailMessage mail = new MailMessage();
                mail.From = new MailAddress(SenderEmail); // Sabit gönderen mail
                mail.To.Add(recipientEmail); // Alıcı mail adresi
                mail.Subject = subject; // Mail başlığı
                mail.Body = body; // Mail içeriği

                // Dosya ekleme (Eğer dosya yolu boş değilse)
                if (!string.IsNullOrEmpty(filePath) && System.IO.File.Exists(filePath))
                {
                    Attachment attachment = new Attachment(filePath); // Dosyayı ekle
                    mail.Attachments.Add(attachment); // Ek olarak mail'e dahil et
                }

                // SMTP istemcisi ayarları
                SmtpClient smtp = new SmtpClient(SmtpServer, SmtpPort);
                smtp.Credentials = new NetworkCredential(SenderEmail, SenderPassword); // Kimlik doğrulama
                smtp.EnableSsl = true; // SSL'i etkinleştir

                // Mail gönder
                smtp.Send(mail);
                MessageBox.Show("Mail başarıyla gönderildi!", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Mail gönderiminde hata: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
        }

        private void button3_Click(object sender, EventArgs e)
        {
            MailGonder();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // Dosya seçici aç
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                textBox4.Text = openFileDialog.FileName; // Seçilen dosyanın yolunu al
            }
        }
    }
}
