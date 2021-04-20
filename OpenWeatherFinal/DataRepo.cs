using OpenWeatherFinal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.IO;
using System.Diagnostics;
using System.Net.Http;

namespace OpenWeatherFinal
{
    public class DataRepo
    {
        public static List<CityModel> AllCities { get; set; }
        public static CityModel SelectedCity { get; set; }
        public static WeekForecast SelectedWeekForecast { get; set; }

        static DataRepo()
        {
            AllCities = new List<CityModel>();
        }

        public static void GetCitiesFromJSONFile()
        {
            AllCities.Clear();

            using (StreamReader r = new StreamReader(@"city.list.json"))
            {
                string json = r.ReadToEnd();
                List<CityModel> c = JsonConvert.DeserializeObject<List<CityModel>>(json);

                AllCities = c;

                //Checking for time parameter. None found yet!
                //foreach(CityModel city in AllCities)
                //{
                //    if(city.Time != 0)
                //    {
                //        Debug.WriteLine("Time found! City: " + city.Name);
                //    }
                //}
            }
        }

        public async static Task GetCityInfo(float id)
        {
            string key = "404b530fa2d3f5cb9a4f858b89d6c4d8";
            string call = $"http://api.openweathermap.org/data/2.5/weather?id={id}&appid={key}&units=metric";

            HttpClient client = new HttpClient();
            string response = await client.GetStringAsync(call);

            try
            {
                CityModel data = JsonConvert.DeserializeObject<CityModel>(response);
                SelectedCity = data;
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
            }
        }

        public async static Task GetWeekForecast(float lat, float lon)
        {
            string key = "404b530fa2d3f5cb9a4f858b89d6c4d8";
            string call = $"http://api.openweathermap.org/data/2.5/onecall?lat={lat}&lon={lon}&exclude=hourly,current,minutely&appid={key}&units=metric";

            HttpClient client = new HttpClient();

            string response = await client.GetStringAsync(call);

            JsonSerializer js = new JsonSerializer();

            try
            {
                WeekForecast data = JsonConvert.DeserializeObject<WeekForecast>(response);
                SelectedWeekForecast = data;
                /*Debug.WriteLine(data.Days[0].Temp.Morning);
                Debug.WriteLine(data.Days[1].Weather[0].Main);*/
            } catch (Exception e)
            {
                Debug.WriteLine(e.Message);
            }
        }
    }
}
