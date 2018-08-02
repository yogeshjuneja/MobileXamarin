using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMeditekApp.Wellogo.Models
{
  public  class UpdateUserDetails
    {
        public string name { get; set; }
        public string gender { get; set; }
        public string date_of_birth { get; set; }
        public string email { get; set; }
        public string employee_id { get; set; }
        public string password { get; set; }
        public string phone { get; set; }
        public string designation { get; set; }
        public string blood_group { get; set; }
        public string seniority_level { get; set; }
        public UserDetailsInfo info { get; set; }
    }

    public class UserDetailsInfo
    {
        public string employee_id { get; set; }
        public string emergency_number { get; set; }
    }
}
