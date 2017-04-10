using System;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.Locations;
using Android.OS;
using Android.Runtime;
using Weather.Common.Interfaces;
using Weather.Common;

namespace Weather.Services
{
    public class GeoService : Java.Lang.Object, IGeoService, ILocationListener
    {
        LocationManager locationManager;
        Location currentLocation;

        public GeoService()
        {
            locationManager = (LocationManager)Application.Context.GetSystemService(Context.LocationService);
            
            string provider1 = locationManager.GetBestProvider(
            new Criteria
            {
                Accuracy = Accuracy.Fine
            }, 
            true);

            string provider2 = LocationManager.GpsProvider;

            if (locationManager.IsProviderEnabled(provider1))
            {
                locationManager.RequestLocationUpdates(provider1, 1000, 1, this);
                currentLocation = locationManager.GetLastKnownLocation(provider1);
            }

            if (locationManager.IsProviderEnabled(provider2))
            {
                locationManager.RequestLocationUpdates(provider2, 1000, 1, this);
                currentLocation = locationManager.GetLastKnownLocation(provider2);
            }
        }

        public DoubleLocation ResolveLocation()
        {
            return new DoubleLocation
            {
                Longtitude = currentLocation.Longitude,
                Latitude = currentLocation.Latitude
            };
        }

        public void OnLocationChanged(Location location)
        {
            currentLocation = location;
        }

        public void OnProviderDisabled(string provider)
        {

        }

        public void OnProviderEnabled(string provider)
        {

        }

        public void OnStatusChanged(string provider, [GeneratedEnum] Availability status, Bundle extras)
        {

        }
    }
}