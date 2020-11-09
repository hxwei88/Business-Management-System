using Google.Cloud.Firestore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Business_Management_System
{
    public partial class ForgetPassword : Form
    {
        FirestoreDb db;

        public ForgetPassword()
        {
            InitializeComponent();
            connectDb();
        }

        private void connectDb()
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + @"ekia.json";
            Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", path);
            db = FirestoreDb.Create("ekia-da749");
        }

        private void btn_confirm_Click(object sender, EventArgs e)
        {
            if (txt_email.TextLength > 0 && txt_user.TextLength > 0)
            {
                if (IsValidEmail(txt_email.Text))
                {
                    checkUsername();
                }
                else
                {
                    MessageBox.Show("Invalid Email!");
                }
            }
            else
            {
                MessageBox.Show("Please enter your username and email!");
            }
        }

        private async void checkUsername()
        {
            load();
            Query query = db.Collection("user").WhereEqualTo("username", txt_user.Text).WhereEqualTo("email", txt_email.Text);
            QuerySnapshot snap = await query.GetSnapshotAsync();

            if(snap.Documents.Count > 0)
            {
                changePassword(snap.Documents[0].Id);
            }
            else
            {
                MessageBox.Show("Invalid username or email!");
                finishLoad();
            }
        }

        private async void changePassword(string id)
        {
            string tempPass = RandomString(10);

            Dictionary<string, object> data = new Dictionary<string, object>()
            {
                {"password", tempPass}
            };

            DocumentReference docref = db.Collection("user").Document(id);

            await docref.UpdateAsync(data);

            sendUserEmail(tempPass);
            finishLoad();
            this.Hide();
            Login form = new Login();
            form.ShowDialog();
            this.Close();
        }

        private void sendUserEmail(string password)
        {
            try
            {
                CertificateValidation();
                MailMessage mail = new MailMessage();
                SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");

                mail.From = new MailAddress("hxwei87@gmail.com");
                mail.To.Add(txt_email.Text);
                mail.Subject = "Here's your temporary password!";
                mail.Body = "Please use the password below to login. Do remember to change your password after login."
                    + "\nPassword: " + password;

                SmtpServer.Port = 587;
                SmtpServer.Credentials = new System.Net.NetworkCredential("hxwei87@gmail.com", "Hxwei990703075849");
                SmtpServer.EnableSsl = true;

                SmtpServer.Send(mail);
                MessageBox.Show("An email has been sent to " + txt_email.Text + "! Please check your mailbox.");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private static Random random = new Random();

        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }

        static void CertificateValidation()
        {
            ServicePointManager.ServerCertificateValidationCallback =
                delegate (
                    object s,
                    X509Certificate certificate,
                    X509Chain chain,
                    SslPolicyErrors sslPolicyErrors
                ) {
                    return true;
                };
        }

        private void load()
        {
            pnl_main.Cursor = Cursors.WaitCursor;
            txt_email.Enabled = false;
            txt_user.Enabled = false;
            btn_confirm.Enabled = false;
        }

        private void finishLoad()
        {
            pnl_main.Cursor = Cursors.Default;
            txt_email.Enabled = true;
            txt_user.Enabled = true;
            btn_confirm.Enabled = true;
        }
    }
}
