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
using Android.Support.V4.Content;
using Android.Util;
using WK.TAA.CCHAxcess.CodeGames.Helpers;

namespace WK.TAA.CCHAxcess.CodeGames.Droid.Notifications
{
    [BroadcastReceiver(Enabled = true, Permission = "com.google.android.c2dm.permission.SEND")]
    [IntentFilter(new[] { "com.google.android.c2dm.intent.RECEIVE" }, Categories = new string[] { "com.wolterskluwer.quicktask" })]
    public class BackgroundBroadcastReciever : WakefulBroadcastReceiver
    {
        public override void OnReceive(Context context, Intent intent)
        {
            try
            {
                Log.Info(AppConstants.TAG, "Message Wakeups the App");
                var service = new Intent(context, typeof(PNRegistrationService));
                service.PutExtra("bgReciver", true);
                StartWakefulService(context, service);
            }
            catch(Exception ex)
            {
                Log.Error(AppConstants.TAG, ex.Message);
            }
        }
    }
}