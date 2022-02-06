namespace WeatherApp.Model
{
    public class TempDetails
    {
        public string DetailType { get; set; }
        public string DetailValue { get; set; }
    }

    public enum DetailType
    {
        Humidity = 0,
        Wind = 1,
        Pressure = 2,
        Cloudness = 3
    }
}
