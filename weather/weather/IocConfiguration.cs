using System;
using TinyIoC;
using Weather.Common.Interfaces;
using Weather.Data.Services;
using Weather.Services;
using Weather.Common;

namespace Weather
{
    public class IocConfiguration
    {
        public IocConfiguration()
        {
            RegisterDependencies();
            RegisterViewModels();
        }

        private void RegisterViewModels()
        {
            TinyIoCContainer.Current.Register<MainViewModel>();
        }

        private void RegisterDependencies()
        {
            TinyIoCContainer.Current.Register<IWeatherProvider, WeatherProvider>();
            TinyIoCContainer.Current.Register<IGeoService, GeoService>();
            TinyIoCContainer.Current.Register<IDialogService, DialogService>();
        }

        public TinyIoCContainer Container
        {
            get
            {
                return TinyIoCContainer.Current;
            }
        }
    }
}