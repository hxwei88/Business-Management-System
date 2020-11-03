namespace Business_Management_System
{
    partial class Report
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea5 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend5 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series9 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series10 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.pnl_header = new System.Windows.Forms.Panel();
            this.cb_month_mth = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cb_month_yr = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btn_run = new System.Windows.Forms.Button();
            this.cb_range = new System.Windows.Forms.ComboBox();
            this.pnl_content = new System.Windows.Forms.Panel();
            this.cht_statistics = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.pnl_main = new System.Windows.Forms.Panel();
            this.pnl_header.SuspendLayout();
            this.pnl_content.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cht_statistics)).BeginInit();
            this.pnl_main.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnl_header
            // 
            this.pnl_header.Controls.Add(this.cb_month_mth);
            this.pnl_header.Controls.Add(this.label2);
            this.pnl_header.Controls.Add(this.cb_month_yr);
            this.pnl_header.Controls.Add(this.label1);
            this.pnl_header.Controls.Add(this.btn_run);
            this.pnl_header.Controls.Add(this.cb_range);
            this.pnl_header.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnl_header.Location = new System.Drawing.Point(0, 0);
            this.pnl_header.Margin = new System.Windows.Forms.Padding(4);
            this.pnl_header.Name = "pnl_header";
            this.pnl_header.Padding = new System.Windows.Forms.Padding(10);
            this.pnl_header.Size = new System.Drawing.Size(1228, 63);
            this.pnl_header.TabIndex = 0;
            // 
            // cb_month_mth
            // 
            this.cb_month_mth.Dock = System.Windows.Forms.DockStyle.Left;
            this.cb_month_mth.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_month_mth.Enabled = false;
            this.cb_month_mth.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cb_month_mth.FormattingEnabled = true;
            this.cb_month_mth.Location = new System.Drawing.Point(715, 10);
            this.cb_month_mth.Margin = new System.Windows.Forms.Padding(4);
            this.cb_month_mth.Name = "cb_month_mth";
            this.cb_month_mth.Size = new System.Drawing.Size(160, 44);
            this.cb_month_mth.TabIndex = 1;
            this.cb_month_mth.SelectedIndexChanged += new System.EventHandler(this.cb_month_mth_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Dock = System.Windows.Forms.DockStyle.Left;
            this.label2.Font = new System.Drawing.Font("Impact", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(604, 10);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.label2.Size = new System.Drawing.Size(111, 37);
            this.label2.TabIndex = 5;
            this.label2.Text = "Month:";
            // 
            // cb_month_yr
            // 
            this.cb_month_yr.Dock = System.Windows.Forms.DockStyle.Left;
            this.cb_month_yr.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_month_yr.Enabled = false;
            this.cb_month_yr.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cb_month_yr.FormattingEnabled = true;
            this.cb_month_yr.Location = new System.Drawing.Point(444, 10);
            this.cb_month_yr.Margin = new System.Windows.Forms.Padding(4);
            this.cb_month_yr.Name = "cb_month_yr";
            this.cb_month_yr.Size = new System.Drawing.Size(160, 44);
            this.cb_month_yr.TabIndex = 0;
            this.cb_month_yr.SelectedIndexChanged += new System.EventHandler(this.cb_month_yr_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Left;
            this.label1.Font = new System.Drawing.Font("Impact", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(357, 10);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.label1.Size = new System.Drawing.Size(87, 37);
            this.label1.TabIndex = 4;
            this.label1.Text = "Year:";
            // 
            // btn_run
            // 
            this.btn_run.BackColor = System.Drawing.Color.Gray;
            this.btn_run.Dock = System.Windows.Forms.DockStyle.Right;
            this.btn_run.Enabled = false;
            this.btn_run.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_run.Font = new System.Drawing.Font("Impact", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_run.ForeColor = System.Drawing.Color.White;
            this.btn_run.Location = new System.Drawing.Point(1118, 10);
            this.btn_run.Margin = new System.Windows.Forms.Padding(4);
            this.btn_run.Name = "btn_run";
            this.btn_run.Size = new System.Drawing.Size(100, 43);
            this.btn_run.TabIndex = 11;
            this.btn_run.Text = "RUN";
            this.btn_run.UseVisualStyleBackColor = false;
            this.btn_run.Click += new System.EventHandler(this.btn_run_Click);
            // 
            // cb_range
            // 
            this.cb_range.Dock = System.Windows.Forms.DockStyle.Left;
            this.cb_range.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_range.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cb_range.FormattingEnabled = true;
            this.cb_range.Items.AddRange(new object[] {
            "Monthly Report",
            "Yearly Report"});
            this.cb_range.Location = new System.Drawing.Point(10, 10);
            this.cb_range.Margin = new System.Windows.Forms.Padding(4);
            this.cb_range.Name = "cb_range";
            this.cb_range.Size = new System.Drawing.Size(347, 44);
            this.cb_range.TabIndex = 10;
            this.cb_range.SelectedIndexChanged += new System.EventHandler(this.cb_range_SelectedIndexChanged);
            // 
            // pnl_content
            // 
            this.pnl_content.Controls.Add(this.pnl_main);
            this.pnl_content.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnl_content.Location = new System.Drawing.Point(0, 0);
            this.pnl_content.Margin = new System.Windows.Forms.Padding(4);
            this.pnl_content.Name = "pnl_content";
            this.pnl_content.Size = new System.Drawing.Size(1228, 721);
            this.pnl_content.TabIndex = 1;
            // 
            // cht_statistics
            // 
            chartArea5.Name = "ChartArea1";
            this.cht_statistics.ChartAreas.Add(chartArea5);
            this.cht_statistics.Dock = System.Windows.Forms.DockStyle.Fill;
            legend5.Font = new System.Drawing.Font("Impact", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            legend5.IsTextAutoFit = false;
            legend5.Name = "Legend1";
            this.cht_statistics.Legends.Add(legend5);
            this.cht_statistics.Location = new System.Drawing.Point(0, 63);
            this.cht_statistics.Margin = new System.Windows.Forms.Padding(4);
            this.cht_statistics.Name = "cht_statistics";
            series9.ChartArea = "ChartArea1";
            series9.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
            series9.IsValueShownAsLabel = true;
            series9.Legend = "Legend1";
            series9.MarkerStyle = System.Windows.Forms.DataVisualization.Charting.MarkerStyle.Circle;
            series9.Name = "Profit (Daily)";
            series10.ChartArea = "ChartArea1";
            series10.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
            series10.Legend = "Legend1";
            series10.Name = "Profit (Lifetime)";
            this.cht_statistics.Series.Add(series9);
            this.cht_statistics.Series.Add(series10);
            this.cht_statistics.Size = new System.Drawing.Size(1228, 658);
            this.cht_statistics.TabIndex = 0;
            this.cht_statistics.Text = "chart1";
            // 
            // pnl_main
            // 
            this.pnl_main.Controls.Add(this.cht_statistics);
            this.pnl_main.Controls.Add(this.pnl_header);
            this.pnl_main.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnl_main.Location = new System.Drawing.Point(0, 0);
            this.pnl_main.Name = "pnl_main";
            this.pnl_main.Size = new System.Drawing.Size(1228, 721);
            this.pnl_main.TabIndex = 1;
            // 
            // Report
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.pnl_content);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Report";
            this.Size = new System.Drawing.Size(1228, 721);
            this.Load += new System.EventHandler(this.Report_Load);
            this.pnl_header.ResumeLayout(false);
            this.pnl_header.PerformLayout();
            this.pnl_content.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.cht_statistics)).EndInit();
            this.pnl_main.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnl_header;
        private System.Windows.Forms.Panel pnl_content;
        private System.Windows.Forms.DataVisualization.Charting.Chart cht_statistics;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cb_month_mth;
        private System.Windows.Forms.ComboBox cb_month_yr;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cb_range;
        private System.Windows.Forms.Button btn_run;
        private System.Windows.Forms.Panel pnl_main;
    }
}
