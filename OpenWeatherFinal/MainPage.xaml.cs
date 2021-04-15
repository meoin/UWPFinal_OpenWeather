using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using OpenWeatherFinal.ViewModels;
using OpenWeatherFinal.Models;
using System.Threading.Tasks;
using System.Diagnostics;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace OpenWeatherFinal
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public CityViewModel CVM { get; set; }
        public MainPage()
        {
            this.InitializeComponent();

            this.CVM = new CityViewModel();
            
        }

        private void Load_Button(object sender, RoutedEventArgs e)
        {

        }

        private void Page_Load(object sender, RoutedEventArgs e)
        {
            
        }

        private void Search_KeyDown(object sender, KeyRoutedEventArgs e)
        {
            if (e.Key == Windows.System.VirtualKey.Enter && FilterTxt.Text.Length >= 4)
            {
                CVM.PerformFiltering();
            }
        }

        private void OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            CityModel selected = CVM.SelectedCity;
            // A city is selected (index is 0 or greater)
            if (FileListView.SelectedIndex >= 0)
            {
                Task.Run(async () =>
                {
                    await DataRepo.GetCityInfo(selected.ID);
                    CVM.SelectedCity = DataRepo.SelectedCity;
                    CVM.CityTemperature = DataRepo.SelectedCity.Main.Temp.ToString();
                    ShowContent();
                }).GetAwaiter();
            }
            // No city is selected (index is -1, indicating no selection)
            else
            {
                
            }
        }

        private void ShowContent()
        {
            /*string info = CVM.SelectedCity.Name;
            WeatherInfo.Text = info;*/
            Debug.WriteLine(CVM.CityName);
            Debug.WriteLine(CVM.CityTemperature);
            CityTemp.Text = CVM.CityTemperature.ToString();
            CVM.Refresh();
        }
    }
}
