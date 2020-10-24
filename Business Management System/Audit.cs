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

        public Audit()
        {
            InitializeComponent();
        }

        private void Audit_Load(object sender, EventArgs e)
        {
            invoice_all = new List<Invoice>();
            connectDb();
        }

        private async void connectDb()
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + @"ekia.json";
            Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", path);
            db = FirestoreDb.Create("ekia-da749");

            oldestDate = await getOldestDate();
            newestDate = await getNewestDate();

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

        private async Task<DateTime> getOldestDate()
        {
            CollectionReference coll = db.Collection("invoice");

            Query stockque = coll
                .OrderByDescending("invoice_date")
                .Limit(1);

            QuerySnapshot snap = await stockque.GetSnapshotAsync();

            Invoice invoice = snap.Documents[0].ConvertTo<Invoice>();

            return invoice.invoice_date;
        }

        private async Task<DateTime> getNewestDate()
        {
            CollectionReference coll = db.Collection("invoice");

            Query stockque = coll
                .OrderBy("invoice_date")
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

        private void editTemplate()
        {
            string temp_vid;
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

            for (int j = 0; j < invoice_all.Count; j++)
            {
                if(temp_vid != invoice_all[j].vendor_id)
                {
                    if(temp.Count > voucherMaxList)
                    {
                        Range RngToCopy = xlWorkSheet.get_Range("Copy_Date", "Copy_Balance").EntireRow;
                        Range RngToInsert = xlWorkSheet.get_Range("B20", Type.Missing).EntireRow;

                        for (int i = 0; i < (voucherMaxList - temp.Count); i++)
                        {
                            RngToInsert.Insert(Microsoft.Office.Interop.Excel.XlInsertShiftDirection.xlShiftDown, RngToCopy.Copy(Type.Missing));
                        }
                    }

                    for(int i = 0; i < temp.Count; i++)
                    {
                        xlWorkSheet.Cells[xlWorkSheet.Range["Date"].Row + i, xlWorkSheet.Range["Date"].Column] = temp[i].invoice_date.ToString("dd/MM/yyyy");
                        xlWorkSheet.Cells[xlWorkSheet.Range["Invoice_Number"].Row + i, xlWorkSheet.Range["Invoice_Number"].Column] = temp[i].invoice_num;
                        xlWorkSheet.Cells[xlWorkSheet.Range["Balance"].Row + i, xlWorkSheet.Range["Balance"].Column] = calculateInvoiceTotal(temp[i]);
                    }

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

            foreach (Worksheet ws in xlWorkBook.Worksheets)
            {
                ws.PageSetup.CenterFooter = "";
            }

            xlApp.Visible = true;
            xlWorkBook.Worksheets.PrintPreview();
            xlApp.Visible = false;
            
            //save file line
            xlWorkBook.Close(false, misValue, misValue);
            xlApp.Quit();

            releaseObject(xlWorkSheet);
            releaseObject(xlWorkBook);
            releaseObject(xlApp);
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
            var today = DateTime.Today;
            var month = new DateTime(today.Year, today.Month, 1, 0, 0, 0, DateTimeKind.Utc);
            var first = month.AddMonths(-1);
            var last = month.AddDays(-1);

            retrieveInvoice(first, last);
        }

        private void btn_choosenMonth_Click(object sender, EventArgs e)
        {
            int year = Int32.Parse(cb_yr.Text);
            int month = Int32.Parse(cb_month.Text);

            retrieveInvoice(new System.DateTime(year, month, 1, 0, 0, 0, DateTimeKind.Utc), new System.DateTime(year, month, DateTime.DaysInMonth(year, month), 0, 0, 0, DateTimeKind.Utc));
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
    }
}
