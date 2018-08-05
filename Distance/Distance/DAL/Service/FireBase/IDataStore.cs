using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Distance.DAL.Service.FireBase
{
    //https://github.com/cabauman/firebase-database-dotnet/tree/xamarin_forms_sample/samples/XamarinForms
    public interface IDataStore<T>
    {
        Task<bool> AddItemAsync(T item);
        Task<bool> UpdateItemAsync(string id, T item);
        Task<bool> DeleteItemAsync(string node);
        Task<T> GetItemAsync(string id);
        Task<IEnumerable<T>> GetItemsAsync(bool forceRefresh = false);

    }
}

