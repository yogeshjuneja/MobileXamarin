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


[assembly: Dependency(typeof(OAuth))]
namespace EMeditekApp.Droid
{

    class OpenPdf : IOpenPfd
    {
        public string OpenPdfFile(byte[] _pdfbytes)
        {
          
            string path = DependencyService.Get<IImageHelpers>().SavePdfFile("KyorReport", _pdfbytes, "download.pdf");
            Android.Net.Uri uri = Android.Net.Uri.Parse(path);
            Intent intent = new Intent(Intent.ActionView);
            intent.SetDataAndType(uri, "application/pdf");
            intent.SetFlags(ActivityFlags.ClearWhenTaskReset | ActivityFlags.NewTask);
            try
            {
                return path;
                //((Activity)objActivity).StartActivity(intent);
                //  ((Activity)objActivity).StartActivityForResult(Intent.CreateChooser(imageIntent, "Select photo"), 1);
                //Xamarin.Forms..Context.StartActivity(intent);
            }
            catch (Exception ex)
            {
                return "";
                //  Toast.MakeText(Android.App.Application.Context, "No Application Available to View PDF", ToastLength.Short).Show();
            }
        }
    }
}