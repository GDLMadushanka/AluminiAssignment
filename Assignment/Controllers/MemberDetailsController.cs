using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Assignment;

namespace Assignment.Controllers
{
    public class MemberDetailsController : Controller
    {
        private StudentAluminiEntities db = new StudentAluminiEntities();

        // GET: MemberDetails
        public ActionResult Index()
        {
            return View(db.MemberDetails.ToList());
        }

        // GET: MemberDetails/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MemberDetail memberDetail = db.MemberDetails.Find(id);
            if (memberDetail == null)
            {
                return HttpNotFound();
            }
            return View(memberDetail);
        }

        // GET: MemberDetails/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: MemberDetails/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "UserName,FirstName,LastName,EmailName,Mobile,NIC,YearOfLeaving,LastClass,Address,ProfilePic,Occupation,ProfessionalQualifications,ActivitiesDuringSchool,AdmissionNumber,DateOfAdmission")] MemberDetail memberDetail)
        {
            if (ModelState.IsValid)
            {
                db.MemberDetails.Add(memberDetail);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(memberDetail);
        }

        // GET: MemberDetails/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MemberDetail memberDetail = db.MemberDetails.Find(id);
            if (memberDetail == null)
            {
                return HttpNotFound();
            }
            return View(memberDetail);
        }

        // POST: MemberDetails/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "UserName,FirstName,LastName,EmailName,Mobile,NIC,YearOfLeaving,LastClass,Address,ProfilePic,Occupation,ProfessionalQualifications,ActivitiesDuringSchool,AdmissionNumber,DateOfAdmission")] MemberDetail memberDetail)
        {
            if (ModelState.IsValid)
            {
                db.Entry(memberDetail).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(memberDetail);
        }

        // GET: MemberDetails/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MemberDetail memberDetail = db.MemberDetails.Find(id);
            if (memberDetail == null)
            {
                return HttpNotFound();
            }
            return View(memberDetail);
        }

        // POST: MemberDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            MemberDetail memberDetail = db.MemberDetails.Find(id);
            db.MemberDetails.Remove(memberDetail);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
