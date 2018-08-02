using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMeditekApp.Wellogo.Models
{
  public   class AhcCheckout :MainAttribute
    {

        public AHCCheckoutData data { get; set; }
    }
    public class AHCCheckoutPrice
    {
        public int id { get; set; }
        public int corporate_id { get; set; }
        public int dc_id { get; set; }
        public int test_id { get; set; }
        public int price { get; set; }
        public string created_at { get; set; }
        public string updated_at { get; set; }
    }

    public class AHCCheckoutTestDump
    {
        public int id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public string created_at { get; set; }
        public string updated_at { get; set; }
        public AHCCheckoutPrice price { get; set; }
    }

    public class AHCCheckoutTest
    {
        public int id { get; set; }
        public int patient_checkup_id { get; set; }
        public AHCCheckoutTestDump test_dump { get; set; }
        public int amount { get; set; }
        public int selection_type { get; set; }
        public string created_at { get; set; }
        public string updated_at { get; set; }
    }
     
    public class AHCCheckoutDcDump
    {
        public int id { get; set; }
        public string mc_code { get; set; }
        public string name { get; set; }
        public int city_id { get; set; }
        public string address { get; set; }
        public string created_at { get; set; }
        public string updated_at { get; set; }
        public OrderSummaryCity city { get; set; }
    }

    public class AHCCheckoutDc
    {
        public int id { get; set; }
        public int patient_checkup_id { get; set; }
        public AHCCheckoutDcDump dc_dump { get; set; }
        public string appointment_date { get; set; }
        public string created_at { get; set; }
        public string updated_at { get; set; }
    }

    public class AHCcheckoutPatient
    {
        public int id { get; set; }
        public string name { get; set; }
        public string employee_code { get; set; }
        public string seniority_level { get; set; }
        public string phone { get; set; }
        public string date_of_birth { get; set; }
        public string marital_status { get; set; }
        public string gender { get; set; }
        public string relation { get; set; }
        public List<AHCCheckoutTest> tests { get; set; }
        public Dc dc { get; set; }
    }

    public class AHCCheckoutData
    {
        public int id { get; set; }
        public int client_id { get; set; }
        public string updated_at { get; set; }
        public string created_at { get; set; }
        public int budget { get; set; }
        public int tests_total_amount { get; set; }
        public List<AHCcheckoutPatient> patients { get; set; }
    }

   
}
