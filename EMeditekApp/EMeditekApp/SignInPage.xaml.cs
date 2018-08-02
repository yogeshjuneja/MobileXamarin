using EMeditekApp.Models;
using EMeditekApp.Wellogo.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Plugin.Connectivity;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Xamarin.Forms;
namespace EMeditekApp
{
    public partial class SignInPage : ContentPage
    {
        public SignInPage()
        {
            InitializeComponent();

            if (Device.RuntimePlatform == "iOS")
            {
                mainbackgroundimage.Source = "Default.png";
            }
            else if (Device.RuntimePlatform == "Android")
            {
                mainbackgroundimage.Source = "whitebglogo.png";
            }
        }
        //protected override void OnAppearing()
        //{
        //}
        #region "SqlLite"

        #endregion
        public async void OnLoginClicked(object o, EventArgs e)
        {
            try
            {

                overlay.IsVisible = true;
                if (((txtUserId.Text.Trim()) == "") && ((txtPassword.Text.Trim()) == ""))
                {
                    await DisplayAlert("Members", "Please select login type and  enter user id and password", "Cancel");
                    txtUserId.Focus();
                }
                else if ((string.IsNullOrEmpty(txtUserId.Text)))
                {
                    await DisplayAlert("Members", "Please enter user id", "Cancel");
                    txtUserId.Focus();
                }
                else if ((string.IsNullOrEmpty(txtPassword.Text)))
                {
                    await DisplayAlert("Members", "Please enter password", "Cancel");
                    txtPassword.Focus();
                }
                //else if(!Regex.IsMatch(txtUserId.Text, @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z", RegexOptions.IgnoreCase))
                //{
                //    DependencyService.Get<IMessage>().LongAlert("Invalid Email");
                //}
                else
                {
                    //switch (Device.RuntimePlatform)
                    //{
                    //    case Device.iOS:
                    //        objLoginModel.DeviceType = "IOS";
                    //        break;
                    //    case Device.Android:
                    //        objLoginModel.DeviceType = "Android";
                    //        break;
                    //}
                    #region KyorLogin
                    LoggedInUser objLogedInUser = new LoggedInUser();
                    KyorLogin objKyorLogin = new KyorLogin();
                    objKyorLogin.email = txtUserId.Text.Trim();
                    objKyorLogin.password = txtPassword.Text.Trim();
                    object response = await App.TodoManager.LoginForKyor(objKyorLogin);
                    if (response.ToString().Contains("errors"))
                    {
                        LoginResponseError objErrorResponse = Newtonsoft.Json.JsonConvert.DeserializeObject<LoginResponseError>(response.ToString());
                        DependencyService.Get<IMessage>().LongAlert(objErrorResponse.errors.message);
                    }
                    else
                    {
                        KyorLoginResponse objKyorLoginData = JsonConvert.DeserializeObject<KyorLoginResponse>(response.ToString());
                        if (objKyorLoginData.data != null)
                        {

                            objLogedInUser.sso_token = objKyorLoginData.data.sso_token;
                            objLogedInUser.id = objKyorLoginData.data.id;
                            objLogedInUser.Name = objKyorLoginData.data.name;
                            objLogedInUser.photo_url = objKyorLoginData.data.photo_url;
                            objLogedInUser.Designation = objKyorLoginData.data.designation.ToString();
                            objLogedInUser.CorporateLogo = objKyorLoginData.data.branch.corporate.logo;
                            objLogedInUser.Gender = objKyorLoginData.data.gender;
                            objLogedInUser.CorporateID = objKyorLoginData.data.corporate_id;
                            List<KyorLoginService> lstKyorLoginService = new List<KyorLoginService>();
                            lstKyorLoginService = objKyorLoginData.data.branch.corporate.services;
                            //  objLogedInUser.MaritalStatus = objKyorLoginData.data.marital_status;
                            App.KyorData = objKyorLoginData.data;
                            App.id = objLogedInUser.id;
                            App.Authorization = objKyorLoginData.data.sso_token;
                            App.CorporateImageURl = objKyorLoginData.data.branch.corporate.logo;
                            App.Gender = objKyorLoginData.data.gender;
                          
                            List<UserMenu> objUserMenu = new List<UserMenu>();
                            var menu = new UserMenu();
                            foreach (KyorLoginService kls in lstKyorLoginService)
                            {
                                menu.id = kls.id;
                                menu.name = kls.name;
                                menu.sequence = kls.sequence;
                                objUserMenu.Add(menu);
                                menu = new UserMenu();
                            }
                            App.Database.SaveItem(objLogedInUser);
                            App.Database.SaveUserMenu(objUserMenu);
                            App.UserMenu = objUserMenu;
                            App.SetupRedirection(new Wellogo.Tabbedpage.TabbedPage1());
                            App.Current.MainPage = App.MasterDetailPage;
                        }
                    }


                    #endregion

                    //objLoginResult.

                }
            }
            catch (Exception ex)
            {
                //    await DisplayAlert("Alert", ex.ToString() + " or check your network connectivity.", "", "Re-Try");
                await DisplayAlert("Alert", ex.ToString(), "", "Re-Try");
            }
            finally
            {
                overlay.IsVisible = false;
            }
        }
        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            //  Navigation.PushModalAsync(new RegisterHere());
        }
        private void forgotpassword_Tapped(object sender, EventArgs e)
        {
            //  Navigation.PushModalAsync(new ForgotPassword());
        }

    }
}
