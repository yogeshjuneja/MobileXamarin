using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMeditekApp.Wellogo.Models
{
   public class StateCity
    {
        public string status { get; set; }
        public string message { get; set; }
        public List<string> data { get; set; }
    }

    public enum eKyorType
    {
        BloodBank,Emergency
    }
}
