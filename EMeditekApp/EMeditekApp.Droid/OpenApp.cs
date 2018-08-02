using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Xamarin.Forms;
using EMeditekApp.Wellogo.Models;
using EMeditekApp.Droid;

[assembly: Dependency(typeof(OpenApp))]
namespace EMeditekApp.Droid
{
 public   class OpenApp :IOpenApp
    {
       public void OpenAApplication(string PackageName, string IOSUrl = null, string AndroidUrl = null)
        {
            try
            {
                Activity objActivity = new Activity();
                Intent intent = Android.App.Application.Context.PackageManager.GetLaunchIntentForPackage(PackageName);
                // If not NULL run the app, if not, take the user to the app store
                if (intent != null)
                {
                    intent.AddFlags(ActivityFlags.NewTask);
                    Android.App.Application.Context.StartActivity(intent);
                }
                else
                {
                    try
                    {
                        //intent = new Intent(Intent.ActionMain);
                        //intent.AddFlags(ActivityFlags.NewTask);
                        //intent.SetData(Android.Net.Uri.Parse("market://details?id=com.flipkart.android"));
                        //Android.App.Application.Context.StartActivity(new Intent(intent));


                        var urlStore = Device.OnPlatform(IOSUrl, AndroidUrl, "");
                        Device.OpenUri(new Uri(urlStore));
                        //intent = new Intent(Intent.ActionMain);
                        //intent.AddFlags(ActivityFlags.NewTask);
                        //intent.SetData(Android.Net.Uri.Parse("market://details?id=com.flipkart.android"));

                        //Android.App.Application.Context.StartActivity(intent);
                    }
                    catch (Exception ex)
                    {
                        DependencyService.Get<IMessage>().LongAlert();
                    }

                }
            }
            catch (Exception ex)
            {

                DependencyService.Get<IMessage>().LongAlert();
            }

        }
    }
}