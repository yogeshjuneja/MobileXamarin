using EMeditekApp.Wellogo.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Plugin.Connectivity;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace EMeditekApp.Wellogo
{
    public partial class AddTest : ContentPage
    {
        DateTime dtvisibleDate;
        public Checkupdata objCheckupdata { get; set; }
        public int CheckUpID { get; set; }
        public int FirstPatientID { get; set; }
        public JObject JsonData    { get; set; }
        private bool EditMode { get; set; }
        ObservableCollection<RequiredTest> lstRequiredTest;
        Int32 TotalAmount = 0;
        public bool InternetConnection
        {
            get
            {
                return CrossConnectivity.Current.IsConnected;
            }
        }
        public AddTest(int _Checkupid, int _FirstPatientID, bool _EditMode = false)
        {
            InitializeComponent();
            CheckUpID = _Checkupid;
            FirstPatientID = _FirstPatientID;
            EditMode = _EditMode;
            TimeSpan t1 = DateTime.Now.TimeOfDay;
            TimeSpan t2 = TimeSpan.Parse("15:00");

            //   int CurrentDay = (int)Enum.Parse(typeof(DayOfWeek), DateTime.Now.DayOfWeek.ToString());
            //   DateTime.Now.DayOfWeek.ToString();
            if (t1 > t2)
            {
                DPDoa.MinimumDate = DateTime.Now.AddDays(4).Date;
                dtvisibleDate = DateTime.Now.AddDays(4).Date;
            }
            else
            {
                DPDoa.MinimumDate = DateTime.Now.AddDays(3).Date;
                dtvisibleDate = DateTime.Now.AddDays(3).Date;
            }
            //MessagingCenter.Subscribe<App, OrderSummaryPatient>((App)Xamarin.Forms.Application.Current, "EditAHCPatient", (app, message) =>
            //{
            //    EditMode = true;
            //    objOrderSummaryPatient = message;
            //    FirstPatientID = message.dc.patient_checkup_id;
            //    GetData();

            //    pkrState.SelectedItem = objOrderSummaryPatient.dc.dc_dump.city.state_id;
            //    pkrCity.SelectedItem = objOrderSummaryPatient.dc.dc_dump.city.id;

            //});

            if (InternetConnection)
            {
                GetData();
            }
            else
            {
                Retry(_Checkupid, _FirstPatientID);
            }
        }
        async void Retry(int _Checkupid, int _FirstPatientID)
        {
            var Retry = await DisplayAlert("No Connection", "Please Check Your Connection", "Retry", "Cancel");
            if (Retry)
            {
                App.Current.MainPage = new NavigationPage(new HealthRiskAssesment(_Checkupid, _FirstPatientID));
            }
        }
        async void GetData()
        {
            try
            {
                overlay.IsVisible = true;
                if (CheckUpID != 0 && FirstPatientID != 0)
                {
                    objCheckupdata = await App.TodoManager.GetAllDetailsofPatient(CheckUpID, FirstPatientID);
                    if (objCheckupdata.status == "success")
                    {
                        pkrState.ItemsSource = objCheckupdata.data.states;
                        if (!EditMode)
                        {
                            if (objCheckupdata.data.next_patient != null)
                            {
                                btnNext.Text = "Next Patient";
                            }
                            else
                            {
                                btnNext.Text = "Proceed";
                            }
                        }
                        else
                        {   
                              JsonData = JObject.Parse(JsonConvert.SerializeObject(objCheckupdata.data.patient.dc));
                            int loopvar = 0;
                            foreach (State s in objCheckupdata.data.states)
                            {
                                if (s.state_id == Convert.ToInt32(JsonData["dc_dump"]["city"]["state"]["state_id"].ToString()))
                                {
                                    pkrState.SelectedIndex = loopvar;
                                    break;
                                }
                                loopvar++;
                            }
                            loopvar = 0;
                           
                            await Task.Delay(1000);
                            foreach (CityID c in pkrCity.ItemsSource)
                            {
                                if (c.city_id == Convert.ToInt32(JsonData["dc_dump"]["city"]["city_id"].ToString()))
                                {
                                    pkrCity.SelectedIndex = loopvar;
                                    break;
                                }
                                loopvar++;
                            }
                            loopvar = 0;

                            await Task.Delay(1000);
                            foreach (DC d in pkrDC.ItemsSource)
                            {
                                if (d.id == Convert.ToInt32(JsonData["dc_dump"]["id"].ToString()))
                                {
                                    pkrDC.SelectedIndex = loopvar;
                                    break;
                                }
                                loopvar++;
                            }
                            loopvar = 0;
                            await Task.Delay(1000);
                            JArray JObjectTests = (JArray)JArray.Parse(JsonConvert.SerializeObject(objCheckupdata.data.patient.tests));
                            foreach (RequiredTest r in lvTests.ItemsSource)
                            {
                                foreach (JObject jobj in JObjectTests)
                                {
                                    if (r.id == Convert.ToInt32(jobj["test_dump"]["id"].ToString()))
                                    {
                                        lstRequiredTest[loopvar].@checked = true;
                                        break;
                                    }
                                }
                                loopvar++;
                            }

                            lvTests.ItemsSource = null;
                            lvTests.ItemsSource = lstRequiredTest;
                            DateTime date =  Convert.ToDateTime(JsonData["appointment_date"].ToString());
                            DPDoa.Date = date;

                            TimeSpan time  = TimeSpan.Parse(Convert.ToDateTime(JsonData["appointment_date"].ToString()).ToString("HH:mm"));
                            TPAppointment.Time = time;
                            loopvar = 0;
                            btnNext.Text = "Proceed";
                        }


                        //if (objCheckupdata.data.previous_patient != null)
                        //{
                        //    btnPrevious.IsVisible = true;
                        //}
                        lblPatientName.Text = objCheckupdata.data.patient.name;
                        int maxdays = objCheckupdata.data.corporate.meta.blocked_days-1;
                        DPDoa.MaximumDate = dtvisibleDate.AddDays(maxdays);
                      
                    }
                    else
                    {
                        await DisplayAlert("", "Unable to Fetch Data", "OK");
                    }
                }
                //  lblPatientName.Text = objSavePatientResponse.data.patients[ActivePatient].name;
            }
            catch (Exception ex)
            {
                overlay.IsVisible = false;
                DependencyService.Get<IMessage>().LongAlert();
            }
            finally
            {
                overlay.IsVisible = false;
            }
        }
        private async void pkrState_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (InternetConnection)
                {
                    CityReponse objCityReponse = new CityReponse();
                    pkrCity.ItemsSource = null;
                    pkrDC.ItemsSource = null;
                    lvTests.ItemsSource = null;
                    overlay.IsVisible = true;
                    State objState = pkrState.SelectedItem as State;
                    int StateID = objState.id;
                    int BundleID = objCheckupdata.data.corporate.meta.bundle_id;
                    objCityReponse = await App.TodoManager.GetCityAHC(StateID, BundleID);
                    pkrCity.ItemsSource = objCityReponse.data;
                     if(!EditMode)
                    overlay.IsVisible = false;
                }
                else
                {
                    var Retry = await DisplayAlert("No Connection", "Please Check Your Connection", "Retry", "Cancel");
                    if (Retry)
                    {
                        pkrState_SelectedIndexChanged(sender, e);
                    }
                }
            }
            catch (Exception ex)
            {
                overlay.IsVisible = false;
                DependencyService.Get<IMessage>().LongAlert();
            }
        }
        private async void pkrCity_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (pkrCity.SelectedItem != null)
                {
                    if (InternetConnection)
                    {
                        overlay.IsVisible = true;
                        CityID objCityID = pkrCity.SelectedItem as CityID;
                        int CityID = objCityID.id;
                        int BundleID = objCheckupdata.data.corporate.meta.bundle_id;
                        //  objCheckupdata.data.patient.id 659
                        DCResponse objDC = await App.TodoManager.GetDCAHC(CityID, BundleID);
                        pkrDC.ItemsSource = objDC.data;
                        if (!EditMode)
                            overlay.IsVisible = false;
                        
                    }
                    else
                    {
                        var Retry = await DisplayAlert("No Connection", "Please Check Your Connection", "Retry", "Cancel");
                        if (Retry)
                        {
                            pkrCity_SelectedIndexChanged(sender, e);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                overlay.IsVisible = false;
                DependencyService.Get<IMessage>().LongAlert();
            }
        }
        private async void pkrDC_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (pkrDC.SelectedItem != null)
                {
                    if (InternetConnection)
                    {
                        overlay.IsVisible = true;
                        stkTests.IsVisible = true;
                        int patientid = objCheckupdata.data.patient.id;
                        int dcid = ((DC)pkrDC.SelectedItem).id;
                        Tests objTests = await App.TodoManager.GetTests(patientid, dcid);
                        lstRequiredTest = new ObservableCollection<RequiredTest>(objTests.data.required_tests as List<RequiredTest>);
                        //lstRequiredTest = objTests.data.required_tests;
                        //lstTests = ;
                        if (lstRequiredTest.Count > 0)
                        {
                            lvTests.HeightRequest = (lstRequiredTest.Count * 100) / 2;
                        }
                        lvTests.ItemsSource = lstRequiredTest;
                        if (!EditMode)
                            overlay.IsVisible = false;
                    }
                    else
                    {
                        var Retry = await DisplayAlert("No Connection", "Please Check Your Connection", "Retry", "Cancel");
                        if (Retry)
                        {
                            pkrDC_SelectedIndexChanged(sender, e);
                        }
                    }
                }
            }
            catch (Exception ex)
            {

                overlay.IsVisible = false;
                DependencyService.Get<IMessage>().LongAlert();
            }
        }
        private async void btnNext_Clicked(object sender, EventArgs e)
        {
            try
            {
                string errorstring = "";
                MessagingCenter.Subscribe<App, string>((App)Xamarin.Forms.Application.Current, "AHCAddtest", (app, message) =>
                {

                    try
                    {
                        AHCError err = JsonConvert.DeserializeObject<AHCError>(message);

                        foreach (var item in err.data)
                        {
                            errorstring += string.Join("\n", item.Value) + "\n";
                        }

                    }
                    catch (Exception ex)
                    {

                        throw;
                    }
                    //var options = obj["fields"][0]["options"].ToList();
                });

                if (InternetConnection)
                {
                    overlay.IsVisible = true;
                    string date = Convert.ToDateTime(DPDoa.Date).ToString("dd-MMM-yyyy");
                    string Time = TPAppointment.Time.ToString().Equals("00:00:00")?"12:00:00": TPAppointment.Time.ToString();
                    DateTime dt = Convert.ToDateTime(date + " " + Time);
                    if (pkrCity.SelectedItem != null && pkrDC.SelectedItem != null && pkrState.SelectedItem != null)
                    {
                        if (Convert.ToDateTime(DPDoa.Date) > System.DateTime.Now.AddDays(1))
                        {
                            if ((Convert.ToDateTime(DPDoa.Date)).DayOfWeek.ToString().ToUpper() != "SUNDAY")
                            {
                                List<RequiredTest> lstReuiredtestcheck = new List<RequiredTest>(lstRequiredTest).FindAll(x => x.@checked == true);
                                if (lstReuiredtestcheck.Count > 0)
                                {
                                    #region Post
                                    SaveTest objSaveTest = new SaveTest();
                                    objSaveTest.appointment_date = dt.ToString("yyyy-MM-dd HH:mm");
                                    objSaveTest.city_id = ((CityID)pkrCity.SelectedItem).id;
                                    objSaveTest.dc_id = ((DC)pkrDC.SelectedItem).id;
                                    objSaveTest.state_id = ((EMeditekApp.Wellogo.Models.State)pkrState.SelectedItem).id;
                                    objSaveTest.required_tests = lstReuiredtestcheck.Select(c => c.id).ToList();
                                    objSaveTest.questionnaires_recommended_tests = new List<int?> { null };
                                    objSaveTest.remaining_tests = new List<int?> { null };

                                    SaveTestResponse objSaveTestResponse = await App.TodoManager.SaveTest(objSaveTest, objCheckupdata.data.checkup.id, objCheckupdata.data.patient.id);
                                    if (objSaveTestResponse.status == "success")
                                    {
                                        if (!EditMode)
                                        {
                                            if (objCheckupdata.data.next_patient != null)
                                            {
                                                await Navigation.PushAsync(new AddTest(objCheckupdata.data.next_patient.checkup_id, objCheckupdata.data.next_patient.id));
                                            }
                                            else
                                            {
                                                await Navigation.PushModalAsync(new NavigationPage(new AHCCheckout(objCheckupdata.data.checkup.id)) { BarBackgroundColor = Color.FromHex("#f44337"), BarTextColor = Color.White });
                                            }
                                        }
                                        else
                                        {
                                            Navigation.PopModalAsync(true);
                                          Navigation.PushModalAsync(new NavigationPage(new AHCCheckout(objCheckupdata.data.checkup.id)) { BarBackgroundColor = Color.FromHex("#f44337"), BarTextColor = Color.White });
                                        }
                                    }
                                    else
                                    {

                                        DependencyService.Get<IMessage>().LongAlert(errorstring);
                                    }

                                    #endregion
                                }
                                else
                                {
                                    DependencyService.Get<IMessage>().LongAlert("Select at least one test");
                                }

                            }
                            else
                            {
                                DependencyService.Get<IMessage>().LongAlert("Unable to Book Tests on Sunday");
                            }

                        }
                        else
                        {
                            DependencyService.Get<IMessage>().LongAlert("Appointment Date Should be greater than day after tommorow");

                        }
                    }
                    else
                    {
                        DependencyService.Get<IMessage>().LongAlert("Select all fields in delivery address");
                    }
                }
                else
                {
                    var Retry = await DisplayAlert("No Connection", "Please Check Your Connection", "Retry", "Cancel");
                    if (Retry)
                    {
                        btnNext_Clicked(sender, e);
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

        private void btnPrevious_Clicked(object sender, EventArgs e)
        {

        }

        private void Switch_Toggled(object sender, ToggledEventArgs e)
        {
            try
            {
                var Switch = (Xamarin.Forms.Switch)sender;
                int Price = ((RequiredTest)Switch.BindingContext).price.price;
                if (Switch.IsToggled)
                {
                    TotalAmount += Price;
                }
                else
                {
                    TotalAmount = TotalAmount - Price;
                }
                lblAmount.Text = TotalAmount.ToString();

            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}
