using EMeditekApp.Wellogo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;


namespace EMeditekApp.Wellogo.Tabbedpage
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TabbedPage1 : Xamarin.Forms.TabbedPage
    {
       
        public TabbedPage1(string type="",string code="")
        {
            InitializeComponent();

            this.CurrentPageChanged += (object sender, EventArgs e) => {
                var i = this.Children.IndexOf(this.CurrentPage);
                if (i == 0)
                {
                    this.Title = "Home";
                }
                else if(i==1)
                {
                    this.Title = "Dashboard";
                }
                else if(i==2)
                {
                    this.Title = "Leaderboard";
                }
            };
            if(Device.RuntimePlatform.ToLower()=="ios")
            {
                if(type!="" && code!="")
                 {
                    Navigation.PushAsync(new FitnessTracker(type, code));
                }
            }
        }
        protected override bool OnBackButtonPressed()
        {
            try
            {
                ShowExitDialog();
                return true;
            }
            catch (Exception ex)
            {
                DependencyService.Get<IMessage>().LongAlert();
            }
            return true;
        }
        private async void ShowExitDialog()
        {
            try
            {
                var answer = await DisplayAlert("Exit", "Do you wan't to exit the App?", "Yes", "No");
                if (answer)
                {
                    DependencyService.Get<IExitApp>().Exit();
                    // Android.OS.Process.KillProcess(Android.OS.Process.MyPid());
                }
            }
            catch (Exception ex)
            {
            }
        }
        private void MenuItem1_Clicked(object sender, EventArgs e)
        {
            try
            {
                Navigation.PushModalAsync( new NavigationPage(new Notification()) {  BarBackgroundColor = Color.FromHex("#f44337"), BarTextColor = Color.White, Title ="Notification"});
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}