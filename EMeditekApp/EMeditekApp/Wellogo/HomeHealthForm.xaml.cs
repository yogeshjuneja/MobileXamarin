using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using EMeditekApp.Wellogo.Models;
using System.Text.RegularExpressions;
    
namespace EMeditekApp.Wellogo
{
    public partial class HomeHealthForm : ContentPage
    {
        public HCAH objHCAH { get; set; }
        public HomeHealthForm(HCAH HCAH)
        {
            InitializeComponent();
            try
            {
                objHCAH = HCAH;
                this.Title = objHCAH.name;
                CommonDropDowns objCommonDropDowns = new Models.CommonDropDowns();
                pkrGender.ItemsSource = objCommonDropDowns.Gender();
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
                if(Validations())
                {
                    FormFields objFormFields = new FormFields();
                    objFormFields.name = txtName.Text.Trim();
                    objFormFields.age = txtAge.Text.Trim();
                    objFormFields.phone = txtPhone.Text.Trim();
                    objFormFields.medical_condition = txtMedicalCondition.Text.Trim();
                    objFormFields.location = txtLocation.Text.Trim();
                    CommonDropDowns objobjFormFields = pkrGender.SelectedItem as CommonDropDowns;
                    objFormFields.gender = objobjFormFields.Name;
                    objFormFields.service_id = objHCAH.id;
                    var answer =   await DisplayAlert("Confirm Submission", "Are You Sure? Clicking on continue will take you to the payment gateway", "CONTINUE", "CANCEL");
                    if (answer)
                    {
                        overlay.IsVisible = true;
                        HCAHFormResponse objHCAHFormResponse = await App.TodoManager.PostHCAHFormData(objFormFields);
                        if (objHCAHFormResponse.status.ToLower() == "success")
                        {
                            ClearControls();
                           await DisplayAlert("Success", "Success Added", "OK");
                        }
                        else
                        {
                            await DisplayAlert(" ", "The given data failed to pass validations.", "OK");
                        }
                        overlay.IsVisible = false;
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
            if (string.IsNullOrEmpty(txtName.Text))
            {
                valid = false;
                lblName.Text = "Enter   Name";
            }
            else if (!Regex.IsMatch(txtName.Text, "^([A-z]+[,.]?[ ]?|[A-z]+['-]?)+$"))
            {
                valid = false;
                lblName.Text = "Invalid Name";
            }
            else
            {
                lblName.Text = "";
            }
            if (string.IsNullOrEmpty(txtAge.Text))
            {
                valid = false;
                lblAge.Text = "Enter Age";
            }
            else
            {
                if (Convert.ToInt32(txtAge.Text) == 0 || Convert.ToInt32(txtAge.Text) >100)
                {
                    valid = false;
                    lblAge.Text = "Enter Valid Age";
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
                    valid = false;
                }
                else
                {
                    lblPhone.Text = "";
                }
            }
            

            if (string.IsNullOrEmpty(txtMedicalCondition.Text))
            {

                valid = false;
                lblMedicalCOndition.Text = "Enter Medical Condition";
            }
            else
            {
                lblMedicalCOndition.Text = "";
            }
            if (pkrGender.SelectedItem == null)
            {
                valid = false;
                lblGender.Text = "Select Gender";
            }
            else
            {
                lblGender.Text = "";
            }
            if(string.IsNullOrEmpty(txtLocation.Text))
            {
                valid = false;
                lblLocation.Text = "Enter Location";
            }
            else
            {
                lblLocation.Text = "";
            }

            return valid;
        }

        void ClearControls()
        {
            try
            {
                txtName.Text = "";
                txtAge.Text = "";
                txtPhone.Text = "";
                txtMedicalCondition.Text = "";
                pkrGender.SelectedItem = null;
                txtLocation.Text = "";
                txtMessage.Text = "";
                 
            }
            catch (Exception ex)
            {

               DependencyService.Get<IMessage>().LongAlert();
            }
        }

        private void btnCancel_Clicked(object sender, EventArgs e)
        {
            try
            {
                Navigation.PopAsync(true);
            }
            catch (Exception ex)
            {

               DependencyService.Get<IMessage>().LongAlert();
            }
        }
    }
}