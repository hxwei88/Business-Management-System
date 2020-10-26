using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Google.Cloud.Firestore;

namespace Business_Management_System
{
    [FirestoreData]

    public class Vendor
    {
        [FirestoreProperty]
        public string vendor_id { get; set; }

        [FirestoreProperty]
        public string vendor_name { get; set; }
    }
}
