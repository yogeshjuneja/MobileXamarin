using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMeditekApp.Wellogo.Models
{
    public class UserPassword
    {
        public string old_password { get; set; }
        public string password { get; set; }
        public string password_confirmation { get; set; }
       
    }


    public class PasswordResponse
    {
        public string message { get; set; }
    }

    public class PasswordRootResponse
    {
        public PasswordResponse data { get; set; }
    }

    public class UserPasswordError
    {
      
        public string message { get; set; }
        public Dictionary<string, List<string>> validation { get; set; }
    }

    public class PasswordError
    {
        public UserPasswordError errors { get; set; }
    }






}
