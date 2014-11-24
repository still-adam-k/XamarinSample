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
        }

        protected void ToCalculator()
        {
            var calculatorActivity = new Intent(this, typeof(NextActivity));
            StartActivity(calculatorActivity);
        }
    }
}

