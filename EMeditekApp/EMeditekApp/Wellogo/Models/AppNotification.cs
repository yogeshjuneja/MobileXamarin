using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMeditekApp.Wellogo.Models
{
    public class AppNotification
    {
        public AppNotificationData data { get; set; }
    }

    public class AppNotificationPivot
    {
        public int client_id { get; set; }
        public int notification_id { get; set; }
    }

    public class AppNotificationDatum
    {
        public int id { get; set; }
        public string title { get; set; }
        public string message { get; set; }
        public int? corporate_id { get; set; }
        public string scheduled_at { get; set; }
        public string sent_at { get; set; }
        public string created_at { get; set; }
        public string updated_at { get; set; }
        public AppNotificationPivot pivot { get; set; }
    }

    public class AppNotificationData
    {
        public int total { get; set; }
        public int per_page { get; set; }
        public int current_page { get; set; }
        public int last_page { get; set; }
        public string next_page_url { get; set; }
        public object prev_page_url { get; set; }
        public int from { get; set; }
        public int to { get; set; }
        public List<AppNotificationDatum> data { get; set; }
    }

  
}
