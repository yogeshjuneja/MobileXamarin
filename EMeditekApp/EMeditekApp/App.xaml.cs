using EMeditekApp.Models;
using EMeditekApp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;
using System.Threading.Tasks;
using System.Diagnostics;
using EMeditekApp.Wellogo.Models;
using EMeditekApp.Wellogo;

// eicore technologies new branch
namespace EMeditekApp
{
    public partial class App : Application
    {
        static DataAccess database;
        public static TodoItemManager TodoManager { get; set; }
        public static int id { get; set; }
    
        public static string ConsumerToken
        {
            get
            {
                return "4bd8a66a2692d961095d420a59ef1dc7d8527551";
            }
        }
        public static string CurrentTimestamp
        {
            get
            {
                return Stopwatch.GetTimestamp().ToString();
            }
        }
        public static string Authorization { get; set; }

        public static MasterDetailPage MasterDetailPage;
        //public static string imagePath = "http://192.168.1.206/webportal/MobileDownload.aspx?FileName=";
        public static string imagePath = "http://emeditek.in/webportal/MobileDownload.aspx?FileName=";
        public static string Url = "http://docs.emeditek.co.in/";
        //public static string Url = "http://192.168.1.243/";
        public static List<UserMenu> UserMenu { get; set; }
        public static void SetupRedirection(Page objPage)

        {
            try
            {
                
                if (objPage != null)
                {
                    MasterDetailPage = new MasterDetailPage
                    {
                        Master = new MenuPage(){Icon=new FileImageSource(){File="hamburger.png"}},
                        Detail = new NavigationPage(objPage) { BarBackgroundColor = Color.FromHex("#f44337"), BarTextColor = Color.White }
                    };
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }


        public static DataAccess Database
        {
            get
            {
                if (database == null)
                {
                    database = new DataAccess();
                }
                return database;
            }
        }
        public static KyorLoginData KyorData { get; set; }

       
        public static string CorporateImageURl { get;   set; }
        public static string Gender { get; internal set; }

        public App(string Integrationtype=null,string code=null)
        {
           try
            {
                InitializeComponent();
               TodoManager = new TodoItemManager(new RestService());
                KyorData = new KyorLoginData();
                //App.SetupRedirection(new Wellogo.index()); MainPage = MasterDetailPage;
                //return;   //Line to be commented
                if (!string.IsNullOrEmpty(App.Authorization) && string.IsNullOrEmpty(Integrationtype) && string.IsNullOrEmpty(code))
                {
                    App.SetupRedirection(new Wellogo.Tabbedpage.TabbedPage1());
                    App.Current.MainPage = App.MasterDetailPage;
                }
                else
                {
                    LoggedInUser objLogedInUser = App.Database.IsUserLogedIn();
                    List<UserMenu> lstUserMenu = App.database.HasMenu();
                  
                    if (objLogedInUser != null  )
                    {
                        if(!string.IsNullOrEmpty(objLogedInUser.sso_token))
                        {
                            if (lstUserMenu.Count > 0)
                            {
                                UserMenu = lstUserMenu;
                            }
                            App.id = objLogedInUser.id;
                            App.Authorization = objLogedInUser.sso_token;
                            App.KyorData.name = objLogedInUser.Name;
                            App.KyorData.photo_url = objLogedInUser.photo_url;
                            App.KyorData.designation = objLogedInUser.Designation;
                            App.KyorData.corporate_id = objLogedInUser.CorporateID;
                            App.CorporateImageURl = objLogedInUser.CorporateLogo;
                            App.Gender = objLogedInUser.Gender;
                            //App.MaritalStatus = objLogedInUser.MaritalStatus;
                            //App.SetupRedirection(new Wellogo.index());
                            //App.Current.MainPage = App.MasterDetailPage;

                            if (Integrationtype != null && code != null)
                            {
                                if (Device.RuntimePlatform == "iOS")
                                {
                                    //FitnessTracker.integrationType = Integrationtype;
                                    //FitnessTracker.Code = code;
                                    App.SetupRedirection(new Wellogo.Tabbedpage.TabbedPage1(Integrationtype, code));
                                    App.Current.MainPage = App.MasterDetailPage;
                              //    MessagingCenter.Send<App, List<string>>((App)Xamarin.Forms.Application.Current, "fitnessintegratedios", new List<string> { Integrationtype, code });
                                }
                                else
                                {
                                    App.Current.MainPage = new NavigationPage(new FitnessTracker(Integrationtype, code)) { BarBackgroundColor = Color.FromHex("#f44337"), BarTextColor = Color.White };
                                }
                            }
                            else
                            {
                                App.SetupRedirection(new Wellogo.Tabbedpage.TabbedPage1());
                                App.Current.MainPage = App.MasterDetailPage;
                            }
                        
                            //MainPage = MasterDetailPage;
                        }
                        else
                        {
                            Signin();
                        }
                        //App.Database.SaveItem(objLogedInUser);
                    }
                    else
                    {
                        Signin();
                    }
                }
            }
            catch (Exception ex)
            {
                DependencyService.Get<IMessage>().LongAlert(ex.ToString());
            }

        }

        private void Signin()
        {
            App.SetupRedirection(new SignInPage());
            MainPage = new SignInPage();
        }

        //private async Task<bool> CheckValidUser(LoginModel objLoginModel)
        //{
        //    LoginResult objLoginResult = new LoginResult();
        //    objLoginResult = await App.TodoManager.ValidateUser(objLoginModel);
        //    if (objLoginResult.Message != null && Convert.ToString(objLoginResult.Message).ToLower() == "success")
        //    {
        //        return true;
        //    }
        //    else
        //        return false;
        //}
        protected override void OnStart()
        {
    
            // Handle when your app starts
        }
        
        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }
        protected override void OnResume()
       {
            //LoggedInUser objLogedInUser = App.Database.IsUserLogedIn();
            //if (objLogedInUser != null)
            //{
            //    CreateSession(objLogedInUser);
            //    App.CardId = Convert.ToInt64(objLogedInUser.CardId);                
            //    App.SetupRedirection(new LandingPage());
            //    MainPage = MasterDetailPage;
            //}
            //else
            //{
            //    MainPage = new SignInPage();
            //}
            // Handle when your app resumes
        }


    }


    public enum eClientLoginType
    {
        Agent = 1, CorporateHRInsurer, Admin, Broker, Hospital, HospitalEmpanelment, DevelopmentOfficer, CardHolder
    }
}
