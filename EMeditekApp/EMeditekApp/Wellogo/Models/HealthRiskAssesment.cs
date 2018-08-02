using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Xamarin.Forms;

namespace EMeditekApp.Wellogo.Models
{


    public class HealthRiskAssesment
    {
        string _icon = "";
        string _Status = "";
        private Color clr;
        [JsonProperty("id")]
        public int ID { get; set; }
        [JsonProperty("client_id")]
        public int ClientID { get; set; }
        [JsonProperty("status")]
        public string Status
        {
            get
            {
                return _Status == "" | _Status == "0" ? "Incomplete" : "Completed";
            }
            set
            {
                _icon = value.ToString() == "" || value.ToString() == "0" ? "ion-android-alert" : "ion-android-checkmark-circle";
                _Status = value.ToString();
                // clr = value.ToString() == "" || value.ToString() == "0" ? Color.Red : Color.Green;
                Color = value.ToString() == "" || value.ToString() == "0" ? Color.Red : Color.Green;
            }
        }
        [JsonProperty("created_at")]
        public DateTime CreatedAt { get; set; }
        [JsonProperty("updated_at")]
        public string UpdatetedAt { get; set; }

        public string icon { get { return _icon; } set { } }
        public Color Color
        { get; set; }

    }
    public class HealthRiskAssesmentList
    {
        [JsonProperty("data")]
        public List<HealthRiskAssesment> lstHealthRiskAssesment { get; set; }
    }
}
