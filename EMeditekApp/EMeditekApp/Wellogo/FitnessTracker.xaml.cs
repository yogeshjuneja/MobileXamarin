using EMeditekApp.Wellogo.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;


namespace EMeditekApp.Wellogo
{
    public partial class FitnessTracker : ContentPage
    {

        public static string integrationType { get; set; }

        public static string Code { get; set; }

        public static bool stack;
      
        //   WebView simpleBrowser;
        public FitnessTracker(string IntegrationType = null, string code = null)
        {
           //stkMainStack.IsVisible = stack;
           // stkIntegratedStack.IsVisible = stack;
            InitializeComponent();
           
            //if (Device.RuntimePlatform == "iOS")
            //{
            // //   ToolbarItems.Add(new ToolbarItem("Back", "", () => { App.SetupRedirection(new index()); App.Current.MainPage = App.MasterDetailPage; }));
            //}
            //if (Device.RuntimePlatform == "Android")
            //{
            //    //ToolbarItems.Add(new ToolbarItem("Home", "back.png", () => { App.SetupRedirection(new index()); App.Current.MainPage = App.MasterDetailPage; }));
            //}
            if (IntegrationType != null && code != null)
            {
                //    integrationType = IntegrationType;
                //    Code = code;
                //if (Device.RuntimePlatform.ToLower() == "android")
                //{
                //    GenerateAccessToken(IntegrationType, code);
                //}
                GenerateAccessToken(IntegrationType, code);

                DependencyService.Get<IMessage>().LongAlert("");

            }
            else
            {
                CheckIntegrateTracker();
            }

             if(Device.RuntimePlatform == "iOS")
            {
                MessagingCenter.Subscribe<App, List<string>>((App)Xamarin.Forms.Application.Current, "fitnessintegratedios", (app, message) =>
                {
                    if(message.Count==2)
                    GenerateAccessToken(message[0].ToString(), code[0].ToString());

                });
            }

             

        }

        protected override void OnAppearing()
        {
         //   GenerateAccessToken(integrationType, Code);
            base.OnAppearing();
        }
        async void CheckIntegrateTracker()
        {
            try
            {
                overlay.IsVisible = true;
                Tracker objTracker = await App.TodoManager.GetClientTracker();
                if (objTracker != null)
                {
                    if (objTracker.data == null)
                    {
                        stkIntegratedStack.IsVisible = false;
                        stkMainStack.IsVisible = true;
                        return;
                    }
                    if (objTracker.data.type == 1)
                    {
                        stkMainStack.IsVisible = false;
                        stkIntegratedStack.IsVisible = true;
                        lblMessage.Text = "Fit Bit Integrated";
                    
                    }
                    else
                    {
                        stkMainStack.IsVisible = false;
                        stkIntegratedStack.IsVisible = true;
                        lblMessage.Text = "Google Fit";
                    }
                }
            }
            catch (Exception ex)
            {

                throw;
            }
            finally
            {
                overlay.IsVisible = false;
            }
        }
         async void GenerateAccessToken(string IntegrationType, string code)
        {
            try
            {
                overlay.IsVisible = true;
                TrackerAcessOutput objUserDataProfile = await App.TodoManager.GenerateAccessTokenViaAcessCode(IntegrationType, code);
                await Task.Delay(1000);
                if (objUserDataProfile != null)
                {
                    if (objUserDataProfile.data != null)
                    {
                        overlay.IsVisible = true;
                        Tracker objTracker = await App.TodoManager.GetClientTracker();
                        if (objTracker != null)
                        {
                            if (objTracker.data == null)
                            {
                                return;
                            }
                            if (objTracker.data.type == 1)
                            {
                                stkMainStack.IsVisible = false;
                                stkIntegratedStack.IsVisible = true;
                                lblMessage.Text = "Fit Bit Integrated";
                               
                            }
                            else if (objTracker.data.type == 2)
                            {
                                stkMainStack.IsVisible = false;
                                stkIntegratedStack.IsVisible = true;
                                lblMessage.Text = "Google Fit";
                            }

                            DependencyService.Get<IMessage>().LongAlert("Fitness Tracker integrated successfully");
                            if(Device.RuntimePlatform.ToLower()=="ios")
                            {
                                CheckIntegrateTracker();
                            }
                        }
                    }
                    else
                    {
                        stkMainStack.IsVisible = true;
                        stkIntegratedStack.IsVisible = false;
                        DependencyService.Get<IMessage>().LongAlert("Unable to add fitness Tracker ");
                    }

                }
                else
                {
                    stkMainStack.IsVisible = true;
                    stkIntegratedStack.IsVisible = false;
                    DependencyService.Get<IMessage>().LongAlert("Unable to add fitness Tracker ");

                }

            }
            catch (Exception ex)
            {

                DependencyService.Get<IMessage>().LongAlert(ex.ToString());
            }
            finally
            {
                overlay.IsVisible = false;
            }
        }
        
        private void TapGestureRecognizer_TappedGoogleFit(object sender, EventArgs e)
        {
            try
            {
                DependencyService.Get<IOAuth>().GetAuthoriseToken(2);
            }
            catch (Exception ex)
            {
                DependencyService.Get<IMessage>().LongAlert();
            }
        }
        private void SimpleBrowser_Navigated(object sender, WebNavigatedEventArgs e)
        {
            Debug.WriteLine("URL received after navigated: {0}", e.Url);

        }
        private void TapGestureRecognizer_TappedFitBit(object sender, EventArgs e)
        {
            DependencyService.Get<IOAuth>().GetAuthoriseToken(1);
        }
        private async void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            try
            {
                overlay.IsVisible = true;
                Tracker objTracker = await App.TodoManager.DeleteTrackerData();
                if (objTracker != null)
                {
                    if (objTracker.data == null)
                    {
                        return;
                    }
                    else
                    {
                        stkMainStack.IsVisible = true;
                        stkIntegratedStack.IsVisible = false;
                        DependencyService.Get<IMessage>().LongAlert("Tracker Unpaired Successfully");
                    }
                }
            }
            catch (Exception ex)
            {

                throw;
            }
            finally
            {
                overlay.IsVisible = false;
            }
        }
    }
}