using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Google.Cloud.Firestore;

namespace Business_Management_System
{
    [FirestoreData]

    public class Invoice_Items : Invoice
    {
        [FirestoreProperty]
        public int quantity { get; set; }

        [FirestoreProperty]
        public int item_id { get; set; }

        [FirestoreProperty]
        public string item_name { get; set; }

        [FirestoreProperty]
        public double wholesale_price { get; set; }
    }
}
