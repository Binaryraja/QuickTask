using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace WK.TAA.CCHAxcess.CodeGames.Notifications
{
    public class CrossAzurePushNotifications
    {
        private static readonly Lazy<IRemoteNotifications> Implementation = new Lazy<IRemoteNotifications>(CreatePluginAzurePushNotifications, LazyThreadSafetyMode.ExecutionAndPublication);

        public static IRemoteNotifications Current
        {
            get
            {
                var ret = Implementation.Value;
                if (ret == null)
                {
                    throw NotImplementedInReferenceAssembly();
                }
                return ret;
            }
        }

        private static IRemoteNotifications CreatePluginAzurePushNotifications()
        {
            return Xamarin.Forms.DependencyService.Get<IRemoteNotifications>();
        }

        private static Exception NotImplementedInReferenceAssembly()
        {
            return
                new NotImplementedException(
                    "This functionality is not implemented in the portable version of this assembly.  " +
                    "You should reference the NuGet package from your main application project in order to reference the platform-specific implementation.");
        }
    }
}
