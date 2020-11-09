using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Google.Cloud.Firestore;
using System.Net.Mail;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;

namespace Business_Management_System
{
    public partial class Users : UserControl
    {
        FirestoreDb db;

        public Users()
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

        private void btn_create_Click(object sender, EventArgs e)
        {
            if (tb_email.TextLength > 0)
            {
                if (IsValidEmail(tb_email.Text))
                {
                    if (cb_auth.SelectedItem.ToString() != "")
                    {
                        createAccount();
                    }
                    else
                    {
                        MessageBox.Show("Please select authority level for the user.");
                    }
                }
                else
                {
                    MessageBox.Show("Invalid email!");
                }
            }
            else
            {
                MessageBox.Show("Please enter the email!");
            }
        }

        private async void createAccount()
        {
            Query query = db.Collection("user").WhereEqualTo("email", tb_email.Text);
            QuerySnapshot snap = await query.GetSnapshotAsync();

            if(snap.Documents.Count > 0)
            {
                MessageBox.Show("User registered with this email exists!");
            }
            else
            {
                User user = new User();

                Query idQuery = db.Collection("user").OrderByDescending("user_id").Limit(1);
                QuerySnapshot idSnap = await idQuery.GetSnapshotAsync();

                User newestId = idSnap.Documents[0].ConvertTo<User>();

                user.auth_level = cb_auth.SelectedItem.ToString();
                user.email = tb_email.Text;
                user.password = RandomString(10);
                user.user_id = newestId.user_id + 1;
                user.username = "User#" + user.user_id;

                insertUserDb(user);
                sendUserEmail(user);
            }
        }

        private async void insertUserDb(User user)
        {
            CollectionReference coll = db.Collection("user");

            Dictionary<string, object> data = new Dictionary<string, object>()
            {
                {"username", user.username},
                {"user_id", user.user_id},
                {"password", user.password},
                {"auth_level", user.auth_level},
                {"email", user.email}
            };

            await coll.AddAsync(data);
        }

        private void sendUserEmail(User user)
        {
            try
            {
                CertificateValidation();
                MailMessage mail = new MailMessage();
                SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");

                mail.From = new MailAddress("hxwei87@gmail.com");
                mail.To.Add(user.email);
                mail.Subject = "Your account has been created!";
                mail.Body = "Username: " + user.username
                    + "\nPassword: " + user.password;

                SmtpServer.Port = 587;
                SmtpServer.Credentials = new System.Net.NetworkCredential("hxwei87@gmail.com", "Hxwei990703075849");
                SmtpServer.EnableSsl = true;

                SmtpServer.Send(mail);
                MessageBox.Show("An email has been sent to the user.");
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
    }
}
