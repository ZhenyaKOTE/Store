using Store.DAL.Entities.StoreEntities;
using Store.DAL.EntityFramework;
using Store.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Store.DAL.Identity.StoreManagers
{
    public class ApplicationStoreManager : IStoreManager
    {
        private ApplicationContext DbContext;

        //public Task<IList<Category>> Categories
        //{
        //    get { return GetCategoriesAsync(); }
        //}

        public async Task<IList<Category>> GetCategoriesAsync()
        {
            return (await Task.Run(() => { return DbContext.Categories; }) as IList<Category>) ;
        }

        public ApplicationStoreManager(ApplicationContext AppDbContext)
        {
            DbContext = AppDbContext;
        }

        public async Task CreateAsync(Product item)
        {
            await Task.Run(() =>
            {
                DbContext.Products.Add(item);
                DbContext.SaveChanges();
            });
        }

        public async Task CreateAsync(Category item)
        {
            await Task.Run(() => 
            {
                DbContext.Categories.Add(item);
                DbContext.SaveChanges();
            });
        }

        public async Task<Category> GetCategoryByName(string CategoryName)
        {
            return await Task.Run(() =>
            {
                return DbContext.Categories.FirstOrDefault(x => x.Name == CategoryName);
            });
        }

        public void Dispose()
        {
            DbContext.Dispose();
        }
    }
}
