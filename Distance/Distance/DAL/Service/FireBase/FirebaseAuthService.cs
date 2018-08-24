using Distance.DAL.Config;
using Distance.Model;
using Firebase.Auth;
using Newtonsoft.Json;
using System;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Xamarin.Forms;
using Distance.BLL.Model;
namespace Distance.DAL.Service.FireBase
{
    public class FirebaseAuthService
    {
        private FirebaseAuthLink _authLink;
        private bool _didInit;
        public FirebaseAuthProvider authProvider;
        const string emailRegex = @"^(?("")("".+?(?<!\\)""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
           @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))$";

        public FirebaseAuthService()
        {
            authProvider = new FirebaseAuthProvider(new FirebaseConfig(Config.ApiKeys.FirebaseApi));

        }
        private void InitFirebaseAuth()
        {
            if (string.IsNullOrEmpty(ApiKeys.FirebaseApi))
            {
                throw new Exception("The Firebase API key is empty. Make sure to set it according to your project.");
            }

            // First, check if our auth object/token is stored locally.
            FirebaseAuth auth = LoadFirebaseAuth();
            if (auth != null)
            {
                _authLink = new FirebaseAuthLink(authProvider, auth);

            }
           

            // Save the auth object/token every time it's refreshed.
            _authLink.FirebaseAuthRefreshed += (s, e) => SaveFirebaseAuth(e.FirebaseAuth);
        }

        public async Task<string> GetFirebaseAuthToken()
        {
            if (!_didInit)
            {
                 InitFirebaseAuth();
                _didInit = true;
            }

            // This will refresh the auth object/token if it's expired.
            _authLink = await _authLink.GetFreshAuthAsync();

            return _authLink.FirebaseToken;
        }

        private async Task Login()
        {
            //var facebookAccessToken = "<login with facebook and get oauth access token>";
            //_authLink = await authProvider.SignInWithOAuthAsync(FirebaseAuthType.Facebook, facebookAccessToken);
            // _authLink = SignInUser();
            // Enable anonymous authentication in your Firebase panel.
           // _authLink = await authProvider.SignInAnonymouslyAsync();

        }

        public async Task<bool> SignInUser(string email, string password)
        {
            try
            {
                _authLink = await authProvider.SignInWithEmailAndPasswordAsync(email, password);
                String token = _authLink.FirebaseToken;
                SaveFirebaseAuth(_authLink);
                // token = Settings.FirebaseAuthJson;
                Firebase.Auth.User user = await authProvider.GetUserAsync(token);
                if (user.IsEmailVerified == false)
                {
                    ResendEmailVerification(_authLink.FirebaseToken);
                    String massage = String.Format("الحساب غير مفعل سوف نعيد ارسال رابط التفعيل الا الايميل ",
                         email);
                    await App.Current.MainPage.DisplayAlert("تأكيد", massage, "موافق");
                }

                return user.IsEmailVerified;
            }
            catch (FirebaseAuthException ee)
            {
                switch (ee.Reason)
                {

                    case AuthErrorReason.WrongPassword:
                        await App.Current.MainPage.DisplayAlert("خطأ", " كلمة السر غير صحيحة", "موافق");
                        break;

                    case AuthErrorReason.UnknownEmailAddress:
                        await App.Current.MainPage.DisplayAlert("خطأ", " الايميل غير مستخدم", "موافق");

                        break;

                }
                return false;
            }
            catch (System.Exception e)
            {
                return false;

                throw e;
            }

        }
        public void ResendEmailVerification(string firebasetoken)
        {
            var y = authProvider.SendEmailVerificationAsync(firebasetoken);

        }
        public async Task<bool> SignUpUser(string email, string password)
        {
            try
            {
                _authLink = await authProvider.CreateUserWithEmailAndPasswordAsync(email, password, "", true);
                SaveFirebaseAuth(_authLink);
                return true;
            }
            catch (FirebaseAuthException ex)
            {

                if (ex.Reason == AuthErrorReason.EmailExists)
                {

                    await App.Current.MainPage.DisplayAlert("خطأ", "الايميل موجود", "موافق");

                }
                return false;

            }



        }
        public void RestPasswordByEmail(string email)
        {
            try
            {
                if (String.IsNullOrEmpty(email) == false)
                {

                    var IsValid = (Regex.IsMatch(email, emailRegex, RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250)));
                    if (IsValid == true)
                    {
                        var se = authProvider.SendPasswordResetEmailAsync(email);


                    }
                }
                else
                {
                    App.Current.MainPage.DisplayAlert("خطأ", "نسيت ادخال الايميل", "موافق");
                }
            }
            catch (System.Exception e)
            {

                throw e;
            }

        }


        private FirebaseAuth LoadFirebaseAuth()
        {
            string json = Settings.FirebaseAuthJson;
            if (string.IsNullOrEmpty(json))
            {
                return null;
            }
            else
            {
                return JsonConvert.DeserializeObject<FirebaseAuth>(json);
            }
        }


        public void DeleteFirebaseAuth()
        {
            string json = "";
            Settings.FirebaseAuthJson = json;
        }
        private void SaveFirebaseAuth(FirebaseAuth auth)
        {
            string json = JsonConvert.SerializeObject(auth);
            Settings.FirebaseAuthJson = json;
        }
    }

}
