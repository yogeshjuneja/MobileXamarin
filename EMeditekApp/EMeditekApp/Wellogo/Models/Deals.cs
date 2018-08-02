using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMeditekApp.Wellogo.Models
{
    public class Deals
    {
        public DealsData data { get; set; }
    }
    public class DealsPhoto
    {
        public int id { get; set; }
        public string name { get; set; }
        public int type { get; set; }
        public string mime_type { get; set; }
        public int imageable_id { get; set; }
        public string imageable_type { get; set; }
        public string created_at { get; set; }
        public string updated_at { get; set; }
    }

    public class DealsDatum
    {
        public int id { get; set; }
        public int partner_id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public int points { get; set; }
        public string created_at { get; set; }
        public string updated_at { get; set; }
        public DealsPhoto photo { get; set; }
    }

    public class DealsData
    {
        public int total { get; set; }
        public int per_page { get; set; }
        public int current_page { get; set; }
        public int last_page { get; set; }
        public string next_page_url { get; set; }
        public object prev_page_url { get; set; }
        public int from { get; set; }
        public int to { get; set; }
        public List<DealsDatum> data { get; set; }
    }

    public class SingleDeal
    {
        public DealsDatum data { get; set; }
    }
}
