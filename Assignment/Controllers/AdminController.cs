using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Assignment.Models;
namespace Assignment.Controllers
{
   
    public class AdminController : Controller
    {
        private StudentAluminiEntities1 db = new StudentAluminiEntities1();
        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult AddUsersToRoles()
        {
            if (Roles.IsUserInRole("Administrator"))
            {
                List<String> users = db.MemberDetails.Where(m => m.Approved == true).Select(m => m.UserName).ToList();
                ViewBag.user = users;
                return View();
            }
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public ActionResult AddUsersToRoles(AssignRoles assignRoles)
        {
            //cleaning previously assigned Roles
            if (Roles.GetRolesForUser(assignRoles.Secretary).Length != 0)
            {
                Roles.RemoveUserFromRoles(assignRoles.Secretary, Roles.GetRolesForUser(assignRoles.Secretary));
            }
            if (Roles.GetRolesForUser(assignRoles.Treasurer).Length != 0)
            {
                Roles.RemoveUserFromRoles(assignRoles.Treasurer, Roles.GetRolesForUser(assignRoles.Treasurer));
            }
            if (Roles.GetRolesForUser(assignRoles.Committee_member).Length != 0)
            {
                Roles.RemoveUserFromRoles(assignRoles.Committee_member, Roles.GetRolesForUser(assignRoles.Committee_member));
            }

            //Clean previous role holders to normal level
            String[] arr1 = Roles.GetUsersInRole("Secretary");
            String[] arr2 = Roles.GetUsersInRole("Committee member");
            String[] arr3 = Roles.GetUsersInRole("Treasurer");
            if (arr1.Length != 0)
            {
                Roles.RemoveUsersFromRole(Roles.GetUsersInRole("Secretary"), "Secretary");
                Roles.AddUsersToRole(arr1, "Normal");
            }
            if (arr2.Length != 0)
            {
                Roles.RemoveUsersFromRole(Roles.GetUsersInRole("Committee member"), "Committee member");
                Roles.AddUsersToRole(arr2, "Normal");
            }
            if (arr3.Length != 0)
            {
                Roles.RemoveUsersFromRole(Roles.GetUsersInRole("Treasurer"), "Treasurer");
                Roles.AddUsersToRole(arr3, "Treasurer");
            }

            Roles.AddUserToRole(assignRoles.Secretary, "Secretary");
            Roles.AddUserToRole(assignRoles.Committee_member, "Committee member");
            Roles.AddUserToRole(assignRoles.Treasurer, "Treasurer");
            return RedirectToAction("Index", "Home");
        }
    }
}