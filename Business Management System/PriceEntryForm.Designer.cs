namespace Business_Management_System
{
    partial class PriceEntryForm
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
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.item_name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.item_price = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btn_cfm = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.item_name,
            this.item_price});
            this.dataGridView1.Cursor = System.Windows.Forms.Cursors.Default;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(0, 0);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersWidth = 51;
            this.dataGridView1.RowTemplate.Height = 24;
            this.dataGridView1.Size = new System.Drawing.Size(800, 384);
            this.dataGridView1.TabIndex = 0;
            // 
            // item_name
            // 
            this.item_name.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.item_name.HeaderText = "Item Name";
            this.item_name.MinimumWidth = 6;
            this.item_name.Name = "item_name";
            // 
            // item_price
            // 
            this.item_price.HeaderText = "Retail Price";
            this.item_price.MinimumWidth = 6;
            this.item_price.Name = "item_price";
            this.item_price.Width = 150;
            // 
            // btn_cfm
            // 
            this.btn_cfm.BackColor = System.Drawing.Color.Gray;
            this.btn_cfm.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btn_cfm.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_cfm.Font = new System.Drawing.Font("Impact", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_cfm.ForeColor = System.Drawing.Color.White;
            this.btn_cfm.Location = new System.Drawing.Point(0, 384);
            this.btn_cfm.Name = "btn_cfm";
            this.btn_cfm.Size = new System.Drawing.Size(800, 66);
            this.btn_cfm.TabIndex = 3;
            this.btn_cfm.Text = "CONFIRM";
            this.btn_cfm.UseVisualStyleBackColor = false;
            this.btn_cfm.Click += new System.EventHandler(this.btn_cfm_Click);
            // 
            // PriceEntryForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.btn_cfm);
            this.Name = "PriceEntryForm";
            this.Text = "Enter item price";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridViewTextBoxColumn item_name;
        private System.Windows.Forms.DataGridViewTextBoxColumn item_price;
        private System.Windows.Forms.Button btn_cfm;
    }
}