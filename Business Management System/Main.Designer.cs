namespace Business_Management_System
{
    partial class Main
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.pnl_navi = new System.Windows.Forms.Panel();
            this.pnl_title = new System.Windows.Forms.Panel();
            this.lbl_brand = new System.Windows.Forms.Label();
            this.pnl_main = new System.Windows.Forms.Panel();
            this.btn_users = new System.Windows.Forms.Button();
            this.btn_storage = new System.Windows.Forms.Button();
            this.btn_report = new System.Windows.Forms.Button();
            this.btn_audit = new System.Windows.Forms.Button();
            this.btn_login = new System.Windows.Forms.Button();
            this.pnl_navi.SuspendLayout();
            this.pnl_title.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnl_navi
            // 
            this.pnl_navi.BackColor = System.Drawing.Color.Black;
            this.pnl_navi.Controls.Add(this.btn_users);
            this.pnl_navi.Controls.Add(this.btn_storage);
            this.pnl_navi.Controls.Add(this.btn_report);
            this.pnl_navi.Controls.Add(this.btn_audit);
            this.pnl_navi.Controls.Add(this.btn_login);
            this.pnl_navi.Controls.Add(this.pnl_title);
            this.pnl_navi.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnl_navi.Location = new System.Drawing.Point(0, 0);
            this.pnl_navi.Margin = new System.Windows.Forms.Padding(4);
            this.pnl_navi.Name = "pnl_navi";
            this.pnl_navi.Size = new System.Drawing.Size(267, 751);
            this.pnl_navi.TabIndex = 0;
            // 
            // pnl_title
            // 
            this.pnl_title.BackColor = System.Drawing.Color.Black;
            this.pnl_title.Controls.Add(this.lbl_brand);
            this.pnl_title.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnl_title.Location = new System.Drawing.Point(0, 0);
            this.pnl_title.Margin = new System.Windows.Forms.Padding(4);
            this.pnl_title.Name = "pnl_title";
            this.pnl_title.Size = new System.Drawing.Size(267, 123);
            this.pnl_title.TabIndex = 0;
            // 
            // lbl_brand
            // 
            this.lbl_brand.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbl_brand.Font = new System.Drawing.Font("Gill Sans Ultra Bold", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_brand.ForeColor = System.Drawing.Color.White;
            this.lbl_brand.Location = new System.Drawing.Point(0, 0);
            this.lbl_brand.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbl_brand.Name = "lbl_brand";
            this.lbl_brand.Size = new System.Drawing.Size(267, 123);
            this.lbl_brand.TabIndex = 0;
            this.lbl_brand.Text = "EKIA";
            this.lbl_brand.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pnl_main
            // 
            this.pnl_main.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnl_main.Location = new System.Drawing.Point(267, 0);
            this.pnl_main.Margin = new System.Windows.Forms.Padding(4);
            this.pnl_main.Name = "pnl_main";
            this.pnl_main.Size = new System.Drawing.Size(1228, 751);
            this.pnl_main.TabIndex = 2;
            // 
            // btn_users
            // 
            this.btn_users.BackColor = System.Drawing.Color.Gray;
            this.btn_users.Dock = System.Windows.Forms.DockStyle.Top;
            this.btn_users.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.btn_users.FlatAppearance.BorderSize = 0;
            this.btn_users.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_users.Font = new System.Drawing.Font("Impact", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_users.ForeColor = System.Drawing.Color.White;
            this.btn_users.Image = global::Business_Management_System.Properties.Resources.users_manage;
            this.btn_users.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btn_users.Location = new System.Drawing.Point(0, 267);
            this.btn_users.Margin = new System.Windows.Forms.Padding(4);
            this.btn_users.Name = "btn_users";
            this.btn_users.Padding = new System.Windows.Forms.Padding(33, 0, 0, 0);
            this.btn_users.Size = new System.Drawing.Size(267, 48);
            this.btn_users.TabIndex = 5;
            this.btn_users.Text = "USERS";
            this.btn_users.UseVisualStyleBackColor = false;
            this.btn_users.Visible = false;
            this.btn_users.Click += new System.EventHandler(this.btn_users_Click);
            // 
            // btn_storage
            // 
            this.btn_storage.BackColor = System.Drawing.Color.Gray;
            this.btn_storage.Dock = System.Windows.Forms.DockStyle.Top;
            this.btn_storage.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.btn_storage.FlatAppearance.BorderSize = 0;
            this.btn_storage.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_storage.Font = new System.Drawing.Font("Impact", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_storage.ForeColor = System.Drawing.Color.White;
            this.btn_storage.Image = global::Business_Management_System.Properties.Resources.storage;
            this.btn_storage.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btn_storage.Location = new System.Drawing.Point(0, 219);
            this.btn_storage.Margin = new System.Windows.Forms.Padding(4);
            this.btn_storage.Name = "btn_storage";
            this.btn_storage.Padding = new System.Windows.Forms.Padding(33, 0, 0, 0);
            this.btn_storage.Size = new System.Drawing.Size(267, 48);
            this.btn_storage.TabIndex = 1;
            this.btn_storage.Text = "STORAGE";
            this.btn_storage.UseVisualStyleBackColor = false;
            this.btn_storage.Click += new System.EventHandler(this.btn_storage_Click);
            // 
            // btn_report
            // 
            this.btn_report.BackColor = System.Drawing.Color.Gray;
            this.btn_report.Dock = System.Windows.Forms.DockStyle.Top;
            this.btn_report.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.btn_report.FlatAppearance.BorderSize = 0;
            this.btn_report.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_report.Font = new System.Drawing.Font("Impact", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_report.ForeColor = System.Drawing.Color.White;
            this.btn_report.Image = global::Business_Management_System.Properties.Resources.report;
            this.btn_report.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btn_report.Location = new System.Drawing.Point(0, 171);
            this.btn_report.Margin = new System.Windows.Forms.Padding(4);
            this.btn_report.Name = "btn_report";
            this.btn_report.Padding = new System.Windows.Forms.Padding(33, 0, 0, 0);
            this.btn_report.Size = new System.Drawing.Size(267, 48);
            this.btn_report.TabIndex = 3;
            this.btn_report.Text = "REPORT";
            this.btn_report.UseVisualStyleBackColor = false;
            this.btn_report.Click += new System.EventHandler(this.btn_report_Click);
            // 
            // btn_audit
            // 
            this.btn_audit.BackColor = System.Drawing.Color.Gray;
            this.btn_audit.Dock = System.Windows.Forms.DockStyle.Top;
            this.btn_audit.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.btn_audit.FlatAppearance.BorderSize = 0;
            this.btn_audit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_audit.Font = new System.Drawing.Font("Impact", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_audit.ForeColor = System.Drawing.Color.White;
            this.btn_audit.Image = global::Business_Management_System.Properties.Resources.audit;
            this.btn_audit.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btn_audit.Location = new System.Drawing.Point(0, 123);
            this.btn_audit.Margin = new System.Windows.Forms.Padding(4);
            this.btn_audit.Name = "btn_audit";
            this.btn_audit.Padding = new System.Windows.Forms.Padding(33, 0, 0, 0);
            this.btn_audit.Size = new System.Drawing.Size(267, 48);
            this.btn_audit.TabIndex = 2;
            this.btn_audit.Text = "AUDIT";
            this.btn_audit.UseVisualStyleBackColor = false;
            this.btn_audit.Click += new System.EventHandler(this.btn_audit_Click);
            // 
            // btn_login
            // 
            this.btn_login.BackColor = System.Drawing.Color.Gray;
            this.btn_login.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btn_login.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.btn_login.FlatAppearance.BorderSize = 0;
            this.btn_login.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_login.Font = new System.Drawing.Font("Impact", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_login.ForeColor = System.Drawing.Color.White;
            this.btn_login.Image = global::Business_Management_System.Properties.Resources.user;
            this.btn_login.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btn_login.Location = new System.Drawing.Point(0, 703);
            this.btn_login.Margin = new System.Windows.Forms.Padding(4);
            this.btn_login.Name = "btn_login";
            this.btn_login.Padding = new System.Windows.Forms.Padding(33, 0, 0, 0);
            this.btn_login.Size = new System.Drawing.Size(267, 48);
            this.btn_login.TabIndex = 4;
            this.btn_login.Text = "ACCOUNT";
            this.btn_login.UseVisualStyleBackColor = false;
            this.btn_login.Click += new System.EventHandler(this.btn_login_Click);
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1495, 751);
            this.Controls.Add(this.pnl_main);
            this.Controls.Add(this.pnl_navi);
            this.ForeColor = System.Drawing.Color.DarkRed;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Main";
            this.Text = "Business Management System";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.pnl_navi.ResumeLayout(false);
            this.pnl_title.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnl_navi;
        private System.Windows.Forms.Panel pnl_title;
        private System.Windows.Forms.Button btn_storage;
        private System.Windows.Forms.Panel pnl_main;
        private System.Windows.Forms.Label lbl_brand;
        private System.Windows.Forms.Button btn_report;
        private System.Windows.Forms.Button btn_audit;
        private System.Windows.Forms.Button btn_login;
        private System.Windows.Forms.Button btn_users;
    }
}