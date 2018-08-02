using EMeditekApp.Wellogo.Models;
using Plugin.Connectivity;
using System;

using Xamarin.Forms;

namespace EMeditekApp.Wellogo
{

    public partial class AHCCheckout : ContentPage
    {
        public bool InternetConnection
        {
            get
            {
                return CrossConnectivity.Current.IsConnected;
            }
        }
        public bool ButtonClicked { get; set; }
        public int CheckupId { get; set; }
        public int EditPatientID { get; set; }
        public AHCCheckout(int _CheckupId)
        {
            ButtonClicked = false;
            InitializeComponent();
            CheckupId = _CheckupId;
            if (InternetConnection)
            {

                BindData();
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
                BindData();
            }
        }
        async void BindData()
        {
            try
            {
                overlay.IsVisible = true;
                OrderSummary objOrderSummary = await App.TodoManager.GetOrderSummary(CheckupId);
                if (objOrderSummary.status=="success")
                {
                  lstCheckout.ItemsSource = objOrderSummary.data.patients;
                }
                overlay.IsVisible = false;
            }
            catch (Exception ex)
            {
                overlay.IsVisible = false;
                DependencyService.Get<IMessage>().LongAlert();
            }
        }
        private async void btnPlaceOrder_Clicked(object sender, EventArgs e)
        {
            try
            {
                if (InternetConnection)
                {
                    AHCPlaceOrder objAHCPlaceOrder = await App.TodoManager.PlaceAHCOrder(CheckupId);

                    if (objAHCPlaceOrder.status == "success")
                    {
                            await DisplayAlert("Order Placed", "Order Placed successfully", "OK");
                            App.SetupRedirection(new Tabbedpage.TabbedPage1());
                            App.Current.MainPage = App.MasterDetailPage;
                    }
                }
                else
                {
                    var Retry = await DisplayAlert("No Connection", "Please Check Your Connection", "Retry", "Cancel");
                    if (Retry)
                    {
                        btnPlaceOrder_Clicked(sender,e);
                    }
                }


            }
            catch (Exception ex)
            {

                DependencyService.Get<IMessage>().LongAlert();
            }
        }

        private void btneditOrder_Clicked(object sender, EventArgs e)
        { 

            try
            {
                ButtonClicked = true;
                var objHealthCheckUpDeafult = ((Button)sender).CommandParameter;
          //     MessagingCenter.Send<App, OrderSummaryPatient>((App)Xamarin.Forms.Application.Current, "EditAHCPatient", (OrderSummaryPatient)objHealthCheckUpDeafult);
                EditPatientID = ((OrderSummaryPatient)objHealthCheckUpDeafult).dc.patient_checkup_id;
                Navigation.PopModalAsync(true);

            }
            catch (Exception ex)
            {

                throw;
            }
        }

        protected override void OnDisappearing()
        {
            if (ButtonClicked)
            {
                Navigation.PushModalAsync(new NavigationPage(new AddTest(CheckupId, EditPatientID, true)) { BarBackgroundColor = Color.FromHex("#f44337"), BarTextColor = Color.White });
            }
            ButtonClicked = false;
        }

        private void close_Clicked(object sender, EventArgs e)
        {
            Navigation.PopModalAsync(true);
        }
    }
}