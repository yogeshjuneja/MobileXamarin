using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

 
namespace EMeditekApp.Wellogo.Models
{
    public class HRAModel
    {
        [JsonProperty(PropertyName = "data")]
        public DataHRA data { get; set; }
    }
    public class DataHRA
    {
        public int client_id { get; set; }
        public string updated_at { get; set; }
        public string created_at { get; set; }
        public int id { get; set; }
    }
    public class HRAQuestions
    {
        public Dictionary<string,string> data { get; set; }
    }
    public class HRAClient
    {
        public int id { get; set; }
        public string name { get; set; }
        public string email { get; set; }
        public int branch_id { get; set; }
        public string gender { get; set; }
        public string date_of_birth { get; set; }
        public string phone { get; set; }
        public int type { get; set; }
        public int status { get; set; }
        public int is_admin { get; set; }
        public int role_id { get; set; }
        public int seniority_level { get; set; }
        public string blood_group { get; set; }
        public int is_hra_completed { get; set; }
        public string designation { get; set; }
        public int is_phone_verified { get; set; }
        public object otp { get; set; }
        public string logged_in_at { get; set; }
        public object last_transaction_at { get; set; }
        public string created_at { get; set; }
        public string updated_at { get; set; }
    }
      
    public class PhysicalExamination: PhysicalExaminationInput
    {
        public int id { get; set; }
        public int hra_id { get; set; }
        public string created_at { get; set; }
        public string updated_at { get; set; }
    }
    public class PhysicalExaminationInput
    {
       
        public int? height_feet { get; set; }
        public int? height_inches { get; set; }
        public int? height_cms { get; set; }
        public int height_dont_know { get; set; }
        public double? weight_kgs { get; set; }
        public int? weight_dont_know { get; set; }
        public double? bmi { get; set; }
        public int? blood_pressure_systolic { get; set; }
        public int blood_pressure_systolic_dont_know { get; set; }
        public int? blood_pressure_diastolic { get; set; }
        public int blood_pressure_diastolic_dont_know { get; set; }
        public string blood_group { get; set; }
    
    }
    public class Investigations
    {
        public int id { get; set; }
        public int hra_id { get; set; }
        public object haemoglobin { get; set; }
        public int haemoglobin_dont_know { get; set; }
        public object total_cholesterol { get; set; }
        public int total_cholesterol_dont_know { get; set; }
        public object blood_sugar { get; set; }
        public int blood_sugar_dont_know { get; set; }
        public string created_at { get; set; }
        public string updated_at { get; set; }
    }
    public class FamilyHistory: FamilyHistoryInput
    {
        public int id { get; set; }
        public int hra_id { get; set; }
        public string created_at { get; set; }
        public string updated_at { get; set; }
    }

    public class FamilyHistoryInput
    {

        public int obesity { get; set; }
        public int high_blood_pressure { get; set; }
        public int high_cholesterol { get; set; }
        public int heart_attack { get; set; }
        public int diabetes { get; set; }
        public int stroke { get; set; }
        public int cancer { get; set; }
    
    }
    public class PersonalHealth: PersonalHealthInput
    {
        public int id { get; set; }
        public int hra_id { get; set; }
        public string created_at { get; set; }
        public string updated_at { get; set; }
    }

    public class PersonalHealthInput
    {
        public int high_blood_pressure { get; set; }
        public int high_cholesterol { get; set; }
        public int diabetes { get; set; }
        public int anaemia { get; set; }
        public int digestive_problems { get; set; }
        public int allergic_problems { get; set; }
        public int pain_in_joints { get; set; }
        public int headaches { get; set; }
        public int sleep_disorders { get; set; }
        public int skin_problems { get; set; }
        public int heart_problems { get; set; }
    }
    public class Nutrition: NuritionInput
    {
        public int id { get; set; }
        public int hra_id { get; set; }
        public string created_at { get; set; }
        public string updated_at { get; set; }
    }

    public class NuritionInput
    {
        public int take_meals_on_time { get; set; }
        public int skip_meals { get; set; }
        public int heavy_dinner { get; set; }
    }
    public class Fitness :FitnessInput
    {
        public int id { get; set; }
        public int hra_id { get; set; }
        public string created_at { get; set; }
        public string updated_at { get; set; }

      
        
    }

    public class FitnessInput
    {
        public int exercise { get; set; }
    }
    public class Stress : StressInput
    {
        public int id { get; set; }
        public int hra_id { get; set; }
    
        public string created_at { get; set; }
        public string updated_at { get; set; }
    }

    public class StressInput
    {
        public int satisfied_with_work { get; set; }
        public int less_time_for_family { get; set; }
    }
    public class Habits : HabitsInput
    {
        public int id { get; set; }
        public int hra_id { get; set; }
        public string created_at { get; set; }
        public string updated_at { get; set; }
    }


    public class HabitsInput
    {
        public int smoke { get; set; }
        public int alchohol { get; set; }
        public int tobacco { get; set; }
    }
    public class FullHRA
    {
        public int id { get; set; }
        public int client_id { get; set; }
        public int status { get; set; }
        public string created_at { get; set; }
        public string updated_at { get; set; }
        public Client client { get; set; }
        public PhysicalExamination physical_examination { get; set; }
        public Investigations investigations { get; set; }
        public FamilyHistory family_history { get; set; }
        public PersonalHealth personal_health { get; set; }
        public Nutrition nutrition { get; set; }
        public Fitness fitness { get; set; }
        public Stress stress { get; set; }
        public Habits habits { get; set; }
        public FemaleSpecific female_specific { get; set; }
    }
    public class FemaleSpecific
    {
        public int pregnant { get; set; }
        public int breast_feeding { get; set; }
        public int discomfort_from_menstruation { get; set; }
        public int menopause { get; set; }

    }
    public class HRA
    {
        public FullHRA data { get; set; }
    }





    #region HRAErrors


    public class HRAValidation
    {
        public List<string> height_feet { get; set; }
        public List<string> height_inches { get; set; }
        public List<string> height_cms { get; set; }
        public List<string> weight_kgs { get; set; }
        public List<string> blood_pressure_systolic { get; set; }
        public List<string> blood_pressure_diastolic { get; set; }
        public List<string> blood_group { get; set; }
    }

    public class HRAErrors
    {
        public string messsage { get; set; }
        public HRAValidation validation { get; set; }
    }

    public class HRAErrorRootObject
    {
        public HRAErrors errors { get; set; }
    }



    public class InvestigationsErrors
    {
        public List<string> haemoglobin { get; set; }
        public List<string> total_cholesterol { get; set; }
        public List<string> blood_sugar { get; set; }
         
    }
     

    public class InvgestigationErrorMessage
    {
        public string messsage { get; set; }
        public InvestigationsErrors validation { get; set; }
    }

    public class InvgestigationErrorRootObject
    {
        public InvgestigationErrorMessage errors { get; set; }
    }

    #endregion

}
