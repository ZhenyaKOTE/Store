using Store.BLL.DTO;
using Store.BLL.Interfaces;
using Store.DAL.Entities.StoreEntitiesWithFilters;
using Store.DAL.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace Store.BLL.Services
{
    public class StoreService : IStoreService
    {
        private IUnitOfWork DBContext;

        public StoreService(IUnitOfWork UOW)
        {
            DBContext = UOW;
        }




        public async Task<IList<CategoryDTO>> GetCategories()
        {
            var a = (await DBContext.StoreManager.GetItemsAsync<Category>()).ToList();
            List<CategoryDTO> list = new List<CategoryDTO>();
            foreach (Category DalCategory in a)
            {
                list.Add(new CategoryDTO { Id = DalCategory.Id, Name = DalCategory.Name });
            }
            return list ?? null;
        }

        public async Task<PageInfoDTO> GetProductsByCategory(string CategoryName) //???
        {
            return null;
        }

        public void Dispose()
        {
            DBContext.Dispose();
        }

        public async Task<ProductDTO> GetProductById(int Id)
        {
            return await Task.Run(() =>
             {
                 Product dalProduct = DBContext.StoreManager.GetItems<Product>().FirstOrDefault(x => x.Id == Id);
                 return new ProductDTO()
                 {
                     Id = dalProduct.Id,
                     Name = dalProduct.Name,
                     Price = dalProduct.Price,
                     Description = dalProduct.Description
                 };
             });
        }
    }
}

