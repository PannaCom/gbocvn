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
    public class TrophyController : Controller
    {
        private golfbookingEntities db = new golfbookingEntities();

        //
        // GET: /Trophy/

        public ActionResult Index()
        {
            return View(db.golf_trophy.ToList());
        }

        //
        // GET: /Trophy/Details/5

        public ActionResult Details(int id = 0)
        {
            golf_trophy golf_trophy = db.golf_trophy.Find(id);
            if (golf_trophy == null)
            {
                return HttpNotFound();
            }
            return View(golf_trophy);
        }

        //
        // GET: /Trophy/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Trophy/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(golf_trophy golf_trophy)
        {
            if (ModelState.IsValid)
            {
                db.golf_trophy.Add(golf_trophy);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(golf_trophy);
        }

        //
        // GET: /Trophy/Edit/5

        public ActionResult Edit(int id = 0)
        {
            golf_trophy golf_trophy = db.golf_trophy.Find(id);
            if (golf_trophy == null)
            {
                return HttpNotFound();
            }
            return View(golf_trophy);
        }

        //
        // POST: /Trophy/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(golf_trophy golf_trophy)
        {
            if (ModelState.IsValid)
            {
                db.Entry(golf_trophy).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(golf_trophy);
        }

        //
        // GET: /Trophy/Delete/5

        public ActionResult Delete(int id = 0)
        {
            golf_trophy golf_trophy = db.golf_trophy.Find(id);
            if (golf_trophy == null)
            {
                return HttpNotFound();
            }
            return View(golf_trophy);
        }

        //
        // POST: /Trophy/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            golf_trophy golf_trophy = db.golf_trophy.Find(id);
            db.golf_trophy.Remove(golf_trophy);
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