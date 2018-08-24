using Distance.BLL.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;

namespace Distance.BLL.ViewModel
{
   public class FirebaseViewModel
    {
        public ObservableCollection<University> ItemsUniversity { get; set; }
        public ObservableCollection<User> ItemsUser{ get; set; }

        public async Task<bool> SaveUniversity()
        {
            try
            {
                ItemsUniversity = new ObservableCollection<University>();
                University university = new University
                {
                    Id = "11",
                    location = "test",
                    turkeyName = "name test",
                    arabicName = " Mobile test",
                    url = "https://firebasestorage.googleapis.com/v0/b/test-969b8.appspot.com/o/university%2F-LKLvtPGtbbrvpyDTvK_..png?alt=media&token=71892fc2-8e9b-4d68-9e6e-8f52c44ec2f4"

                };
                var _item = university as University;
                ItemsUniversity.Add(_item);
                await App.DataStoreContainer.UniversityStore.AddItemAsync(_item);

                return true;

            }
            catch (Exception ex)
            {
                throw ex;
                return false;
            }
        }

        public async Task<bool> SavedUser( String email , String password , String userName)
        {
            try
            {
                ItemsUser = new ObservableCollection<User>();
                User user = new User
                {   Name = userName,
                    Email = email,
                    Password = password
                  
                };
                var _item = user as User;
                ItemsUser.Add(_item);
                await App.DataStoreContainer.UserStore.AddItemAsync(_item);

                return true;

            }
            catch (Exception ex)
            {
                throw ex;
                return false;
            }
        }

    }
}
