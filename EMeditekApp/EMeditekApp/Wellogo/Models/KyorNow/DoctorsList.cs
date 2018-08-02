using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMeditekApp.Wellogo.Models.KyorNow
{
    //class DoctorsList
    //{
    //}

    public class DoctorsList
    {
        public List<Datum> data { get; set; }
        public Meta meta { get; set; }
    }

    public class Specialities
    {
        public List<object> data { get; set; }
    }

    public class Data
    {
        public int id { get; set; }
        public string name { get; set; }
        public int is_background_verified { get; set; }
        public int audio_active { get; set; }
        public int is_in_house { get; set; }
        public int practicing_since { get; set; }
        public int video_active { get; set; }
        public int msg_active { get; set; }
        public string phone { get; set; }
        public int type { get; set; }
        public string gender { get; set; }
        public string medical_registration_number { get; set; }
        public int audio_charge_min { get; set; }
        public int video_charge_min { get; set; }
        public string bio { get; set; }
        public object nearest_location { get; set; }
        public string created_at { get; set; }
        public Specialities specialities { get; set; }
    }

    public class Profile
    {
        public Data data { get; set; }
    }

    public class Languages
    {
        public List<object> data { get; set; }
    }

    public class Education
    {
        public List<object> data { get; set; }
    }

    public class Reviews
    {
        public List<object> data { get; set; }
    }

    public class Datum
    {
        public int id { get; set; }
        public string name { get; set; }
        public string email { get; set; }
        public int wallet_amount { get; set; }
        public bool is_client { get; set; }
        public string avatar { get; set; }
        public bool is_online { get; set; }
        public double? rating { get; set; }
        public object distance { get; set; }
        public int reviews_count { get; set; }
        public Profile profile { get; set; }
        public Languages languages { get; set; }
        public Education education { get; set; }
        public Reviews reviews { get; set; }
    }

    public class Pagination
    {
        public int total { get; set; }
        public int count { get; set; }
        public int per_page { get; set; }
        public int current_page { get; set; }
        public int total_pages { get; set; }
        public List<object> links { get; set; }
    }

    public class Meta
    {
        public Pagination pagination { get; set; }
    }

   
}
