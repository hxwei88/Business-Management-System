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
            order_all = new List<Order>();
            stock_all = new List<Stock>();
            connectDb();
            getDate();
            retrieveStock();
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
                            profit += (search.unit_price - search.wholesale_price) * item.quantity;
                            break;
                        }
                    }
                }

                return profit;
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
                cb_month_yr.Items.Add(year);
            }

            for (int month = 1; month <= 12; month++)
            {
                cb_month_mth.Items.Add(month);
            }
        }

        private void populateChartMonth(int year, int month)
        {
            //monthly sales report
            foreach (System.DateTime date in AllDatesInMonth(year, month))
            {
                var search = order_all.FindAll(x => x.order_date.Day == date.Day && x.order_date.Month == date.Month);

                if (search.Count > 0)
                {
                    cht_statistics.Series["Profit"].Points.AddXY(date.Day, calculateProfit(exe, search));
                }
                else
                {
                    cht_statistics.Series["Profit"].Points.AddXY(date.Day, 0);
                }
            }
        }

        private void populateChartYear(int year)
        {
            int count = 1;

            for (int month = 1; month <= 12; month++)
            {
                foreach (System.DateTime date in AllDatesInMonth(year, month))
                {
                    var search = order_all.FindAll(x => x.order_date.Day == date.Day && x.order_date.Month == date.Month);

                    if (search.Count > 0)
                    {
                        cht_statistics.Series["Profit"].Points.AddXY(count++, calculateProfit(exe, search));
                    }
                    else
                    {
                        cht_statistics.Series["Profit"].Points.AddXY(count++, 0);
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

        private async void btn_month_Click(object sender, EventArgs e)
        {
            int year = Int32.Parse(cb_month_yr.Text);
            int month = Int32.Parse(cb_month_mth.Text);

            foreach (var series in cht_statistics.Series)
            {
                series.Points.Clear();
            }

            exe = await retrieveOrder(new System.DateTime(year, month, 1, 0, 0, 0, DateTimeKind.Utc), new System.DateTime(year, month, System.DateTime.DaysInMonth(year, month), 0, 0, 0, DateTimeKind.Utc));
            populateChartMonth(year, month);
        }

        private async void btn_year_Click(object sender, EventArgs e)
        {
            int year = Int32.Parse(cb_month_yr.Text);

            foreach (var series in cht_statistics.Series)
            {
                series.Points.Clear();
            }

            exe = await retrieveOrder(new System.DateTime(year, 1, 1, 0, 0, 0, DateTimeKind.Utc), new System.DateTime(year, 12, 31, 0, 0, 0, DateTimeKind.Utc));
            populateChartYear(year);
        }

        private void cb_month_yr_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(cb_month_yr.SelectedItem.ToString() != "")
            {
                cb_month_mth.Enabled = true;
            }
            else
            {
                cb_month_mth.Enabled = false;
            }
        }
    }
}
