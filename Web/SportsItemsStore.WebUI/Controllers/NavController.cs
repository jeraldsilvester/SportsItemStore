using SportsItemsStore.Domain.Abstract;
using SportsItemsStore.Domain.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace SportsItemsStore.WebUI.Controllers
{
    public class NavController : Controller
    {
        private IProductsRepository repository;

        public NavController(IProductsRepository repo)
        {
            repository = repo;
        }

        public PartialViewResult Menu(string category = null)
        {
            ViewBag.SelectedCategory = category;

            IEnumerable<Category> categories = repository.Categories;

            List<SelectListItem> sizes = repository.Sizes.Select(x => new SelectListItem { Text = x.ShortName, Value = x.SizeID.ToString() }).ToList();
            sizes.Insert(0, new SelectListItem { Text = "--Select--", Value = "0" });

            ViewData["Sizes"] = sizes;

            List<SelectListItem> colors = repository.Colors.Select(x => new SelectListItem { Text = x.Name, Value = x.ColorID.ToString() }).ToList();
            colors.Insert(0, new SelectListItem { Text = "--Select--", Value = "0" });

            ViewData["Colors"] = colors;

            List<SelectListItem> manufacturers = repository.Manufacturers.Select(x => new SelectListItem { Text = x.Name, Value = x.ManufacturerID.ToString() }).ToList();
            manufacturers.Insert(0, new SelectListItem { Text = "--Select--", Value = "0" });

            ViewData["Manufacturers"] = manufacturers;

            return PartialView(categories);
        }

        [ChildActionOnly]
        public void ClearSessionValues()
        {
            //Session["Start"] = null;
            //Session["End"] = null;
            Session["SizeId"] = null;
            Session["ColorId"] = null;
        }
    }
}