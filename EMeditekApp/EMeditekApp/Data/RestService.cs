
using EMeditekApp;
using EMeditekApp.Models;
using EMeditekApp.Wellogo;
using EMeditekApp.Wellogo.KyorNow;
using EMeditekApp.Wellogo.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
//using System.Web;
namespace EMeditekApp
{
    public class RestService : IRestService
    {
        HttpClient client;
        public RestService()
        {
            //kdehfjkgf

            //var authData = string.Format("{0}:{1}", Constants.Username, Constants.Password);
            //var authHeaderValue = Convert.ToBase64String(Encoding.UTF8.GetBytes(authData));
            client = new HttpClient();
            //client.MaxResponseContentBufferSize = 1024 * 1024 * 10;
            //client.MaxResponseContentBufferSize = 256000;
            client.DefaultRequestHeaders.Clear();
            // client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", authHeaderValue);
            // Below Lines to be commented after testing
            //if (!DependencyService.Get<ICheckSimulator>().Check())
            //    Constants.RestUrl = "http://14.102.111.48/emslApi/api/";


        }
        private void AddDefaultParameterstoHttpClient(HttpClient HttpClient)
        {
            try
            {
                HttpClient.DefaultRequestHeaders.Clear();
                HttpClient.DefaultRequestHeaders.Add("Consumer-Token", App.ConsumerToken);
                HttpClient.DefaultRequestHeaders.Add("Current-Timestamp", App.CurrentTimestamp);
                HttpClient.DefaultRequestHeaders.Add("Authorization", App.Authorization);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        private void AddDefaultParameterstoHttpClientKyorNow(HttpClient HttpClient)
        {
            try
            {
                HttpClient.DefaultRequestHeaders.Clear();
                HttpClient.DefaultRequestHeaders.Add("Authorization",
                    "eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJzdWIiOiIxNjY1YWY5NTI5ZDgzZWFkIiwiaXNzIjoiaHR0cHM6Ly9zdGFnaW5nLmt5b3Jub3cuY29tL2FwaS92MS9hdXRoZW50aWNhdGUiLCJpYXQiOjE1MjYyODkwNTMsImV4cCI6MTUyOTg4OTA1MywibmJmIjoxNTI2Mjg5MDUzLCJqdGkiOiIwbkVWZjhIemNkZE1QOVZuIn0.9sPZWYUCcqcMvB00AtIWKFyg1_Xln9jobSaQLe21kxQ");
            }
            catch (Exception ex)
            {

                throw;
            }
        }


        #region Kyor

        public async Task<HealthRiskAssesmentList> GetHealthAssesments()
        {
            client.DefaultRequestHeaders.Clear();
            CommonApiParameters objCommonApiParameters = new CommonApiParameters();
            client.DefaultRequestHeaders.Add("Consumer-Token", App.ConsumerToken);
            client.DefaultRequestHeaders.Add("Current-Timestamp", App.CurrentTimestamp);
            client.DefaultRequestHeaders.Add("Authorization", App.Authorization);
            HttpResponseMessage objHttpResponseMessage;
            HealthRiskAssesmentList objHealthRiskAssesmentList = new HealthRiskAssesmentList();
            var url = Constants.WellogoUrl + "hra/api/v1/hras";
            //var json = JsonConvert.SerializeObject(objCommonApiParameters);
            //var Data = new StringContent(json, Encoding.UTF8, "applicationjson");
            objHttpResponseMessage = await client.GetAsync(url.ToString());
            try
            {
                string response = await objHttpResponseMessage.Content.ReadAsStringAsync();
                objHealthRiskAssesmentList = JsonConvert.DeserializeObject<HealthRiskAssesmentList>(response);

            }
            catch (Exception ex)
            {
                return objHealthRiskAssesmentList;
            }
            return objHealthRiskAssesmentList;

        }
        public async Task<BloodBankRootClass> GetBloodBanks(string state, string city, int page = 1)
        {
            client.DefaultRequestHeaders.Clear();
            BloodBankRootClass objBloodBankRootClass = new Wellogo.Models.BloodBankRootClass();
            HttpResponseMessage objHttpResponseMessage;
            var URL = string.Format(Constants.WellogoUrl + "bloodbank/api/v1/search?state={0}&city={1}&page={2}", new object[] { state, city, page });
            client.DefaultRequestHeaders.Add("Consumer-Token", App.ConsumerToken);
            client.DefaultRequestHeaders.Add("Current-Timestamp", App.CurrentTimestamp);
            client.DefaultRequestHeaders.Add("Authorization", App.Authorization);
            objHttpResponseMessage = await client.GetAsync(URL);
            try
            {

                string response = await objHttpResponseMessage.Content.ReadAsStringAsync();
                objBloodBankRootClass = JsonConvert.DeserializeObject<BloodBankRootClass>(response);
            }
            catch (Exception ex)
            {

            }


            return objBloodBankRootClass;
        }
        public async Task<StateCity> GetStatesOrCity(eKyorType eKyorType, string StateName)
        {
            StateCity objStateCity = new Wellogo.Models.StateCity();
            HttpResponseMessage objHttpResponseMessage;
            AddDefaultParameterstoHttpClient(client);
            string Url = Constants.WellogoUrl;
            if (eKyorType == eKyorType.BloodBank)
            {
                if (StateName == "")
                    Url += "bloodbank/api/v1/states";
                else
                    Url += string.Format("bloodbank/api/v1/cities?state={0}", new object[] { StateName });
            }
            else
            {
                if (StateName == "")
                    Url += "emergency/api/v1/states";
                else
                    Url += string.Format("emergency/api/v1/cities?state={0}", new object[] { StateName });
            }
            objHttpResponseMessage = await client.GetAsync(Url);
            try
            {
                var Response = await objHttpResponseMessage.Content.ReadAsStringAsync();
                objStateCity = JsonConvert.DeserializeObject<StateCity>(Response.ToString());
            }
            catch (Exception ex)
            {
                throw;
            }
            return objStateCity;
        }
        public async Task<object> AddDoner(BloodBankDoner objBloodBankDoner)
        {
            object obj;
            BloodBankDonerRootResponse objBloodBankDonerRootResponse = new Wellogo.Models.BloodBankDonerRootResponse();
            AddDefaultParameterstoHttpClient(client);
            HttpResponseMessage objHttpResponseMessage;
            string url = Constants.WellogoUrl + "bloodbank/api/v1/donation-request";
            var json = JsonConvert.SerializeObject(objBloodBankDoner);
            var Data = new StringContent(json.ToString(), Encoding.UTF8, "application/json");
            objHttpResponseMessage = await client.PostAsync(url, Data);
            try
            {

                string response = await objHttpResponseMessage.Content.ReadAsStringAsync();
                obj = JsonConvert.DeserializeObject<object>(response);
            }
            catch (Exception ex)
            {
                throw;
            }
            return obj;
        }
        public async Task<AmbulanceServicesModel> GetAmbulanceServices(string StateName, string CityName)
        {
            AmbulanceServicesModel objAmbulanceServicesModel = new Wellogo.Models.AmbulanceServicesModel();
            AddDefaultParameterstoHttpClient(client);
            HttpResponseMessage objHttpResponseMessage;
            var URL = string.Format(Constants.WellogoUrl + "emergency/api/v1/search?state={0}&city={1}", new object[] { StateName, CityName });
            objHttpResponseMessage = await client.GetAsync(URL);
            try
            {
                string response = await objHttpResponseMessage.Content.ReadAsStringAsync();
                objAmbulanceServicesModel = JsonConvert.DeserializeObject<AmbulanceServicesModel>(response);
            }
            catch (Exception ex)
            {

            }

            return objAmbulanceServicesModel;

        }
        public async Task<HRAModel> NewHRA()
        {
            HRAModel objHRAModel = new Wellogo.Models.HRAModel();
            HttpResponseMessage objHttpResponseMessage;
            AddDefaultParameterstoHttpClient(client);
            string URL = Constants.WellogoUrl + "hra/api/v1/hras";
            objHttpResponseMessage = await client.PostAsync(URL, null);
            try
            {
                string response = await objHttpResponseMessage.Content.ReadAsStringAsync();
                objHRAModel = JsonConvert.DeserializeObject<HRAModel>(response);
            }
            catch (Exception ex)
            {
                throw;
            }
            return objHRAModel;


        }
        public async Task<DashBoardHRA> DashBoardHRA()
        {
            DashBoardHRA objDashBoardHRA = new Wellogo.Models.DashBoardHRA();
            HttpResponseMessage objHttpResponseMessage;
            AddDefaultParameterstoHttpClient(client);
            string URL = Constants.WellogoUrl + "hra/api/v1/health-stats-dashboard";
            objHttpResponseMessage = await client.GetAsync(URL);
            try
            {
                string response = await objHttpResponseMessage.Content.ReadAsStringAsync();
                objDashBoardHRA = JsonConvert.DeserializeObject<DashBoardHRA>(response);
            }
            catch (Exception ex)
            {
                throw;
            }
            return objDashBoardHRA;
        }
        public async Task<BlogFeatured> GetFeaturedBlog()
        {
            BlogFeatured objBlogFeatured = new Wellogo.Models.BlogFeatured();
            HttpResponseMessage objHttpResponseMessage;
            AddDefaultParameterstoHttpClient(client);
            string URL = Constants.WellogoUrl + "blog/api/v1/blog/featured";
            objHttpResponseMessage = await client.GetAsync(URL);
            try
            {
                string response = await objHttpResponseMessage.Content.ReadAsStringAsync();
                objBlogFeatured = JsonConvert.DeserializeObject<BlogFeatured>(response);
            }
            catch (Exception ex)
            {

                throw;
            }
            return objBlogFeatured;
        }
        public async Task<AllBlogs> GetAllBlogs(string search, string category, string tag, int page)
        {
            AllBlogs objAllBlogs = new Wellogo.Models.AllBlogs();
            HttpResponseMessage objHttpResponseMessage;
            AddDefaultParameterstoHttpClient(client);
            String URL = Constants.WellogoUrl + "blog/api/v1/blog/all";
            if (search != "" | category != "" | tag != "" | page != 0)
            {
                bool IsQueryStringAttached = false;
                URL += "?";
                if (search != "")
                {
                    URL += "search=" + search;
                    IsQueryStringAttached = true;
                }
                if (tag != "")
                {
                    URL += IsQueryStringAttached ? "&tag=" : "tag=" + tag;
                    IsQueryStringAttached = true;
                }
                if (category != "")
                {
                    URL += IsQueryStringAttached ? "&category=" : "category=" + category;
                    IsQueryStringAttached = true;
                }
                if (page != 0)
                {
                    string Page = page.ToString();
                    URL += IsQueryStringAttached ? "&page=" : "page=" + Page;
                }
            }
            objHttpResponseMessage = await client.GetAsync(URL);
            try
            {
                string response = await objHttpResponseMessage.Content.ReadAsStringAsync();
                objAllBlogs = JsonConvert.DeserializeObject<AllBlogs>(response);
            }
            catch (Exception ex)
            {

                throw;
            }
            return objAllBlogs;
        }
        public async Task<BlogFeatured> GetBlogByID(Int64 BlogID)
        {
            BlogFeatured objBlogFeatured = new Wellogo.Models.BlogFeatured();
            HttpResponseMessage objHttpResponseMessage;
            AddDefaultParameterstoHttpClient(client);
            string URL = string.Format(Constants.WellogoUrl + "blog/api/v1/blog/{0}/get", new object[] { BlogID });
            objHttpResponseMessage = await client.GetAsync(URL);
            try
            {
                string response = await objHttpResponseMessage.Content.ReadAsStringAsync();
                objBlogFeatured = JsonConvert.DeserializeObject<BlogFeatured>(response);
            }
            catch (Exception ex)
            {
                throw;
            }
            return objBlogFeatured;
        }
        public async Task<Allcategories> GetAllBlogCategories()
        {

            Allcategories objAllcategories = new Wellogo.Models.Allcategories();
            HttpResponseMessage objHttpResponseMessage;
            AddDefaultParameterstoHttpClient(client);
            string URL = string.Format(Constants.WellogoUrl + "blog/api/v1/category/all");
            objHttpResponseMessage = await client.GetAsync(URL);
            try
            {
                string response = await objHttpResponseMessage.Content.ReadAsStringAsync();
                objAllcategories = JsonConvert.DeserializeObject<Allcategories>(response);
            }
            catch (Exception ex)
            {
                throw;
            }
            return objAllcategories;
        }
        public async Task<HCAHData> GetHCAhServices()
        {

            HCAHData objHomeHealthData = new Wellogo.Models.HCAHData();
            HttpResponseMessage objHttpResponseMessage;
            AddDefaultParameterstoHttpClient(client);
            string URL = string.Format(Constants.WellogoUrl + "hcah/api/v1/services");
            objHttpResponseMessage = await client.GetAsync(URL);
            try
            {
                string response = await objHttpResponseMessage.Content.ReadAsStringAsync();
                objHomeHealthData = JsonConvert.DeserializeObject<HCAHData>(response);
            }
            catch (Exception ex)
            {
                throw;
            }
            return objHomeHealthData;
        }
        public async Task<HCAHFormResponse> PostHCAHFormData(FormFields objFormFields)
        {
            HCAHFormResponse objHCAHFormResponse = new Wellogo.Models.HCAHFormResponse();
            AddDefaultParameterstoHttpClient(client);
            HttpResponseMessage objHttpResponseMessage;
            string url = Constants.WellogoUrl + "hcah/api/v1/hcah";
            var json = JsonConvert.SerializeObject(objFormFields);
            var Data = new StringContent(json.ToString(), Encoding.UTF8, "application/json");
            objHttpResponseMessage = await client.PostAsync(url, Data);
            try
            {
                string response = await objHttpResponseMessage.Content.ReadAsStringAsync();
                if (response.Contains("error"))
                {
                    objHCAHFormResponse.status = "error";
                    objHCAHFormResponse.message = "";
                }
                else
                {
                    objHCAHFormResponse = JsonConvert.DeserializeObject<HCAHFormResponse>(response);
                }

            }
            catch (Exception ex)
            {

                return objHCAHFormResponse;
            }
            return objHCAHFormResponse;
        }
        public async Task<HealthStats> GetHealthStats()
        {
            HealthStats objHealthStats = new Wellogo.Models.HealthStats();
            HttpResponseMessage objHttpResponseMessage;
            AddDefaultParameterstoHttpClient(client);
            string URL = Constants.WellogoUrl + "hra/api/v1/health-stats";
            objHttpResponseMessage = await client.GetAsync(URL);
            try
            {
                string response = await objHttpResponseMessage.Content.ReadAsStringAsync();
                objHealthStats = JsonConvert.DeserializeObject<HealthStats>(response);
            }
            catch (Exception ex)
            {
                throw;
            }
            return objHealthStats;
        }
        public async Task<Pharmacy> UploadPrescription(List<byte[]> photos, List<string> lstimagename, int orderid)
        {
            string url = "";
            if (orderid == 0)
            {
                url = Constants.WellogoUrl + "pharmacy/api/v1/upload-prescription";
            }
            else
            {
                url = string.Format(Constants.WellogoUrl + "pharmacy/api/v1/upload-prescription/{0}", orderid);
            }

            MultipartFormDataContent objMultipartFormDataContent = new MultipartFormDataContent();
            ByteArrayContent objByteArrayContent;
            for (int i = 0; i < photos.Count; i++)
            {
                objByteArrayContent = new ByteArrayContent(photos[i]);
                objMultipartFormDataContent.Add(objByteArrayContent, "photos[]", lstimagename[i]);
            }

            Pharmacy objPharmacy = new Wellogo.Models.Pharmacy();
            AddDefaultParameterstoHttpClient(client);
            HttpResponseMessage objHttpResponseMessage;
            objHttpResponseMessage = await client.PostAsync(url, objMultipartFormDataContent);
            try
            {
                string response = await objHttpResponseMessage.Content.ReadAsStringAsync();
                objPharmacy = JsonConvert.DeserializeObject<Pharmacy>(response);
            }
            catch (Exception ex)
            {
                return objPharmacy;
            }
            return objPharmacy;
        }
        public async Task<Pharmacy> PharmacyDeleivery(PrescriptionDeleiveryDetails objPrescriptionDeleiveryDetails)
        {

            Pharmacy objPharmacy = new Wellogo.Models.Pharmacy();
            AddDefaultParameterstoHttpClient(client);
            HttpResponseMessage objHttpResponseMessage;
            string url = string.Format(Constants.WellogoUrl + "pharmacy/api/v1/orders/{0}/delivery-details", new object[] { objPrescriptionDeleiveryDetails.order_id });
            var json = JsonConvert.SerializeObject(objPrescriptionDeleiveryDetails);
            var Data = new StringContent(json.ToString(), Encoding.UTF8, "application/json");
            objHttpResponseMessage = await client.PostAsync(url, Data);
            try
            {
                string response = await objHttpResponseMessage.Content.ReadAsStringAsync();

                objPharmacy = JsonConvert.DeserializeObject<Pharmacy>(response);


            }
            catch (Exception ex)
            {

                return objPharmacy;
            }
            return objPharmacy;
        }
        public async Task<Pharmacy> PharmacyPlaceOrder(int OrderID)
        {
            Pharmacy objPharmacy = new Wellogo.Models.Pharmacy();
            AddDefaultParameterstoHttpClient(client);
            HttpResponseMessage objHttpResponseMessage;
            string url = string.Format(Constants.WellogoUrl + "pharmacy/api/v1/orders/{0}/place-order", OrderID);
            //var json = JsonConvert.SerializeObject(objPharmacy);
            //var Data = new StringContent("", Encoding.UTF8, "application/json");
            objHttpResponseMessage = await client.PostAsync(url, null);
            string response = "";
            try
            {
                response = await objHttpResponseMessage.Content.ReadAsStringAsync();
                objPharmacy = JsonConvert.DeserializeObject<Pharmacy>(response);
                return objPharmacy;
            }
            catch (Exception ex)
            {
                ErrorResponse objErrorResponse = JsonConvert.DeserializeObject<ErrorResponse>(response);
                objPharmacy.message = objErrorResponse.message;
                return objPharmacy;

            }

        }
        public async Task<PharmacyPhotoDelete> PharmacyDeletePhoto(int OrderID, int PhotoID)
        {
            PharmacyPhotoDelete objPharmacyPhotoDelete = new Wellogo.Models.PharmacyPhotoDelete();
            AddDefaultParameterstoHttpClient(client);
            HttpResponseMessage objHttpResponseMessage;
            string url = string.Format(Constants.WellogoUrl + "pharmacy/api/v1/orders/{0}/prescription-photos/{1}", new object[] { OrderID, PhotoID });
            //var json = JsonConvert.SerializeObject(objPharmacy);
            //  var Data = new StringContent("", Encoding.UTF8, "application/json");
            objHttpResponseMessage = await client.DeleteAsync(url);
            try
            {
                string response = await objHttpResponseMessage.Content.ReadAsStringAsync();
                objPharmacyPhotoDelete = JsonConvert.DeserializeObject<PharmacyPhotoDelete>(response);
            }
            catch (Exception ex)
            {
                throw;
            }
            return objPharmacyPhotoDelete;
        }
        public async Task<SavePatientResponse> SavePatientDetails(PatientDetailsList objPatientDetailsList)
        {

            SavePatientResponse objSavePatientResponse = new Wellogo.Models.SavePatientResponse();
            AddDefaultParameterstoHttpClient(client);
            HttpResponseMessage objHttpResponseMessage;
            string url = Constants.WellogoUrl + "ahc/api/v1/checkups";
            var json = JsonConvert.SerializeObject(objPatientDetailsList);
            var Data = new StringContent(json.ToString(), Encoding.UTF8, "application/json");
            objHttpResponseMessage = await client.PostAsync(url, Data);
            try
            {

                string response = await objHttpResponseMessage.Content.ReadAsStringAsync();
                objSavePatientResponse = JsonConvert.DeserializeObject<SavePatientResponse>(response);
                if (objSavePatientResponse.status != "success")
                {
                    MessagingCenter.Send<App, string>((App)Xamarin.Forms.Application.Current, "AHCHealthCheckup", response);
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return objSavePatientResponse;
        }
        public async Task<SavedPatientResponse> GetSavedPatient(string search)
        {
            SavedPatientResponse objSavedPatientResponse = new Wellogo.Models.SavedPatientResponse();
            HttpResponseMessage objHttpResponseMessage;
            AddDefaultParameterstoHttpClient(client);
            string URL = string.Format(Constants.WellogoUrl + "ahc/api/v1/my-patients/search?name={0}", new object[] { search });
            objHttpResponseMessage = await client.GetAsync(URL);
            try
            {

                string response = await objHttpResponseMessage.Content.ReadAsStringAsync();
                objSavedPatientResponse = JsonConvert.DeserializeObject<SavedPatientResponse>(response);
            }
            catch (Exception ex)
            {
                throw;
            }
            return objSavedPatientResponse;
        }
        public async Task<Checkupdata> GetAllDetailsofPatient(int CheckupId, int PatientID)
        {
            Checkupdata objCheckupdata = new Wellogo.Models.Checkupdata();
            HttpResponseMessage objHttpResponseMessage;
            AddDefaultParameterstoHttpClient(client);
            string URL = string.Format(Constants.WellogoUrl + "ahc/api/v1/checkups/{0}/patients/{1}/tests", new object[] { CheckupId, PatientID });
            objHttpResponseMessage = await client.GetAsync(URL);
            try
            {

                string response = await objHttpResponseMessage.Content.ReadAsStringAsync();
                objCheckupdata = JsonConvert.DeserializeObject<Checkupdata>(response);
            }
            catch (Exception ex)
            {
                throw;
            }
            return objCheckupdata;
        }
        public async Task<CityReponse> GetCityAHC(int stateid, int bundleid)
        {

            CityReponse objCityReponse = new Wellogo.Models.CityReponse();
            HttpResponseMessage objHttpResponseMessage;
            AddDefaultParameterstoHttpClient(client);
            string URL = string.Format(Constants.WellogoUrl + "ahc/api/v1/cities/search?state_id={0}&bundle_id={1}", new object[] { stateid, bundleid });
            objHttpResponseMessage = await client.GetAsync(URL);
            try
            {
                string response = await objHttpResponseMessage.Content.ReadAsStringAsync();
                objCityReponse = JsonConvert.DeserializeObject<CityReponse>(response);
            }
            catch (Exception ex)
            {
                throw;
            }
            return objCityReponse;
        }
        public async Task<DCResponse> GetDCAHC(int cityid, int bundleid)
        {
            DCResponse objDCResponse = new Wellogo.Models.DCResponse();
            HttpResponseMessage objHttpResponseMessage;
            AddDefaultParameterstoHttpClient(client);
            string URL = string.Format(Constants.WellogoUrl + "ahc/api/v1/dcs/search?city_id={0}&bundle_id={1}", new object[] { cityid, bundleid });
            objHttpResponseMessage = await client.GetAsync(URL);
            try
            {
                string response = await objHttpResponseMessage.Content.ReadAsStringAsync();
                objDCResponse = JsonConvert.DeserializeObject<DCResponse>(response);
            }
            catch (Exception ex)
            {
                throw;
            }
            return objDCResponse;
        }
        public async Task<Tests> GetTests(int patientid, int dcid)
        {
            Tests objTests = new Wellogo.Models.Tests();
            HttpResponseMessage objHttpResponseMessage;
            AddDefaultParameterstoHttpClient(client);
            string URL = string.Format(Constants.WellogoUrl + "ahc/api/v1/patients/{0}/dcs/{1}/tests", new object[] { patientid, dcid });
            objHttpResponseMessage = await client.GetAsync(URL);
            try
            {
                string response = await objHttpResponseMessage.Content.ReadAsStringAsync();
                objTests = JsonConvert.DeserializeObject<Tests>(response);
            }
            catch (Exception ex)
            {
                throw;
            }

            return objTests;
        }
        public async Task<SaveTestResponse> SaveTest(SaveTest objSaveTest, int CheckupID, int PatientID)
        {
            SaveTestResponse objSaveTestResponse = new Wellogo.Models.SaveTestResponse();
            AddDefaultParameterstoHttpClient(client);
            HttpResponseMessage objHttpResponseMessage;
            string url = string.Format(Constants.WellogoUrl + "ahc/api/v1/checkups/{0}/patients/{1}/tests", new object[] { CheckupID, PatientID });
            var json = JsonConvert.SerializeObject(objSaveTest);
            var Data = new StringContent(json.ToString(), Encoding.UTF8, "application/json");
            objHttpResponseMessage = await client.PostAsync(url, Data);
            try
            {
                string response = await objHttpResponseMessage.Content.ReadAsStringAsync();
                objSaveTestResponse = JsonConvert.DeserializeObject<SaveTestResponse>(response);
                if (objSaveTestResponse.status != "success")
                {

                    MessagingCenter.Send<App, string>((App)Xamarin.Forms.Application.Current, "AHCAddtest", response);
                }
            }
            catch (Exception ex)
            {

                throw;
            }
            return objSaveTestResponse;
        }
        public async Task<OrderSummary> GetOrderSummary(int CheckupId)
        {
            OrderSummary objOrderSummary = new Wellogo.Models.OrderSummary();
            HttpResponseMessage objHttpResponseMessage;
            AddDefaultParameterstoHttpClient(client);
            string URL = string.Format(Constants.WellogoUrl + "ahc/api/v1/checkups/{0}/order-summary", new object[] { CheckupId });
            objHttpResponseMessage = await client.GetAsync(URL);
            try
            {
                string response = await objHttpResponseMessage.Content.ReadAsStringAsync();
                objOrderSummary = JsonConvert.DeserializeObject<OrderSummary>(response);
            }
            catch (Exception ex)
            {
                throw;
            }

            return objOrderSummary;
        }
        public async Task<AHCPlaceOrder> PlaceAHCOrder(int CheckupId)
        {
            AHCPlaceOrder objAHCPlaceOrder = new AHCPlaceOrder();
            AddDefaultParameterstoHttpClient(client);
            HttpResponseMessage objHttpResponseMessage;
            string url = string.Format(Constants.WellogoUrl + "ahc/api/v1/checkups/{0}/place-order", new object[] { CheckupId });
            objHttpResponseMessage = await client.PostAsync(url, null);
            try
            {
                string response = await objHttpResponseMessage.Content.ReadAsStringAsync();
                objAHCPlaceOrder = JsonConvert.DeserializeObject<AHCPlaceOrder>(response);
            }
            catch (Exception ex)
            {

                throw;
            }
            return objAHCPlaceOrder;
        }
        public async Task<HRAQuestions> GetHRAQuestions(int stepid)
        {
            HRAQuestions objHRAQuestions = new Wellogo.Models.HRAQuestions();
            HttpResponseMessage objHttpResponseMessage;
            AddDefaultParameterstoHttpClient(client);
            string URL = string.Format(Constants.WellogoUrl + "hra/api/v1/hras/steps/{0}/questions", new object[] { stepid });
            objHttpResponseMessage = await client.GetAsync(URL);
            try
            {
                string response = await objHttpResponseMessage.Content.ReadAsStringAsync();
                objHRAQuestions = JsonConvert.DeserializeObject<HRAQuestions>(response);
            }
            catch (Exception ex)
            {
                throw;
            }
            return objHRAQuestions;
        }
        public async Task<HRA> GetHRAbyId(int HraId)
        {
            HRA objHRAClient = new Wellogo.Models.HRA();
            HttpResponseMessage objHttpResponseMessage;
            AddDefaultParameterstoHttpClient(client);
            string URL = string.Format(Constants.WellogoUrl + "hra/api/v1/hras/{0}", new object[] { HraId });
            objHttpResponseMessage = await client.GetAsync(URL);
            try
            {
                string response = await objHttpResponseMessage.Content.ReadAsStringAsync();
                objHRAClient = JsonConvert.DeserializeObject<HRA>(response);
            }
            catch (Exception ex)
            {
                throw;
            }
            return objHRAClient;
        }
        public async Task<object> SaveHraStep1(PhysicalExaminationInput objPhysicalExamination, int Hraid)
        {
            JObject JsonObject = new JObject();
            //Height
            if (objPhysicalExamination.height_dont_know != 1)
            {
                if (objPhysicalExamination.height_cms != null)
                    JsonObject.Add("height_cms", objPhysicalExamination.height_cms);
                else
                {
                    JsonObject.Add("height_feet", objPhysicalExamination.height_feet);
                    JsonObject.Add("height_inches", objPhysicalExamination.height_inches);
                }
            }
            else
                JsonObject.Add("height_dont_know", objPhysicalExamination.height_dont_know);

            //Weight
            if (objPhysicalExamination.weight_dont_know != 1)
                JsonObject.Add("weight_kgs", objPhysicalExamination.weight_kgs);
            else
                JsonObject.Add("weight_dont_know", objPhysicalExamination.weight_dont_know);

            if (objPhysicalExamination.weight_dont_know != 1 && objPhysicalExamination.height_dont_know != 1 && objPhysicalExamination.bmi != null)
            {
                JsonObject.Add("bmi", Math.Round((double)objPhysicalExamination.bmi, 2));
            }

            // Blood pressure systolic
            if (objPhysicalExamination.blood_pressure_systolic_dont_know != 1)
                JsonObject.Add("blood_pressure_systolic", objPhysicalExamination.blood_pressure_systolic);
            else
                JsonObject.Add("blood_pressure_systolic_dont_know", objPhysicalExamination.blood_pressure_systolic_dont_know);


            // BloodPressure diastolic
            if (objPhysicalExamination.blood_pressure_diastolic_dont_know != 1)
                JsonObject.Add("blood_pressure_diastolic", objPhysicalExamination.blood_pressure_diastolic);
            else
            {
                JsonObject.Add("blood_pressure_diastolic_dont_know", objPhysicalExamination.blood_pressure_diastolic_dont_know);
            }
            // blood group
            JsonObject.Add("blood_group", objPhysicalExamination.blood_group);

            object obj;
            AddDefaultParameterstoHttpClient(client);
            HttpResponseMessage objHttpResponseMessage;
            string url = string.Format(Constants.WellogoUrl + "hra/api/v1/hras/{0}/steps/1", new object[] { Hraid.ToString() });
            HttpContent objHttpContent = new StringContent(JsonObject.ToString(), Encoding.UTF8, "application/json");
            objHttpResponseMessage = await client.PostAsync(url, objHttpContent);
            try
            {
                string response = await objHttpResponseMessage.Content.ReadAsStringAsync();
                obj = JsonConvert.DeserializeObject<object>(response);
            }
            catch (Exception ex)
            {

                throw;
            }
            return obj;
        }
        public async Task<object> SaveHraStep2(Investigations objInvestigations, int hraid)
        {
            JObject JsonObject = new JObject();
            object obj;
            // Hemoglobin
            if (objInvestigations.haemoglobin_dont_know != 1 && (objInvestigations.haemoglobin != null))
                JsonObject.Add("haemoglobin", objInvestigations.haemoglobin.ToString());
            else if (objInvestigations.haemoglobin_dont_know == 1)
                JsonObject.Add("haemoglobin_dont_know", objInvestigations.haemoglobin_dont_know);

            // Cholestrol
            if (objInvestigations.total_cholesterol_dont_know != 1 && (objInvestigations.total_cholesterol != null))
                JsonObject.Add("total_cholesterol", objInvestigations.total_cholesterol.ToString());
            else if (objInvestigations.total_cholesterol_dont_know == 1)
                JsonObject.Add("total_cholesterol_dont_know", objInvestigations.total_cholesterol_dont_know);

            // blood sugar
            if (objInvestigations.blood_sugar_dont_know != 1 && (objInvestigations.blood_sugar != null))
                JsonObject.Add("blood_sugar", objInvestigations.blood_sugar.ToString());
            else if (objInvestigations.blood_sugar_dont_know == 1)
                JsonObject.Add("blood_sugar_dont_know", objInvestigations.blood_sugar_dont_know);

            AddDefaultParameterstoHttpClient(client);
            HttpResponseMessage objHttpResponseMessage;
            string url = string.Format(Constants.WellogoUrl + "hra/api/v1/hras/{0}/steps/2", new object[] { hraid.ToString() });
            HttpContent objHttpContent = new StringContent(JsonObject.ToString(), Encoding.UTF8, "application/json");
            objHttpResponseMessage = await client.PostAsync(url, objHttpContent);
            try
            {
                string response = await objHttpResponseMessage.Content.ReadAsStringAsync();
                obj = JsonConvert.DeserializeObject<object>(response);
            }
            catch (Exception ex)
            {
                throw;
            }
            return obj;
        }
        public async Task<HRA> SaveHraStep3(FamilyHistoryInput objFamilyHistory, int hraid)
        {

            HRA objHRA = new Wellogo.Models.HRA();
            AddDefaultParameterstoHttpClient(client);
            HttpResponseMessage objHttpResponseMessage;
            string url = string.Format(Constants.WellogoUrl + "hra/api/v1/hras/{0}/steps/3", new object[] { hraid.ToString() });
            string jsoninput = JsonConvert.SerializeObject(objFamilyHistory);
            HttpContent objHttpContent = new StringContent(jsoninput, Encoding.UTF8, "application/json");
            objHttpResponseMessage = await client.PostAsync(url, objHttpContent);
            try
            {
                string response = await objHttpResponseMessage.Content.ReadAsStringAsync();
                objHRA = JsonConvert.DeserializeObject<HRA>(response);
            }
            catch (Exception ex)
            {

                throw;
            }
            return objHRA;
        }
        public async Task<HRA> SaveHraStep4(PersonalHealthInput objPersonalHealth, int hraid)
        {
            HRA objHRA = new Wellogo.Models.HRA();
            AddDefaultParameterstoHttpClient(client);
            HttpResponseMessage objHttpResponseMessage;
            string url = string.Format(Constants.WellogoUrl + "hra/api/v1/hras/{0}/steps/4", new object[] { hraid.ToString() });
            string jsoninput = JsonConvert.SerializeObject(objPersonalHealth);
            HttpContent objHttpContent = new StringContent(jsoninput, Encoding.UTF8, "application/json");
            objHttpResponseMessage = await client.PostAsync(url, objHttpContent);
            try
            {
                string response = await objHttpResponseMessage.Content.ReadAsStringAsync();
                objHRA = JsonConvert.DeserializeObject<HRA>(response);
            }
            catch (Exception ex)
            {

                throw;
            }
            return objHRA;
        }
        public async Task<HRA> SaveHraStep5(NuritionInput objNutrition, int hraid)
        {
            HRA objHRA = new Wellogo.Models.HRA();
            AddDefaultParameterstoHttpClient(client);
            HttpResponseMessage objHttpResponseMessage;
            string url = string.Format(Constants.WellogoUrl + "hra/api/v1/hras/{0}/steps/5", new object[] { hraid.ToString() });
            string jsoninput = JsonConvert.SerializeObject(objNutrition);
            HttpContent objHttpContent = new StringContent(jsoninput, Encoding.UTF8, "application/json");
            objHttpResponseMessage = await client.PostAsync(url, objHttpContent);
            try
            {
                string response = await objHttpResponseMessage.Content.ReadAsStringAsync();
                objHRA = JsonConvert.DeserializeObject<HRA>(response);
            }
            catch (Exception ex)
            {

                throw;
            }
            return objHRA;
        }
        public async Task<HRA> SaveHraStep6(FitnessInput objFitness, int hraid)
        {
            HRA objHRA = new Wellogo.Models.HRA();
            AddDefaultParameterstoHttpClient(client);
            HttpResponseMessage objHttpResponseMessage;
            string url = string.Format(Constants.WellogoUrl + "hra/api/v1/hras/{0}/steps/6", new object[] { hraid.ToString() });
            string jsoninput = JsonConvert.SerializeObject(objFitness);
            HttpContent objHttpContent = new StringContent(jsoninput, Encoding.UTF8, "application/json");
            objHttpResponseMessage = await client.PostAsync(url, objHttpContent);
            try
            {
                string response = await objHttpResponseMessage.Content.ReadAsStringAsync();
                objHRA = JsonConvert.DeserializeObject<HRA>(response);
            }
            catch (Exception ex)
            {

                throw;
            }
            return objHRA;
        }
        public async Task<HRA> SaveHraStep7(StressInput objStress, int hraid)
        {
            HRA objHRA = new Wellogo.Models.HRA();
            AddDefaultParameterstoHttpClient(client);
            HttpResponseMessage objHttpResponseMessage;
            string url = string.Format(Constants.WellogoUrl + "hra/api/v1/hras/{0}/steps/7", new object[] { hraid.ToString() });
            string jsoninput = JsonConvert.SerializeObject(objStress);
            HttpContent objHttpContent = new StringContent(jsoninput, Encoding.UTF8, "application/json");
            objHttpResponseMessage = await client.PostAsync(url, objHttpContent);
            try
            {
                string response = await objHttpResponseMessage.Content.ReadAsStringAsync();
                objHRA = JsonConvert.DeserializeObject<HRA>(response);
            }
            catch (Exception ex)
            {

                throw;
            }
            return objHRA;
        }
        public async Task<HRA> SaveHraStep8(HabitsInput objHabits, int hraid)
        {
            HRA objHRA = new Wellogo.Models.HRA();
            AddDefaultParameterstoHttpClient(client);
            HttpResponseMessage objHttpResponseMessage;
            string url = string.Format(Constants.WellogoUrl + "hra/api/v1/hras/{0}/steps/8", new object[] { hraid.ToString() });
            string jsoninput = JsonConvert.SerializeObject(objHabits);
            HttpContent objHttpContent = new StringContent(jsoninput, Encoding.UTF8, "application/json");
            objHttpResponseMessage = await client.PostAsync(url, objHttpContent);
            try
            {
                string response = await objHttpResponseMessage.Content.ReadAsStringAsync();
                objHRA = JsonConvert.DeserializeObject<HRA>(response);
            }
            catch (Exception ex)
            {

                throw;
            }
            return objHRA;
        }
        public async Task<HRA> SaveHraStep9(FemaleSpecific objFemaleSpecific, int hraid)
        {
            HRA objHRA = new Wellogo.Models.HRA();
            AddDefaultParameterstoHttpClient(client);
            HttpResponseMessage objHttpResponseMessage;
            string url = string.Format(Constants.WellogoUrl + "hra/api/v1/hras/{0}/steps/9", new object[] { hraid.ToString() });
            string jsoninput = JsonConvert.SerializeObject(objFemaleSpecific);
            HttpContent objHttpContent = new StringContent(jsoninput, Encoding.UTF8, "application/json");
            objHttpResponseMessage = await client.PostAsync(url, objHttpContent);
            try
            {
                string response = await objHttpResponseMessage.Content.ReadAsStringAsync();
                objHRA = JsonConvert.DeserializeObject<HRA>(response);
            }
            catch (Exception ex)
            {

                throw;
            }
            return objHRA;
        }
        public async Task<byte[]> DownloadHRAReport(int HRAid)
        {
            AddDefaultParameterstoHttpClient(client);
            string URL = string.Format(Constants.WellogoUrl + "hra/api/v1/hras/{0}/reports/download", new object[] { HRAid });
            try
            {
                var response = await client.GetByteArrayAsync(URL);
                return response;
            }
            catch (Exception ex)
            {

                return null;
            }

        }

        public async Task<KyorLoginCheckResponse> CheckKyorVerificationStatus(KyorLoginCheckinput objKyorLoginCheckinput)
        {
            KyorLoginCheckResponse objKyorLoginCheckResponse = new Wellogo.Models.KyorLoginCheckResponse();
            AddDefaultParameterstoHttpClient(client);
            HttpResponseMessage objHttpResponseMessage;
            string url = string.Format(Constants.RestUrl + "api/KyorLogin/CheckKyorVerification");
            var json = JsonConvert.SerializeObject(objKyorLoginCheckinput);
            var Data = new StringContent(json.ToString(), Encoding.UTF8, "application/json");
            objHttpResponseMessage = await client.PostAsync(url, Data);
            try
            {
                string response = await objHttpResponseMessage.Content.ReadAsStringAsync();
                objKyorLoginCheckResponse = JsonConvert.DeserializeObject<KyorLoginCheckResponse>(response);
            }
            catch (Exception ex)
            {

                throw;
            }
            return objKyorLoginCheckResponse;
        }
        public async Task<KyorLoginCheckResponse> UpdateKyorVerificationStatus(KyorLoginCheckinput objKyorLoginCheckinput)
        {


            KyorLoginCheckResponse objKyorLoginCheckResponse = new Wellogo.Models.KyorLoginCheckResponse();
            //AddDefaultParameterstoHttpClient(client);
            HttpResponseMessage objHttpResponseMessage;
            string url = string.Format(Constants.RestUrl + "api/KyorLogin/UpdateKyorVerification");
            var json = JsonConvert.SerializeObject(objKyorLoginCheckinput);
            var Data = new StringContent(json.ToString(), Encoding.UTF8, "application/json");
            objHttpResponseMessage = await client.PostAsync(url, Data);
            try
            {
                string response = await objHttpResponseMessage.Content.ReadAsStringAsync();
                objKyorLoginCheckResponse = JsonConvert.DeserializeObject<KyorLoginCheckResponse>(response);
            }
            catch (Exception ex)
            {

                throw;
            }
            return objKyorLoginCheckResponse;
        }
        public async Task<KyorRegistrationOutput> RegisterForKyor(KyorRegistration objKyorRegistration)
        {
            KyorRegistrationOutput objKyorRegistrationOutput = new Wellogo.Models.KyorRegistrationOutput();
            //  AddDefaultParameterstoHttpClient(client);
            HttpResponseMessage objHttpResponseMessage;
            string url = string.Format(Constants.WellogoUrl + "api/v1/corporate-client-register");
            var json = JsonConvert.SerializeObject(objKyorRegistration);
            var Data = new StringContent(json.ToString(), Encoding.UTF8, "application/json");
            objHttpResponseMessage = await client.PostAsync(url, Data);
            try
            {
                string response = await objHttpResponseMessage.Content.ReadAsStringAsync();
                objKyorRegistrationOutput = JsonConvert.DeserializeObject<KyorRegistrationOutput>(response);
            }
            catch (Exception ex)
            {

                throw;
            }
            return objKyorRegistrationOutput;
        }

        public async Task<object> LoginForKyor(KyorLogin objKyorLogin)
        {
            KyorLoginResponse objKyorLoginData = new Wellogo.Models.KyorLoginResponse();
            //AddDefaultParameterstoHttpClient(client);
            HttpResponseMessage objHttpResponseMessage;
            string url = string.Format(Constants.WellogoUrl + "api/v1/sso/login");
            var json = JsonConvert.SerializeObject(objKyorLogin);
            var Data = new StringContent(json.ToString(), Encoding.UTF8, "application/json");
            objHttpResponseMessage = await client.PostAsync(url, Data);
            string response = "";
            try
            {
                response = await objHttpResponseMessage.Content.ReadAsStringAsync();


                //  objKyorLoginData = JsonConvert.DeserializeObject<KyorLoginResponse>(response);
            }
            catch (Exception ex)
            {

                throw;
            }
            return response;
        }
        public async Task<object> Logout()
        {
            HttpResponseMessage objHttpResponseMessage;
            string url = string.Format(Constants.WellogoUrl + "api/v1/sso/logout");
            var Data = new StringContent("", Encoding.UTF8, "application/json");
            objHttpResponseMessage = await client.PostAsync(url, Data);
            string response = "";
            try
            {
                response = await objHttpResponseMessage.Content.ReadAsStringAsync();
            }
            catch (Exception ex)
            {


                throw;
            }
            return response;
        }
        public Task<string> GetDataAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<UserDetais> GetUserDetails(int UserID)
        {
            UserDetais objUserDetais = new Wellogo.Models.UserDetais();
            HttpResponseMessage objHttpResponseMessage;
            string url = string.Format(Constants.WellogoUrl + "api/v1/clients/{0}", new object[] { UserID });
            objHttpResponseMessage = await client.GetAsync(url);
            string response = "";
            try
            {
                response = await objHttpResponseMessage.Content.ReadAsStringAsync();
                objUserDetais = JsonConvert.DeserializeObject<UserDetais>(response);
            }
            catch (Exception ex)
            {


                throw;
            }
            return objUserDetais;
        }

        public async Task<object> ChangePassword(UserPassword objChangePassword)
        {

            AddDefaultParameterstoHttpClient(client);
            string url = string.Format(Constants.WellogoUrl + "api/v1/clients/old-password");
            var json = JsonConvert.SerializeObject(objChangePassword);
            var Data = new StringContent(json.ToString(), Encoding.UTF8, "application/json");

            HttpResponseMessage objHttpResponseMessage = await client.PutAsync(url, Data);

            string opt = await objHttpResponseMessage.Content.ReadAsStringAsync();
            object response = null;


            try
            {

                if (opt.ToString().Contains("errors"))
                {
                    response = JsonConvert.DeserializeObject<PasswordError>(opt);
                }
                else
                {
                    response = JsonConvert.DeserializeObject<PasswordRootResponse>(opt);
                }

                return response;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<object> UpdateuserDetails(UpdateUserDetails objUpdateUserDetails)
        {

            object obj;
            HttpResponseMessage objHttpResponseMessage;
            string url = string.Format(Constants.WellogoUrl + "api/v1/clients/{0}", new object[] { App.id });
            var json = JsonConvert.SerializeObject(objUpdateUserDetails);
            var input = new StringContent(json.ToString(), Encoding.UTF8, "application/json");
            objHttpResponseMessage = await client.PutAsync(url, input);
            string response = await objHttpResponseMessage.Content.ReadAsStringAsync();
            try
            {
                if (objHttpResponseMessage.IsSuccessStatusCode)
                {
                    obj = JsonConvert.DeserializeObject<UserDetais>(response);
                }
                else
                {
                    obj = JsonConvert.DeserializeObject<PasswordError>(response);
                }
                return obj;
            }
            catch (Exception ex)
            {
                throw;
            }

        }
        public async Task<object> UpdateUserProfilePicture(object objUpdateUserDetails)
        {
            object obj;
            HttpResponseMessage objHttpResponseMessage;
            MultipartFormDataContent objMultipartFormDataContent = (MultipartFormDataContent)objUpdateUserDetails;
            string url = string.Format(Constants.WellogoUrl + "api/v1/clients/{0}/photo", new object[] { App.id });
            AddDefaultParameterstoHttpClient(client);
            objHttpResponseMessage = await client.PostAsync(url, objMultipartFormDataContent);
            string response = await objHttpResponseMessage.Content.ReadAsStringAsync();
            try
            {
                if (objHttpResponseMessage.IsSuccessStatusCode)
                {
                    obj = JsonConvert.DeserializeObject<UserDetais>(response);
                }
                else
                {
                    obj = JsonConvert.DeserializeObject<PasswordError>(response);
                }
                return obj;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public async Task<LeaderboardData> GetLeaderboardUsers()
        {
            LeaderboardData objLeaderboardData = new LeaderboardData();
            HttpResponseMessage objHttpRequestMessage;
            string url = string.Format(Constants.WellogoUrl + "api/v1/clients/{0}/leaderboard", new object[] { App.id });
            AddDefaultParameterstoHttpClient(client);
            objHttpRequestMessage = await client.GetAsync(url);
            string response = "";
            if (objHttpRequestMessage.IsSuccessStatusCode)
            {
                response = await objHttpRequestMessage.Content.ReadAsStringAsync();
                objLeaderboardData = JsonConvert.DeserializeObject<LeaderboardData>(response);
            }
            return objLeaderboardData;
        }
        public async Task<LeaderBoardLogRootObject> GetLeaderboardLog(int pageno)
        {
            LeaderBoardLogRootObject objLeaderBoardLogRootObject = new Wellogo.Models.LeaderBoardLogRootObject();
            HttpResponseMessage objHttpResponseMessage;
            string url = string.Format(Constants.WellogoUrl + "api/v1/clients/{0}/points/log?page={1}", new object[] { App.id, pageno });
            AddDefaultParameterstoHttpClient(client);
            objHttpResponseMessage = await client.GetAsync(url);
            string response = "";
            if (objHttpResponseMessage.IsSuccessStatusCode)
            {
                response = await objHttpResponseMessage.Content.ReadAsStringAsync();
                objLeaderBoardLogRootObject = JsonConvert.DeserializeObject<LeaderBoardLogRootObject>(response);
            }
            return objLeaderBoardLogRootObject;
        }

        public async Task<TrackerAcessOutput> GenerateAccessTokenViaAcessCode(string TrackerType, string Code)
        {
            TrackerAcessOutput objUserDataProfile = new Wellogo.Models.TrackerAcessOutput();
            HttpResponseMessage objHttpResponseMessage;
            string url = string.Format(Constants.WellogoUrl + "api/v1/clients/{0}/generate-access-token", new object[] { App.id });
            AddDefaultParameterstoHttpClient(client);
            JObject JsonObject = new JObject();
            JsonObject.Add("code", Code);
            JsonObject.Add("tracker_type", TrackerType);
            HttpContent objHttpContent = new StringContent(JsonObject.ToString(), Encoding.UTF8, "application/json");
            objHttpResponseMessage = await client.PostAsync(url, objHttpContent);
            string response = "";
            if (objHttpResponseMessage.IsSuccessStatusCode)
            {
                try
                {
                    response = await objHttpResponseMessage.Content.ReadAsStringAsync();
                    objUserDataProfile = JsonConvert.DeserializeObject<TrackerAcessOutput>(response);
                }
                catch (Exception ex)
                {
                    DependencyService.Get<IMessage>().LongAlert(ex.ToString());
                }
            }
            return objUserDataProfile;
        }

        public async Task<Tracker> GetClientTracker()
        {
            Tracker objTracker = new Wellogo.Models.Tracker();
            HttpResponseMessage objHttpResponseMessage;
            string url = string.Format(Constants.WellogoUrl + "api/v1/clients/{0}/tracker", new object[] { App.id });
            AddDefaultParameterstoHttpClient(client);
            objHttpResponseMessage = await client.GetAsync(url);
            string response = "";
            if (objHttpResponseMessage.IsSuccessStatusCode)
            {
                response = await objHttpResponseMessage.Content.ReadAsStringAsync();
                objTracker = JsonConvert.DeserializeObject<Tracker>(response);
            }
            return objTracker;
        }

        public async Task<Tracker> DeleteTrackerData()
        {
            Tracker objTracker = new Wellogo.Models.Tracker();
            HttpResponseMessage objHttpResponseMessage;
            string url = string.Format(Constants.WellogoUrl + "api/v1/clients/{0}/tracker", new object[] { App.id });
            AddDefaultParameterstoHttpClient(client);
            objHttpResponseMessage = await client.DeleteAsync(url);
            string response = "";
            if (objHttpResponseMessage.IsSuccessStatusCode)
            {
                response = await objHttpResponseMessage.Content.ReadAsStringAsync();
                objTracker = JsonConvert.DeserializeObject<Tracker>(response);
            }
            return objTracker;
        }
        
        public async Task<AppNotification> GetNotofications(int pageno)
        {

            AppNotification objAppNotification = new Wellogo.Models.AppNotification();
            HttpResponseMessage objHttpResponseMessage;
            string url = string.Format(Constants.WellogoUrl + "api/v1/clients/{0}/notifications?page={1}", new object[] { App.id, pageno });
            AddDefaultParameterstoHttpClient(client);
            objHttpResponseMessage = await client.GetAsync(url);
            string response = "";
            if (objHttpResponseMessage.IsSuccessStatusCode)
            {
                response = await objHttpResponseMessage.Content.ReadAsStringAsync();
                objAppNotification = JsonConvert.DeserializeObject<AppNotification>(response);
            }
            return objAppNotification;
        }


        public async Task<Deals> GetDeals()
        {
            try
            {
                Deals objDeals = new Wellogo.Models.Deals();
                HttpResponseMessage objHttpResponseMessage;
                string url = string.Format(Constants.WellogoUrl + "api/v1/deals");
                AddDefaultParameterstoHttpClient(client);
                objHttpResponseMessage = await client.GetAsync(url);
                string response = "";
                if (objHttpResponseMessage.IsSuccessStatusCode)
                {
                    response = await objHttpResponseMessage.Content.ReadAsStringAsync();
                    objDeals = JsonConvert.DeserializeObject<Deals>(response);
                }
                return objDeals;
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public async Task<EarnPoints> HowToEarnPoints()
        {
            try
            {
                EarnPoints objEarnPoints = new Wellogo.Models.EarnPoints();
                HttpResponseMessage objHttpResponseMessage;
                string url = string.Format(Constants.WellogoUrl + "api/v1/corporates/{0}/points-actions", new object[] {App.KyorData.corporate_id });
                AddDefaultParameterstoHttpClient(client);
                objHttpResponseMessage = await client.GetAsync(url);
                string response = "";
                if (objHttpResponseMessage.IsSuccessStatusCode)
                {
                    response = await objHttpResponseMessage.Content.ReadAsStringAsync();
                    objEarnPoints = JsonConvert.DeserializeObject<EarnPoints>(response);
                }
                return objEarnPoints;
            }
            catch (Exception ex)
            {

                throw;
            }
        }


        public async Task<SingleDeal> GetSingleDeal(int partnerid, int dealid)
        {
            try
            {
                SingleDeal objSingleDeal = new Wellogo.Models.SingleDeal();
                HttpResponseMessage objHttpResponseMessage;
                string url = string.Format(Constants.WellogoUrl + "api/v1/partners/{0}/deals/{1}", new object[] { partnerid,dealid });
                AddDefaultParameterstoHttpClient(client);
                objHttpResponseMessage = await client.GetAsync(url);
                string response = "";
                if (objHttpResponseMessage.IsSuccessStatusCode)
                {
                    response = await objHttpResponseMessage.Content.ReadAsStringAsync();
                    objSingleDeal = JsonConvert.DeserializeObject<SingleDeal>(response);
                }
                return objSingleDeal;
            }
            catch (Exception ex)
            {

                throw;
            }
        }


        public async Task<object> RedeemDeal(int Dealid)
        {
            AddDefaultParameterstoHttpClient(client);
            string url = string.Format(Constants.WellogoUrl + "api/v1/clients/{0}/points/redeem",new object[] {App.id });
            JObject objJObject = new JObject();
            objJObject.Add("deal_id", Dealid.ToString());
            var Data = new StringContent(objJObject.ToString(), Encoding.UTF8, "application/json");
            HttpResponseMessage objHttpResponseMessage = await client.PostAsync(url, Data);
            string opt = await objHttpResponseMessage.Content.ReadAsStringAsync();
            RedeemDealResponse objRedeemDealResponse = new RedeemDealResponse();
            try
            {
                if (opt.ToString().Contains("errors"))
                {
                    RedeemDealError objRedeemDealError= JsonConvert.DeserializeObject<RedeemDealError>(opt);
                    return objRedeemDealError;
                }
                else
                {
                    objRedeemDealResponse = JsonConvert.DeserializeObject<RedeemDealResponse>(opt);
                    return objRedeemDealResponse;
                }
            }
            catch (Exception ex)
            {
              
                return null;
            }
        }



        #endregion Kyor

        #region KyorNow
        public async Task<object> GetDoctorsList(string sortby = "name", string page = "1", string limit = "10", params string[] include)
        {
            if (include.Length == 0)
            {
                include[0] = "profile";
                include[1] = "specialities";
                include[2] = "languages";
                include[3] = "education";
                include[4] = "reviews";
            }
            HttpResponseMessage objHttpResponse;
            AddDefaultParameterstoHttpClientKyorNow(client);
            string Url = string.Format(Constants.KyorNowUrl + "v1/doctors?order_by=asc&sort_by= " + sortby + " &include=" + string.Join(",", include) + "&page=" + page + "&limit=" + limit + "");
            objHttpResponse = await client.GetAsync(Url);
            string response = "";
            if (objHttpResponse.IsSuccessStatusCode)
            {
                response = await objHttpResponse.Content.ReadAsStringAsync();
                DoctorsList objDoctorsList = JsonConvert.DeserializeObject<DoctorsList>(response);
                return objDoctorsList;
            }
            else
            {
                return null;
            }
        }
        #endregion



    }
}