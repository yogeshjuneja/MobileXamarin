using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMeditekApp.Wellogo.Models
{
    public  class OrderSummary :MainAttribute
    {
        public OrderSummaryData data { get; set; }
    }

    public class AHCPlaceOrder : MainAttribute
    {
        public AHCPlaceOrderData Data { get; set; }
    }
    public class AHCPlaceOrderData
    {
        public int id { get; set; }
        public int total_amount { get; set; }
        public int payable_amount { get; set; }
        public int discount_amount { get; set; }
        public int is_payment_successful { get; set; }
        public string created_at { get; set; }
        public string updated_at { get; set; }
    }

    public class TestDump
    {
        public int id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public string created_at { get; set; }
        public string updated_at { get; set; }
        public Price price { get; set; }
    }
    public class Test
    {
        public int id { get; set; }
        public int patient_checkup_id { get; set; }
        public TestDump test_dump { get; set; }
        public int amount { get; set; }
        public int selection_type { get; set; }
        public string created_at { get; set; }
        public string updated_at { get; set; }
    }
    public class OrderSummaryCity:City
    {
        public State state { get; set; }
    }
    public class DcDump
    {

        public int id { get; set; }
        public string mc_code { get; set; }
        public string name { get; set; }
        public int city_id { get; set; }
        public string address { get; set; }
        public string created_at { get; set; }
        public string updated_at { get; set; }
        public City city { get; set; }
    }
    public class Dc
    {
        public int id { get; set; }
        public int patient_checkup_id { get; set; }
        public DcDump dc_dump { get; set; }
        public string appointment_date { get; set; }
        public string created_at { get; set; }
        public string updated_at { get; set; }
    }
    public class OrderSummaryPatient
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
        public List<Test> tests { get; set; }
        public Dc dc { get; set; }
    }
    public class OrderSummaryData
    {
        public int id { get; set; }
        public int client_id { get; set; }
        public string updated_at { get; set; }
        public string created_at { get; set; }
        public int budget { get; set; }
        public int tests_total_amount { get; set; }
        public List<OrderSummaryPatient> patients { get; set; }
    }
    


}
