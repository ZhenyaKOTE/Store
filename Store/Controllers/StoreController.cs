using Microsoft.AspNet.Identity.Owin;
using Newtonsoft.Json;
using Store.BLL.DTO;
using Store.BLL.Interfaces;
using Store.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;

namespace Store.Controllers
{
    public class StoreController :  Controller
    {

        private IStoreService StoreService
        {
            get
            {
                return HttpContext.GetOwinContext().Get<IStoreService>();
            }
        }


        [HttpGet]
        public async Task<ActionResult> ProductsView(string NameCategory, int Page = 1)
        {
            if (NameCategory == null)
                return null;

            return View( await Task.Run(async () =>
            {
                PageInfoDTO pageInfo = await StoreService.GetProductsByCategory(NameCategory, Page-1);
                if (pageInfo != null)
                {
                    List<ProductDTO> DTO = pageInfo.Products as List<ProductDTO>;

                    List<GeneralProductModel> GPM = new List<GeneralProductModel>();
                    foreach (ProductDTO pr in DTO)
                    {
                        GeneralProductModel PrModel = new GeneralProductModel();
                        PrModel.Id = pr.Id;
                        PrModel.Price = pr.Price;
                        PrModel.PhotoPath = Url.Content(pr.PhotoPath) ?? "";
                        PrModel.TextUrl = pr.Name;
                        PrModel.UrlToBuy = (Url.Content("~/Store/ToBuyProduct/" + pr.Id) ?? "");
                        GPM.Add(PrModel);
                    }

                    NavigationModel navigation = new NavigationModel();
                    navigation.MaxPages = pageInfo.MaxPages;
                    navigation.SelectedPage = Page;
                    
                    InfoForPageProductModel info = new InfoForPageProductModel();
                    info.ProductModels = GPM as IList<GeneralProductModel>;

                    info.NavigationModel_ = navigation;
                    info.ThisCategory = NameCategory;
                    return info;
                }
                else
                    return null;

            }));
        }

        [HttpGet]
        public async Task<ActionResult> ToBuyProduct(int Id) //GetProductById
        {

            var model = await Task.Run(async () =>
            {
                ProductDTO product = await StoreService.GetProductById(Id);
                if (product != null)
                {
                    GeneralProductModel GeneralModel = new GeneralProductModel
                    {
                        Description = product.Description,
                        Id = product.Id,
                        TextUrl = product.Name,
                        Price = product.Price,
                        PhotoPath = product.PhotoPath,
                        //UrlToBuy = 
                    };

                    foreach (var pr in product.Characteristics)
                    {
                        GeneralModel.Characteristics.Add(new CharacteriscitModel { Key = pr.Key, Value = pr.Value });
                    }

                    return GeneralModel;
                }
                else
                    return null;
            });

            return View("ToBuyProduct", model);
        }




        [HttpGet]
        public async Task<ActionResult> TestPage()
        {
            return View();
        }


        [HttpPost]
        public async Task<PartialViewResult> _FilterView(string SerializeFilters)
        {
            
            return PartialView("_FilterView");
        }



        [HttpPost]
        [OutputCache(Duration = 3600, Location = OutputCacheLocation.ServerAndClient)]
        public async Task<string> GetCategory()
        {

            return await Task.Run(async () =>
            {
                var a = await StoreService.GetCategories();
                List<CategoryModel> list = new List<CategoryModel>();
                foreach (CategoryDTO DTOCategory in a)
                {
                    list.Add(new CategoryModel
                    {
                        Id = DTOCategory.Id,
                        Name = DTOCategory.Name,
                        UrlToMove = (Url.Content("~/Store/ProductsView/" + DTOCategory.Name + "/1"))
                    });
           
                }
                return JsonConvert.SerializeObject(list);
            });
            
           // return null;
        }
    }
}





//CategoryDTO category = new CategoryDTO();
//category.Name = "Диски";
//            await StoreService.CreateAsync(category);

//ProductDTO product = new ProductDTO();
//product.Name = "Диск для машини";
//            product.PhotoPath = "~/ContentImages/7.jpg";
//            product.Price = 3247;
//            product.Description = "А це диск для машин";

//            await StoreService.AddProductToCategory(product, category.Name);


//CategoryDTO category = new CategoryDTO();
//category.Name = "Tires";
//await StoreService.CreateAsync(category);

//await StoreService.CreateAsync(new CategoryDTO { Name = "Tires" });

//ProductDTO product = new ProductDTO();
//product.Name = "Покришка";
//product.PhotoPath = "~/ContentImages/1.jpg";
//product.Price = 1567;
//product.Description = "This is cool tire";

//ProductDTO product1 = new ProductDTO();
//product1.Name = "Мега покришка";
//product1.PhotoPath = "~/ContentImages/3.jpg";
//product1.Price = 14577;
//product1.Description = "Це дуже крута покришка";
//await StoreService.AddProductToCategory(product, "Tires");
//await StoreService.AddProductToCategory(product1, "Tires");