using Android.Content;
using Android.Widget;
using EMeditekApp.Droid;
using Xamarin.Forms;
using Plugin.CurrentActivity;
using Android.App;
using System;

[assembly: Dependency(typeof(Galleryy))]
namespace EMeditekApp.Droid
{
    class Galleryy : IGallery
    {
        //int? IGallery.ImagesSelection
        //{
        //    get
        //    {
        //        return Convert.ToInt32(Xamarin.Forms.Application.Current.Properties["ImagesSelection"]);
        //    }

        //    set
        //    {
        //        Xamarin.Forms.Application.Current.Properties["ImagesSelection"] = value;
        //    }
        //}

        public void OpenGallery(int imageselection)
        {
            Android.App.Activity objActivity = CrossCurrentActivity.Current.Activity;
            //var imageIntent = new Intent(Intent.ActionView, MediaStore.Images.Media.ExternalContentUri);
            Intent imageIntent = new Intent(Intent.ActionPick);
            imageIntent.SetType("image/*");
            imageIntent.PutExtra(Intent.ExtraAllowMultiple, true);
            imageIntent.SetAction(Intent.ActionGetContent);
            ((Activity)objActivity).StartActivityForResult(Intent.CreateChooser(imageIntent, "Select photo"), 1);
            Toast.MakeText(Android.App.Application.Context, "Select a maximum of " + imageselection + " image/s", ToastLength.Long).Show();
        }


    }
}