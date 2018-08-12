using Distance.BLL.ViewModel;
using Distance.DAL.Service.FireBase;
using Distance.Interface;
using Distance.Model;
using Distance.View.LoginPages;
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
    public partial class MainMasterDetailPage : MasterDetailPage
    {
        FirebaseAuthService firebaseAuthService;
        MainMasterDetailViewModel mainMasterDetailViewModel;

        // custom width master page 
        public readonly static BindableProperty WidthRatioProperty =
                   BindableProperty.Create("WidthRatio",
                   typeof(float),
                   typeof(MainMasterDetailPage),
                   (float)0.2);

        public float WidthRatio
        {
            get
            {
                return (float)GetValue(WidthRatioProperty);
            }
            set
            {
                SetValue(WidthRatioProperty, value);
            }
        }
        public MainMasterDetailPage()
        {
            NavigationPage.SetHasNavigationBar(this, false);
            firebaseAuthService = new  FirebaseAuthService();
            InitializeComponent();
            WidthRatio = (float)0.3;


            Detail = new NavigationPage(new Universites());
            IsPresented = false;
            mainMasterDetailViewModel = new MainMasterDetailViewModel();
            BindingContext = mainMasterDetailViewModel;
        }


        private void signout_Activated(object sender, EventArgs e)
        {
            Settings.RememberMe = false;
            firebaseAuthService.DeleteFirebaseAuth();
            Navigation.PushAsync(new SignInPage());
        }


        private void Univsersity_Clicked(object sender, EventArgs e)
        {
            Detail = new NavigationPage(new Universites());
            IsPresented = false;
        }
        private void AboutUs_Clicked(object sender, EventArgs e)
        {
            Detail = new NavigationPage(new AboutUs());
            IsPresented = false;
        }
        protected  override bool OnBackButtonPressed()
        {
            if (mainMasterDetailViewModel._canClose)
            {
                mainMasterDetailViewModel.ShowExitDialog();
            }
            return mainMasterDetailViewModel._canClose;
        }

        
    }
}