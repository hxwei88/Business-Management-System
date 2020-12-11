using System;
using System.Collections;
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
    public partial class PriceEntryForm : Form
    {
        List<Stock> editStock;

        public PriceEntryForm(List<Stock> stock)
        {
            InitializeComponent();

            editStock = stock;

            for (int i = 0; i < stock.Count; i++)
            {
                dataGridView1.Rows.Add();
                dataGridView1.Rows[i].Cells["item_name"].Value = stock[i].item_name;
                dataGridView1.Rows[i].Cells["item_name"].ReadOnly = true;
                dataGridView1.Rows[i].HeaderCell.Value = i + 1;
            }
        }

        private void btn_cfm_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if(Convert.ToString(row.Cells["item_price"].Value) == "")
                {
                    MessageBox.Show("Please complete every item price!");
                    return;
                }
            }

            for (int i = 0; i < editStock.Count; i++)
            {
                 editStock[i].unit_price = Double.Parse(dataGridView1.Rows[i].Cells["item_price"].Value.ToString()); 
            }

            this.Close();
        }
    }
}
