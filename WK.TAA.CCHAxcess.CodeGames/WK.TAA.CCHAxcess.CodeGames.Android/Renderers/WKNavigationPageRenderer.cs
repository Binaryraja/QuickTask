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
using Xamarin.Forms.Platform.Android.AppCompat;
using Android.Graphics;
using WK.TAA.CCHAxcess.CodeGames.Controls;
using WK.TAA.CCHAxcess.CodeGames.Droid.Renderers;
using Xamarin.Forms;

[assembly: ExportRenderer(typeof(WKNavigationPage), typeof(WKNavigationPageRenderer))]
namespace WK.TAA.CCHAxcess.CodeGames.Droid.Renderers
{
    public class WKNavigationPageRenderer : NavigationPageRenderer
    {
        public WKNavigationPageRenderer():base(Android.App.Application.Context)
        {

        }

        private Android.Support.V7.Widget.Toolbar toolbar;

        public override void OnViewAdded(Android.Views.View child)
        {
            base.OnViewAdded(child);
            if (child.GetType() == typeof(Android.Support.V7.Widget.Toolbar))
            {
                toolbar = (Android.Support.V7.Widget.Toolbar)child;
                toolbar.ChildViewAdded += Toolbar_ChildViewAdded;
            }
        }

        void Toolbar_ChildViewAdded(object sender, ChildViewAddedEventArgs e)
        {
            var view = e.Child.GetType();

            if (view == typeof(Android.Widget.TextView))
            {
                var textView = (Android.Widget.TextView)e.Child;
                var spaceFont = Typeface.CreateFromAsset(Android.App.Application.Context.ApplicationContext.Assets, "FiraSans-Regular.otf");
                textView.Typeface = spaceFont;
                toolbar.ChildViewAdded -= Toolbar_ChildViewAdded;
            }
            // If your app is not using the AppCompatTextView, you don't need this check
            if (view == typeof(Android.Support.V7.Widget.AppCompatTextView))
            {
                var textView = (Android.Support.V7.Widget.AppCompatTextView)e.Child;
                var spaceFont = Typeface.CreateFromAsset(Android.App.Application.Context.ApplicationContext.Assets, "FiraSans-Regular.otf");
                textView.Typeface = spaceFont;
                Console.WriteLine(textView.Text);
                toolbar.ChildViewAdded -= Toolbar_ChildViewAdded;
            }
        }
    }
}