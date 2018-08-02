using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Widget;
using Android.Database;
using Java.Lang;
using Android.Provider;
using Xamarin.Forms;
using Android.Content;
using System.Collections.Generic;
using EMeditekApp.Wellogo;
using AsNum.XFControls.Droid;
using ImageCircle.Forms.Plugin.Droid;



//using UIKit;

namespace EMeditekApp.Droid
{

   [Activity(Label = "Kyor", Icon = "@drawable/icon", Theme = "@style/MainTheme", MainLauncher = false, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    [IntentFilter(new[] { Android.Content.Intent.ActionView },
        DataScheme = "kyor",
        Categories = new[] { Android.Content.Intent.CategoryDefault, Android.Content.Intent.CategoryBrowsable })]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        //public static Activity CurrentActivity { get { return (MainActivity)Forms.Context;  } }
        const string TAG = "MainActivity";
        protected override void OnCreate(Bundle bundle)
        {
            try
            {
            TabLayoutResource = 2130903113;// Resource.Layout.Tabbar;
            ToolbarResource = 2130903114;// Resource.Layout.Toolbar;
            base.OnCreate(bundle);
            AsNumAssemblyHelper.HoldAssembly();
            //FFImageLoading.Forms.Touch.CachedImageRenderer.Init();
         //   global::Xamd.ImageCarousel.Forms.Plugin.Droid.ImageCarouselRenderer.Init();
            global::Xamarin.Forms.Forms.Init(this, bundle);
            global::Xamarin.Auth.Presenters.XamarinAndroid.AuthenticationConfiguration.Init(this, bundle);
            OxyPlot.Xamarin.Forms.Platform.Android.PlotViewRenderer.Init();
                ImageCircleRenderer.Init();
                // CrossCurrentActivity.Current.Init(this, bundle);
                //FormsPlugin.Iconize.Droid.IconControls.Init(Resource.Id.toolbar);
                FormsPlugin.Iconize.Droid.IconControls.Init();
            FFImageLoading.Forms.Droid.CachedImageRenderer.Init(true);
                
                if (Intent.Data != null)
                {
                    if (Intent.Data.EncodedAuthority.ToString() == "tracker")
                    {
                        string url = Intent.DataString.ToString();
                        //string[] values = Intent.DataString.ToString().Split('/');
                        //  string inttype = values[values.Length - 1];
                        string integrationtype = url.Trim().Contains("google_fit") ? "2" : "1";
                        string integration = url.Substring(url.IndexOf("code=")+4 + 1);
                        LoadApplication(new App(integrationtype, integration));
                    }
            }
            else
            {
                LoadApplication(new App());
            }
                //Log.Debug(TAG, "InstanceID token: " + FirebaseInstanceId.Instance.Token);
                //ConfigureFireBase();
            }
            catch (System.Exception ex)
            {
                Message a = new Message();
                a.LongAlert(ex.ToString());
            }
        }
        protected override void OnStart()
        {
            //UINavigationBar.Appearance.TintColor = UIColor.FromRGB(255, 18, 27);

            try
            {
                base.OnStart();
            }
            catch (System.Exception  ex)
            {

                throw;
            }
         
        }
        [Android.Runtime.Register("onActivityResult", "(IILandroid/content/Intent;)V", "GetOnActivityResult_IILandroid_content_Intent_Handler")]
        protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            if (requestCode == 1)
            {
                #region GetDataFromGallery
                UploadPrescription UploadPrescription;
                var str = "";
                base.OnActivityResult(requestCode, resultCode, data);
                if (resultCode == Result.Ok)
                {
                    int Totalsize = 0;
                    List<byte[]> images = new List<byte[]>();
                    List<string> imagename = new List<string>();
                    var IsValid = true;
                    if (data != null)
                    {
                        ClipData clipData = data.ClipData;
                        if (clipData != null)
                        {
                            if (clipData.ItemCount > 4)
                            {
                                MessagingCenter.Send<App, string>((App)Xamarin.Forms.Application.Current, "imagevalidation", "Please Upload Select 4 Images");
                                return;
                            }
                            for (int i = 0; i < clipData.ItemCount; i++)
                            {
                                ClipData.Item item = clipData.GetItemAt(i);
                                Android.Net.Uri uri = item.Uri;
                                var path = GetRealPathFromURI(uri);
                                if (path != null)
                                {
                                    //Rotate Image
                                    var imageRotated = DependencyService.Get<IImageHelpers>().RotateImage(path);
                                    Totalsize += imageRotated.Length;
                                    if (Totalsize > 10000000)
                                    {
                                        IsValid = false;
                                        MessagingCenter.Send<App, string>((App)Xamarin.Forms.Application.Current, "imagevalidation", "Image Cannot be Greater than 10 MB");
                                        break;
                                    }
                                    //var newPath = ImageHelpers.SaveFile("TmpPictures", imageRotated, System.DateTime.Now.ToString("yyyyMMddHHmmssfff"));
                                    //  str = System.Convert.ToBase64String(imageRotated);
                                    images.Add(imageRotated);
                                    imagename.Add(uri.LastPathSegment);
                                    //   images.Add(newPath);
                                }
                            }
                        }
                        else
                        {
                            Android.Net.Uri uri = data.Data;
                            var path = GetRealPathFromURI(uri);
                            if (path != null)
                            {
                                //Rotate Image
                                var imageRotated = DependencyService.Get<IImageHelpers>().RotateImage(path);
                                Totalsize += imageRotated.Length;
                                if (Totalsize > 10000000)
                                {
                                    IsValid = false;
                                    MessagingCenter.Send<App, string>((App)Xamarin.Forms.Application.Current, "imagevalidation", "Image Cannot be Greater than 10 MB");
                                }
                                //  var newPath = ImageHelpers.SaveFile("TmpPictures", imageRotated, System.DateTime.Now.ToString("yyyyMMddHHmmssfff"));
                                str = System.Convert.ToBase64String(imageRotated);
                                images.Add(imageRotated);
                                imagename.Add(uri.LastPathSegment);
                                //images.Add(str);
                                //images.Add(newPath);
                            }
                        }
                        if (IsValid)
                         MessagingCenter.Send<App, List<byte[]>>((App)Xamarin.Forms.Application.Current, "ImagesSelected", images);
                        MessagingCenter.Send<App, List<string>>((App)Xamarin.Forms.Application.Current, "ImagesSelectedName", imagename);
                    }
                }
                #endregion GetDataFromGallery
            }
            else
            {

            }
        }
        public string GetRealPathFromURI(Android.Net.Uri contentURI)
        {
            try
            {
                string fullPathToImage = "";
                if (contentURI.EncodedAuthority == "")
                {
                    fullPathToImage = contentURI.Path;
                }
                else
                {
                    ICursor imageCursor = null;
                    imageCursor = ContentResolver.Query(contentURI, null, null, null, null);
                    imageCursor.MoveToFirst();
                    int idx = imageCursor.GetColumnIndex(MediaStore.Images.ImageColumns.Data);
                    if (idx != -1)
                    {
                        fullPathToImage = imageCursor.GetString(idx);
                    }
                    else
                    {
                        ICursor cursor = null;
                        var docID = DocumentsContract.GetDocumentId(contentURI);
                        var id = docID.Split(':')[1];
                        var whereSelect = MediaStore.Images.ImageColumns.Id + "=?";
                        var projections = new string[] { MediaStore.Images.ImageColumns.Data };
                        cursor = ContentResolver.Query(MediaStore.Images.Media.InternalContentUri, projections, whereSelect, new string[] { id }, null);
                        if (cursor.Count == 0)
                        {
                            cursor = ContentResolver.Query(MediaStore.Images.Media.ExternalContentUri, projections, whereSelect, new string[] { id }, null);
                        }
                        var colData = cursor.GetColumnIndexOrThrow(MediaStore.Images.ImageColumns.Data);
                        cursor.MoveToFirst();
                        fullPathToImage = cursor.GetString(colData);
                    }
                }
                return fullPathToImage;
            }
            catch (Exception ex)
            {
                 Toast.MakeText(Xamarin.Forms.Forms.Context, "Unable to get path", ToastLength.Long).Show();
                return null;
            }
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, Android.Content.PM.Permission[] grantResults)
        {
            Plugin.Permissions.PermissionsImplementation.Current.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        //        private void ConfigureFireBase()
        //        {

        //#if DEBUG

        //            Task.Run(() =>
        //            {
        //                var instanceId = FirebaseInstanceId.Instance;
        //                instanceId.DeleteInstanceId();
        //                Android.Util.Log.Debug("TAG", "{0} {1}", instanceId?.Token?.ToString(), instanceId.GetToken(GetString(Resource.String.gcm_defaultSenderId), Firebase.Messaging.FirebaseMessaging.InstanceIdScope));

        //            });

        //            // For debug mode only - will accept the HTTPS certificate of Test/Dev server, as the HTTPS certificate is invalid /not trusted
        //            ServicePointManager.ServerCertificateValidationCallback += (o, certificate, chain, errors) => true;


        //#endif
        //        }


    }
}

