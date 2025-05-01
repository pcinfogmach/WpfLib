using Microsoft.Web.WebView2.WinForms;
using System.Windows.Forms.Integration;
using System.Windows.Input;
using WpfLib.ViewModels;

namespace WpfLib.Controls
{
    public class WebViewHost : WindowsFormsHost
    {
        public static WebView2 WebView { get; } = new WebView2();

        public ICommand GoBackCommand = new RelayCommand(() => WebView.GoBack());
        public ICommand GoBackForward = new RelayCommand(() => WebView.GoForward());
        public ICommand RefreshCommand = new RelayCommand(() => WebView.Refresh());

        public WebViewHost() 
        {
            this.Child = WebView;
        }
    }
}
