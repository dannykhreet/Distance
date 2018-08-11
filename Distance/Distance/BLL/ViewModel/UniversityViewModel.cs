using System;
using System.Collections.Generic;
using System.Text;
using System;
using Xamarin.Forms;
using System.Collections.ObjectModel;
using Distance.BLL.Model;
using System.Diagnostics;
using System.Threading.Tasks;
using Distance.DAL.Service.FireBase;
using System.Linq;

namespace Distance.BLL.ViewModel
{
   public class UniversityViewModel : ViewModelBase
    {
        FirebaseAuthService firebaseAuthService;
        private ObservableCollection <University> items;
        public ObservableCollection<University> Items
        {
            get { return items; }
            set
            {
                items = value;
                OnPropertyChanged();
            }
        }

        public Command LoadItemsCommand { get; set; }
      
        private ObservableCollection<University> suggestions;
        public ObservableCollection<University> Suggestions
        {
            get { return suggestions; }
            set { suggestions = value;
                OnPropertyChanged();
            }
        }

        private List<University> universities;
        public List<University> Universities
        {
            get { return universities; }
            set
            {
                universities = value;
                OnPropertyChanged();
            }
        }

        private bool isSearch;
        public bool IsSearch
        {
            get {
                    return isSearch;
                }
            set
            {
                isSearch = value;
                OnPropertyChanged();
            }
        }
        public async void Search( String key)
        {

            if (string.IsNullOrEmpty(key))
            {
                // Show all Univsersity 
               await ExecuteLoadItemsCommand();
                IsSearch = true;
            }
            else if (key.Length >=1)
            {
                IsSearch = false;
                await ExecuteLoadItemsCommand();
                Universities = Items.Where(c => c.Name.ToLowerInvariant().Contains(key.ToLowerInvariant())).ToList();
                Suggestions = new ObservableCollection<University>(universities);
                Items.Clear();
                for (int i = 0; i < Suggestions.Count(); i++)
                {
                    Items.Add(Suggestions[i]);
                }
            }
        }

      
        public UniversityViewModel()
        {
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());
            Items = new ObservableCollection<University>();
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
