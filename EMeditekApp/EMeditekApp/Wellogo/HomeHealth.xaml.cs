using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EMeditekApp.Wellogo.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Plugin.Connectivity;


namespace EMeditekApp.Wellogo
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HomeHealth : ContentPage
    {
        public bool InternetConnection
        {
            get
            {
                return CrossConnectivity.Current.IsConnected;
            }
        }
        public HomeHealth()
        {
            InitializeComponent();
            try
            {
                if (Device.RuntimePlatform == "iOS")
                {
                    ToolbarItems.Add(new ToolbarItem("Back", "", () => { App.SetupRedirection(new index()); App.Current.MainPage = App.MasterDetailPage; }));
                }
                if (Device.RuntimePlatform == "Android")
                {
                    ToolbarItems.Add(new ToolbarItem("Home", "back.png", () => { App.SetupRedirection(new index()); App.Current.MainPage = App.MasterDetailPage; }));
                }
                if (InternetConnection)
                {
                    BindData();
                }
                else
                {
                    AskForRetry();
                }
            }
            catch (Exception ex)
            {
                DisplayAlert("Exception", ex.ToString(), "Cancel");
            }
        }

        async void AskForRetry()
        {
            var Retry = await DisplayAlert("No Connection", "Please Check Your Connection", "Retry", "Cancel");
            if (Retry)
            {
                BindData();
            }
        }
        protected override bool OnBackButtonPressed()
        {
            try
            {
                App.SetupRedirection(new index());
                App.Current.MainPage = App.MasterDetailPage;
            }
            catch (Exception ex)
            {

                DisplayAlert("Exception", ex.ToString(), "Cancel");
            }
            return true;
        }
        private void listHomeHealth_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            try
            {
                var Item = e.Item as HCAH;
                string PageName =  Item.name;
                var page = new NavigationPage(new HomeHealthForm(Item));
                Navigation.PushModalAsync(page);
            }
            catch (Exception ex)
            {
                DisplayAlert("Exception", ex.ToString(), "Cancel");
            }
        }


        async void BindData()
        {
            try
            {
                overlay.IsVisible = true;
                HCAHData lstHomeHealthData = await App.TodoManager.HCAHServices();
                listHomeHealth.ItemsSource = lstHomeHealthData.data;
                overlay.IsVisible = false;
            }
            catch (Exception ex)
            {
               await DisplayAlert("Exception", ex.ToString(), "Cancel");
            }
        }

    }






  


     

   
}