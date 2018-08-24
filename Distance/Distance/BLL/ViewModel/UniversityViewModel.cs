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
using System.Windows.Input;
using Distance.DAL.Config;

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

        public string AdUnitId { get; set; } = "ca-app-pub-3940256099942544/6300978111";


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

        private University selectedUniversity;
        public University SelectedUniversity
        {
            get
            {
                return selectedUniversity;
            }
            set
            {
                selectedUniversity = value;
                OnPropertyChanged();
            }
        }
        private bool isSearch;
        public bool IsSearch
        {
            get
            {
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
                await ExecuteLoadItemsCommand();
                Universities = Items.Where(c => c.arabicName.ToLowerInvariant().Contains(key.ToLowerInvariant())).ToList();
                Suggestions = new ObservableCollection<University>(universities);
                Items.Clear();
                for (int i = 0; i < Suggestions.Count(); i++)
                {
                    Items.Add(Suggestions[i]);
                }
                IsSearch = false;
            }
        }

      
        public UniversityViewModel()
        {
            IsSearch = true;
            if (Device.RuntimePlatform == Device.iOS)
                AdUnitId = "iOS Key";
            else if (Device.RuntimePlatform == Device.Android)
                AdUnitId = "ca-app-pub-3940256099942544/6300978111";
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
