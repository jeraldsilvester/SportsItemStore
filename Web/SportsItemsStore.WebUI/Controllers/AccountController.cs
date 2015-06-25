using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SportsItemsStore.Domain.Abstract;
using SportsItemsStore.Domain.Entities;
using SportsItemsStore.WebUI.Models;
using System.Web.Security;

namespace SportsItemsStore.WebUI.Controllers
{
    public class AccountController : Controller
    {
        private IProductsRepository repository;

        public AccountController(IProductsRepository repo)
        {
            repository = repo;
        }

        public ActionResult Index()
        {
            

            return View();
        }

        public ViewResult Login(string returnUrl)
        {
            LoginViewModel model = new LoginViewModel();
            model.Username = string.Empty;
            model.Password = string.Empty;
            model.returnUrl = returnUrl;
            this.Session["User"] = null;

            return View(model);
        }

        [HttpPost]
        public ActionResult Login(LoginViewModel model,string returnUrl)
        {
            if (ModelState.IsValid)
            {
                User user = repository.Users.FirstOrDefault(item => item.Username.ToLower() == model.Username.ToLower() && item.Password == model.Password);

                if (user == null || user.Password != model.Password)
                {
                    ModelState.AddModelError("","The Username or password id correct");
                    return View(model);
                }
                else
                {
                 
                    FormsAuthentication.SetAuthCookie(model.Username, false);

                    Session["User"] = user;

                    //return Redirect(returnUrl ?? @Html.ActionLink("Checkout", "Index", "Cart"));

                    if (returnUrl != "" && returnUrl !=null && returnUrl !="/Product/ViewDetails")
                    {
                        return Redirect(returnUrl);
                    }
                    else
                    {
                        return RedirectToAction("List", "Product");
                    }
                }
            }
            return View(model);
        }

        [Authorize]
        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();
   
            this.Session["User"] = null;
            return RedirectToAction("List", "Product");
        }

        public ActionResult Register(string returnUrl)
        {
            UserRegistrationModel registerUser = new UserRegistrationModel();

            return View(registerUser);
        }

        [HttpPost]
        public ViewResult Register(UserRegistrationModel model,string returnUrl)
        {
            if (ModelState.IsValid)
            {
                User existUser = repository.Users.FirstOrDefault(item => item.Username.ToLower() == model.Username.ToLower());
                if (existUser == null)
                {

                    User user = new User();
                    user.Username = model.Username;
                    user.Password = model.Password;
                    user.Email = model.Email;

                    repository.SaveUser(user);

                    //int UserId = user.ID;
                    

                    ViewBag.returnUrl = returnUrl;
                    return View("RegistrationCompleted");
                }
                else
                {
                    ModelState.AddModelError("", "An User with same User name already exists");
                    //
                    return View(model);
                }
            }
            //
            // If we got this far, something failed, redisplay form!
            //
            return View(model);
        }
    }
}
