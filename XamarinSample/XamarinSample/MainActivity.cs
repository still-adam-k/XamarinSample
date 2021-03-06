﻿using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

namespace XamarinSample
{
    [Activity(Label = "XamarinSample", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        int count = 1;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

             // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            // Get our button from the layout resource,
            // and attach an event to it
            
            var newButton = FindViewById<Button>(Resource.Id.button1);
            newButton.Click += delegate { ToCalculator(); };

            FindViewById<Button>(Resource.Id.btnWebview)
                .Click += delegate { StartActivity(typeof (WebviewActivity)); };

            FindViewById<Button>(Resource.Id.goToHttpClient)
                .Click += delegate { StartActivity(typeof(ExternalRestActivity)); };

            FindViewById<Button>(Resource.Id.goToLocation)
                .Click += delegate { StartActivity(typeof(LocationActivity)); };

            FindViewById<Button>(Resource.Id.goToContactList)
                .Click += delegate { StartActivity(typeof(ContactListAcitvity)); };

            FindViewById<Button>(Resource.Id.goToAddContact)
                .Click += delegate { StartActivity(typeof(AddContactActivity)); };

            FindViewById<Button>(Resource.Id.goToCamera)
                .Click += delegate { StartActivity(typeof(TakePhotoActivity)); };

            FindViewById<Button>(Resource.Id.goToMaps)
                .Click += delegate { ToMaps(); };
        }

        protected void ToCalculator()
        {
            var calculatorActivity = new Intent(this, typeof(NextActivity));
            StartActivity(calculatorActivity);
        }

        protected void ToMaps()
        {
            var geoUri = Android.Net.Uri.Parse("geo:52.2313123,21.0053084");
            //var geoUri = Android.Net.Uri.Parse("geo:0,0?q=Palace+of+culture+and+science+plac+Defilad+1+warszawa");
            var mapIntent = new Intent(Intent.ActionView, geoUri);
            StartActivity(mapIntent);
        }
    }
}

