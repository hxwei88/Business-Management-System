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
        private CollectionReference salesColl;
        private CollectionReference orderColl;

        public Report()
        {
            InitializeComponent();
        }

        public Sales Sales
        {
            get => default;
            set
            {
            }
        }

        public Order Order
        {
            get => default;
            set
            {
            }
        }

        public Stock Stock
        {
            get => default;
            set
            {
            }
        }

        private void Report_Load(object sender, EventArgs e)
        {
            order_all = new List<Order>();
            stock_all = new List<Stock>();
            connectDb();
            getDate();
        }

        private void connectDb()
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + @"ekia.json";
            Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", path);
            db = FirestoreDb.Create("ekia-da749");
            salesColl = db.Collection("sales");
            orderColl = db.Collection("order");
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

            Query salesque = salesColl.OrderByDescending("sales_date").Limit(1);
            QuerySnapshot salessnap = await salesque.GetSnapshotAsync();
            Sales sales = salessnap.Documents[0].ConvertTo<Sales>();

            if(sales.sales_date.Month > System.DateTime.Now.AddMonths(-1).Month)
                await updateSales(newestDate.Year, newestDate.Month);

            finishLoad();
        }

        private async Task<bool> retrieveOrder(System.DateTime from, System.DateTime to)
        {
            //need make it dynamic
            System.DateTime startDate = from;
            System.DateTime endDate = to;

            Query stockque = orderColl.WhereGreaterThanOrEqualTo("order_date", startDate).WhereLessThanOrEqualTo("order_date", endDate); ;
            QuerySnapshot snap = await stockque.GetSnapshotAsync();

            QuerySnapshot item_snap;

            foreach (DocumentSnapshot docsnap in snap.Documents)
            {
                Order order = docsnap.ConvertTo<Order>();

                if (docsnap.Exists)
                {
                    item_snap = await orderColl.Document(docsnap.Id).Collection("order_items").GetSnapshotAsync();

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
                            profit += search.unit_price * item.quantity;
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
            loading();
            Query coll = db.Collection("order").OrderByDescending("order_date");

            Query oldestQue = coll.LimitToLast(1);
            QuerySnapshot oldestSnap = await oldestQue.GetSnapshotAsync();

            DocumentSnapshot oldestDoc = oldestSnap.Documents[0];
            Order oldestOrder = oldestDoc.ConvertTo<Order>();

            oldestDate = oldestOrder.order_date;

            Query newestQue = coll.Limit(1);
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

            retrieveStock();

            pnl_option.Hide();
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
            double lifetimeprofit = 0;
            double lifetimePercent = 0;
            double profit = 0;

            lifetimeprofit = await getMonthLifetimeProfit(year, month);

            //monthly sales report
            foreach (System.DateTime date in AllDatesInMonth(year, month))
            {
                var search = order_all.FindAll(x => x.order_date.Day == date.Day && x.order_date.Month == date.Month);

                profit = calculateProfit(exe, search);

                lifetimeprofit = lifetimeprofit + profit - dailycost;

                cht_statistics.Series["Profit (Daily)"].Points.AddXY(date.Day, profit - dailycost);
                cht_statistics.Series["Profit (Lifetime)"].Points.AddXY(date.Day, lifetimeprofit);
            }

            lifetimePercent = (cht_statistics.Series["Profit (Lifetime)"].Points.Last().YValues[0] - cht_statistics.Series["Profit (Lifetime)"].Points.First().YValues[0]) / Math.Abs(cht_statistics.Series["Profit (Lifetime)"].Points.First().YValues[0]);

            if (lifetimePercent > 0)
            {
                lbl_sales_percent_lifetime.ForeColor = System.Drawing.Color.Green;
                lbl_sales_percent_lifetime.Text = "+";
            }

            if (lifetimePercent < 0)
            {
                lbl_sales_percent_lifetime.ForeColor = System.Drawing.Color.Red;
                lbl_sales_percent_lifetime.Text = "";
            }

            lbl_sales_percent_lifetime.Text += Math.Round((lifetimePercent * 100), 0).ToString() + "%";

            finishLoad();
        }

        private async void populateChartYear(int year)
        {
            int day = 0;
            double lifetimePercent = 0;

            for (int month = 1; month <= 12; month++)
            {
                double dailycost = await calculateDailyCost(year, month);
                double profit = 0;
                double lifetimeprofit = await getMonthLifetimeProfit(year, month);

                foreach (System.DateTime date in AllDatesInMonth(year, month))
                {
                    day++;

                    var search = order_all.FindAll(x => x.order_date.Day == date.Day && x.order_date.Month == date.Month);

                    profit = calculateProfit(exe, search);

                    lifetimeprofit = lifetimeprofit + profit - dailycost;

                    if ((profit - dailycost) != 0)
                    {
                        cht_statistics.Series["Profit (Daily)"].Points.AddXY(day, profit - dailycost);
                        cht_statistics.Series["Profit (Lifetime)"].Points.AddXY(day, lifetimeprofit);
                    }
                }
            }

            lifetimePercent = (cht_statistics.Series["Profit (Lifetime)"].Points.Last().YValues[0] - cht_statistics.Series["Profit (Lifetime)"].Points.First().YValues[0]) / Math.Abs(cht_statistics.Series["Profit (Lifetime)"].Points.First().YValues[0]);

            if (lifetimePercent > 0)
            {
                lbl_sales_percent_lifetime.ForeColor = System.Drawing.Color.Green;
                lbl_sales_percent_lifetime.Text = "+";
            }

            if (lifetimePercent < 0)
            {
                lbl_sales_percent_lifetime.ForeColor = System.Drawing.Color.Red;
                lbl_sales_percent_lifetime.Text = "";
            }

            lbl_sales_percent_lifetime.Text += Math.Round((lifetimePercent * 100), 0).ToString() + "%";

            finishLoad();
        }

        int count = 0;

        //recurse from newest monnth
        private async Task<double> updateSales(int year, int month)
        {
            System.DateTime date = new System.DateTime(year, month, 1, 0, 0, 0, DateTimeKind.Utc);

            Query stockque = salesColl.WhereEqualTo("sales_date", date);

            QuerySnapshot snap = await stockque.GetSnapshotAsync();

            double profit = 0;
            double dailycost = 0;
            //care if month equals 0
            if (month - 1 == 0)
            {
                dailycost = await calculateDailyCost(year - 1, 12);
            }
            else
            {
                dailycost = await calculateDailyCost(year, month - 1);
            }

            if (!(year == oldestDate.Year && month == oldestDate.Month))
            {
                if (month - 1 == 0)
                {
                    profit = await updateSales(year - 1, 12);
                }
                else
                {
                    profit = await updateSales(year, month - 1);
                }
            }

            await retrieveOrder(date.AddMonths(-1), date.AddDays(-1));

            var search = order_all;
            //care if month equals 0
            if (month - 1 == 0)
            {
                search = order_all.FindAll(x => x.order_date.Year == date.Year - 1 && x.order_date.Month == 12);
            }
            else
            {
                search = order_all.FindAll(x => x.order_date.Year == date.Year && x.order_date.Month == date.Month - 1);
            }

            foreach (Order order in search)
            {
                foreach (Order_Items order_items in order.order_items)
                {
                    var search2 = stock_all.Find(x => x.item_id == order_items.item_id);

                    profit += search2.unit_price * order_items.quantity;
                }
            }

            for (int day = 0; day < date.AddMonths(1).AddDays(-1).Day; day++)
            {
                profit -= dailycost;
            }

            Dictionary<string, object> data = new Dictionary<string, object>()
            {
                {"sales_date", date},
                {"sales_profit", profit}
            };

            if (snap.Documents.Count <= 0)
            {
                await salesColl.AddAsync(data);

                order_all.Clear();
            }
            else
            {
                DocumentReference docref = salesColl.Document(snap.Documents[0].Id);

                await docref.SetAsync(data);
            }

            return profit;
        }

        private async Task<double> getMonthLifetimeProfit(int year, int month)
        {
            System.DateTime date = new System.DateTime(year, month, 1, 0, 0, 0, DateTimeKind.Utc);

            Query stockque = db.Collection("sales").WhereEqualTo("sales_date", date);

            QuerySnapshot snap = await stockque.GetSnapshotAsync();

            if (snap.Documents.Count == 0)
            {
                stockque = db.Collection("sales").OrderByDescending("sales_date").Limit(1);

                snap = await stockque.GetSnapshotAsync();
            }

            Sales sales = snap.Documents[0].ConvertTo<Sales>();

            return Math.Round(sales.sales_profit, 2);

            /*System.DateTime date = new System.DateTime(year, month, 1, 0, 0, 0, DateTimeKind.Utc);

            Query stockque = db.Collection("sales").WhereEqualTo("sales_date", date);

            QuerySnapshot snap = await stockque.GetSnapshotAsync();

            if (snap.Documents.Count > 0)
            {
                Sales sales = snap.Documents[0].ConvertTo<Sales>();

                return sales.sales_profit;
            }

            if (snap.Documents.Count <= 0)
            {
                double profit = 0;
                //care if month equals 0
                double dailycost = await calculateDailyCost(year, month - 1);

                if (!(year == oldestDate.Year && month == oldestDate.Month - 1))
                {
                    if (month - 1 == 0)
                    {
                        profit = await getMonthLifetimeProfit(year - 1, 12);
                    }
                    else
                    {
                        profit = await getMonthLifetimeProfit(year, month - 1);
                    }
                }

                await retrieveOrder(date.AddMonths(-1), date.AddDays(-1));

                //care if month equals 0
                var search = order_all.FindAll(x => x.order_date.Year == date.Year && x.order_date.Month == date.Month - 1);

                foreach(Order order in search)
                {
                    foreach(Order_Items order_items in order.order_items)
                    {
                        var search2 = stock_all.Find(x => x.item_id == order_items.item_id);

                        profit += search2.unit_price * order_items.quantity;
                    }
                }

                for(int day = 0; day < date.AddMonths(1).AddDays(-1).Day; day++)
                {
                    profit -= dailycost;
                }

                CollectionReference coll = db.Collection("sales");

                Dictionary<string, object> data = new Dictionary<string, object>()
                {
                    {"sales_date", date},
                    {"sales_profit", profit}
                };
                
                await coll.AddAsync(data);

                order_all.Clear();

                return profit;
            }

            return 0;*/
        }

        private async void populateMonthComboBox()
        {
            cb_month_mth.Enabled = false;

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

            cb_month_mth.Enabled = true;
        }

        public static IEnumerable<System.DateTime> AllDatesInMonth(int year, int month)
        {
            int days = System.DateTime.DaysInMonth(year, month);
            for (int day = 1; day <= days; day++)
            {
                yield return new System.DateTime(year, month, day);
            }
        }

        private void cb_month_yr_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cb_range.SelectedIndex == 0)
            {
                populateMonthComboBox();
            }
            else
            {
                btn_run.Enabled = true;
            }
        }

        private void cb_range_SelectedIndexChanged(object sender, EventArgs e)
        {
            cb_month_yr.Enabled = true;

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
            double lifetimePercent = 0;

            loading();

            order_all.Clear();

            cht_statistics.Series["Profit (Daily)"].Points.Clear();

            Title title = new Title();
            title.Font = new Font("Impact", 18);

            switch (cb_range.SelectedIndex)
            {
                case 0:
                    year = Int32.Parse(cb_month_yr.Text);
                    month = Int32.Parse(cb_month_mth.Text);

                    cht_statistics.Series["Profit (Daily)"].MarkerStyle = MarkerStyle.Circle;
                    cht_statistics.Series["Profit (Lifetime)"].MarkerStyle = MarkerStyle.Circle;
                    
                    title.Text = "Sales Report in " + year + "/" + month;

                    cht_statistics.Titles.Clear();
                    cht_statistics.Titles.Add(title);
                    //cht_statistics.ChartAreas["ChartArea1"].AxisX.Interval;

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
                    cht_statistics.Series["Profit (Daily)"].MarkerStyle = MarkerStyle.None;
                    cht_statistics.Series["Profit (Lifetime)"].IsValueShownAsLabel = false;
                    cht_statistics.Series["Profit (Lifetime)"].MarkerStyle = MarkerStyle.None;

                    title.Text = "Sales Report in " + year;

                    cht_statistics.Titles.Clear();
                    cht_statistics.Titles.Add(title);

                    foreach (var series in cht_statistics.Series)
                    {
                        series.Points.Clear();
                    }

                    exe = await retrieveOrder(new System.DateTime(year, 1, 1, 0, 0, 0, DateTimeKind.Utc), new System.DateTime(year, 12, 31, 0, 0, 0, DateTimeKind.Utc));
                    populateChartYear(year);
                    break;
            }
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
            pnl_main.Enabled = false;
            pnl_content.Cursor = Cursors.WaitCursor;
            pnl_header.Cursor = Cursors.WaitCursor;
            pnl_option.Hide();
        }

        private void finishLoad()
        {
            pnl_main.Enabled = true;
            pnl_content.Cursor = Cursors.Default;
            pnl_header.Cursor = Cursors.Default;
            pnl_option.Show();
        }

        private void cb_sales_num_CheckedChanged(object sender, EventArgs e)
        {
            if (cb_sales_num.Checked)
            {
                cht_statistics.Series["Profit (Lifetime)"].IsValueShownAsLabel = true;
                cht_statistics.Series["Profit (Daily)"].IsValueShownAsLabel = true;
            }
            else
            {
                cht_statistics.Series["Profit (Lifetime)"].IsValueShownAsLabel = false;
                cht_statistics.Series["Profit (Daily)"].IsValueShownAsLabel = false;
            }    
        }

        private void cb_hide_daily_CheckedChanged(object sender, EventArgs e)
        {
            if (cb_hide_daily.Checked)
            {
                cht_statistics.Series["Profit (Daily)"].Enabled = false;
            }
            else
            {
                cht_statistics.Series["Profit (Daily)"].Enabled = true;
            }
        }

        private void cb_hide_lifetime_CheckedChanged(object sender, EventArgs e)
        {
            if (cb_hide_lifetime.Checked)
            {
                cht_statistics.Series["Profit (Lifetime)"].Enabled = false;
            }
            else
            {
                cht_statistics.Series["Profit (Lifetime)"].Enabled = true;
            }
        }
    }
}
