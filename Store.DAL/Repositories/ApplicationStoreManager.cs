using Store.DAL.Entities.StoreEntities;
using Store.DAL.EntityFramework;
using Store.DAL.Interfaces;

namespace Store.DAL.Identity.StoreManagers
{
    public class ApplicationStoreManager : IStoreManager
    {
        private ApplicationContext DbContext;
        public ApplicationStoreManager(ApplicationContext AppDbContext)
        {
            DbContext = AppDbContext;
        }

        public void Create(Product item)
        {
            DbContext.Products.Add(item);
            DbContext.SaveChanges();
        }

        public void Dispose()
        {
            DbContext.Dispose();
        }
    }
}
