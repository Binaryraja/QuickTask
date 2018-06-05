using System;
using System.Collections.Generic;
using System.Text;
using WK.TAA.CCHAxcess.CodeGames.Configuration;
using Xamarin.Forms;

namespace WK.TAA.CCHAxcess.CodeGames.Controls
{
    public class WKNavigationPage : NavigationPage
    {
        public WKNavigationPage(Page root) : base(root)
        {
            BarBackgroundColor = Color.FromHex(AppConfiguration.NAV_BAR_COLOR);
            BarTextColor = Color.White;
        }
    }
}
