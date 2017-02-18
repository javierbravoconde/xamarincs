using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Webkit;
using Android.Widget;
using Android.OS;
using App9.Views;
using App9.Models;
using Java.Interop;
using Android.Webkit;

namespace App9
{
    [Activity(Label = "App9", MainLauncher = true)]
    public class MainActivity : Activity
    {

        [Export]
        [JavascriptInterface]
        public void processEvent(string eventStr)
        {
           
        }

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            var webView = FindViewById<WebView>(Resource.Id.webView);
            webView.Settings.JavaScriptEnabled = true;

            // Use subclassed WebViewClient to intercept hybrid native calls
            webView.SetWebChromeClient(new WebChromeClient());
            webView.Settings.AllowUniversalAccessFromFileURLs = true; //allows javascript to load local files

            webView.AddJavascriptInterface(this, "CSharp");
            WebView.SetWebContentsDebuggingEnabled(true);
            // Load the rendered HTML into the view with a base URL 
            // that points to the root of the bundled Assets folder
            webView.LoadUrl("file:///android_asset/index.html");

        }

        private class HybridWebViewClient : WebViewClient
        {
            public override bool ShouldOverrideUrlLoading(WebView webView, string url)
            {

                // If the URL is not our own custom scheme, just let the webView load the URL as usual
                var scheme = "hybrid:";

                if (!url.StartsWith(scheme))
                    return false;

                // This handler will treat everything between the protocol and "?"
                // as the method name.  The querystring has all of the parameters.
                var resources = url.Substring(scheme.Length).Split('?');
                var method = resources[0];
                var parameters = System.Web.HttpUtility.ParseQueryString(resources[1]);

                if (method == "UpdateLabel")
                {
                    var textbox = parameters["textbox"];

                    // Add some text to our string here so that we know something
                    // happened on the native part of the round trip.
                    var prepended = string.Format("C# says \"{0}\"", textbox);

                    // Build some javascript using the C#-modified result
                    var js = string.Format("SetLabelText('{0}');", prepended);

                    webView.LoadUrl("javascript:" + js);
                }

                return true;
            }
        }
    }
}

