using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenWeatherFinal.Models
{
    public class CityModel
    {
        [JsonProperty("id")]
        public float ID { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("state")]
        public string State { get; set; }
        [JsonProperty("country")]
        public string Country { get; set; }

        [JsonProperty("dt")]
        public int Time { get; set; }
        [JsonProperty("timezone")]
        public int Timezone { get; set; }

        [JsonProperty("coord")]
        public Coordinates Coords { get; set; }

        [JsonProperty("weather")]
        public List<Weather> Weather { get; set; }

        [JsonProperty("main")]
        public ClimateInfo Main { get; set; }

        [JsonProperty("wind")]
        public WindInfo Wind { get; set; }

        [JsonProperty("sys")]
        public SunInfo Sun { get; set; }

        public CityModel(float id, string name, string state, string country, Coordinates coords)
        {
            this.ID = id;
            this.Name = name;
            this.State = state;
            this.Country = country;
            this.Coords = coords;
        }
    }

    public class WindInfo
    {
        [JsonProperty("speed")]
        public float Speed { get; set; }
        [JsonProperty("deg")]
        public float Direction { get; set; }
    }

    public class ClimateInfo
    {
        [JsonProperty("temp")]
        public float Temp { get; set; }
        [JsonProperty("feels_like")]
        public float Feels_Like { get; set; }
        [JsonProperty("temp_min")]
        public float Temp_Min { get; set; }
        [JsonProperty("temp_max")]
        public float Temp_Max { get; set; }
        [JsonProperty("pressure")]
        public float Pressure { get; set; }
        [JsonProperty("humidity")]
        public float Humidity { get; set; }
    }

    public class Coordinates
    {
        [JsonProperty("lon")]
        public float Lon { get; set; }
        [JsonProperty("lat")]
        public float Lat { get; set; }
    }

    public class SunInfo
    {
        [JsonProperty("sunrise")]
        public int Sunrise { get; set; }
        [JsonProperty("sunset")]
        public int Sunset { get; set; }
    }

    public class Weather
    {
        [JsonProperty("id")]
        public float ID { get; set; }
        [JsonProperty("main")]
        public string Main { get; set; }
        [JsonProperty("description")]
        public string Description { get; set; }
        [JsonProperty("icon")]
        public string Icon { get; set; }
    }

    public class WeekForecast
    {
        [JsonProperty("daily")]
        public List<Forecast> Days { get; set; }

        public WeekForecast(List<Forecast> days)
        {
            this.Days = days;
        }
    }

    public class Forecast
    {
        [JsonProperty("dt")]
        public float Date { get; set; }
        [JsonProperty("sunrise")]
        public float Sunrise { get; set; }
        [JsonProperty("sunset")]
        public float Sunset { get; set; }
        [JsonProperty("moonrise")]
        public float Moonrise { get; set; }
        [JsonProperty("moonset")]
        public float Moonset { get; set; }
        [JsonProperty("temp")]
        public ForecastTemp Temp { get; set; }
        [JsonProperty("feels_like")]
        public ForecastTempFeelsLike FeelsLike { get; set; }
        [JsonProperty("pressure")]
        public float Pressure { get; set; }
        [JsonProperty("humidity")]
        public float Humidity { get; set; }
        [JsonProperty("wind_speed")]
        public float WindSpeed { get; set; }
        [JsonProperty("clouds")]
        public float Clouds { get; set; }
        // pop = probability of precipitation
        [JsonProperty("pop")]
        public float Pop { get; set; }
        [JsonProperty("weather")]
        public List<Weather> Weather { get; set; }
    }

    public class ForecastTemp
    {
        [JsonProperty("day")]
        public float Day { get; set; }
        [JsonProperty("min")]
        public float Minimum { get; set; }
        [JsonProperty("max")]
        public float Maximum { get; set; }
        [JsonProperty("night")]
        public float Night { get; set; }
        [JsonProperty("eve")]
        public float Evening { get; set; }
        [JsonProperty("morn")]
        public float Morning { get; set; }
    }

    public class ForecastTempFeelsLike
    {
        [JsonProperty("day")]
        public float Day { get; set; }
        [JsonProperty("night")]
        public float Night { get; set; }
        [JsonProperty("eve")]
        public float Evening { get; set; }
        [JsonProperty("morn")]
        public float Morning { get; set; }
    }
}
