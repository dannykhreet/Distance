using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Distance.View
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class WebView : ContentPage
	{
        private string Url;

        public WebView ()
		{
			InitializeComponent ();
		}

        public WebView(string url)
        {
            InitializeComponent();
            this.Url = url;
            WebPageUrl.Source = Url;
            var current = Connectivity.NetworkAccess;

            if (current != NetworkAccess.Internet)
            {
                WebPageUrl.IsVisible = false;
                internetMassage.IsVisible = true;
            }
        }
    }
}