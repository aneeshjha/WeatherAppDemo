namespace WeatherApp.Model
{
    public class CityDetailsModel
    {
        public string CityName { get; set; }
        public double Lat { get; set; }
        public double Long { get; set; }

        public CityDetailsModel(string name,double lat,double lon)
        {
            CityName = name;
            Lat = lat;
            Long = lon;
        }
    }
}
