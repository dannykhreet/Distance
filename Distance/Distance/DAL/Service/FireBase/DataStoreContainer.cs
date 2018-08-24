using Distance.BLL.Model;
using Distance.DAL.Service.FireBase;
using System;
using System.Collections.Generic;
using System.Text;
[assembly: Xamarin.Forms.Dependency(typeof(DataStoreContainer))]

namespace Distance.DAL.Service.FireBase
{
    public class DataStoreContainer
    {
        private FirebaseAuthService _firebaseAuthService;
        private IDataStore<University> _universityStore;
        private IDataStore<User> _UserStore;


        public DataStoreContainer(FirebaseAuthService firebaseAuthService)
        {
            _firebaseAuthService = firebaseAuthService;
        }

        public IDataStore<University> UniversityStore
        {
            get
            {
                if (_universityStore == null)
                {
                    _universityStore = new FirebaseOfflineDataStore<University>(_firebaseAuthService, "universitys");
                   // _universityStore = new FirebaseDataStore<University>(_firebaseAuthService, "university");
                }

                return _universityStore;
            }
        }
        public IDataStore<User> UserStore
        {
            get
            {
                if (_UserStore == null)
                {
                    _UserStore = new FirebaseDataStore<User>(_firebaseAuthService, "Users");
                }

                return _UserStore;
            }
        }
    }

}
