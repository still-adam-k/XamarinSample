using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Locations;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Java.Lang;

namespace XamarinSample
{
    [Activity(Label = "LocationActivity")]
    public class LocationActivity : Activity, ILocationListener
    {
        LocationManager locationManager;
        private TextView locationLabel;


        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.location);

            FindViewById<Button>(Resource.Id.btnCheckLocation).Click += delegate { EnableLocation(); };
            locationLabel = FindViewById<TextView>(Resource.Id.location);
            locationManager = GetSystemService(LocationService) as LocationManager;
            

        }

        public void EnableLocation()
        {
            var locationProvider = LocationManager.GpsProvider;
            if (locationManager.IsProviderEnabled(locationProvider))
                locationManager.RequestLocationUpdates(locationProvider, 1000, 1, this);
            else locationLabel.Text = locationProvider + "is not enabled";
        }

        public void OnLocationChanged(Location location)
        {
            locationLabel.Text = location.ToString();
        }

        public void OnProviderDisabled(string provider)
        {
            locationLabel.Text = "Provider disabled";
        }

        public void OnProviderEnabled(string provider)
        {
            locationLabel.Text = "Provider Enabled";
        }

        public void OnStatusChanged(string provider, Availability status, Bundle extras)
        {
            locationLabel.Text = "Provider status changed";
        }
    }
}