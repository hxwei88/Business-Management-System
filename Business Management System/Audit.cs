using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Drawing.Printing;
using Firebase.Storage;
using Firebase;
using System.Net;
using Excel = Microsoft.Office.Interop.Excel;
using Microsoft.Office.Interop.Excel;
using Google.Cloud.Firestore;
using System.Collections;

namespace Business_Management_System
{
    public partial class Audit : UserControl
    {
        FirestoreDb db;
        private string path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\EKIA";
        private List<Invoice> invoice_all;
        private const int voucherMaxList = 5;
        private DateTime oldestDate;
        private DateTime newestDate;
        private int cb_yr_index = -1;

        public Audit()
        {
            InitializeComponent();
        }

        public Invoice Invoice
        {
            get => default;
            set
            {
            }
        }

        public Vendor Vendor
        {
            get => default;
            set
            {
            }
        }

        private void Audit_Load(object sender, EventArgs e)
        {
            invoice_all = new List<Invoice>();
            connectDb();
        }

        private void waitLoad()
        {
            flp_option.Enabled = false;
            pnl_ckbox.Enabled = false;
            pnl_main.Cursor = Cursors.WaitCursor;
        }

        private void finishLoad()
        {
            flp_option.Enabled = true;
            pnl_ckbox.Enabled = true;
            pnl_main.Cursor = Cursors.Default;
        }

        private async void connectDb()
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + @"ekia.json";
            Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", path);
            db = FirestoreDb.Create("ekia-da749");

            waitLoad();
            oldestDate = await getOldestDate();
            newestDate = await getNewestDate();
            finishLoad();

            populateYrCb();
        }

        private async void retrieveInvoice(DateTime start, DateTime end)
        {
            System.DateTime startDate = start;
            System.DateTime endDate = end;

            CollectionReference coll = db.Collection("invoice");

            Query stockque = coll
                .WhereGreaterThanOrEqualTo("invoice_date", startDate)
                .WhereLessThanOrEqualTo("invoice_date", endDate);
            QuerySnapshot snap = await stockque.GetSnapshotAsync();

            QuerySnapshot item_snap;

            if (snap.Documents.Count > 0)
            {
                foreach (DocumentSnapshot docsnap in snap.Documents)
                {
                    Invoice invoice = docsnap.ConvertTo<Invoice>();

                    if (docsnap.Exists)
                    {
                        item_snap = await coll.Document(docsnap.Id).Collection("invoice_items").GetSnapshotAsync();

                        invoice.invoice_items = new List<Invoice_Items>();

                        foreach (DocumentSnapshot item_docsnap in item_snap.Documents)
                        {
                            Invoice_Items invoice_items = item_docsnap.ConvertTo<Invoice_Items>();

                            if (item_docsnap.Exists)
                            {
                                invoice.invoice_items.Add(invoice_items);
                            }
                        }

                        invoice_all.Add(invoice);
                    }
                }

                invoice_all.Sort((x, y) => x.vendor_id.CompareTo(y.vendor_id));

                //null at the end for voucher generation
                invoice_all.Add(new Invoice());

                fetchTemplate();
            }
            else
            {
                MessageBox.Show("No invoices are found for the previous month!");
                pnl_main.Enabled = true;
            }
        }

        private async Task<DateTime> getOldestDate()
        {
            CollectionReference coll = db.Collection("invoice");

            Query stockque = coll
                .OrderByDescending("invoice_date")
                .LimitToLast(1);

            QuerySnapshot snap = await stockque.GetSnapshotAsync();

            Invoice invoice = snap.Documents[0].ConvertTo<Invoice>();

            return invoice.invoice_date;
        }

        private async Task<DateTime> getNewestDate()
        {
            CollectionReference coll = db.Collection("invoice");

            Query stockque = coll
                .OrderByDescending("invoice_date")
                .Limit(1);

            QuerySnapshot snap = await stockque.GetSnapshotAsync();

            Invoice invoice = snap.Documents[0].ConvertTo<Invoice>();

            return invoice.invoice_date;
        }

        private async void fetchTemplate()
        {
            var task = await new FirebaseStorage("ekia-da749.appspot.com")
                .Child("template")
                .Child("PaymentVoucher.xlsx")
                .GetDownloadUrlAsync();

            WebClient webClient = new WebClient();

            if(!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            //change download directory
            webClient.DownloadFile(task, path + @"\PaymentVoucher.xlsx");

            editTemplate();
        }

        private async void editTemplate()
        {
            int temp_vid;
            List<Invoice> temp = new List<Invoice>();

            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            Excel.Application xlApp;
            Excel.Workbook xlWorkBook;
            Excel.Worksheet xlWorkSheet;
            Excel.Worksheet copyXlWorkSheet;
            object misValue = System.Reflection.Missing.Value;

            xlApp = new Excel.Application();
            //change directory
            xlWorkBook = xlApp.Workbooks.Open(path + @"\PaymentVoucher.xlsx", 0, true, 5, "", "", true, Microsoft.Office.Interop.Excel.XlPlatform.xlWindows, "\t", false, false, 0, true, 1, 0);
            xlWorkSheet = (Excel.Worksheet)xlWorkBook.Worksheets.get_Item(1);
            temp_vid = invoice_all[0].vendor_id;
            copyXlWorkSheet = xlWorkSheet;

            DocumentReference docref0 = db.Collection("voucher").Document("2L3goYlcI1TWRj3G0wT2");
            DocumentSnapshot vsnap = await docref0.GetSnapshotAsync();
            int id = vsnap.ConvertTo<Voucher>().voucher_id;

            for (int j = 0; j < invoice_all.Count; j++)
            {
                if(temp_vid != invoice_all[j].vendor_id)
                {
                    //should go with vendor name
                    xlWorkSheet.Cells[xlWorkSheet.Range["Company_Name"].Row, xlWorkSheet.Range["Company_Name"].Column] = await setCompanyName(invoice_all[j - 1].vendor_id);
                    xlWorkSheet.Cells[xlWorkSheet.Range["Voucher_Date"].Row, xlWorkSheet.Range["Voucher_Date"].Column] = DateTime.Today.ToString("MM/dd/yyyy");

                    if (temp.Count > voucherMaxList)
                    {
                        Range RngToCopy = xlWorkSheet.get_Range("Copy_Date", "Copy_Balance").EntireRow;
                        Range RngToInsert = xlWorkSheet.get_Range("B20", Type.Missing).EntireRow;

                        for (int i = 0; i < (voucherMaxList - temp.Count); i++)
                        {
                            RngToInsert.Insert(Microsoft.Office.Interop.Excel.XlInsertShiftDirection.xlShiftDown, RngToCopy.Copy(Type.Missing));
                        }
                    }
                    
                    for (int i = 0; i < temp.Count; i++)
                    {
                        xlWorkSheet.Cells[xlWorkSheet.Range["Date"].Row + i, xlWorkSheet.Range["Date"].Column] = temp[i].invoice_date.ToString("dd/MM/yyyy");
                        xlWorkSheet.Cells[xlWorkSheet.Range["Invoice_Number"].Row + i, xlWorkSheet.Range["Invoice_Number"].Column] = temp[i].invoice_num;
                        xlWorkSheet.Cells[xlWorkSheet.Range["Balance"].Row + i, xlWorkSheet.Range["Balance"].Column] = calculateInvoiceTotal(temp[i]);
                    }

                    id = id + 1;
                    xlWorkSheet.Cells[xlWorkSheet.Range["Voucher_Number"].Row, xlWorkSheet.Range["Voucher_Number"].Column] = "v" + id;

                    if (j != (invoice_all.Count - 1))
                    {
                        xlWorkBook.Worksheets.Add();
                        copyXlWorkSheet.Copy(xlWorkBook.Worksheets.get_Item(xlWorkBook.Worksheets.Count));
                        xlWorkSheet = (Excel.Worksheet)xlWorkBook.Worksheets.get_Item(xlWorkBook.Worksheets.Count);
                        temp_vid = invoice_all[j].vendor_id;
                        temp.Clear();
                    }
                }

                if (temp_vid == invoice_all[j].vendor_id)
                {
                    temp.Add(invoice_all[j]);
                }
            }

            DocumentReference docref = db.Collection("voucher").Document("2L3goYlcI1TWRj3G0wT2");

            //if no print will also update
            Dictionary<string, object> data = new Dictionary<string, object>()
            {
                {"voucher_id", id}
            };

            await docref.UpdateAsync(data);

            foreach (Worksheet ws in xlWorkBook.Worksheets)
            {
                ws.PageSetup.CenterFooter = "";
            }

            xlApp.Visible = true;
            xlWorkBook.Worksheets.PrintPreview();
            xlApp.Visible = false;

            xlWorkBook.SaveAs(path + @"\PaymentVoucher" + DateTime.Now.ToString("dd-MM-yyyy HH.mm.ss") + ".xlsx", misValue,
            misValue, misValue, misValue, misValue, Excel.XlSaveAsAccessMode.xlNoChange,
            misValue, misValue, misValue, misValue, misValue);

            //save file line
            xlWorkBook.Close(false, misValue, misValue);
            xlApp.Quit();

            releaseObject(xlWorkSheet);
            releaseObject(xlWorkBook);
            releaseObject(xlApp);

            invoice_all.Clear();
            pnl_main.Enabled = true;
        }

        private async Task<string> setCompanyName(int vendor_id)
        {
            Query coll = db.Collection("vendor").WhereEqualTo("vendor_id", vendor_id);

            QuerySnapshot snap = await coll.GetSnapshotAsync();

            Vendor vendor = snap.Documents[0].ConvertTo<Vendor>();

            return vendor.vendor_name;
        }

        private double calculateInvoiceTotal(Invoice invoice)
        {
            double total = 0;

            foreach(Invoice_Items i in invoice.invoice_items)
            {
                total += i.wholesale_price * i.quantity;
            }

            return total;
        }

        private void populateYrCb()
        {
            for(int yr = oldestDate.Year; yr <= newestDate.Year; yr++)
            {
                cb_yr.Items.Add(yr);
            }
        }

        private void releaseObject(object obj)
        {
            try
            {
                System.Runtime.InteropServices.Marshal.ReleaseComObject(obj);
                obj = null;
            }
            catch (Exception ex)
            {
                obj = null;
                MessageBox.Show("Unable to release the Object " + ex.ToString());
            }
            finally
            {
                GC.Collect();
            }
        }

        private void btn_prevMonth_Click(object sender, EventArgs e)
        {
            pnl_main.Enabled = false;

            var today = DateTime.Today;
            var month = new DateTime(today.Year, today.Month, 1, 0, 0, 0, DateTimeKind.Utc);
            var first = month.AddMonths(-1);
            var last = month.AddDays(-1);

            retrieveInvoice(first, last);
        }

        private async void btn_choosenMonth_Click(object sender, EventArgs e)
        {
            pnl_main.Enabled = false;

            if (ckb_vendor.Checked)
            {
                CollectionReference coll = db.Collection("invoice");

                ArrayList invoiceNum = new ArrayList();

                getChosenInvoices(tv_invoices.Nodes, invoiceNum);

                if (invoiceNum.Count > 0)
                {
                    foreach (string num in invoiceNum)
                    {
                        Query stockque = coll
                            .WhereEqualTo("invoice_num", num);

                        QuerySnapshot snap = await stockque.GetSnapshotAsync();

                        QuerySnapshot item_snap;

                        foreach (DocumentSnapshot docsnap in snap.Documents)
                        {
                            Invoice invoice = docsnap.ConvertTo<Invoice>();

                            if (docsnap.Exists)
                            {
                                item_snap = await coll.Document(docsnap.Id).Collection("invoice_items").GetSnapshotAsync();

                                invoice.invoice_items = new List<Invoice_Items>();

                                foreach (DocumentSnapshot item_docsnap in item_snap.Documents)
                                {
                                    Invoice_Items invoice_items = item_docsnap.ConvertTo<Invoice_Items>();

                                    if (item_docsnap.Exists)
                                    {
                                        invoice.invoice_items.Add(invoice_items);
                                    }
                                }

                                invoice_all.Add(invoice);
                            }
                        }
                    }

                    invoice_all.Sort((x, y) => x.vendor_id.CompareTo(y.vendor_id));

                    //null at the end for voucher generation
                    invoice_all.Add(new Invoice());

                    fetchTemplate();
                }
                else
                {
                    MessageBox.Show("Please select your invoices!");
                    pnl_main.Enabled = true;
                }
            }
            else
            {
                if (cb_yr.Text != "" && cb_month.Text != "")
                {
                    int year = Int32.Parse(cb_yr.Text);
                    int month = Int32.Parse(cb_month.Text);

                    retrieveInvoice(new System.DateTime(year, month, 1, 0, 0, 0, DateTimeKind.Utc), new System.DateTime(year, month, DateTime.DaysInMonth(year, month), 0, 0, 0, DateTimeKind.Utc));
                }
                else
                {
                    MessageBox.Show("Please choose your year and month!");
                    pnl_main.Enabled = true;
                }
            }
        }

        private void getChosenInvoices(TreeNodeCollection nodeColl, ArrayList invoiceNum)
        {
            foreach(TreeNode node in nodeColl)
            {
                if(node.Checked)
                {
                    if (node.Nodes.Count > 0)
                    {
                        getChosenInvoices(node.Nodes, invoiceNum);
                    }
                    else
                    {
                        invoiceNum.Add(node.Text);
                    }
                }
            }
        }

        private async Task<bool> monthExists(DateTime start, DateTime end)
        {
            CollectionReference coll = db.Collection("invoice");

            Query stockque;

            QuerySnapshot snap;

            stockque = coll
                .WhereGreaterThanOrEqualTo("invoice_date", start)
                .WhereLessThanOrEqualTo("invoice_date", end);

            snap = await stockque.GetSnapshotAsync();

            if (snap.Documents.Count > 0)
                return true;

            return false;
        }

        private async void cb_yr_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cb_yr_index != cb_yr.SelectedIndex)
            {
                if (Int32.Parse(cb_yr.SelectedItem.ToString()) == oldestDate.Year)
                {
                    for (int month = oldestDate.Month; month <= 12; month++)
                    {
                        if (await monthExists(new DateTime(Int32.Parse(cb_yr.SelectedItem.ToString()), month, 1, 0, 0, 0, DateTimeKind.Utc), new DateTime(Int32.Parse(cb_yr.SelectedItem.ToString()), month, DateTime.DaysInMonth(Int32.Parse(cb_yr.SelectedItem.ToString()), month), 0, 0, 0, DateTimeKind.Utc)))
                        {
                            cb_month.Items.Add(month);
                        }
                    }
                }
                else if (Int32.Parse(cb_yr.SelectedItem.ToString()) > oldestDate.Year && Int32.Parse(cb_yr.SelectedItem.ToString()) < newestDate.Year)
                {
                    for (int month = 1; month <= 12; month++)
                    {
                        if (await monthExists(new DateTime(Int32.Parse(cb_yr.SelectedItem.ToString()), month, 1, 0, 0, 0, DateTimeKind.Utc), new DateTime(Int32.Parse(cb_yr.SelectedItem.ToString()), month, DateTime.DaysInMonth(Int32.Parse(cb_yr.SelectedItem.ToString()), month), 0, 0, 0, DateTimeKind.Utc)))
                        {
                            cb_month.Items.Add(month);
                        }
                    }
                }
                else if (Int32.Parse(cb_yr.SelectedItem.ToString()) == newestDate.Year)
                {
                    for (int month = 1; month <= newestDate.Month; month++)
                    {
                        if (await monthExists(new DateTime(Int32.Parse(cb_yr.SelectedItem.ToString()), month, 1, 0, 0, 0, DateTimeKind.Utc), new DateTime(Int32.Parse(cb_yr.SelectedItem.ToString()), month, DateTime.DaysInMonth(Int32.Parse(cb_yr.SelectedItem.ToString()), month), 0, 0, 0, DateTimeKind.Utc)))
                        {
                            cb_month.Items.Add(month);
                        }
                    }
                }
            }

            cb_yr_index = cb_yr.SelectedIndex;
            cb_month.Enabled = true;
        }

        private void cb_vendor_CheckedChanged(object sender, EventArgs e)
        {
            if(ckb_vendor.Checked)
            {
                if (tv_invoices.GetNodeCount(true) == 0)
                {
                    getVendorInvoices();
                }
                cb_yr.Enabled = false;
                cb_month.Enabled = false;
                pnl_invoices.Enabled = true;
            }
            else
            {
                pnl_invoices.Enabled = false;
                cb_yr.Enabled = true;
                cb_month.Enabled = true;
            }
        }

        private async void getVendorInvoices()
        {
            Query vendorQue = db.Collection("vendor");

            QuerySnapshot vendorSnap = await vendorQue.GetSnapshotAsync();

            foreach (DocumentSnapshot vendorDocsnap in vendorSnap.Documents)
            {
                Vendor vendor = vendorDocsnap.ConvertTo<Vendor>();

                if (vendorDocsnap.Exists)
                {
                    Query invoiceQue = db.Collection("invoice").WhereEqualTo("vendor_id", vendor.vendor_id);

                    QuerySnapshot invoiceSnap = await invoiceQue.GetSnapshotAsync();

                    if (invoiceSnap.Documents.Count > 0)
                    {
                        tv_invoices.Nodes.Add(vendor.vendor_name, vendor.vendor_name);

                        foreach (DocumentSnapshot invoiceDocsnap in invoiceSnap.Documents)
                        {
                            Invoice invoice = invoiceDocsnap.ConvertTo<Invoice>();

                            checkInvoiceDate(tv_invoices.Nodes[vendor.vendor_name], invoice, 0);
                        }
                    }
                }
            }

            pnl_invoices.Enabled = true;
        }

        private void checkInvoiceDate(TreeNode tree, Invoice invoice, int count)
        {
            switch(count)
            {
                case 0:
                    if(tree.Nodes.Count == 0 || !tree.Nodes.ContainsKey(invoice.invoice_date.Year.ToString()))
                    {
                        tree.Nodes.Add(invoice.invoice_date.Year.ToString(), invoice.invoice_date.Year.ToString());
                    }
                    checkInvoiceDate(tree.Nodes[invoice.invoice_date.Year.ToString()], invoice, ++count);
                    break;
                case 1:
                    if (tree.GetNodeCount(false) == 0 || !tree.Nodes.ContainsKey(invoice.invoice_date.Month.ToString()))
                    {
                        tree.Nodes.Add(invoice.invoice_date.Month.ToString(), invoice.invoice_date.Month.ToString());
                    }
                    checkInvoiceDate(tree.Nodes[invoice.invoice_date.Month.ToString()], invoice, ++count);
                    break;
                case 2:
                    if (tree.GetNodeCount(false) == 0 || !tree.Nodes.ContainsKey(invoice.invoice_date.Day.ToString()))
                    {
                        tree.Nodes.Add(invoice.invoice_date.Day.ToString(), invoice.invoice_date.Day.ToString());
                    }
                    checkInvoiceDate(tree.Nodes[invoice.invoice_date.Day.ToString()], invoice, ++count);
                    break;
                case 3:
                    if (tree.GetNodeCount(false) == 0 || !tree.Nodes.ContainsKey(invoice.invoice_num))
                    {
                        tree.Nodes.Add(invoice.invoice_num, invoice.invoice_num);
                    }
                    break;
            }
        }

        private void tv_invoices_AfterCheck(object sender, TreeViewEventArgs e)
        {
            if (e.Action != TreeViewAction.Unknown)
            {
                e.Node.Descendants().ToList().ForEach(x =>
                {
                    x.Checked = e.Node.Checked;
                });
                e.Node.Ancestors().ToList().ForEach(x =>
                {
                    x.Checked = x.Descendants().ToList().Any(y => y.Checked);
                });
            }
        }
    }
}
