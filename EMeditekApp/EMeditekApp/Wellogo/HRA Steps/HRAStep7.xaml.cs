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
   
    public partial class HRAStep7 : ContentPage
    {
        List<CommonDropDowns> lstCommonDropDown = new List<CommonDropDowns>()
              { new CommonDropDowns(1, "Yes"), new CommonDropDowns(0, "No"), new CommonDropDowns(2, "Sometimes") };
        public HRA objHRA { get; set; }
        public Stress objStress { get; set; }
        public int Hraid { get; set; }
        public HRAStep7(HRA HRA,int HRAID)
        {
            InitializeComponent();

          
            pkrlesstime.ItemsSource = lstCommonDropDown;
            pkrSatisfy.ItemsSource = lstCommonDropDown;
            Hraid = HRAID;
            objHRA = HRA;
            if(HRA!=null)
            {
                if(HRA.data.stress!=null)
                {
                    objStress = HRA.data.stress;
                    ShowData();
                }
            }
                
        }
         void ShowData()
        {
            pkrSatisfy.SelectedItem = lstCommonDropDown.Find(x => x.ID == objStress.satisfied_with_work);
            pkrlesstime.SelectedItem=lstCommonDropDown.Find(x => x.ID == objStress.less_time_for_family);
        }
       

        bool Validations()
        {
            lbllesstime.IsVisible = pkrlesstime.SelectedItem == null;
            lblStisfy.IsVisible = pkrSatisfy.SelectedItem == null;
            return pkrlesstime.SelectedItem != null && pkrSatisfy.SelectedItem != null;
        }


        private async void ShowExitDialog()
        {
            var answer = await DisplayAlert("Exit", "Do you wan't to exit the App?", "Yes", "No");
            if (answer)
            {
              //  Android.OS.Process.KillProcess(Android.OS.Process.MyPid());
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

                if (Validations())
                {
                    StressInput objStress = new Models.StressInput();
                    objStress.satisfied_with_work = ((CommonDropDowns)pkrSatisfy.SelectedItem).ID;
                    objStress.less_time_for_family = ((CommonDropDowns)pkrlesstime.SelectedItem).ID;
                    objHRA = await App.TodoManager.SaveHraStep7(objStress, Hraid);
                    if (objHRA != null)
                    {
                        await Navigation.PushAsync(new HRAStep8(objHRA, Hraid));
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
    }
}