

namespace Store.DAL.EntityFramework
{
    using Store.DAL.Entities.StoreEntitiesWithFilters;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using System.Text;
    using System.Threading.Tasks;

    namespace DAL.Entities
    {
        public class DatabaseInitializerIsExist : CreateDatabaseIfNotExists<ApplicationContext>//DropCreateDatabaseAlways<EFContext>
        {
            protected override void Seed(ApplicationContext context)
            {
                string codeBase = Assembly.GetExecutingAssembly().CodeBase;
                UriBuilder uri = new UriBuilder(codeBase);
                string path = Uri.UnescapeDataString(uri.Path);

                string baseDir = Path.GetDirectoryName(path) + "\\Migrations\\ViewFilters\\vFilterNameGroups.sql";
                context.Database.ExecuteSqlCommand(File.ReadAllText(baseDir));
                #region InitCategory
                context.Categories.AddOrUpdate(
                    h => h.Id,
                    new Category
                    {
                        Id = 1,
                        Name = "Одяг",
                        ParentId = null
                    });
                #endregion

                #region InitFilteName
                context.FilterNames.AddOrUpdate(
                    h => h.Id,
                    new FilterName
                    {
                        Id = 1,
                        Name = "Колір"
                    });
                context.FilterNames.AddOrUpdate(
                    h => h.Id,
                    new FilterName
                    {
                        Id = 2,
                        Name = "Розмір"
                    });
                #endregion

                #region InitFilteValue
                context.FilterValues.AddOrUpdate(
                    h => h.Id,
                    new FilterValue
                    {
                        Id = 1,
                        Name = "L"
                    });
                context.FilterValues.AddOrUpdate(
                    h => h.Id,
                    new FilterValue
                    {
                        Id = 2,
                        Name = "M"
                    });
                context.FilterValues.AddOrUpdate(
                    h => h.Id,
                    new FilterValue
                    {
                        Id = 3,
                        Name = "XL"
                    });
                context.FilterValues.AddOrUpdate(
                    h => h.Id,
                    new FilterValue
                    {
                        Id = 4,
                        Name = "XX"
                    });
                context.FilterValues.AddOrUpdate(
                    h => h.Id,
                    new FilterValue
                    {
                        Id = 5,
                        Name = "Чорний"
                    });
                context.FilterValues.AddOrUpdate(
                    h => h.Id,
                    new FilterValue
                    {
                        Id = 6,
                        Name = "Білий"
                    });
                context.FilterValues.AddOrUpdate(
                    h => h.Id,
                    new FilterValue
                    {
                        Id = 7,
                        Name = "Зелений"
                    });
                context.FilterValues.AddOrUpdate(
                    h => h.Id,
                    new FilterValue
                    {
                        Id = 8,
                        Name = "Жовтий"
                    });
                #endregion

                #region InitProduct
                context.Products.AddOrUpdate(
                    h => h.Id,
                    new Product
                    {
                        Id = 1,
                        CategoryId = 1,
                        Name = "Джинси",
                        Price = 800,
                        Quantity = 5,
                        DateCreate = DateTime.Now
                    });
                context.Products.AddOrUpdate(
                    h => h.Id,
                    new Product
                    {
                        Id = 2,
                        CategoryId = 1,
                        Name = "Брюкі",
                        Price = 140,
                        Quantity = 2,
                        DateCreate = DateTime.Now
                    });
                context.Products.AddOrUpdate(
                    h => h.Id,
                    new Product
                    {
                        Id = 3,
                        CategoryId = 1,
                        Name = "Труси",
                        Price = 1040,
                        Quantity = 30,
                        DateCreate = DateTime.Now
                    });
                context.Products.AddOrUpdate(
                    h => h.Id,
                    new Product
                    {
                        Id = 4,
                        CategoryId = 1,
                        Name = "Майкі",
                        Price = 40,
                        Quantity = 20,
                        DateCreate = DateTime.Now
                    });
                #endregion

                #region InitFilterNameGroup
                context.FilterNameGroups.AddOrUpdate(
                    h => new { h.FilterNameId, h.FilterValueId },
                    new FilterNameGroup
                    {
                        FilterNameId = 1,
                        FilterValueId = 5
                    });
                context.FilterNameGroups.AddOrUpdate(
                    h => new { h.FilterNameId, h.FilterValueId },
                    new FilterNameGroup
                    {
                        FilterNameId = 1,
                        FilterValueId = 6
                    });
                context.FilterNameGroups.AddOrUpdate(
                    h => new { h.FilterNameId, h.FilterValueId },
                    new FilterNameGroup
                    {
                        FilterNameId = 1,
                        FilterValueId = 7
                    });
                context.FilterNameGroups.AddOrUpdate(
                    h => new { h.FilterNameId, h.FilterValueId },
                    new FilterNameGroup
                    {
                        FilterNameId = 1,
                        FilterValueId = 8
                    });
                context.FilterNameGroups.AddOrUpdate(
                    h => new { h.FilterNameId, h.FilterValueId },
                    new FilterNameGroup
                    {
                        FilterNameId = 2,
                        FilterValueId = 1
                    });
                context.FilterNameGroups.AddOrUpdate(
                    h => new { h.FilterNameId, h.FilterValueId },
                    new FilterNameGroup
                    {
                        FilterNameId = 2,
                        FilterValueId = 2
                    });
                context.FilterNameGroups.AddOrUpdate(
                    h => new { h.FilterNameId, h.FilterValueId },
                    new FilterNameGroup
                    {
                        FilterNameId = 2,
                        FilterValueId = 3
                    });
                context.FilterNameGroups.AddOrUpdate(
                    h => new { h.FilterNameId, h.FilterValueId },
                    new FilterNameGroup
                    {
                        FilterNameId = 2,
                        FilterValueId = 4
                    });
                #endregion

                #region InitFilters
                context.Filters.AddOrUpdate(
                    h => new { h.FilterNameId, h.FilterValueId, h.ProductId },
                    new Filter
                    {
                        FilterNameId = 1,
                        FilterValueId = 6,
                        ProductId = 4
                    });
                context.Filters.AddOrUpdate(
                    h => new { h.FilterNameId, h.FilterValueId, h.ProductId },
                    new Filter
                    {
                        FilterNameId = 1,
                        FilterValueId = 7,
                        ProductId = 4
                    });
                context.Filters.AddOrUpdate(
                    h => new { h.FilterNameId, h.FilterValueId, h.ProductId },
                    new Filter
                    {
                        FilterNameId = 2,
                        FilterValueId = 2,
                        ProductId = 4
                    });
                context.Filters.AddOrUpdate(
                    h => new { h.FilterNameId, h.FilterValueId, h.ProductId },
                    new Filter
                    {
                        FilterNameId = 1,
                        FilterValueId = 6,
                        ProductId = 2
                    });
                context.Filters.AddOrUpdate(
                    h => new { h.FilterNameId, h.FilterValueId, h.ProductId },
                    new Filter
                    {
                        FilterNameId = 1,
                        FilterValueId = 7,
                        ProductId = 1
                    });
                #endregion
                base.Seed(context);
            }
        }
    }

}
