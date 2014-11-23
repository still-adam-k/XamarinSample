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

namespace XamarinSample
{
    [Activity(Label = "NewActivity")]
    public class NextActivity : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            SetContentView(Resource.Layout.Next);

            base.OnCreate(bundle);
            // Create your application here
        }
    }
}