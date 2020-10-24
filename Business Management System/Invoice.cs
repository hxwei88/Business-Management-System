using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Google.Cloud.Firestore;

namespace Business_Management_System
{
    [FirestoreData]

    public class Invoice
    {
        [FirestoreProperty]
        public string invoice_id { get; set; }

        [FirestoreProperty]
        public string invoice_num { get; set; }

        [FirestoreProperty]
        public string vendor_id { get; set; }

        [FirestoreProperty]
        public DateTime invoice_date { get; set; }

        [FirestoreProperty]
        public List<Invoice_Items> invoice_items { get; set; }
    }
}
