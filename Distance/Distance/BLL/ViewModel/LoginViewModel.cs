using System;
using Xamarin.Forms;
using System.Collections.ObjectModel;
using Distance.BLL.Model;
using System.Diagnostics;
using System.Threading.Tasks;
using Distance.DAL.Service.FireBase;

namespace Distance.BLL.ViewModel
{
    public class LoginViewModel : ViewModelBase
    {
         FirebaseAuthService firebaseAuthService;
        public ObservableCollection<University> Items { get; set; }
        public Command LoadItemsCommand { get; set; }
        public Command LoadItemsCommandRefresh { get; set; }
    
        public LoginViewModel()
        {
            Items = new ObservableCollection<University>();
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());
        }

        async Task ExecuteLoadItemsCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                Items.Clear();
                var items = await App.DataStoreContainer.UniversityStore.GetItemsAsync(true);
                foreach (var item in items)
                {
                    Items.Add(item);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }

    }
}