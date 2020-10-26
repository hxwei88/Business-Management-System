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
        private Storage ctrl_storage = new Storage();
        private Audit ctrl_audit = new Audit();
        private Report ctrl_report = new Report();
        private Sell ctrl_sell = new Sell();
        private Database ctrl_database = new Database();

        public Main()
        {
            InitializeComponent();

            //default page
            uc_storage();
        }

        private void uc_sell()
        {
            pnl_main.Controls.Clear();
            ctrl_sell.Dock = DockStyle.Fill;
            pnl_main.Controls.Add(ctrl_sell);
        }

        private void uc_storage()
        {
            pnl_main.Controls.Clear();
            ctrl_storage.Dock = DockStyle.Fill;
            pnl_main.Controls.Add(ctrl_storage);
        }

        private void uc_audit()
        {
            pnl_main.Controls.Clear();
            ctrl_audit.Dock = DockStyle.Fill;
            pnl_main.Controls.Add(ctrl_audit);
        }

        private void uc_report()
        {
            pnl_main.Controls.Clear();
            ctrl_report.Dock = DockStyle.Fill;
            pnl_main.Controls.Add(ctrl_report);
        }

        private void uc_database()
        {
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
