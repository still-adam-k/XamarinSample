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
using Cirrious.MvvmCross.Droid.Views;
using Sample.Core;
using Sample.Core.ViewModel;

namespace XamarinSample
{
    [Activity(Label = "NewActivity")]
    public class NextActivity : MvxActivity
    {
        protected override void OnViewModelSet()
        {
            SetContentView(Resource.Layout.Next);
        }

        public new CalculatorViewModel ViewModel
        {
            get { return (CalculatorViewModel)base.ViewModel; }
            set { base.ViewModel = value; }
        }
    }
}