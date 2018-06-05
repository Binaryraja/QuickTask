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
using Serilog;

namespace WK.TAA.CCHAxcess.CodeGames.Droid.Notifications
{
    public class NotificationHelper
    {
            private const String CHECK_OP_NO_THROW = "checkOpNoThrow";
            private const String OP_POST_NOTIFICATION = "OP_POST_NOTIFICATION";

            public static bool IsNotificationEnabled(global::Android.Content.Context context)
            {
                AppOpsManager mAppOps = (AppOpsManager)context.GetSystemService(global::Android.Content.Context.AppOpsService);
                ApplicationInfo appInfo = context.ApplicationInfo;
                String pkg = context.ApplicationContext.PackageName;
                int uid = appInfo.Uid;
                try
                {
                    var appOpsClass = Java.Lang.Class.ForName("android.app.AppOpsManager");
                    var checkOpNoThrowMethod = appOpsClass.GetMethod(CHECK_OP_NO_THROW, Java.Lang.Integer.Type, Java.Lang.Integer.Type, new Java.Lang.String().Class); 
                    var opPostNotificationValue = appOpsClass.GetDeclaredField(OP_POST_NOTIFICATION);
                    var value = (int)opPostNotificationValue.GetInt(Java.Lang.Integer.Type);
                    var mode = (int)checkOpNoThrowMethod.Invoke(mAppOps, value, uid, pkg);
                    return (mode == (int)AppOpsManagerMode.Allowed);
                }
                catch (Exception ex)
                {
                  Log.Information(ex.Message);
                }
            return false;
        }
    }
}