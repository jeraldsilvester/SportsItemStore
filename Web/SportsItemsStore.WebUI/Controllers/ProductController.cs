using SportsItemsStore.Domain.Abstract;
using SportsItemsStore.Domain.Entities;
using SportsItemsStore.WebUI.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.Mvc;

namespace SportsItemsStore.WebUI.Controllers
{
    public class ProductController : Controller
    {
        private IProductsRepository repository;
        private ItemLoaderManager iLoaderMgr;
        public int PageSize = 4;

        public ProductController(IProductsRepository productRepository)
        {
            this.repository = productRepository;
            this.iLoaderMgr = new ItemLoaderManager(productRepository);
        }

        public ActionResult List(int categoryId = 0, int BlockNumber = 1, int BlockSize = 5, string searchTerm = null,
            int sizeId = 0, int colorId = 0, int start = 0, int end = 0, int manufacturerId = 0)
        {
            var model = iLoaderMgr.ProductsList(categoryId, BlockNumber, BlockSize, searchTerm, sizeId, colorId, start, end, manufacturerId);

            return View(model);
        }

        [HttpPost]
        public ActionResult InfinateScroll(int categoryId = 0, int BlockNumber = 1, int BlockSize = 5, string searchTerm = null,
            int sizeId = 0, int colorId = 0, int start = 0, int end = 0, int manufacturerId = 0)
        {
            //////////////// THis line of code only for demo. Needs to be removed ///////////////
            System.Threading.Thread.Sleep(3000);
            ////////////////////////////////////////////////////////////////////////////////////////

            var productModel = iLoaderMgr.ProductsList(categoryId, BlockNumber, BlockSize, searchTerm, sizeId, colorId, start, end, manufacturerId);

            JsonModel jsonModel = new JsonModel();
            jsonModel.NoMoreData = productModel.Products.Count() < BlockSize;
            jsonModel.HTMLString = RenderPartialViewToString("ProductPopList", productModel);

            return Json(jsonModel);
        }

        protected string RenderPartialViewToString(string viewName, object model)
        {
            if (string.IsNullOrEmpty(viewName))
                viewName = ControllerContext.RouteData.GetRequiredString("action");

            ViewData.Model = model;

            using (StringWriter sw = new StringWriter())
            {
                ViewEngineResult viewResult = ViewEngines.Engines.FindPartialView(ControllerContext, viewName);
                ViewContext viewContext = new ViewContext(ControllerContext, viewResult.View, ViewData, TempData, sw);
                viewResult.View.Render(viewContext, sw);

                return sw.GetStringBuilder().ToString();
            }
        }

        public JsonResult GetProducts(string term)
        {
            IList<string> prods = null;

            prods = repository.Products.Where(p => (p.Name.Contains(term) || p.Description.Contains(term))).Select(y => y.Name).ToList();

            return Json(prods, JsonRequestBehavior.AllowGet);
        }

        public ViewResult ViewDetails(int productId,string returnUrl)
        {
            Product prod = null;
            //IList<SelectListItem> sizes, colors, manufacteres;
            

            if (productId != 0)
            {
                prod = repository.Products.Where(p => p.ProductID == productId).SingleOrDefault();
            }
            //IList<string> sizes = repository.Products.Where(x => x.ProductID == productId).Single().ProductSizes.Select(y => y.Size.ShortName).ToList();
            //IList<string> colors = repository.Products.Where(x => x.ProductID == productId).SingleOrDefault().ProductColors.Select(y => y.Color.Name).ToList();
            //IList<string> manufacteres = repository.Products.Where(x => x.ProductID == productId).SingleOrDefault().ProductManufacturers.Select(z => z.Manufacturer.Name).ToList();

            if (prod != null)
            {
                var sizes= prod.ProductSizes.Select(x => new SelectListItem { Text = x.Size.ShortName, Value = x.Size.SizeID.ToString() }).ToList();
                sizes.Insert(0, new SelectListItem { Text = "--Select--", Value = "0" });
                ViewData["Sizes"] = sizes;

                var colors = prod.ProductColors.Select(y => new SelectListItem { Text = y.Color.Name, Value = y.Color.ColorID.ToString() }).ToList();
                colors.Insert(0, new SelectListItem { Text = "--Select--", Value = "0" });
                ViewData["Colors"] = colors;

                var manufacterers = prod.ProductManufacturers.Select(z => new SelectListItem { Text = z.Manufacturer.Name, Value = z.Manufacturer.ManufacturerID.ToString() }).ToList();
                manufacterers.Insert(0, new SelectListItem { Text = "--Select--", Value = "0" });
                ViewData["Manufacterers"] = manufacterers;

            }
           
            DetailsViewModel model = new DetailsViewModel
            {
                product = prod,
                ReturnUrl = returnUrl,
                

            };
            
            

            return View(model);
            //return RedirectToAction("Index", new { returnUrl });
        }

        public PartialViewResult SearchWithFilters(int categoryId = 0, int BlockNumber = 1, int BlockSize = 5, string searchTerm = null,
           int sizeId = 0, int colorId = 0, int start = 0, int end = 0, int manufacturerId = 0)
        {
            ViewBag.SizeId = sizeId;
            ViewBag.ColorId = colorId;
            ViewBag.Start = start;
            ViewBag.End = end;
            if (manufacturerId != 0)
                TempData["ManfacturerId"] = manufacturerId;
 
            var model = iLoaderMgr.ProductsList(categoryId, BlockNumber, BlockSize, searchTerm, sizeId, colorId, start, end, manufacturerId);

            return PartialView("ProductsList", model);
        }

        public PartialViewResult GetallView(int categoryId = 0, int BlockNumber = 1, int BlockSize = 5, string searchTerm = null,
           int sizeId = 0, int colorId = 0, int start = 0, int end = 0,int manufacturerId=0)
        {
            ViewBag.SizeId = sizeId;
            ViewBag.ColorId = colorId;
            ViewBag.Start = start;
            ViewBag.End = end;

            if (manufacturerId != 0)
                TempData["ManfacturerId"] = manufacturerId;

            var model = iLoaderMgr.ProductsList(categoryId, BlockNumber, BlockSize, searchTerm, sizeId, colorId, start, end, manufacturerId);

            return PartialView("ProductsList", model);
        }

        public PartialViewResult GetallByPriceRange(int categoryId = 0, int BlockNumber = 1, int BlockSize = 5, string searchTerm = null,
           int sizeId = 0, int colorId = 0, int start = 0, int end = 0, int manufacturerId=0)
        {
            ViewBag.SizeId = sizeId;
            ViewBag.ColorId = colorId;
            ViewBag.Start = start;
            ViewBag.End = end;

            if (manufacturerId != 0)
                TempData["ManfacturerId"] = manufacturerId;

            var model = iLoaderMgr.ProductsList(categoryId, BlockNumber, BlockSize, searchTerm, sizeId, colorId, start, end, manufacturerId);

            return PartialView("ProductsList", model);
        }
    }
}