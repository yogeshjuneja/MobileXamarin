using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMeditekApp.Wellogo.Models
{
    public class BloodBankMainData
    {
        public int id { get; set; }
        public string name { get; set; }
        public string contact { get; set; }
        public string address { get; set; }
        public string pincode { get; set; }
        public string city { get; set; }
        public string state { get; set; }
        public string created_at { get; set; }
        public string updated_at { get; set; }
    }
    public class BloodBankPageData
    {
        public int total { get; set; }
        public int per_page { get; set; }
        public int current_page { get; set; }
        public int last_page { get; set; }
        public string next_page_url { get; set; }
        public object prev_page_url { get; set; }
        public int from { get; set; }
        public int to { get; set; }
        public List<BloodBankMainData> data { get; set; }
    }

    public class BloodBankRootClass
    {
        public string status { get; set; }
        public string message { get; set; }
        public BloodBankPageData data { get; set; }
    }

   

    public class BloodBankDoner
    {
            public string full_name { get; set; }
            public string email { get; set; }
            public string age { get; set; }
            public string phone { get; set; }
            public string address { get; set; }
            public string gender { get; set; }
            public string preferred_date { get; set; }
            public string comments { get; set; }
        
    }
     public class BloodDonerResponse: BloodBankDoner
    {
        public int client_id { get; set; }
        public string updated_at { get; set; }
        public string created_at { get; set; }
        public int id { get; set; }
    }
    public class  BloodBankDonerRootResponse
    {
        public string status { get; set; }
        public string message { get; set; }
        public BloodDonerResponse data { get; set; }
    }

}
