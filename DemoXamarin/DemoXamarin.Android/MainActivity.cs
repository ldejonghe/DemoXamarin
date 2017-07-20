using System;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using DemoXamarin;
using Android.Locations;
using System.Collections.Generic;
using Android.Util;

namespace DemoXamarin.Droid
{
	[Activity (Label = "DemoXamarin.Android", MainLauncher = true, Icon = "@drawable/icon")]
	public class MainActivity : Activity, ILocationListener
	{
        string TAG = "DemoXamarin";
        int count = 1;
        Location _currentLocation;
        LocationManager _locationManager;
        string _locationProvider;

        Button button1;
        Button buttonLocation;
        TextView locationText;


        protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			// Set our view from the "main" layout resource
			SetContentView (Resource.Layout.Main);

            // Get our button from the layout resource,
            // and attach an event to it
            button1 = FindViewById<Button>(Resource.Id.FirstButton);
            buttonLocation = FindViewById<Button>(Resource.Id.SecondButton);
            locationText = FindViewById<TextView>(Resource.Id.location_text);

            locationText.Text = "Location not retrieved yet";
            button1.Click += delegate {
				button1.Text = string.Format ("{0} clicks!",Calculator.add());
			};

            InitializeLocationManager();
            buttonLocation.Click += delegate {
                _locationManager.RemoveUpdates(this);
                _locationManager.RequestLocationUpdates(_locationProvider, 0, 0, this);
                buttonLocation.Text = "Location using (" + _locationProvider + ")";
            };
            

        }


        void InitializeLocationManager()
        {
            _locationManager = (LocationManager)GetSystemService(LocationService);
            Criteria criteriaForLocationService = new Criteria
            {
                Accuracy = Accuracy.Fine
            };
            IList<string> acceptableLocationProviders = _locationManager.GetProviders(criteriaForLocationService, true);

            if (acceptableLocationProviders.Count > 0)
            {
  
                _locationProvider = acceptableLocationProviders[0];

            }
            else
            {
                _locationProvider = string.Empty;
            }
            Log.Debug("", "Using " + _locationProvider + ".");
        }

        public void OnLocationChanged(Location location)
        {
            locationText.Text = location.ToString();
        }

        public void OnProviderDisabled(string provider)
        {
            //throw new NotImplementedException();
            Log.Debug(TAG, "Enter OnProviderDisabled, provider :{0} " + provider);
        }

        public void OnProviderEnabled(string provider)
        {
            Log.Debug(TAG, "Enter OnProviderEnabled, provider :{0} " + provider);
        }

        public void OnStatusChanged(string provider, [GeneratedEnum] Availability status, Bundle extras)
        {
            //throw new NotImplementedException();
            Log.Debug(TAG, "Enter OnStatusChanged, provider :{0} " + provider); 
        }

    }
}


