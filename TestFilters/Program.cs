using Store.DAL.Entities.StoreEntitiesWithFilters;
using Store.DAL.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TestFilter;

namespace TestFilters
{
    static class Program
    {
        static void Main(string[] args)
        {
            Console.InputEncoding = Encoding.Unicode;
            Console.OutputEncoding = Encoding.Unicode;

            ApplicationContext context = new ApplicationContext();
            {
                //TestWorkInitDatabasePrint(context);

                var listFilters = GetFilters(context);
                foreach (var fName in listFilters)
                {
                    Console.WriteLine("{0} - {1}", fName.Id, fName.Name);
                    foreach (var fValue in fName.Childrens)
                    {
                        Console.WriteLine("\t{0} - {1}", fValue.Id, fValue.Name);
                    }
                }

                //Масив фільтрів, які потрібно примінити до продуктів
                int[] fValsSearch = { 6, 7, 2 };
                var query = context
                    .Products
                    //.Include()
                    .AsQueryable();
                foreach (var fName in listFilters)
                {
                    int count = 0; //Кількість співпадніть у групі фільтрів
                    var predicate = PredicateBuilder.False<Product>();
                    foreach (var fVale in fName.Childrens)
                    {
                        for (int i = 0; i < fValsSearch.Count(); i++)
                        {
                            var idV = fVale.Id;
                            if (fValsSearch[i] == idV)
                            {
                                predicate = predicate
                                    .Or(p => p.Filters
                                    .Any(f => f.FilterValueId == idV));
                                count++;
                            }
                        }
                    }
                    if (count != 0)
                        query = query.Where(predicate);
                }
                var resultSearch = query.Select(p => new
                {
                    Id = p.Id,
                    Name = p.Name,
                    Price = p.Price
                });
                foreach (var p in resultSearch)
                {
                    Console.WriteLine($"{p.Id} - {p.Name} - {p.Price}");
                }

            }
        }
        static void TestWorkInitDatabasePrint(ApplicationContext context)
        {
            Console.WriteLine("Кількість назв фільтрів {0}",
                   context.FilterNames.Count());
            Console.WriteLine("Кількість значень фільрів {0}",
                context.FilterValues.Count());
            Console.WriteLine("Кількість категорій {0}",
                context.Categories.Count());
            Console.WriteLine("Кількість продуктів {0}",
                context.Categories.Count());
            Console.WriteLine("Кількість по групах фільрів назв {0}",
                context.FilterNameGroups.Count());
            Console.WriteLine("Кількість фільрів по продуктах {0}",
                context.Filters.Count());
        }
        static List<FNameViewModel> GetFilters(ApplicationContext context)
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
                node.Childrens = (from v in filterName
                                  group v by new FValueViewItem
                                  {
                                      Id = v.FValueId,
                                      Name = v.FValue
                                  } into g
                                  select g.Key).ToList();
                listFilters.Add(node);
            }
            return listFilters;
        }

    }
}
