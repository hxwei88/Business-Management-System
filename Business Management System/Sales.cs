using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Google.Cloud.Firestore;

namespace Business_Management_System
{
    [FirestoreData]

    public class Sales
    {
        [FirestoreProperty]
        public DateTime sales_date { get; set; }

        [FirestoreProperty]
        public double sales_profit { get; set; }
    }
}
