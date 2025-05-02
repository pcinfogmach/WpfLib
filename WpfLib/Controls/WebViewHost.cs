using Microsoft.Web.WebView2.WinForms;
using System.Windows;
using System;
using System.Windows.Forms.Integration;
using System.Windows.Input;
using WpfLib.ViewModels;
using static System.Net.WebRequestMethods;
using Microsoft.Web.WebView2.Core;

namespace WpfLib.Controls
{
    public class WebViewHost : WindowsFormsHost
    {
        public static WebView2 WebView { get; set; } = new WebView2 ();

        public static readonly DependencyProperty SourceProperty =
            DependencyProperty.Register(
                nameof(Source),
                typeof(string),
                typeof(WebViewHost),
                new PropertyMetadata(null, OnSourceChanged));

        public string Source
        {
            get => (string)GetValue(SourceProperty);
            set => SetValue(SourceProperty, value);
        }

        private static void OnSourceChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue is string uri)
                WebView.CoreWebView2.Navigate(uri);
        }

        public ICommand GoBackCommand = new RelayCommand(() => WebView.GoBack());
        public ICommand GoBackForward = new RelayCommand(() => WebView.GoForward());
        public ICommand RefreshCommand = new RelayCommand(() => WebView.Refresh());

        public WebViewHost() 
        {
            this.Child = WebView;
            SetCore();
        }

        async void SetCore()
        {
            try
            {
                string tempWebCacheDir = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
                var webView2Environment = await CoreWebView2Environment.CreateAsync(userDataFolder: tempWebCacheDir);
                await WebView.EnsureCoreWebView2Async(webView2Environment);
                WebView.NavigationCompleted += WebView_NavigationCompleted;
                WebView.AllowExternalDrop = false;
            }
            catch {  }
        }

        private void WebView_NavigationCompleted(object sender, CoreWebView2NavigationCompletedEventArgs e)
        {
            WebView.CoreWebView2.Settings.UserAgent = "Mozilla/5.0 (Linux; Android 12; Pixel 6) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/100.0.4896.100 Mobile Safari/537.36";
        }
    }
}
