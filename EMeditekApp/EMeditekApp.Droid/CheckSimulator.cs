using EMeditekApp.Wellogo;
using System.Text;
using System;
using Xamarin.Forms;
using EMeditekApp.Droid;
using System.IO;
using Android.Content;
using Xamarin.Forms.OAuth;
using Android.App;
using System.Threading.Tasks;

[assembly: Dependency(typeof(CheckSimulator))]
namespace EMeditekApp.Droid
{
    class CheckSimulator   :ICheckSimulator
    {

      public  bool Check()
        {
            
            string fing = Android.OS.Build.Fingerprint;
            bool isEmulator = false;
            
                isEmulator = fing.Contains("vbox") || fing.Contains("generic");
            return isEmulator;
                
        }
    }
}