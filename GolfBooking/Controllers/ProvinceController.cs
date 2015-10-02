using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GolfBooking.Models;
using PagedList;
namespace GolfBooking.Controllers
{
    public class ProvinceController : Controller
    {
        private golfbookingEntities db = new golfbookingEntities();

        //
        // GET: /Province/

        public ActionResult Index(string name, int? page)
        {
            //return View(db.provinces.Where(o=>o.deleted==0).OrderBy(o=>o.country_id).ThenBy(o=>o.name).ToList());
            if (name == null) name = "";
            ViewBag.name = name;
            var p = (from q in db.provinces where q.name.Contains(name) && q.deleted == 0 select q).OrderBy(o=>o.country_id).ThenBy(o=>o.name).Take(100);
            int pageSize = 25;
            int pageNumber = (page ?? 1);
            return View(p.ToPagedList(pageNumber, pageSize));
        }
        public string getListProvin(int? country_id, int? id)
        {
            var p = db.provinces.Where(o => o.deleted == 0).Where(o => o.country_id == country_id).OrderBy(o => o.name).ToList();
            string val = "";
            for (int i = 0; i < p.Count; i++)
            {
                if (id == p[i].id)
                {
                    val += "<option value=\"" + p[i].id + "\" selected>" + p[i].name + "</option>";
                }
                else
                {
                    val += "<option value=\"" + p[i].id + "\">" + p[i].name + "</option>";
                }
            }
            return val;
        }
        //
        // GET: /Province/Details/5

        public ActionResult Details(int id = 0)
        {
            province province = db.provinces.Find(id);
            if (province == null)
            {
                return HttpNotFound();
            }
            return View(province);
        }

        //
        // GET: /Province/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Province/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(province province)
        {
            if (ModelState.IsValid)
            {
                province.deleted = 0;
                db.provinces.Add(province);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(province);
        }

        //
        // GET: /Province/Edit/5

        public ActionResult Edit(int id = 0)
        {
            province province = db.provinces.Find(id);
            if (province == null)
            {
                return HttpNotFound();
            }
            return View(province);
        }

        //
        // POST: /Province/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(province province)
        {
            if (ModelState.IsValid)
            {
                db.Entry(province).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(province);
        }

        //
        // GET: /Province/Delete/5

        public ActionResult Delete(int id = 0)
        {
            province province = db.provinces.Find(id);
            if (province == null)
            {
                return HttpNotFound();
            }
            return View(province);
        }

        //
        // POST: /Province/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            //province province = db.provinces.Find(id);
            //db.provinces.Remove(province);
            //db.SaveChanges();
            string query = "update province set deleted=1 where id=" + id;
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