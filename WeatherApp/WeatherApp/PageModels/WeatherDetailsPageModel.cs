using Acr.UserDialogs;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using WeatherApp.Model;
using WeatherApp.Services;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Constants = WeatherApp.Utility.Constants;

namespace WeatherApp.PageModels
{
    public class WeatherDetailsPageModel : BasePageModel
    {
        #region Properties
        private readonly IWeatherService _weatherService;
        public string Temp { get; set; }
        public string Condition { get; set; }
        public string Date { get; set; }
        public ObservableCollection<TempDetails> tempDetails { get; set; }
        public ObservableCollection<List> calendarDetails { get; set; }
        public ICommand DateTimeSelectedCommand { get; set; }
        private WeatherRoot weatherRoot;
        public CityDetailsModel cityDetails { get; set; }
        #endregion

        #region Constructor
        public WeatherDetailsPageModel(IWeatherService weatherService)
        {
            _weatherService = weatherService;
            DateTimeSelectedCommand = new Command(DateTimeSelectedCommandAsync);
        }
        #endregion

        /// <summary>
        /// Getting the passed data from previous page.
        /// </summary>
        /// <param name="initData"></param>
        public override void Init(object initData)
        {
            cityDetails = (CityDetailsModel)initData;
        }

        /// <summary>
        /// This methods is called when the View is appearing
        /// </summary>
        protected async override void ViewIsAppearing(object sender, EventArgs e)
        {
            try
            {
                if (isBusy)
                {
                    return;
                }
                isBusy = true;
                using (UserDialogs.Instance.Loading())
                {
                    weatherRoot = await _weatherService.GetWeatherAsync(cityDetails.Lat, cityDetails.Long);
                    if (weatherRoot.cod == "200")
                    {
                        PrepareCalendarList();
                    }
                    else
                    {
                        await ShowAlert(Constants.Alert, weatherRoot.message, Constants.Ok);
                    }
                }
            }
            catch (Exception ex)
            {
                await ShowAlert(Constants.Alert, ex.Message, Constants.Ok);
            }
            finally
            {
                isBusy = false;
            }
        }

        /// <summary>
        /// Preparing temp deatils
        /// like humidity,pressure,wind speed etc.
        /// </summary>
        /// <param name="index"></param>
        private void PrepareTempDetails(int index = 0)
        {
            Temp = weatherRoot?.list[index]?.Main?.Temp.ToString() + "°C";
            Condition = weatherRoot?.list[index]?.Weather[0]?.Description ?? string.Empty;
            Date = weatherRoot?.list[index]?.DisplayDate;

            tempDetails = new ObservableCollection<TempDetails>();
            tempDetails.Add(new TempDetails
            {
                DetailType = DetailType.Humidity.ToString(),
                DetailValue = $"{weatherRoot?.list[index]?.Main?.Humidity.ToString() ?? string.Empty} %"
            });
            tempDetails.Add(new TempDetails
            {
                DetailType = DetailType.Wind.ToString(),
                DetailValue = $"{weatherRoot?.list[index]?.Wind.Speed.ToString() ?? string.Empty} m/s"
            });
            tempDetails.Add(new TempDetails
            {
                DetailType = DetailType.Pressure.ToString(),
                DetailValue = $"{weatherRoot?.list[index]?.Main?.Pressure.ToString() ?? string.Empty} hpa"
            });
            tempDetails.Add(new TempDetails
            {
                DetailType = DetailType.Cloudness.ToString(),
                DetailValue = $"{weatherRoot?.list[index]?.Clouds.All.ToString() ?? string.Empty} unit"
            });
        }

        /// <summary>
        /// List to show the dates with time 
        /// and select the latest date
        /// </summary>
        private void PrepareCalendarList()
        {
            if (weatherRoot?.list != null && weatherRoot.list.Any())
            {
                calendarDetails = new ObservableCollection<List>(weatherRoot.list);
                calendarDetails.ForEach(x => x.IsSelected = false);
                //Shows the current or nearest time temp.
                var nearestTime = calendarDetails.FirstOrDefault(x => DateTime.Parse(x.Dt_txt) >= DateTime.Now);
                nearestTime.IsSelected = true;
                var index = calendarDetails.IndexOf(nearestTime);
                PrepareTempDetails(index);
            }
        }

        /// <summary>
        /// Update the details on tap of date selection.
        /// </summary>
        /// <param name="obj"></param>
        private async void DateTimeSelectedCommandAsync(object obj)
        {
            try
            {
                if (isBusy)
                {
                    return;
                }
                isBusy = true;
                if (obj is List dateObj)
                {
                    calendarDetails.ForEach(x => x.IsSelected = false);
                    calendarDetails.FirstOrDefault(x => x.Dt == dateObj.Dt).IsSelected = true;
                    var index = calendarDetails.IndexOf(calendarDetails.FirstOrDefault(x => x.Dt == dateObj.Dt));
                    PrepareTempDetails(index);
                }
            }
            catch (Exception ex)
            {
                await ShowAlert(Constants.Alert, ex.Message, Constants.Ok);
            }
            finally
            {
                isBusy = false;
            }
        }

        public override async Task<bool> ShowAlert(string title, string message, string ok = "", string cancel = "")
        {
            var result = await base.ShowAlert(title, message, ok, cancel);
            await CoreMethods.PopToRoot(true);
            return result;
        }
    }
}
