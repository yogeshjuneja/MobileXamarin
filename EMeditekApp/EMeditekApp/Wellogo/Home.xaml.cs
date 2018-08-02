using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using FormsPlugin.Iconize;
using Plugin.Connectivity;
using EMeditekApp.Wellogo.Models;
using EMeditekApp.Wellogo.KyorNow;
using EMeditekApp.Models;

namespace EMeditekApp.Wellogo
{

    public partial class Home : ContentPage
    {
        public bool InternetConnection
        {
            get
            {
                return CrossConnectivity.Current.IsConnected;
            }
        }
        public Page pge { get; set; }
        public Home()
        {
            InitializeComponent();
            if (InternetConnection)
            {
                BindMainMenu();
            }
            else
            {
                AskForRetry();
            }
        }

        async void AskForRetry()
        {
            var Retry = await DisplayAlert("No Connection", "Please Check Your Connection", "Retry", "Cancel");
            if (Retry)
            {
                BindMainMenu();
            }
        }

        async void BindMainMenu()
        {
            if (App.UserMenu.Count > 0)
            {
                LoadMenuFromLocalDB();
            }
            else
            {
                LoadMenuFromApi();
            }
        }
        private void LoadMenuFromLocalDB()
        {
            overlay.IsVisible = true;
            List<UserMenu> lstusermenu = App.UserMenu.OrderBy(o=>o.sequence).ToList();
             
            List<WellogoMainMenu> lstWellogoMainMenu = new List<WellogoMainMenu>();
            eHomeMenuItems page = 0;
            foreach (UserMenu u in lstusermenu)
            {
                page = (eHomeMenuItems)u.id;
                if (page == eHomeMenuItems.HealthCheckups)
                {
                    lstWellogoMainMenu.Add(new WellogoMainMenu(Color.OrangeRed, u.name, eHomeMenuItems.HealthCheckups, "Exclusive health checks tailor made for you, covering all the tests to diagnose common ailments from the network of reputed labs across country. Get yourself screened today.", "executivehealth.png")); continue;
                }
                if (page == eHomeMenuItems.HomeHealth)
                {
                    lstWellogoMainMenu.Add(new WellogoMainMenu(Color.LightGreen, u.name, eHomeMenuItems.HomeHealth, "Get well at home. Quality  healthcare right at your doorstep.", "heartrate.png")); continue;
                }
                if (page == eHomeMenuItems.HealthRiskAssesment)
                {
                    lstWellogoMainMenu.Add(new WellogoMainMenu(Color.SkyBlue, u.name, eHomeMenuItems.HealthRiskAssesment, "Learn more about your health risks and what you need to do to ticking.", "heartrate.png")); continue;
                }
                if (page == eHomeMenuItems.OrderMedicine)
                {
                    lstWellogoMainMenu.Add(new WellogoMainMenu(Color.Orange, u.name, eHomeMenuItems.OrderMedicine, "Your only trustworthy online pharmacy delivery service right at your doorstep. And don't miss the fabulous offers.", "medicine.png")); continue;
                }
                if (page == eHomeMenuItems.AmbulanceServices)
                {
                  lstWellogoMainMenu.Add(new WellogoMainMenu(Color.LightBlue, u.name, eHomeMenuItems.AmbulanceServices, "A handy directory of reliable ambulance service providers near you.", "ambulance.png"));continue;
                }
                if (page == eHomeMenuItems.BloodBanks)
                {
                    lstWellogoMainMenu.Add(new WellogoMainMenu(Color.Red, u.name, eHomeMenuItems.BloodBanks, "A convinient located list of trusted blood banks in your location.", "bloodbank.png")); continue;
                }
                if (page == eHomeMenuItems.Blog)
                {
                    lstWellogoMainMenu.Add(new WellogoMainMenu(Color.GreenYellow, u.name, eHomeMenuItems.Blog, "Read health blogs written by experts to help you get a better and healthier life.", "blog.png")); continue;
                }
                if (page == eHomeMenuItems.TalkToDoctor)
                {
                    lstWellogoMainMenu.Add(new WellogoMainMenu(Color.Purple, u.name, eHomeMenuItems.TalkToDoctor, "Talk to a doctor anytime, anywhere over video, voice or text chat. Speak to a general physician or a specialist.", "talktodoctor.png")); continue;
                }
                if (page == eHomeMenuItems.FitnessTracker)
                {
                    lstWellogoMainMenu.Add(new WellogoMainMenu(Color.MediumVioletRed, u.name, eHomeMenuItems.FitnessTracker, "Integrate fitness tracker and earn koins.", "fitness.png")); continue;
                }
            }
            lstWellogo.ItemsSource = lstWellogoMainMenu;

            overlay.IsVisible = false;
        }
        private   async void LoadMenuFromApi()
        {
            overlay.IsVisible = true;
            UserDetais objUserDetais = await App.TodoManager.UserDetais(App.id);
            List<UserMenu> lstUserDetais = new List<UserMenu>();
            var menu = new UserMenu();

            List<Service> services = objUserDetais.data.branch.corporate.services.OrderBy(o=>o.sequence).ToList();
            if (objUserDetais.data != null)
            {
                foreach (Service s in services)
                {
                    menu.id = s.id;
                    menu.name = s.name;
                    menu = new UserMenu();
                    lstUserDetais.Add(menu);
                }
                App.UserMenu = lstUserDetais;
                App.Database.SaveUserMenu(lstUserDetais);
                LoadMenuFromLocalDB();
                overlay.IsVisible = false;
            }
        }
        private void TapGestureRecognizer_Name(object sender, EventArgs e)
        {
            try
            {
                
                overlay.IsVisible = true;
                dynamic obj = e;
                eHomeMenuItems pge = (eHomeMenuItems)obj.Parameter;
                if (pge == eHomeMenuItems.HealthCheckups)
                {
                    Navigation.PushAsync(new HealthCheckups());
                  //  App.Current.MainPage = new NavigationPage(new HealthCheckups()) { BarBackgroundColor = Color.FromHex("#f44337"), BarTextColor = Color.White };
                }
                else if (pge == eHomeMenuItems.HomeHealth)
                {
                    Navigation.PushAsync(new HCAHServices());
                    //App.Current.MainPage = new NavigationPage(new HCAHServices()) { BarBackgroundColor = Color.FromHex("#f44337"), BarTextColor = Color.White };
                }
                else if (pge == eHomeMenuItems.HealthRiskAssesment)
                {
                    Navigation.PushAsync(new HealthRiskAssesment());
                   // App.Current.MainPage = new NavigationPage(new HealthRiskAssesment()) { BarBackgroundColor = Color.FromHex("#f44337"), BarTextColor = Color.White };
                }
                else if (pge == eHomeMenuItems.BloodBanks)
                {
                    Navigation.PushAsync(new BloodBanks());
                   // App.Current.MainPage = new NavigationPage(new BloodBanks()) { BarBackgroundColor = Color.FromHex("#f44337"), BarTextColor = Color.White };
                }
                else if (pge == eHomeMenuItems.AmbulanceServices)
                {
                    Navigation.PushAsync(new AmbulanceServices());
                  //  App.Current.MainPage = new NavigationPage(new AmbulanceServices()) { BarBackgroundColor = Color.FromHex("#f44337"), BarTextColor = Color.White };
                }
                else if (pge == eHomeMenuItems.TalkToDoctor)
                {
                    Navigation.PushAsync(new ExplainProblem());
                 //   App.Current.MainPage = new NavigationPage(new ExplainProblem()) { BarBackgroundColor = Color.FromHex("#f44337"), BarTextColor = Color.White };
                    //App.Current.MainPage = new NavigationPage(new TalkToDoctor()) { BarBackgroundColor = Color.FromHex("#f44337"), BarTextColor = Color.White };
                }
                else if (pge == eHomeMenuItems.OrderMedicine)
                {

                    Navigation.PushAsync(new UploadPrescription());
               //     App.Current.MainPage = new NavigationPage(new UploadPrescription()) { BarBackgroundColor = Color.FromHex("#f44337"), BarTextColor = Color.White };
                }
                else if (pge == eHomeMenuItems.Blog)
                {

                    Navigation.PushAsync(new ViewBlog());
                  //  App.Current.MainPage = new NavigationPage(new ViewBlog()) { BarBackgroundColor = Color.FromHex("#f44337"), BarTextColor = Color.White };
                }
                else if (pge == eHomeMenuItems.FitnessTracker)
                {
                    Navigation.PushAsync(new FitnessTracker());
                  //  App.Current.MainPage = new NavigationPage(new FitnessTracker()) { BarBackgroundColor = Color.FromHex("#f44337"), BarTextColor = Color.White };
                }
                overlay.IsVisible = false;
            }
            catch (Exception ex)
            {
                DependencyService.Get<IMessage>().LongAlert();
            }
        }
        private async void TapGestureRecognizer_icon(object sender, EventArgs e)
        {
            try
            {
                dynamic obj = e;
                string[] param = obj.Parameter;
                string Name = param[0].ToString();
                string Description = param[1].ToString();
                await DisplayAlert(Name, Description, "OK");
            }
            catch (Exception ex)
            {

                DependencyService.Get<IMessage>().LongAlert();
            }
        }
        protected override bool OnBackButtonPressed()
        {
            ShowExitDialog();
            return true;
        }

        private async void ShowExitDialog()
        {
            try
            {
                var answer = await DisplayAlert("Exit", "Do you wan't to exit the App?", "Yes", "No");
                if (answer)
                {
                    DependencyService.Get<IExitApp>().Exit();
                    // Android.OS.Process.KillProcess(Android.OS.Process.MyPid());
                }
            }
            catch (Exception ex)
            {

            }
        }

        private void lstWellogo_Refreshing(object sender, EventArgs e)
        {
            lstWellogo.IsRefreshing = false;
            LoadMenuFromApi();
            lstWellogo.IsRefreshing = false;

        }
    }
    public class WellogoMainMenu
    {
        public string Name { get; set; }
        public eHomeMenuItems Page { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public string[] Name_Description { get; set; }
        public Color Color { get; set; }

        public WellogoMainMenu(Color _color, string _Name, eHomeMenuItems _page = 0, string _description = "", string _image = "")
        {
            Name = _Name;
            Page = _page;
            Description = _description;
            Image = _image;
            Name_Description = new string[] { _Name, _description };
            Color = _color;
        }
    }
    public enum eHomeMenuItems
    {
        HealthCheckups = 13, HomeHealth = 6, HealthRiskAssesment = 5, OrderMedicine = 14, AmbulanceServices = 2, BloodBanks = 1, Blog = 3, TalkToDoctor = 11, FitnessTracker = 8
    }


}