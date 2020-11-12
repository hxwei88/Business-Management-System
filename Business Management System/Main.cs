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
    public partial class Main : Form
    {
        User user;

        public Main(User u)
        {
            InitializeComponent();
            user = u;

            //default page
            uc_storage();

            if(user.auth_level == "admin")
            {
                btn_users.Show();
            }
        }

        public Account Account
        {
            get => default;
            set
            {
            }
        }

        public Audit Audit
        {
            get => default;
            set
            {
            }
        }

        public Storage Storage
        {
            get => default;
            set
            {
            }
        }

        public Report Report
        {
            get => default;
            set
            {
            }
        }

        public Users Users
        {
            get => default;
            set
            {
            }
        }

        private void uc_storage()
        {
            Storage ctrl_storage = new Storage(user.auth_level);

            pnl_main.Controls.Clear();
            ctrl_storage.Dock = DockStyle.Fill;
            pnl_main.Controls.Add(ctrl_storage);
        }

        private void uc_audit()
        {
            Audit ctrl_audit = new Audit();

            pnl_main.Controls.Clear();
            ctrl_audit.Dock = DockStyle.Fill;
            pnl_main.Controls.Add(ctrl_audit);
        }

        private void uc_report()
        {
            Report ctrl_report = new Report();

            pnl_main.Controls.Clear();
            ctrl_report.Dock = DockStyle.Fill;
            pnl_main.Controls.Add(ctrl_report);
        }

        private void uc_users()
        {
            Users ctrl_users = new Users();

            pnl_main.Controls.Clear();
            ctrl_users.Dock = DockStyle.Fill;
            pnl_main.Controls.Add(ctrl_users);
        }

        private void btn_login_Click(object sender, EventArgs e)
        {
            Account account = new Account(user);
            account.ShowDialog(this);
            if(account.logout)
            {
                Login login = new Login();
                this.Hide();
                login.ShowDialog();
            }
        }

        private void btn_storage_Click(object sender, EventArgs e)
        {
            uc_storage();
        }

        private void btn_audit_Click(object sender, EventArgs e)
        {
            uc_audit();
        }

        private void btn_report_Click(object sender, EventArgs e)
        {
            uc_report();
        }

        private void btn_users_Click(object sender, EventArgs e)
        {
            uc_users();
        }
    }
}
