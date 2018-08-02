using EMeditekApp.Models;
using EMeditekApp.Wellogo.Models;
using Plugin.FilePicker;
using Plugin.FilePicker.Abstractions;
using Plugin.Media;
using Plugin.Media.Abstractions;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
using System;
using System.IO;
using System.Net.Http;
using System.Text.RegularExpressions;

using Xamarin.Forms;



namespace EMeditekApp.Wellogo
{
    public partial class Profile : ContentPage
    {

        private MediaFile _mediaFile;
        private FileData filedata;

        public Profile()
        {

            InitializeComponent();
            if (Device.RuntimePlatform == "iOS")
            {
                ToolbarItems.Add(new ToolbarItem("Update", "", () => { UpdateDetails(); }));
                ToolbarItems.Add(new ToolbarItem("Back", "", () => { App.SetupRedirection(new Tabbedpage.TabbedPage1()); App.Current.MainPage = App.MasterDetailPage; }));

            }
            if (Device.RuntimePlatform == "Android")
            {
                ToolbarItems.Add(new ToolbarItem("Update", "", () => { UpdateDetails(); }));
                ToolbarItems.Add(new ToolbarItem("Home", "back.png", () => { App.SetupRedirection(new Tabbedpage.TabbedPage1()); App.Current.MainPage = App.MasterDetailPage; }));
            }
            txtDob.MinimumDate = DateTime.Now.AddYears(-100);
            txtDob.MaximumDate = System.DateTime.Now;
            userdetails();
        }
        private async void UpdateDetails()
        {
            lblerrorheader.Text = "";
            lblError.Text = "";
            if (Validations())
            {
                CommonDropDowns objCommonDropDowns = new Models.CommonDropDowns();
                UpdateUserDetails objUpdateUserDetails = new Models.UpdateUserDetails();
                objUpdateUserDetails.name = txtName.Text.Trim();
                objUpdateUserDetails.email = txtEmail.Text.Trim();
                objUpdateUserDetails.phone = txtPhone.Text.Trim();
                objUpdateUserDetails.designation = txtDesignation.Text.Trim();
                objUpdateUserDetails.date_of_birth = Convert.ToDateTime(txtDob.Date).ToString("yyyy-MM-dd");
                objUpdateUserDetails.gender = ((CommonDropDowns)pkrGender.SelectedItem).Name;
                objUpdateUserDetails.blood_group = pkrBloodGroup.SelectedItem.ToString();
                objUpdateUserDetails.seniority_level = ((CommonDropDowns)pkrseniorityLevel.SelectedItem).ID.ToString();
                UserDetailsInfo objUserDetailsInfo = new Models.UserDetailsInfo()
                { emergency_number = txtEmergencyNumber.Text != null ? txtEmergencyNumber.Text.Trim() : "", employee_id = txtId.Text != null ? txtId.Text.Trim() : "" };
                objUpdateUserDetails.info = objUserDetailsInfo;
                object response = await App.TodoManager.UpdateProfileDetails(objUpdateUserDetails);
                if (response is UserDetais)
                {
                    lblError.TextColor = Color.Green;
                    lblerrorheader.TextColor = Color.Green;
                    lblerrorheader.Text = "";
                    lblError.Text = "Profile Updated successfully";

                    #region ChangeDatainSQL
                    // Change data in sql lite after updating profile
                    LoggedInUser objLogedInUser = new LoggedInUser();
                    objLogedInUser.sso_token = App.Authorization;
                    objLogedInUser.id = App.id;
                    objLogedInUser.Name = txtName.Text;
                    objLogedInUser.photo_url = App.KyorData.photo_url;
                    objLogedInUser.Designation = txtDesignation.Text;
                    objLogedInUser.CorporateLogo = App.CorporateImageURl;
                    objLogedInUser.Gender = objUpdateUserDetails.gender;
                    //objLogedInUser.MaritalStatus = objUpdateUserDetails.marital_status;
                    App.Database.SaveItem(objLogedInUser);
                    App.KyorData.name = txtName.Text;
                    App.Gender = objUpdateUserDetails.gender;
                    //  App.MaritalStatus = objUpdateUserDetails.marital_status;
                    #endregion
                    DependencyService.Get<IMessage>().LongAlert("Profile Updated successfully");
                }
                else
                {
                    string ErrorString = "";
                    PasswordError objPasswordError = (PasswordError)response;
                    foreach (var v in objPasswordError.errors.validation)
                    {
                        ErrorString += string.Join("\n", v.Value);
                    }
                    lblError.TextColor = Color.Red;
                    lblError.Text = ErrorString;
                    lblerrorheader.Text = "Validations";
                    lblerrorheader.TextColor = Color.Red;
                    DependencyService.Get<IMessage>().LongAlert("Unable to pass validations");
                }
            }
        }
        bool Validations()
        {
            try
            {
                bool valid = true;


                if (string.IsNullOrEmpty(txtName.Text))
                {
                    valid = false;
                    lblName.TextColor = Color.Red;
                    lblName.Text = "Enter Name";

                }
                else if (!Regex.IsMatch(txtName.Text, "^([a-z]+[,.]?[ ]?|[a-z]+['-]?)+$", RegexOptions.IgnoreCase))
                {
                    valid = false;
                    lblName.TextColor = Color.Red;
                    lblName.Text = "Invalid Name";
                }
                else
                {
                    lblName.TextColor = Color.FromHex("#000");
                    lblName.Text = "Name";
                }


                if (string.IsNullOrEmpty(txtEmail.Text))
                {
                    valid = false;
                    lblEmail.TextColor = Color.Red;
                    lblEmail.Text = "Enter Email";
                }
                else
                {
                    if (!Regex.IsMatch(txtEmail.Text, @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z", RegexOptions.IgnoreCase))
                    {
                        valid = false;
                        lblEmail.TextColor = Color.Red;
                        lblEmail.Text = "Invalid Email Entered";

                    }
                    else
                    {
                        lblEmail.TextColor = Color.FromHex("#000");
                        lblEmail.Text = "Email";
                    }
                }

                if (string.IsNullOrEmpty(txtPhone.Text))
                {
                    valid = false;
                    lblPhone.TextColor = Color.Red;
                    lblPhone.Text = "Enter Phone No";
                }
                else
                {
                    if (!Regex.IsMatch(txtPhone.Text, @"^[0-9]{10}$"))
                    {
                        lblPhone.TextColor = Color.Red;
                        lblPhone.Text = "Invalid Mobile No";
                    }
                    else
                    {
                        lblPhone.TextColor = Color.FromHex("#000");
                        lblPhone.Text = "Phone";
                    }
                }


                if (string.IsNullOrEmpty(txtDesignation.Text))
                {
                    valid = false;
                    lblDesignation.TextColor = Color.Red;
                    lblDesignation.Text = "Enter Designation";
                }
                else
                {
                    lblDesignation.TextColor = Color.FromHex("#000");
                    lblDesignation.Text = "Designation";
                }


                if (pkrGender.SelectedItem == null)
                {
                    valid = false;
                    lblGender.TextColor = Color.Red;
                    lblGender.Text = "Select Gender";
                }
                else
                {
                    lblGender.TextColor = Color.FromHex("#000");
                    lblGender.Text = "Gender";
                }

                if (pkrBloodGroup.SelectedItem == null)
                {
                    valid = false;
                    lblBG.TextColor = Color.Red;
                    lblBG.Text = "Select Blood Group";
                }
                else
                {
                    lblBG.TextColor = Color.FromHex("#000");
                    lblBG.Text = "Blood Group";
                }


                if (pkrseniorityLevel.SelectedItem == null)
                {
                    valid = false;
                    lblseniority.TextColor = Color.Red;
                    lblseniority.Text = "Enter Seniority Level";
                }
                else
                {
                    lblseniority.TextColor = Color.FromHex("#000");
                    lblseniority.Text = "Seniority Level";
                }
                if (!string.IsNullOrEmpty(txtEmergencyNumber.Text))
                {
                    if (!Regex.IsMatch(txtEmergencyNumber.Text, @"^[0-9]{10}$"))
                    {
                        lblemergency.TextColor = Color.Red;
                        lblemergency.Text = "Invalid Number";
                        valid = false;
                    }
                    else
                    {
                        lblemergency.TextColor = Color.FromHex("#000");
                        lblemergency.Text = "Emergency Number";
                    }
                }
                else
                {
                    lblemergency.TextColor = Color.FromHex("#000");
                    lblemergency.Text = "Emergency Number";
                }
                return valid;

            }
            catch (Exception ex)
            {

                throw;
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
        async void userdetails(object UserDetails = null)
        {
            try
            {
                overlay.IsVisible = true;
                UserDetais objUserDetais = new Models.UserDetais();

                if (UserDetails == null)
                {
                    var Dropdowns = new CommonDropDowns();
                    objUserDetais = await App.TodoManager.UserDetais(App.id);
                    pkrGender.ItemsSource = Dropdowns.Gender();
                    pkrseniorityLevel.ItemsSource = Dropdowns.GetSenorityLevels();
                }
                txtId.Text = objUserDetais.data.employee_code;
                txtName.Text = objUserDetais.data.name;
                txtEmail.Text = objUserDetais.data.email;
                txtPhone.Text = objUserDetais.data.phone;
                txtDesignation.Text = objUserDetais.data.designation;
                txtDob.Date = Convert.ToDateTime(objUserDetais.data.date_of_birth);
                pkrBloodGroup.SelectedItem = objUserDetais.data.blood_group;
                pkrGender.SelectedIndex = objUserDetais.data.gender.ToLower() == "male" ? 0 : 1;
                pkrseniorityLevel.SelectedIndex = objUserDetais.data.seniority_level - 1;
                txtId.Text = objUserDetais.data.info.employee_id;
                txtEmergencyNumber.Text = objUserDetais.data.info.emergency_number;
                ImgUserImage.Source = objUserDetais.data.photo_url;
                ImgUserImage.Source = App.KyorData != null ? App.KyorData.photo_url : "http://squareboat.testing.kyor.com/profile/photo/default.png";



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

        void LoadProfileImage()
        {

            ImgUserImage.Source = App.KyorData != null ? App.KyorData.photo_url : "http://squareboat.testing.kyor.com/profile/photo/default.png";
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            try
            {
                bool valid = false;
                var content = new MultipartFormDataContent();
                var action = await DisplayActionSheet("Upload", "Cancel", null, "Camera", "Gallary");
                if (action == "Camera")
                {
                    var cameraStatus = await CrossPermissions.Current.CheckPermissionStatusAsync(Permission.Camera);
                    var storageStatus = await CrossPermissions.Current.CheckPermissionStatusAsync(Permission.Storage);

                    if (cameraStatus != PermissionStatus.Granted || storageStatus != PermissionStatus.Granted)
                    {
                        var results = await CrossPermissions.Current.RequestPermissionsAsync(new[] { Permission.Camera, Permission.Storage });
                        cameraStatus = results[Permission.Camera];
                        storageStatus = results[Permission.Storage];
                    }
                    else
                    {
                        valid = true;
                    }

                    if ((cameraStatus == PermissionStatus.Granted && storageStatus == PermissionStatus.Granted) | valid)
                    {
                        valid = true;
                        await CrossMedia.Current.Initialize();
                        if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
                        {
                            await DisplayAlert("No Camera", ":( No camera available.", "OK");
                            return;
                        }
                        overlay.IsVisible = true;
                        _mediaFile = await CrossMedia.Current.TakePhotoAsync(new StoreCameraMediaOptions
                        {
                            Directory = "Sample",
                            Name = "Image.jpg",
                            PhotoSize = PhotoSize.Medium,
                            CompressionQuality = 95,
                        });
                        // lblUpload.Text = _mediaFile.Path;
                        overlay.IsVisible = true;
                        if (_mediaFile == null)
                            return;
                        string toBeSearched = "Sample/";
                        int ix = (_mediaFile.Path).IndexOf(toBeSearched);
                        if (ix != -1)
                        {
                            string name = (_mediaFile.Path).Substring(ix + toBeSearched.Length);
                            var memoryStream = new MemoryStream();
                            _mediaFile.GetStream().CopyTo(memoryStream);
                            _mediaFile.Dispose();
                            content.Add(new StringContent(txtPhone.Text), "phone");
                            content.Add(new StreamContent(new MemoryStream(memoryStream.ToArray())), "photo", name);
                        }
                    }
                    else
                    {
                        DependencyService.Get<IMessage>().LongAlert("Camera Permission Not Granted");
                    }

                }
                else
                    if (action == "Gallary")
                {
                    try
                    {
                        filedata = await CrossFilePicker.Current.PickFile();
                        overlay.IsVisible = true;
                        byte[] _bytes = DependencyService.Get<IImageCompress>().ResizeImage(filedata.DataArray);
                        if (filedata.DataArray.Length < 5000000)
                        {
                            content.Add(new StringContent(txtPhone.Text), "phone");
                            content.Add(new StreamContent(new MemoryStream(_bytes)), "photo", filedata.FileName);
                            valid = true;
                        }
                        else
                        {
                            DependencyService.Get<IMessage>().LongAlert("Please Upload Photo less than 5 Mb");
                            return;
                        }
                    }
                    catch (Exception ex)
                    {
                        DependencyService.Get<IMessage>().LongAlert(ex.ToString());
                    }
                }

                if (valid)
                {
                    object response = await App.TodoManager.UpdateUserProfilePicture(content);
                    if (response is UserDetais)
                    {
                        App.KyorData.photo_url = ((UserDetais)response).data.photo_url;
                        ImgUserImage.Source = ((UserDetais)response).data.photo_url;
                        DependencyService.Get<IMessage>().LongAlert("Photo Upload successfully");
                    }
                    else
                    {
                        string ErrorString = "";
                        PasswordError objPasswordError = (PasswordError)response;
                        try
                        {
                            foreach (var v in objPasswordError.errors.validation)
                            {
                                ErrorString += string.Join("\n", v.Value);
                            }
                            lblError.TextColor = Color.Red;
                            lblError.Text = ErrorString;
                            lblerrorheader.Text = "Validations";
                            lblerrorheader.TextColor = Color.Red;
                            DependencyService.Get<IMessage>().LongAlert(objPasswordError.errors.message);
                        }
                        catch (Exception)
                        {
                            DependencyService.Get<IMessage>().LongAlert(objPasswordError.errors.message);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                DependencyService.Get<IMessage>().LongAlert(ex.ToString());
                await DisplayAlert("Exception", ex.ToString(), "Ok");
            }
            finally
            {
                overlay.IsVisible = false;
            }
        }

        private void txtEmergencyNumber_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(txtEmergencyNumber.Text))
            {
                if (!Regex.IsMatch(txtEmergencyNumber.Text, @"^[0-9]{10}$"))
                {
                    lblemergency.TextColor = Color.Red;
                    lblemergency.Text = "Invalid Number";
                }
                else
                {
                    lblemergency.TextColor = Color.FromHex("#000");
                    lblemergency.Text = "Emergency Number";
                }
            }
            else
            {
                lblemergency.TextColor = Color.FromHex("#000");
                lblemergency.Text = "Emergency Number";
            }
        }
    }
}