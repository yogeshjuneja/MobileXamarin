using EMeditekApp.Wellogo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace EMeditekApp.Wellogo
{
   
    public partial class ChangePassword : ContentPage
    {
        public ChangePassword()
        {

            InitializeComponent();
            if (Device.RuntimePlatform == "iOS")
            {
                ToolbarItems.Add(new ToolbarItem("Back", "", () => { App.SetupRedirection(new Wellogo.Tabbedpage.TabbedPage1()); App.Current.MainPage = App.MasterDetailPage; }));
            }
            if (Device.RuntimePlatform == "Android")
            {
        
                ToolbarItems.Add(new ToolbarItem("Home", "back.png", () => { App.SetupRedirection(new Wellogo.Tabbedpage.TabbedPage1()); App.Current.MainPage = App.MasterDetailPage; }));
            }
            
        }

        protected override bool OnBackButtonPressed()
        {
            try
            {
                App.SetupRedirection(new Wellogo.Tabbedpage.TabbedPage1());
                App.Current.MainPage = App.MasterDetailPage;

            }
            catch (Exception ex)
            {

                DependencyService.Get<IMessage>().LongAlert();
            }
            return true;
        }

        private async void btnSave_Clicked(object sender, EventArgs e)
        {
            try
            {
                lblError.Text = "";
                if (!string.IsNullOrEmpty(txtOldPassword.Text) && !string.IsNullOrEmpty(txtNewPassword.Text) && !string.IsNullOrEmpty(txtReNewPassword.Text))
                {

                    UserPassword UserPassword = new UserPassword();
                    UserPassword.old_password = txtOldPassword.Text.Trim();
                    UserPassword.password = txtNewPassword.Text.Trim();
                    UserPassword.password_confirmation = txtReNewPassword.Text.Trim();
                    object Response = await App.TodoManager.ChangePassword(UserPassword);

                    if (Response is PasswordRootResponse)
                    {
                        txtNewPassword.Text = "";
                        txtOldPassword.Text = "";
                        txtReNewPassword.Text = "";
                        lblError.TextColor = Color.Green;
                        lblError.Text= ((PasswordRootResponse)Response).data.message;
                        DependencyService.Get<IMessage>().LongAlert(((PasswordRootResponse)Response).data.message);
                    }
                    else
                    {
                       
                        string ErrorString = "";
                        PasswordError objPasswordError = (PasswordError)Response; 
                       foreach (var v in objPasswordError.errors.validation)
                        {
                            ErrorString +=  string.Join("\n",v.Value);
                        }
                        lblError.TextColor = Color.Red;
                        lblError.Text = ErrorString;
                    }
                }
                else
                {
                    DependencyService.Get<IMessage>().LongAlert("All Fields are required");
                }

            }
            catch (Exception ex)
            {

                DependencyService.Get<IMessage>().LongAlert();
            }
        }
    }
}