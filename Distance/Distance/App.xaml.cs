using Distance.View.LoginPages;
using Distance.View;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using DLToolkit.Forms.Controls;
using Distance.DAL.Service.FireBase;
using Distance.Model;

[assembly: XamlCompilation (XamlCompilationOptions.Compile)]
namespace Distance
{
	public partial class App : Application
    {
        public static DataStoreContainer DataStoreContainer { get; private set; }

        public App ()
		{
			InitializeComponent();
            // for flow list view
            FlowListView.Init();
            DataStoreContainer = new DataStoreContainer(new FirebaseAuthService());

            if (Settings.RememberMe != false)
            {
                MainPage = new NavigationPage(new MainMasterDetailPage());
            }
            else
            {
                MainPage = new NavigationPage(new SignInPage());
            }
		}

		protected override void OnStart ()
		{
			// Handle when your app starts
		}

		protected override void OnSleep ()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume ()
		{
			// Handle when your app resumes
		}
	}
}
