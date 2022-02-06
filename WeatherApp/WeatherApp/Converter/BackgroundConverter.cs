using System;
using System.Globalization;
using Xamarin.Forms;

namespace WeatherApp.Converter
{
    public class BackgroundConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return string.Empty;

            var condition = value.ToString();
            string image = string.Empty;

            if (condition.Contains("cloud"))
            {
                image= Device.RuntimePlatform == Device.UWP? "Assets/cloudsbackground.jpg" : "cloudsbackground.jpg";
            }
            else if (condition.Contains("rain"))
            {
                image= Device.RuntimePlatform == Device.UWP ? "Assets/rainbackground.jpg" : "rainbackground.jpg";
            }
            else if (condition.Contains("sun") || (condition.Contains("clear sky")))
            {
                image= Device.RuntimePlatform == Device.UWP ? "Assets/sunbackground.jpg" : "sunbackground.jpg";
            }
            return image;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
