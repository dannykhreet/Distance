using Distance.BLL.ViewModel;
using Distance.DAL;
using Distance.DAL.Service.FireBase;
using Plugin.DeviceOrientation;
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
	public partial class SignUpPage : ContentPage
	{
        FirebaseViewModel firebaseViewModel;

        public SignUpPage ()
		{
            
			InitializeComponent ();
            firebaseViewModel = new FirebaseViewModel();
            // Lock all the pages on android and ios
            CrossDeviceOrientation.Current.LockOrientation(Plugin.DeviceOrientation.Abstractions.DeviceOrientations.Portrait);
        }

        private async void SignUpButtonSend_Clicked(object sender, EventArgs e)
        {
            var current = Connectivity.NetworkAccess;

            if (current == NetworkAccess.Internet)
            {

                if(( emailValidator.IsValid == true) &&(passwordValidator.IsValid == true))
                {

                    SignUp();

                }
                else if((emailValidator.IsValid == false))
                {
                    await App.Current.MainPage.DisplayAlert("تنبية", "الرجاء التحقق من الايميل ", "موافق");

                }
                else if (passwordValidator.IsValid == true)
                {
                    await App.Current.MainPage.DisplayAlert("تنبية", "الرجاء التحقق من كلمة السر ", "موافق");
                }
               
            }
            else
            {
                await App.Current.MainPage.DisplayAlert("تنبية", "الرجاء التحقق من اتصال الانترنت", "موافق");

            }

        }
        private async void SignUp()
        {
            FirebaseAuthService dbFirebase = new FirebaseAuthService();
            bool CreatedSucssfully = await dbFirebase.SignUpUser(emailEntry.Text, passwordEntry.Text );
          if (CreatedSucssfully)
            {
                await firebaseViewModel.SavedUser(emailEntry.Text, passwordEntry.Text , userNameEntry.Text);
                String massage = String.Format("رجاء تفقد ايميلك {0} لقد ارسنا لك رابط تفعيل الحساب ",
                         emailEntry.Text);
                await App.Current.MainPage.DisplayAlert("تأكيد", massage, "موافق");
            }
        }
      
    }
}