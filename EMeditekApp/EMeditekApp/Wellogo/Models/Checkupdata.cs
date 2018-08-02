using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMeditekApp.Wellogo.Models
{

    public class MainAttribute
    {
        public string status { get; set; }
        public string message { get; set; }
    }
    public class Checkupdata : MainAttribute
    {

        public CheckupResponse data { get; set; }

    }
    public class CheckupResponse
    {
        public Checkup checkup { get; set; }
        public Patient2 patient { get; set; }
        public Corporate corporate { get; set; }
        public List<PatientsBudget> patients_budget { get; set; }
        public List<PatientsWithBudget> patients_with_budget { get; set; }
        public Patient previous_patient { get; set; }
        public Patient next_patient { get; set; }
        public List<State> states { get; set; }
    }
    public class Checkup
    {
        public int id { get; set; }
        public int client_id { get; set; }
        public string created_at { get; set; }
        public string updated_at { get; set; }
        public object tests_total_amount { get; set; }
        public List<Patient> patients { get; set; }
        public Client client { get; set; }
        public List<object> tests { get; set; }
    }
    public class ErpRegistration
    {
    }
    public class Patient
    {
        public int id { get; set; }
        public int checkup_id { get; set; }
        public string name { get; set; }
        public int relation { get; set; }
        public object employee_code { get; set; }
        public object seniority_level { get; set; }
        public string phone { get; set; }
        public string date_of_birth { get; set; }
        public int marital_status { get; set; }
        public int gender { get; set; }
        public ErpRegistration erp_registration { get; set; }
        public string created_at { get; set; }
        public string updated_at { get; set; }
        public object tests_total_amount { get; set; }
        public List<object> tests { get; set; }
    }
    public class Checkup2
    {
        public int id { get; set; }
        public int client_id { get; set; }
        public string created_at { get; set; }
        public string updated_at { get; set; }
        public object tests_total_amount { get; set; }
        public Client client { get; set; }
        public List<Patient> patients { get; set; }
        public List<object> tests { get; set; }
    }
    public class Patient2 : Patient
    {

        public object dc { get; set; }
        public Checkup checkup { get; set; }
    }
    public class City
    {
        public int id { get; set; }
        public string name { get; set; }
        public int state_id { get; set; }
        public int is_active { get; set; }
        public string created_at { get; set; }
        public string updated_at { get; set; }

    }
    public class CityID : City
    {
        public int city_id { get; set; }
    }
    public class CityReponse
    {
        public string status { get; set; }
        public string message { get; set; }
        public List<CityID> data { get; set; }
    }
    public class Branch
    {
        public int id { get; set; }
        public string name { get; set; }
        public int corporate_id { get; set; }
        public int city_id { get; set; }
        public string created_at { get; set; }
        public string updated_at { get; set; }
        public City city { get; set; }
    }
    public class Pivot
    {
        public int corporate_id { get; set; }
        public int service_id { get; set; }
        public int id { get; set; }
        public string created_at { get; set; }
        public string updated_at { get; set; }
    }
    public class Service
    {
        public int id { get; set; }
        public string name { get; set; }
        public string slug { get; set; }
        public int sequence { get; set; }
        public string created_at { get; set; }
        public string updated_at { get; set; }
        public Pivot pivot { get; set; }
    }
    public class Meta
    {
        public int id { get; set; }
        public int corporate_id { get; set; }
        public int bundle_id { get; set; }
        public bool is_test_compulsory { get; set; }
        public int is_hra_available { get; set; }
        public int is_a_la_carte_available { get; set; }
        public int blocked_days { get; set; }
        public int relation_only { get; set; }
        public string escalation_matrix { get; set; }
        public object appointment_date_range_start { get; set; }
        public object appointment_date_range_end { get; set; }
        public string created_at { get; set; }
        public string updated_at { get; set; }
    }
    public class Corporate
    {
        public int id { get; set; }
        public string name { get; set; }
        public string slug { get; set; }
        public int status { get; set; }
        public string no_of_employees { get; set; }
        public int logo_show { get; set; }
        public string created_at { get; set; }
        public string updated_at { get; set; }
        public int hide_unsubscribed_services { get; set; }
        public int emeditek_corporate_id { get; set; }
        public int is_gamification_available { get; set; }
        public object email_domain { get; set; }
        public int is_seniority_level_available { get; set; }
        public string logo { get; set; }
        public List<Branch> branches { get; set; }
        public List<Service> services { get; set; }
        public Meta meta { get; set; }


    }
    public class PatientsBudget
    {
        public Checkup checkup { get; set; }
        public Patient patient { get; set; }
        public int total { get; set; }
        public int utilised { get; set; }
        public int remaining { get; set; }
    }
    public class PatientsWithBudget
    {
        public int id { get; set; }
        public int checkup_id { get; set; }
        public string name { get; set; }
        public int relation { get; set; }
        public object employee_code { get; set; }
        public object seniority_level { get; set; }
        public string phone { get; set; }
        public string date_of_birth { get; set; }
        public int marital_status { get; set; }
        public int gender { get; set; }
        public ErpRegistration erp_registration { get; set; }
        public string created_at { get; set; }
        public string updated_at { get; set; }
        public object tests_total_amount { get; set; }
        public List<object> tests { get; set; }
    }
    public class State
    {
        public int id { get; set; }
        public int state_id { get; set; }
        public string name { get; set; }
        public string created_at { get; set; }
        public string updated_at { get; set; }
    }
    public class DCResponse : MainAttribute
    {
        public List<DC> data { get; set; }

    }
    public class DC
    {
        public int id { get; set; }
        public string mc_code { get; set; }
        public string name { get; set; }
        public int city_id { get; set; }
        public string address { get; set; }
        public string created_at { get; set; }
        public string updated_at { get; set; }
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
    public class RequiredTest
    {
        public int id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public string created_at { get; set; }
        public string updated_at { get; set; }
        public bool @checked { get; set; }
        public Price price { get; set; }
    }
    public class TestData
    {
        public List<RequiredTest> required_tests { get; set; }
        public dynamic questionnaires_recommended_tests { get; set; }
        public dynamic remaining_tests { get; set; }
        public dynamic patient_selected_tests { get; set; }
    }
    public class Tests
    {
        public string status { get; set; }
        public string message { get; set; }
        public TestData data { get; set; }
    }

    public class SaveTest
    {
        public List<int> required_tests { get; set; }
        public List<int?> questionnaires_recommended_tests { get; set; }
        public List<int?> remaining_tests { get; set; }
        public string appointment_date { get; set; }

        public int state_id { get; set; }
        public int city_id { get; set; }
        public int dc_id { get; set; }


    }
    public class SaveTestResponse: MainAttribute
    {
        public object data { get; set; }
    }
}
