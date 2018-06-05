using Plugin.Connectivity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace WK.TAA.CCHAxcess.CodeGames.Helpers
{
    public class ConnectivityManager
    {
        private static ConnectivityManager instance = null;

        private ConnectivityManager()
        {

        }

        public static ConnectivityManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new ConnectivityManager();
                }
                return instance;
            }
        }

        public async Task<bool> IsConnected()
        {
            return await CrossConnectivity.Current.IsRemoteReachable(AppConstants.INET_CONNECT_TEST_URL);
        }
    }
}
