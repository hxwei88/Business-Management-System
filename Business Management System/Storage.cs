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
using System.Collections;
using Excel = Microsoft.Office.Interop.Excel;
using Microsoft.Office.Interop.Excel;
using System.IO;

namespace Business_Management_System
{
    public partial class Storage : UserControl
    {
        FirestoreDb db;
        private SearchBar ctrl_search = new SearchBar();
        private string cell_temp;
        private ArrayList coll_id = new ArrayList();
        private ArrayList del_coll_id = new ArrayList();
        private ArrayList edit_coll_id = new ArrayList();
        private List<Stock> add_coll = new List<Stock>();
        private string auth_level;

        public Storage(string auth)
        {
            InitializeComponent();
            auth_level = auth;
            uc_srchbar();
            connectDb();

            if(auth_level == "admin")
            {
                dataGridView1.ReadOnly = false;
                btn_save.Show();
            }
            else
            {
                dataGridView1.ReadOnly = true;
                btn_save.Hide();
            }

        }

        private void connectDb()
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + @"ekia.json";
            Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", path);
            db = FirestoreDb.Create("ekia-da749");

            retrieveDb("stock");

        }

        private async void deleteData()
        {
            Query orderColl = db.Collection("invoice").WhereGreaterThanOrEqualTo("invoice_date", new DateTime(2019, 1, 1, 0, 0, 0, DateTimeKind.Utc)).WhereLessThanOrEqualTo("invoice_date", new DateTime(2019, 10, 1, 0, 0, 0, DateTimeKind.Utc));
            QuerySnapshot snap = await orderColl.GetSnapshotAsync();

            foreach(DocumentSnapshot docsnap in snap)
            {
                await docsnap.Reference.DeleteAsync();
            }
        }

        private async void simulateData()
        {
            CollectionReference orderColl = db.Collection("order");
            CollectionReference invoiceColl = db.Collection("invoice");
            CollectionReference stockColl = db.Collection("stock");

            QuerySnapshot stockSnap = await stockColl.GetSnapshotAsync();

            List<Stock> stockList = new List<Stock>();

            foreach(DocumentSnapshot stockDocsnap in stockSnap.Documents)
            {
                Stock stock = stockDocsnap.ConvertTo<Stock>();

                stockList.Add(stock);
            }

            Order order = new Order();
            order.order_id = "";
            order.order_status = "";
            Invoice invoice = new Invoice();
            invoice.invoice_id = -1;
            invoice.invoice_num = "dummyinvoicenumber";

            Random random = new Random();

            for(int year = 2020; year <= 2020; year++)
            {
                if (year == 2019)
                {
                    for (int month = 1; month <= 12; month++)
                    {
                        //simulate new invoices at start of every month
                        foreach(Stock stock in stockList)
                        {
                            Dictionary<string, object> data = new Dictionary<string, object>()
                            {
                                {"invoice_date", new DateTime(year, month, 1, 0, 0, 0, DateTimeKind.Utc)},
                                {"invoice_id", invoice.invoice_id},
                                {"invoice_num", invoice.invoice_num},
                                {"vendor_id", stock.vendor_id}
                            };

                            DocumentReference invoiceDocref = await invoiceColl.AddAsync(data);

                            Dictionary<string, object> data2 = new Dictionary<string, object>()
                            {
                                {"item_id", stock.item_id},
                                {"item_name", stock.item_name},
                                {"quantity", 100},
                                {"wholesale_price", stock.wholesale_price}
                            };

                            await invoiceDocref.Collection("invoice_items").AddAsync(data2);
                        }

                        //simulate sales order everyday
                        for(int day = 1; day <= DateTime.DaysInMonth(year, month); day++)
                        {
                            //loop through stock
                            for(int i = 0; i < 10; i++)
                            {
                                int orderRand = random.Next(0, 10);
                                //if 0 means the item has no sales, else sell according to the number for quantity
                                if (orderRand > 0)
                                {
                                    Dictionary<string, object> data = new Dictionary<string, object>()
                                    {
                                        {"order_date", new DateTime(year, month, day, 0, 0, 0, DateTimeKind.Utc)},
                                        {"order_id", "dummyorderid"},
                                        {"order_status", "dummy"},
                                    };

                                    DocumentReference orderDocref = await orderColl.AddAsync(data);

                                    Dictionary<string, object> data2 = new Dictionary<string, object>()
                                    {
                                        {"item_id", stockList[i].item_id},
                                        {"quantity", orderRand},
                                    };

                                    await orderDocref.Collection("order_items").AddAsync(data2);
                                }
                            }
                        }
                    }
                }
                else
                {
                    for (int month = 12; month <= 12; month++)
                    {
                        foreach (Stock stock in stockList)
                        {
                            Dictionary<string, object> data = new Dictionary<string, object>()
                            {
                                {"invoice_date", new DateTime(year, month, 1, 0, 0, 0, DateTimeKind.Utc)},
                                {"invoice_id", invoice.invoice_id},
                                {"invoice_num", invoice.invoice_num},
                                {"vendor_id", stock.vendor_id}
                            };

                            DocumentReference invoiceDocref = await invoiceColl.AddAsync(data);

                            Dictionary<string, object> data2 = new Dictionary<string, object>()
                            {
                                {"item_id", stock.item_id},
                                {"item_name", stock.item_name},
                                {"quantity", 100},
                                {"wholesale_price", stock.wholesale_price}
                            };

                            await invoiceDocref.Collection("invoice_items").AddAsync(data2);
                        }

                        for (int day = 1; day <= DateTime.DaysInMonth(year, month); day++)
                        {
                            //loop through stock
                            for (int i = 0; i < 3; i++)
                            {
                                int orderRand = random.Next(0, 10);
                                //if 0 means the item has no sales, else sell according to the number for quantity
                                if (orderRand > 0)
                                {
                                    Dictionary<string, object> data = new Dictionary<string, object>()
                                    {
                                        {"order_date", new DateTime(year, month, day, 0, 0, 0, DateTimeKind.Utc)},
                                        {"order_id", "dummyorderid"},
                                        {"order_status", "dummy"},
                                    };

                                    DocumentReference orderDocref = await orderColl.AddAsync(data);

                                    Dictionary<string, object> data2 = new Dictionary<string, object>()
                                    {
                                        {"item_id", stockList[i].item_id},
                                        {"quantity", orderRand},
                                    };

                                    await orderDocref.Collection("order_items").AddAsync(data2);
                                }
                            }
                        }
                    }
                }
            }
        }

        private void refreshDGV()
        {
            dataGridView1.Rows.Clear();
            coll_id.Clear();
            retrieveDb("stock");
        }

        private void insertDb()
        {
            CollectionReference coll = db.Collection("stock");

            foreach (Stock stock in add_coll)
            {
                Dictionary<string, object> data = new Dictionary<string, object>()
                {
                    {"item_name", stock.item_name},
                    {"vendor_id", stock.vendor_id},
                    {"unit_price", stock.unit_price},
                    {"quantity", stock.quantity},
                    {"wholesale_price", stock.wholesale_price},
                    {"item_id", stock.item_id}
                };
                coll.AddAsync(data);
            }

            add_coll.Clear();
        }

        async void editDb()
        {
            foreach (string id in edit_coll_id)
            {
                DocumentReference docref = db.Collection("stock").Document(id);

                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    string temp = Convert.ToString(dataGridView1.Rows[i].Cells[dataGridView1.Columns["id"].Index].Value);

                    if (temp != "")
                    {
                        if(temp == id)
                        {
                            Dictionary<string, object> data = new Dictionary<string, object>()
                            {
                                {"item_name", dataGridView1.Rows[i].Cells[dataGridView1.Columns["item_name"].Index].Value.ToString()},
                                {"vendor_id", dataGridView1.Rows[i].Cells[dataGridView1.Columns["vendor_id"].Index].Value.ToString()},
                                {"unit_price", Convert.ToDouble(dataGridView1.Rows[i].Cells[dataGridView1.Columns["unit_price"].Index].Value.ToString())},
                                {"quantity", Int32.Parse(dataGridView1.Rows[i].Cells[dataGridView1.Columns["quantity"].Index].Value.ToString())},
                                {"wholesale_price", Convert.ToDouble(dataGridView1.Rows[i].Cells[dataGridView1.Columns["wholesale_price"].Index].Value.ToString())},
                                {"item_id", dataGridView1.Rows[i].Cells[dataGridView1.Columns["item_id"].Index].Value.ToString()}
                            };

                            DocumentSnapshot snap = await docref.GetSnapshotAsync();

                            if (snap.Exists)
                            {
                                await docref.SetAsync(data);
                            }

                            break;
                        }
                    }
                }
            }

            edit_coll_id.Clear();
        }

        private void deleteDb()
        {
            foreach (string id in del_coll_id)
            {
                DocumentReference docref = db.Collection("stock").Document(id);
                docref.DeleteAsync();
            }

            del_coll_id.Clear();
        }

        public void updateDb()
        {
            waitLoad();
            if (add_coll.Count > 0)
                insertDb();
            if (edit_coll_id.Count > 0)
                editDb();
            if (del_coll_id.Count > 0)
                deleteDb();
            finishLoad();
        }

        async void retrieveDb(string nameOfCollection)
        {
            waitLoad();

            Query stockque = db.Collection(nameOfCollection);
            QuerySnapshot snap = await stockque.GetSnapshotAsync();

            dataGridView1.EnableHeadersVisualStyles = false;

            foreach (DocumentSnapshot docsnap in snap.Documents)
            {
                Stock stock = docsnap.ConvertTo<Stock>();

                coll_id.Add(docsnap.Id);

                if(docsnap.Exists)
                {
                    dataGridView1.Rows.Add();

                    dataGridView1.Rows[dataGridView1.RowCount - 2].Cells[dataGridView1.Columns["id"].Index].Value = docsnap.Id;

                    colorizeRow(stock.quantity, dataGridView1.RowCount - 2);

                    dataGridView1.Rows[dataGridView1.RowCount - 2].Cells[dataGridView1.Columns["item_name"].Index].Value = stock.item_name;
                    dataGridView1.Rows[dataGridView1.RowCount - 2].Cells[dataGridView1.Columns["item_id"].Index].Value = stock.item_id;
                    dataGridView1.Rows[dataGridView1.RowCount - 2].Cells[dataGridView1.Columns["vendor_id"].Index].Value = stock.vendor_id;
                    dataGridView1.Rows[dataGridView1.RowCount - 2].Cells[dataGridView1.Columns["quantity"].Index].Value = stock.quantity;
                    dataGridView1.Rows[dataGridView1.RowCount - 2].Cells[dataGridView1.Columns["wholesale_price"].Index].Value = stock.wholesale_price;
                    dataGridView1.Rows[dataGridView1.RowCount - 2].Cells[dataGridView1.Columns["unit_price"].Index].Value = stock.unit_price;
                    //dataGridView1.Rows[dataGridView1.RowCount - 1].ReadOnly = true;
                }
            }

            finishLoad();
        }

        private void colorizeRow(int quantity, int row)
        {
            if (quantity > 50)
            {
                dataGridView1.Rows[row].HeaderCell.Style.BackColor = Color.LightGreen;
                dataGridView1.Rows[row].HeaderCell.Value = "In Stock";
            }
            else if (quantity <= 50 && quantity > 0)
            {
                dataGridView1.Rows[row].HeaderCell.Style.BackColor = Color.LightYellow;
                dataGridView1.Rows[row].HeaderCell.Value = "Running Out of Stock";
            }
            else
            {
                dataGridView1.Rows[row].HeaderCell.Style.BackColor = Color.Red;
                dataGridView1.Rows[row].HeaderCell.Value = "Out of Stock";
            }
        }

        private void dataGridView1_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
        {
            DialogResult result = MessageBox.Show("Do you want really to delete the selected row?", "Confirm", MessageBoxButtons.OKCancel);
                
            e.Cancel = true;

            if (result == DialogResult.OK)
            {
                foreach (DataGridViewRow row in dataGridView1.SelectedRows)
                {
                    string temp = Convert.ToString(dataGridView1.Rows[row.Index].Cells[dataGridView1.Columns["id"].Index].Value);

                    //need redo (if no doc id means new row)
                    if (temp != "")
                    {
                        string id = coll_id[row.Index].ToString();

                        coll_id.Remove(row.Index);

                        del_coll_id.Add(temp);
                    }
                    else
                    {
                        //delete add_coll
                        add_coll.RemoveAt(row.Index - 1);
                    }

                    dataGridView1.Rows.Remove(row);
                }
            }
        }

        private void waitLoad()
        {
            dataGridView1.Enabled = false;
            ctrl_search.Enabled = false;
            pnl_content.Cursor = Cursors.WaitCursor;
            btn_save.Enabled = false;
            btn_upload.Enabled = false;
        }

        private void finishLoad()
        {
            dataGridView1.Enabled = true;
            ctrl_search.Enabled = true;
            pnl_content.Cursor = Cursors.Default;
            btn_save.Enabled = true;
            btn_upload.Enabled = true;
        }

        private void dataGridView1_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            if (dataGridView1.CurrentCell.Value != null)
                cell_temp = dataGridView1.CurrentCell.Value.ToString();
            else
                cell_temp = null;
        }

        private void dataGridView1_UserAddedRow(object sender, DataGridViewRowEventArgs e)
        {
            Stock stock = new Stock();

            add_coll.Add(stock);
        }

        //need changes
        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1[e.ColumnIndex, e.RowIndex].Value != null)
            {
                if (cell_temp != dataGridView1[e.ColumnIndex, e.RowIndex].Value.ToString())
                {
                    string temp = Convert.ToString(dataGridView1.Rows[e.RowIndex].Cells[dataGridView1.Columns["id"].Index].Value);

                    if (temp != "")
                    {
                        //edit
                        edit_coll_id.Add(temp);
                    }
                    else
                    {
                        int row = dataGridView1.Rows.Count - e.RowIndex - 2;
                        //add
                        switch (dataGridView1.Columns[e.ColumnIndex].Name)
                        {
                            case "item_name":
                                add_coll[row].item_name = dataGridView1[e.ColumnIndex, e.RowIndex].Value.ToString();
                                break;
                            case "vendor_id":
                                add_coll[row].vendor_id = Convert.ToInt32(dataGridView1[e.ColumnIndex, e.RowIndex].Value.ToString());
                                break;
                            case "unit_price":
                                if (Double.TryParse(dataGridView1[e.ColumnIndex, e.RowIndex].Value.ToString(), out double unit_price))
                                {
                                    add_coll[row].unit_price = Convert.ToDouble(dataGridView1[e.ColumnIndex, e.RowIndex].Value.ToString());
                                }
                                else
                                {
                                    MessageBox.Show("Only numbers are allowed!");
                                    dataGridView1[e.ColumnIndex, e.RowIndex].Value = "";
                                }
                                break;
                            case "quantity":
                                if (int.TryParse(dataGridView1[e.ColumnIndex, e.RowIndex].Value.ToString(), out int quantity))
                                {
                                    add_coll[row].quantity = Int32.Parse(dataGridView1[e.ColumnIndex, e.RowIndex].Value.ToString());
                                    colorizeRow(Int32.Parse(dataGridView1[e.ColumnIndex, e.RowIndex].Value.ToString()), e.RowIndex);
                                }
                                else
                                {
                                    MessageBox.Show("Only numbers are allowed!");
                                    dataGridView1[e.ColumnIndex, e.RowIndex].Value = "";
                                }
                                break;
                            case "item_id":
                                add_coll[row].item_id = Convert.ToInt32(dataGridView1[e.ColumnIndex, e.RowIndex].Value);
                                break;
                            case "wholesale_price":
                                if (Double.TryParse(dataGridView1[e.ColumnIndex, e.RowIndex].Value.ToString(), out double wholesale_price))
                                {
                                    add_coll[row].wholesale_price = Convert.ToDouble(dataGridView1[e.ColumnIndex, e.RowIndex].Value.ToString());
                                }
                                else
                                {
                                    MessageBox.Show("Only numbers are allowed!");
                                    dataGridView1[e.ColumnIndex, e.RowIndex].Value = "";
                                }
                                break;
                        }
                    }
                }
            }
        }

        private void uc_srchbar()
        {
            ctrl_search.Dock = DockStyle.Top;
        }

        private void btn_save_Click(object sender, EventArgs e)
        {
            updateDb();
            MessageBox.Show("Stock Updated!");
            refreshDGV();
        }

        ArrayList failedUpload = new ArrayList();
        ArrayList newInvoiceStock = new ArrayList();
        ArrayList invoiceDupe = new ArrayList();
        private void btn_upload_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Title = "Browse Invoices";
            openFileDialog1.DefaultExt = "xlsx";
            openFileDialog1.Filter = "xlsx files (*.xlsx)|*.xlsx";
            openFileDialog1.CheckFileExists = true;
            openFileDialog1.CheckPathExists = true;
            openFileDialog1.Multiselect = true;
            openFileDialog1.ShowDialog();

            waitLoad();

            foreach (String file in openFileDialog1.FileNames)
            {
                uploadInvoice(file);
            }

            if (failedUpload.Count > 0)
            {
                string message = "File(s) chosen below failed to upload:\n";

                foreach (string filename in failedUpload)
                {
                    message += "-" + filename + "\n";
                }

                MessageBox.Show(message, "Invalid file(s) detected!");
            }

            failedUpload.Clear();
        }

        private async void uploadInvoice(String file)
        {
            Excel.Application xlApp = new Excel.Application();
            Excel.Workbook xlWorkBook = xlApp.Workbooks.Open(file);
            object misValue = System.Reflection.Missing.Value;

            foreach (Excel.Worksheet xlWorkSheet in xlWorkBook.Worksheets)
            {
                try
                {
                    string temp;
                    temp = xlWorkSheet.Range["Vendor_Name"].Value2.ToString();
                    temp = xlWorkSheet.Range["Invoice_Number"].Value2.ToString();
                    temp = xlWorkSheet.Range["Invoice_Date"].Value2.ToString();
                    temp = xlWorkSheet.Range["Item_Number"].Value2.ToString();
                    temp = xlWorkSheet.Range["Item_Desc"].Value2.ToString();
                    temp = xlWorkSheet.Range["Item_Quantity"].Value2.ToString();
                    temp = xlWorkSheet.Range["Item_Price"].Value2.ToString();
                    temp = xlWorkSheet.Range["Item_Total_Price"].Value2.ToString();
                    temp = xlWorkSheet.Range["Total_Balance"].Value2.ToString();

                    CollectionReference icoll = db.Collection("invoice");
                    Query iquery = icoll.WhereEqualTo("invoice_num", xlWorkSheet.Range["Invoice_Number"].Value2.ToString());
                    QuerySnapshot isnap = await iquery.GetSnapshotAsync();

                    Query iquery2 = icoll.OrderByDescending("invoice_id").Limit(1);
                    QuerySnapshot isnap2 = await iquery2.GetSnapshotAsync();
                    int invoiceid = isnap2.Documents[0].ConvertTo<Invoice>().invoice_id;

                    if (isnap.Count == 0)
                    {
                        try
                        {
                            for (int count = 0; ; count++)
                            {
                                newInvoiceStock.Add(xlWorkSheet.Cells[xlWorkSheet.Range["Item_Desc"].Row + count, xlWorkSheet.Range["Item_Desc"].Column].Value2.ToString());
                            }
                        }
                        catch
                        {
                            CollectionReference coll = db.Collection("stock");

                            Query query2 = coll.OrderByDescending("item_id").Limit(1);
                            QuerySnapshot stocksnap = await query2.GetSnapshotAsync();
                            int stockid = stocksnap.Documents[0].ConvertTo<Stock>().item_id;

                            int vendor_id = await updateVendorDb(xlWorkSheet.Range["Vendor_Name"].Value2.ToString());

                            DateTime date = DateTime.FromOADate(Convert.ToDouble(xlWorkSheet.Range["Invoice_Date"].Value2.ToString()));
                            DateTime.SpecifyKind(date, DateTimeKind.Utc);

                            invoiceid = invoiceid + 1;

                            Dictionary<string, object> idata = new Dictionary<string, object>()
                            {
                                //auto generate invoice id
                                {"invoice_date", Timestamp.FromDateTimeOffset(date)},
                                {"invoice_id", invoiceid},
                                {"invoice_num", xlWorkSheet.Range["Invoice_Number"].Value2.ToString()},
                                {"vendor_id", vendor_id}
                            };
                            DocumentReference idoc = await icoll.AddAsync(idata);

                            for (int i = 0; i < newInvoiceStock.Count; i++)
                            {
                                //query need to repeat to verify existence of item
                                Query query = coll
                                    .WhereEqualTo("item_name", newInvoiceStock[i].ToString())
                                    .WhereEqualTo("vendor_id", vendor_id);

                                QuerySnapshot snap = await query.GetSnapshotAsync();

                                if (snap.Documents.Count <= 0)
                                {
                                    //add new item
                                    Stock stock = new Stock();

                                    stockid = stockid + 1;

                                    stock.item_id = stockid;
                                    stock.item_name = xlWorkSheet.Cells[xlWorkSheet.Range["Item_Desc"].Row + i, xlWorkSheet.Range["Item_Desc"].Column].Value2.ToString();
                                    stock.vendor_id = vendor_id;
                                    stock.wholesale_price = Double.Parse(xlWorkSheet.Cells[xlWorkSheet.Range["Item_Price"].Row + i, xlWorkSheet.Range["Item_Price"].Column].Value2.ToString());
                                    stock.quantity = Int32.Parse(xlWorkSheet.Cells[xlWorkSheet.Range["Item_Quantity"].Row + i, xlWorkSheet.Range["Item_Quantity"].Column].Value2.ToString());

                                    add_coll.Add(stock);

                                    Dictionary<string, object> idata2 = new Dictionary<string, object>()
                                    {
                                        {"item_id", stock.item_id},
                                        {"item_name", stock.item_name},
                                        {"quantity", stock.quantity},
                                        {"wholesale_price", stock.wholesale_price}
                                    };

                                    await idoc.Collection("invoice_items").AddAsync(idata2);
                                }
                                else
                                {
                                    //edit existing item
                                    string id = snap.Documents[0].Id;

                                    Stock stock = snap.Documents[0].ConvertTo<Stock>();

                                    DocumentReference docref = coll.Document(id);

                                    Dictionary<string, object> data = new Dictionary<string, object>()
                                    {
                                        {"quantity", stock.quantity + Convert.ToInt32(xlWorkSheet.Cells[xlWorkSheet.Range["Item_Quantity"].Row + i, xlWorkSheet.Range["Item_Quantity"].Column].Value2) }
                                    };

                                    await docref.UpdateAsync(data);

                                    Dictionary<string, object> idata2 = new Dictionary<string, object>()
                                    {
                                        {"item_id", stock.item_id},
                                        {"item_name", stock.item_name},
                                        {"quantity", Convert.ToInt32(xlWorkSheet.Cells[xlWorkSheet.Range["Item_Quantity"].Row + i, xlWorkSheet.Range["Item_Quantity"].Column].Value2)},
                                        {"wholesale_price", stock.wholesale_price}
                                    };

                                    await idoc.Collection("invoice_items").AddAsync(idata2);
                                }
                            }

                            if (add_coll.Count > 0)
                            {
                                PriceEntryForm form = new PriceEntryForm(add_coll);

                                form.Show();

                                form.FormClosed += new FormClosedEventHandler(Form_Closed);
                            }
                            else
                            {
                                refreshDGV();
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("Invoice duplicate detected!");
                    }
                }
                catch
                {
                    failedUpload.Add(Path.GetFileName(file));
                }
            }

            xlWorkBook.Close(false, misValue, misValue);
            xlApp.Quit();

            releaseObject(xlWorkBook);
            releaseObject(xlApp);

            finishLoad();
        }

        async void Form_Closed(object sender, FormClosedEventArgs e)
        {
            CollectionReference coll = db.Collection("stock");

            MessageBox.Show("hi");

            foreach (Stock stock in add_coll)
            {
                Dictionary<string, object> data2 = new Dictionary<string, object>()
                {
                    {"item_name", stock.item_name},
                    {"vendor_id", stock.vendor_id},
                    {"unit_price", stock.unit_price},
                    {"quantity", stock.quantity},
                    {"wholesale_price", stock.wholesale_price},
                    {"item_id", stock.item_id}
                };

                await coll.AddAsync(data2);
            }

            add_coll.Clear();

            refreshDGV();
        }

        private async Task<int> updateVendorDb(string vendorName)
        {
            CollectionReference coll = db.Collection("vendor");

            Query query = coll.WhereEqualTo("vendor_name", vendorName);
            Query query2 = coll.OrderByDescending("vendor_id").Limit(1);

            QuerySnapshot snap = await query.GetSnapshotAsync();
            QuerySnapshot snap2 = await query2.GetSnapshotAsync();
            int id = snap2.Documents[0].ConvertTo<Vendor>().vendor_id;

            if (snap.Documents.Count == 0)
            {
                id = id + 1;

                Dictionary<string, object> data = new Dictionary<string, object>()
                {
                    {"vendor_id", id},
                    {"vendor_name", vendorName}
                };

                await coll.AddAsync(data);

                return id;
            }

            Vendor vendor = snap.Documents[0].ConvertTo<Vendor>();

            return vendor.vendor_id;
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

        public PriceEntryForm PriceEntryForm
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
    }
}
