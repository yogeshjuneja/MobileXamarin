using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMeditekApp.Wellogo.Models
{
    public class Tracker
    {
        public TrackerData data { get; set; }
    }

    public class TrackerData
    {
        public int id { get; set; }
        public int? client_id { get; set; }
        public int type { get; set; }
        public string response { get; set; }
        public string access_token { get; set; }
        public string refresh_token { get; set; }
        public object source_bundle_id { get; set; }
        public int status { get; set; }
        public string created_at { get; set; }
        public string updated_at { get; set; }
    }


    public class TrackerAcessOutput
    {
        public TrackerAcessOutputRoot data { get; set; }
    }

    public class TrackerAcessOutputRoot
    {
       
            public int id { get; set; }
            public string name { get; set; }
            public string unique_id { get; set; }
            public string email { get; set; }
            public int branch_id { get; set; }
            public string gender { get; set; }
            public string date_of_birth { get; set; }
            public string phone { get; set; }
            public int type { get; set; }
            public int status { get; set; }
            public int is_admin { get; set; }
            public int role_id { get; set; }
            public int seniority_level { get; set; }
            public string blood_group { get; set; }
            public int is_hra_completed { get; set; }
            public string designation { get; set; }
            public int is_phone_verified { get; set; }
            public object otp { get; set; }
            public double points { get; set; }
            public object relation_id { get; set; }
            public object client_id { get; set; }
            public object metas { get; set; }
            public int can_invite_members { get; set; }
            public object client_relation_id { get; set; }
            public string logged_in_at { get; set; }
            public object last_transaction_at { get; set; }
            public string created_at { get; set; }
            public string updated_at { get; set; }
            public int age { get; set; }
            public string photo_url { get; set; }
            public int corporate_id { get; set; }
            public string employee_code { get; set; }
            public TrackerData tracker { get; set; }

        
    }

    
}
