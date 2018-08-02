using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using EMeditekApp.Wellogo.Models;

namespace EMeditekApp.Wellogo.HRA_Steps
{
   
    public partial class HRAStep4 : ContentPage
    {
        List<CommonDropDowns> lstCommonDropDown = new List<CommonDropDowns>() { new CommonDropDowns(1, "Yes"), new CommonDropDowns(0, "No") };
        public HRA objHra { get; set; }
        public int Hraid { get; set; }

        public PersonalHealth objPersonalHealth { get; set; }


        public HRAStep4(HRA HRA, int HRAID)
        {
            InitializeComponent();


            pkrhighbloodpressure.ItemsSource = lstCommonDropDown;
            pkrHighCholestrol.ItemsSource = lstCommonDropDown;
            pkrDiabetes.ItemsSource = lstCommonDropDown;
            pkrAnaemia.ItemsSource = lstCommonDropDown;

            pkrdigestiveProblems.ItemsSource = lstCommonDropDown;
            pkrFrequentAllergies.ItemsSource = lstCommonDropDown;
            pkPainJoints.ItemsSource = lstCommonDropDown;
            pkFrequentHeadaches.ItemsSource = lstCommonDropDown;
            pkSleepDisorders.ItemsSource = lstCommonDropDown;
            pkSkinProblems.ItemsSource = lstCommonDropDown;
            pkHeartProblems.ItemsSource = lstCommonDropDown;
             

            //pkrhighbloodpressure.SelectedIndex = 0;
            //pkrHighCholestrol.SelectedIndex = 0;
            //pkrDiabetes.SelectedIndex = 0;
            //pkrAnaemia.SelectedIndex = 0;

            //pkrdigestiveProblems.SelectedIndex = 0;
            //pkrFrequentAllergies.SelectedIndex = 0;
            //pkPainJoints.SelectedIndex = 0;
            //pkFrequentHeadaches.SelectedIndex = 0;
            //pkSleepDisorders.SelectedIndex = 0;
            //pkSkinProblems.SelectedIndex = 0;
            //pkHeartProblems.SelectedIndex = 0;
            Hraid = HRAID;
            if (HRA != null)
            {
                objHra = HRA;
                if (HRA.data.personal_health != null)
                {
                    objPersonalHealth = HRA.data.personal_health;
                    showData();
                }
            }
        }

       

        void showData()
        {
            pkrhighbloodpressure.SelectedIndex = objPersonalHealth.high_blood_pressure == 1 ? 0 : 1;
            pkrHighCholestrol.SelectedIndex = objPersonalHealth.high_cholesterol == 1 ? 0 : 1;
            pkrDiabetes.SelectedIndex = objPersonalHealth.diabetes == 1 ? 0 : 1;
            pkrAnaemia.SelectedIndex = objPersonalHealth.anaemia == 1 ? 0 : 1;
            pkrdigestiveProblems.SelectedIndex = objPersonalHealth.digestive_problems == 1 ? 0 : 1;
            pkrFrequentAllergies.SelectedIndex = objPersonalHealth.allergic_problems == 1 ? 0 : 1;
            pkPainJoints.SelectedIndex = objPersonalHealth.pain_in_joints == 1 ? 0 : 1;
            pkFrequentHeadaches.SelectedIndex = objPersonalHealth.headaches == 1 ? 0 : 1;
            pkSleepDisorders.SelectedIndex = objPersonalHealth.sleep_disorders == 1 ? 0 : 1;
            pkSkinProblems.SelectedIndex = objPersonalHealth.skin_problems == 1 ? 0 : 1;
            pkHeartProblems.SelectedIndex = objPersonalHealth.heart_problems == 1 ? 0 : 1;
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
                if (Validation())
                {
                    overlay.IsVisible = true;
                    PersonalHealthInput objPersonalHealthInput = new PersonalHealthInput();
                    objPersonalHealthInput.high_blood_pressure = ((CommonDropDowns)pkrhighbloodpressure.SelectedItem).ID;
                    objPersonalHealthInput.high_cholesterol = ((CommonDropDowns)pkrHighCholestrol.SelectedItem).ID;
                    objPersonalHealthInput.diabetes = ((CommonDropDowns)pkrDiabetes.SelectedItem).ID;
                    objPersonalHealthInput.anaemia = ((CommonDropDowns)pkrAnaemia.SelectedItem).ID;
                    objPersonalHealthInput.digestive_problems = ((CommonDropDowns)pkrdigestiveProblems.SelectedItem).ID;
                    objPersonalHealthInput.allergic_problems = ((CommonDropDowns)pkrFrequentAllergies.SelectedItem).ID;
                    objPersonalHealthInput.pain_in_joints = ((CommonDropDowns)pkPainJoints.SelectedItem).ID;
                    objPersonalHealthInput.headaches = ((CommonDropDowns)pkFrequentHeadaches.SelectedItem).ID;
                    objPersonalHealthInput.sleep_disorders = ((CommonDropDowns)pkSleepDisorders.SelectedItem).ID;
                    objPersonalHealthInput.skin_problems = ((CommonDropDowns)pkSkinProblems.SelectedItem).ID;
                    objPersonalHealthInput.heart_problems = ((CommonDropDowns)pkHeartProblems.SelectedItem).ID;
                    objHra = await App.TodoManager.SaveHraStep4(objPersonalHealthInput, Hraid);
                    if (objHra != null)
                    {
                        await Navigation.PushAsync(new HRAStep5(objHra, Hraid));
                    }
                }
                else
                {
                    overlay.IsVisible = false;
                    DependencyService.Get<IMessage>().LongAlert("Unable to pass Validation");
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


        bool Validation()
        {


            lblBP.IsVisible = pkrhighbloodpressure.SelectedItem == null;
            lblCholestrol.IsVisible = pkrHighCholestrol.SelectedItem == null;
            lblDiabetes.IsVisible = pkrDiabetes.SelectedItem == null;
            lblAnaemia.IsVisible = pkrAnaemia.SelectedItem == null;
            lblDigestive.IsVisible = pkrdigestiveProblems.SelectedItem == null;
            lblFrequentAllergies.IsVisible = pkrFrequentAllergies.SelectedItem == null;
            lblPaininJoints.IsVisible = pkPainJoints.SelectedItem == null;
            lblFrequentHeadaches.IsVisible = pkFrequentHeadaches.SelectedItem == null;
            lblSleepDisorders.IsVisible = pkSleepDisorders.SelectedItem == null;
            lblSkinProblems.IsVisible = pkSkinProblems.SelectedItem == null;
            lblHeartProblems.IsVisible = pkHeartProblems.SelectedItem == null;



            return pkrhighbloodpressure.SelectedItem != null &&
              pkrHighCholestrol.SelectedItem != null &&
             pkrDiabetes.SelectedItem != null &&
             pkrAnaemia.SelectedItem != null &&
             pkrdigestiveProblems.SelectedItem != null &&
             pkrFrequentAllergies.SelectedItem != null &&
             pkPainJoints.SelectedItem != null &&
            pkFrequentHeadaches.SelectedItem != null &&
             pkSleepDisorders.SelectedItem != null &&
             pkSkinProblems.SelectedItem != null &&
             pkHeartProblems.SelectedItem != null;



        }
    }
}






