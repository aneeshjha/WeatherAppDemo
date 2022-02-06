using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using WeatherApp.Model;
using WeatherApp.Pages;
using Xamarin.Forms;

namespace WeatherApp.PageModels
{
    public class SelectcityPageModel : BasePageModel
    {
        #region Properties
        public ObservableCollection<CityDetailsModel> cityDetails { get; set; }
        public ICommand CitySelectedCommand { get; set; }
        #endregion

        #region Constructor
        public SelectcityPageModel()
        {
            CitySelectedCommand = new Command(CitySelectedCommandAsync);
        } 
        #endregion

        protected override void ViewIsAppearing(object sender, EventArgs e)
        {
            base.ViewIsAppearing(sender, e);
            //Create list of cities with latitude and longitude.
            CreateData();
        }

        private void CreateData()
        {
            cityDetails = new ObservableCollection<CityDetailsModel>();
            cityDetails.Add(new CityDetailsModel("Delhi", 28.704, 77.1025));
            cityDetails.Add(new CityDetailsModel("Bengaluru", 12.9716, 77.5946));
            cityDetails.Add(new CityDetailsModel("Mumbai", 19.0760, 72.8777));
            cityDetails.Add(new CityDetailsModel("Kolkata", 22.5726, 88.3639));
            cityDetails.Add(new CityDetailsModel("Jaipur", 26.9124, 75.7873));
            cityDetails.Add(new CityDetailsModel("Chennai", 13.0827, 80.2707));
        }

        /// <summary>
        /// Navigate to weather detail page by passing the city details
        /// </summary>
        /// <param name="obj"></param>
        private async void CitySelectedCommandAsync(object obj)
        {
            //To prevent multiple pushing the page in stack
            var existingPages = App.Current.MainPage.Navigation.NavigationStack.ToList();
            if (obj is CityDetailsModel citySelected && !existingPages.Any(x=>x.GetType()==typeof(WeatherDetailsPage)))
            {
                //Pass city details while navigating.
                await CoreMethods.PushPageModel<WeatherDetailsPageModel>(citySelected, false, true);
            }
        }
    }
}
