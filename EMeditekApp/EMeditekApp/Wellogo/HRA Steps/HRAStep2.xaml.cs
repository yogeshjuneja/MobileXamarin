using EMeditekApp.Wellogo.Models;
using Newtonsoft.Json;
using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace EMeditekApp.Wellogo.HRA_Steps
{
   
    public partial class HRAStep2 : ContentPage
    {
        public HRA objHRA { get; set; }
        public int HRAid { get; set; }
        public Investigations objInvestigations { get; set; }
        public HRAStep2(HRA HRA)
        {
            InitializeComponent();
         
            objHRA = HRA;
            if (HRA != null)
            {
                HRAid = objHRA.data.id;
                if (HRA.data.investigations != null)
                {
                    objInvestigations = objHRA.data.investigations;
                    ShhowData();
                }
            }
            else
            {
                btnSave.IsEnabled = false;

            }
        }
        async void ShhowData()
        {
            try
            {
                overlay.IsVisible = true;
                chkHaemoglobinKnown.Checked = Convert.ToBoolean(objInvestigations.haemoglobin_dont_know);
                chkHaemoglobinKnown_CheckChanged(null, null);
                if (!chkHaemoglobinKnown.Checked)
                {
                    txtHaemoglobin.Text = objInvestigations.haemoglobin.ToString();
                }
                chkCholestrol.Checked = Convert.ToBoolean(objInvestigations.total_cholesterol_dont_know);
                chkCholestrol_CheckChanged(null, null);
                if (!chkCholestrol.Checked)
                {
                    txtCholestrol.Text = objInvestigations.total_cholesterol.ToString();
                }
                chkbloodsugar.Checked = Convert.ToBoolean(objInvestigations.blood_sugar_dont_know);
                if (!chkbloodsugar.Checked)
                {
                    txtBloodsugar.Text = objInvestigations.blood_sugar.ToString();
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
       


        private async void ShowExitDialog()
        {
            var answer = await DisplayAlert("Exit", "Do you wan't to exit the App?", "Yes", "No");
            if (answer)
            {
              //  Android.OS.Process.KillProcess(Android.OS.Process.MyPid());
            }
        }


        private async void btnSave_Clicked(object sender, EventArgs e)
        {


            try
            {
                object obj;
                overlay.IsVisible = true;
                objInvestigations = new Models.Investigations();
                if (!chkHaemoglobinKnown.Checked && !string.IsNullOrEmpty(txtHaemoglobin.Text) )
                {
                    objInvestigations.haemoglobin = txtHaemoglobin.Text.Trim();
                }
                objInvestigations.haemoglobin_dont_know = Convert.ToInt32(chkHaemoglobinKnown.Checked);
                if (!chkCholestrol.Checked  && !string.IsNullOrEmpty(txtCholestrol.Text))
                {
                    objInvestigations.total_cholesterol = txtCholestrol.Text.Trim();
                }
                objInvestigations.total_cholesterol_dont_know = Convert.ToInt32(chkCholestrol.Checked);
                if (!chkbloodsugar.Checked && !string.IsNullOrEmpty(txtBloodsugar.Text))
                {
                    objInvestigations.blood_sugar = txtBloodsugar.Text.Trim();
                }
                objInvestigations.blood_sugar_dont_know = Convert.ToInt32(chkbloodsugar.Checked);
                obj = await App.TodoManager.SaveHraStep2(objInvestigations, HRAid);
                if (obj.ToString().Contains("error"))
                {
                    InvgestigationErrorRootObject objInvgestigationErrorRootObject = JsonConvert.DeserializeObject<InvgestigationErrorRootObject>(obj.ToString());
                    DependencyService.Get<IMessage>().LongAlert(objInvgestigationErrorRootObject.errors.messsage);
                    ShowErrors(objInvgestigationErrorRootObject.errors.validation);
                }
                else
                { 
                    objHRA = JsonConvert.DeserializeObject<HRA>(obj.ToString());
                    if (objHRA.data != null)
                    {
                        await Navigation.PushAsync(new HRAStep3(objHRA, HRAid));
                    }
                }

                overlay.IsVisible = false;


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
        void ShowErrors(InvestigationsErrors Validation)
        {

            if(Validation.haemoglobin!=null)
            {
                lblHaemoglobin.Text = Validation.haemoglobin[0];
                lblHaemoglobin.IsVisible = true;
            }
            else
            {
                lblHaemoglobin.IsVisible = false;
            }
            if(Validation.total_cholesterol!=null)
            {
                lblCholestrol.Text = Validation.total_cholesterol[0];
                lblCholestrol.IsVisible = true;
            }
            else
            {
                lblCholestrol.IsVisible = false;
            }

            if(Validation.blood_sugar!=null)
            {
                lblBloodsugar.Text = Validation.blood_sugar[0].ToString();
                lblBloodsugar.IsVisible = true;
            }
            else
            {
                lblBloodsugar.IsVisible = false;
            }
            
        }

        private void btnCancel_Clicked(object sender, EventArgs e)
        {
            App.Current.MainPage = new NavigationPage(new HealthRiskAssesment()) { BarBackgroundColor = Color.FromHex("#f44337"), BarTextColor = Color.White };
        }

        private void chkHaemoglobinKnown_CheckChanged(object sender, EventArgs e)
        {
            txtHaemoglobin.IsEnabled = !chkHaemoglobinKnown.Checked;
        }

        private void chkCholestrol_CheckChanged(object sender, EventArgs e)
        {
            txtCholestrol.IsEnabled = !chkCholestrol.Checked;
        }

        private void chkbloodsugar_CheckChanged(object sender, EventArgs e)
        {

            txtBloodsugar.IsEnabled = !chkbloodsugar.Checked;
        }
    }
}