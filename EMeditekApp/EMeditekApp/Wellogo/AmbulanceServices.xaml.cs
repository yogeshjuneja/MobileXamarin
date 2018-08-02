using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using EMeditekApp.Wellogo.Models;
using Plugin.Connectivity;

namespace EMeditekApp.Wellogo
{
   
    public partial class AmbulanceServices : ContentPage
    {
        public bool InternetConnection
        {
            get
            {
                return CrossConnectivity.Current.IsConnected;
            }
        }
        public AmbulanceServices(string State = "", string city = "")
        {
            try
            {
                InitializeComponent();
                if (Device.RuntimePlatform == "iOS")
                {
                  //  ToolbarItems.Add(new ToolbarItem("<Back", "", () => { App.SetupRedirection(new index()); App.Current.MainPage = App.MasterDetailPage; }));
                    ToolbarItems.Add(new ToolbarItem("Filter", "", () =>
                    {
                        Navigation.PushModalAsync( new NavigationPage(new BloodBankFilter(Models.eKyorType.Emergency, State == "" ? "Any" : State, city == "" ? "Any" : city)) { BarBackgroundColor = Color.FromHex("#f44337"), BarTextColor = Color.White } );
                    }));
                }
                if (Device.RuntimePlatform == "Android")
                {
                  
                    ToolbarItems.Add(new ToolbarItem("Filter", "filter.png", () =>
                    {
                        Navigation.PushModalAsync(new NavigationPage(new BloodBankFilter(Models.eKyorType.Emergency, State == "" ? "Any" : State, city == "" ? "Any" : city)) { BarBackgroundColor = Color.FromHex("#f44337"), BarTextColor = Color.White });
                    }));
                  //  ToolbarItems.Add(new ToolbarItem("Home", "back.png", () => { App.SetupRedirection(new index()); App.Current.MainPage = App.MasterDetailPage; }));
                }


                if (InternetConnection)
                {
                    BindData(State, city);
                }
                else
                {
                    AskForRetry(State, city);
                }

            }
            catch (Exception ex)
            {

               DependencyService.Get<IMessage>().LongAlert();
            }
        }
        async void AskForRetry(string state, string city)
        {
            var Retry = await DisplayAlert("No Connection", "Please Check Your Connection", "Retry", "Cancel");
            if (Retry)
            {
                BindData(state, city);
            }
        }

        private void lstBloodBanks_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            try
            {
                BloodBankMainData objBloodBankMainData = (BloodBankMainData)e.Item;
                var page =new BloodBankDetail(objBloodBankMainData);
                Navigation.PushAsync(page);
            }
            catch (Exception ex)
            {

               DependencyService.Get<IMessage>().LongAlert();
            }
        }

        async void BindData(string State,string City)
        {
            try
            {
                 
                overlay.IsVisible = true;

                AmbulanceServicesModel objAmbulanceServicesModel = await App.TodoManager.GetAmbulanceServices(State == "Any" ? "" : State, City == "Any" ? "" : City);
                lstBloodBanks.ItemsSource = objAmbulanceServicesModel.data;
                txtTotalNo.Text = objAmbulanceServicesModel.data.Count.ToString();                
                overlay.IsVisible = false;

            }
            catch (Exception ex)
            {
                overlay.IsVisible = false;
                DependencyService.Get<IMessage>().LongAlert();
            }
        }
       
    }
}