using System;
using Android.App;
using Android.Locations;
using Android.Net;
using Android.OS;
using Android.Runtime;
using Android.Widget;
using GalaSoft.MvvmLight.Helpers;
using Square.Picasso;
using Android.Content;

namespace Weather
{
    [Activity(Label = "Weather", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        private IocConfiguration configuration;
        
        public MainViewModel ViewModel
        {
            get;
            private set;
        }

        public TextView WeatherDescriptionTextView { get; private set; }
        public TextView TemperatureTextView { get; private set; }
        public ImageView WeatherImageView { get; private set; }

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            CurrentActivity = this;
            SetContentView(Resource.Layout.Main);
            configuration = new IocConfiguration();
            ViewModel = configuration.Container.Resolve<MainViewModel>();
            ResolveControls();
            PerformBindings();
            ViewModel.StartUpdatingWeather();
        }

        private void ResolveControls()
        {
            WeatherDescriptionTextView = FindViewById<TextView>(Resource.Id.DescriptionTextView);
            TemperatureTextView = FindViewById<TextView>(Resource.Id.TemperatureView);
            WeatherImageView = FindViewById<ImageView>(Resource.Id.WeatherImageView);
        }

        private void PerformBindings()
        {
            ViewModel.PropertyChanged += OnPropertyChanged;
            this.SetBinding(
                () => ViewModel.Temperature,
                () => TemperatureTextView.Text);

            this.SetBinding(
                () => ViewModel.WeatherDescription,
                () => WeatherDescriptionTextView.Text);
        }

        private void OnPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(ViewModel.WeatherImageUrl))
            {
                Picasso.With(this).Load(ViewModel.WeatherImageUrl).Into(WeatherImageView);
            }
        }

        public static Context CurrentActivity
        {
            get;
            private set;
        }
    }
}

