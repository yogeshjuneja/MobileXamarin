using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMeditekApp.Wellogo.Models
{
    public class AmbulanceServicesModel 
    {
        public string status { get; set; }
        public string message { get; set; }
        public List<BloodBankMainData> data { get; set; }
    }
}
