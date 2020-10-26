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
        //private int currentMouseOverRow;
        private ArrayList coll_id = new ArrayList();
        private ArrayList del_coll_id = new ArrayList();
        private ArrayList edit_coll_id = new ArrayList();
        private List<Stock> add_coll = new List<Stock>();
        private Load ctrl_load = new Load();

        public Storage()
        {
            InitializeComponent();
            uc_srchbar();
            connectDb();
        }

        private void connectDb()
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + @"ekia.json";
            Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", path);
            db = FirestoreDb.Create("ekia-da749");

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

                for(int i = 0; i < coll_id.Count; i++)
                {
                    if(id == coll_id[i].ToString())
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

                    colorizeRow(stock.quantity, coll_id.Count - 1);

                    dataGridView1.Rows[coll_id.Count - 1].Cells[dataGridView1.Columns["item_name"].Index].Value = stock.item_name;
                    dataGridView1.Rows[coll_id.Count - 1].Cells[dataGridView1.Columns["item_id"].Index].Value = stock.item_id;
                    dataGridView1.Rows[coll_id.Count - 1].Cells[dataGridView1.Columns["vendor_id"].Index].Value = stock.vendor_id;
                    dataGridView1.Rows[coll_id.Count - 1].Cells[dataGridView1.Columns["quantity"].Index].Value = stock.quantity;
                    dataGridView1.Rows[coll_id.Count - 1].Cells[dataGridView1.Columns["wholesale_price"].Index].Value = stock.wholesale_price;
                    dataGridView1.Rows[coll_id.Count - 1].Cells[dataGridView1.Columns["unit_price"].Index].Value = stock.unit_price;
                    //dataGridView1.Rows[dataGridView1.RowCount - 1].ReadOnly = true;
                }
            }

            finishLoad();
        }

        private void colorizeRow(int quantity, int row)
        {
            if (quantity > 10)
            {
                dataGridView1.Rows[row].HeaderCell.Style.BackColor = Color.LightGreen;
                dataGridView1.Rows[row].HeaderCell.Value = "In Stock";
            }
            else if (quantity <= 10 && quantity > 0)
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
                    if (row.Index < coll_id.Count)
                    {
                        string id = coll_id[row.Index].ToString();

                        coll_id.Remove(row.Index);

                        del_coll_id.Add(id);
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
                    if (e.RowIndex < coll_id.Count)
                    {
                        //edit
                        edit_coll_id.Add(coll_id[e.RowIndex]);
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
                                add_coll[row].vendor_id = dataGridView1[e.ColumnIndex, e.RowIndex].Value.ToString();
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
                                add_coll[row].item_id = dataGridView1[e.ColumnIndex, e.RowIndex].Value.ToString();
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
            //wont work when user click on menu, need to end edit mode inside code
        }

        private void uc_srchbar()
        {
            ctrl_search.Dock = DockStyle.Top;
            pnl_search.Controls.Add(ctrl_search);
        }

        private void btn_save_Click(object sender, EventArgs e)
        {
            updateDb();
            MessageBox.Show("Stock Updated!");
        }

        ArrayList failedUpload = new ArrayList();

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

            finishLoad();
        }

        private async void uploadInvoice(String file)
        {
            Excel.Application xlApp = new Excel.Application();Excel.Workbook xlWorkBook = xlApp.Workbooks.Open(file);
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

                    updateStockDb(xlWorkSheet, await updateVendorDb(xlWorkSheet.Range["Vendor_Name"].Value2.ToString()));
                }
                catch
                {
                    failedUpload.Add(Path.GetFileName(file));
                }
                finally
                {
                    xlWorkBook.Close(false, misValue, misValue);
                    xlApp.Quit();

                    releaseObject(xlWorkSheet);
                    releaseObject(xlWorkBook);
                    releaseObject(xlApp);
                }
            }
        }

        private async Task<string> updateVendorDb(string vendorName)
        {
            CollectionReference coll = db.Collection("vendor");

            Query query = coll.WhereEqualTo("vendor_name", vendorName);

            QuerySnapshot snap = await query.GetSnapshotAsync();

            if (snap.Documents.Count == 0)
            {
                Dictionary<string, object> data = new Dictionary<string, object>()
                {
                    {"vendor_id", "temporary"},
                    {"vendor_name", vendorName}
                };

                await coll.AddAsync(data);

                return "temporary";
            }

            Vendor vendor = snap.Documents[0].ConvertTo<Vendor>();

            return vendor.vendor_id;
        }

        private async void updateStockDb(Excel.Worksheet xlWorkSheet, string id)
        {
            CollectionReference coll = db.Collection("stock");

            Query query = coll
                .WhereEqualTo("item_num", xlWorkSheet.Range["Item_Number"].Value2.ToString())
                .WhereEqualTo("vendor_id", id);

            QuerySnapshot snap = await query.GetSnapshotAsync();

            
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
    }
}
