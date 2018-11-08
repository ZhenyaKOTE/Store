using Store.DAL.Entities.StoreEntities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Store.DAL.Interfaces
{
    public interface IStoreManager :IDisposable
    {
        Task CreateAsync(Product item);
        Task CreateAsync(Category item);
        //void Create(Characteristic item);
        Task<IList<Category>> GetCategoriesAsync();
        Task<Category> GetCategoryByName(string CategoryName);
    }
}