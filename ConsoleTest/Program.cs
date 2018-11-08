using Store.DAL.Entities.StoreEntities;
using Store.DAL.Interfaces;
using Store.DAL.Repositories;
using System;
using System.Collections.Generic;
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
            Product product = new Product();
            product.Name = "";
            product.Price = 128

            unit.StoreManager.Create();
        }
    }
}
