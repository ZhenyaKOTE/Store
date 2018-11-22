using Microsoft.AspNet.Identity.EntityFramework;
using Store.DAL.Entities;
using Store.DAL.Entities.StoreEntities;
using System;
using System.Collections.Generic;
using System.Data.Entity;

namespace Store.DAL.EntityFramework
{
    internal class ContextInitializer : DropCreateDatabaseAlways<ApplicationContext>
    {
        protected override void Seed(ApplicationContext db)
        {

           

            Category TireCategory = new Category();
            TireCategory.Name = "Шини";


            Category DiskCategory = new Category();
            DiskCategory.Name = "Диски";

            //db.Set<Category>().Add(TireCategory);
            //db.Set<Category>().Add(DiskCategory);
            

            Random rand = new Random();
            for (int i = 0; i < 87; i++)
            {

                Product product = new Product();
                product.Name = "Premiorri ViaMaggiore " + i.ToString(); 
                product.PhotoPath = "~/Images/shiny.png";
                product.Price = rand.Next(500, 5000);
                
                Characteristic characteristic1 = new Characteristic();
                characteristic1.Key = "Ширина";
                characteristic1.Value = "195";

                Characteristic characteristic2 = new Characteristic();
                characteristic2.Key = "Профиль";
                characteristic2.Value = "65";

                Characteristic characteristic3 = new Characteristic();
                characteristic3.Key = "Диаметр";
                characteristic3.Value = "R15";

                Characteristic characteristic4 = new Characteristic();
                characteristic4.Key = "Бренд";
                characteristic4.Value = "Premiorri";

                Characteristic characteristic5 = new Characteristic();
                characteristic5.Key = "Сезон";
                characteristic5.Value = "Зима";

                product.Characteristics.Add(characteristic1);
                product.Characteristics.Add(characteristic2);
                product.Characteristics.Add(characteristic3);
                product.Characteristics.Add(characteristic4);
                product.Characteristics.Add(characteristic5);

                TireCategory.Products.Add(product);
                
            }
            db.Set<Category>().Add(TireCategory);

            for (int i = 0; i < 87; i++)
            {

                Product product = new Product();
                product.Name = "ДК Ford" + i.ToString();
                product.PhotoPath = "~/Images/shiny.png";
                product.Price = rand.Next(500, 5000);

                Characteristic characteristic1 = new Characteristic();
                characteristic1.Key = "Ширина";
                characteristic1.Value = "195";

                Characteristic characteristic2 = new Characteristic();
                characteristic2.Key = "Профиль";
                characteristic2.Value = "65";

                Characteristic characteristic3 = new Characteristic();
                characteristic3.Key = "Диаметр";
                characteristic3.Value = "R15";

                Characteristic characteristic4 = new Characteristic();
                characteristic4.Key = "Бренд";
                characteristic4.Value = "Premiorri";

                Characteristic characteristic5 = new Characteristic();
                characteristic5.Key = "Сезон";
                characteristic5.Value = "Зима";

                product.Characteristics.Add(characteristic1);
                product.Characteristics.Add(characteristic2);
                product.Characteristics.Add(characteristic3);
                product.Characteristics.Add(characteristic4);
                product.Characteristics.Add(characteristic5);

                DiskCategory.Products.Add(product);

            }
            db.Set<Category>().Add(TireCategory);
            db.Set<Category>().Add(DiskCategory);

        }
    }
}