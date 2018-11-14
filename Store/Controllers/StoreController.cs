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
    public class StoreController : Controller
    {

        private IStoreService StoreService
        {
            get
            {
                return HttpContext.GetOwinContext().Get<IStoreService>();
            }
        }


        [HttpGet]
        public async Task<ActionResult> Test(string NameCategory, int Page = 1)
        {
            Page -= 1;
            //Debug.Write(NameCategory + "\n\n\n\n\n\n");
            return View("Test", await Task.Run(async () =>
            {
                PageInfoDTO pageInfo = await StoreService.GetProductsByCategory(NameCategory, Page);
                if (pageInfo != null)
                {
                    List<ProductDTO> DTO = pageInfo.Products as List<ProductDTO>;
                    //Debug.Write(DTO.Count + "\n\n\n\n");

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

                    //return GPM as IList<GeneralProductModel>;

                    NavigationModel navigation = new NavigationModel();
                    navigation.MaxPages = pageInfo.MaxPages;
                    navigation.SelectedPage = Page+1;
                    

                    InfoForPageProductModel info = new InfoForPageProductModel();
                    info.ProductModels = GPM as IList<GeneralProductModel>;

                    info.NavigationModel_ = navigation;
                    info.ThisCategory = NameCategory;
                    return info;
                }
                else
                    return null;

            }));

            //return View("Test", null);
           

        }

        //[HttpPost]
        //public async Task<PartialViewResult> _NavigationView(int MaxPages = 0, int SelectedPage = 1)
        //{
        //    NavigationModel model = new NavigationModel();
        //    model.ActionUrl = new List<string>();
        //    model.MaxPages = MaxPages;
        //    model.SelectedPage = SelectedPage;
        //    return PartialView("_NavigationView", model);
        //}


        [HttpGet]
        public async Task<ActionResult> ToBuyProduct(int Id) //GetProductById
        {
            Debug.Write(Id + "\n\n\n\n");
            var model = await Task.Run(() =>
            {
                GeneralProductModel _model = new GeneralProductModel();
                //model.PhotoPath = System.IO.File.ReadAllBytes(@"C:\Users\zhenyastufeev\Source\Repos\Store\Store\Images\shiny.png");
                _model.PhotoPath = Url.Content("~/ContentImages/Content.png");
                _model.Price = 1566;
                _model.TextUrl = "Bridgestone Blizzak DM-V2 245/70 R16 107S";
                //_model.UrlToBuy = Url.Content("~/Store/ToBuyProduct/" + Id.ToString());

                return _model;
            });

            return View("ToBuyProduct", model);
        }

        [HttpGet]
        public async Task<PartialViewResult> GetFilters(IEnumerable<Store.Models.CheckBoxModel> Message)
        {
            if (Message == null)
                Debug.Write("null!!!\n\n\n\n\n\n");
            else
            {
                Debug.Write("NotNull\n\n\n\n\n\n");
                Debug.Write((Message as List<CheckBoxModel>).Count); //Приходить пустий список
            }
            // ViewBag.ToReloadFilters = false;
            return await Task.Run(() =>
            {

                List<CheckBoxModel> list = new List<CheckBoxModel>();

                CheckBoxModel.RefreshId();

                CheckBoxModel model = new CheckBoxModel();
                model.Value = "Test1";
                model.IsChecked = false;
                CheckBoxModel model1 = new CheckBoxModel();
                model1.Value = "Test2";
                model1.IsChecked = false;
                CheckBoxModel model2 = new CheckBoxModel();
                model2.Value = "Test3";
                model2.IsChecked = true;

                list.Add(model);
                list.Add(model1);
                list.Add(model2);

                return PartialView("_FilterView", list);
                // return JsonConvert.SerializeObject(list);
            });

        }


        [HttpPost]
        public async Task SendFilters(IEnumerable<CheckBoxModel> checks)
        {
            await Task.Run(() =>
            {
                List<CheckBoxModel> list = checks as List<CheckBoxModel>;
            });

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
                        UrlToMove = (Url.Content("~/Store/Test/" + DTOCategory.Name + "/1"))
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