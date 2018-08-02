using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMeditekApp.Wellogo.Models
{
  public  class ErrorResponse
    {
        public string status { get; set; }
        public string message { get; set; }
        public List<string> data { get; set; }
    }

    public class LoginResponseError
    {
        public Errors errors { get; set; }
    }
    public class Errors
    {
        public string message { get; set; }
    }

  
    public class AHCError
    {
        public string status { get; set; }
        public string message { get; set; }
        public Dictionary<string, List<string>> data { get; set; }
    }



}
