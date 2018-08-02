using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace EMeditekApp.Wellogo.Models
{
   public  class CommonApiParameters
    {
       
        [JsonProperty("Consumer-Token")]
        public int ConsumerToken { get; set; }
        [JsonProperty("Current-Timestamp")]
        public int CurrentTimestamp { get; set; }
        [JsonProperty("Authorization")]
        public int Authorization { get; set; }
       
    }
}
