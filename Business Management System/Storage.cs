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
                    {"quantity", stock.quantity}
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
                            {"item_name", dataGridView1.Rows[i].Cells[0].Value.ToString()},
                            {"vendor_id", dataGridView1.Rows[i].Cells[1].Value.ToString()},
                            {"unit_price", Convert.ToDouble(dataGridView1.Rows[i].Cells[2].Value.ToString())},
                            {"quantity", Int32.Parse(dataGridView1.Rows[i].Cells[3].Value.ToString())}
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
            if (add_coll.Count > 0)
                insertDb();
            if (edit_coll_id.Count > 0)
                editDb();
            if (del_coll_id.Count > 0)
                deleteDb();
        }

        async void retrieveDb(string nameOfCollection)
        {
            Query stockque = db.Collection(nameOfCollection);
            QuerySnapshot snap = await stockque.GetSnapshotAsync();

            foreach (DocumentSnapshot docsnap in snap.Documents)
            {
                Stock stock = docsnap.ConvertTo<Stock>();

                coll_id.Add(docsnap.Id);

                if(docsnap.Exists)
                {
                    dataGridView1.Rows.Add(stock.item_name, stock.vendor_id, stock.unit_price, stock.quantity);
                    //dataGridView1.Rows[dataGridView1.RowCount - 1].ReadOnly = true;
                }
            }
        }
        /*
        private void itemAdd_Click(object sender, System.EventArgs e)
        {
            dataGridView1.Rows.Add(1);
        }

        private void itemEdit_Click(object sender, System.EventArgs e)
        {
            dataGridView1.Rows[currentMouseOverRow].ReadOnly = false;
        }

        private void itemDelete_Click(object sender, System.EventArgs e)
        {
            string id = del_coll_id[currentMouseOverRow].ToString();

            del_coll_id.RemoveAt(currentMouseOverRow);

            deleteDb();

            dataGridView1.Rows.RemoveAt(currentMouseOverRow);
        }
        */
        private void dataGridView1_MouseClick(object sender, MouseEventArgs e)
        {
            /*
            if (e.Button == MouseButtons.Right)
            {
                ContextMenu m = new ContextMenu();
                MenuItem item_add = new MenuItem("&Add");
                MenuItem item_edit = new MenuItem("&Edit");
                MenuItem item_delete = new MenuItem("&Delete");

                m.MenuItems.Add(item_add);

                currentMouseOverRow = dataGridView1.HitTest(e.X, e.Y).RowIndex;

                if (currentMouseOverRow >= 0)
                {
                    m.MenuItems.Add(item_edit);
                    m.MenuItems.Add(item_delete);
                    m.MenuItems.Add(new MenuItem(string.Format("Do something to row {0}", currentMouseOverRow.ToString())));
                }

                m.Show(dataGridView1, new Point(e.X, e.Y));

                item_add.Click += new System.EventHandler(this.itemAdd_Click);
                item_edit.Click += new System.EventHandler(this.itemEdit_Click);
                item_delete.Click += new System.EventHandler(this.itemDelete_Click);
            }
            */
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
                        switch (e.ColumnIndex)
                        {
                            case 0:
                                add_coll[row].item_name = dataGridView1[0, e.RowIndex].Value.ToString();
                                break;
                            case 1:
                                add_coll[row].vendor_id = dataGridView1[1, e.RowIndex].Value.ToString();
                                break;
                            case 2:
                                add_coll[row].unit_price = Convert.ToDouble(dataGridView1[2, e.RowIndex].Value.ToString());
                                break;
                            case 3:
                                add_coll[row].quantity = Int32.Parse(dataGridView1[3, e.RowIndex].Value.ToString());
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
    }
}
