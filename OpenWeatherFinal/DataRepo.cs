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

                /*Debug.WriteLine(AllCities[0].Name);
                Debug.WriteLine(AllCities[0].Country);
                Debug.WriteLine(AllCities[0].Coords.Lat);*/
            }
        }

        public async static Task GetCityInfo(float id)
        {
            string key = "404b530fa2d3f5cb9a4f858b89d6c4d8";
            string call = $"http://api.openweathermap.org/data/2.5/weather?id={id}&appid={key}&units=metric";

            HttpClient client = new HttpClient();

            string response = await client.GetStringAsync(call);

            JsonSerializer js = new JsonSerializer();

            CityModel data = JsonConvert.DeserializeObject<CityModel>(response);

            SelectedCity = data;

            Debug.WriteLine(SelectedCity.Name);
            Debug.WriteLine(SelectedCity.Main.Temp);
        }
    }
}
