using EMeditekApp.Wellogo.Models;
using Plugin.Connectivity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace EMeditekApp.Wellogo
{
   
    public partial class index : ContentPage
    {
        public bool InternetConnection
        {
            get
            {
                return CrossConnectivity.Current.IsConnected;
            }
        }
        public static eWellogoHomeTab LastTab { get; set; }
        public index()
        {
            InitializeComponent();
            try
            {
                GC.Collect();
                if (InternetConnection)
                {
                    if (LastTab == eWellogoHomeTab.HealthServices)
                    {
                        TapGestureRecognizer_Tapped_Services(null, null);
                    }
                    else
                    {
                        TapGestureRecognizer_Tapped_Home(null, null);
                    }
                }
                else
                {
                    AskForRetry();
                }
            }
            catch (Exception ex)
            {
               DependencyService.Get<IMessage>().LongAlert();
            }
        }
        async void AskForRetry()
        {
            var Retry = await DisplayAlert("No Connection", "Please Check Your Connection", "Retry", "Cancel");
            if (Retry)
            {
                if (LastTab == eWellogoHomeTab.HealthServices)
                {
                    TapGestureRecognizer_Tapped_Services(null, null);
                }
                else
                {
                    TapGestureRecognizer_Tapped_Home(null, null);
                }
            }
        }

        private async void TapGestureRecognizer_Tapped_Home(object sender, EventArgs e)
        {
            try
            {

                if (InternetConnection)
                {
                    var page = new HealthServices();
                    placeholder.Content = page.Content;
                    imgHome.IconColor = Color.Red;
                    imghealthservices.IconColor = Color.Black;
                    icnServices.IconColor = Color.Black;
                    boxHome.IsVisible = true;
                    boxSocial.IsVisible = false;
                    boxHealthServices.IsVisible = false;

                    lblHome.TextColor = Color.Red;
                    lblHealth.TextColor = Color.Default;
                    lblSocial.TextColor = Color.Default;
                    //stkHome.BackgroundColor = Color.FromHex("#f44337") ;
                    //stkHealthServices.BackgroundColor = Color.AliceBlue;
                }
                else
                {
                    var Retry = await DisplayAlert("No Connection", "Please Check Your Connection", "Retry", "Cancel");
                    if (Retry)
                    {
                        TapGestureRecognizer_Tapped_Home(sender, e);
                    }
                }
            }
            catch (Exception ex)
            {
                DependencyService.Get<IMessage>().LongAlert();
            }
        }
        private async void TapGestureRecognizer_Tapped_Services(object sender, EventArgs e)
        {
            try
            {
                if (InternetConnection)
                {
                    var page = new Home();
                    placeholder.Content = page.Content;
                    imgHome.IconColor = Color.Black;
                    imghealthservices.IconColor = Color.Red;
                    icnServices.IconColor = Color.Black;
                    boxHome.IsVisible = false;
                    boxSocial.IsVisible = false;
                    boxHealthServices.IsVisible = true;

                    lblHome.TextColor = Color.Default;
                    lblHealth.TextColor = Color.Red;
                    lblSocial.TextColor = Color.Default;
                }
                else
                {
                    var Retry = await DisplayAlert("No Connection", "Please Check Your Connection", "Retry", "Cancel");
                    if (Retry)
                    {
                        TapGestureRecognizer_Tapped_Services(sender,e);
                    }
                }
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

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            try
            {
                App.SetupRedirection(new Wellogo.Social());
                App.Current.MainPage = App.MasterDetailPage;

                 imgHome.IconColor = Color.Black;
                 imghealthservices.IconColor = Color.Black;
                 icnServices.IconColor = Color.Red;
                boxHome.IsVisible = false;
                boxSocial.IsVisible = true;
                boxHealthServices.IsVisible = false;


                lblHome.TextColor = Color.Default;
                lblHealth.TextColor = Color.Default;
                lblSocial.TextColor = Color.Red;
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        private void MenuItem1_Clicked(object sender, EventArgs e)
        {

        }
    }

    public enum eWellogoHomeTab
    {
        Home,HealthServices,Social
    }
    
}