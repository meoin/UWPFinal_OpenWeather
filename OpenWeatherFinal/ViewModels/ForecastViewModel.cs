using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenWeatherFinal.Models;

namespace OpenWeatherFinal.ViewModels
{
    public class ForecastViewModel
    {
        public ObservableCollection<WeekForecastModel> Forecasts { get; set; }

        public ForecastViewModel()
        {
            Forecasts = new ObservableCollection<WeekForecastModel>();
        }

        public async Task LoadForecasts(float lat, float lon)
        {
            await DataRepo.GetWeekForecast(lat, lon);
            foreach (Forecast day in DataRepo.SelectedWeekForecast.Days) //each week forecast in datarepo.selectedweekforecast
            {
                WeekForecastModel forecast = new WeekForecastModel(day.Date, day.Sunrise, day.Sunset, day.Temp.Maximum, day.Temp.Minimum,
                    day.Pressure, day.Humidity, day.Clouds, day.Pop, day.Weather[0].Description);
                Forecasts.Add(forecast);
                //Debug.WriteLine(forecast.Date);
            }
        }
    }
}
