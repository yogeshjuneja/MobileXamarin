using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EMeditekApp.Wellogo;

namespace EMeditekApp.Wellogo
{
public class AHCCheckout :
    {

    }
    public class Price
    {
        public int id { get; set; }
        public int corporate_id { get; set; }
        public int dc_id { get; set; }
        public int test_id { get; set; }
        public int price { get; set; }
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

    public class State
    {
        public int id { get; set; }
        public int state_id { get; set; }
        public string name { get; set; }
        public string created_at { get; set; }
        public string updated_at { get; set; }
    }

    public class City
    {
        public int id { get; set; }
        public int city_id { get; set; }
        public int state_id { get; set; }
        public string name { get; set; }
        public string created_at { get; set; }
        public string updated_at { get; set; }
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

    public class Patient
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

    public class Data
    {
        public int id { get; set; }
        public int client_id { get; set; }
        public string updated_at { get; set; }
        public string created_at { get; set; }
        public int budget { get; set; }
        public int tests_total_amount { get; set; }
        public List<Patient> patients { get; set; }
    }

    public class RootObject
    {
        public string status { get; set; }
        public string message { get; set; }
        public Data data { get; set; }
    }
}
