using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenWeatherFinal.Models;

namespace OpenWeatherFinal.ViewModels
{
    public class CityViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged = delegate { };
        public ObservableCollection<CityModel> Cities { get; set; }
        public List<CityModel> _allCities = new List<CityModel>();
        private CityModel _selectedCity;
        private string _filter;
        private TimeSpan offset;

        public string CityName { get; set; }
        public string CityTemperature { get; set; }
        public string CityState { get; set; }
        public string CityCountry { get; set; }
        public string CityFeelsLike { get; set; }
        public string CityTempMin { get; set; }
        public string CityTempMax { get; set; }
        public string CityPressure { get; set; }
        public string CityHumidity { get; set; }
        public string CityWindSpeed { get; set; }
        public string CityWindDirection { get; set; }
        public string CityTime { get; set; }
        public string UpdateTime { get; set; }
        public string CitySunrise { get; set; }
        public string CitySunset { get; set; }

        public CityViewModel()
        {
            Cities = new ObservableCollection<CityModel>();

            //Gather text files from the FileRepo retrieval
            DataRepo.GetCitiesFromJSONFile();
            _allCities = DataRepo.AllCities;
            offset = TimeZoneInfo.Local.GetUtcOffset(DateTime.UtcNow);
        }

        public CityModel SelectedCity
        {
            get { return _selectedCity; }
            set 
            { 
                _selectedCity = value;
                if (value == null)
                {
                    CityName = "";
                    CityTemperature = "";
                    CityState = "";
                    CityCountry = "";
                    CityTime = "";
                    CitySunrise = "";
                    CitySunset = "";
                    CityFeelsLike = "";
                    CityTempMin = "";
                    CityTempMax = "";
                    CityPressure = "";
                    CityHumidity = "";
                    CityWindSpeed = "";
                    CityWindDirection = "";
                    UpdateTime = "";
                }
                else
                {
                    if (value.Name != null)
                        CityName = value.Name;
                    if (value.State != null)
                        CityState = value.State;
                    if (value.Country != null)
                        CityCountry = value.Country;
                    UpdateTime = ((new DateTime(1970, 1, 1, 0, 0, 0, 0)).AddSeconds(value.Time + offset.TotalSeconds)).ToString();
                    CityTime = (DateTime.UtcNow.AddSeconds(value.Timezone)).ToShortTimeString();

                    if (value.Sun != null)
                    {
                        CitySunrise = new DateTime(1970, 1, 1, 0, 0, 0, 0).AddSeconds(value.Sun.Sunrise + value.Timezone).ToShortTimeString();
                        CitySunset = new DateTime(1970, 1, 1, 0, 0, 0, 0).AddSeconds(value.Sun.Sunset + value.Timezone).ToShortTimeString();
                    }

                    if (value.Main != null)
                    {
                        CityTemperature = roundTemp(value.Main.Temp);
                        CityFeelsLike = "Feels like " + roundTemp(value.Main.Feels_Like);
                        CityTempMin = roundTemp(value.Main.Temp_Min);
                        CityTempMax = roundTemp(value.Main.Temp_Max);
                        CityPressure = value.Main.Pressure.ToString() + " hPa";
                        CityHumidity = value.Main.Humidity.ToString() + "%";
                    }

                    if (value.Wind != null)
                    {
                        CityWindSpeed = value.Wind.Speed.ToString() + " m/s";

                        if (value.Wind.Direction > 348.75 && value.Wind.Direction < 11.25)//convert from degrees to cardinal direction
                            CityWindDirection = "N";
                        else if (value.Wind.Direction >= 11.25 && value.Wind.Direction < 33.75)
                            CityWindDirection = "NNE";
                        else if (value.Wind.Direction >= 33.75 && value.Wind.Direction <= 56.25)
                            CityWindDirection = "NE";
                        else if (value.Wind.Direction > 56.25 && value.Wind.Direction <= 78.75)
                            CityWindDirection = "ENE";
                        else if (value.Wind.Direction > 78.75 && value.Wind.Direction < 101.25)
                            CityWindDirection = "E";
                        else if (value.Wind.Direction >= 101.25 && value.Wind.Direction < 123.75)
                            CityWindDirection = "ESE";
                        else if (value.Wind.Direction >= 123.75 && value.Wind.Direction <= 146.25)
                            CityWindDirection = "SE";
                        else if (value.Wind.Direction > 146.25 && value.Wind.Direction <= 168.75)
                            CityWindDirection = "SSE";
                        else if (value.Wind.Direction > 168.75 && value.Wind.Direction < 191.25)
                            CityWindDirection = "S";
                        else if (value.Wind.Direction >= 191.25 && value.Wind.Direction < 213.75)
                            CityWindDirection = "SSW";
                        else if (value.Wind.Direction >= 213.75 && value.Wind.Direction <= 236.25)
                            CityWindDirection = "SW";
                        else if (value.Wind.Direction > 236.25 && value.Wind.Direction <= 258.75)
                            CityWindDirection = "WSW";
                        else if (value.Wind.Direction > 258.75 && value.Wind.Direction < 281.25)
                            CityWindDirection = "W";
                        else if (value.Wind.Direction >= 281.25 && value.Wind.Direction < 303.75)
                            CityWindDirection = "WNW";
                        else if (value.Wind.Direction >= 303.75 && value.Wind.Direction <= 326.75)
                            CityWindDirection = "NW";
                        else
                            CityWindDirection = "NNW";
                    }

                }
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("CityName"));
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("CityTemperature"));
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("CityState"));
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("CityCountry"));
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("CityFeelsLike"));
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("CityTempMin"));
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("CityTempMax"));
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("CityPressure"));
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("CityHumidity"));
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("CityWindSpeed"));
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("CityWindDirection"));
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("CityTime"));
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("CitySunrise"));
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("CitySunset"));
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("CityTimezone"));
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("UpdateTime"));
            }
        }

        public void Refresh()
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("CityName"));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("CityTemperature"));
        }

        public string Filter
        {
            get { return _filter; }
            set
            {
                if (value == _filter) { return; }
                _filter = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Filter)));
            }
        }

        public void PerformFiltering()
        {
            if (_filter == null)
            {
                _filter = "";
            }

            _allCities = DataRepo.AllCities;

            //Lower-case and trim string
            var lowerCaseFilter = Filter.ToLowerInvariant().Trim();

            //Use LINQ query to get all personmodel names that match filter text, as a list
            var result = _allCities.Where(tf => tf.Name.ToLowerInvariant().Contains(lowerCaseFilter)).ToList();
            var resultCount = result.Count;

            var toRemove = new List<CityModel>();
            if (Cities.Count > 0)
            {
                toRemove = Cities.Except(result).ToList();
            }

            //Loop to remove items that fail filter
            foreach (var x in toRemove)
            {
                Cities.Remove(x);
            }

            // Add back in correct order.
            for (int i = 0; i < resultCount; i++)
            {
                var resultItem = result[i];
                if (i + 1 > Cities.Count || !Cities[i].Equals(resultItem))
                {
                    Cities.Insert(i, resultItem);
                }
            }
        }

        public string roundTemp(float temp)
        {
            int roundedTemp = (int)Math.Round(temp);
            string roundedTempStr = roundedTemp.ToString();
            if (roundedTempStr.Length == 1)
            {
                roundedTempStr = "0" + roundedTempStr + "°C";
            }
            else roundedTempStr = roundedTempStr + "°C";

            return roundedTempStr;
        }
    }
}
