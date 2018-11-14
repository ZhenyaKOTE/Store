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
                var AllCategories = await DBContext.StoreManager.GetItemsAsync<Category>() ?? null;

                if (AllCategories == null)
                    return new OperationDetails(false, "Category is not found", "");
                else
                {
                    Category category = AllCategories.FirstOrDefault(x => x.Name == CategoryName);
                    Product ProductDAL = new Product
                    {
                        Name = item.Name,
                        PhotoPath = item.PhotoPath,
                        Description = item.Description,
                        Price = item.Price,
                    };

                    foreach (CharacteristicDTO CDTO in item.Characteristics)
                    {
                        ProductDAL.Characteristics.Add(new Characteristic { Key = CDTO.Key, Value = CDTO.Value });
                    }

                    category.Products.Add(ProductDAL);
                    await DBContext.SaveAsync();
                }
            }
            return new OperationDetails(true, "The product has been successfully added", "");
        }

        public async Task<IList<CategoryDTO>> GetCategories()
        {
            return await Task.Run(async () =>
            {
                var a = (await DBContext.StoreManager.GetItemsAsync<Category>()).ToList();
                List<CategoryDTO> list = new List<CategoryDTO>();
                foreach (Category DalCategory in a)
                {
                    list.Add(new CategoryDTO { Id = DalCategory.Id, Name = DalCategory.Name });
                }
                return list ?? null;
            });

        }

        public async Task<PageInfoDTO> GetProductsByCategory(string CategoryName, int Page)
        {
            return await Task.Run(async () =>
            {

                int BeginCountProduct = 12 * Page;
                int EndCountProduct = BeginCountProduct + 12;
                List<ProductDTO> list = new List<ProductDTO>();

                var ListDalProducts = await Task.Run(async () =>
                {
                    Category category = (await DBContext.StoreManager.GetItemsAsync<Category>() as List<Category>)
                         .FirstOrDefault(x => x.Name == CategoryName);

                    if (category == null)
                        return null;
                    else
                        return category.Products ?? null;
                });

                if (ListDalProducts != null)
                {
                    for (int i = BeginCountProduct; (i < EndCountProduct && i < ListDalProducts.Count); i++)
                    {
                        list.Add(new ProductDTO
                        {
                            Price = ListDalProducts[i].Price,
                            Description = ListDalProducts[i].Description,
                            Name = ListDalProducts[i].Name,
                            PhotoPath = ListDalProducts[i].PhotoPath,
                            Id = ListDalProducts[i].Id
                        });
                    }

                    PageInfoDTO pageInfoDTO = new PageInfoDTO();

                    pageInfoDTO.Products = list ?? null;
                    int PageCount = (ListDalProducts.Count / 12);


                    if ((double)(ListDalProducts.Count / 12.0) > PageCount)
                        PageCount += 1;

                    pageInfoDTO.MaxPages = PageCount;

                    return pageInfoDTO;
                }
                else
                    return null;
            });

        }

        public void Dispose()
        {
            DBContext.Dispose();
        }
    }
}

