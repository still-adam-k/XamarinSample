using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace XamarinSample
{
     [Activity(Label = "Read from internet")]
    public class ExternalRestActivity : Activity
    {
        private string testUrl = "http://date.jsontest.com";
        private HttpClient httpClient;
        private TextView resultLabel;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.HttpClient);

            httpClient = new HttpClient();
            resultLabel = FindViewById<TextView>(Resource.Id.response);

            FindViewById<Button>(Resource.Id.btnGetDateFromRest).Click
                += delegate { GetInformationFromWebservice(); };
        }

        public async void GetInformationFromWebservice()
        {
            var httpresponse = await httpClient.GetStringAsync(testUrl);

            resultLabel.Text = httpresponse;
        }
    }
}