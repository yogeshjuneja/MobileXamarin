using SQLite;
 

namespace EMeditekApp.Models
{
    [Table("LoggedInUser")]
    public class LoggedInUser
    {
         
        [PrimaryKey]
        public int id { get; set; }
        public string sso_token { get; set; }
        public string photo_url { get; set; }
        public string Name { get; set; }
        public string Designation { get; set; }
         public string CorporateLogo { get; set; }
        public string Gender { get; set; }
        public int CorporateID { get; set; }
        //public string MaritalStatus { get; set; } 
    }

    [Table("Usermenu")]
    public class UserMenu
    {
        public int id { get; set; }
        public string name { get; set; }

        public int sequence { get; set; }
    }
}
