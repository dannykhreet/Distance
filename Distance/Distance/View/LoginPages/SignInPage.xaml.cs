using Distance.BLL.ViewModel;
using Distance.DAL;
using Distance.DAL.Service.FireBase;
using Distance.Model;
using Plugin.DeviceOrientation;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Distance.View.LoginPages
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class SignInPage : ContentPage
    {
        LoginViewModel loginViewModel;

        public SignInPage ()
        {
            loginViewModel = new LoginViewModel();
            this.BindingContext = loginViewModel;
            

            InitializeComponent();
            // Lock all the pages on android and ios
            CrossDeviceOrientation.Current.LockOrientation(Plugin.DeviceOrientation.Abstractions.DeviceOrientations.Portrait);
        }

        private void SignUpButton_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync( new SignUpPage());
        }
        //زر فتح نافذة جديدة
        private async void LoginButton_Clicked(object sender, EventArgs e)
        {
            var current = Connectivity.NetworkAccess;

            if (current == NetworkAccess.Internet)
            {

                if ((emailValidator.IsValid == true) && (passwordValidator.IsValid == true))
                {
                    SignIn();
                }
                else if(emailValidator.IsValid == false)
                {
                    await App.Current.MainPage.DisplayAlert("تنبية", "الرجاء التحقق من الايميل ", "موافق");

                }
                else if (passwordValidator.IsValid == false)
                {
                    await App.Current.MainPage.DisplayAlert("تنبية", "الرجاء التحقق من كلمة السر  ", "موافق");
                }
            }
            else
            {
              await  App.Current.MainPage.DisplayAlert("تنبية", "الرجاء التحقق من اتصال الانترنت", "موافق");

            }

        }

        private async void SignIn()
        {
            FirebaseAuthService dbFirebase = new FirebaseAuthService();
            bool SignInSucssfully =  await dbFirebase.SignInUser(emailEntry.Text, passwordEntry.Text);

            if (SignInSucssfully)
            {
                await Navigation.PushAsync(new MainMasterDetailPage());
            }
            
            CrossDeviceOrientation.Current.UnlockOrientation();
        }

        private  void ResetPassword(object sender, EventArgs e)
        {
            PopupNavigation.Instance.PushAsync(new PopUp());
        }


        protected override void OnAppearing()
        {
            base.OnAppearing();
            if (Settings.RememberMe != false)
            {
                Navigation.PushAsync(new MainMasterDetailPage());
            }
            
        }

        private void rememberMe_CheckedChanged(object sender, XLabs.EventArgs<bool> e)
        {
            Settings.RememberMe = e.Value;
        }
    }
    
}