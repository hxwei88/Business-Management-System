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
        string auth_level;

        public Main(string auth)
        {
            InitializeComponent();
            auth_level = auth;

            //default page
            uc_storage();
        }

        private void uc_sell()
        {
            Sell ctrl_sell = new Sell();

            pnl_main.Controls.Clear();
            ctrl_sell.Dock = DockStyle.Fill;
            pnl_main.Controls.Add(ctrl_sell);
        }

        private void uc_storage()
        {
            Storage ctrl_storage = new Storage(auth_level);

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

        private void uc_database()
        {
            Database ctrl_database = new Database();

            pnl_main.Controls.Clear();
            ctrl_database.Dock = DockStyle.Fill;
            pnl_main.Controls.Add(ctrl_database);
        }

        private void btn_login_Click(object sender, EventArgs e)
        {
            Login login = new Login();
            login.ShowDialog(this);
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

        private void btn_sell_Click(object sender, EventArgs e)
        {
            uc_sell();
        }

        private void btn_database_Click(object sender, EventArgs e)
        {
            uc_database();
        }
    }
}
