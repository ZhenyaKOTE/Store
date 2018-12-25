using Store.DAL.Entities.StoreEntitiesWithFilters;
using Store.DAL.Context;
using System;
using System.Collections.Generic;

using TestFilter;
using Store.BLL.Interfaces;
using Store.BLL.Services;
using Store.DAL.Repositories;
using System.Threading.Tasks;
using System.Linq;

namespace TestFilters
{
    static class Program
    {
        static IStoreService storeService = new StoreService(new UnitOfWork("StoreContext"));
        static void Main(string[] args)
        {
            int[] numbers = { -3, -2, -1, 0, 1, 2, 3 };
            var result = numbers.Skip(5).Take(12);

            foreach (int i in result)
                Console.WriteLine(i);

        }
        public static void GetNames()
        {
           // Task.Run(async () =>
           // {
           //    var a = await storeService.GetCategories();
           //    foreach (var k in a)
           //    {
           //        Console.WriteLine(k.Name);
           //    }
           //});
        }
        public static void DO()
        {
            //int[] filters = { };
            //Filtration filtration = new Filtration(new ApplicationContext());

            //var Products = filtration.GetProductsByFilters(filters);

            //foreach (var a in Products)
            //{
            //    Console.WriteLine(a.Name + "   " + a.Id);
            //}
            //Console.Read();
        }

    }

    public class Filtration
    {
        private ApplicationContext context;

        public Filtration(ApplicationContext Db)
        {
            context = Db;
        }

        public IList<ProductViewModel> GetProductsByFilters(int[] FiltersId)
        {

            var FilterList = GetFilters(context); //Отримати всі фільтри (Query)
            var query = context.Products.AsQueryable();

            foreach (FNameViewModel fName in FilterList)
            {
                int Count_FilterGroup = 0; //Кількість співпадніть у групі фільтрів
                var Predicate = PredicateBuilder.False<Product>();
                foreach (var fVale in fName.Childrens)
                {
                    //for (int i = 0; i < FiltersId.Count(); i++)
                    //{
                    //    var idV = fVale.Id;
                    //    if (FiltersId[i] == idV)
                    //    {
                    //        Predicate = Predicate
                    //            .Or(p => p.Filters
                    //            .Any(f => f.FilterValueId == idV));
                    //        Count_FilterGroup++;
                    //    }
                    //}
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

            var FilteredProducts = query.Select(fProducts => new ProductViewModel
            {
                Id = fProducts.Id,
                Name = fProducts.Name,
                Price = fProducts.Price
            }).ToList();


            return FilteredProducts;
        }

        private List<FNameViewModel> GetFilters(ApplicationContext context)
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

            var query = context.VFilterNameGroups
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
    }
}
