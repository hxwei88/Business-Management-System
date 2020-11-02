using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Google.Cloud.Firestore;
using Google.Type;
using System.Collections;
using System.Windows.Forms.DataVisualization.Charting;
using Google.Cloud.Firestore.V1;

namespace Business_Management_System
{
    public partial class Report : UserControl
    {
        FirestoreDb db;
        private SearchBar ctrl_search = new SearchBar();
        private double total_earning;
        private double total_wholesale;
        private List<Order> order_all;
        private static List<Stock> stock_all;
        private bool exe;
        private System.DateTime oldestDate;
        private System.DateTime newestDate;

        public Report()
        {
            InitializeComponent();
        }

        private void Report_Load(object sender, EventArgs e)
        {
            loading();
            order_all = new List<Order>();
            stock_all = new List<Stock>();
            connectDb();
            getDate();
            retrieveStock();
            finishLoad();
        }

        private void connectDb()
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + @"ekia.json";
            Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", path);
            db = FirestoreDb.Create("ekia-da749");
        }

        private async void retrieveStock()
        {
            Query stockque = db.Collection("stock");
            QuerySnapshot snap = await stockque.GetSnapshotAsync();

            foreach (DocumentSnapshot docsnap in snap.Documents)
            {
                Stock stock = docsnap.ConvertTo<Stock>();

                if (docsnap.Exists)
                {
                    stock_all.Add(stock);
                }
            }
        }

        private async Task<bool> retrieveOrder(System.DateTime from, System.DateTime to)
        {
            //need make it dynamic
            System.DateTime startDate = from;
            System.DateTime endDate = to;

            CollectionReference coll = db.Collection("order");

            Query stockque = coll.WhereGreaterThanOrEqualTo("order_date", startDate).WhereLessThanOrEqualTo("order_date", endDate); ;
            QuerySnapshot snap = await stockque.GetSnapshotAsync();

            QuerySnapshot item_snap;

            foreach (DocumentSnapshot docsnap in snap.Documents)
            {
                Order order = docsnap.ConvertTo<Order>();

                if (docsnap.Exists)
                {
                    item_snap = await coll.Document(docsnap.Id).Collection("order_items").GetSnapshotAsync();

                    order.order_items = new List<Order_Items>();

                    foreach (DocumentSnapshot item_docsnap in item_snap.Documents)
                    {
                        Order_Items order_items = item_docsnap.ConvertTo<Order_Items>();

                        if (item_docsnap.Exists)
                        {
                            order.order_items.Add(order_items);
                        }
                    }

                    order_all.Add(order);
                }
            }

            return true;
        }

        //maybe can query with product_id
        private double calculateProfit(bool exe, List<Order> list)
        {
            if (exe)
            {
                double profit = 0;

                foreach (Order order in list)
                {
                    foreach (Order_Items item in order.order_items)
                    {
                        var search = stock_all.Find(x => x.item_id == item.item_id);

                        if (search != null)
                        {
                            total_earning += search.unit_price * item.quantity;
                            total_wholesale += search.wholesale_price * item.quantity;
                            //profit += (search.unit_price - search.wholesale_price) * item.quantity;
                            break;
                        }
                    }
                }

                return total_earning;
            }

            return 0;
        }

        private async void getDate()
        {
            Query coll = db.Collection("order").OrderByDescending("order_date");

            Query oldestQue = coll.Limit(1);
            QuerySnapshot oldestSnap = await oldestQue.GetSnapshotAsync();

            DocumentSnapshot oldestDoc = oldestSnap.Documents[0];
            Order oldestOrder = oldestDoc.ConvertTo<Order>();

            oldestDate = oldestOrder.order_date;

            Query newestQue = coll.LimitToLast(1);
            QuerySnapshot newestSnap = await newestQue.GetSnapshotAsync();

            DocumentSnapshot newestDoc = newestSnap.Documents[0];
            Order newestOrder = newestDoc.ConvertTo<Order>();

            newestDate = newestOrder.order_date;

            for (int year = oldestDate.Year; year <= newestDate.Year; year++)
            {
                System.DateTime from = new System.DateTime(year, 1, 1, 0, 0, 0, DateTimeKind.Utc);
                System.DateTime to = new System.DateTime(year, 12, 31, 0, 0, 0, DateTimeKind.Utc);

                QuerySnapshot docsnap = await coll.WhereGreaterThanOrEqualTo("order_date", from).WhereLessThanOrEqualTo("order_date", to).GetSnapshotAsync();

                if (docsnap.Documents.Count > 0)
                {
                    cb_month_yr.Items.Add(year);
                }
            }
        }

        private async Task<double> calculateDailyCost(int year, int month)
        {
            double totalWholesale = 0;

            CollectionReference coll = db.Collection("invoice");

            Query query = coll.WhereGreaterThanOrEqualTo("invoice_date", new System.DateTime(year, month, 1, 0, 0, 0, DateTimeKind.Utc)).WhereLessThanOrEqualTo("invoice_date", new System.DateTime(year, month, System.DateTime.DaysInMonth(year, month), 0, 0, 0, DateTimeKind.Utc));

            QuerySnapshot snap = await query.GetSnapshotAsync();

            foreach(DocumentSnapshot docsnap in snap.Documents)
            {
                Invoice order = docsnap.ConvertTo<Invoice>();

                QuerySnapshot item_snap = await coll.Document(docsnap.Id).Collection("invoice_items").GetSnapshotAsync();

                foreach(DocumentSnapshot item_docsnap in item_snap.Documents)
                {
                    Invoice_Items invoice_item = item_docsnap.ConvertTo<Invoice_Items>();

                    totalWholesale += invoice_item.quantity * invoice_item.wholesale_price;
                }
            }

            return Math.Round(totalWholesale/System.DateTime.DaysInMonth(year, month), 2);
        }

        private async void populateChartMonth(int year, int month)
        {
            double dailycost = await calculateDailyCost(year, month);

            //monthly sales report
            foreach (System.DateTime date in AllDatesInMonth(year, month))
            {
                var search = order_all.FindAll(x => x.order_date.Day == date.Day && x.order_date.Month == date.Month);

                if (search.Count > 0)
                {
                    cht_statistics.Series["Profit (Daily)"].Points.AddXY(date.Day, calculateProfit(exe, search) - dailycost);
                    //cht_statistics.Series["Profit (Lifetime)"].Points.AddXY();
                }
                else
                {
                    cht_statistics.Series["Profit (Daily)"].Points.AddXY(date.Day, -dailycost);
                    //cht_statistics.Series["Profit (Lifetime)"].Points.AddXY();
                }
            }
        }

        private async void populateChartYear(int year)
        {
            int count = 1;

            for (int month = 1; month <= 12; month++)
            {
                double dailycost = await calculateDailyCost(year, month);

                foreach (System.DateTime date in AllDatesInMonth(year, month))
                {
                    var search = order_all.FindAll(x => x.order_date.Day == date.Day && x.order_date.Month == date.Month);

                    if (search.Count > 0)
                    {
                        cht_statistics.Series["Profit (Daily)"].Points.AddXY(count++, calculateProfit(exe, search) - dailycost);
                        //cht_statistics.Series["Profit (Lifetime)"].Points.AddXY();
                    }
                    else
                    {
                        cht_statistics.Series["Profit (Daily)"].Points.AddXY(count++, -dailycost);
                        //cht_statistics.Series["Profit (Lifetime)"].Points.AddXY();
                    }
                }
            }
        }

        public static IEnumerable<System.DateTime> AllDatesInMonth(int year, int month)
        {
            int days = System.DateTime.DaysInMonth(year, month);
            for (int day = 1; day <= days; day++)
            {
                yield return new System.DateTime(year, month, day);
            }
        }

        string selectedyear;

        private async void cb_month_yr_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cb_month_yr.SelectedItem.ToString() != selectedyear)
            {
                cb_month_mth.Enabled = false;
            }

            if (cb_month_yr.SelectedItem.ToString() != "" && cb_month_yr.SelectedItem.ToString() != selectedyear)
            {
                cb_month_mth.Items.Clear();

                for (int month = 1; month <= 12; month++)
                {
                    System.DateTime from = new System.DateTime(Int32.Parse(cb_month_yr.SelectedItem.ToString()), month, 1, 0, 0, 0, DateTimeKind.Utc);
                    System.DateTime to = from.AddMonths(1).AddDays(-1);

                    QuerySnapshot snap = await db.Collection("order").WhereGreaterThanOrEqualTo("order_date", from).WhereLessThanOrEqualTo("order_date", to).GetSnapshotAsync();

                    if (snap.Documents.Count > 0)
                    {
                        cb_month_mth.Items.Add(month);
                    }
                }

                selectedyear = cb_month_yr.SelectedItem.ToString();

                cb_month_mth.Enabled = true;
            }
        }

        private void cb_range_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (cb_range.SelectedIndex)
            {
                case 0:
                    if (Convert.ToString(cb_month_yr.SelectedItem) != "" && Convert.ToString(cb_month_mth.SelectedItem) != "")
                    {
                        btn_run.Enabled = true;
                    }
                    break;
                case 1:
                    cb_month_mth.Enabled = false;
                    break;
            }
        }

        private async void btn_run_Click(object sender, EventArgs e)
        {
            int year;
            int month;

            loading();

            switch (cb_range.SelectedIndex)
            {
                case 0:
                    year = Int32.Parse(cb_month_yr.Text);
                    month = Int32.Parse(cb_month_mth.Text);

                    cht_statistics.Series["Profit (Daily)"].IsValueShownAsLabel = true;

                    foreach (var series in cht_statistics.Series)
                    {
                        series.Points.Clear();
                    }

                    exe = await retrieveOrder(new System.DateTime(year, month, 1, 0, 0, 0, DateTimeKind.Utc), new System.DateTime(year, month, System.DateTime.DaysInMonth(year, month), 0, 0, 0, DateTimeKind.Utc));
                    populateChartMonth(year, month);
                    break;
                case 1:
                    year = Int32.Parse(cb_month_yr.Text);

                    cht_statistics.Series["Profit (Daily)"].IsValueShownAsLabel = false;

                    foreach (var series in cht_statistics.Series)
                    {
                        series.Points.Clear();
                    }

                    exe = await retrieveOrder(new System.DateTime(year, 1, 1, 0, 0, 0, DateTimeKind.Utc), new System.DateTime(year, 12, 31, 0, 0, 0, DateTimeKind.Utc));
                    populateChartYear(year);
                    break;
            }

            finishLoad();
        }

        private void cb_month_mth_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Convert.ToString(cb_range.SelectedItem) != "")
            {
                btn_run.Enabled = true;
            }
        }

        private void loading()
        {
            pnl_header.Enabled = false;
            pnl_header.Cursor = Cursors.WaitCursor;
            pnl_content.Enabled = false;
            pnl_content.Cursor = Cursors.WaitCursor;
        }

        private void finishLoad()
        {
            pnl_header.Enabled = true;
            pnl_header.Cursor = Cursors.Default;
            pnl_content.Enabled = true;
            pnl_content.Cursor = Cursors.Default;
        }
    }
}
