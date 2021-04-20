using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenWeatherFinal.Models;

namespace OpenWeatherFinal.ViewModels
{
    class ForecastViewModel
    {
        public ObservableCollection<WeekForecastModel> Forecasts { get; set; }
        public List<WeekForecastModel> _allForecasts = new List<WeekForecastModel>();

        public ForecastViewModel()
        {
            Forecasts = new ObservableCollection<WeekForecastModel>();

            // need to find a way to pass the lat and lon variables to this viewmodel to pass here
            DataRepo.GetWeekForecast();
            for() //each week forecast in datarepo.selectedweekforecast
            {
                // create a new weekforecastmodel object with variables from normal forecast model, but modified to be formatted strings
                // and then add to _allForecasts list
            }
        }
    }
}
