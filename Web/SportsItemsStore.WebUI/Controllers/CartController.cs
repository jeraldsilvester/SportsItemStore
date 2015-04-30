using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SportsItemsStore.Domain.Abstract;
using SportsItemsStore.Domain.Entities;
using SportsItemsStore.WebUI.Models;
using System.Text;

namespace SportsItemsStore.WebUI.Controllers
{
    public class CartController : Controller
    {
        private IProductsRepository repository;

        public CartController(IProductsRepository repo)
        {
            repository = repo;
        }

        public ViewResult Index(Cart cart, string returnUrl)
        {
            return View(new CartIndexViewModel
            {
                Cart = cart,
                ReturnUrl = returnUrl
            });
        }

        //private Cart GetCart()
        //{
        //    Cart cart = (Cart)Session["Cart"];
        //    if (cart == null)
        //    {
        //        cart = new Cart();
        //        Session["Cart"] = cart;
        //    }
        //    return cart;
        //}

        public RedirectToRouteResult AddToCart(Cart cart, int productId, string returnUrl, string sizeId, string colorId, string mnfcId)
        {
            Product product = repository.Products.FirstOrDefault(p => p.ProductID == productId);

            int szId = Convert.ToInt32(sizeId == "" ? "0" : sizeId);
            int clrId =Convert.ToInt32( colorId == "" ? "0" : colorId);
            int mnId = Convert.ToInt32(mnfcId == "" ? "0" : mnfcId);

            string size = repository.Sizes.Where(x => x.SizeID == szId).Select(y=>y.ShortName).SingleOrDefault();
            string color = repository.Colors.Where(x => x.ColorID == clrId).Select(y=>y.Name).SingleOrDefault();
            string mnfc = repository.Manufacturers.Where(x => x.ManufacturerID == mnId).Select(y => y.Name).SingleOrDefault();

            if (product != null)
            {
                cart.AddItem(product, 1, size,color,mnfc,szId,clrId,mnId,"",DateTime.Now);
            }

            //returnUrl = returnUrl.Contains("GetallByPriceRange") ? "/Product/List" : returnUrl;

            returnUrl = Url.Action("List", "Product");

            //returnUrl = returnUrl.Contains("GetallByPriceRange") ? returnUrl.Replace("GetallByPriceRange", "List") : returnUrl;

           //returnUrl = returnUrl.Contains("SearchWithFilters") ? returnUrl.Replace("SearchWithFilters","List") : returnUrl;

            

            return RedirectToAction("Index", new { returnUrl });
        }

        public RedirectToRouteResult RemoveFromCart(Cart cart, int productId, string returnUrl, int sizeId, int colorId, int mnfcId)
        {
            Product product = repository.Products.FirstOrDefault(p => p.ProductID == productId);
            
            if (product != null)
            {
                cart.RemoveLine(product,sizeId,colorId,mnfcId);
            }
            return RedirectToAction("Index", new { returnUrl });
        }

        [ChildActionOnly]
        public PartialViewResult Summary(Cart cart)
        {
            int userId=0;
            
            if (Session["User"] != null)
            {
                User user = (User)Session["User"];
                userId = user.ID;
            }

            if (userId > 0)
            {
                if (repository.Orders.Any(o => o.UserId == userId))
                {
                    ViewBag.UserId = userId;
                }
            }
        
            return PartialView(cart);
        }

        public ViewResult MyOrder(string returnUrl, int userId)
        {
            IList<Order> orders = repository.Orders.Where(o => o.UserId == userId).OrderByDescending(x=>x.OrderId).Take(20).ToList();
            return View(orders);
        }

        [HttpPost]
        public PartialViewResult MyOrderDetails(int orderId)
        {
            var  orderDetails = repository.OrderDetails.Where(o => o.Order.OrderId == orderId).ToList();
            return PartialView(orderDetails);
        }

        [ChildActionOnly]
        public void ClearCart(Cart cart)
        {
            if (cart != null)
            {
                cart.Clear();
            }
        }

        [Authorize]
        public ViewResult Checkout()
        {
            return View(new ShippingDetails());
        }

        [HttpPost]
        public ViewResult Checkout(Cart cart, ShippingDetails shippingDetails)
        {
            if (cart.Lines.Count() == 0)
            {
                ModelState.AddModelError("", "Sorry, your cart is empty!");
            }
            if (ModelState.IsValid)
            {
                Address addrs = new Address {
                     Name =shippingDetails.Name,
                     Line1=shippingDetails.Line1,
                     Line2=shippingDetails.Line2,
                     Line3=shippingDetails.Line3,
                     City=shippingDetails.City,
                     State=shippingDetails.State,
                     Zip=shippingDetails.Zip,
                     Country=shippingDetails.Country,
                     UserId=shippingDetails.UserId
                };

                repository.SaveAddress(addrs);

               

                if (addrs.AddressID > 0)
                {
                    
                    Order order = new Order
                    {
                        AddressId = addrs.AddressID,
                        UserId = shippingDetails.UserId,
                        OrderDate=DateTime.Now
                    };
                    List<OrderDetail> orderDetails = new List<OrderDetail>();
                    foreach (CartLine line in cart.Lines)
                    {
                        OrderDetail orderDetail = new OrderDetail
                        {
                            ProductId = line.Product.ProductID,
                            Quantity=line.Quantity,
                            SizeId = line.SizeId,
                            ColorId = line.ColorId,
                            ManufacturerId = line.ManufactererId,
                            SubTotal=(line.Product.Price * line.Quantity)
                        };
                        order.OrderTotal += orderDetail.SubTotal;

                        orderDetails.Add(orderDetail);
                    }

                    order.OrderDetails = orderDetails;

                   repository.SaveOrder(order);
                }

                return View("Completed", new ShippingViewModel
                {
                    crt=cart,
                    shipDtls=shippingDetails
                });
            }
            else
            {
                return View(shippingDetails);
            }
        }
    }
}
