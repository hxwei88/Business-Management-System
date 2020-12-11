using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Google.Cloud.Firestore;

namespace Business_Management_System
{
    [FirestoreData]

    public class Voucher
    {
        [FirestoreProperty]
        public int voucher_id { get; set; }
    }
}
