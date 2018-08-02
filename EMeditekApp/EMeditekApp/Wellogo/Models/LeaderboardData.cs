using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMeditekApp.Wellogo.Models
{
    public class LeaderboardData
    {
        public LeaderBoardPage data { get; set; }
    }

    public class LeaderBoardPage
    {
        public int count { get; set; }
        public List<UsersLeaderBoard> result { get; set; }
        public int idWithRankOne { get; set; }
    }

    public class UsersLeaderBoard
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
        public int? seniority_level { get; set; }
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
        public int rank { get; set; }
        public int? age { get; set; }
        public string photo_url { get; set; }
        public int corporate_id { get; set; }
        public string employee_code { get; set; }
    }


    #region LeaderBoardLog


    public class LeaderBoardLogdata
    {
        string _blogstring = "";
        public int id { get; set; }
        public int client_id { get; set; }
        public int value { get; set; }
        public int type
        {
            get
            {
                return this.type;

            }
            set
            {
                if (value == 1)
                {
                    _blogstring = this.value + " Koins Received";
                }
                else
                {
                    _blogstring = this.value + " Koins Deducted";
                }
            }
        }
        public string comment { get; set; }
        public int? action_id { get; set; }
        public string created_at { get; set; }
        public string updated_at { get; set; }
        public string BlogString
        {
            get
            {
                return _blogstring;
            }
            set
            {
                _blogstring = value;
            }
        }



    }

    public class LeaderBoardLog
    {
        public int total { get; set; }
        public int per_page { get; set; }
        public int current_page { get; set; }
        public int last_page { get; set; }
        public string next_page_url { get; set; }
        public object prev_page_url { get; set; }
        public int from { get; set; }
        public int to { get; set; }
        public List<LeaderBoardLogdata> data { get; set; }
    }


    public class LeaderBoardLogRootObject
    {
        public LeaderBoardLog data { get; set; }
    }
    #endregion
}
