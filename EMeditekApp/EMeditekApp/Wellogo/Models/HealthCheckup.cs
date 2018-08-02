using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMeditekApp.Wellogo.Models
{
    public class HealthCheckup
    {
      
        // public SavedPatient ExistingPatient { get; set; }
        public int ID { get; set; }
        public string Gender { get; set; }
        public int? GenderID { get; set; }
        public string Relation { get; set; }
        public string RelationID { get; set; }
        public string SeniorityLevel { get; set; }
        public string SeniorityLevelID { get; set; }
        public string MaritalStatus { get; set; }
        public int? MaritalStatusID { get; set; }
        public string Name { get; set; }
        public string EmployeeCode { get; set; }
        public string Phone { get; set; }
        public string DateOfBirth { get; set; }
         

        #region validations
        public Boolean RemoveButtonVisiblity { get; set; }
        public Boolean ExistingPatientVisiblity { get; set; }
        
        public Boolean ErrormessageVisiblity { get; set; }
        public string ErrorMessage { get; set; }
        
        private bool _NameEnablity = true;
        private bool _PhoneEnablity = true;
        private bool _EmployeeCodeEnablity = false;
        private bool _DobEnablity = true;
        private bool _GenderEnablity = true;
        private bool _SeniorityLevelEnablity = false;

        public Boolean SeniorityLevelEnablity { get { return _SeniorityLevelEnablity; } set { _SeniorityLevelEnablity = value; } }
        public Boolean NameEnablity { get { return _NameEnablity; } set { _NameEnablity = value; } }
        public Boolean PhoneEnablity { get { return _PhoneEnablity;  } set { _PhoneEnablity = value; } } 
        public Boolean EmployeeCodeEnablity { get { return _EmployeeCodeEnablity; } set { _EmployeeCodeEnablity = value; } }
        public Boolean DobEnablity { get { return _DobEnablity; } set { _DobEnablity = value; } }
        public Boolean GenderEnablity { get { return _GenderEnablity; } set { _GenderEnablity = value; } }
        public DateTime AHCMinDate { get { return DateTime.Now.AddYears(-100); } }
        public DateTime AHCMaxDate { get { return DateTime.Now; } }
        #endregion
    }

    public class HealthCheckUpDeafult : HealthCheckup
    {
        //public List<CommonDropDowns> lstRelations { get; set; }
        //public List<CommonDropDowns> lstSeniority { get; set; }
        //public List<CommonDropDowns> lstMaritalStatus { get; set; }
        //public List<CommonDropDowns> lstGender { get; set; }
        public List<SavedPatient> lstExistingPatient { get; set; }


        // public List<CommonDropDowns> lstExistingPatients { get; set; }
    }

    public class ListHealthCheckup
    {
        public int ExistingPatient { get; set; }
        public List<HealthCheckup> lstHealthCheckup { get; set; }
    }


    public class ExistingPatient
    {
        public int ID { get; set; }
        public int Name { get; set; }
        public ExistingPatient()
        {

        }
        public ExistingPatient(int _id, int _Name)
        {
            ID = _id;
            Name = _Name;
        }
    }

    public class CommonDropDowns
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public CommonDropDowns(int _ID, string _Name)
        {
            ID = _ID; Name = _Name;
        }
        public CommonDropDowns()
        {

        }
        public List<CommonDropDowns> GetRalations()
        {
            List<CommonDropDowns> objlstRelation = new List<CommonDropDowns>();
            objlstRelation.Add(new CommonDropDowns(1, "Self"));
            objlstRelation.Add(new CommonDropDowns(2, "Wife"));
            objlstRelation.Add(new CommonDropDowns(3, "Husband"));
            objlstRelation.Add(new CommonDropDowns(4, "Mother"));
            objlstRelation.Add(new CommonDropDowns(5, "Father"));
            objlstRelation.Add(new CommonDropDowns(6, "Son"));
            objlstRelation.Add(new CommonDropDowns(7, "Daughter"));
            objlstRelation.Add(new CommonDropDowns(8, "Sister"));
            objlstRelation.Add(new CommonDropDowns(9, "Brother"));
            objlstRelation.Add(new CommonDropDowns(10, "Father in Law"));
            objlstRelation.Add(new CommonDropDowns(11, "Mother In Law"));
            objlstRelation.Add(new CommonDropDowns(12, "Dependent Child"));
            objlstRelation.Add(new CommonDropDowns(13, "Infant"));
            objlstRelation.Add(new CommonDropDowns(14, "Co - Worker"));
            return objlstRelation;
        }
        public List<CommonDropDowns> GetSenorityLevels()
        {
            List<CommonDropDowns> objlstRelation = new List<CommonDropDowns>();
            objlstRelation.Add(new CommonDropDowns(1, "Junior Level"));
            objlstRelation.Add(new CommonDropDowns(2, "Middle Level"));
            objlstRelation.Add(new CommonDropDowns(3, "Senior Level"));
            objlstRelation.Add(new CommonDropDowns(4, "CXO Level"));
            return objlstRelation;
        }
        public List<CommonDropDowns> MaritalStatus()
        {
            List<CommonDropDowns> objlstRelation = new List<CommonDropDowns>();
            objlstRelation.Add(new CommonDropDowns(1, "Single"));
            objlstRelation.Add(new CommonDropDowns(2, "Married"));
            objlstRelation.Add(new CommonDropDowns(3, "Separated"));
            objlstRelation.Add(new CommonDropDowns(4, "Divorced"));
            objlstRelation.Add(new CommonDropDowns(5, "Widowed"));
            return objlstRelation;
        }

        public List<CommonDropDowns> Gender()
        {
            List<CommonDropDowns> objlstRelation = new List<CommonDropDowns>();
            objlstRelation.Add(new CommonDropDowns(1, "Male"));
            objlstRelation.Add(new CommonDropDowns(2, "Female"));
            // objlstRelation.Add(new CommonDropDowns(3, "Others"));
            return objlstRelation;
        }
         
    }


    public class PatientDetails
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













    }

    public class PatientDetailsList
    {
        public List<PatientDetails> patients { get; set; }
    }

    #region Response

    public class PatientResponse
    {
        public int id { get; set; }
        public int client_id { get; set; }
        public string updated_at { get; set; }
        public string created_at { get; set; }
        public List<PatientDetails> patients { get; set; }
    }

    public class SavePatientResponse
    {
        public string status { get; set; }
        public string message { get; set; }
        public PatientResponse data { get; set; }
    }

    #endregion
    #region

    public class SavedPatientResponse
    {
        public string status { get; set; }
        public string message { get; set; }
        public List<SavedPatient> data { get; set; }
    }
    public class SavedPatient
    {
        public int id { get; set; }
        public int? client_id { get; set; }
        public object client_relation_id { get; set; }
        public string name { get; set; }
        public int relation { get; set; }
        public string employee_code { get; set; }
        public int? seniority_level { get; set; }
        public string phone { get; set; }
        public string date_of_birth { get; set; }
        public int? marital_status { get; set; }
        public string gender { get; set; }
        public string created_at { get; set; }
        public string updated_at { get; set; }
    }

    #endregion
    public enum enmRelations {Self=1, Wife, Husband, Mother, Father, Son, Daughter, Sister,
        Brother, FatherinLaw, MotherInLaw, DependentChild, Infant, CoWorker
    }

}
