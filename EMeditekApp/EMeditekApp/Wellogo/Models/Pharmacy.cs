using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Net.Http;

namespace EMeditekApp.Wellogo.Models
{
    public class Pharmacy
    {
        public string status { get; set; }
        public string message { get; set; }
        [JsonProperty(PropertyName ="data")]
        public PrescriptionData data { get; set; }
    }
     
    public class PrescriptionData
    {
        public int id { get; set; }
        public int client_id { get; set; }
        public PharmacyAddressdump address_dump { get; set; }
        public object prescription_type { get; set; }
        public object medicines { get; set; }
        public int is_successful { get; set; }
        public List<PrescriptionPhotos> photos { get; set; }
        public string created_at { get; set; }
        public string updated_at { get; set; }
    }
    public class PrescriptionPhotos
    {
        public int id { get; set; }
        public string name { get; set; }
        public string slug { get; set; }
        public string mime_type { get; set; }
        public string url { get; set; }
        public string created_at { get; set; }
        public string updated_at { get; set; }
    }

    public class PrescriptionDeleiveryDetails
    {
        public int order_id { get; set; }
        public string name { get; set; }
        public string phone { get; set; }
        public string address { get; set; }
        public string city { get; set; }
        public string state { get; set; }
        public int pin { get; set; }
        public int prescription_type { get; set; }
        public string medicines { get; set; }
    }

    public  class PharmacyAddressdump
    {
        public string name { get; set; }
        public string phone { get; set; }
        public string address { get; set; }
        public string city { get; set; }
        public string state { get; set; }
        public string pin { get; set; }
    }


    public class PharmacyPhotoDelete
    {
        public string status { get; set; }
        public string message { get; set; }
        [JsonProperty(PropertyName = "data")]
        public PhotoDetail data { get; set; }

    }

    public class PhotoDetail
    {
        public int id { get; set; }
        public int order_id { get; set; }
        public string name { get; set; }
        public string slug { get; set; }
        public string mime_type { get; set; }
        public string created_at { get; set; }
        public string updated_at { get; set; }
    }


}
