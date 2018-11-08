using Store.BLL.DTO;
using Store.BLL.Infrastructure;
using Store.BLL.Interfaces;
using Store.DAL.Entities.StoreEntities;
using Store.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

        public async CreateCategory()
        {

        }

        public async Task<OperationDetails> AddProductToCategory(ProductDTO item, string CategoryName)
        {

            if (item == null || CategoryName == null || CategoryName == "")
                return new OperationDetails(false, "Some models are Empty", "");
            else
            {

                Category category = await DBContext.StoreManager.GetCategoryByName(CategoryName) ?? null;

                if (category == null)
                    return new OperationDetails(false, "Category is not found", "");
                else
                {

                    Product ProductDAL = new Product
                    {
                        Name = item.Name,
                        Description = item.Description,
                        Price = item.Price,
                    };

                    foreach (CharacteristicDTO CDTO in item.Characteristics)
                    {
                        ProductDAL.Characteristics.Add(new Characteristic { Key = CDTO.Key, Value = CDTO.Value });
                    }

                    category.Products.Add(ProductDAL);
                }
            }
            return new OperationDetails(true, "The product has been successfully added", "");
        }

        public void Dispose()
        {
            DBContext.Dispose();
        }
    }
}
