using Distance.BLL.ViewModel;
using Distance.DAL;
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
	public partial class Universites : ContentPage
    {
        UniversityViewModel viewModel;
        FirebaseViewModel firebaseViewModel;
        public Universites ()
		{

            InitializeComponent ();
            firebaseViewModel = new FirebaseViewModel();
            BindingContext = viewModel = new UniversityViewModel();

        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
           await firebaseViewModel.SaveUniversity();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (viewModel.Items.Count == 0)
                viewModel.LoadItemsCommand.Execute(null);
        }
       
        private async void deleted(object sender, ItemTappedEventArgs e)
        {            
        


        }

        private void SearchBar_TextChanged(object sender, TextChangedEventArgs e)
        {
            viewModel.Search(SearchBar.Text);
        }
    }
}