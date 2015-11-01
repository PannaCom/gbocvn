using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GolfBooking.Models;
using PagedList;
using Newtonsoft.Json;
namespace GolfBooking.Controllers
{
    public class SlideController : Controller
    {
        private golfbookingEntities db = new golfbookingEntities();

        //
        // GET: /Slide/

        public ActionResult Index()
        {
            return View(db.slides.ToList());
        }

        //
        // GET: /Slide/Details/5

        public ActionResult Details(int id = 0)
        {
            slide slide = db.slides.Find(id);
            if (slide == null)
            {
                return HttpNotFound();
            }
            return View(slide);
        }

        //
        // GET: /Slide/Create

        public ActionResult Create(int? id)
        {
            if (id == null) id = 0;
            ViewBag.id = id;
            if (id != 0)
            {
                slide sl = db.slides.Find(id);
                ViewBag.caption = sl.caption;
                ViewBag.slogan = sl.slogan;
                ViewBag.linktext = sl.linktext;
                ViewBag.link = sl.link;
                ViewBag.image = sl.image;
                ViewBag.type = sl.type;
            }else{
                ViewBag.caption = "";
                ViewBag.slogan = "";
                ViewBag.linktext = "";
                ViewBag.link = "";
                ViewBag.image = "";
                ViewBag.type = 1;
            }

            return View();
        }

        //
        // POST: /Slide/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(slide slide)
        {
            if (ModelState.IsValid)
            {
                db.slides.Add(slide);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(slide);
        }
        [HttpPost]
        [AcceptVerbs(HttpVerbs.Post)]
        public string UploadImageProcess(HttpPostedFileBase file, string filename)
        {
            string physicalPath = HttpContext.Server.MapPath("../" + Config.SlideImagePath + "\\");
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
            //string ok = resizeImage(Config.imgWidthBigSlide, Config.imgHeightBigSlide, fullPath, Config.SlideImagePath + "/" + nameFile);
            return Config.SlideImagePath + "/" + nameFile;
        }
        //
        // GET: /Slide/Edit/5

        public ActionResult Edit(int id = 0)
        {
            slide slide = db.slides.Find(id);
            if (slide == null)
            {
                return HttpNotFound();
            }
            return View(slide);
        }

        //
        // POST: /Slide/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(slide slide)
        {
            if (ModelState.IsValid)
            {
                db.Entry(slide).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(slide);
        }

        //
        // GET: /Slide/Delete/5

        public ActionResult Delete(int id = 0)
        {
            slide slide = db.slides.Find(id);
            if (slide == null)
            {
                return HttpNotFound();
            }
            return View(slide);
        }

        //
        // POST: /Slide/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            slide slide = db.slides.Find(id);
            db.slides.Remove(slide);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
        [HttpPost]
        public string Update(string caption, string slogan, string linktext, string link, string image, int? type,int id)
        {
            try
            {
                if (id == 0)
                {
                    slide sl = new slide();
                    sl.caption = caption;
                    sl.slogan = slogan;
                    sl.linktext = linktext;
                    sl.link = link;
                    sl.image = image;
                    sl.type = type;
                    db.slides.Add(sl);
                    db.SaveChanges();
                    return sl.id.ToString();
                }
                else
                {
                    slide sl = db.slides.Find(id);
                    sl.caption = caption;
                    sl.slogan = slogan;
                    sl.linktext = linktext;
                    sl.link = link;
                    sl.image = image;
                    sl.type = type;
                    db.Entry(sl).State = EntityState.Modified;
                    db.SaveChanges();
                    return id.ToString();
                }
            }
            catch (Exception ex) {
                return "0";
            }
        }
    }
}
