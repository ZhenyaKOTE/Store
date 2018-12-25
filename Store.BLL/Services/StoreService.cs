using DAL.Migrations.Views.Filters;
using Store.BLL.DTO;
using Store.BLL.Infrastructure;
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

        public IEnumerable<PreViewCategoryDTO> Get(int[] CategoriesId = null) 
        {
            List<PreViewCategoryDTO> CategoriesDTO = new List<PreViewCategoryDTO>();

            if (CategoriesId == null || CategoriesId.Count() == 0)
            {
                foreach (Category DalCategory in DbContext.StoreManager
                    .GetItems<Category>().ToList())
                {
                    CategoriesDTO.Add(new PreViewCategoryDTO { Id = DalCategory.Id, Name = DalCategory.Name });
                }
            }
            else // Тeed to refactor
            {
                foreach (int CategoryId in CategoriesId)
                {
                    
                   var dbCategory = DbContext.StoreManager
                        .GetItems<Category>()
                        //.FirstOrDefault(dbCategoryId => dbCategoryId.Id == CategoryId);
                        .First(x => x.Id == CategoryId);

                    CategoriesDTO.Add(new PreViewCategoryDTO
                    {
                        Id = dbCategory.Id,
                        Name = dbCategory.Name
                    });
                }
            }

            return CategoriesDTO ?? null;
        }

        public ProductDTO Get(int Id)
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


        private ICollection<FilterDTO> ConvertorFilters(ICollection<Filter> DAL_Filters)
        {
            List<FilterDTO> list = new List<FilterDTO>();

            foreach (Filter f in DAL_Filters)
            {
                var a = new FilterDTO
                { FilterNameId = f.FilterNameId, FilterNameOf = f.FilterNameOf, FilterValueId =  }
            }
        }

        public IEnumerable<ProductDTO> Filter(int[] FiltersId) //Доробити з фотографіями
        {
            var FilterList = GetFilters(); //Отримати всі фільтри (Query)
            var query = DbContext.StoreManager.GetItems<Product>().AsQueryable();

            foreach (FNameViewModel fName in FilterList)
            {
                int Count_FilterGroup = 0; //Кількість співпадніть у групі фільтрів
                var Predicate = PredicateBuilder.False<Product>();
                foreach (var fVale in fName.Childrens)
                {
                    foreach (var FilterId in FiltersId)
                    {
                        var ValueId = fVale.Id;
                        if (FilterId == ValueId)
                        {
                            Predicate = Predicate
                                .Or(p => p.Filters
                                .Any(filter => filter.FilterValueId == ValueId));

                            Count_FilterGroup++;
                        }
                    }
                }
                if (Count_FilterGroup != 0)
                    query = query.Where(Predicate);
            }

            var FilteredProducts = query.Select(fProducts => new ProductDTO
            {
                Id = fProducts.Id,
                Name = fProducts.Name,
                Price = fProducts.Price,
                Description = fProducts.Description,
                DateCreate = fProducts.DateCreate,
                Quantity = fProducts.Quantity,

                Filters = ConvertorFilters(fProducts.Filters)
                
            }).ToArray();


            return FilteredProducts;
        }

        private IEnumerable<FNameViewModel> GetFilters()
        {
            //var query = from filter in context.VFilterNameGroups.AsQueryable()
            //            where filter.FilterValueId != null
            //            select new
            //            {
            //                FNameId = filter.FilterNameId,
            //                FName = filter.FilterName,
            //                FValueId = filter.FilterValueId,
            //                FValue = filter.FilterValue
            //            };

            var query = DbContext.StoreManager
                .GetItems<VFilterNameGroup>()
                .AsQueryable()
                .Where(filter => filter.FilterValueId != null)
                .Select(filter => new
                {
                    FNameId = filter.FilterNameId,
                    FName = filter.FilterName,
                    FValueId = filter.FilterValueId,
                    FValue = filter.FilterValue
                });

            //var groupNames = from filter in query
            //                 group filter by new
            //                 {
            //                     Id = filter.FNameId,
            //                     Name = filter.FName
            //                 } into g
            //                 orderby g.Key.Name
            //                 select g;


            var groupNames = query
                .GroupBy(filter => (new { Id = filter.FNameId, Name = filter.FName }))
                .OrderBy(x => x.Key.Name);

            List<FNameViewModel> FilterList = new List<FNameViewModel>();

            foreach (var filterName in groupNames)
            {
                FNameViewModel node = new FNameViewModel
                {
                    Id = filterName.Key.Id,
                    Name = filterName.Key.Name
                };

                node.Childrens = filterName
                    .GroupBy(f => new FValueViewItem { Id = f.FValueId, Name = f.FValue })
                    .Select(f => f.Key)
                    .ToList();

                FilterList.Add(node);
            }

            return FilterList;
        }

        public void Dispose()
        {
            DbContext.Dispose();
        }
    }
}

