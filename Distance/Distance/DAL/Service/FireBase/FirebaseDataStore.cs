﻿using Firebase.Database;
using Firebase.Database.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Distance.DAL.Service.FireBase
{
    public class FirebaseDataStore<T> : IDataStore<T> where T : class
    {

        private readonly ChildQuery _query;

        public FirebaseDataStore(FirebaseAuthService authService, string path)
        {
            FirebaseOptions options = new FirebaseOptions()
            {
                AuthTokenAsyncFactory = async () => await authService.GetFirebaseAuthToken()
            };

            _query = new FirebaseClient(Config.ApiKeys.FirebaseDataBaseApi, options).Child(path);
        }

        public async Task<bool> AddItemAsync(T item)
        {
            try
            {
                await _query
                    .PostAsync(item);
            }
            catch (Exception ex)
            {
                throw ex;
                return false;
            }

            return true;
        }

        public async Task<bool> UpdateItemAsync(string id, T item)
        {
            try
            {
                await _query
                    .Child(id)
                    .PutAsync(item);
            }
            catch (Exception ex)
            {
                return false;
            }

            return true;
        }

        public async Task<bool> DeleteItemAsync(string node)
        {
            try
            {
                await _query
                    .Child(node)
                    .DeleteAsync();
            }
            catch (Exception ex)
            {
                return false;
            }

            return true;
        }

        public async Task<T> GetItemAsync(string id)
        {
            try
            {
                return await _query
                    .Child(id)
                    .OnceSingleAsync<T>();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<IEnumerable<T>> GetItemsAsync(bool forceRefresh = false)
        {
            try
            {
                var firebaseObjects = await _query
                    .OnceAsync<T>();

                return firebaseObjects
                    .Select(x => x.Object);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

    }
}
