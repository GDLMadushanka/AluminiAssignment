using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Assignment.Controllers
{
    public class NotificationController : Controller
    {
        private StudentAluminiEntities1 db = new StudentAluminiEntities1();
        // GET: Notification
        public ActionResult caughtNotification(String notificationID)
        {

            int num = int.Parse(notificationID);
            var original = db.Notifications.Find(num);

            if (original != null)
            {
                original.Caught = true;
                db.SaveChanges();
            }




           
            return Json(null, JsonRequestBehavior.AllowGet);
        }

    }
}