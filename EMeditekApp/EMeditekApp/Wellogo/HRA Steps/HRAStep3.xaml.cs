using System;
using System.Collections.Generic;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using EMeditekApp.Wellogo.Models;

namespace EMeditekApp.Wellogo.HRA_Steps
{
   
    public partial class HRAStep3 : ContentPage
    {
        public HRA objHRA { get; set; }
        public int HRAid { get; set; }
        public FamilyHistory objFamilyHistory { get; set; }
        public List<CommonDropDowns> lstCommonDropDown = new List<Models.CommonDropDowns>();
        CommonDropDowns objCommonDropDowns = new CommonDropDowns(1, "Yes");

        public HRAStep3(HRA HRA, int HRAID)
        {
            InitializeComponent();
            HRAid = HRAID;
            objHRA = HRA;
           
            lstCommonDropDown.Add(new CommonDropDowns(1, "Yes"));
            lstCommonDropDown.Add(new CommonDropDowns(0, "No"));
            lstCommonDropDown.Add(new CommonDropDowns(2, "Don't Know"));
            BindValues();
            if (HRA != null)
            {
                if (HRA.data.family_history != null)
                {
                    objFamilyHistory = HRA.data.family_history;
                    ShowData();
                }
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
        async void BindValues()
        {
            try
            {


                pkrObesity.ItemsSource = lstCommonDropDown;
                pkrhighbloodpressure.ItemsSource = lstCommonDropDown;
                pkrHighCholestrol.ItemsSource = lstCommonDropDown;
                pkrHeartAttack.ItemsSource = lstCommonDropDown;
                pkrDiabetes.ItemsSource = lstCommonDropDown;
                pkrStroke.ItemsSource = lstCommonDropDown;
                pkrCancer.ItemsSource = lstCommonDropDown;




                //pkrObesity.SelectedIndex = 0;
                //pkrhighbloodpressure.SelectedIndex = 0;
                //  pkrHighCholestrol.SelectedIndex = 0;
                //    pkrHeartAttack.SelectedIndex = 0;
                //    pkrDiabetes.SelectedIndex = 0;
                //    pkrStroke.SelectedIndex = 0;
                //    pkrCancer.SelectedIndex = 0;

            }
            catch (Exception ex)
            {

                DependencyService.Get<IMessage>().LongAlert();
            }
             
        }

        async void ShowData()
        {
            try
            {
                pkrObesity.SelectedItem = lstCommonDropDown.Find(x => x.ID == objFamilyHistory.obesity);
                pkrhighbloodpressure.SelectedItem = lstCommonDropDown.Find(x => x.ID == objFamilyHistory.high_blood_pressure);
                pkrHighCholestrol.SelectedItem = lstCommonDropDown.Find(x => x.ID == objFamilyHistory.high_cholesterol);
                pkrHeartAttack.SelectedItem = lstCommonDropDown.Find(x => x.ID == objFamilyHistory.heart_attack);
                pkrDiabetes.SelectedItem = lstCommonDropDown.Find(x => x.ID == objFamilyHistory.diabetes);
                pkrStroke.SelectedItem = lstCommonDropDown.Find(x => x.ID == objFamilyHistory.stroke);
                pkrCancer.SelectedItem = lstCommonDropDown.Find(x => x.ID == objFamilyHistory.cancer);

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



        private void btnCancel_Clicked_1(object sender, EventArgs e)
        {
            App.Current.MainPage = new NavigationPage(new HealthRiskAssesment());
        }

        private async void btnSave_Clicked(object sender, EventArgs e)
        {

            try
            {
                if (Validation())
                {
                    overlay.IsVisible = true;
                    FamilyHistoryInput objFamilyHistoryFamilyHistoryInput = new Models.FamilyHistoryInput();
                    objFamilyHistoryFamilyHistoryInput.obesity = ((CommonDropDowns)pkrObesity.SelectedItem).ID;
                    objFamilyHistoryFamilyHistoryInput.high_blood_pressure = ((CommonDropDowns)pkrhighbloodpressure.SelectedItem).ID;
                    objFamilyHistoryFamilyHistoryInput.high_cholesterol = ((CommonDropDowns)pkrHighCholestrol.SelectedItem).ID;
                    objFamilyHistoryFamilyHistoryInput.heart_attack = ((CommonDropDowns)pkrHeartAttack.SelectedItem).ID;
                    objFamilyHistoryFamilyHistoryInput.diabetes = ((CommonDropDowns)pkrDiabetes.SelectedItem).ID;
                    objFamilyHistoryFamilyHistoryInput.stroke = ((CommonDropDowns)pkrStroke.SelectedItem).ID;
                    objFamilyHistoryFamilyHistoryInput.cancer = ((CommonDropDowns)pkrCancer.SelectedItem).ID;
                    objHRA = await App.TodoManager.SaveHraStep3(objFamilyHistoryFamilyHistoryInput, HRAid);
                    if (objHRA != null)
                    {
                        await Navigation.PushAsync(new HRAStep4(objHRA, HRAid));
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

        bool Validation()
        {
           

            lblObesity.IsVisible = pkrObesity.SelectedItem == null;
            lblBP.IsVisible = pkrhighbloodpressure.SelectedItem == null;
            lblCholestrol.IsVisible = pkrHighCholestrol.SelectedItem == null;
            lblHeartAttack.IsVisible = pkrHeartAttack.SelectedItem == null;
            lbldiebaties.IsVisible = pkrDiabetes.SelectedItem == null;
            lblStroke.IsVisible = pkrStroke.SelectedItem == null;
            lblcancer.IsVisible = pkrCancer.SelectedItem == null;

            return pkrObesity.SelectedItem != null &&
            pkrhighbloodpressure.SelectedItem != null &&
                   pkrHighCholestrol.SelectedItem != null &&
                   pkrHeartAttack.SelectedItem != null &&
                   pkrDiabetes.SelectedItem != null &&
              pkrStroke.SelectedItem != null &&
                   pkrCancer.SelectedItem != null;
        }
    }
}




