using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SportsItemsStore.Domain.Entities;
using SportsItemsStore.Domain.Abstract;
using SportsItemsStore.WebUI.Models;

namespace SportsItemsStore.WebUI.Controllers
{
    public class ProductController : Controller
    {

        private IProductsRepository repository;
        public int PageSize = 4;

        public ProductController(IProductsRepository productRepository)
        {
            this.repository = productRepository;
        }



        public ActionResult List(string category, int page = 1, string searchTerm = null , int sizeId=0,int colorId=0,int start=0,int end=0)
        {
            IEnumerable<Product> prods = null;
            int totItems = 0;

            Session["SizeId"] = sizeId.ToString();
            Session["ColorId"] = colorId.ToString();
            Session["Start"] = start;
            Session["End"] = end;

            string sizeName = "", colorName = "";
            if (sizeId > 0)
            {
                sizeName = repository.Sizes.Where(s => s.SizeID == sizeId).Select(ss => ss.ShortName).SingleOrDefault();
            }
            if (colorId > 0)
            {
                colorName = repository.Colors.Where(c => c.ColorID == colorId).Select(cc => cc.Name).SingleOrDefault();
            }

            if (searchTerm != null && searchTerm != "")
            {
                if (start > 0 || end > 0)                
                {
                    if (sizeId != 0 && colorId != 0)
                    {
                        prods = repository.Products.Where(p => p.Name.Contains(searchTerm) && p.Price >= start && p.Price <= end && p.ProductSizes.Select(ps => ps.Size.SizeID).Contains(sizeId) && p.ProductColors.Select(pc => pc.Color.ColorID).Contains(colorId))
                                                    .OrderBy(p => p.ProductID).Skip((page - 1) * PageSize).Take(PageSize);

                        totItems = start == 0 && end == 0 ? repository.Products.Where(p => p.Name.Contains(searchTerm) && p.ProductSizes.Select(ps => ps.Size.SizeID).Contains(sizeId) && p.ProductColors.Select(pc => pc.Color.ColorID).Contains(colorId)).Count() : repository.Products.Where(p => p.Name.Contains(searchTerm) && p.Price >= start && p.Price <= end && p.ProductSizes.Select(ps => ps.Size.SizeID).Contains(sizeId) && p.ProductColors.Select(pc => pc.Color.ColorID).Contains(colorId)).Count();

                        Session["Size"] = sizeName;
                        Session["Color"] = colorName;

                        Session["SizeId"] = sizeId.ToString();
                        Session["ColorId"] = colorId.ToString();

                    }
                    else if (sizeId != 0 && colorId ==0)
                    {
                        prods = repository.Products.Where(p => p.Name.Contains(searchTerm) && p.Price >= start && p.Price <= end && p.ProductSizes.Select(ps => ps.Size.SizeID).Contains(sizeId))
                                                     .OrderBy(p => p.ProductID).Skip((page - 1) * PageSize).Take(PageSize);
                       

                        totItems = start == 0 && end == 0 ? repository.Products.Where(p => p.Name.Contains(searchTerm) && p.ProductSizes.Select(ps => ps.Size.SizeID).Contains(sizeId)).Count() : repository.Products.Where(p =>p.Name.Contains(searchTerm) && p.Price >= start && p.Price <= end && p.ProductSizes.Select(ps => ps.Size.SizeID).Contains(sizeId)).Count();

                        Session["Size"] = sizeName;
                         Session["SizeId"] = sizeId.ToString();

                    }
                    else if(colorId !=0 && sizeId==0)
                    {
                        prods = repository.Products.Where(p => p.Name.Contains(searchTerm) && p.Price >= start && p.Price <= end && p.ProductColors.Select(pc => pc.Color.ColorID).Contains(colorId))
                                                     .OrderBy(p => p.ProductID).Skip((page - 1) * PageSize).Take(PageSize);

                        totItems = start == 0 && end == 0 ? repository.Products.Where(p => p.Name.Contains(searchTerm) && p.ProductColors.Select(pc => pc.Color.ColorID).Contains(colorId)).Count() : repository.Products.Where(p =>p.Name.Contains(searchTerm) && p.Price >= start && p.Price <= end && p.ProductColors.Select(pc => pc.Color.ColorID).Contains(colorId)).Count();
                       
                       
                        Session["Color"] = colorName;
                        Session["ColorId"] = colorId.ToString();
                    }
                    
                    else
                    {
                        prods = repository.Products.Where(p => p.Name.Contains(searchTerm))
                                                     .Where(p => p.Price >= start && p.Price <= end)
                                                     .OrderBy(p => p.ProductID).Skip((page - 1) * PageSize).Take(PageSize);

                        totItems = start == 0 && end == 0 ? repository.Products.Where(p => p.Name.Contains(searchTerm)).Count() : repository.Products.Where(p => p.Name.Contains(searchTerm)).Where(p => p.Price >= start && p.Price <= end).Count();
                    }
                   // Session["SearchTerm"] = searchTerm;
                    //Session["Start"] = start;
                    //Session["End"] = end;

                }
                else
                {
                    if (sizeId != 0 && colorId != 0)
                    {

                        prods = repository.Products.Where(p => p.Name.Contains(searchTerm) && p.ProductSizes.Select(ps => ps.Size.SizeID).Contains(sizeId) && p.ProductColors.Select(pc => pc.Color.ColorID).Contains(colorId))
                                          .OrderBy(p => p.ProductID).Skip((page - 1) * PageSize).Take(PageSize).ToList();

                        totItems = category == null ? repository.Products.Where(p => p.Name.Contains(searchTerm) && p.ProductSizes.Select(ps => ps.Size.SizeID).Contains(sizeId) && p.ProductColors.Select(pc => pc.Color.ColorID).Contains(colorId)).Count() : repository.Products.Where(x => x.Category == category && x.Name.Contains(searchTerm) && x.ProductSizes.Select(ps => ps.Size.SizeID).Contains(sizeId) && x.ProductColors.Select(pc => pc.Color.ColorID).Contains(colorId)).Count();

                        Session["Size"] = sizeName;
                        Session["Color"] = colorName;
                        //Session["SearchTerm"] = searchTerm;

                        Session["SizeId"] = sizeId.ToString();
                        Session["ColorId"] = colorId.ToString();
                    }
                    else if (sizeId != 0 && colorId == 0)
                    {

                        prods = repository.Products.Where(p => p.Name.Contains(searchTerm) && p.ProductSizes.Select(ps => ps.Size.SizeID).Contains(sizeId))
                                          .OrderBy(p => p.ProductID).Skip((page - 1) * PageSize).Take(PageSize).ToList();

                        totItems = category == null ? repository.Products.Where(p => p.Name.Contains(searchTerm) && p.ProductSizes.Select(ps => ps.Size.SizeID).Contains(sizeId)).Count() : repository.Products.Where(x => x.Category == category && x.Name.Contains(searchTerm) && x.ProductSizes.Select(ps => ps.Size.SizeID).Contains(sizeId)).Count();

                        Session["Size"] = sizeName;
                         Session["SizeId"] = sizeId.ToString();
                    }
                    else if (colorId != 0 && sizeId == 0)
                    {

                        prods = repository.Products.Where(p => p.Name.Contains(searchTerm) && p.ProductColors.Select(pc => pc.Color.ColorID).Contains(colorId))
                                          .OrderBy(p => p.ProductID).Skip((page - 1) * PageSize).Take(PageSize).ToList();


                        totItems = category == null ? repository.Products.Where(p => p.Name.Contains(searchTerm) && p.ProductColors.Select(pc => pc.Color.ColorID).Contains(colorId)).Count() : repository.Products.Where(x => x.Category == category && x.Name.Contains(searchTerm) && x.ProductColors.Select(pc => pc.Color.ColorID).Contains(colorId)).Count();
                        Session["Color"] = colorName;
                         Session["ColorId"] = colorId.ToString();

                    }
                    else
                    {
                        prods = repository.Products
                                        .Where(p => p.Name.Contains(searchTerm))
                                        .OrderBy(p => p.ProductID).Skip((page - 1) * PageSize).Take(PageSize).ToList();

                        totItems = category == null ? repository.Products.Where(y => y.Name.Contains(searchTerm)).Count() : repository.Products.Where(x => x.Category == category && x.Name.Contains(searchTerm)).Count();

                        Session["Color"] = null;
                        Session["Size"] = null;
                        Session["SizeId"] = null;
                        Session["ColorId"] = null;
                        Session["Start"] = null;
                        Session["End"] = null;
                    }
                    //else
                    //{
                    //    prods = repository.Products
                    //                             .Where(p => p.Price >= start && p.Price <= end)
                    //                             .OrderBy(p => p.ProductID).Skip((page - 1) * PageSize).Take(PageSize);

                    //    totItems = start == 0 && end == 0 ? repository.Products.Count() : repository.Products.Where(p => p.Price >= start && p.Price <= end).Count();
                    //}
                }
                Session["SearchTerm"] = searchTerm;
            }

            else 
            {
                if (start > 0 || end > 0)
                {

                    //*************
                    if (sizeId != 0 && colorId != 0)
                    {
                        prods = repository.Products.Where(p => p.Price >= start && p.Price <= end && p.ProductSizes.Select(ps => ps.Size.SizeID).Contains(sizeId) && p.ProductColors.Select(pc => pc.Color.ColorID).Contains(colorId))
                                                    .OrderBy(p => p.ProductID).Skip((page - 1) * PageSize).Take(PageSize);

                        totItems = start == 0 && end == 0 ? repository.Products.Where(p => p.ProductSizes.Select(ps => ps.Size.SizeID).Contains(sizeId) && p.ProductColors.Select(pc => pc.Color.ColorID).Contains(colorId)).Count() : repository.Products.Where(p => p.Price >= start && p.Price <= end && p.ProductSizes.Select(ps => ps.Size.SizeID).Contains(sizeId) && p.ProductColors.Select(pc => pc.Color.ColorID).Contains(colorId)).Count();

                        Session["Size"] = sizeName;
                        Session["Color"] = colorName;

                         Session["SizeId"] = sizeId.ToString();
                         Session["ColorId"] = colorId.ToString();

                    }

                    else if (sizeId != 0 && colorId == 0)
                    {
                        prods = repository.Products.Where(p => p.Price >= start && p.Price <= end && p.ProductSizes.Select(ps => ps.Size.SizeID).Contains(sizeId))
                                                     .OrderBy(p => p.ProductID).Skip((page - 1) * PageSize).Take(PageSize);

                        totItems = start == 0 && end == 0 ? repository.Products.Where(p => p.ProductSizes.Select(ps => ps.Size.SizeID).Contains(sizeId)).Count() : repository.Products.Where(p => p.Price >= start && p.Price <= end && p.ProductSizes.Select(ps => ps.Size.SizeID).Contains(sizeId)).Count();

                        Session["Size"] = sizeName;
                         Session["SizeId"] = sizeId.ToString();

                    }
                    else if (colorId != 0 && sizeId == 0)
                    {
                        prods = repository.Products.Where(p => p.Price >= start && p.Price <= end && p.ProductColors.Select(pc => pc.Color.ColorID).Contains(colorId))
                                                     .OrderBy(p => p.ProductID).Skip((page - 1) * PageSize).Take(PageSize);

                        totItems = start == 0 && end == 0 ? repository.Products.Where(p => p.ProductColors.Select(pc => pc.Color.ColorID).Contains(colorId)).Count() : repository.Products.Where(p => p.Price >= start && p.Price <= end && p.ProductColors.Select(pc => pc.Color.ColorID).Contains(colorId)).Count();

                        //string colorName = repository.Colors.Where(c => c.ColorID == colorId).Select(cc => cc.Name).SingleOrDefault();
                        Session["Color"] = colorName;
                        Session["ColorId"] = colorId.ToString();

                    }
                    //*****************
                    else
                    {
                        prods = repository.Products
                                                       .Where(p => p.Price >= start && p.Price <= end)
                                                       .OrderBy(p => p.ProductID).Skip((page - 1) * PageSize).Take(PageSize);

                        totItems = start == 0 && end == 0 ? repository.Products.Count() : repository.Products.Where(p => p.Price >= start && p.Price <= end).Count();
                    }
                    //***************
                }
                else
                {
                        if (sizeId != 0 && colorId != 0)
                        {

                            prods = repository.Products.Where(p => p.ProductSizes.Select(ps => ps.Size.SizeID).Contains(sizeId) && p.ProductColors.Select(pc => pc.Color.ColorID).Contains(colorId))
                                              .OrderBy(p => p.ProductID).Skip((page - 1) * PageSize).Take(PageSize).ToList();

                            totItems = category == null ? repository.Products.Where(p => p.ProductSizes.Select(ps => ps.Size.SizeID).Contains(sizeId) && p.ProductColors.Select(pc => pc.Color.ColorID).Contains(colorId)).Count() : repository.Products.Where(x => x.Category == category && x.ProductSizes.Select(ps => ps.Size.SizeID).Contains(sizeId) && x.ProductColors.Select(pc => pc.Color.ColorID).Contains(colorId)).Count();

                             Session["Size"] = sizeName;
                             Session["Color"] = colorName;

                             Session["SizeId"] = sizeId.ToString();
                             Session["ColorId"] = colorId.ToString();

                        }
                        else if (sizeId > 0 && colorId == 0)
                        {
                            prods = repository.ProductSizes.Where(ps => ps.Size.SizeID == sizeId).Select(p => p.Product).OrderBy(p => p.ProductID).Skip((page - 1) * PageSize).Take(PageSize);
                            
                            totItems = category == null ? repository.ProductSizes.Where(ps => ps.Size.SizeID == sizeId).Select(p => p.Product).Count() : repository.ProductSizes.Where(ps => ps.Size.SizeID == sizeId).Select(p => p.Product).Where(pp => pp.Category == category).Count();

                            Session["Size"] = sizeName;
                            Session["SizeId"] = sizeId.ToString();
                        }
                        else if (colorId > 0 && sizeId == 0)
                        {                            
                            prods = repository.ProductColors.Where(pc => pc.Color.ColorID == colorId).Select(p => p.Product).OrderBy(p => p.ProductID).Skip((page - 1) * PageSize).Take(PageSize);
                         
                            totItems = category == null ? repository.ProductColors.Where(pc => pc.Color.ColorID == colorId).Select(p => p.Product).Count() : repository.ProductColors.Where(pc => pc.Color.ColorID == colorId).Select(p => p.Product).Where(pp => pp.Category == category).Count();

                             Session["Color"] = colorName;
                             Session["ColorId"] = colorId.ToString();
                        }

                        else
                        {
                            prods = repository.Products
                                        .Where(p => category == null || p.Category == category)
                                        .OrderBy(p => p.ProductID).Skip((page - 1) * PageSize).Take(PageSize);

                            totItems = category == null ? repository.Products.Count() : repository.Products.Where(x => x.Category == category).Count();
                            Session["SearchTerm"] = null;
                            Session["Color"] = null;
                            Session["Size"] = null;
                            Session["SizeId"] = null;
                            Session["ColorId"] = null;
                            Session["Start"] = null;
                            Session["End"] = null;

                        }

                    
                }

            }           
           
            
            ProductsListViewModel model = new ProductsListViewModel
            {
                Products = prods,

                PagingInfo = new PagingInfo
                {
                    CurrentPage = page,
                    ItemsPerPage = PageSize,
                    TotalItems = totItems,
                    SizeId=sizeId,
                    ColorId=colorId,
                    StartPrice=start,
                    EndPrice=end
                },

                CurrentCategory = category,
                SearchTerm=searchTerm
            };
      
                return View(model);          
        }


        public JsonResult GetProducts(string term)
        {
            IList<string> prods = null;

            prods = repository.Products.Where(p => p.Name.Contains(term)).Select(y => y.Name).ToList();
            
            return Json(prods, JsonRequestBehavior.AllowGet);
        }


        public ViewResult ViewDetails( int productId, string returnUrl)
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
                 ViewData["Sizes"] = prod.ProductSizes.Select(x => new SelectListItem { Text = x.Size.ShortName, Value = x.Size.SizeID.ToString() }).ToList();

                 ViewData["Colors"] = prod.ProductColors.Select(y => new SelectListItem { Text = y.Color.Name, Value = y.Color.ColorID.ToString() }).ToList();

                 ViewData["Manufacterers"] = prod.ProductManufacturers.Select(z => new SelectListItem { Text = z.Manufacturer.Name, Value = z.Manufacturer.ManufacturerID.ToString() }).ToList();

            }

            DetailsViewModel model = new DetailsViewModel
            {
                product=prod,
                ReturnUrl=returnUrl
            };

            Session["SearchTerm"] = null;
            Session["Color"] = null;
            Session["Size"] = null;
            Session["SizeId"] = null;
            Session["ColorId"] = null;
            Session["Start"] = null;
            Session["End"] = null;
            return View(model);
            //return RedirectToAction("Index", new { returnUrl });
        }

        public PartialViewResult SearchWithFilters(string searchTerm, string category, int page = 1, int sizeId = 0, int colorId = 0, int start = 0, int end = 0)
        {
            IEnumerable<Product> prods = null;
            int totItems = 0;
            ProductsListViewModel model = null;
            string sizeName = "",colorName="" ;
            if (sizeId > 0)
            {
                 sizeName = repository.Sizes.Where(s => s.SizeID == sizeId).Select(ss => ss.ShortName).SingleOrDefault();
            }
            if (colorId > 0)
            {
                colorName = repository.Colors.Where(c => c.ColorID == colorId).Select(cc => cc.Name).SingleOrDefault();
            }

            if (searchTerm != null && searchTerm != "")
            {
                if (start > 0 || end > 0)
                {
                    if (sizeId != 0 && colorId != 0)
                    {
                        prods = repository.Products.Where(p => p.Name.Contains(searchTerm) && p.Price >= start && p.Price <= end && p.ProductSizes.Select(ps => ps.Size.SizeID).Contains(sizeId) && p.ProductColors.Select(pc => pc.Color.ColorID).Contains(colorId))
                                                    .OrderBy(p => p.ProductID).Skip((page - 1) * PageSize).Take(PageSize);

                        totItems = start == 0 && end == 0 ? repository.Products.Where(p => p.Name.Contains(searchTerm) && p.ProductSizes.Select(ps => ps.Size.SizeID).Contains(sizeId) && p.ProductColors.Select(pc => pc.Color.ColorID).Contains(colorId)).Count() : repository.Products.Where(p => p.Name.Contains(searchTerm) && p.Price >= start && p.Price <= end && p.ProductSizes.Select(ps => ps.Size.SizeID).Contains(sizeId) && p.ProductColors.Select(pc => pc.Color.ColorID).Contains(colorId)).Count();

                        //string sizeName = repository.Sizes.Where(s => s.SizeID == sizeId).Select(ss => ss.ShortName).SingleOrDefault();
                        Session["Size"] = sizeName;

                        //string colorName = repository.Colors.Where(c => c.ColorID == colorId).Select(cc => cc.Name).SingleOrDefault();
                        Session["Color"] = colorName;

                    }

                    else if (sizeId != 0 && colorId == 0)
                    {
                        prods = repository.Products.Where(p => p.Name.Contains(searchTerm) && p.Price >= start && p.Price <= end && p.ProductSizes.Select(ps => ps.Size.SizeID).Contains(sizeId))
                                                     .OrderBy(p => p.ProductID).Skip((page - 1) * PageSize).Take(PageSize);

                        //totItems = start == 0 && end == 0 ? repository.Products.Where(p => p.Name.Contains(searchTerm) && p.ProductSizes.Select(ps => ps.Size.SizeID).Contains(sizeId)).Count() : repository.Products.Where(p => p.Name.Contains(searchTerm)).Where(p => p.Price >= start && p.Price <= end && p.ProductSizes.Select(ps => ps.Size.SizeID).Contains(sizeId)).Count();

                        totItems = start == 0 && end == 0 ? repository.Products.Where(p => p.Name.Contains(searchTerm) && p.ProductSizes.Select(ps => ps.Size.SizeID).Contains(sizeId)).Count() : repository.Products.Where(p => p.Name.Contains(searchTerm) && p.Price >= start && p.Price <= end && p.ProductSizes.Select(ps => ps.Size.SizeID).Contains(sizeId)).Count();

                        //string sizeName = repository.Sizes.Where(s => s.SizeID == sizeId).Select(ss => ss.ShortName).SingleOrDefault();
                        Session["Size"] = sizeName;

                    }
                    else if (colorId != 0 && sizeId == 0)
                    {
                        prods = repository.Products.Where(p => p.Name.Contains(searchTerm) && p.Price >= start && p.Price <= end && p.ProductColors.Select(pc => pc.Color.ColorID).Contains(colorId))
                                                     .OrderBy(p => p.ProductID).Skip((page - 1) * PageSize).Take(PageSize);

                        totItems = start == 0 && end == 0 ? repository.Products.Where(p => p.Name.Contains(searchTerm) && p.ProductColors.Select(pc => pc.Color.ColorID).Contains(colorId)).Count() : repository.Products.Where(p => p.Name.Contains(searchTerm) && p.Price >= start && p.Price <= end && p.ProductColors.Select(pc => pc.Color.ColorID).Contains(colorId)).Count();

                        //string colorName = repository.Colors.Where(c => c.ColorID == colorId).Select(cc => cc.Name).SingleOrDefault();
                        Session["Color"] = colorName;

                    }
                    else
                    {
                        prods = repository.Products.Where(p => p.Name.Contains(searchTerm))
                                                     .Where(p => p.Price >= start && p.Price <= end)
                                                     .OrderBy(p => p.ProductID).Skip((page - 1) * PageSize).Take(PageSize);

                        totItems = start == 0 && end == 0 ? repository.Products.Where(p => p.Name.Contains(searchTerm)).Count() : repository.Products.Where(p => p.Name.Contains(searchTerm)).Where(p => p.Price >= start && p.Price <= end).Count();
                    }
                }
                else
                {
                    if (sizeId != 0 && colorId != 0)
                    {

                        prods = repository.Products.Where(p => p.Name.Contains(searchTerm) && p.ProductSizes.Select(ps => ps.Size.SizeID).Contains(sizeId) && p.ProductColors.Select(pc => pc.Color.ColorID).Contains(colorId))
                                          .OrderBy(p => p.ProductID).Skip((page - 1) * PageSize).Take(PageSize).ToList();

                        totItems = category == null ? repository.Products.Where(p => p.Name.Contains(searchTerm) && p.ProductSizes.Select(ps => ps.Size.SizeID).Contains(sizeId) && p.ProductColors.Select(pc => pc.Color.ColorID).Contains(colorId)).Count() : repository.Products.Where(x => x.Category == category && x.Name.Contains(searchTerm) && x.ProductSizes.Select(ps => ps.Size.SizeID).Contains(sizeId) && x.ProductColors.Select(pc => pc.Color.ColorID).Contains(colorId)).Count();

                        //Session["Size"] = sizeName;
                        //Session["Color"] = colorName;
                        //Session["SearchTerm"] = searchTerm;
                    }
                    else if (sizeId != 0 && colorId==0)
                    {

                        prods = repository.Products.Where(p => p.Name.Contains(searchTerm) && p.ProductSizes.Select(ps => ps.Size.SizeID).Contains(sizeId))
                                          .OrderBy(p => p.ProductID).Skip((page - 1) * PageSize).Take(PageSize).ToList();

                        totItems = category == null ? repository.Products.Where(p => p.Name.Contains(searchTerm) && p.ProductSizes.Select(ps => ps.Size.SizeID).Contains(sizeId)).Count() : repository.Products.Where(x => x.Category == category && x.Name.Contains(searchTerm) && x.ProductSizes.Select(ps => ps.Size.SizeID).Contains(sizeId)).Count();

                    }
                    else if (colorId != 0 && sizeId==0)
                    {

                        prods = repository.Products.Where(p => p.Name.Contains(searchTerm) && p.ProductColors.Select(pc => pc.Color.ColorID).Contains(colorId))
                                          .OrderBy(p => p.ProductID).Skip((page - 1) * PageSize).Take(PageSize).ToList();


                        totItems = category == null ? repository.Products.Where(p => p.Name.Contains(searchTerm) && p.ProductColors.Select(pc => pc.Color.ColorID).Contains(colorId)).Count() : repository.Products.Where(x => x.Category == category && x.Name.Contains(searchTerm) && x.ProductColors.Select(pc => pc.Color.ColorID).Contains(colorId)).Count();

                    }
                    else
                    {
                        prods = repository.Products
                                        .Where(p => p.Name.Contains(searchTerm))
                                        .OrderBy(p => p.ProductID).Skip((page - 1) * PageSize).Take(PageSize).ToList();

                        totItems = category == null ? repository.Products.Where(y => y.Name.Contains(searchTerm)).Count() : repository.Products.Where(x => x.Category == category && x.Name.Contains(searchTerm)).Count();
                    }
                }
            }
            else
            {
                if (start > 0 || end > 0)
                {

                    //*************
                      if (sizeId != 0 && colorId != 0)
                        {
                            prods = repository.Products.Where(p =>p.Price >= start && p.Price <= end && p.ProductSizes.Select(ps => ps.Size.SizeID).Contains(sizeId) && p.ProductColors.Select(pc => pc.Color.ColorID).Contains(colorId))
                                                        .OrderBy(p => p.ProductID).Skip((page - 1) * PageSize).Take(PageSize);

                            totItems = start == 0 && end == 0 ? repository.Products.Where(p =>  p.ProductSizes.Select(ps => ps.Size.SizeID).Contains(sizeId) && p.ProductColors.Select(pc => pc.Color.ColorID).Contains(colorId)).Count() : repository.Products.Where(p =>  p.Price >= start && p.Price <= end && p.ProductSizes.Select(ps => ps.Size.SizeID).Contains(sizeId) && p.ProductColors.Select(pc => pc.Color.ColorID).Contains(colorId)).Count();

                            //string sizeName = repository.Sizes.Where(s => s.SizeID == sizeId).Select(ss => ss.ShortName).SingleOrDefault();
                            Session["Size"] = sizeName;

                            //string colorName = repository.Colors.Where(c => c.ColorID == colorId).Select(cc => cc.Name).SingleOrDefault();
                            Session["Color"] = colorName;

                        }

                        else if (sizeId != 0 && colorId == 0)
                        {
                            prods = repository.Products.Where(p =>  p.Price >= start && p.Price <= end && p.ProductSizes.Select(ps => ps.Size.SizeID).Contains(sizeId))
                                                         .OrderBy(p => p.ProductID).Skip((page - 1) * PageSize).Take(PageSize);

                            totItems = start == 0 && end == 0 ? repository.Products.Where(p =>  p.ProductSizes.Select(ps => ps.Size.SizeID).Contains(sizeId)).Count() : repository.Products.Where(p => p.Price >= start && p.Price <= end && p.ProductSizes.Select(ps => ps.Size.SizeID).Contains(sizeId)).Count();

                            //string sizeName = repository.Sizes.Where(s => s.SizeID == sizeId).Select(ss => ss.ShortName).SingleOrDefault();
                            Session["Size"] = sizeName;

                        }
                        else if (colorId != 0 && sizeId == 0)
                        {
                            prods = repository.Products.Where(p =>p.Price >= start && p.Price <= end && p.ProductColors.Select(pc => pc.Color.ColorID).Contains(colorId))
                                                         .OrderBy(p => p.ProductID).Skip((page - 1) * PageSize).Take(PageSize);

                            totItems = start == 0 && end == 0 ? repository.Products.Where(p =>  p.ProductColors.Select(pc => pc.Color.ColorID).Contains(colorId)).Count() : repository.Products.Where(p =>  p.Price >= start && p.Price <= end && p.ProductColors.Select(pc => pc.Color.ColorID).Contains(colorId)).Count();

                            //string colorName = repository.Colors.Where(c => c.ColorID == colorId).Select(cc => cc.Name).SingleOrDefault();
                            Session["Color"] = colorName;

                        }
                       //*****************
                        else
                        {
                            prods = repository.Products
                                                           .Where(p => p.Price >= start && p.Price <= end)
                                                           .OrderBy(p => p.ProductID).Skip((page - 1) * PageSize).Take(PageSize);

                            totItems = start == 0 && end == 0 ? repository.Products.Count() : repository.Products.Where(p => p.Price >= start && p.Price <= end).Count();
                        }
                    //***************
                }
                else
                {
                    if (sizeId != 0 || colorId != 0)
                    {
                        if (sizeId != 0 && colorId != 0)
                        {

                            prods = repository.Products.Where(p => p.ProductSizes.Select(ps => ps.Size.SizeID).Contains(sizeId) && p.ProductColors.Select(pc => pc.Color.ColorID).Contains(colorId))
                                              .OrderBy(p => p.ProductID).Skip((page - 1) * PageSize).Take(PageSize).ToList();

                            totItems = category == null ? repository.Products.Where(p => p.ProductSizes.Select(ps => ps.Size.SizeID).Contains(sizeId) && p.ProductColors.Select(pc => pc.Color.ColorID).Contains(colorId)).Count() : repository.Products.Where(x => x.Category == category && x.ProductSizes.Select(ps => ps.Size.SizeID).Contains(sizeId) && x.ProductColors.Select(pc => pc.Color.ColorID).Contains(colorId)).Count();

                            //Session["Size"] = sizeName;
                            //Session["Color"] = colorName;
                            //Session["SearchTerm"] = searchTerm;

                        }
                        else if (sizeId > 0 && colorId == 0)
                        {
                            prods = repository.ProductSizes.Where(ps => ps.Size.SizeID == sizeId).Select(p => p.Product).OrderBy(p => p.ProductID).Skip((page - 1) * PageSize).Take(PageSize);

                            // totItems = category == null ? repository.Products.Where(y => y.Name.Contains(searchTerm)).Count() : repository.Products.Where(x => x.Category == category && x.Name.Contains(searchTerm)).Count();
                            totItems = category == null ? repository.ProductSizes.Where(ps => ps.Size.SizeID == sizeId).Select(p => p.Product).Count() : repository.ProductSizes.Where(ps => ps.Size.SizeID == sizeId).Select(p => p.Product).Where(pp => pp.Category == category).Count();


                            //Session["Size"] = sizeName;
                        }
                        else if (colorId > 0 && sizeId == 0)
                        {
                            //prods = repository.ProductSizes.Where(ps => ps.Size.SizeID == sizeId).Select(p => p.Product).OrderBy(p => p.ProductID).Skip((page - 1) * PageSize).Take(PageSize);
                            prods = repository.ProductColors.Where(pc => pc.Color.ColorID == colorId).Select(p => p.Product).OrderBy(p => p.ProductID).Skip((page - 1) * PageSize).Take(PageSize);

                            //totItems = category == null ? repository.ProductSizes.Where(ps => ps.Size.SizeID == sizeId).Select(p => p.Product).Count() : repository.ProductSizes.Where(ps => ps.Size.SizeID == sizeId).Select(p => p.Product).Where(pp => pp.Category == category).Count();
                            totItems = category == null ? repository.ProductColors.Where(pc => pc.Color.ColorID == colorId).Select(p => p.Product).Count() : repository.ProductColors.Where(pc => pc.Color.ColorID == colorId).Select(p => p.Product).Where(pp => pp.Category == category).Count();
                        }

                        else
                        {
                            prods = repository.Products
                                        .Where(p => category == null || p.Category == category)
                                        .OrderBy(p => p.ProductID).Skip((page - 1) * PageSize).Take(PageSize);

                            totItems = category == null ? repository.Products.Count() : repository.Products.Where(x => x.Category == category).Count();
                            //Session["SearchTerm"] = null;
                        }

                    }
                }
            }

            model = new ProductsListViewModel
            {
                Products = prods,

                PagingInfo = new PagingInfo
                {
                    CurrentPage = page,
                    ItemsPerPage = PageSize,
                    TotalItems = totItems,
                    SizeId = sizeId,
                    ColorId = colorId,
                    StartPrice=start,
                    EndPrice=end
                },

                CurrentCategory = category,
                SearchTerm=searchTerm
            };
            return PartialView("ProductsList", model);
                       
        }

        public PartialViewResult GetallView(string searchTerm,string category, int page = 1, int sizeId = 0, int colorId = 0,int start=0,int end=0)
        {
            IEnumerable<Product> prods = null;
            int totItems = 0;
            ProductsListViewModel model = null;

            //if (sizeId == 0)
            //{

            //}


            if (sizeId == 0 || colorId == 0)
            {
                if (searchTerm != null && searchTerm != "")
                {
                    prods = repository.Products
                                        .Where(p => p.Name.Contains(searchTerm))
                                        .OrderBy(p => p.ProductID).Skip((page - 1) * PageSize).Take(PageSize).ToList();

                    totItems = category == null ? repository.Products.Where(y => y.Name.Contains(searchTerm)).Count() : repository.Products.Where(x => x.Category == category && x.Name.Contains(searchTerm)).Count();
                }

                else
                {

                    prods = repository.Products
                                               .Where(p => category == null || p.Category == category)
                                               .OrderBy(p => p.ProductID).Skip((page - 1) * PageSize).Take(PageSize);

                    totItems = category == null ? repository.Products.Count() : repository.Products.Where(x => x.Category == category).Count();
                }

            }


            model = new ProductsListViewModel
            {
                Products = prods,

                PagingInfo = new PagingInfo
                {
                    CurrentPage = page,
                    ItemsPerPage = PageSize,
                    TotalItems = totItems,
                    SizeId = sizeId,
                    ColorId = colorId,
                    StartPrice = start,
                    EndPrice = end
                },

                CurrentCategory = category,
                SearchTerm = searchTerm
            };
            return PartialView("ProductsList", model);
        }

   

        public PartialViewResult GetallByPriceRange(string searchTerm, int start = 0, int end = 0, int page = 1, string category = null, int sizeId = 0, int colorId = 0)
        {
            IEnumerable<Product> prods = null;
            int totItems = 0;
            ProductsListViewModel model = null;
            int st = Convert.ToInt32(start);
            int en = Convert.ToInt32(end);

            //Session["Start"] = st;
            //Session["End"] = en;

            if (searchTerm != null && searchTerm != "")
            {
                if (sizeId != 0 && colorId != 0)
                {
                    prods = repository.Products.Where(p => p.Name.Contains(searchTerm) && p.Price >= start && p.Price <= end && p.ProductSizes.Select(ps => ps.Size.SizeID).Contains(sizeId) && p.ProductColors.Select(pc => pc.Color.ColorID).Contains(colorId))
                                                .OrderBy(p => p.ProductID).Skip((page - 1) * PageSize).Take(PageSize);

                    totItems = start == 0 && end == 0 ? repository.Products.Where(p => p.Name.Contains(searchTerm) && p.ProductSizes.Select(ps => ps.Size.SizeID).Contains(sizeId) && p.ProductColors.Select(pc => pc.Color.ColorID).Contains(colorId)).Count() : repository.Products.Where(p => p.Name.Contains(searchTerm) && p.Price >= start && p.Price <= end && p.ProductSizes.Select(ps => ps.Size.SizeID).Contains(sizeId) && p.ProductColors.Select(pc => pc.Color.ColorID).Contains(colorId)).Count();

                    string sizeName = repository.Sizes.Where(s => s.SizeID == sizeId).Select(ss => ss.ShortName).SingleOrDefault();
                    Session["Size"] = sizeName;

                    string colorName = repository.Colors.Where(c => c.ColorID == colorId).Select(cc => cc.Name).SingleOrDefault();
                    Session["Color"] = colorName;

                }

                else if (sizeId != 0 && colorId==0)
                {
                    prods = repository.Products.Where(p => p.Name.Contains(searchTerm) && p.Price >= start && p.Price <= end && p.ProductSizes.Select(ps => ps.Size.SizeID).Contains(sizeId))
                                                 .OrderBy(p => p.ProductID).Skip((page - 1) * PageSize).Take(PageSize);

                    //totItems = start == 0 && end == 0 ? repository.Products.Where(p => p.Name.Contains(searchTerm) && p.ProductSizes.Select(ps => ps.Size.SizeID).Contains(sizeId)).Count() : repository.Products.Where(p => p.Name.Contains(searchTerm)).Where(p => p.Price >= start && p.Price <= end && p.ProductSizes.Select(ps => ps.Size.SizeID).Contains(sizeId)).Count();

                    totItems = start == 0 && end == 0 ? repository.Products.Where(p => p.Name.Contains(searchTerm) && p.ProductSizes.Select(ps => ps.Size.SizeID).Contains(sizeId)).Count() : repository.Products.Where(p => p.Name.Contains(searchTerm) && p.Price >= start && p.Price <= end && p.ProductSizes.Select(ps => ps.Size.SizeID).Contains(sizeId)).Count();

                    string sizeName = repository.Sizes.Where(s => s.SizeID == sizeId).Select(ss => ss.ShortName).SingleOrDefault();
                    Session["Size"] = sizeName;

                }
                else if (colorId != 0 && sizeId==0)
                {
                    prods = repository.Products.Where(p => p.Name.Contains(searchTerm) && p.Price >= start && p.Price <= end && p.ProductColors.Select(pc => pc.Color.ColorID).Contains(colorId))
                                                 .OrderBy(p => p.ProductID).Skip((page - 1) * PageSize).Take(PageSize);

                    totItems = start == 0 && end == 0 ? repository.Products.Where(p => p.Name.Contains(searchTerm) && p.ProductColors.Select(pc => pc.Color.ColorID).Contains(colorId)).Count() : repository.Products.Where(p => p.Name.Contains(searchTerm) && p.Price >= start && p.Price <= end && p.ProductColors.Select(pc => pc.Color.ColorID).Contains(colorId)).Count();

                    string colorName = repository.Colors.Where(c => c.ColorID == colorId).Select(cc => cc.Name).SingleOrDefault();
                    Session["Color"] = colorName;

                }
                else
                {
                    prods = repository.Products.Where(p => p.Name.Contains(searchTerm))
                                                 .Where(p => p.Price >= st && p.Price <= en)
                                                 .OrderBy(p => p.ProductID).Skip((page - 1) * PageSize).Take(PageSize);

                    totItems = st == 0 && en == 0 ? repository.Products.Where(p => p.Name.Contains(searchTerm)).Count() : repository.Products.Where(p => p.Name.Contains(searchTerm)).Where(p => p.Price >= st && p.Price <= en).Count();
                }

            }
           //***************************
            else if (sizeId != 0 && colorId != 0)
            {
                prods = repository.Products.Where(p =>p.Price >= start && p.Price <= end && p.ProductSizes.Select(ps => ps.Size.SizeID).Contains(sizeId) && p.ProductColors.Select(pc => pc.Color.ColorID).Contains(colorId))
                                            .OrderBy(p => p.ProductID).Skip((page - 1) * PageSize).Take(PageSize);

                totItems = start == 0 && end == 0 ? repository.Products.Where(p =>  p.ProductSizes.Select(ps => ps.Size.SizeID).Contains(sizeId) && p.ProductColors.Select(pc => pc.Color.ColorID).Contains(colorId)).Count() : repository.Products.Where(p =>  p.Price >= start && p.Price <= end && p.ProductSizes.Select(ps => ps.Size.SizeID).Contains(sizeId) && p.ProductColors.Select(pc => pc.Color.ColorID).Contains(colorId)).Count();

                string sizeName = repository.Sizes.Where(s => s.SizeID == sizeId).Select(ss => ss.ShortName).SingleOrDefault();
                Session["Size"] = sizeName;

                string colorName = repository.Colors.Where(c => c.ColorID == colorId).Select(cc => cc.Name).SingleOrDefault();
                Session["Color"] = colorName;

            }

            else if (sizeId != 0 && colorId == 0)
            {
                prods = repository.Products.Where(p =>  p.Price >= start && p.Price <= end && p.ProductSizes.Select(ps => ps.Size.SizeID).Contains(sizeId))
                                             .OrderBy(p => p.ProductID).Skip((page - 1) * PageSize).Take(PageSize);

                totItems = start == 0 && end == 0 ? repository.Products.Where(p =>  p.ProductSizes.Select(ps => ps.Size.SizeID).Contains(sizeId)).Count() : repository.Products.Where(p => p.Price >= start && p.Price <= end && p.ProductSizes.Select(ps => ps.Size.SizeID).Contains(sizeId)).Count();

                string sizeName = repository.Sizes.Where(s => s.SizeID == sizeId).Select(ss => ss.ShortName).SingleOrDefault();
                Session["Size"] = sizeName;

            }
            else if (colorId != 0 && sizeId == 0)
            {
                prods = repository.Products.Where(p =>p.Price >= start && p.Price <= end && p.ProductColors.Select(pc => pc.Color.ColorID).Contains(colorId))
                                             .OrderBy(p => p.ProductID).Skip((page - 1) * PageSize).Take(PageSize);

                totItems = start == 0 && end == 0 ? repository.Products.Where(p =>  p.ProductColors.Select(pc => pc.Color.ColorID).Contains(colorId)).Count() : repository.Products.Where(p =>  p.Price >= start && p.Price <= end && p.ProductColors.Select(pc => pc.Color.ColorID).Contains(colorId)).Count();

                string colorName = repository.Colors.Where(c => c.ColorID == colorId).Select(cc => cc.Name).SingleOrDefault();
                Session["Color"] = colorName;

            }
           //*****************
            else
            {
                prods = repository.Products
                                               .Where(p => p.Price >= st && p.Price <= en)
                                               .OrderBy(p => p.ProductID).Skip((page - 1) * PageSize).Take(PageSize);

                totItems = st == 0 && en == 0 ? repository.Products.Count() : repository.Products.Where(p => p.Price >= st && p.Price <= en).Count();
            }

            model = new ProductsListViewModel
            {
                Products = prods,

                PagingInfo = new PagingInfo
                {
                    CurrentPage = page,
                    ItemsPerPage = PageSize,
                    TotalItems = totItems,
                    StartPrice = st,
                    EndPrice = en,
                    SizeId=sizeId,
                    ColorId=colorId
                },

                CurrentCategory = category,
                SearchTerm = searchTerm
            };




            return PartialView("ProductsList", model);
        }
        
    }
}
