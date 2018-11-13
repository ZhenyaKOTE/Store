using Store.BLL.DTO;
using Store.BLL.Infrastructure;
using Store.BLL.Interfaces;
using Store.DAL.Entities.StoreEntities;
using Store.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
            //Debug.Write(UOW.ToString() + "Alax akbar \n\n\n\n\n\n");
            DBContext = UOW;
        }
        
        public async Task<bool> Exists(CategoryDTO item)
        {
            Category result = (await DBContext.StoreManager.GetItemsAsync<Category>() as List<Category>)
                .FirstOrDefault(x => x.Name == item.Name);

            if (result != null)
                return true;
            else
                return false;
        }

        public async Task<bool> Exists(ProductDTO item)
        {
            Product result = (await DBContext.StoreManager.GetItemsAsync<Product>() as List<Product>)
                .FirstOrDefault(x => x.Name == item.Name);

            if (result != null)
                return true;
            else
                return false;
        }

        public async Task CreateAsync(CategoryDTO item)
        {
            await DBContext.StoreManager.CreateAsync(new Category { Name = item.Name });
        }

        public async Task<OperationDetails> AddProductToCategory(ProductDTO item, string CategoryName)
        {

            if (item == null || (CategoryName == null || CategoryName == ""))
                return new OperationDetails(false, "Some models are Empty", "");
            else
            {

                Category category = (await DBContext.StoreManager.GetItemsAsync<Category>()).ToArray().
                    FirstOrDefault(x => x.Name == CategoryName) ?? null;

                if (category == null)
                {

                    return new OperationDetails(false, "Category is not found", "");
                }
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

        public async Task<IList<string>> GetCategoryNames()
        {
            return await Task.Run(async () =>
            {
                List<string> NamesList = new List<string>();
                var temp = (await DBContext.StoreManager.GetItemsAsync<Category>()).ToList<Category>();
                //Debug.Write(temp.Count + "\n\n\n\n\n\n\n");
                foreach (Category category in temp)
                {
                    NamesList.Add(category.Name);
                }
                return NamesList;
            });
        }

        public void Dispose()
        {
            DBContext.Dispose();
        }

        public async Task<IList<ProductDTO>> GetProductsByPosition(int beginCount, int endCount)
        {
            return await Task.Run(async () =>
            {
                List<ProductDTO> list = new List<ProductDTO>();
                var listDAL = await DBContext.StoreManager.GetItemsAsync<Product>();
                for (int i = beginCount; i < endCount; i++)
                {
                    list.Add(new ProductDTO
                    {
                        Price = listDAL[i].Price,
                        Description = listDAL[i].Description,
                        Name = listDAL[i].Name,
                        PhotoPath = listDAL[i].PhotoPath,
                        Id = listDAL[i].Id
                    });
                }
                return list;
            });
        }
    }
}
