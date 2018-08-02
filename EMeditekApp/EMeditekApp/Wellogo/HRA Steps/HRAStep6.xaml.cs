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
   
    public partial class HRAStep6 : ContentPage
    {
        List<CommonDropDowns> lstCommonDropDown = new List<CommonDropDowns>()
        { new CommonDropDowns(1, "Yes"), new CommonDropDowns(0, "No"), new CommonDropDowns(2, "Sometimes") };

        public Fitness objFitness { get; set; }
        public HRA objHRA { get; set; }
        public int hraid { get; set; }
        public HRAStep6(HRA HRA, int HRAID)
        {
            InitializeComponent();
          
            pkrExercise.ItemsSource = lstCommonDropDown;
         
            hraid = HRAID;
            objHRA = HRA;
            if(HRA!=null)
            {
                if(HRA.data.fitness!=null)
                {

                    objFitness = HRA.data.fitness;
                    ShowData();
                }
            }


        }
        async void ShowData()
        {
            try
            {
                overlay.IsVisible = true;
                pkrExercise.SelectedItem = lstCommonDropDown.Find(x => x.ID == objFitness.exercise);
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
  

        private void btnCancel_Clicked(object sender, EventArgs e)
        {
            App.Current.MainPage = new NavigationPage(new HealthRiskAssesment()) { BarBackgroundColor = Color.FromHex("#f44337"), BarTextColor = Color.White };
        }

        private async void btnSave_Clicked(object sender, EventArgs e)
        {
            try
            {
                if (pkrExercise.SelectedItem != null)
                {

                    overlay.IsVisible = true;
                    FitnessInput objFitness = new Models.FitnessInput();
                    objFitness.exercise = ((CommonDropDowns)pkrExercise.SelectedItem).ID;
                    objHRA = await App.TodoManager.SaveHraStep6(objFitness, hraid);
                    if (objHRA != null)
                    {
                        await Navigation.PushAsync(new HRAStep7(objHRA, hraid));
                    }
                }
                else
                {
                    lblExercise.IsVisible = true;
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