using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenWeatherFinal.Models
{
    public class WeekForecastModel
    {
        public string Date { get; set; }
        public string Sunrise { get; set; }
        public string Sunset { get; set; }
        public string TempMax { get; set; }
        public string TempMin { get; set; }
        public string Pressure { get; set; }
        public string Humidity { get; set; }
        public string Clouds { get; set; }
        public string Precipitation { get; set; }
        public string WeatherDescription { get; set; }
        

        public WeekForecastModel(
            float date, float sunrise, float sunset, float tempmax, float tempmin, 
            float pressure, float humidity, float clouds, float prec, string weatherdesc)
        {
            this.Date = ((new DateTime(1970, 1, 1, 0, 0, 0, 0)).AddSeconds(date)).DayOfWeek.ToString() +
                "\n" + ((new DateTime(1970, 1, 1, 0, 0, 0, 0)).AddSeconds(date)).Date.ToString("d");
            this.Sunrise = ((new DateTime(1970, 1, 1, 0, 0, 0, 0)).AddSeconds(sunrise)).TimeOfDay.ToString("t");
            this.Sunset = ((new DateTime(1970, 1, 1, 0, 0, 0, 0)).AddSeconds(sunset)).TimeOfDay.ToString("t");
            this.TempMax = tempmax.ToString("0.0") + "°C";
            this.TempMin = tempmin.ToString("0.0") + "°C";
            this.Pressure = pressure.ToString() + " hPa";
            this.Humidity = humidity.ToString() + "%";
            this.Clouds = clouds.ToString() + "%";
            this.Precipitation = (prec * 100).ToString() + "%";
            char[] desc = weatherdesc.ToCharArray();
            desc[0] = char.ToUpper(desc[0]);
            this.WeatherDescription = new string(desc);
        }
    }

    public class WeekForecast
    {
        [JsonProperty("daily")]
        public List<Forecast> Days { get; set; }

        public IEnumerator GetEnumerator()
        {
            return (IEnumerator)this;
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
