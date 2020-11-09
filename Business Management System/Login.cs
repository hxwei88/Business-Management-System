using Google.Cloud.Firestore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Business_Management_System
{
    public partial class Login : Form
    {
        FirestoreDb db;

        public Login()
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

        private async void btn_login_Click(object sender, EventArgs e)
        {
            if (txt_password.TextLength == 0 || txt_user.TextLength == 0)
            {
                MessageBox.Show("Please enter both the username and password!");
            }
            else
            {
                txt_password.Enabled = false;
                txt_user.Enabled = false;
                btn_login.Enabled = false;
                pnl_main.Cursor = Cursors.WaitCursor;

                Query stockque = db.Collection("user").WhereEqualTo("username", txt_user.Text);
                QuerySnapshot snap = await stockque.GetSnapshotAsync();

                if (snap.Documents.Count <= 0)
                {
                    MessageBox.Show("User does not exist!");
                }
                else
                {
                    User user = snap.Documents[0].ConvertTo<User>();

                    if (user.password == txt_password.Text)
                    {
                        this.Hide();
                        Main menu = new Main(user);
                        menu.ShowDialog(this);
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Incorrect Password!");
                    }
                }
            }

            txt_password.Enabled = true;
            txt_user.Enabled = true;
            btn_login.Enabled = true;
            pnl_main.Cursor = Cursors.Default;
        }

        private void link_forget_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Hide();
            ForgetPassword form = new ForgetPassword();
            form.ShowDialog();
            this.Close();
        }
    }
}
