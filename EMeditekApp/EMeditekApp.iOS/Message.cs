using EMeditekApp.iOS;
using EMeditekApp.Wellogo.Models;
using Foundation;
using MessageBar;
using Plugin.Toasts;
using System;
using UIKit;
using Xamarin.Forms;

[assembly: Dependency(typeof(Message))]
namespace EMeditekApp.iOS
{
    class Message : IMessage
    {
        NSTimer alertDelay;
        UIAlertController alert;
        public void LongAlert(string message)
        {
            ShowAlert(message, 2.0);
        }
        public void ShortAlert(string message)
        {
            ShowAlert(message, 2.0);
        }
        void ShowAlert(string message, double seconds)
        {
            new UIAlertView(null, message, null, "OK", null).Show();
            //alertDelay = NSTimer.CreateScheduledTimer(seconds, (obj) =>
            //{
            //    dismissMessage();
            //});
            //alert = UIAlertController.Create(null, message, UIAlertControllerStyle.Alert);
            //UIApplication.SharedApplication.KeyWindow.RootViewController.PresentViewController(alert, true, null);
        }
        void dismissMessage()
        {
            if (alert != null)
            {
                alert.DismissViewController(true, null);
            }
            if (alertDelay != null)
            {
                alertDelay.Dispose();
            }
        }
    }
}
