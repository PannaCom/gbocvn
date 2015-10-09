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
        [HttpPost]
        [AcceptVerbs(HttpVerbs.Post)]
        public string UploadImageProcess(HttpPostedFileBase file)
        {
            string physicalPath = HttpContext.Server.MapPath("../" + Config.TrophyGolfImagePath + "\\");
            string nameFile = String.Format("{0}.jpg", Guid.NewGuid().ToString());
            int countFile = Request.Files.Count;
            string fullPath = physicalPath + System.IO.Path.GetFileName(nameFile);
            for (int i = 0; i < countFile; i++)
            {
                if (System.IO.File.Exists(fullPath))
                {
                    System.IO.File.Delete(fullPath);
                }
                Request.Files[i].SaveAs(fullPath);
                break;
            }
            //string ok = resizeImage(Config.imgWidthNews, Config.imgHeightNews, fullPath, Config.NewsImagePath + "/" + nameFile);
            return Config.TrophyGolfImagePath + "/" + nameFile;
        }
        public string getList(int id) { 
           
            var p = db.golf_trophy_category.OrderBy(o=>o.name).ToList();
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
    }
}