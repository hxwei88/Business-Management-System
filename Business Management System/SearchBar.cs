using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Business_Management_System
{
    public partial class SearchBar : UserControl
    {
        public SearchBar()
        {
            InitializeComponent();
        }

        private void txt_search_Enter(object sender, EventArgs e)
        {
            txt_search.ForeColor = Color.Gray;

            if (txt_search.Text == "Search Something...")
                txt_search.Text = "";
        }

        private void txt_search_Leave(object sender, EventArgs e)
        {
            if (txt_search.Text == "")
                txt_search.Text = "Search Something...";

            txt_search.ForeColor = Color.Silver;
        }
    }
}
