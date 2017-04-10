using System;
using System.Diagnostics;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using Weather.Common;
using Weather.Common.Interfaces;
using Weather.Data.DTO;
using System.Net;

namespace Weather
{
    public class MainViewModel : ViewModelBase
    {
        #region Constants

        private const string ErrorMessageForWeather = "Can't update Weather info. No internet connection! Please connect to internet using wifi or mobile internet";
        private const string ErrorTitle = "Error!";

        #endregion
        
        #region Fields

        private IWeatherProvider weatherProvider;
        private IGeoService geoService;
        private IDialogService dialogService;

        private IWeatherInfo weatherInfo;
        private string weatherImageUrl;
        private string weatherDescription;

        #endregion

        #region Constructors

        public MainViewModel(IWeatherProvider weatherProvider, IGeoService geoService, IDialogService dialogService)
        {
            this.weatherProvider = weatherProvider;
            this.geoService = geoService;
            this.dialogService = dialogService;
        }

        #endregion

        #region Properties

        public string WeatherDescription
        {
            get
            {
                return weatherDescription;
            }

            set
            {
                weatherDescription = value;
                RaisePropertyChanged();
            }
        }

        public string WeatherImageUrl
        {
            get
            {
                return weatherImageUrl;
            }

            set
            {
                if (WeatherImageUrl == value)
                {
                    return;
                }
                
                weatherImageUrl = value;
                RaisePropertyChanged();
            }
        }

        public string Temperature
        {
            get
            {
                return weatherInfo == null ? CommonStrings.DefaultTemperature : $"{weatherInfo.MinTemperature:+0;-0;0}℃ -  {weatherInfo.MaxTemperature:+0;-0;0}℃";
            }
        }

        public int RefreshInterval { get; set; } = 2000;

        #endregion

        #region Public Methods

        public async void StartUpdatingWeather()
        {
            while (true)
            {
                try
                {
                    var location = geoService.ResolveLocation();
                    ProvideWeatherInfo(await weatherProvider.GetWeatherInfoAsync(location.Longtitude, location.Latitude));
                    await Task.Delay(RefreshInterval);
                }
                catch (WebException)
                {
                    await dialogService.ShowErrorAsync(ErrorMessageForWeather, ErrorTitle);
                    await Task.Delay(RefreshInterval);
                }
                catch (Exception ex)
                {
                    await dialogService.ShowErrorAsync(ex.Message, ErrorTitle);
                    return;
                }
            }
        }

        #endregion

        #region Private methods

        private void ProvideWeatherInfo(IWeatherInfo weatherInfo)
        {
            this.weatherInfo = weatherInfo;
            WeatherDescription = weatherInfo.WeatherDescription;
            WeatherImageUrl = weatherInfo.WeatherImageUrl;
            RaisePropertyChanged(nameof(Temperature));
        }

        #endregion
    }
}