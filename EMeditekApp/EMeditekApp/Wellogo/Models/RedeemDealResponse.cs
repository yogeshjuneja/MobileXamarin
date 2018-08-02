using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMeditekApp.Wellogo.Models
{
    public class RedeemDealResponse
    {
        public RedeemDealResponseData data { get; set; }
    }
    public class RedeemDealResponseCorporate
    {
        public int id { get; set; }
        public string name { get; set; }
        public string slug { get; set; }
        public int status { get; set; }
        public int block_field_status { get; set; }
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
    }

    public class RedeemDealResponseBranch
    {
        public int id { get; set; }
        public string name { get; set; }
        public int corporate_id { get; set; }
        public int city_id { get; set; }
        public string created_at { get; set; }
        public string updated_at { get; set; }
        public Corporate corporate { get; set; }
    }

    public class RedeemDealResponseData
    {
        public int id { get; set; }
        public string name { get; set; }
        public string unique_id { get; set; }
        public string email { get; set; }
        public int branch_id { get; set; }
        public string gender { get; set; }
        public string marital_status { get; set; }
        public string date_of_birth { get; set; }
        public string phone { get; set; }
        public int type { get; set; }
        public int status { get; set; }
        public int is_admin { get; set; }
        public int role_id { get; set; }
        public int seniority_level { get; set; }
        public string blood_group { get; set; }
        public int is_hra_completed { get; set; }
        public string designation { get; set; }
        public int is_phone_verified { get; set; }
        public object otp { get; set; }
        public double points { get; set; }
        public object relation_id { get; set; }
        public object client_id { get; set; }
        public object metas { get; set; }
        public int can_invite_members { get; set; }
        public object client_relation_id { get; set; }
        public string logged_in_at { get; set; }
        public string logged_out_at { get; set; }
        public int first_login_column { get; set; }
        public object last_transaction_at { get; set; }
        public string created_at { get; set; }
        public string updated_at { get; set; }
        public int age { get; set; }
        public string photo_url { get; set; }
        public int corporate_id { get; set; }
        public string employee_code { get; set; }
        public RedeemDealResponseBranch branch { get; set; }
    }


    #region ErrorResponse
    public class RedeemDealValidation
    {
        public List<string> value { get; set; }
    }

    public class RedeemDealErrors
    {
        public string message { get; set; }
        public RedeemDealValidation validation { get; set; }
    }

    public class RedeemDealError
    {
        public RedeemDealErrors errors { get; set; }
    }
    #endregion

}
