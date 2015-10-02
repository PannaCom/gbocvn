using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GolfBooking.Models;
using Newtonsoft.Json;
namespace GolfBooking.Controllers
{
    public class CountryController : Controller
    {
        private golfbookingEntities db = new golfbookingEntities();

        //
        // GET: /Country/

        public ActionResult Index()
        {
            return View(db.countries.Where(o=>o.deleted==0).ToList());
        }
        public string listCountry(int? id) {
            var p = db.countries.Where(o => o.deleted == 0).OrderBy(o=>o.name).ToList();
            string val = "";
            for (int i = 0; i < p.Count; i++) {
                if (id == p[i].id)
                {
                    val += "<option value=\"" + p[i].id + "\" selected>" + p[i].name + "</option>";
                }
                else {
                    val += "<option value=\"" + p[i].id + "\">" + p[i].name + "</option>";
                }
            }
            return val;
        }
        public string getCountryName(int id) { 
            string p=db.countries.Where(o=>o.id==id).FirstOrDefault().name;
            return p;
        }
        //
        // GET: /Country/Details/5

        public ActionResult Details(int id = 0)
        {
            country country = db.countries.Find(id);
            if (country == null)
            {
                return HttpNotFound();
            }
            return View(country);
        }

        //
        // GET: /Country/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Country/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(country country)
        {
            if (ModelState.IsValid)
            {
                country.deleted = 0;
                db.countries.Add(country);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(country);
        }

        //
        // GET: /Country/Edit/5

        public ActionResult Edit(int id = 0)
        {
            country country = db.countries.Find(id);
            if (country == null)
            {
                return HttpNotFound();
            }
            return View(country);
        }

        //
        // POST: /Country/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(country country)
        {
            if (ModelState.IsValid)
            {
                db.Entry(country).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(country);
        }

        //
        // GET: /Country/Delete/5

        public ActionResult Delete(int id = 0)
        {
            country country = db.countries.Find(id);
            if (country == null)
            {
                return HttpNotFound();
            }
            return View(country);
        }

        //
        // POST: /Country/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            //country country = db.countries.Find(id);
            //db.countries.Remove(country);
            //db.SaveChanges();
            string query = "update country set deleted=1 where id=" + id;
            db.Database.ExecuteSqlCommand(query);
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}