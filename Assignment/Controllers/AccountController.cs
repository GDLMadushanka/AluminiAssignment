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
                    Session["Logged"] = true;
                    var current = db.MemberDetails.Where(m => m.UserName == userdata.UserName).ToList();


                  
                    if (current.Count > 0)
                    {
                        if (returnUrl != null)
                        {
                            return Redirect(returnUrl);
                        }
                        if (current.ElementAt(0).Approved)
                        {
                            Session["FirstName"] = current.ElementAt(0).FirstName;
                            Session["LastName"] = current.ElementAt(0).LastName;

                            return RedirectToAction("Home", "Account");
                        }
                        else { ModelState.AddModelError("", "Sorry! Your account not yet approved"); WebSecurity.Logout(); }
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
                    Roles.AddUserToRole(userdata.UserName, "Normal");
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

        public ActionResult addNominations()
        {
            String username = Membership.GetUser().UserName;
            Nomination temp = db.Nominations.Find(username);
            if (temp == null)
            {
                List<String> users = db.MemberDetails.Where(m => m.Approved == true).Select(c => c.UserName).ToList();
                users.Remove("Lahiru"); // removing administrator
                ViewBag.user = users;
                return View();
            }
            else return RedirectToAction("Home","Account");
        } 


        [HttpPost]
        public ActionResult addNominations(Nomination nomination)
        {
            nomination.UserName = Membership.GetUser().UserName;
            nomination.Nomination1AcceptState = false;
            nomination.Nomination2AcceptState = false;
            nomination.AdminAcceptState = false;
            nomination.Approved = false;
            db.Nominations.Add(nomination);
            db.SaveChanges();

            // creating notifications for nominated users

            Notification n1 = new Notification();
            n1.UserName = Membership.GetUser().UserName;
            n1.Caught = false;
            n1.Destination = nomination.Nomination1;
            n1.NotificationText = Membership.GetUser().UserName + " request you to verify his account";

            Notification n2 = new Notification();
            n2.UserName = Membership.GetUser().UserName;
            n2.Caught = false;
            n2.Destination = nomination.Nomination2;
            n2.NotificationText = Membership.GetUser().UserName + " request you to verify his account";

            db.Notifications.Add(n1);
            db.Notifications.Add(n2);
            db.SaveChanges();

            return RedirectToAction("Index", "Home");
        }
        public ActionResult LogOut()
        {
            WebSecurity.Logout();
            return RedirectToAction("Index", "Home");
        }


        public ActionResult Home()
        {
            if ((bool)Session["Logged"])
            {
                String username = Membership.GetUser().UserName;
                String[] roleList=   Roles.GetRolesForUser(username);
                ViewBag.role = roleList[0];
                var notifications = db.Notifications.Where(m => m.Destination.Equals(username) && m.Caught == false ).ToList();
                ViewBag.notifications = notifications;
                return View();
            }
            return RedirectToAction("Index", "Home");
        }
    }
}