using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Assignment.Controllers
{
    public class NominationController : Controller
    {
        private StudentAluminiEntities1 db = new StudentAluminiEntities1();
        // GET: Nomination
        public ActionResult Accept(String noticeId)
        {
            var original = db.Nominations.Find(noticeId);
            String user = Membership.GetUser().UserName;
            String[] roles=Roles.GetRolesForUser(user);
           
                if (original != null)
                {
                    if (original.Nomination1.Equals(user))
                    {
                        original.Nomination1AcceptState = true;
                    }
                    else if (original.Nomination2.Equals(user))
                    {
                        original.Nomination2AcceptState = true;
                    }
                    db.SaveChanges();

                    if (original.Nomination1AcceptState && original.Nomination2AcceptState)
                    {
                        //String[] admin = Roles.GetUsersInRole("Administrator");
                        //String admini = admin[0];

                        //Notification allVerified = new Notification();
                        //allVerified.Destination = admini;
                        //allVerified.NotificationText = "Validate the account of mr/mrs " + original.UserName;
                        //allVerified.Caught = false;

                        //db.Notifications.Add(allVerified);
                        var user2 = db.MemberDetails.Find(noticeId);
                        if (user2 != null)
                        {
                            user2.Approved = true;

                        }
                        db.SaveChanges();
               


                    }
                }
            

         


            return Json(null, JsonRequestBehavior.AllowGet);
        }
    }
}