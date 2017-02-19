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
        private WebView m_WebView;

        [Export]
        [JavascriptInterface]
        public void processEvent(string eventStr)
        {
            PushEvent("Hello from c#");
        }

        public void PushEvent(string eventStr)
        {
            try
            {
                using (var h = new Handler(Looper.MainLooper))
                {
                    try
                    {
                        h.Post(() =>
                        {
                            //var jsr = new JavascriptResult();
                            var method = string.Format("my.namespace.publicFunc('{0}')", eventStr);
                            m_WebView.EvaluateJavascript(method, null);
                        });
                    }
                    catch (System.Exception ex)
                    {
                        throw;
                    }
                }
            }
            catch (System.Exception ex)
            {
                //MTODO proper logger
            }
        }

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            m_WebView = FindViewById<WebView>(Resource.Id.webView);
            m_WebView.Settings.JavaScriptEnabled = true;

            // Use subclassed WebViewClient to intercept hybrid native calls
            m_WebView.SetWebChromeClient(new WebChromeClient());
            m_WebView.Settings.AllowUniversalAccessFromFileURLs = true; //allows javascript to load local files

            m_WebView.AddJavascriptInterface(this, "CSharp");
            WebView.SetWebContentsDebuggingEnabled(true);
            // Load the rendered HTML into the view with a base URL 
            // that points to the root of the bundled Assets folder
            m_WebView.LoadUrl("file:///android_asset/index.html");

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

