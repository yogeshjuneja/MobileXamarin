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
   
    public partial class BloodBankFilter : ContentPage
    {
        eKyorType objeKyorType;
        string StateName = null;
        string CityName = null;
        StateCity objStateCity = new StateCity();
        public bool InternetConnection
        {
            get
            {
                return CrossConnectivity.Current.IsConnected;
            }
        }
        public BloodBankFilter(eKyorType eKyorType, string StateName = "Any", string CityName = "Any")
        {
            InitializeComponent();
            try
            {
                objeKyorType = eKyorType;
                if (Device.RuntimePlatform == "iOS")
                {
                    ToolbarItems.Add(new ToolbarItem("Back", "", () => { Navigation.PopModalAsync(true); }));
                }
                if (Device.RuntimePlatform == "Android")
                {
                    ToolbarItems.Add(new ToolbarItem("Back", "down.png", () => { Navigation.PopModalAsync(true); }));
                }
                if (InternetConnection)
                {
                    Bind(eKyorType, StateName, CityName);
                }
                else
                {
                    AskForRetry(eKyorType, StateName, CityName);
                }



            }
            catch (Exception ex)
            {

               DependencyService.Get<IMessage>().LongAlert();
            }

        }
        async void AskForRetry(eKyorType eKyorType, string StateName, string CityName)
        {
            var Retry = await DisplayAlert("No Connection", "Please Check Your Connection", "Retry", "Cancel");
            if (Retry)
            {
                Bind(eKyorType, StateName, CityName);
            }
        }


        void Bind(eKyorType eKyorType, string StateName, string CityName)
        {
            overlay.IsVisible = true;
            objStateCity = Task.Run(() => App.TodoManager.GetStateOrCity(eKyorType)).Result;
            List<string> lstState = objStateCity.data;
            lstState.Insert(0, "Any");
            pkrState.ItemsSource = lstState;

            if (StateName != "")
                pkrState.SelectedItem = StateName;
            if (CityName != "")
            {
                pkrCity.SelectedItem = CityName;
            }
            overlay.IsVisible = false;
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            try
            {
                StateName = !string.IsNullOrEmpty(pkrState.SelectedItem.ToString()) | pkrState.SelectedItem.ToString() == "Any" ? pkrState.SelectedItem.ToString() : "";
                CityName = !string.IsNullOrEmpty(pkrCity.SelectedItem.ToString()) | pkrCity.SelectedItem.ToString() == "Any" | pkrCity.SelectedItem.ToString() != null ? pkrCity.SelectedItem.ToString() : "";
                if (objeKyorType == eKyorType.BloodBank)
                {
                    Navigation.PushAsync(new BloodBanks(StateName, CityName));
                }
                else
                {
                    Navigation.PushAsync(new AmbulanceServices(StateName, CityName));
                }
            }
            catch (Exception ex)
            {

               DependencyService.Get<IMessage>().LongAlert();
            }
        }
        private void Button_Clicked_1(object sender, EventArgs e)
        {
            try
            {
                Navigation.PopModalAsync(true);
            }
            catch (Exception ex)
            {

               DependencyService.Get<IMessage>().LongAlert();
            }
        }
        private  async void pkrState_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (InternetConnection)
                {
                    overlay.IsVisible = true;
                    objStateCity = Task.Run(() => App.TodoManager.GetStateOrCity(objeKyorType, pkrState.SelectedItem.ToString())).Result;
                    List<string> lstCity = objStateCity.data;
                    lstCity.Insert(0, "Any");
                    pkrCity.ItemsSource = lstCity;
                    pkrCity.SelectedItem = "Any";
                    overlay.IsVisible = false;
                }
                else
                {
                    var Retry = await DisplayAlert("No Connection", "Please Check Your Connection", "Retry", "Cancel");
                    if (Retry)
                    {
                        pkrState_SelectedIndexChanged(sender, e);
                    }
                }
            }
            catch (Exception ex)
            {

                DependencyService.Get<IMessage>().LongAlert();
            }

        }
    }
}