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
                    Location = "locat te",
                    Name = "name test",
                    Url = "https://firebasestorage.googleapis.com/v0/b/test-969b8.appspot.com/o/download%20(1).jpeg?alt=media&token=7c0948e8-38b5-433c-bf72-0e525748f26f"

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
