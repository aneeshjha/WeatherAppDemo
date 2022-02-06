using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace WeatherApp.Model
{
    public class Main
    {
        [JsonProperty("temp")]
        public double Temp { get; set; }
        [JsonProperty("feels_like")]
        public double Feels_like { get; set; }
        [JsonProperty("temp_min")]
        public double Temp_min { get; set; }
        [JsonProperty("temp_max")]
        public double Temp_max { get; set; }
        [JsonProperty("pressure")]
        public int Pressure { get; set; }
        [JsonProperty("sea_level")]
        public int Sea_level { get; set; }
        [JsonProperty("grnd_level")]
        public int Grnd_level { get; set; }
        [JsonProperty("humidity")]
        public int Humidity { get; set; }
        [JsonProperty("temp_kf")]
        public double Temp_kf { get; set; }
    }

    public class Weather
    {
        [JsonProperty("id")]
        public int Id { get; set; }
        [JsonProperty("main")]
        public string Main { get; set; }
        [JsonProperty("description")]
        public string Description { get; set; }
        [JsonProperty("icon")]
        public string Icon { get; set; }
    }

    public class Clouds
    {
        [JsonProperty("all")]
        public int All { get; set; }
    }

    public class Wind
    {
        [JsonProperty("speed")]
        public double Speed { get; set; }
        [JsonProperty("deg")]
        public int Deg { get; set; }
        [JsonProperty("gust")]
        public double Gust { get; set; }
    }

    public class Sys
    {
        [JsonProperty("pod")]
        public string Pod { get; set; }
    }

    public class Rain
    {
        [JsonProperty("_3h")]
        public double _3h { get; set; }
    }

    public class List : BaseModel
    {
        [JsonProperty("dt")]
        public int Dt { get; set; }
        [JsonProperty("main")]
        public Main Main { get; set; }
        [JsonProperty("weather")]
        public List<Weather> Weather { get; set; }
        [JsonProperty("clouds")]
        public Clouds Clouds { get; set; }
        [JsonProperty("wind")]
        public Wind Wind { get; set; }
        [JsonProperty("visibility")]
        public int Visibility { get; set; }
        [JsonProperty("pop")]
        public double Pop { get; set; }
        [JsonProperty("sys")]
        public Sys Sys { get; set; }
        [JsonProperty("dt_txt")]
        public string Dt_txt { get; set; }
        [JsonProperty("rain")]
        public Rain Rain { get; set; }

        [JsonIgnore]
        public string DisplayDate => DateTime.Parse(Dt_txt).ToString("dddd, dd MMMM yyyy hh:mm tt");
        [JsonIgnore]
        public string SmallDisplayDate => DateTime.Parse(Dt_txt).ToString("dddd dd, hh:mm tt");
        [JsonIgnore]
        public string DisplayTemp => $"{Main?.Temp.ToString() ?? string.Empty} °C";
        [JsonIgnore]
        private bool _isSelected;
        public bool IsSelected
        {
            get
            {
                return _isSelected;
            }
            set
            {
                _isSelected = value;
                RaisePropertyChanged(nameof(IsSelected));
            }
        }
    }

    public class Coord
    {
        [JsonProperty("lat")]
        public double Lat { get; set; }
        [JsonProperty("lon  ")]
        public double Lon { get; set; }
    }

    public class City
    {
        public int id { get; set; }
        public string name { get; set; }
        public Coord coord { get; set; }
        public string country { get; set; }
        public int population { get; set; }
        public int timezone { get; set; }
        public int sunrise { get; set; }
        public int sunset { get; set; }
    }

    public class WeatherRoot
    {
        public string cod { get; set; }
        public string message { get; set; }
        public int cnt { get; set; }
        public List<List> list { get; set; }
        public City city { get; set; }
    }
}
