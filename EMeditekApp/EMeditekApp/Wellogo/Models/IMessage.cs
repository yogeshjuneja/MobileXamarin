using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
 


namespace EMeditekApp.Wellogo.Models
{
  public  interface IMessage 
    {
        void LongAlert(string message= "Something went wrong, dont' worry our engineers are fixing it right now ! May be your internet connect is not working");
        void ShortAlert(string message= "Something went wrong, dont' worry our engineers are fixing it right now ! May be your internet connect is not working");
    }
}
