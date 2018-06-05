using Prism;
using Prism.Ioc;
using WK.TAA.CCHAxcess.CodeGames.ViewModels;
using WK.TAA.CCHAxcess.CodeGames.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Prism.DryIoc;
using System;
using System.Threading.Tasks;
using WK.TAA.CCHAxcess.CodeGames.Helpers;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace WK.TAA.CCHAxcess.CodeGames
{
    public partial class App : PrismApplication
    {
        /* 
         * The Xamarin Forms XAML Previewer in Visual Studio uses System.Activator.CreateInstance.
         * This imposes a limitation in which the App class must have a default constructor. 
         * App(IPlatformInitializer initializer = null) cannot be handled by the Activator.
         */
        public App() : this(null) { }

        public App(IPlatformInitializer initializer) : base(initializer) { }

        protected override async void OnInitialized()
        {
            InitializeComponent();
            try
            {
                if(!WK.TAA.CCHAxcess.CodeGames.Helpers.Settings.Current.UserLoggedIn)
                    await NavigationService.NavigateAsync("Navigation/Login");
                else
                    await NavigationService.NavigateAsync("/Index/Navigation/Home");
            }
            catch(Exception ex)
            {
                SetMainPageFromException(ex);
            }
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
           containerRegistry.RegisterForNavigation<NavigationPage>("Navigation");
            containerRegistry.RegisterForNavigation<MasterPage>("Index");
            containerRegistry.RegisterForNavigation<MainPage>("Home");
            containerRegistry.RegisterForNavigation<LoginPage>("Login");
            containerRegistry.RegisterForNavigation<About>("About");
            containerRegistry.RegisterForNavigation<WK.TAA.CCHAxcess.CodeGames.Views.Settings>("Settings");
            containerRegistry.RegisterForNavigation<JobDetail>("JobDetail");
        }

        private void SetMainPageFromException(Exception ex)
        {
            var layout = new StackLayout
            {
                Padding = new Thickness(40)
            };

            layout.Children.Add(new Label
            {
                Text = ex?.GetType()?.Name ?? "Unknown Error encountered",
                FontAttributes = FontAttributes.Bold,
                HorizontalOptions = LayoutOptions.Center
            });

            layout.Children.Add(new ScrollView
            {
                Content = new Label
                {
                    Text = $"{ex}",
                    LineBreakMode = LineBreakMode.WordWrap
                }
            });

            MainPage = new ContentPage
            {
                Content = layout
            };
        }
  }
}

