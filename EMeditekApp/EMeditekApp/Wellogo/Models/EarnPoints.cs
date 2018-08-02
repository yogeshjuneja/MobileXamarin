using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMeditekApp.Wellogo.Models
{
    //class EarnPoints
    //{
    //}
    public class EarnPoints
    {
        public List<EarnPointsDatum> data { get; set; }
    }
    public class EarnPointsPivot
    {
        public int corporate_id { get; set; }
        public int points_action_id { get; set; }
        public double points { get; set; }
        public string created_at { get; set; }
        public string updated_at { get; set; }
    }

    public class EarnPointsDatum
    {
        public int id { get; set; }
        public string name { get; set; }
        public string slug { get; set; }
        public string created_at { get; set; }
        public string updated_at { get; set; }
        public EarnPointsPivot pivot { get; set; }
    }

   
}
