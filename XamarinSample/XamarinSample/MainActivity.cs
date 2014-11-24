using System;
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
            Button button = FindViewById<Button>(Resource.Id.MyButton);

            button.Click += delegate { button.Text = string.Format("{0} clicks!", count++); };

            var newButton = FindViewById<Button>(Resource.Id.button1);
            newButton.Click += delegate { PassNumbersToCalculator(); };
        }

        protected void PassNumbersToCalculator()
        {
            var no1 = Int32.Parse(FindViewById<EditText>(Resource.Id.no1).Text);
            var no2 = Int32.Parse(FindViewById<EditText>(Resource.Id.no2).Text);

            var calculatorActivity = new Intent(this, typeof(NextActivity));
            calculatorActivity.PutExtra("no1", no1);
            calculatorActivity.PutExtra("no2", no2);

            StartActivity(calculatorActivity);
        }
    }
}

