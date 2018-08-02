using EMeditekApp.Wellogo.Models;
using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Newtonsoft.Json;

namespace EMeditekApp.Wellogo.HRA_Steps
{

   
    public partial class HRAStep1 : ContentPage
    {
        public int HraId { get; set; }
        public HRA objHRA { get; set; }

        public PhysicalExamination objPhysicalExamination { get; set; }
        public double BMI { get; set; }
        public HRAStep1(int _HraId)
        {
            InitializeComponent();
            
            HraId = _HraId;
            GetHRA();
        }
        async void GetHRA()
        {
            try
            {

                overlay.IsVisible = true;
                #region GetHra
                objHRA = await App.TodoManager.GetHRAbyId(HraId);
                objPhysicalExamination = objHRA.data.physical_examination;

                txtBloodGroup.Text = objHRA.data.client.blood_group;
                txtBloodGroup.IsEnabled = string.IsNullOrEmpty(objHRA.data.client.blood_group);

                if (objPhysicalExamination != null)
                {

                    if (objPhysicalExamination.height_cms == null)
                    {
                        txtHeightFeet.Text = objPhysicalExamination.height_feet.ToString();
                        txtHeightInches.Text = objPhysicalExamination.height_inches.ToString();
                        chkFeetInches.Checked = true;
                        chkFeetInches_CheckChanged(null, null);
                    }
                    else
                    {
                        txtHeightCentimeters.Text = objPhysicalExamination.height_cms.ToString();
                        chkCentimeters.Checked = true;
                        chkCentimeters_CheckChanged(null, null);
                    }
                    chkHeightKnow.Checked = Convert.ToBoolean(objPhysicalExamination.height_dont_know);
                    chkHeightKnow_CheckChanged(null, null);
                    if (objPhysicalExamination.weight_kgs != null)
                    {
                        txtWeight.Text = objPhysicalExamination.weight_kgs.ToString();
                    }
                    chkWeightKnown.Checked = Convert.ToBoolean(objPhysicalExamination.weight_dont_know);
                    chkWeightKnown_CheckChanged(null, null);
                    txtBMI.Text = objPhysicalExamination.bmi.ToString();
                    if (objPhysicalExamination.blood_pressure_systolic != null)
                    {
                        txtBPSys.Text = objPhysicalExamination.blood_pressure_systolic.ToString();
                    }
                    chkBPSysKnown.Checked = Convert.ToBoolean(objPhysicalExamination.blood_pressure_systolic_dont_know);
                    chkBPSysKnown_CheckChanged(null, null);
                    if (objPhysicalExamination.blood_pressure_diastolic != null)
                    {
                        txtBPDio.Text = objPhysicalExamination.blood_pressure_diastolic.ToString();
                    }
                    chkBPDioKnown.Checked = Convert.ToBoolean(objPhysicalExamination.blood_pressure_diastolic_dont_know);
                    chkBPDioKnown_CheckChanged(null, null);

                }

                overlay.IsVisible = false;

                #endregion
            }
            catch (Exception ex)
            {
                //  DependencyService.Get<IMessage>().LongAlert();
                overlay.IsVisible = false;
                DependencyService.Get<IMessage>().LongAlert();
            }
        }
        private void swcFeetInches_Toggled(object sender, ToggledEventArgs e)
        {
            try
            {

            }
            catch (Exception ex)
            {
                DependencyService.Get<IMessage>().LongAlert();
            }
        }
        private void chkHeightKnow_CheckChanged(object sender, EventArgs e)
        {

            try
            {
                chkFeetInches.IsEnabled = !chkHeightKnow.Checked;
                chkCentimeters.IsEnabled = !chkHeightKnow.Checked;
                txtHeightFeet.IsEnabled = !chkHeightKnow.Checked;
                txtHeightInches.IsEnabled = !chkHeightKnow.Checked;
                txtHeightCentimeters.IsEnabled = !chkHeightKnow.Checked;
            }
            catch (Exception ex)
            {

                DependencyService.Get<IMessage>().LongAlert();
            }

        }
        private void chkCentimeters_CheckChanged(object sender, EventArgs e)
        {
            stkFeetInches.IsVisible = !chkCentimeters.Checked;
            stkCentimeters.IsVisible = chkCentimeters.Checked;
            chkFeetInches.Checked = false;

        }
        private void chkFeetInches_CheckChanged(object sender, EventArgs e)
        {
            stkFeetInches.IsVisible = chkFeetInches.Checked;
            stkCentimeters.IsVisible = !chkFeetInches.Checked;
            chkCentimeters.Checked = false;

        }
        private void chkWeightKnown_CheckChanged(object sender, EventArgs e)
        {
            txtWeight.IsEnabled = !chkWeightKnown.Checked;
        }
        private void chkBPSysKnown_CheckChanged(object sender, EventArgs e)
        {
            txtBPSys.IsEnabled = !chkBPSysKnown.Checked;
        }
        private void chkBPDioKnown_CheckChanged(object sender, EventArgs e)
        {
            txtBPDio.IsEnabled = !chkBPDioKnown.Checked;
        }

        void ShowErrors(HRAValidation Validation)
        {


            if (Validation.height_feet != null)
            {
                lblHeightFeet.Text = Validation.height_feet[0];
                lblHeightFeet.IsVisible = true;
            }
            if (Validation.height_inches != null)
            {
                lblHeightInches.Text = Validation.height_inches[0];
                lblHeightInches.IsVisible = true;

            }
            if (Validation.height_cms != null)
            {
                lblHeightCentimeter.Text = Validation.height_cms[0];
                lblHeightCentimeter.IsVisible = true;
            }
            if (Validation.weight_kgs != null)
            {
                lblWeight.Text = Validation.weight_kgs[0];
                lblWeight.IsVisible = true;
            }
            if (Validation.blood_pressure_systolic != null)
            {
                lblBpsys.IsVisible = true;
                lblBpsys.Text = Validation.blood_pressure_systolic[0];
            }
            if (Validation.blood_pressure_diastolic != null)
            {
                lblBpDio.IsVisible = true;
                lblBpDio.Text = Validation.blood_pressure_diastolic[0];
            }
            if (Validation.blood_group != null)
            {
                lblBloodGroup.IsVisible = true;
                lblBloodGroup.Text = Validation.blood_group[0];
            }
        }

        bool CheckValidations()
        {
            bool val = true;
            if (!chkHeightKnow.Checked)
            {
                if (chkCentimeters.Checked)
                {
                    if (string.IsNullOrEmpty(txtHeightCentimeters.Text))
                    {
                        val = false;
                        lblHeightCentimeter.IsVisible = true;
                        lblHeightCentimeter.Text = "The Height centimeter field is requires when none of height centimeter dont know are present";
                    }
                }
                else
                {
                    if (string.IsNullOrEmpty(txtHeightFeet.Text))
                    {
                        val = false;
                        lblHeightFeet.IsVisible = true;
                        lblHeightFeet.Text = "The Height feet field is required when none of height feet  dont know are present.";
                    }
                    //else if (Convert.ToInt32(txtHeightFeet.Text) > 99)
                    //{
                    //    val = false;
                    //    Toast.MakeText(Android.App.Application.Context, "Height Feet Not Valid", ToastLength.Long);
                    //}

                    if (string.IsNullOrEmpty(txtHeightInches.Text))
                    {
                        val = false;
                        lblHeightInches.IsVisible = true;
                        lblHeightInches.Text = "The Height inches field is requires when none of height inches dont know are present.";
                    }
                    //else if (Convert.ToInt32(txtHeightInches.Text) > 12)
                    //{
                    //    val = false;
                    //    Toast.MakeText(Android.App.Application.Context, "Height Inches Not Valid", ToastLength.Long);
                    //}
                }
            }
            if (!chkWeightKnown.Checked)
            {
                if (string.IsNullOrEmpty(txtWeight.Text))
                {
                    val = false;
                    lblWeight.IsVisible = true;
                    lblWeight.Text = "The Weight kgs field is required when weight dont know is not present";
                }
            }
            if (!chkBPSysKnown.Checked)
            {
                if (string.IsNullOrEmpty(txtBPSys.Text))
                {
                    val = false;
                    lblBpsys.IsVisible = true;
                    lblBpsys.Text = "The blood pressure systolic field is required when blood pressure systolic dont know is not present";
                }
            }
            if (!chkBPDioKnown.Checked)
            {
                if (string.IsNullOrEmpty(txtBPDio.Text))
                {
                    val = false;
                    lblBpDio.IsVisible = true;
                    lblBpDio.Text = "The blood pressure diastolic field is required when blood pressure diastolic dont know is not present";
                }
            }
            return val;
        }

        private async void btnSave_Clicked(object sender, EventArgs e)
        {
            try
            {
                if (CheckValidations())
                {
                    PhysicalExaminationInput objPhysicalExaminationInput = new PhysicalExaminationInput();
                    overlay.IsVisible = true;
                    if (!chkHeightKnow.Checked)
                    {
                        if (chkFeetInches.Checked)
                        {
                            objPhysicalExaminationInput.height_feet = txtHeightFeet.Text != null ? Convert.ToInt32(txtHeightFeet.Text) : (int?)null;
                            objPhysicalExaminationInput.height_inches = txtHeightInches.Text != null ? Convert.ToInt32(txtHeightInches.Text) : (int?)null;
                        }
                        else
                        {
                            objPhysicalExaminationInput.height_cms = txtHeightCentimeters.Text != null ? Convert.ToInt32(txtHeightCentimeters.Text) : (int?)null;
                        }
                    }
                    objPhysicalExaminationInput.height_dont_know = Convert.ToInt32(chkHeightKnow.Checked);

                    if (!chkWeightKnown.Checked)
                    {
                        objPhysicalExaminationInput.weight_kgs = txtWeight.Text != null ? Convert.ToInt32(txtWeight.Text) : (int?)null;
                    }
                    objPhysicalExaminationInput.weight_dont_know = Convert.ToInt32(chkWeightKnown.Checked);
                    txtBMI.Text = "0";

                    if (!chkBPSysKnown.Checked)
                    {
                        objPhysicalExaminationInput.blood_pressure_systolic = txtBPSys.Text != null ? Convert.ToInt32(txtBPSys.Text) : (int?)null;
                    }
                    objPhysicalExaminationInput.blood_pressure_systolic_dont_know = Convert.ToInt32(chkBPSysKnown.Checked);

                    if (!chkBPDioKnown.Checked)
                    {
                        objPhysicalExaminationInput.blood_pressure_diastolic = txtBPDio.Text != null ? Convert.ToInt32(txtBPDio.Text) : (int?)null;
                    }
                    objPhysicalExaminationInput.blood_pressure_diastolic_dont_know = Convert.ToInt32(chkBPDioKnown.Checked);
                    objPhysicalExaminationInput.blood_group = txtBloodGroup.Text;
                    if (!chkHeightKnow.Checked && !chkWeightKnown.Checked)
                    {
                        if (txtBMI.Text != null)
                        {
                            objPhysicalExaminationInput.bmi = Convert.ToDouble(BMI);
                        }
                    }
                    object json = await App.TodoManager.SaveHraStep1(objPhysicalExaminationInput, HraId);
                    if (json.ToString().Contains("error"))
                    {
                        HRAErrorRootObject Error = JsonConvert.DeserializeObject<HRAErrorRootObject>(json.ToString());
                        DependencyService.Get<IMessage>().LongAlert(Error.errors.messsage);
                        ShowErrors(Error.errors.validation);
                    }
                    else
                    {
                        objHRA = JsonConvert.DeserializeObject<HRA>(json.ToString());
                        await Navigation.PushAsync(new HRAStep2(objHRA));
                    }
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

        private void btnCancel_Clicked(object sender, EventArgs e)
        {
            App.Current.MainPage = new NavigationPage(new HealthRiskAssesment()) { BarBackgroundColor = Color.FromHex("#f44337"), BarTextColor = Color.White };
        }

        private void txtHeightFeet_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                if (!chkHeightKnow.Checked && !chkWeightKnown.Checked && !string.IsNullOrEmpty(txtWeight.Text))
                {
                    double weight = Convert.ToDouble(txtWeight.Text);
                    double HeightCm = 0;

                    bool valid = true;

                    if (chkCentimeters.Checked)
                    {
                        if (!string.IsNullOrEmpty(txtHeightCentimeters.Text))
                        {
                            HeightCm = Convert.ToDouble(txtHeightCentimeters.Text.Trim());
                        }
                        else
                        {
                            valid = false;
                        }
                    }
                    else
                    {
                        double HeightFeet = 0;
                        double HeightInch = 0;
                        if (!string.IsNullOrEmpty(txtHeightFeet.Text) && !string.IsNullOrEmpty(txtHeightInches.Text))
                        {
                            HeightFeet = Convert.ToDouble(txtHeightFeet.Text);
                            HeightInch = Convert.ToDouble(txtHeightInches.Text);
                            HeightCm = (HeightFeet * 30.48) + (HeightInch * 2.54);
                        }
                        else
                        {
                            valid = false;
                        }
                    }
                    if (valid)
                    {
                        HeightCm = HeightCm / 100; // Get Height in meter
                        BMI = weight / (HeightCm * HeightCm);
                        txtBMI.Text = Math.Round(BMI, 2).ToString();
                    }
                    else
                    {
                        txtBMI.Text = "0";
                    }


                }
                else
                {
                    txtBMI.Text = "0";
                }



            }
            catch (Exception ex)
            {
                DependencyService.Get<IMessage>().LongAlert();

            }
        }

       


        private async void ShowExitDialog()
        {
            var answer = await DisplayAlert("Exit", "Do you wan't to exit the App?", "Yes", "No");
            if (answer)
            {
              DependencyService.Get<IExitApp>().Exit();
            }
        }
    }
}