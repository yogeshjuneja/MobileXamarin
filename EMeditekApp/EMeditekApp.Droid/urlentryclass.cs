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
using Android.Content.PM;

namespace EMeditekApp.Droid
{
    [Activity(Theme = "@style/Theme.Splash",
        MainLauncher = true,
        NoHistory = true, Label = "@string/app_name", ScreenOrientation = ScreenOrientation.Portrait, Icon = "@drawable/icon", ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    //[IntentFilter(new[] { Android.Content.Intent.ActionView },
    //    DataScheme = "kyor",
    //    Categories = new[] { Android.Content.Intent.CategoryDefault, Android.Content.Intent.CategoryBrowsable })]

    public class urlentryclass : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            Intent outsideIntent = Intent;
            if (Intent.Data.EncodedAuthority.ToString() == "tracker")
            {
                string[] values = Intent.DataString.ToString().Split('/');
                string inttype = values[values.Length - 1];
                string integrationtype = inttype.Substring(0, inttype.IndexOf('?'));
                string integration = inttype.Substring(inttype.IndexOf('=')+1);
            }
            StartActivity(typeof(MainActivity));
        }
    }
}