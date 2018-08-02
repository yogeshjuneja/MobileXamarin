using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMeditekApp.Wellogo.Models
{


    #region KyorRegistration
    public  class KyorRegistration
    {
        public int branch_id { get; set; }
        public string name { get; set; }
        public string unique_id { get; set; }
        public string gender { get; set; }
        public string date_of_birth { get; set; }
        public string password { get; set; }
    }
    public class KyorRegistrationOutputData
    {
        public string branch_id { get; set; }
        public string name { get; set; }
        public string unique_id { get; set; }
        public string gender { get; set; }
        public string date_of_birth { get; set; }
        public string updated_at { get; set; }
        public string created_at { get; set; }
        public int id { get; set; }
        public int age { get; set; }
        public string photo_url { get; set; }
        public int corporate_id { get; set; }
        public object employee_code { get; set; }
        public string sso_token { get; set; }
    }
    public class KyorRegistrationOutput
    {
        public KyorRegistrationOutputData data { get; set; }
    }
    #endregion
    #region KyorLogin
    public class KyorLogin
    {
        public string password { get; set; }
        
        public string email { get; set; }
    }

 
    public class KyorLoginPivot
    {
        public int corporate_id { get; set; }
        public int service_id { get; set; }
        public int id { get; set; }
        public string created_at { get; set; }
        public string updated_at { get; set; }
    }

    public class KyorLoginService
    {
        public int id { get; set; }
        public string name { get; set; }
        public string slug { get; set; }
        public int sequence { get; set; }
        public string created_at { get; set; }
        public string updated_at { get; set; }
        public KyorLoginPivot pivot { get; set; }
    }

    public class KyorLoginCorporate
    {
        public int id { get; set; }
        public string name { get; set; }
        public string slug { get; set; }
        public int status { get; set; }
        public string no_of_employees { get; set; }
        public int logo_show { get; set; }
        public string created_at { get; set; }
        public string updated_at { get; set; }
        public int hide_unsubscribed_services { get; set; }
        public int emeditek_corporate_id { get; set; }
        public int is_gamification_available { get; set; }
        public object email_domain { get; set; }
        public int is_seniority_level_available { get; set; }
        public object secret { get; set; }
        public string logo { get; set; }
        public List<KyorLoginService> services { get; set; }
    }

    public class KyorLoginBranch
    {
        public int id { get; set; }
        public string name { get; set; }
        public int corporate_id { get; set; }
        public int city_id { get; set; }
        public string created_at { get; set; }
        public string updated_at { get; set; }
        public KyorLoginCorporate corporate { get; set; }
    }
    public class KyorLoginData
    {
        public  int id { get; set; }
        public string name { get; set; }
        public string unique_id { get; set; }
        public object email { get; set; }
        public int branch_id { get; set; }
        public string gender { get; set; }
        public string date_of_birth { get; set; }
        public object phone { get; set; }
        public int type { get; set; }
        public int status { get; set; }
        public int is_admin { get; set; }
        public int role_id { get; set; }
        public object seniority_level { get; set; }
        public object blood_group { get; set; }
        public int is_hra_completed { get; set; }
        public object designation { get; set; }
        public int is_phone_verified { get; set; }
        public object otp { get; set; }
        public string points { get; set; }
        public object relation_id { get; set; }
        public object client_id { get; set; }
        public object metas { get; set; }
        public int can_invite_members { get; set; }
        public object logged_in_at { get; set; }
        public object last_transaction_at { get; set; }
        public string created_at { get; set; }
        public string updated_at { get; set; }
        public int? age { get; set; }
        public string photo_url { get; set; }
        public int corporate_id { get; set; }
        public object employee_code { get; set; }
        public string sso_token { get; set; }
        public KyorLoginBranch branch { get; set; }
        public object info { get; set; }
        public object tracker { get; set; }
    }

    public class KyorLoginResponse
    {
        public KyorLoginData data { get; set; }
    }

    #endregion

    #region CheckKyorRegistration

    public class KyorLoginCheckResponse
    {
        public string Message { get; set; }
        public KyorLoginCheck data { get; set; }

    }
    public class KyorLoginCheck
    {
        public int KyorID { get; set; }
    }
    public class KyorLoginCheckinput
    {
        public string loginType { get; set; }
        public string vchCardNo { get; set; }
        public int intKyorID { get; set; }



    }
    #endregion





}
