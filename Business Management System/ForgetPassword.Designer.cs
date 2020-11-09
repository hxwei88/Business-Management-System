namespace Business_Management_System
{
    partial class ForgetPassword
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
            this.txt_user = new System.Windows.Forms.TextBox();
            this.lbl_user = new System.Windows.Forms.Label();
            this.lbl_info = new System.Windows.Forms.Label();
            this.btn_confirm = new System.Windows.Forms.Button();
            this.txt_email = new System.Windows.Forms.TextBox();
            this.lbl_email = new System.Windows.Forms.Label();
            this.pnl_main = new System.Windows.Forms.Panel();
            this.pnl_main.SuspendLayout();
            this.SuspendLayout();
            // 
            // txt_user
            // 
            this.txt_user.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_user.Location = new System.Drawing.Point(205, 89);
            this.txt_user.Margin = new System.Windows.Forms.Padding(4);
            this.txt_user.Name = "txt_user";
            this.txt_user.Size = new System.Drawing.Size(279, 30);
            this.txt_user.TabIndex = 4;
            // 
            // lbl_user
            // 
            this.lbl_user.AutoSize = true;
            this.lbl_user.Font = new System.Drawing.Font("Impact", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_user.ForeColor = System.Drawing.Color.White;
            this.lbl_user.Location = new System.Drawing.Point(25, 76);
            this.lbl_user.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbl_user.Name = "lbl_user";
            this.lbl_user.Size = new System.Drawing.Size(160, 42);
            this.lbl_user.TabIndex = 3;
            this.lbl_user.Text = "USERNAME";
            // 
            // lbl_info
            // 
            this.lbl_info.AutoSize = true;
            this.lbl_info.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_info.ForeColor = System.Drawing.Color.White;
            this.lbl_info.Location = new System.Drawing.Point(29, 33);
            this.lbl_info.Name = "lbl_info";
            this.lbl_info.Size = new System.Drawing.Size(294, 17);
            this.lbl_info.TabIndex = 5;
            this.lbl_info.Text = "Please Enter Your Username and Email";
            // 
            // btn_confirm
            // 
            this.btn_confirm.BackColor = System.Drawing.Color.Gray;
            this.btn_confirm.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btn_confirm.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_confirm.Font = new System.Drawing.Font("Impact", 12F);
            this.btn_confirm.ForeColor = System.Drawing.Color.White;
            this.btn_confirm.Location = new System.Drawing.Point(0, 251);
            this.btn_confirm.Margin = new System.Windows.Forms.Padding(3, 2, 11, 2);
            this.btn_confirm.Name = "btn_confirm";
            this.btn_confirm.Size = new System.Drawing.Size(512, 71);
            this.btn_confirm.TabIndex = 7;
            this.btn_confirm.Text = "CONFIRM";
            this.btn_confirm.UseVisualStyleBackColor = false;
            this.btn_confirm.Click += new System.EventHandler(this.btn_confirm_Click);
            // 
            // txt_email
            // 
            this.txt_email.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_email.Location = new System.Drawing.Point(205, 170);
            this.txt_email.Margin = new System.Windows.Forms.Padding(4);
            this.txt_email.Name = "txt_email";
            this.txt_email.Size = new System.Drawing.Size(279, 30);
            this.txt_email.TabIndex = 9;
            // 
            // lbl_email
            // 
            this.lbl_email.AutoSize = true;
            this.lbl_email.Font = new System.Drawing.Font("Impact", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_email.ForeColor = System.Drawing.Color.White;
            this.lbl_email.Location = new System.Drawing.Point(25, 157);
            this.lbl_email.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbl_email.Name = "lbl_email";
            this.lbl_email.Size = new System.Drawing.Size(96, 42);
            this.lbl_email.TabIndex = 8;
            this.lbl_email.Text = "EMAIL";
            // 
            // pnl_main
            // 
            this.pnl_main.Controls.Add(this.lbl_info);
            this.pnl_main.Controls.Add(this.txt_email);
            this.pnl_main.Controls.Add(this.lbl_user);
            this.pnl_main.Controls.Add(this.lbl_email);
            this.pnl_main.Controls.Add(this.txt_user);
            this.pnl_main.Controls.Add(this.btn_confirm);
            this.pnl_main.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnl_main.Location = new System.Drawing.Point(0, 0);
            this.pnl_main.Name = "pnl_main";
            this.pnl_main.Size = new System.Drawing.Size(512, 322);
            this.pnl_main.TabIndex = 10;
            // 
            // ForgetPassword
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(512, 322);
            this.Controls.Add(this.pnl_main);
            this.MaximizeBox = false;
            this.Name = "ForgetPassword";
            this.Text = "ForgetPassword";
            this.pnl_main.ResumeLayout(false);
            this.pnl_main.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox txt_user;
        private System.Windows.Forms.Label lbl_user;
        private System.Windows.Forms.Label lbl_info;
        private System.Windows.Forms.Button btn_confirm;
        private System.Windows.Forms.TextBox txt_email;
        private System.Windows.Forms.Label lbl_email;
        private System.Windows.Forms.Panel pnl_main;
    }
}