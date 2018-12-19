using Store.DAL.Entities.StoreEntitiesWithFilters;
using Store.DAL.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using TestFilter;

namespace TestFilters
{
    static class Program
    {
        static void Main(string[] args)
        {
            int[] filters = { 6 };
            Filtration filtration = new Filtration(new ApplicationContext());

            var Products = filtration.GetProductsByFilters(filters);
            
            Console.WriteLine(Products.Count);
            Console.Read();
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
                    for (int i = 0; i < FiltersId.Count(); i++)
                    {
                        var idV = fVale.Id;
                        if (FiltersId[i] == idV)
                        {
                            Predicate = Predicate
                                .Or(p => p.Filters
                                .Any(f => f.FilterValueId == idV));
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
            var query = from f in context.VFilterNameGroups.AsQueryable()
                        where f.FilterValueId != null
                        select new
                        {
                            FNameId = f.FilterNameId,
                            FName = f.FilterName,
                            FValueId = f.FilterValueId,
                            FValue = f.FilterValue
                        };
            var groupNames = from f in query
                             group f by new
                             {
                                 Id = f.FNameId,
                                 Name = f.FName
                             } into g
                             orderby g.Key.Name

                             select g;
            List<FNameViewModel> listFilters = new List<FNameViewModel>();
            foreach (var filterName in groupNames)
            {
                FNameViewModel node = new FNameViewModel
                {
                    Id = filterName.Key.Id,
                    Name = filterName.Key.Name
                };
                //node.Childrens = filterName
                //    .GroupBy(f => new FValueViewItem { Id = f.FValueId, Name = f.FValue })
                //    .Select(f => f.Key)
                //    .ToList();
                node.Childrens = (from v in filterName
                                  group v by new FValueViewItem
                                  {
                                      Id = v.FValueId,
                                      Name = v.FValue
                                  } into g
                                  select g.Key).ToList();

                //var a = 

                listFilters.Add(node);
            }
            
            return listFilters;
        }
    }
}
