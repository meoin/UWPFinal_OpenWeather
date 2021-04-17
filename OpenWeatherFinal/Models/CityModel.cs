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
        [JsonProperty("sunrise")]
        public int Sunrise { get; set; }
        [JsonProperty("sunset")]
        public int Sunset { get; set; }
        [JsonProperty("dt")]
        public int Time { get; set; }

        [JsonProperty("coord")]
        public Coordinates Coords { get; set; }

        [JsonProperty("weather")]
        public List<Weather> Weather { get; set; }

        [JsonProperty("main")]
        public ClimateInfo Main { get; set; }

        [JsonProperty("wind")]
        public WindInfo Wind { get; set; }

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
}
