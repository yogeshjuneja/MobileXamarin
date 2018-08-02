using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace EMeditekApp.Wellogo.Models
{
    public class HealthStats
    {
        public Data data { get; set; }
    }
    public class Result
    {
        [JsonProperty(PropertyName = "title")]
        public string title { get; set; }
        [JsonProperty(PropertyName = "selector")]
        public string  selector { get; set; }
        [JsonProperty(PropertyName = "data")]
        public Dictionary<string ,int> data { get; set; }
        [JsonProperty(PropertyName = "max_value")]
        public int MAxValue { get; set; }
    }

    public class Result2
    {
        [JsonProperty(PropertyName = "title")]
        public string title { get; set; }
        [JsonProperty(PropertyName = "selector")]
        public string selector { get; set; }
        [JsonProperty(PropertyName = "data")]
        public Dictionary<string, double> data { get; set; }
        [JsonProperty(PropertyName = "max_value")]
        public int MAxValue { get; set; }
    }

    public class Data
    {
        [JsonProperty(PropertyName = "cholesterol")]
        public Result cholesterol { get; set; }
        [JsonProperty(PropertyName = "sugar")]
        public Result sugar { get; set; }
        [JsonProperty(PropertyName = "bmi")]
        public Result2 bmi { get; set; }
        [JsonProperty(PropertyName = "blood_pressure_systolic")]
        public Result blood_pressure_systolic { get; set; }
        [JsonProperty(PropertyName = "blood_pressure_diastolic")]
        public Result blood_pressure_diastolic { get; set; }
    }

     
}
