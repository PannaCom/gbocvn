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
    public class TrophyCatController : Controller
    {
        private golfbookingEntities db = new golfbookingEntities();

        //
        // GET: /TrophyCat/

        public ActionResult Index()
        {
            return View(db.golf_trophy_category.ToList());
        }

        //
        // GET: /TrophyCat/Details/5

        public ActionResult Details(int id = 0)
        {
            golf_trophy_category golf_trophy_category = db.golf_trophy_category.Find(id);
            if (golf_trophy_category == null)
            {
                return HttpNotFound();
            }
            return View(golf_trophy_category);
        }

        //
        // GET: /TrophyCat/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /TrophyCat/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(golf_trophy_category golf_trophy_category)
        {
            if (ModelState.IsValid)
            {
                db.golf_trophy_category.Add(golf_trophy_category);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(golf_trophy_category);
        }

        //
        // GET: /TrophyCat/Edit/5

        public ActionResult Edit(int id = 0)
        {
            golf_trophy_category golf_trophy_category = db.golf_trophy_category.Find(id);
            if (golf_trophy_category == null)
            {
                return HttpNotFound();
            }
            return View(golf_trophy_category);
        }

        //
        // POST: /TrophyCat/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(golf_trophy_category golf_trophy_category)
        {
            if (ModelState.IsValid)
            {
                db.Entry(golf_trophy_category).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(golf_trophy_category);
        }

        //
        // GET: /TrophyCat/Delete/5

        public ActionResult Delete(int id = 0)
        {
            golf_trophy_category golf_trophy_category = db.golf_trophy_category.Find(id);
            if (golf_trophy_category == null)
            {
                return HttpNotFound();
            }
            return View(golf_trophy_category);
        }

        //
        // POST: /TrophyCat/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            golf_trophy_category golf_trophy_category = db.golf_trophy_category.Find(id);
            db.golf_trophy_category.Remove(golf_trophy_category);
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