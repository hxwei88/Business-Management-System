namespace Business_Management_System
{
    partial class Audit
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
            this.cb_yr = new System.Windows.Forms.ComboBox();
            this.cb_month = new System.Windows.Forms.ComboBox();
            this.btn_prevMonth = new System.Windows.Forms.Button();
            this.btn_choosenMonth = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // cb_yr
            // 
            this.cb_yr.FormattingEnabled = true;
            this.cb_yr.Location = new System.Drawing.Point(507, 32);
            this.cb_yr.Name = "cb_yr";
            this.cb_yr.Size = new System.Drawing.Size(121, 24);
            this.cb_yr.TabIndex = 0;
            this.cb_yr.SelectedIndexChanged += new System.EventHandler(this.cb_yr_SelectedIndexChanged);
            // 
            // cb_month
            // 
            this.cb_month.FormattingEnabled = true;
            this.cb_month.Location = new System.Drawing.Point(689, 32);
            this.cb_month.Name = "cb_month";
            this.cb_month.Size = new System.Drawing.Size(121, 24);
            this.cb_month.TabIndex = 1;
            // 
            // btn_prevMonth
            // 
            this.btn_prevMonth.Location = new System.Drawing.Point(42, 32);
            this.btn_prevMonth.Name = "btn_prevMonth";
            this.btn_prevMonth.Size = new System.Drawing.Size(123, 97);
            this.btn_prevMonth.TabIndex = 2;
            this.btn_prevMonth.Text = "Generate Last Month";
            this.btn_prevMonth.UseVisualStyleBackColor = true;
            this.btn_prevMonth.Click += new System.EventHandler(this.btn_prevMonth_Click);
            // 
            // btn_choosenMonth
            // 
            this.btn_choosenMonth.Location = new System.Drawing.Point(272, 32);
            this.btn_choosenMonth.Name = "btn_choosenMonth";
            this.btn_choosenMonth.Size = new System.Drawing.Size(136, 97);
            this.btn_choosenMonth.TabIndex = 3;
            this.btn_choosenMonth.Text = "Generate Custom Date";
            this.btn_choosenMonth.UseVisualStyleBackColor = true;
            this.btn_choosenMonth.Click += new System.EventHandler(this.btn_choosenMonth_Click);
            // 
            // Audit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.btn_choosenMonth);
            this.Controls.Add(this.btn_prevMonth);
            this.Controls.Add(this.cb_month);
            this.Controls.Add(this.cb_yr);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Audit";
            this.Size = new System.Drawing.Size(1228, 721);
            this.Load += new System.EventHandler(this.Audit_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox cb_yr;
        private System.Windows.Forms.ComboBox cb_month;
        private System.Windows.Forms.Button btn_prevMonth;
        private System.Windows.Forms.Button btn_choosenMonth;
    }
}
