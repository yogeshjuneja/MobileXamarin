using System;
using Xamarin.Forms;
using EMeditekApp.Droid;
using System.IO;
using Android.Content;
using EMeditekApp.Wellogo.Models;
using Android.Widget;

[assembly: Dependency(typeof(Message))]
namespace EMeditekApp.Droid
{
    class Message : IMessage
    {
        public void LongAlert(string message)
        {
            Toast.MakeText(Android.App.Application.Context, message, ToastLength.Long).Show();
        }
        public void ShortAlert(string message)
        {
            Toast.MakeText(Android.App.Application.Context, message, ToastLength.Short).Show();
        }
    }
}