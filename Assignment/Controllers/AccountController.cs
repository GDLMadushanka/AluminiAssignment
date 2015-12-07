using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Assignment.Models;
using WebMatrix.WebData;
namespace Assignment.Controllers
{
    public class AccountController : Controller
    {
        private StudentAluminiEntities1 db = new StudentAluminiEntities1();
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
                    var current = db.MemberDetails.Where(m => m.UserName == userdata.UserName);
                    if (current.ToList().Count > 0)
                    {
                        if (returnUrl != null)
                        {
                            return Redirect(returnUrl);
                        }
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        return RedirectToAction("Create", "MemberDetails");
                    }
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
                    String[] names=new string[1];
                    names[0] = userdata.UserName;
                    Roles.AddUsersToRole(names, "Normal");
                }
                catch (System.Web.Security.MembershipCreateUserException ex)
                {
                    ModelState.AddModelError("", "Sorry the user name alredy exists");
                    return View(userdata);
                }
                return RedirectToAction("Login", "Account");
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