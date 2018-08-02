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
   
    public partial class HRAStep8 : ContentPage
    {
        List<CommonDropDowns> lstCommonDropDown = new List<CommonDropDowns>()
              { new CommonDropDowns(1, "Daily"), new CommonDropDowns(0, "Never"), new CommonDropDowns(2, "Sometimes") };
        public HRA objHra { get; set; }
        public Habits objHabits { get; set; }
        public int hraid { get; set; }

        public HRAStep8(HRA HRA, int HRAID)
        {
            InitializeComponent();
         
            pkrChew.ItemsSource = lstCommonDropDown;
            pkrSmoke.ItemsSource = lstCommonDropDown;
            pkealcohol.ItemsSource = lstCommonDropDown;


            hraid = HRAID;
            objHra = HRA;
            if (HRA != null)
            {
                if (HRA.data.habits != null)
                {
                    objHabits = HRA.data.habits;
                    ShowData();
                }
            }
        }


        void ShowData()
        {
            try
            {
                pkrSmoke.SelectedItem = lstCommonDropDown.Find(x => x.ID == objHabits.smoke);
                pkealcohol.SelectedItem = lstCommonDropDown.Find(x => x.ID == objHabits.alchohol);
                pkrChew.SelectedItem = lstCommonDropDown.Find(x => x.ID == objHabits.tobacco);
            }
            catch (Exception ex)
            {
                DependencyService.Get<IMessage>().LongAlert();
            }
        }
     
        bool validations()
        {
            lblSmoke.IsVisible = pkrSmoke.SelectedItem == null;
            lblChew.IsVisible = pkrChew.SelectedItem == null;
            lblAlcohol.IsVisible = pkealcohol.SelectedItem == null;
            return pkrSmoke.SelectedItem != null &&
                 pkrChew.SelectedItem != null &&
                    pkealcohol.SelectedItem != null;
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
                if (validations())
                {

                    HabitsInput objHabits = new Models.HabitsInput();
                    objHabits.smoke = ((CommonDropDowns)pkrSmoke.SelectedItem).ID;
                    objHabits.tobacco = ((CommonDropDowns)pkrChew.SelectedItem).ID;
                    objHabits.alchohol = ((CommonDropDowns)pkealcohol.SelectedItem).ID;

                    objHra = await App.TodoManager.SaveHraStep8(objHabits, hraid);
                    if (objHra != null)
                    {
                        if (objHra.data.client.gender.ToString().ToLower() == "female")
                        {
                            await Navigation.PushAsync(new HRAStep9(objHra, hraid));
                        }
                        else
                        {
                            App.Current.MainPage = new NavigationPage(new HealthRiskAssesment(0, objHra.data.id));

                        }
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