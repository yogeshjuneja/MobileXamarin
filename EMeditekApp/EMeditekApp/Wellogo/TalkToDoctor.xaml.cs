using EMeditekApp.Wellogo.Models;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace EMeditekApp.Wellogo
{
   
    public partial class TalkToDoctor : ContentPage
    {
        public TalkToDoctor()
        {
            InitializeComponent();
            try
            {
                //if (Device.RuntimePlatform == "iOS")
                //{
                //    ToolbarItems.Add(new ToolbarItem("Back", "", () => { App.SetupRedirection(new index()); App.Current.MainPage = App.MasterDetailPage; }));
                //}
                //if (Device.RuntimePlatform == "Android")
                //{
                //    ToolbarItems.Add(new ToolbarItem("Home", "back.png", () => { App.SetupRedirection(new index()); App.Current.MainPage = App.MasterDetailPage; }));
                //}
            }
            catch (Exception ex)
            {

               DependencyService.Get<IMessage>().LongAlert();
            }
        }

        private void Button_Clicked(object sender, EventArgs e)
        {

            try
            {
                DependencyService.Get<IOpenApp>().OpenAApplication("com.kyordoctors.patients", "https://itunes.apple.com/st/app/kyornow/id1201925144?mt=8", "https://play.google.com/store/apps/details?id=com.kyordoctors.patients");
            }
            catch (Exception ex)
            {
                DependencyService.Get<IMessage>().LongAlert();
            }
        }
        protected override bool OnBackButtonPressed()
        {
            try
            {
                //App.SetupRedirection(new index());
                //App.Current.MainPage = App.MasterDetailPage;
                
            }
            catch (Exception ex)
            {

               DependencyService.Get<IMessage>().LongAlert();
            }
            return true;
        }
    }
}