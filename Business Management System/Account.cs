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
    public partial class Account : Form
    {
        FirestoreDb db;
        public bool logout;
        private User user;

        public Account(User u)
        {
            InitializeComponent();
            connectDb();
            user = u;
            lbl_id.Text = "U" + user.user_id;
            lbl_auth.Text = user.auth_level;
            tb_name.Text = user.username;
        }

        private void connectDb()
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + @"ekia.json";
            Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", path);
            db = FirestoreDb.Create("ekia-da749");
        }

        private void btn_name_Click(object sender, EventArgs e)
        {
            if(btn_name.Text == "Edit")
            {
                btn_name.Text = "Save";
                tb_name.Enabled = true;
                tb_name.Focus();
            }
            else
            {
                updateName();
            }
        }

        private async void updateName()
        {
            load();
            Query namequery = db.Collection("user").WhereEqualTo("username", tb_name.Text);
            QuerySnapshot namesnap = await namequery.GetSnapshotAsync();

            if (namesnap.Documents.Count == 0)
            {
                Query query = db.Collection("user").WhereEqualTo("user_id", Int32.Parse(lbl_id.Text.Substring(1)));
                QuerySnapshot snap = await query.GetSnapshotAsync();

                string id = snap.Documents[0].Id;

                DocumentReference docref = db.Collection("user").Document(id);

                Dictionary<string, object> data = new Dictionary<string, object>()
                {
                    {"username", tb_name.Text}
                };

                await docref.UpdateAsync(data);

                MessageBox.Show("Username Updated!");

                btn_name.Text = "Edit";
                tb_name.Enabled = false;
            }
            else
            {
                MessageBox.Show("Username Taken!");
            }
            finishLoad();
        }

        private async void updatePassword()
        {
            load();
            if(tb_password.TextLength == 0 || tb_new_password.TextLength == 0 || tb_confirm_password.TextLength == 0)
            {
                MessageBox.Show("Please complete the old, new and confirm password!");
            }
            else
            {
                if(tb_password.Text != user.password)
                {
                    MessageBox.Show("Incorrect old password!");
                }
                else
                {
                    if(tb_confirm_password.Text != tb_new_password.Text)
                    {
                        MessageBox.Show("New password does not match with confirmed password!");
                    }
                    else
                    {
                        Query query = db.Collection("user").WhereEqualTo("user_id", Int32.Parse(lbl_id.Text.Substring(1)));
                        QuerySnapshot snap = await query.GetSnapshotAsync();

                        string id = snap.Documents[0].Id;

                        DocumentReference docref = db.Collection("user").Document(id);

                        Dictionary<string, object> data = new Dictionary<string, object>()
                        {
                            {"password", tb_new_password.Text}
                        };

                        await docref.UpdateAsync(data);

                        MessageBox.Show("Password Updated!");

                        btn_password.Text = "Edit";
                        tb_password.Enabled = false;
                        tb_password.Text = "******";
                        lbl_new_password.Hide();
                        lbl_confirm_password.Hide();
                        tb_new_password.Hide();
                        tb_confirm_password.Hide();
                    }
                }
            }
            finishLoad();
        }

        private void btn_password_Click(object sender, EventArgs e)
        {
            if (btn_password.Text == "Edit")
            {
                btn_password.Text = "Save";
                tb_password.Text = "";
                tb_password.Enabled = true;
                lbl_new_password.Show();
                lbl_confirm_password.Show();
                tb_new_password.Show();
                tb_confirm_password.Show();
                tb_password.Focus();
            }
            else
            {
                updatePassword();
            }
        }

        private void btn_logout_Click(object sender, EventArgs e)
        {
            logout = true;
            this.Close();
        }

        private void load()
        {
            pnl_main.Cursor = Cursors.WaitCursor;
            btn_logout.Enabled = false;
            btn_name.Enabled = false;
            btn_password.Enabled = false;
            tb_name.Enabled = false;
            tb_password.Enabled = false;
            tb_new_password.Enabled = false;
            tb_confirm_password.Enabled = false;
        }

        private void finishLoad()
        {
            pnl_main.Cursor = Cursors.Default;
            btn_logout.Enabled = true;
            btn_name.Enabled = true;
            btn_password.Enabled = true;
            tb_name.Enabled = true;
            tb_password.Enabled = true;
            tb_new_password.Enabled = true;
            tb_confirm_password.Enabled = true;
        }
    }
}
