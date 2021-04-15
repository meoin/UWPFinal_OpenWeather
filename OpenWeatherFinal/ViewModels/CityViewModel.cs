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
        public event PropertyChangedEventHandler PropertyChanged;
        public ObservableCollection<CityModel> Cities { get; set; }
        public List<CityModel> _allCities = new List<CityModel>();
        private CityModel _selectedCity;
        private string _filter;

        public string CityName { get; set; }
        public string CityTemperature { get; set; }

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
                }
                else
                {
                    CityName = value.Name;
                    if (value.Main != null) CityTemperature = value.Main.Temp.ToString();
                }
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("CityName"));
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("CityTemperature"));
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
