using EMeditekApp.Wellogo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace EMeditekApp.Wellogo.HRA_Steps
{
   
    public partial class HRAStep9 : ContentPage
    {
        List<CommonDropDowns> lstCommonDropDown = new List<CommonDropDowns>()
              { new CommonDropDowns(1, "Yes"), new CommonDropDowns(0, "No") };

        public FemaleSpecific objFemaleSpecific { get; set; }
        public HRA objHRA { get; set; }
        public int hraid { get; set; }
        public HRAStep9(HRA HRA,int HRAID)
        {
            InitializeComponent();

            if (Device.RuntimePlatform == "Android")
            {
                ToolbarItems.Add(new ToolbarItem("Home", "crosswhite.png", () => { App.Current.MainPage = new NavigationPage(new HealthRiskAssesment()) { BarBackgroundColor = Color.FromHex("#f44337"), BarTextColor = Color.White }; }));
            }
            else
            {
                ToolbarItems.Add(new ToolbarItem("X", "", () => { App.Current.MainPage = new NavigationPage(new HealthRiskAssesment()) { BarBackgroundColor = Color.FromHex("#f44337"), BarTextColor = Color.White }; }));
            }

            pkrPregnent.ItemsSource = lstCommonDropDown;
            pkrbreastfeed.ItemsSource = lstCommonDropDown;
            pkrmesuration.ItemsSource = lstCommonDropDown;
            pkrmenopause.ItemsSource = lstCommonDropDown;
             
            hraid = HRAID;
            objHRA = HRA;
            if(HRA!=null)
            {
                if(HRA.data.female_specific!=null)
                {
                    objFemaleSpecific = HRA.data.female_specific;
                    ShowData();
                }
            }

        }


        bool Validations()
        {
             lblPregnant.IsVisible=    pkrPregnent.SelectedItem == null;
            lblbreastfeed.IsVisible =  pkrbreastfeed.SelectedItem == null;
            lblmensuration.IsVisible = pkrmesuration.SelectedItem == null;
            lblmenopause.IsVisible =   pkrmenopause.SelectedItem == null && pkrmenopause.IsEnabled;


            return pkrPregnent.SelectedItem != null &&
            pkrbreastfeed.SelectedItem != null &&
              pkrmesuration.SelectedItem != null &&
                (pkrmenopause.SelectedItem != null || !pkrmenopause.IsEnabled) ;
        }         

        void ShowData()
        {
            try
            {
                pkrPregnent.SelectedIndex = objFemaleSpecific.pregnant == 0 ? 1 : 0;
                pkrbreastfeed.SelectedIndex = objFemaleSpecific.breast_feeding == 0 ? 1 : 0;
               pkrmesuration.SelectedIndex = objFemaleSpecific.discomfort_from_menstruation == 0 ? 1 : 0;
                pkrmenopause.SelectedIndex = objFemaleSpecific.menopause == 0 ? 1 : 0;
            }
            catch (Exception ex)
            {

               DependencyService.Get<IMessage>().LongAlert();
            }
        }

        private async void ShowExitDialog()
        {
            var answer = await DisplayAlert("Exit", "Do you wan't to exit the App?", "Yes", "No");
            if (answer)
            {
                //Android.OS.Process.KillProcess(Android.OS.Process.MyPid());
            }
        }

        private void btnCancel_Clicked(object sender, EventArgs e)
        {
            App.Current.MainPage = new NavigationPage(new HealthRiskAssesment()) { BarBackgroundColor = Color.FromHex("#f44337"), BarTextColor = Color.White };
        }

        private async void btnSave_Clicked(object sender, EventArgs e)
        {
            try
            {


                if(Validations())
                {

                    overlay.IsVisible = true;

                    objFemaleSpecific = new Models.FemaleSpecific();
                    objFemaleSpecific.pregnant = ((CommonDropDowns)pkrPregnent.SelectedItem).ID;
                    objFemaleSpecific.breast_feeding = ((CommonDropDowns)pkrbreastfeed.SelectedItem).ID;
                    objFemaleSpecific.discomfort_from_menstruation = ((CommonDropDowns)pkrmesuration.SelectedItem).ID;
                    if (pkrmenopause.IsEnabled)
                    {
                        objFemaleSpecific.menopause = ((CommonDropDowns)pkrmenopause.SelectedItem).ID;
                    }

                    objHRA = await App.TodoManager.SaveHraStep9(objFemaleSpecific, hraid);
                    if (objHRA != null)
                    {
                        App.Current.MainPage = new NavigationPage(new HealthRiskAssesment(0, objHRA.data.id));
                    }

                }
                else
                {
                    DependencyService.Get<IMessage>().LongAlert("Unable to pass validations");
                }

               
            }
            catch (Exception ex)
            {

                DependencyService.Get<IMessage>().LongAlert();
            }
            finally
            {
                overlay.IsVisible = false;
            }
        }

        private void pkrmesuration_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                int selected = ((CommonDropDowns)pkrmesuration.SelectedItem).ID;
                pkrmenopause.IsEnabled = selected == 0;
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}