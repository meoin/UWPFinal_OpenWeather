using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
        public string CitySunrise { get; set; }
        public string CitySunset { get; set; }

        public CityViewModel()
        {
            Cities = new ObservableCollection<CityModel>();

            //Gather text files from the FileRepo retrieval
            DataRepo.GetCitiesFromJSONFile();
            _allCities = DataRepo.AllCities;
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
                    CitySunrise = "";
                    CitySunset = "";
    }
                else
                {
                    if (value.Name != null)
                        CityName = value.Name;
                    if (value.State != null)
                        CityState = value.State;
                    if (value.Country != null)
                        CityCountry = value.Country;
                    CityTime = ((new DateTime(1970, 1, 1, 0, 0, 0, 0)).AddSeconds(value.Time)).ToString();

                    if (value.Sun != null)
                    {
                        CitySunrise = new DateTime(1970, 1, 1, 0, 0, 0, 0).AddSeconds(value.Sun.Sunrise).ToShortTimeString();
                        CitySunset = new DateTime(1970, 1, 1, 0, 0, 0, 0).AddSeconds(value.Sun.Sunset).ToShortTimeString();
                    }

                    if (value.Main != null)
                    {
                        CityTemperature = value.Main.Temp.ToString();
                        CityFeelsLike = value.Main.Feels_Like.ToString();
                        CityTempMin = value.Main.Temp_Min.ToString();
                        CityTempMax = value.Main.Temp_Max.ToString();
                        CityPressure = value.Main.Pressure.ToString();
                        CityHumidity = value.Main.Humidity.ToString();
    }

                    if (value.Wind != null)
                    {
                        CityWindSpeed = value.Wind.Speed.ToString();
                        CityWindDirection = value.Wind.Direction.ToString();
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
    }
}
