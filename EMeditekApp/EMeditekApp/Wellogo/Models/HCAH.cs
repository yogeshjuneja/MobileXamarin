using System.Collections.Generic;
using Xamarin.Forms;

namespace EMeditekApp.Wellogo.Models
{
    public class HCAH
    {

        string _about = "";
        public int id { get; set; }
        public string name { get; set; }
        public string image { get; set; }


        public string about { get { return DependencyService.Get<IParseHTML>().Parse(_about).ToString();  } set { _about = value; } }


         
        //public string about { get; set; }
    }
    public class HCAHData
    {
        public string status { get; set; }
        public string message { get; set; }
        public List<HCAH> data { get; set; }
    }

    

    #region HCAHForm
    public class Client 
    {
        public int id { get; set; }
        public string name { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public int branch_id { get; set; }
        public string gender { get; set; }
        public string date_of_birth { get; set; }
        public string phone { get; set; }
        public int type { get; set; }
        public int status { get; set; }
        public object is_admin { get; set; }
        public int role_id { get; set; }
        public int seniority_level { get; set; }
        public string blood_group { get; set; }
        public int is_hra_completed { get; set; }
        public string designation { get; set; }
        public int is_phone_verified { get; set; }
        public object otp { get; set; }
        public object remember_token { get; set; }
        public string logged_in_at { get; set; }
        public object last_transaction_at { get; set; }
        public string created_at { get; set; }
        public string updated_at { get; set; }
    }

    public class UserData : FormFields
    {
       
        public int amount { get; set; }
        public int is_payment_successful { get; set; }
        public int client_id { get; set; }
        public string updated_at { get; set; }
        public string created_at { get; set; }
        public int id { get; set; }
        public Client client { get; set; }
    }

    public class HCAHFormResponse
    {
        public string status { get; set; }
        public string message { get; set; }
        public UserData data { get; set; }
    }


    public class FormFields
    {
        public string name { get; set; }
        public string age { get; set; }
        public string phone { get; set; }
        public string medical_condition { get; set; }
        public string location { get; set; }
        public string gender { get; set; }
        public int service_id { get; set; }
    }
    #endregion

}
