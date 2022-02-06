using System;
using System.Globalization;
using Xamarin.Forms;

namespace WeatherApp.Converter
{
    public class BundledImageValueConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value==null)
            {
                return string.Empty;
            }
            var image = value.ToString().ToLower();
            image = image.Replace(" ", String.Empty);

            return Device.RuntimePlatform == Device.UWP ? $"Assets/{image}.png" : $"{image}.png";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
