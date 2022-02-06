using FreshMvvm;
using FreshTinyIoC;
using WeatherApp.PageModels;
using WeatherApp.Services;
using Xamarin.Forms;

namespace WeatherApp
{
    public partial class App : Application
    {
        public App()
        {
            SetupDependencies();
            InitializeComponent();
        }

        /// <summary>
        /// Regestring apis.
        /// </summary>
        private void SetupDependencies()
        {
            FreshTinyIoCContainer.Current.Register<IWeatherService, WeatherService>();
        }

        protected override void OnStart()
        {
            var page = FreshPageModelResolver.ResolvePageModel<SelectcityPageModel>();
            var basicNavContainer = new FreshNavigationContainer(page);
            MainPage = basicNavContainer;
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
