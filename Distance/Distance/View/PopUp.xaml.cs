using Distance.DAL;
using Distance.DAL.Service.FireBase;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Distance.View
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class PopUp
	{
		public PopUp ()
		{
			InitializeComponent ();
		}

        private  void ResetPassword(object sender, EventArgs e)
        {
            FirebaseAuthService dbFirebase = new FirebaseAuthService();
             dbFirebase.RestPasswordByEmail(email.Text);
            PopupNavigation.Instance.PopAsync(true);
        }
    }
}