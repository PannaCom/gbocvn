using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GolfBooking.Models;

namespace GolfBooking.Controllers
{
    public class PackageStayController : Controller
    {
        private golfbookingEntities db = new golfbookingEntities();

        //
        // GET: /PackageStay/

        public ActionResult Index()
        {
            return View(db.golf_package_stay.ToList());
        }

        //
        // GET: /PackageStay/Details/5

        public ActionResult Details(int id = 0)
        {
            golf_package_stay golf_package_stay = db.golf_package_stay.Find(id);
            if (golf_package_stay == null)
            {
                return HttpNotFound();
            }
            return View(golf_package_stay);
        }

        //
        // GET: /PackageStay/Create

        public ActionResult Create(int? id)
        {
            if (id == null) id = 0;
            ViewBag.golf_id = 0;
            ViewBag.id = id;
            return View();
        }

        //
        // POST: /PackageStay/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(golf_package_stay golf_package_stay)
        {
            if (ModelState.IsValid)
            {
                db.golf_package_stay.Add(golf_package_stay);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(golf_package_stay);
        }

        //
        // GET: /PackageStay/Edit/5

        public ActionResult Edit(int id = 0)
        {
            golf_package_stay golf_package_stay = db.golf_package_stay.Find(id);
            if (golf_package_stay == null)
            {
                return HttpNotFound();
            }
            return View(golf_package_stay);
        }

        //
        // POST: /PackageStay/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(golf_package_stay golf_package_stay)
        {
            if (ModelState.IsValid)
            {
                db.Entry(golf_package_stay).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(golf_package_stay);
        }

        //
        // GET: /PackageStay/Delete/5

        public ActionResult Delete(int id = 0)
        {
            golf_package_stay golf_package_stay = db.golf_package_stay.Find(id);
            if (golf_package_stay == null)
            {
                return HttpNotFound();
            }
            return View(golf_package_stay);
        }

        //
        // POST: /PackageStay/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            golf_package_stay golf_package_stay = db.golf_package_stay.Find(id);
            db.golf_package_stay.Remove(golf_package_stay);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}