namespace Business_Management_System
{
    partial class Users
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.pnl_main = new System.Windows.Forms.Panel();
            this.lbl_email = new System.Windows.Forms.Label();
            this.btn_create = new System.Windows.Forms.Button();
            this.gb_account = new System.Windows.Forms.GroupBox();
            this.tb_email = new System.Windows.Forms.TextBox();
            this.cb_auth = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.pnl_main.SuspendLayout();
            this.gb_account.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnl_main
            // 
            this.pnl_main.Controls.Add(this.gb_account);
            this.pnl_main.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnl_main.Location = new System.Drawing.Point(0, 0);
            this.pnl_main.Name = "pnl_main";
            this.pnl_main.Size = new System.Drawing.Size(1228, 751);
            this.pnl_main.TabIndex = 1;
            // 
            // lbl_email
            // 
            this.lbl_email.AutoSize = true;
            this.lbl_email.Location = new System.Drawing.Point(47, 69);
            this.lbl_email.Name = "lbl_email";
            this.lbl_email.Size = new System.Drawing.Size(42, 17);
            this.lbl_email.TabIndex = 2;
            this.lbl_email.Text = "Email";
            // 
            // btn_create
            // 
            this.btn_create.Location = new System.Drawing.Point(332, 194);
            this.btn_create.Name = "btn_create";
            this.btn_create.Size = new System.Drawing.Size(75, 23);
            this.btn_create.TabIndex = 7;
            this.btn_create.Text = "Create";
            this.btn_create.UseVisualStyleBackColor = true;
            this.btn_create.Click += new System.EventHandler(this.btn_create_Click);
            // 
            // gb_account
            // 
            this.gb_account.Controls.Add(this.label1);
            this.gb_account.Controls.Add(this.cb_auth);
            this.gb_account.Controls.Add(this.tb_email);
            this.gb_account.Controls.Add(this.btn_create);
            this.gb_account.Controls.Add(this.lbl_email);
            this.gb_account.Location = new System.Drawing.Point(29, 34);
            this.gb_account.Name = "gb_account";
            this.gb_account.Size = new System.Drawing.Size(485, 244);
            this.gb_account.TabIndex = 8;
            this.gb_account.TabStop = false;
            this.gb_account.Text = "Create An Account";
            // 
            // tb_email
            // 
            this.tb_email.Location = new System.Drawing.Point(122, 66);
            this.tb_email.Name = "tb_email";
            this.tb_email.Size = new System.Drawing.Size(239, 22);
            this.tb_email.TabIndex = 8;
            // 
            // cb_auth
            // 
            this.cb_auth.FormattingEnabled = true;
            this.cb_auth.Items.AddRange(new object[] {
            "admin",
            "common"});
            this.cb_auth.Location = new System.Drawing.Point(122, 129);
            this.cb_auth.Name = "cb_auth";
            this.cb_auth.Size = new System.Drawing.Size(121, 24);
            this.cb_auth.TabIndex = 9;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(47, 129);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(64, 17);
            this.label1.TabIndex = 10;
            this.label1.Text = "Authority";
            // 
            // Users
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.pnl_main);
            this.Name = "Users";
            this.Size = new System.Drawing.Size(1228, 751);
            this.pnl_main.ResumeLayout(false);
            this.gb_account.ResumeLayout(false);
            this.gb_account.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel pnl_main;
        private System.Windows.Forms.GroupBox gb_account;
        private System.Windows.Forms.Button btn_create;
        private System.Windows.Forms.Label lbl_email;
        private System.Windows.Forms.TextBox tb_email;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cb_auth;
    }
}
