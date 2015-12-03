using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Assignment.Models;
using WebMatrix.WebData;
namespace Assignment.Controllers
{
    public class AccountController : Controller
    {
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(UserLogin userdata,String returnUrl)
        {
            if (ModelState.IsValid)
            {
                if (WebSecurity.Login(userdata.UserName, userdata.Password))
                {
                    if (returnUrl != null)
                    {
                        return Redirect(returnUrl);
                    }
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Sorry the user name or password is incorrect");
                    return View(userdata);
                }
            }
            ModelState.AddModelError("", "Sorry the user name or password is incorrect");
            return View(userdata);
        }
        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(UserLogin userdata)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    WebSecurity.CreateUserAndAccount(userdata.UserName, userdata.Password);
                }
                catch (System.Web.Security.MembershipCreateUserException ex)
                {
                    ModelState.AddModelError("", "Sorry the user name alredy exists");
                    return View(userdata);
                }
                return RedirectToAction("Index", "Home");
            }
            ModelState.AddModelError("", "Sorry the user name alredy exists");
            return View(userdata);
            
        }

        public ActionResult LogOut()
        {
            WebSecurity.Logout();
            return RedirectToAction("Index", "Home");
        }
    }
}