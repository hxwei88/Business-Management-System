using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Google.Cloud.Firestore;

namespace Business_Management_System
{
    [FirestoreData]

    public class Order
    {
        [FirestoreProperty]
        public DateTime order_date { get; set; }

        [FirestoreProperty]
        public string order_id { get; set; }

        [FirestoreProperty]
        public string order_status { get; set; }

        [FirestoreProperty]
        public List<Order_Items> order_items { get; set; }
    }
}
