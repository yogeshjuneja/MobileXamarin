using EMeditekApp.Wellogo.Models;
using Plugin.Connectivity;
using System;
using System.Text.RegularExpressions;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace EMeditekApp.Wellogo
{

    public partial class AddBloodDoner : ContentPage
    {
        CommonDropDowns objCommonDropDowns = new Models.CommonDropDowns();
        public AddBloodDoner()
        {
            try
            {
                InitializeComponent();
                pkrGender.ItemsSource = objCommonDropDowns.Gender();
                if (Device.RuntimePlatform == "Android")
                {
                   // ToolbarItems.Add(new ToolbarItem("Back", "down.png", () => { Navigation.PopModalAsync(true); }));
                }
                else
                {
                   // ToolbarItems.Add(new ToolbarItem("Back", "", () => { Navigation.PopModalAsync(true); }));
                }
                dpPrefferedDate.MinimumDate = DateTime.Now;
                dpPrefferedDate.MaximumDate = DateTime.Now.AddMonths(1);
            }

            catch (Exception ex)
            {
                DependencyService.Get<IMessage>().LongAlert();
            }

        }

        private async void btnSave_Clicked(object sender, EventArgs e)
        {
            try
            {
                if (CrossConnectivity.Current.IsConnected)
                {
                    if (Validations())
                    {
                        overlay.IsVisible = true;
                        objCommonDropDowns = pkrGender.SelectedItem as CommonDropDowns;
                        BloodBankDoner objBloodBankDoner = new Models.BloodBankDoner();
                        BloodBankDonerRootResponse objBloodDonerResponse = new Models.BloodBankDonerRootResponse();
                        objBloodBankDoner.full_name = txtFullName.Text.Trim();
                        objBloodBankDoner.email = txtEmail.Text.Trim();
                        objBloodBankDoner.age = txtAge.Text.Trim();
                        objBloodBankDoner.phone = txtPhone.Text.Trim();
                        objBloodBankDoner.address = txtAddress.Text.Trim();
                        objBloodBankDoner.gender = objCommonDropDowns.Name;
                        objBloodBankDoner.preferred_date = Convert.ToDateTime(dpPrefferedDate.Date.ToString()).ToString("yyyy-MM-dd");
                        objBloodBankDoner.comments = txtComment.Text.Trim();
                        Object obj = await App.TodoManager.AddDoner(objBloodBankDoner);
                        if (obj.ToString().Contains("error"))
                        {
                            ErrorResponse objErrorResponse = Newtonsoft.Json.JsonConvert.DeserializeObject<ErrorResponse>(obj.ToString());
                            DependencyService.Get<IMessage>().LongAlert(objErrorResponse.message);

                            errstack.Children.Clear();
                            foreach (string s in objErrorResponse.data)
                            {
                                errstack.Children.Add(new Label() { Text = s, TextColor = Color.Red });
                            }

                            errstack.Focus();
                        }
                        else
                        {
                            objBloodDonerResponse = Newtonsoft.Json.JsonConvert.DeserializeObject<BloodBankDonerRootResponse>(obj.ToString());
                            if (objBloodDonerResponse.status.ToLower() == "success")
                            {
                                await DisplayAlert("Thank You", "Your Request for becoming a blood donar has been registered . We will contact you back within a few Days", "Dismiss");
                                await Navigation.PopModalAsync(true);
                            }
                            else
                            {
                                await DisplayAlert("Request Not Completed", "Unable to process  request please try again later", "Cancel");
                            }
                        }
                        overlay.IsVisible = false;
                    }
                }
                else
                {
                    var Retry = await DisplayAlert("No Connection", "Please Check Your Connection", "Retry", "Cancel");
                    if (Retry)
                    {
                        btnSave_Clicked(null, null);
                    }
                }
            }
            catch (Exception ex)
            {
                overlay.IsVisible = false;
                DependencyService.Get<IMessage>().LongAlert();
            }
        }

        bool Validations()
        {
            bool valid = true;


            if (string.IsNullOrEmpty(txtFullName.Text))
            {
                valid = false;
                lblName.Text = "Enter Full Name";
            }
            else if (!Regex.IsMatch(txtFullName.Text, "^([a-z]+[,.]?[ ]?|[a-z]+['-]?)+$"))
            {
                valid = false;
                lblName.Text = "Invalid Name";
            }
            else
            {
                lblName.Text = "";
            }
            if (string.IsNullOrEmpty(txtEmail.Text))
            {
                valid = false;
                lblEmail.Text = "Enter Email";
            }
            else
            {
                if (!Regex.IsMatch(txtEmail.Text, @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z", RegexOptions.IgnoreCase))
                {
                    valid = false;
                    lblEmail.Text = "Enter Valid Email";
                }
                else
                {
                    lblEmail.Text = "";
                }
            }

            if (string.IsNullOrEmpty(txtAge.Text))
            {
                valid = false;
                lblAge.Text = "Enter Age";
            }
            else
            {
                if (Convert.ToInt32(txtAge.Text) == 0 || Convert.ToInt32(txtAge.Text) <18)
                {
                    valid = false;
                    lblAge.Text = "Age Should between 18 to 100";
                }
                else
                {
                    lblAge.Text = "";
                }
            }
            if (string.IsNullOrEmpty(txtPhone.Text))
            {
                valid = false;
                lblPhone.Text = "Enter Phone No";
            }
            else
            {
                if (!Regex.IsMatch(txtPhone.Text, @"^[0-9]{10}$"))
                {
                    lblPhone.Text = "Enter 10 digits Phone No";
                }
                else
                {
                    lblPhone.Text = "";
                }
            }
            if (string.IsNullOrEmpty(txtAddress.Text))
            {
                valid = false;
                lblAddress.Text = "Enter Address";
            }
            else
            {
                lblAddress.Text = "";
            }

            //if (string.IsNullOrEmpty(txtComment.Text))
            //{

            //    valid = false;
            //    lblComment.Text = "Enter Comment";
            //}
            //else
            //{
            //    lblComment.Text = "";
            //}
            if (pkrGender.SelectedItem == null)
            {
                valid = false;
                lblGender.Text = "Select Gender";
            }
            else
            {
                lblGender.Text = "";
            }

            return valid;
        }
        //protected override bool OnBackButtonPressed()
        //{
        //    Navigation.PopModalAsync(true);
        //    return true;
        //}

        private void btnCancel_Clicked(object sender, EventArgs e)
        {
            Navigation.PopAsync(true);

        }

        private void txtAge_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (e.NewTextValue.Length > 2)
            {
                txtAge.Text = txtAge.Text.Remove(2);
            }
          
        }

        private void FloatingActionButton_Clicked(object sender, EventArgs e)
        {

        }
    }
}