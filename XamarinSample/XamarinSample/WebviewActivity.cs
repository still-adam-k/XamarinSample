using System;
using System.Collections.Generic;
using System.Text;
using Android.App;
using Android.OS;
using Android.Views;
using Android.Webkit;

namespace XamarinSample
{
    public class HelloWebViewClient : WebViewClient
    {
        public override bool ShouldOverrideUrlLoading(WebView view, string url)
        {
            view.LoadUrl(url);
            return true;
        }
    }

    [Activity(Label = "Web browser page")]
    public class WebviewActivity : Activity
    {
        private WebView browser;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            
            SetContentView(Resource.Layout.webview);
            SetupWebviewBrowser();
            StartDefaultPage();
        }

        private void SetupWebviewBrowser()
        {
            browser = FindViewById<WebView>(Resource.Id.webview);
            browser.SetWebViewClient(new HelloWebViewClient());
            browser.Settings.JavaScriptEnabled = true;
        }

        public void StartDefaultPage()
        {
            browser.LoadUrl("http://www.google.pl");
        }

        public override bool OnKeyDown(Keycode keyCode, KeyEvent e)
        {
            if (keyCode == Keycode.Back && browser.CanGoBack())
            {
                browser.GoBack();
                return true;
            }

            return base.OnKeyDown(keyCode, e);
        }
    }
}
