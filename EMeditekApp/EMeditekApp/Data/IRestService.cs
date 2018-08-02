using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EMeditekApp.Models;
using EMeditekApp.Wellogo;
using EMeditekApp.Wellogo.Models;

namespace EMeditekApp
{
    public interface IRestService
    {
        Task<String> GetDataAsync();
        Task<HealthRiskAssesmentList> GetHealthAssesments();
        Task<BloodBankRootClass> GetBloodBanks(string state, string city, int page);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="eKyorType">Enum Blood bank or emergency</param>
        /// <param name="State">Pass Statename to get city else leave</param>
        /// <returns></returns>
        Task<StateCity> GetStatesOrCity(eKyorType eKyorType, string StateName = "");
        Task<object> AddDoner(BloodBankDoner objBloodBankDoner);
        Task<AmbulanceServicesModel> GetAmbulanceServices(string StateName = "", string CityName = "");
        Task<HRAModel> NewHRA();
        Task<DashBoardHRA> DashBoardHRA();
        Task<BlogFeatured> GetFeaturedBlog();
        Task<AllBlogs> GetAllBlogs(string search , string category , string tag , int page );
        Task<BlogFeatured> GetBlogByID(Int64 BlogID);
        Task<Allcategories> GetAllBlogCategories();
        Task<HCAHData> GetHCAhServices();
        Task<HCAHFormResponse> PostHCAHFormData(FormFields objFormFields);
        Task<HealthStats> GetHealthStats();
        Task<Pharmacy> UploadPrescription(List<byte[]> photos, List<string> lstimagename, int orderid);
        Task<Pharmacy> PharmacyDeleivery(PrescriptionDeleiveryDetails objPrescriptionDeleiveryDetails);
        Task<Pharmacy> PharmacyPlaceOrder(int OrderID);
        Task<PharmacyPhotoDelete> PharmacyDeletePhoto(int OrderID, int PhotoID );
        Task<SavePatientResponse> SavePatientDetails(PatientDetailsList objPatientDetailsList);
        Task<SavedPatientResponse> GetSavedPatient(string search);
        Task<Checkupdata> GetAllDetailsofPatient(int CheckupId, int PatientID);
        Task<CityReponse> GetCityAHC(int stateid, int bundleid);
        Task<DCResponse> GetDCAHC(int cityid, int bundleid);
        Task<Tests> GetTests(int patientid, int dcid);
        Task<SaveTestResponse> SaveTest(SaveTest objSaveTest, int CheckupID, int PatientID);
        Task<OrderSummary> GetOrderSummary(int CheckupId);
        Task<AHCPlaceOrder> PlaceAHCOrder(int CheckupId);
        Task<HRAQuestions> GetHRAQuestions(int stepid);
        Task<HRA> GetHRAbyId(int hraid);
        Task<object> SaveHraStep1(PhysicalExaminationInput objPhysicalExamination,int hraid);
        Task<object> SaveHraStep2(Investigations objInvestigations, int hraid);
        Task<HRA> SaveHraStep3(FamilyHistoryInput objFamilyHistory, int hraid);
        Task<HRA> SaveHraStep4(PersonalHealthInput objPersonalHealth , int hraid);
        Task<HRA> SaveHraStep5(NuritionInput objNutrition, int hraid);
        Task<HRA> SaveHraStep6(FitnessInput objFitness, int hraid);
        Task<HRA> SaveHraStep7(StressInput objStress, int hraid);
        Task<HRA> SaveHraStep8(HabitsInput objHabits, int hraid);
        Task<HRA> SaveHraStep9(FemaleSpecific objHabits, int hraid);
        Task<byte[]> DownloadHRAReport(int HRAid);
        Task<KyorLoginCheckResponse> CheckKyorVerificationStatus(KyorLoginCheckinput objKyorLoginCheckinput);
        Task<KyorLoginCheckResponse>UpdateKyorVerificationStatus(KyorLoginCheckinput objKyorLoginCheckinput);
        Task<KyorRegistrationOutput> RegisterForKyor(KyorRegistration objKyorRegistration);
        Task<object> LoginForKyor(KyorLogin objKyorLogin);
        Task<UserDetais> GetUserDetails(int UserID);
        Task<object> Logout();
         Task<object> ChangePassword(UserPassword objChangePassword);
        Task<object> UpdateuserDetails(UpdateUserDetails objUpdateUserDetails);
        Task<object> UpdateUserProfilePicture(object objUpdateUserDetails);
        Task<LeaderboardData> GetLeaderboardUsers();
        Task<LeaderBoardLogRootObject> GetLeaderboardLog(int pageno);
        Task<TrackerAcessOutput> GenerateAccessTokenViaAcessCode(string TrackerType, string Code);
        Task<Tracker> GetClientTracker();
        Task<Tracker> DeleteTrackerData();

        Task<AppNotification> GetNotofications(int page);
        Task<Deals> GetDeals();
        Task<EarnPoints> HowToEarnPoints();
        Task<SingleDeal> GetSingleDeal(int partnerid, int dealid);
        Task<object>  RedeemDeal(int Dealid);




    }
}
