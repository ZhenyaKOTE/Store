//using Store.DAL.Entities.StoreEntities;
using Store.DAL.Context;
using Store.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Store.DAL.Identity.StoreManagers
{
    public class ApplicationStoreManager : IStoreManager
    {
        private ApplicationContext DbContext;
        public ApplicationStoreManager(ApplicationContext AppDbContext)
        {
            DbContext = AppDbContext;
        }

        public void Create<T>(T item) where T : class
        {
            DbContext.Set<T>().Add(item);
            DbContext.SaveChanges();
        }

        public IList<T> GetItems<T>() where T : class
        {
            return DbContext.Set<T>().ToList() ?? null;
        }

        public void Dispose()
        {
            DbContext.Dispose();
        }


    }
}
