using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EMeditekApp.Models;
using Xamarin.Forms;
using EMeditekApp;
using EMeditekApp.Wellogo.Models;
using EMeditekApp.Wellogo;

namespace EMeditekApp
{
    public class TodoItemManager
    {
        IRestService restService;
        public TodoItemManager(IRestService service)
        {
            restService = service;
        }
        public Task<string> MainGetDataAsync()
        {
            return restService.GetDataAsync();
        }

        public Task<HealthRiskAssesmentList> GetHealthAssesment()
        {

            return restService.GetHealthAssesments();

        }
        public Task<BloodBankRootClass> GetBloodBank(string state, string city, int page = 1)
        {
            return restService.GetBloodBanks(state, city, page);
        }
        /// <summary>
        /// EKyortype as enum BloodBank or emergency
        /// </summary>
        /// <param name="eKyorType">enum</param>
        /// <param name="StateName">Pass StateName to get city or leave to get  statenames</param>
        /// <returns></returns>
        public Task<StateCity> GetStateOrCity(eKyorType eKyorType, string StateName = "")
        {
            return restService.GetStatesOrCity(eKyorType, StateName);
        }
        public Task<object> AddDoner(BloodBankDoner objBloodBankDoner)
        {
            return restService.AddDoner(objBloodBankDoner);
        }
        public Task<AmbulanceServicesModel> GetAmbulanceServices(string StateName = "", string CityName = "")
        {
            return restService.GetAmbulanceServices(StateName, CityName);
        }
        public Task<HRAModel> NewHRA()
        {
            return restService.NewHRA();
        }
        public Task<DashBoardHRA> DashboardHRA()
        {
            return restService.DashBoardHRA();
        }
        public Task<BlogFeatured> FeturedBlog()
        {
            return restService.GetFeaturedBlog();
        }
        public Task<AllBlogs> GetAllBlogs(string search = "", string category = "", string tag = "", int page = 1)
        {
            return restService.GetAllBlogs(search, category, tag, page);
        }
        public Task<BlogFeatured> GetBlogByID(Int64 BlogID)
        {
            return restService.GetBlogByID(BlogID);
        }
        public Task<Allcategories> GetAllBlogCategories()
        {
            return restService.GetAllBlogCategories();
        }
        public Task<HCAHData> HCAHServices()
        {
            return restService.GetHCAhServices();
        }
        public Task<HCAHFormResponse> PostHCAHFormData(FormFields objFormFields)
        {
            return restService.PostHCAHFormData(objFormFields);
        }
        public Task<HealthStats> GetHealthStats()
        {
            return restService.GetHealthStats();
        }
        public Task<Pharmacy> UploadPrescription(List<byte[]> photos, List<string> lstimagename, int orderid = 0)
        {
            return restService.UploadPrescription(photos, lstimagename, orderid);
        }
        public Task<Pharmacy> PharmacyDeleivery(PrescriptionDeleiveryDetails objPrescriptionDeleiveryDetails)
        {
            return restService.PharmacyDeleivery(objPrescriptionDeleiveryDetails);

        }
        public Task<Pharmacy> PharmacyPlaceOrder(int OrderID)
        {
            return restService.PharmacyPlaceOrder(OrderID);
        }
        public Task<PharmacyPhotoDelete> PharmacyDeletePhoto(int OrderID, int PhotoID)
        {
            return restService.PharmacyDeletePhoto(OrderID, PhotoID);
        }
        public Task<SavePatientResponse> SavePatientDetails(PatientDetailsList objPatientDetailsList)
        {
            return restService.SavePatientDetails(objPatientDetailsList);
        }
        public Task<SavedPatientResponse> GetSavedPatient(string search = "")
        {
            return restService.GetSavedPatient(search);
        }
        public Task<Checkupdata> GetAllDetailsofPatient(int CheckupId, int PatientID)
        {
            return restService.GetAllDetailsofPatient(CheckupId, PatientID);
        }
        public Task<CityReponse> GetCityAHC(int stateid, int bundleid)
        {
            return restService.GetCityAHC(stateid, bundleid);
        }
        public Task<DCResponse> GetDCAHC(int cityid, int bundleid)
        {
            return restService.GetDCAHC(cityid, bundleid);
        }
        public Task<Tests> GetTests(int patientid, int dcid)
        {
            return restService.GetTests(patientid, dcid);
        }
        public Task<SaveTestResponse> SaveTest(SaveTest objSaveTest, int CheckupID, int PatientID)
        {
            return restService.SaveTest(objSaveTest, CheckupID, PatientID);
        }
        public Task<OrderSummary> GetOrderSummary(int CheckupId)
        {
            return restService.GetOrderSummary(CheckupId);
        }
        public Task<AHCPlaceOrder> PlaceAHCOrder(int CheckupId)
        {
            return restService.PlaceAHCOrder(CheckupId);
        }
        public Task<HRAQuestions> GetHRAQuestions(int stepid)
        {
            return restService.GetHRAQuestions(stepid);
        }
        public Task<HRA> GetHRAbyId(int HraId)
        {
            return restService.GetHRAbyId(HraId);
        }

        public Task<object> SaveHraStep1(PhysicalExaminationInput objPhysicalExamination, int hraid)
        {
            return restService.SaveHraStep1(objPhysicalExamination, hraid);
        }
        public Task<object> SaveHraStep2(Investigations objPhysicalExamination, int hraid)
        {
            return restService.SaveHraStep2(objPhysicalExamination, hraid);
        }
        public Task<HRA> SaveHraStep3(FamilyHistoryInput objFamilyHistory, int hraid)
        {
            return restService.SaveHraStep3(objFamilyHistory, hraid);
        }
        public Task<HRA> SaveHraStep4(PersonalHealthInput objPersonalHealth, int hraid)
        {
            return restService.SaveHraStep4(objPersonalHealth, hraid);
        }
        public Task<HRA> SaveHraStep5(NuritionInput objNutrition, int hraid)
        {
            return restService.SaveHraStep5(objNutrition, hraid);
        }
        public Task<HRA> SaveHraStep6(FitnessInput objFitness, int hraid)
        {
            return restService.SaveHraStep6(objFitness, hraid);
        }
        public Task<HRA> SaveHraStep7(StressInput objStress, int hraid)
        {
            return restService.SaveHraStep7(objStress, hraid);
        }
        public Task<HRA> SaveHraStep8(HabitsInput objHabits, int hraid)
        {
            return restService.SaveHraStep8(objHabits, hraid);
        }
        public Task<HRA> SaveHraStep9(FemaleSpecific objFemaleSpecific, int hraid)
        {
            return restService.SaveHraStep9(objFemaleSpecific, hraid);
        }

        public Task<byte[]> DownloadHRAReport(int HRAid)
        {
            return restService.DownloadHRAReport(HRAid);
        }

        public Task<KyorLoginCheckResponse> CheckKyorVerificationStatus(KyorLoginCheckinput objKyorLoginCheckinput)
        {
            return restService.CheckKyorVerificationStatus(objKyorLoginCheckinput);
        }
        public Task<KyorLoginCheckResponse> UpdateKyorVerificationStatus(KyorLoginCheckinput objKyorLoginCheckinput)
        {
            return restService.UpdateKyorVerificationStatus(objKyorLoginCheckinput);
        }

        public Task<KyorRegistrationOutput> RegisterForKyor(KyorRegistration objKyorRegistration)
        {
            return restService.RegisterForKyor(objKyorRegistration);
        }

        public Task<object> LoginForKyor(KyorLogin objKyorLogin)
        {
            return restService.LoginForKyor(objKyorLogin);
        }
        public Task<object> Logout()
        {
            return restService.Logout();
        }

        public Task<UserDetais> UserDetais(int UserID)
        {
            return restService.GetUserDetails(UserID);
        }

        public Task<object> ChangePassword(UserPassword objChangePassword)
        {
            return restService.ChangePassword(objChangePassword);
        }
        public Task<object> UpdateProfileDetails(UpdateUserDetails objUpdateUserDetails)
        {
            return restService.UpdateuserDetails(objUpdateUserDetails);
        }
        public Task<object> UpdateUserProfilePicture(object objUpdateUserDetails)
        {
            return restService.UpdateUserProfilePicture(objUpdateUserDetails);
        }
        public Task<LeaderboardData> GetLeaderboardUsers()
        {
            return restService.GetLeaderboardUsers();
        }
        public Task<LeaderBoardLogRootObject> GetLeaderboardLog(int pageno = 1)
        {
            return restService.GetLeaderboardLog(pageno);
        }
        public Task<TrackerAcessOutput> GenerateAccessTokenViaAcessCode(string TrackerType, string Code)
        {
            return restService.GenerateAccessTokenViaAcessCode(TrackerType, Code);
        }
        public Task<Tracker> GetClientTracker()
        {
            return restService.GetClientTracker();
        }
        public Task<Tracker> DeleteTrackerData()
        {
            return restService.DeleteTrackerData();
        }

        public Task<AppNotification> GetNotofications(int page)
        {
            return restService.GetNotofications(page);
        }
        public Task<Deals>  GetDeals()
        {
            return restService.GetDeals();
        }

        public Task<EarnPoints> HowToEarnPoints()
        {
            return restService.HowToEarnPoints();
        }
        public Task<SingleDeal> GetSingleDeal(int partnerid, int dealid)
        {
            return restService.GetSingleDeal(partnerid, dealid);
        }

        public Task<object> RedeemDeal(int Dealid)
        {
            return restService.RedeemDeal(Dealid);
        }



    }
}
