using System;
using System.Collections.Generic;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using EMeditekApp.Wellogo.Models;
using System.Collections.ObjectModel;
using System.Text.RegularExpressions;
using Newtonsoft.Json;

using System.Linq;
using System.Globalization;

namespace EMeditekApp.Wellogo
{
    
    public partial class HealthCheckups : ContentPage
    {
       
        private int LastIndex =-1;
        public ObservableCollection<HealthCheckUpDeafult> lstHealthCheckupInitial { get; set; }
        private SavedPatientResponse objSavedPatientResponse { get; set; }
        CommonDropDowns objCommonDropDowns = new Models.CommonDropDowns();
        HealthCheckUpDeafult objHealthCheckUpDeafult = new Models.HealthCheckUpDeafult();  
        public HealthCheckups()
        {
            InitializeComponent();

            lstHealthCheckupInitial = new ObservableCollection<HealthCheckUpDeafult>();
            objHealthCheckUpDeafult = new Models.HealthCheckUpDeafult();
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
                BindPageItems();
            }
            catch (Exception ex)
            {
                DependencyService.Get<IMessage>().LongAlert();
            }
        }
       
        private async void btnAddMore_Clicked(object sender, EventArgs e)
        {
            try
            {
                overlay.IsVisible = true;
                objHealthCheckUpDeafult = new Models.HealthCheckUpDeafult();
                objHealthCheckUpDeafult.RemoveButtonVisiblity = lstHealthCheckupInitial.Count > 0;
                objHealthCheckUpDeafult.ExistingPatientVisiblity = true;
                objHealthCheckUpDeafult.Name = "";
                objHealthCheckUpDeafult.EmployeeCode = "";
                objHealthCheckUpDeafult.SeniorityLevel = null;
                objHealthCheckUpDeafult.Phone = "";
                objHealthCheckUpDeafult.MaritalStatus = null;
                objHealthCheckUpDeafult.Gender = null;
                objHealthCheckUpDeafult.DateOfBirth = System.DateTime.Now.ToString();
                objHealthCheckUpDeafult.lstExistingPatient = objSavedPatientResponse.data;
                objHealthCheckUpDeafult.SeniorityLevelEnablity = true;
                lstHealthCheckupInitial.Add(objHealthCheckUpDeafult);
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
                string Message = "All Fields are Required";
                //await Navigation.PushAsync(new AddTest());
                //return;
                var Valid = true;
                #region Validation
                foreach (HealthCheckUpDeafult hc in lstHealthCheckupInitial)
                {
                    if (hc.Name == "")
                    {
                        Valid = false;
                        break;
                    }
                    if (hc.Relation == null)
                    {
                        Valid = false;
                        break;
                    }
                     hc.RelationID = objCommonDropDowns.GetRalations().Find(x => x.Name == hc.Relation).ID.ToString();
                    if (hc.EmployeeCode == "" && hc.Relation == "Self")
                    {
                        Valid = false;
                        break;
                    }
                    if (hc.ID == 0 && hc.Relation == "Self")
                    {
                        if (hc.SeniorityLevel == null)
                        {
                            Valid = false;
                            break;
                        }
                        else if (hc.SeniorityLevelID == null)
                        {
                            hc.SeniorityLevelID = objCommonDropDowns.GetSenorityLevels().Find(x => x.Name == hc.SeniorityLevel).ID.ToString();
                        }
                    }
                    else
                    {
                        if (hc.SeniorityLevelID == null && hc.SeniorityLevel != null)
                        {
                            hc.SeniorityLevelID = objCommonDropDowns.GetSenorityLevels().Find(x => x.Name == hc.SeniorityLevel).ID.ToString();
                        }
                    }
                    
                    if (hc.Phone == "")
                    {
                        Valid = false;
                        break;
                    }
                    else if (!Regex.IsMatch(hc.Phone, @"^[0-9]{10}$"))
                    {
                        Message = "Not Valid Mobile No.";
                       // DependencyService.Get<IMessage>().LongAlert("Not Valid Mobile No.");
                        Valid = false;
                    }
                    if (hc.DateOfBirth == "")
                    {
                        Valid = false;
                        break;
                    }
                    if (hc.MaritalStatus == null)
                    {
                        Valid = false;
                        break;
                    }
                    else if (hc.MaritalStatusID == null)
                    {
                        hc.MaritalStatusID = objCommonDropDowns.MaritalStatus().Find(x => x.Name == hc.MaritalStatus).ID;
                    }
                    if (hc.Gender == null)
                    {
                        Valid = false;
                        break;
                    }
                    else
                    {
                        hc.GenderID = objCommonDropDowns.Gender().Find(x => x.Name == hc.Gender).ID;
                    }

                    if(hc.Name.Length<2)
                    {
                        Valid = false;
                        Message = "Name should be of atleast 2 characters";
                    }
                }
                #endregion Validation
                string errorstring = "";
                AHCError err = new AHCError();
                MessagingCenter.Subscribe<App, string>((App)Xamarin.Forms.Application.Current, "AHCHealthCheckup", (app, message) =>
                {
                    try
                    {
                        err = JsonConvert.DeserializeObject<AHCError>(message);
                    }
                    catch (Exception ex)
                    {

                        throw;
                    }
                    //var options = obj["fields"][0]["options"].ToList();
                });

                if (Valid)
                {
                    PatientDetailsList objPatientDetailsList = new PatientDetailsList();
                    List<PatientDetails> lstPatientDetails = new List<Models.PatientDetails>();
                    PatientDetails objPatientDetails;

                    foreach (HealthCheckUpDeafult hc in lstHealthCheckupInitial)
                    {
                        objPatientDetails = new PatientDetails();
                        objPatientDetails.relation = hc.RelationID.ToString();
                        objPatientDetails.employee_code = hc.EmployeeCode;
                        objPatientDetails.name = hc.Name;
                        objPatientDetails.phone = hc.Phone;
                        CultureInfo.DefaultThreadCurrentCulture = new CultureInfo("en-US");
                        try
                        {
                            objPatientDetails.date_of_birth = Convert.ToDateTime(hc.DateOfBirth).ToString("yyyy-MM-dd"); //hc.DateOfBirth;
                        }
                        catch (Exception)
                        {
                            string Date = hc.DateOfBirth.ToString().Replace("00:00:00", "").Replace(" ", "");
                            objPatientDetails.date_of_birth = Date;
                        }
                        objPatientDetails.marital_status = hc.MaritalStatusID.ToString();
                        objPatientDetails.gender = hc.GenderID.ToString();
                        objPatientDetails.seniority_level = hc.SeniorityLevelID;
                        lstPatientDetails.Add(objPatientDetails);
                    }
                    objPatientDetailsList.patients = lstPatientDetails;
                    overlay.IsVisible = true;
                    SavePatientResponse objSavePatientResponse = await App.TodoManager.SavePatientDetails(objPatientDetailsList);
                    if (objSavePatientResponse.status == "success")
                    {
                        #region  Remove error messages
                        int index = 0;
                        foreach (HealthCheckUpDeafult hc in lstHealthCheckupInitial)
                        {
                            lstHealthCheckupInitial[index].ErrormessageVisiblity = false;
                            lstHealthCheckupInitial[index].ErrorMessage = "";
                          //  lstHealthCheckUpDeafult.Add(objHealthCheckUpDeafult);
                            index++;
                        }

                        lvHealthCheckups.ItemsSource = null;
                        lvHealthCheckups.ItemsSource = lstHealthCheckupInitial;
                        #endregion
                        await Navigation.PushAsync(new AddTest(objSavePatientResponse.data.id, objSavePatientResponse.data.patients[0].id));
                        //await DisplayAlert("Success", "Patient Added Successfully", "Ok");
                    }
                    else
                    {
                        int index = 0;
                        int errorindex = 0;
                        string Errormessage = "";
                        dynamic item = null;
                        ObservableCollection<HealthCheckUpDeafult> lstHealthCheckUpDeafult = new ObservableCollection<HealthCheckUpDeafult>();
                        HealthCheckUpDeafult objHealthCheckUpDeafult = new HealthCheckUpDeafult();
                        foreach (HealthCheckUpDeafult hc in lstHealthCheckupInitial)
                        {
                            objHealthCheckUpDeafult = hc;
                            if (errorindex < index | index == 0)
                            {
                                item = err.data.ElementAt(index);
                                errorindex = Convert.ToInt32(item.Key.ToString().Substring(item.Key.IndexOf('.') + 1, 1));
                            }
                            if (errorindex == index)
                            {
                                Errormessage = "";
                                hc.ErrormessageVisiblity = true;
                                Errormessage += "\n Error: \n" + string.Join("\n", item.Value) + "\n";
                                objHealthCheckUpDeafult.ErrormessageVisiblity = true;
                                objHealthCheckUpDeafult.ErrorMessage = Errormessage;
                                //lstHealthCheckUpDeafult[index]=
                                //lstHealthCheckupInitial[index].ErrormessageVisiblity  = true;
                                //  lstHealthCheckupInitial[index].ErrorMessage = Errormessage;
                            }
                            else
                            {
                                objHealthCheckUpDeafult.ErrormessageVisiblity = false;
                                objHealthCheckUpDeafult.ErrorMessage = "";
                            }
                            lstHealthCheckUpDeafult.Add(objHealthCheckUpDeafult);
                            index++;
                        }
                        lstHealthCheckupInitial = lstHealthCheckUpDeafult;
                        lvHealthCheckups.ItemsSource = lstHealthCheckUpDeafult;
                        DependencyService.Get<IMessage>().LongAlert(objSavePatientResponse.message);
                    }
                }
                else

                {
                    DependencyService.Get<IMessage>().LongAlert(Message);
                }
            }
            catch (InvalidOperationException ex)
            {

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
        async void BindPageItems()
        {
            try
            {
                overlay.IsVisible = true;
                objSavedPatientResponse = await App.TodoManager.GetSavedPatient();
                objHealthCheckUpDeafult.Name = "";
                objHealthCheckUpDeafult.Relation = null;
                objHealthCheckUpDeafult.ExistingPatientVisiblity = true;
                objHealthCheckUpDeafult.EmployeeCode = "";
                objHealthCheckUpDeafult.SeniorityLevel = null;
                objHealthCheckUpDeafult.Phone = "";
                objHealthCheckUpDeafult.MaritalStatus = null;
                objHealthCheckUpDeafult.Gender = null;
                objHealthCheckUpDeafult.DateOfBirth = System.DateTime.Now.ToString();
                objHealthCheckUpDeafult.lstExistingPatient = objSavedPatientResponse.data;
                objHealthCheckUpDeafult.RemoveButtonVisiblity = false;
                lstHealthCheckupInitial.Add(objHealthCheckUpDeafult);
                lvHealthCheckups.ItemsSource = lstHealthCheckupInitial;
                overlay.IsVisible = false;
            }
            catch (Exception ex)
            {
                overlay.IsVisible = false;
                DependencyService.Get<IMessage>().LongAlert();
            }
        }
        private async void btnRemove_Clicked(object sender, EventArgs e)
        {
            try
            {
                var IsDeleted = await DisplayAlert("", "Do you really want to remove", "AGREE", "DISAGREE");
                if (IsDeleted)
                {
                    HealthCheckUpDeafult objHealthCheckUpDeafult = ((HealthCheckUpDeafult)((Xamarin.Forms.Button)sender).CommandParameter);
                    lstHealthCheckupInitial.Remove(objHealthCheckUpDeafult);
                    //    lvHealthCheckups.ItemsSource= lstHealthCheckupInitial;
                }
            }
            catch (Exception ex)
            {
                
            }

        }
        private async void pkrExistingPatients_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                #region 

                overlay.IsVisible = true;
                var pkr = (Picker)sender;
                if (pkr.SelectedItem != null)
                {
                    int index = lstHealthCheckupInitial.IndexOf((HealthCheckUpDeafult)pkr.BindingContext);
                    HealthCheckUpDeafult objHealthCheckUpDeafult = ((HealthCheckUpDeafult)pkr.BindingContext) as HealthCheckUpDeafult;

                    SavedPatientResponse SavedPatientResponse = await App.TodoManager.GetSavedPatient(((SavedPatient)pkr.SelectedItem).name);
                    objSavedPatientResponse.data.Remove((SavedPatient)pkr.SelectedItem);
                    overlay.IsVisible = true;
                    objHealthCheckUpDeafult.ID = SavedPatientResponse.data[0].id;
                    objHealthCheckUpDeafult.Name = SavedPatientResponse.data[0].name;
                    objHealthCheckUpDeafult.RelationID = SavedPatientResponse.data[0].relation.ToString();
                    objHealthCheckUpDeafult.Relation = objCommonDropDowns.GetRalations()[SavedPatientResponse.data[0].relation - 1].Name;
                    objHealthCheckUpDeafult.MaritalStatusID = Convert.ToInt32(SavedPatientResponse.data[0].marital_status);
                    objHealthCheckUpDeafult.MaritalStatus = objCommonDropDowns.MaritalStatus()[Convert.ToInt32(SavedPatientResponse.data[0].marital_status) - 1].Name;
                    objHealthCheckUpDeafult.GenderID = Convert.ToInt32(SavedPatientResponse.data[0].gender);
                    int Gender = Convert.ToInt32(SavedPatientResponse.data[0].gender);
                    objHealthCheckUpDeafult.Gender = objCommonDropDowns.Gender()[Gender - 1].Name;
                    objHealthCheckUpDeafult.EmployeeCode = SavedPatientResponse.data[0].employee_code;
                    objHealthCheckUpDeafult.ExistingPatientVisiblity = false;
                    if (SavedPatientResponse.data[0].seniority_level != null)
                    {
                        objHealthCheckUpDeafult.SeniorityLevelID = SavedPatientResponse.data[0].seniority_level.ToString();
                        objHealthCheckUpDeafult.SeniorityLevel = objCommonDropDowns.GetSenorityLevels()[(int)SavedPatientResponse.data[0].seniority_level - 1].Name;
                    }
                    else
                    {
                        objHealthCheckUpDeafult.SeniorityLevelEnablity = false;
                    }
                    objHealthCheckUpDeafult.Phone = SavedPatientResponse.data[0].phone;
                    objHealthCheckUpDeafult.DateOfBirth = Convert.ToDateTime(SavedPatientResponse.data[0].date_of_birth).ToString("dd/MMM/yyyy");
                    lstHealthCheckupInitial[index] = objHealthCheckUpDeafult;
                    #endregion
                }
                overlay.IsVisible = false;

            }
            catch (Exception ex)
            {
                overlay.IsVisible = false;
                DependencyService.Get<IMessage>().LongAlert();
            }
        }
        private void pkrGender_BindingContextChanged(object sender, EventArgs e)
        {
            //var pkr = (Picker)sender;
            //string Title = pkr.Title;

            //if (Title == "Gender")
            //{
            //    if (objPickersData.Gender != null)
            //    {
            //        pkr.SelectedIndex = Convert.ToInt32(objPickersData.Gender);
            //    }
            //}

            //if (Title == "Relation")
            //{
            //    if (objPickersData.Relation != null)
            //    {
            //        pkr.SelectedIndex = (int)objPickersData.Relation;
            //    }
            //}
            //if (Title == "Marital Status")
            //{
            //    if (objPickersData.MaritalStatus != null)
            //    {
            //        pkr.SelectedIndex = (int)objPickersData.MaritalStatus;
            //    }
            //}
            //if (Title == "Seniority Level")
            //{
            //    if (objPickersData.SeniorityLevel != null)
            //    {
            //        pkr.SelectedIndex = (int)objPickersData.SeniorityLevel;
            //    }
            //}
        }
        private async void pkrRelation_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                var pkr = (Picker)sender;
                if (LastIndex != pkr.SelectedIndex)
                {
                    int id = objCommonDropDowns.GetRalations().Find(x => x.Name == pkr.SelectedItem.ToString()).ID;
                    enmRelations Relation = (enmRelations)id;
                    int index = lstHealthCheckupInitial.IndexOf((HealthCheckUpDeafult)pkr.BindingContext);
                    HealthCheckUpDeafult objHealthCheckUpDeafult = new Models.HealthCheckUpDeafult();
                    objHealthCheckUpDeafult=((HealthCheckUpDeafult)pkr.BindingContext) as HealthCheckUpDeafult;
                    if (Relation == enmRelations.Self)
                    {
 
                        UserDetais objUserDetais = await App.TodoManager.UserDetais(App.id);
                        objHealthCheckUpDeafult.SeniorityLevelEnablity = false;
                        objHealthCheckUpDeafult.EmployeeCodeEnablity = false;
                        objHealthCheckUpDeafult.GenderEnablity = false;
                        objHealthCheckUpDeafult.DobEnablity = false;
                        objHealthCheckUpDeafult.NameEnablity = false;
                        objHealthCheckUpDeafult.PhoneEnablity = false;
                        objHealthCheckUpDeafult.ID = objUserDetais.data.id;
                        objHealthCheckUpDeafult.Name = objUserDetais.data.name;
                        objHealthCheckUpDeafult.RelationID = "1";
                        objHealthCheckUpDeafult.Relation = "Self";
                        //  objHealthCheckUpDeafult.MaritalStatusID = objUserDetais.data.status;
                        //objHealthCheckUpDeafult.MaritalStatus = objCommonDropDowns.MaritalStatus()[Convert.ToInt32(SavedPatientResponse.data[0].marital_status) - 1].Name;
                        objHealthCheckUpDeafult.GenderID = objUserDetais.data.gender.ToLower() == "male" ? 0 : 1;
                        int Gender = objUserDetais.data.gender.ToLower() == "male" ? 0 : 1;
                        objHealthCheckUpDeafult.Gender = objCommonDropDowns.Gender()[Gender].Name;
                        objHealthCheckUpDeafult.EmployeeCode = objUserDetais.data.employee_code;
                        objHealthCheckUpDeafult.ExistingPatientVisiblity = false;
                        if (objUserDetais.data.seniority_level != 0)
                        {
                            objHealthCheckUpDeafult.SeniorityLevelID = objUserDetais.data.seniority_level.ToString();
                            objHealthCheckUpDeafult.SeniorityLevel = objCommonDropDowns.GetSenorityLevels()[(int)objUserDetais.data.seniority_level - 1].Name;
                        }
                        else
                        {
                            objHealthCheckUpDeafult.SeniorityLevelEnablity = false;
                        }
                        objHealthCheckUpDeafult.Phone = objUserDetais.data.phone;
                        objHealthCheckUpDeafult.DateOfBirth = Convert.ToDateTime(objUserDetais.data.date_of_birth).ToString("dd/MMM/yyyy");
                        LastIndex = pkr.SelectedIndex;
                        lstHealthCheckupInitial[index] = objHealthCheckUpDeafult;
                      
                    }
                    else
                    {
                           //lvHealthCheckups.ItemsSource = null;
                            objHealthCheckUpDeafult.SeniorityLevelEnablity = false;
                            objHealthCheckUpDeafult.EmployeeCodeEnablity = Relation == enmRelations.CoWorker;
                            objHealthCheckUpDeafult.GenderEnablity = true;
                            objHealthCheckUpDeafult.DobEnablity = true;
                            objHealthCheckUpDeafult.NameEnablity = true;
                            objHealthCheckUpDeafult.PhoneEnablity = true;
                            objHealthCheckUpDeafult.EmployeeCode = "";
                            objHealthCheckUpDeafult.SeniorityLevel = null;
                         

                            LastIndex = pkr.SelectedIndex;
                            lstHealthCheckupInitial[index] = objHealthCheckUpDeafult;
                            lvHealthCheckups.ItemsSource = lstHealthCheckupInitial;
                    }
                }
               


            }
            catch (Exception ex)
            {

                overlay.IsVisible = false;
                DependencyService.Get<IMessage>().LongAlert();
            }

        }


    }


}