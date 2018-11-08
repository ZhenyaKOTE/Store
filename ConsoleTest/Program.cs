using Store.DAL.Entities.StoreEntities;
using Store.DAL.Interfaces;
using Store.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleTest
{
    class Program
    {
        static void Main(string[] args)
        {

            IUnitOfWork unit = new IdentityUnitOfWork("entityFramework");
            var a = unit.StoreManager.GetCategoryByName("Блять");
            Console.WriteLine(a.Name);
            //Category category = new Category();
            //category.Name = "Блять";

            //unit.SaveAsync();

            //Category category = new Category();
            //category.Name = "Шини";

            //Product product = new Product();
            //product.Name = "Bridgestone Blizzak REVO GZ 185/60 R15 84S";
            //product.Price = 1520.00;

            //Characteristic c = new Characteristic();
            //c.Key = "Ширина шины";
            //c.Value = "185";

            //Characteristic c1 = new Characteristic();
            //c1.Key = "Профиль";
            //c1.Value = "60";

            //Characteristic c2 = new Characteristic();
            //c2.Key = "Сензон";
            //c2.Value = "Зимние";

            //product.Characteristics.Add(c);
            //product.Characteristics.Add(c1);
            //product.Characteristics.Add(c2);

            //category.Products.Add(product);
            //unit.StoreManager.Create(category);
            //unit.SaveAsync();
        }
    }
}
