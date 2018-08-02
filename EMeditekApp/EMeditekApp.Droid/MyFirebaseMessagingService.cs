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
using Firebase.Messaging;
using Android.Util;
using Android.Graphics;

namespace EMeditekApp.Droid
{
    [Service]
    [IntentFilter(new[] { "com.google.firebase.MESSAGING_EVENT" })]
    class MyFirebaseMessagingService : FirebaseMessagingService
    {
        const string TAG = "MyFirebaseMsgService";
        public override void OnMessageReceived(RemoteMessage message)
        {
            try
            {
                Log.Debug(TAG, "From: " + message.From);
                Log.Debug(TAG, "Notification Message Body: " + message.GetNotification().Body);
                SendNotification(message);
            }
#pragma warning disable CS0168 // The variable 'ex' is declared but never used
            catch (Exception ex)
#pragma warning restore CS0168 // The variable 'ex' is declared but never used
            {

            }
        }
        void SendNotification(RemoteMessage message)
        {
            try
            {
                var intent = new Intent(this, typeof(MainActivity));
                intent.AddFlags(ActivityFlags.ClearTop);

                foreach (var item in message.Data)
                {
                    if (item.Key == "Id")
                        intent.PutExtra("Id", item.Value);
                    if (item.Key == "Type")
                        intent.PutExtra("Type", item.Value);
                    if (item.Key == "GroupId")
                        intent.PutExtra("GroupId", item.Value);
                }


                var pendingIntent = PendingIntent.GetActivity(this, 0, intent, PendingIntentFlags.OneShot);
                Context context = this;
                Android.Content.Res.Resources res = context.Resources;
                Bitmap aBitmap = BitmapFactory.DecodeResource(res, Resource.Drawable.Splash);

                // Instantiate the Image (Big Picture) style:
                Notification.BigPictureStyle picStyle = new Notification.BigPictureStyle();
                // Convert the image to a bitmap before passing it into the style:
                picStyle.BigPicture(BitmapFactory.DecodeResource(Resources, Resource.Drawable.Splash));
                picStyle.SetSummaryText("Test");

                var notificationBuilder = new Notification.Builder(this)
                    .SetLargeIcon(aBitmap)
                    //.SetFullScreenIntent(pendingIntent, true)
                    .SetSmallIcon(Resource.Drawable.icon)                    
                    .SetContentTitle(message.GetNotification().Title)
                    .SetContentText(message.GetNotification().Body)                    
                    .SetAutoCancel(true)
                    .SetStyle(picStyle)
                    .SetDefaults(NotificationDefaults.Vibrate | NotificationDefaults.Sound)
                .SetContentIntent(pendingIntent);

                var notificationManager = NotificationManager.FromContext(this);
                notificationManager.Notify(0, notificationBuilder.Build());
            }
#pragma warning disable CS0168 // The variable 'ex' is declared but never used
            catch (Exception ex)
#pragma warning restore CS0168 // The variable 'ex' is declared but never used
            {

            }
        }
    }
}