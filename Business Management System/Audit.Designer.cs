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
            this.pnl_main = new System.Windows.Forms.Panel();
            this.pnl_invoices = new System.Windows.Forms.Panel();
            this.tv_invoices = new System.Windows.Forms.TreeView();
            this.pnl_ckbox = new System.Windows.Forms.Panel();
            this.ckb_vendor = new System.Windows.Forms.CheckBox();
            this.flp_option = new System.Windows.Forms.FlowLayoutPanel();
            this.flp_button = new System.Windows.Forms.FlowLayoutPanel();
            this.btn_prevMonth = new System.Windows.Forms.Button();
            this.btn_choosenMonth = new System.Windows.Forms.Button();
            this.tlp_combobox = new System.Windows.Forms.TableLayoutPanel();
            this.lbl_yr = new System.Windows.Forms.Label();
            this.cb_month = new System.Windows.Forms.ComboBox();
            this.cb_yr = new System.Windows.Forms.ComboBox();
            this.lbl_month = new System.Windows.Forms.Label();
            this.pnl_main.SuspendLayout();
            this.pnl_invoices.SuspendLayout();
            this.pnl_ckbox.SuspendLayout();
            this.flp_option.SuspendLayout();
            this.flp_button.SuspendLayout();
            this.tlp_combobox.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnl_main
            // 
            this.pnl_main.Controls.Add(this.pnl_invoices);
            this.pnl_main.Controls.Add(this.pnl_ckbox);
            this.pnl_main.Controls.Add(this.flp_option);
            this.pnl_main.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnl_main.Location = new System.Drawing.Point(0, 0);
            this.pnl_main.Name = "pnl_main";
            this.pnl_main.Size = new System.Drawing.Size(1228, 721);
            this.pnl_main.TabIndex = 0;
            // 
            // pnl_invoices
            // 
            this.pnl_invoices.Controls.Add(this.tv_invoices);
            this.pnl_invoices.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnl_invoices.Enabled = false;
            this.pnl_invoices.Location = new System.Drawing.Point(0, 165);
            this.pnl_invoices.Name = "pnl_invoices";
            this.pnl_invoices.Padding = new System.Windows.Forms.Padding(5);
            this.pnl_invoices.Size = new System.Drawing.Size(1228, 556);
            this.pnl_invoices.TabIndex = 14;
            // 
            // tv_invoices
            // 
            this.tv_invoices.BackColor = System.Drawing.SystemColors.Menu;
            this.tv_invoices.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tv_invoices.CheckBoxes = true;
            this.tv_invoices.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tv_invoices.LineColor = System.Drawing.Color.White;
            this.tv_invoices.Location = new System.Drawing.Point(5, 5);
            this.tv_invoices.Name = "tv_invoices";
            this.tv_invoices.Size = new System.Drawing.Size(1218, 546);
            this.tv_invoices.TabIndex = 13;
            this.tv_invoices.AfterCheck += new System.Windows.Forms.TreeViewEventHandler(this.tv_invoices_AfterCheck);
            // 
            // pnl_ckbox
            // 
            this.pnl_ckbox.AutoSize = true;
            this.pnl_ckbox.Controls.Add(this.ckb_vendor);
            this.pnl_ckbox.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnl_ckbox.Location = new System.Drawing.Point(0, 134);
            this.pnl_ckbox.Name = "pnl_ckbox";
            this.pnl_ckbox.Padding = new System.Windows.Forms.Padding(5);
            this.pnl_ckbox.Size = new System.Drawing.Size(1228, 31);
            this.pnl_ckbox.TabIndex = 9;
            // 
            // ckb_vendor
            // 
            this.ckb_vendor.AutoSize = true;
            this.ckb_vendor.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ckb_vendor.ForeColor = System.Drawing.Color.Black;
            this.ckb_vendor.Location = new System.Drawing.Point(5, 5);
            this.ckb_vendor.Name = "ckb_vendor";
            this.ckb_vendor.Size = new System.Drawing.Size(1218, 21);
            this.ckb_vendor.TabIndex = 15;
            this.ckb_vendor.Text = "Audit on vendor selection";
            this.ckb_vendor.UseVisualStyleBackColor = true;
            this.ckb_vendor.CheckedChanged += new System.EventHandler(this.cb_vendor_CheckedChanged);
            // 
            // flp_option
            // 
            this.flp_option.AutoSize = true;
            this.flp_option.Controls.Add(this.flp_button);
            this.flp_option.Controls.Add(this.tlp_combobox);
            this.flp_option.Dock = System.Windows.Forms.DockStyle.Top;
            this.flp_option.Location = new System.Drawing.Point(0, 0);
            this.flp_option.Name = "flp_option";
            this.flp_option.Padding = new System.Windows.Forms.Padding(5);
            this.flp_option.Size = new System.Drawing.Size(1228, 134);
            this.flp_option.TabIndex = 8;
            // 
            // flp_button
            // 
            this.flp_button.AutoSize = true;
            this.flp_button.Controls.Add(this.btn_prevMonth);
            this.flp_button.Controls.Add(this.btn_choosenMonth);
            this.flp_button.Location = new System.Drawing.Point(8, 8);
            this.flp_button.Name = "flp_button";
            this.flp_button.Size = new System.Drawing.Size(452, 118);
            this.flp_button.TabIndex = 0;
            // 
            // btn_prevMonth
            // 
            this.btn_prevMonth.BackColor = System.Drawing.Color.Gray;
            this.btn_prevMonth.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_prevMonth.Font = new System.Drawing.Font("Impact", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_prevMonth.ForeColor = System.Drawing.Color.White;
            this.btn_prevMonth.Location = new System.Drawing.Point(3, 3);
            this.btn_prevMonth.Name = "btn_prevMonth";
            this.btn_prevMonth.Size = new System.Drawing.Size(220, 112);
            this.btn_prevMonth.TabIndex = 2;
            this.btn_prevMonth.Text = "MONTHLY AUDIT";
            this.btn_prevMonth.UseVisualStyleBackColor = false;
            this.btn_prevMonth.Click += new System.EventHandler(this.btn_prevMonth_Click);
            // 
            // btn_choosenMonth
            // 
            this.btn_choosenMonth.BackColor = System.Drawing.Color.Gray;
            this.btn_choosenMonth.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_choosenMonth.Font = new System.Drawing.Font("Impact", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_choosenMonth.ForeColor = System.Drawing.Color.White;
            this.btn_choosenMonth.Location = new System.Drawing.Point(229, 3);
            this.btn_choosenMonth.Name = "btn_choosenMonth";
            this.btn_choosenMonth.Size = new System.Drawing.Size(220, 112);
            this.btn_choosenMonth.TabIndex = 3;
            this.btn_choosenMonth.Text = "CUSTOM AUDIT";
            this.btn_choosenMonth.UseVisualStyleBackColor = false;
            this.btn_choosenMonth.Click += new System.EventHandler(this.btn_choosenMonth_Click);
            // 
            // tlp_combobox
            // 
            this.tlp_combobox.AutoSize = true;
            this.tlp_combobox.ColumnCount = 2;
            this.tlp_combobox.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 53.31126F));
            this.tlp_combobox.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 46.68874F));
            this.tlp_combobox.Controls.Add(this.lbl_yr, 0, 0);
            this.tlp_combobox.Controls.Add(this.cb_month, 1, 1);
            this.tlp_combobox.Controls.Add(this.cb_yr, 1, 0);
            this.tlp_combobox.Controls.Add(this.lbl_month, 0, 1);
            this.tlp_combobox.Location = new System.Drawing.Point(466, 8);
            this.tlp_combobox.Name = "tlp_combobox";
            this.tlp_combobox.Padding = new System.Windows.Forms.Padding(5);
            this.tlp_combobox.RowCount = 2;
            this.tlp_combobox.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlp_combobox.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlp_combobox.Size = new System.Drawing.Size(314, 114);
            this.tlp_combobox.TabIndex = 1;
            // 
            // lbl_yr
            // 
            this.lbl_yr.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lbl_yr.Font = new System.Drawing.Font("Impact", 19.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_yr.ForeColor = System.Drawing.Color.Black;
            this.lbl_yr.Location = new System.Drawing.Point(8, 5);
            this.lbl_yr.Name = "lbl_yr";
            this.lbl_yr.Size = new System.Drawing.Size(156, 52);
            this.lbl_yr.TabIndex = 2;
            this.lbl_yr.Text = "Year:";
            this.lbl_yr.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // cb_month
            // 
            this.cb_month.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.cb_month.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_month.Enabled = false;
            this.cb_month.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cb_month.FormattingEnabled = true;
            this.cb_month.Location = new System.Drawing.Point(170, 64);
            this.cb_month.Name = "cb_month";
            this.cb_month.Size = new System.Drawing.Size(136, 37);
            this.cb_month.TabIndex = 1;
            // 
            // cb_yr
            // 
            this.cb_yr.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.cb_yr.BackColor = System.Drawing.SystemColors.Window;
            this.cb_yr.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_yr.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cb_yr.FormattingEnabled = true;
            this.cb_yr.Location = new System.Drawing.Point(170, 12);
            this.cb_yr.Name = "cb_yr";
            this.cb_yr.Size = new System.Drawing.Size(136, 37);
            this.cb_yr.TabIndex = 0;
            this.cb_yr.SelectedIndexChanged += new System.EventHandler(this.cb_yr_SelectedIndexChanged);
            // 
            // lbl_month
            // 
            this.lbl_month.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lbl_month.Font = new System.Drawing.Font("Impact", 19.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_month.ForeColor = System.Drawing.Color.Black;
            this.lbl_month.Location = new System.Drawing.Point(8, 57);
            this.lbl_month.Name = "lbl_month";
            this.lbl_month.Size = new System.Drawing.Size(156, 52);
            this.lbl_month.TabIndex = 3;
            this.lbl_month.Text = "Month:";
            this.lbl_month.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Audit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.pnl_main);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Audit";
            this.Size = new System.Drawing.Size(1228, 721);
            this.Load += new System.EventHandler(this.Audit_Load);
            this.pnl_main.ResumeLayout(false);
            this.pnl_main.PerformLayout();
            this.pnl_invoices.ResumeLayout(false);
            this.pnl_ckbox.ResumeLayout(false);
            this.pnl_ckbox.PerformLayout();
            this.flp_option.ResumeLayout(false);
            this.flp_option.PerformLayout();
            this.flp_button.ResumeLayout(false);
            this.tlp_combobox.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnl_main;
        private System.Windows.Forms.Panel pnl_invoices;
        private System.Windows.Forms.TreeView tv_invoices;
        private System.Windows.Forms.Panel pnl_ckbox;
        private System.Windows.Forms.FlowLayoutPanel flp_option;
        private System.Windows.Forms.FlowLayoutPanel flp_button;
        private System.Windows.Forms.Button btn_prevMonth;
        private System.Windows.Forms.Button btn_choosenMonth;
        private System.Windows.Forms.TableLayoutPanel tlp_combobox;
        private System.Windows.Forms.Label lbl_month;
        private System.Windows.Forms.Label lbl_yr;
        private System.Windows.Forms.ComboBox cb_month;
        private System.Windows.Forms.ComboBox cb_yr;
        private System.Windows.Forms.CheckBox ckb_vendor;
    }
}
