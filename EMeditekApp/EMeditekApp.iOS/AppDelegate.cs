
using Foundation;
using UIKit;

using Firebase.InstanceID;
using Firebase.Analytics;
using Firebase.CloudMessaging;
using System;
using UserNotifications;
using XFShapeView.iOS;
using AsNum.XFControls.iOS;
using Plugin.Toasts;
using Xamarin.Forms;
using SuaveControls.FloatingActionButton.iOS.Renderers;
using ImageCircle.Forms.Plugin.iOS;
using System.Collections.Generic;

namespace EMeditekApp.iOS
{
    // The UIApplicationDelegate for the application. This class is responsible for launching the 
    // User Interface of the application, as well as listening (and optionally responding) to 
    // application events from iOS.
    [Register("AppDelegate")]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
    {
        //
        // This method is invoked when the application has loaded and is ready to run. In this 
        // method you should instantiate the window, load the UI into it and then make the window
        // visible.
        //
        // You have 17 seconds to return from this method, or iOS will terminate your application.
        //
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            AsNumAssemblyHelper.HoldAssembly();
            global::Xamarin.Forms.Forms.Init();
            FloatingActionButtonRenderer.InitRenderer();
            OxyPlot.Xamarin.Forms.Platform.iOS.PlotViewRenderer.Init();
            DependencyService.Register<ToastNotification>();
            Plugin.Toasts.ToastNotification.Init();
            //global::Xamarin.FormsMaps.Init();
            FormsPlugin.Iconize.iOS.IconControls.Init();
            var dummy = new FFImageLoading.Forms.Touch.CachedImageRenderer();
            FFImageLoading.Forms.Touch.CachedImageRenderer.Init();

            FormsPlugin.Iconize.iOS.IconControls.Init();

            Plugin.Iconize.Iconize.With(new Plugin.Iconize.Fonts.FontAwesomeModule());
            Plugin.Iconize.Iconize.With(new Plugin.Iconize.Fonts.IoniconsModule());
            ShapeRenderer.Init();
            ImageCircleRenderer.Init();
            LoadApplication(new App());

            return base.FinishedLaunching(app, options);
        }

        public override void DidEnterBackground(UIApplication application)
        {
            // Use this method to release shared resources, save user data, invalidate timers and store the application state.
            // If your application supports background exection this method is called instead of WillTerminate when the user quits.
            Messaging.SharedInstance.Disconnect();
            Console.WriteLine("Disconnected from FCM");
        }

        public override void WillEnterForeground(UIApplication application)
        {
            //ConnectToFCM (Window.RootViewController);
        }

        void SetupNotification(UIApplication application, NSDictionary userInfo)
        {
            var Id = userInfo["Id"] as NSString;
            var Type = userInfo["Type"] as NSString;
            var GroupId = userInfo["GroupId"] as NSString;

            if (application.ApplicationState == UIApplicationState.Active)
            {
                System.Diagnostics.Debug.WriteLine(userInfo);
                var aps_d = userInfo["aps"] as NSDictionary;
                var alert_d = aps_d["alert"] as NSDictionary;
                var body = alert_d["body"] as NSString;
                var title = alert_d["title"] as NSString;

                debugAlert(title, body, Id, Type, GroupId);
            }
            else if (application.ApplicationState == UIApplicationState.Inactive)
            {
                System.Diagnostics.Debug.WriteLine(userInfo);
                LoadApplication(new App());
            }
        }

        // To receive notifications in foregroung on iOS 9 and below.
        // To receive notifications in background in any iOS version
        public override void DidReceiveRemoteNotification(UIApplication application, NSDictionary userInfo, Action<UIBackgroundFetchResult> completionHandler)
        {

            SetupNotification(application, userInfo);

            // If you are receiving a notification message while your app is in the background,
            // this callback will not be fired 'till the user taps on the notification launching the application.

            // If you disable method swizzling, you'll need to call this method. 
            // This lets FCM track message delivery and analytics, which is performed
            // automatically with method swizzling enabled.
            //Messaging.GetInstance().AppDidReceiveMessage (userInfo);

            //if (NotificationReceived == null)
            //    return;

            //var e = new UserInfoEventArgs { UserInfo = userInfo };
            //NotificationReceived(this, e);


        }
        private void debugAlert(string title, string message, string Id, string Type, string GroupId)
        {
            var alert = new UIAlertView(title ?? "Title", message ?? "Message", null, "Cancel", "Ok");
            alert.Clicked += (sender, buttonArgs) =>
            {
                if (buttonArgs.ButtonIndex != alert.CancelButtonIndex)
                {
                    if (!string.IsNullOrEmpty(message))
                    {
                        //App.Current.MainPage = new MainPage();
                        LoadApplication(new App());
                        //App.Current.MainPage = new Banner("123", "Test");
                    }
                }
            };
            alert.Show();
        }
        // You'll need this method if you set "FirebaseAppDelegateProxyEnabled": NO in GoogleService-Info.plist
        public override void RegisteredForRemoteNotifications(UIApplication application, NSData deviceToken)
        {
            InstanceId.SharedInstance.SetApnsToken(deviceToken, ApnsTokenType.Sandbox);
        }

        class CustomNotification
        {
            public string ID { get; set; }
            public string Type { get; set; }
            public string GroupId { get; set; }


        }

        // To receive notifications in foreground on iOS 10 devices.
        [Export("userNotificationCenter:willPresentNotification:withCompletionHandler:")]
        public void WillPresentNotification(UNUserNotificationCenter center, UNNotification notification, Action<UNNotificationPresentationOptions> completionHandler)
        {
            NSDictionary userInfo = notification.Request.Content.UserInfo;
            var Id = userInfo["Id"] as NSString;
            var Type = userInfo["Type"] as NSString;
            var GroupId = userInfo["GroupId"] as NSString;

            NSDictionary aps = (NSDictionary)userInfo["aps"];
            var alert = (NSDictionary)aps["alert"];

            var body = alert["body"] as NSString;
            var title = alert["title"] as NSString;

            debugAlert(title, body, Id, Type, GroupId);

            //if (NotificationReceived == null)
            //    return;

            //var e = new UserInfoEventArgs { UserInfo = notification.Request.Content.UserInfo };
            //NotificationReceived(this, e);

        }

        // Receive data message on iOS 10 devices.
        public void ApplicationReceivedRemoteMessage(RemoteMessage remoteMessage)
        {
            Console.WriteLine(remoteMessage.AppData);
        }

        #region Workaround for handling notifications in background for iOS 10

        [Export("userNotificationCenter:didReceiveNotificationResponse:withCompletionHandler:")]
        public void DidReceiveNotificationResponse(UNUserNotificationCenter center, UNNotificationResponse response, Action completionHandler)
        {
            NSDictionary userInfo = response.Notification.Request.Content.UserInfo;
            var Id = userInfo["Id"] as NSString;
            var Type = userInfo["Type"] as NSString;
            var GroupId = userInfo["GroupId"] as NSString;

            NSDictionary aps = (NSDictionary)userInfo["aps"];
            var alert = (NSDictionary)aps["alert"];

            var body = alert["body"] as NSString;
            var title = alert["title"] as NSString;

            debugAlert(title, body, Id, Type, GroupId);

            //if (NotificationReceived == null)
            //    return;

            //var e = new UserInfoEventArgs { UserInfo = response.Notification.Request.Content.UserInfo };
            //NotificationReceived(this, e);
        }

        #endregion

        //////////////////
        ////////////////// END OF WORKAROUND
        //////////////////
        /// 
        public static void ShowMessage(string title, string message, UIViewController fromViewController, Action actionForOk = null)
        {
            if (UIDevice.CurrentDevice.CheckSystemVersion(8, 0))
            {
                var alert = UIAlertController.Create(title, message, UIAlertControllerStyle.Alert);
                alert.AddAction(UIAlertAction.Create("Ok", UIAlertActionStyle.Default, (obj) =>
                {
                    if (actionForOk != null)
                    {
                        actionForOk();
                    }
                }));

                fromViewController.PresentViewController(alert, true, null);
            }
            else
            {
                //new UIAlertView(title, message, null, "Ok", null).Show ();
            }
        }
        public override void OnActivated(UIApplication uiApplication)
        {
            UIApplication.SharedApplication.ApplicationIconBadgeNumber = 0;
            //UINavigationBar.Appearance.TintColor = UIColor.White;
            //UINavigationBar.Appearance.BackgroundColor = UIColor.FromRGB(13, 59, 93);
        }
        public override void ReceivedRemoteNotification(UIApplication application, NSDictionary userInfo)
        {

        }


        public override bool OpenUrl(UIApplication application, NSUrl receivedurl, string sourceApplication, NSObject annotation)
        {
            AsNumAssemblyHelper.HoldAssembly();
            global::Xamarin.Forms.Forms.Init();
            FloatingActionButtonRenderer.InitRenderer();
            OxyPlot.Xamarin.Forms.Platform.iOS.PlotViewRenderer.Init();
            DependencyService.Register<ToastNotification>();
            Plugin.Toasts.ToastNotification.Init();
            //global::Xamarin.FormsMaps.Init();
            FormsPlugin.Iconize.iOS.IconControls.Init();
            var dummy = new FFImageLoading.Forms.Touch.CachedImageRenderer();
            FFImageLoading.Forms.Touch.CachedImageRenderer.Init();

            FormsPlugin.Iconize.iOS.IconControls.Init();

            Plugin.Iconize.Iconize.With(new Plugin.Iconize.Fonts.FontAwesomeModule());
            Plugin.Iconize.Iconize.With(new Plugin.Iconize.Fonts.IoniconsModule());
            ShapeRenderer.Init();
            ImageCircleRenderer.Init();

            string url = receivedurl.ToString();
            //string[] values = Intent.DataString.ToString().Split('/');
            //  string inttype = values[values.Length - 1];
            string integrationtype = url.Trim().Contains("google_fit") ? "2" : "1";
            string integration = url.Substring(url.IndexOf("code=") + 4 + 1);
            //LoadApplication(new App(integrationtype, integration));
            LoadApplication(new App(integrationtype, integration));

            /* now store the url somewhere in the app’s context. The url is in the url NSUrl object. The data is in url.Host if the link as a scheme as superduperapp://something_interesting */
            return true;
        }
    }
}
