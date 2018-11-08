using Store.DAL.Entities.StoreEntities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Store.DAL.Interfaces
{
    public interface IStoreManager :IDisposable
    {
        
        Task CreateAsync<T>(T item) where T : class;
        Task<IList<T>> GetAsync<T>() where T: class;
        Task<IList<T>> GetItemsAsync<T>() where T : class;


        //void Create(Characteristic item);
        //Task<Category> GetCategoryByName(string CategoryName);
        //Task<Category> GetItemByName(string name);
        //Task CreateAsync(Product item);
    }
}