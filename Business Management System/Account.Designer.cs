namespace Business_Management_System
{
    partial class Account
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
            this.pnl_main = new System.Windows.Forms.Panel();
            this.lbl_id = new System.Windows.Forms.Label();
            this.lbl_id_title = new System.Windows.Forms.Label();
            this.lbl_confirm_password = new System.Windows.Forms.Label();
            this.tb_confirm_password = new System.Windows.Forms.TextBox();
            this.btn_logout = new System.Windows.Forms.Button();
            this.lbl_new_password = new System.Windows.Forms.Label();
            this.tb_new_password = new System.Windows.Forms.TextBox();
            this.btn_password = new System.Windows.Forms.Button();
            this.lbl_auth = new System.Windows.Forms.Label();
            this.lbl_auth_title = new System.Windows.Forms.Label();
            this.lbl_name = new System.Windows.Forms.Label();
            this.btn_name = new System.Windows.Forms.Button();
            this.lbl_password = new System.Windows.Forms.Label();
            this.tb_name = new System.Windows.Forms.TextBox();
            this.tb_password = new System.Windows.Forms.TextBox();
            this.pnl_main.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnl_main
            // 
            this.pnl_main.Controls.Add(this.lbl_id);
            this.pnl_main.Controls.Add(this.lbl_id_title);
            this.pnl_main.Controls.Add(this.lbl_confirm_password);
            this.pnl_main.Controls.Add(this.tb_confirm_password);
            this.pnl_main.Controls.Add(this.btn_logout);
            this.pnl_main.Controls.Add(this.lbl_new_password);
            this.pnl_main.Controls.Add(this.tb_new_password);
            this.pnl_main.Controls.Add(this.btn_password);
            this.pnl_main.Controls.Add(this.lbl_auth);
            this.pnl_main.Controls.Add(this.lbl_auth_title);
            this.pnl_main.Controls.Add(this.lbl_name);
            this.pnl_main.Controls.Add(this.btn_name);
            this.pnl_main.Controls.Add(this.lbl_password);
            this.pnl_main.Controls.Add(this.tb_name);
            this.pnl_main.Controls.Add(this.tb_password);
            this.pnl_main.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnl_main.Location = new System.Drawing.Point(0, 0);
            this.pnl_main.Name = "pnl_main";
            this.pnl_main.Size = new System.Drawing.Size(559, 358);
            this.pnl_main.TabIndex = 0;
            // 
            // lbl_id
            // 
            this.lbl_id.AutoSize = true;
            this.lbl_id.ForeColor = System.Drawing.Color.White;
            this.lbl_id.Location = new System.Drawing.Point(183, 70);
            this.lbl_id.Name = "lbl_id";
            this.lbl_id.Size = new System.Drawing.Size(18, 17);
            this.lbl_id.TabIndex = 16;
            this.lbl_id.Text = "U";
            // 
            // lbl_id_title
            // 
            this.lbl_id_title.AutoSize = true;
            this.lbl_id_title.ForeColor = System.Drawing.Color.White;
            this.lbl_id_title.Location = new System.Drawing.Point(128, 70);
            this.lbl_id_title.Name = "lbl_id_title";
            this.lbl_id_title.Size = new System.Drawing.Size(25, 17);
            this.lbl_id_title.TabIndex = 15;
            this.lbl_id_title.Text = "ID:";
            // 
            // lbl_confirm_password
            // 
            this.lbl_confirm_password.AutoSize = true;
            this.lbl_confirm_password.ForeColor = System.Drawing.Color.White;
            this.lbl_confirm_password.Location = new System.Drawing.Point(28, 230);
            this.lbl_confirm_password.Name = "lbl_confirm_password";
            this.lbl_confirm_password.Size = new System.Drawing.Size(125, 17);
            this.lbl_confirm_password.TabIndex = 14;
            this.lbl_confirm_password.Text = "Confirm Password:";
            this.lbl_confirm_password.Visible = false;
            // 
            // tb_confirm_password
            // 
            this.tb_confirm_password.Location = new System.Drawing.Point(186, 230);
            this.tb_confirm_password.Name = "tb_confirm_password";
            this.tb_confirm_password.Size = new System.Drawing.Size(200, 22);
            this.tb_confirm_password.TabIndex = 13;
            this.tb_confirm_password.UseSystemPasswordChar = true;
            this.tb_confirm_password.Visible = false;
            // 
            // btn_logout
            // 
            this.btn_logout.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btn_logout.Location = new System.Drawing.Point(0, 300);
            this.btn_logout.Name = "btn_logout";
            this.btn_logout.Size = new System.Drawing.Size(559, 58);
            this.btn_logout.TabIndex = 12;
            this.btn_logout.Text = "LOGOUT";
            this.btn_logout.UseVisualStyleBackColor = true;
            this.btn_logout.Click += new System.EventHandler(this.btn_logout_Click);
            // 
            // lbl_new_password
            // 
            this.lbl_new_password.AutoSize = true;
            this.lbl_new_password.ForeColor = System.Drawing.Color.White;
            this.lbl_new_password.Location = new System.Drawing.Point(49, 189);
            this.lbl_new_password.Name = "lbl_new_password";
            this.lbl_new_password.Size = new System.Drawing.Size(104, 17);
            this.lbl_new_password.TabIndex = 11;
            this.lbl_new_password.Text = "New Password:";
            this.lbl_new_password.Visible = false;
            // 
            // tb_new_password
            // 
            this.tb_new_password.Location = new System.Drawing.Point(186, 186);
            this.tb_new_password.Name = "tb_new_password";
            this.tb_new_password.Size = new System.Drawing.Size(200, 22);
            this.tb_new_password.TabIndex = 10;
            this.tb_new_password.UseSystemPasswordChar = true;
            this.tb_new_password.Visible = false;
            // 
            // btn_password
            // 
            this.btn_password.Location = new System.Drawing.Point(403, 150);
            this.btn_password.Name = "btn_password";
            this.btn_password.Size = new System.Drawing.Size(75, 23);
            this.btn_password.TabIndex = 9;
            this.btn_password.Text = "Edit";
            this.btn_password.UseVisualStyleBackColor = true;
            this.btn_password.Click += new System.EventHandler(this.btn_password_Click);
            // 
            // lbl_auth
            // 
            this.lbl_auth.AutoSize = true;
            this.lbl_auth.ForeColor = System.Drawing.Color.White;
            this.lbl_auth.Location = new System.Drawing.Point(183, 34);
            this.lbl_auth.Name = "lbl_auth";
            this.lbl_auth.Size = new System.Drawing.Size(47, 17);
            this.lbl_auth.TabIndex = 8;
            this.lbl_auth.Text = "Admin";
            // 
            // lbl_auth_title
            // 
            this.lbl_auth_title.AutoSize = true;
            this.lbl_auth_title.ForeColor = System.Drawing.Color.White;
            this.lbl_auth_title.Location = new System.Drawing.Point(89, 34);
            this.lbl_auth_title.Name = "lbl_auth_title";
            this.lbl_auth_title.Size = new System.Drawing.Size(68, 17);
            this.lbl_auth_title.TabIndex = 7;
            this.lbl_auth_title.Text = "Authority:";
            // 
            // lbl_name
            // 
            this.lbl_name.AutoSize = true;
            this.lbl_name.ForeColor = System.Drawing.Color.White;
            this.lbl_name.Location = new System.Drawing.Point(80, 108);
            this.lbl_name.Name = "lbl_name";
            this.lbl_name.Size = new System.Drawing.Size(77, 17);
            this.lbl_name.TabIndex = 0;
            this.lbl_name.Text = "Username:";
            // 
            // btn_name
            // 
            this.btn_name.Location = new System.Drawing.Point(403, 108);
            this.btn_name.Name = "btn_name";
            this.btn_name.Size = new System.Drawing.Size(75, 23);
            this.btn_name.TabIndex = 6;
            this.btn_name.Text = "Edit";
            this.btn_name.UseVisualStyleBackColor = true;
            this.btn_name.Click += new System.EventHandler(this.btn_name_Click);
            // 
            // lbl_password
            // 
            this.lbl_password.AutoSize = true;
            this.lbl_password.ForeColor = System.Drawing.Color.White;
            this.lbl_password.Location = new System.Drawing.Point(80, 150);
            this.lbl_password.Name = "lbl_password";
            this.lbl_password.Size = new System.Drawing.Size(73, 17);
            this.lbl_password.TabIndex = 1;
            this.lbl_password.Text = "Password:";
            // 
            // tb_name
            // 
            this.tb_name.Enabled = false;
            this.tb_name.Location = new System.Drawing.Point(186, 108);
            this.tb_name.Name = "tb_name";
            this.tb_name.Size = new System.Drawing.Size(200, 22);
            this.tb_name.TabIndex = 2;
            // 
            // tb_password
            // 
            this.tb_password.Enabled = false;
            this.tb_password.Location = new System.Drawing.Point(186, 147);
            this.tb_password.Name = "tb_password";
            this.tb_password.Size = new System.Drawing.Size(200, 22);
            this.tb_password.TabIndex = 3;
            this.tb_password.Text = "******";
            this.tb_password.UseSystemPasswordChar = true;
            // 
            // Account
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(559, 358);
            this.Controls.Add(this.pnl_main);
            this.Name = "Account";
            this.Text = "Account";
            this.pnl_main.ResumeLayout(false);
            this.pnl_main.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnl_main;
        private System.Windows.Forms.Label lbl_password;
        private System.Windows.Forms.Label lbl_name;
        private System.Windows.Forms.Button btn_name;
        private System.Windows.Forms.TextBox tb_password;
        private System.Windows.Forms.TextBox tb_name;
        private System.Windows.Forms.Label lbl_auth;
        private System.Windows.Forms.Label lbl_auth_title;
        private System.Windows.Forms.Button btn_password;
        private System.Windows.Forms.TextBox tb_new_password;
        private System.Windows.Forms.Label lbl_new_password;
        private System.Windows.Forms.Label lbl_confirm_password;
        private System.Windows.Forms.TextBox tb_confirm_password;
        private System.Windows.Forms.Button btn_logout;
        private System.Windows.Forms.Label lbl_id;
        private System.Windows.Forms.Label lbl_id_title;
    }
}