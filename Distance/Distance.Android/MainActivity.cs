using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Plugin.CurrentActivity;
using Plugin.DeviceOrientation;
using Android.Content.Res;
using Android.Gms.Common;
using Plugin.FirebasePushNotification;
using Android.Content;
using Android.Gms.Ads;

namespace Distance.Droid
{
    [Activity(Label = "Distance", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(bundle);
            // for FFImageLoading
            FFImageLoading.Forms.Platform.CachedImageRenderer.Init(enableFastRenderer: true);

            // for popup
            Rg.Plugins.Popup.Popup.Init(this, bundle);

            // Step 1: Orientation
            CrossCurrentActivity.Current.Activity = this;
            // 

            // for admob "YOUR ANDROID APP ID HERE"
            MobileAds.Initialize(ApplicationContext, "ca-app-pub-5963831393043908~7484791244");
            global::Xamarin.Forms.Forms.Init(this, bundle);
            try
            {
            LoadApplication(new App());

            }
            catch (Exception e)
            {

                throw e;
            }

            CheckForGoogleServices();

            FirebasePushNotificationManager.ProcessIntent(this, Intent);
        }

        protected override void OnNewIntent(Intent intent)
        {
            base.OnNewIntent(intent);
            FirebasePushNotificationManager.ProcessIntent(this, intent);
        }

        // Check if the Google Play Services is available to recieve Push Notification from Firebase
        public bool CheckForGoogleServices()
        {
            int resultCode = GoogleApiAvailability.Instance.IsGooglePlayServicesAvailable(this);
            if (resultCode != ConnectionResult.Success)
            {
                if (GoogleApiAvailability.Instance.IsUserResolvableError(resultCode))
                {
                    Toast.MakeText(this, GoogleApiAvailability.Instance.GetErrorString(resultCode), ToastLength.Long);
                }
                else
                {
                    Toast.MakeText(this, "This device does not support Google Play Services", ToastLength.Long);
                }
                return false;
            }
            return true;
        }


        // Step 2:
        public override void OnConfigurationChanged(Configuration newConfig)
        {
            base.OnConfigurationChanged(newConfig);

            DeviceOrientationImplementation.NotifyOrientationChange(newConfig.Orientation);
        }
    }
}

