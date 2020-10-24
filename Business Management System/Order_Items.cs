using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Google.Cloud.Firestore;

namespace Business_Management_System
{
    [FirestoreData]

    public class Order_Items
    {
        [FirestoreProperty]
        public string item_id { get; set; }

        [FirestoreProperty]
        public int quantity { get; set; }

        /*[FirestoreProperty]
        public double discount { get; set; }*/
    }
}