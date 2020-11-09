using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Google.Cloud.Firestore;

namespace Business_Management_System
{
    [FirestoreData]

    public class User
    {
        [FirestoreProperty]
        public int user_id { get; set; }

        [FirestoreProperty]
        public string username { get; set; }

        [FirestoreProperty]
        public string auth_level { get; set; }

        [FirestoreProperty]
        public string password { get; set; }

        [FirestoreProperty]
        public string email { get; set; }
    }
}
