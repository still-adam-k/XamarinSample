using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Sample.Core;

namespace XamarinSample
{
    [Activity(Label = "NewActivity")]
    public class NextActivity : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            SetContentView(Resource.Layout.Next);
            
            var calculator = new Calculator();
            var s1 = Intent.GetIntExtra("no1", 1);
            var s2 = Intent.GetIntExtra("no2", 1);
            var sum = calculator.Add(s1, s2);

            var label = FindViewById<TextView>(Resource.Id.textView1);
            label.Text = "The sum of " + s1 + " and " + s2 + " is " + sum;

            base.OnCreate(bundle);
        }
    }
}