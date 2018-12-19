using Store.BLL.DTO;
using Store.BLL.Interfaces;
using Store.DAL.Entities.StoreEntitiesWithFilters;
using Store.DAL.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace Store.BLL.Services
{
    public class StoreService : IStoreService
    {
        private IUnitOfWork DbContext;

        public StoreService(IUnitOfWork UOW)
        {
            DbContext = UOW;
        }

        public IList<CategoryDTO> GetCategories()
        {
            var a = DbContext.StoreManager.GetItems<Category>().ToList();

            List<CategoryDTO> list = new List<CategoryDTO>();

            foreach (Category DalCategory in a)
                list.Add(new CategoryDTO { Id = DalCategory.Id, Name = DalCategory.Name });
            
            return list ?? null;
        }

        public PaginationViewModel GetProductsByCategoryId(PaginationParamsModel PaginationModel) 
        {
            //int[] numbers = { -3, -2, -1, 0, 1, 2, 3 };
            //var result = numbers.Skip(5).Take(12);

            int Skip = (PaginationModel.Page - 1) * PaginationModel.CountViewProducts;

            new PaginationViewModel() {
                Products = 
                DbContext.StoreManager.GetItems<Product>().Where(x => x.CategoryId == PaginationModel.CategoryId).Skip()

            return null;
        }

        public ProductDTO GetProductById(int Id)
        {
            Product dalProduct = DbContext.StoreManager.GetItems<Product>().FirstOrDefault(x => x.Id == Id);
            return new ProductDTO()
            {
                Id = dalProduct.Id,
                Name = dalProduct.Name,
                Price = dalProduct.Price,
                Description = dalProduct.Description
            };
        }
        public void Dispose()
        {
            DbContext.Dispose();
        }
    }
}

