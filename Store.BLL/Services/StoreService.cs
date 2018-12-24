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


        //if null comes, then return all categories
        //else return selected categories by ID
        public IEnumerable<PreViewCategoryDTO> GetCategories(int?[] CategoriesId = null) 
        {
            List<PreViewCategoryDTO> CategoriesDTO = new List<PreViewCategoryDTO>();

            if (CategoriesId == null || CategoriesId.Count() == 0)
            {
                foreach (Category DalCategory in DbContext.StoreManager
                    .GetItems<Category>())
                {
                    CategoriesDTO.Add(new PreViewCategoryDTO { Id = DalCategory.Id, Name = DalCategory.Name });
                }
            }
            else
            {
                foreach (int CategoryId in CategoriesId)
                {
                    
                   var dbCategory = DbContext.StoreManager
                        .GetItems<Category>()
                        .FirstOrDefault(dbCategoryId => dbCategoryId.Id == CategoryId);

                    CategoriesDTO.Add(new PreViewCategoryDTO
                    {
                        Id = dbCategory.Id,
                        Name = dbCategory.Name
                    });
                }
            }

            return CategoriesDTO ?? null;
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

