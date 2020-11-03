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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Title title1 = new System.Windows.Forms.DataVisualization.Charting.Title();
            this.pnl_header = new System.Windows.Forms.Panel();
            this.btn_run = new System.Windows.Forms.Button();
            this.cb_range = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.cb_month_mth = new System.Windows.Forms.ComboBox();
            this.cb_month_yr = new System.Windows.Forms.ComboBox();
            this.pnl_content = new System.Windows.Forms.Panel();
            this.cht_statistics = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.pnl_header.SuspendLayout();
            this.pnl_content.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cht_statistics)).BeginInit();
            this.SuspendLayout();
            // 
            // pnl_header
            // 
            this.pnl_header.AutoSize = true;
            this.pnl_header.Controls.Add(this.btn_run);
            this.pnl_header.Controls.Add(this.cb_range);
            this.pnl_header.Controls.Add(this.label2);
            this.pnl_header.Controls.Add(this.label1);
            this.pnl_header.Controls.Add(this.cb_month_mth);
            this.pnl_header.Controls.Add(this.cb_month_yr);
            this.pnl_header.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnl_header.Location = new System.Drawing.Point(0, 0);
            this.pnl_header.Margin = new System.Windows.Forms.Padding(4);
            this.pnl_header.Name = "pnl_header";
            this.pnl_header.Size = new System.Drawing.Size(1228, 50);
            this.pnl_header.TabIndex = 0;
            // 
            // btn_run
            // 
            this.btn_run.Enabled = false;
            this.btn_run.Location = new System.Drawing.Point(1128, 0);
            this.btn_run.Margin = new System.Windows.Forms.Padding(4);
            this.btn_run.Name = "btn_run";
            this.btn_run.Size = new System.Drawing.Size(100, 46);
            this.btn_run.TabIndex = 11;
            this.btn_run.Text = "RUN";
            this.btn_run.UseVisualStyleBackColor = true;
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
            this.cb_range.Location = new System.Drawing.Point(0, 0);
            this.cb_range.Margin = new System.Windows.Forms.Padding(4);
            this.cb_range.Name = "cb_range";
            this.cb_range.Size = new System.Drawing.Size(347, 44);
            this.cb_range.TabIndex = 10;
            this.cb_range.SelectedIndexChanged += new System.EventHandler(this.cb_range_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(627, 5);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(107, 36);
            this.label2.TabIndex = 5;
            this.label2.Text = "Month:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(356, 4);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(85, 36);
            this.label1.TabIndex = 4;
            this.label1.Text = "Year:";
            // 
            // cb_month_mth
            // 
            this.cb_month_mth.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_month_mth.Enabled = false;
            this.cb_month_mth.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cb_month_mth.FormattingEnabled = true;
            this.cb_month_mth.Location = new System.Drawing.Point(748, 1);
            this.cb_month_mth.Margin = new System.Windows.Forms.Padding(4);
            this.cb_month_mth.Name = "cb_month_mth";
            this.cb_month_mth.Size = new System.Drawing.Size(160, 44);
            this.cb_month_mth.TabIndex = 1;
            this.cb_month_mth.SelectedIndexChanged += new System.EventHandler(this.cb_month_mth_SelectedIndexChanged);
            // 
            // cb_month_yr
            // 
            this.cb_month_yr.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_month_yr.Enabled = false;
            this.cb_month_yr.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cb_month_yr.FormattingEnabled = true;
            this.cb_month_yr.Location = new System.Drawing.Point(457, 1);
            this.cb_month_yr.Margin = new System.Windows.Forms.Padding(4);
            this.cb_month_yr.Name = "cb_month_yr";
            this.cb_month_yr.Size = new System.Drawing.Size(160, 44);
            this.cb_month_yr.TabIndex = 0;
            this.cb_month_yr.SelectedIndexChanged += new System.EventHandler(this.cb_month_yr_SelectedIndexChanged);
            // 
            // pnl_content
            // 
            this.pnl_content.Controls.Add(this.cht_statistics);
            this.pnl_content.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnl_content.Location = new System.Drawing.Point(0, 50);
            this.pnl_content.Margin = new System.Windows.Forms.Padding(4);
            this.pnl_content.Name = "pnl_content";
            this.pnl_content.Size = new System.Drawing.Size(1228, 671);
            this.pnl_content.TabIndex = 1;
            // 
            // cht_statistics
            // 
            chartArea1.Name = "ChartArea1";
            this.cht_statistics.ChartAreas.Add(chartArea1);
            this.cht_statistics.Dock = System.Windows.Forms.DockStyle.Fill;
            legend1.Font = new System.Drawing.Font("Impact", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            legend1.IsTextAutoFit = false;
            legend1.Name = "Legend1";
            this.cht_statistics.Legends.Add(legend1);
            this.cht_statistics.Location = new System.Drawing.Point(0, 0);
            this.cht_statistics.Margin = new System.Windows.Forms.Padding(4);
            this.cht_statistics.Name = "cht_statistics";
            series1.ChartArea = "ChartArea1";
            series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series1.IsValueShownAsLabel = true;
            series1.Legend = "Legend1";
            series1.MarkerStyle = System.Windows.Forms.DataVisualization.Charting.MarkerStyle.Circle;
            series1.Name = "Profit (Daily)";
            series2.ChartArea = "ChartArea1";
            series2.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series2.Legend = "Legend1";
            series2.Name = "Profit (Lifetime)";
            this.cht_statistics.Series.Add(series1);
            this.cht_statistics.Series.Add(series2);
            this.cht_statistics.Size = new System.Drawing.Size(1228, 671);
            this.cht_statistics.TabIndex = 0;
            this.cht_statistics.Text = "chart1";
            title1.Font = new System.Drawing.Font("Impact", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            title1.Name = "monthlySales";
            title1.Text = "Monthly Sales";
            this.cht_statistics.Titles.Add(title1);
            // 
            // Report
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.pnl_content);
            this.Controls.Add(this.pnl_header);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Report";
            this.Size = new System.Drawing.Size(1228, 721);
            this.Load += new System.EventHandler(this.Report_Load);
            this.pnl_header.ResumeLayout(false);
            this.pnl_header.PerformLayout();
            this.pnl_content.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.cht_statistics)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

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
    }
}
