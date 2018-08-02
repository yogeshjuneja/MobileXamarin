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
   
    public partial class HRAStep5 : ContentPage
    {
        List<CommonDropDowns> lstCommonDropDown = new List<CommonDropDowns>()
        { new CommonDropDowns(1, "Yes"), new CommonDropDowns(0, "No"), new CommonDropDowns(2, "Sometimes") };
        public HRA objHRA { get; set; }
        public int Hraid { get; set; }
        public Nutrition objNutrition { get; set; }
        public HRAStep5(HRA HRA, int HRAID)
        {
            InitializeComponent();

           
            pkrmealstimely.ItemsSource = lstCommonDropDown;
            pkrmealsfrequently.ItemsSource = lstCommonDropDown;
            pkrHeavyDinner.ItemsSource = lstCommonDropDown;

 
            objHRA = HRA;
            Hraid = HRAID;
            if (HRA != null)
            {
              
                if (HRA.data.nutrition != null)
                {
                    objNutrition = HRA.data.nutrition;
                    ShowData();
                }
            }
             
        }

        async void ShowData()
        {
            try
            {
                pkrmealstimely.SelectedItem = lstCommonDropDown.Find(x=>x.ID==objNutrition.take_meals_on_time);
                pkrmealsfrequently.SelectedItem = lstCommonDropDown.Find(x => x.ID == objNutrition.skip_meals); 
                pkrHeavyDinner.SelectedItem = lstCommonDropDown.Find(x => x.ID == objNutrition.heavy_dinner); 
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


        bool Validations()
        {
            lblTimelyMeals.IsVisible = pkrmealstimely.SelectedItem == null;
            lblMeals.IsVisible = pkrmealsfrequently.SelectedItem == null;
            lblHeavyDinner.IsVisible = pkrHeavyDinner.SelectedItem == null;


            return pkrmealstimely.SelectedItem != null &&
              pkrmealsfrequently.SelectedItem != null &&
         pkrHeavyDinner.SelectedItem != null;
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
                    overlay.IsVisible = true;
                    NuritionInput objNutritionNuritionInput = new Models.NuritionInput();
                    objNutritionNuritionInput.take_meals_on_time = ((CommonDropDowns)pkrmealstimely.SelectedItem).ID;
                    objNutritionNuritionInput.skip_meals = ((CommonDropDowns)pkrmealsfrequently.SelectedItem).ID;
                    objNutritionNuritionInput.heavy_dinner = ((CommonDropDowns)pkrHeavyDinner.SelectedItem).ID;
                    objHRA = await App.TodoManager.SaveHraStep5(objNutritionNuritionInput, Hraid);
                    if (objHRA != null)
                    {
                        await Navigation.PushAsync(new HRAStep6(objHRA, Hraid));
                    }
                }
                else
                {
                    DependencyService.Get<IMessage>().LongAlert("Unable to pass Validations");
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

      


        private async void ShowExitDialog()
        {
            var answer = await DisplayAlert("Exit", "Do you wan't to exit the App?", "Yes", "No");
            if (answer)
            {
                //Android.OS.Process.KillProcess(Android.OS.Process.MyPid());
            }
        }
    }
}