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

namespace EMeditekApp.Droid
{
    [Activity(Theme = "@style/Theme.Splash",
        MainLauncher = true,
        NoHistory = true)]
//    [Activity(Label = "SplashActivity")]
    public class SplashActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
         {


            try
            {
                base.OnCreate(savedInstanceState);
                System.Threading.Thread.Sleep(2000);
                StartActivity(typeof(MainActivity));
            }
            catch (Exception ex )
            {
                Message a = new Message();
                a.LongAlert(ex.ToString());
               
            }
           
            // Create your application here
        }
    }
}