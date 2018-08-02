using System;
using System.Collections.Generic;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using EMeditekApp.Wellogo.Models;
using System.Text.RegularExpressions;


namespace EMeditekApp.Wellogo
{
   
    public partial class MedicineDeleiveryDetails : ContentPage
    {
        PrescriptionDeleiveryDetails objPrescriptionDeleiveryDetails = new PrescriptionDeleiveryDetails();
        public Pharmacy _Pharmacy { get; set; }
        List<CommonDropDowns> lstCommonDropDowns = new List<Models.CommonDropDowns>();
        CommonDropDowns objCommonDropDowns = new CommonDropDowns();
        public MedicineDeleiveryDetails(Pharmacy objPharmacy = null)
        {
            InitializeComponent();
            _Pharmacy = objPharmacy;
            lstCommonDropDowns.Add(new Models.CommonDropDowns(1, "I’ll inform the pharmacist when he calls to confirm my order"));
            lstCommonDropDowns.Add(new Models.CommonDropDowns(2, "Yes, I want all the medicines"));
            lstCommonDropDowns.Add(new Models.CommonDropDowns(3, "No, let me specify the medicines i want from the prescription"));
            pkrprescription.ItemsSource = lstCommonDropDowns;
             BindForm();
        }
        bool Validations()
        {
            bool valid = true;
            if (string.IsNullOrEmpty(txtFullName.Text))
            {
                valid = false;
                lblName.Text = "Enter Full Name";
            }
            else if (!Regex.IsMatch(txtFullName.Text, "^([A-z]+[,.]?[ ]?|[A-z]+['-]?)+$"))
            {
                valid = false;
                lblName.Text = "Invalid Name";
            }
            else
            {
                    lblName.Text = "";
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
                    valid = false;
                    lblPhone.Text = "Enter 10 digits Phone No";
                }
                else
                {
                    lblPhone.Text = "";
                    
                }
            }


            if (string.IsNullOrEmpty(txtPIN.Text))
            {
                valid = false;
                lblPIN.Text = "Enter PIN";
            }
            else
            {
                if (!Regex.IsMatch(txtPIN.Text, @"^[0-9]{6}$"))
                {
                    lblPIN.Text = "Enter Valid PIN";
                    valid = false;
                }
                else
                {
                    lblPIN.Text = "";
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


            if (string.IsNullOrEmpty(txtState.Text))
            {
                valid = false;
                lblState.Text = "Enter State";
            }
            else
            {
                lblState.Text = "";
            }
            if (string.IsNullOrEmpty(txtCity.Text))
            {
                valid = false;
                lblCity.Text = "Enter City";
            }
            else
            {
                lblCity.Text = "";
            }

            if (pkrprescription.SelectedItem == null)
            {
                valid = false;
                lblprescription.Text = "This field is Mandatory";
            }
            else
            {

                if (((CommonDropDowns)pkrprescription.SelectedItem).ID == 3)
                {
                    if (txtMedicine.Text == "" || txtMedicine.Text == null)
                    {
                        valid = false;
                        lblprescription.Text = "This field is Mandatory";
                    }
                    else
                    {
                        lblprescription.Text = "";
                    }
                }
                else
                {
                   
                    lblprescription.Text = "";
                }
            }




            return valid;
        }
        async void BindForm()
        {
            try
            {
                
                overlay.IsVisible = true;
                if (_Pharmacy.data.address_dump != null)
                {
                    if (!string.IsNullOrEmpty(_Pharmacy.data.address_dump.address))
                    {
                        txtAddress.Text = _Pharmacy.data.address_dump.address;
                    }
                    if (!string.IsNullOrEmpty(_Pharmacy.data.address_dump.address))
                    {
                        txtAddress.Text = _Pharmacy.data.address_dump.address;
                    }
                    if (!string.IsNullOrEmpty(_Pharmacy.data.address_dump.name))
                    {
                        txtFullName.Text = _Pharmacy.data.address_dump.name;
                    }
                    if (!string.IsNullOrEmpty(_Pharmacy.data.address_dump.phone))
                    {
                        txtPhone.Text = _Pharmacy.data.address_dump.phone;
                    }
                    if (!string.IsNullOrEmpty(_Pharmacy.data.address_dump.address))
                    {
                        txtAddress.Text = _Pharmacy.data.address_dump.address;
                    }
                    if (!string.IsNullOrEmpty(_Pharmacy.data.address_dump.city))
                    {
                        txtCity.Text = _Pharmacy.data.address_dump.city;
                    }
                    if (!string.IsNullOrEmpty(_Pharmacy.data.address_dump.state))
                    {
                        txtState.Text = _Pharmacy.data.address_dump.state;
                    }
                    if (!string.IsNullOrEmpty(_Pharmacy.data.address_dump.pin.ToString()) && _Pharmacy.data.address_dump.pin.ToString().Trim() != "0")
                    {
                        txtPIN.Text = _Pharmacy.data.address_dump.pin.ToString();
                    }
                    if (_Pharmacy.data.prescription_type != null)
                    {
                        pkrprescription.SelectedIndex = Convert.ToInt32(_Pharmacy.data.prescription_type) -1;
                        if (((CommonDropDowns)pkrprescription.SelectedItem).ID == 3 )
                        {
                            txtMedicine.Text = _Pharmacy.data.medicines.ToString();
                            StackMedicine.IsVisible = true;
                        }
                    }
                }
                
                overlay.IsVisible = false;
            }
            catch (Exception ex)
            {
                overlay.IsVisible = false;
                DependencyService.Get<IMessage>().LongAlert();
            }
        }
        private async void btnSave_Clicked(object sender, EventArgs e)
        {
            try
            {
                if (Validations())
                {
                    overlay.IsVisible = true;
                    //txtMedicine.Text = objCommonDropDowns.ID == 3 ? txtMedicine.Text : "";
                    objPrescriptionDeleiveryDetails.address = txtAddress.Text.Trim();
                    objPrescriptionDeleiveryDetails.order_id = _Pharmacy.data.id;
                    objPrescriptionDeleiveryDetails.name = txtFullName.Text.Trim();
                    objPrescriptionDeleiveryDetails.phone = txtPhone.Text.Trim();
                    objPrescriptionDeleiveryDetails.address = txtAddress.Text.Trim();
                    objPrescriptionDeleiveryDetails.city = txtCity.Text.Trim();
                    objPrescriptionDeleiveryDetails.state = txtState.Text.Trim();
                    objPrescriptionDeleiveryDetails.pin = Convert.ToInt32(txtPIN.Text.Trim());
                    objPrescriptionDeleiveryDetails.prescription_type = ((CommonDropDowns)pkrprescription.SelectedItem).ID;
                    objPrescriptionDeleiveryDetails.medicines = ((CommonDropDowns)pkrprescription.SelectedItem).ID == 3 ? txtMedicine.Text : "";
                    Pharmacy objPharmacy = await App.TodoManager.PharmacyDeleivery(objPrescriptionDeleiveryDetails);
                    if (objPharmacy.status == "success")
                    {
                        await Navigation.PushAsync(new OrderConfirmationPharmacy(objPharmacy));
                    }
                    overlay.IsVisible = false;
                }
                else
                {
                    DependencyService.Get<IMessage>().LongAlert("Complete all Validations");
                }
            }
            catch (Exception ex)
            {
                overlay.IsVisible = false;
                DependencyService.Get<IMessage>().LongAlert();
            }
        }

        private async void pkrprescription_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                overlay.IsVisible = true;
                if (pkrprescription.SelectedItem != null)
                {
                    CommonDropDowns objCommonDropDowns = pkrprescription.SelectedItem as CommonDropDowns;
                    StackMedicine.IsVisible = objCommonDropDowns.ID == 3;

                }
                overlay.IsVisible = false;
            }
            catch (Exception ex)
            {
                await DisplayAlert("Validation passes", "Passes", "cancel");
            }
        }
    }
}