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
    public class TrophyController : Controller
    {
        private golfbookingEntities db = new golfbookingEntities();

        //
        // GET: /Trophy/

        public ActionResult Index(string name, int? cat_id, int? page)
        {
            if (name == null) name = "";
            name = name.Replace("%20", " ");
            name = name.Trim();
            if (cat_id == null) cat_id = 2;
            ViewBag.name = name;

            ViewBag.cat_id = cat_id;
            var p = (from q in db.golf_trophy where q.name.Contains(name) && q.golf_trophy_cat_id == cat_id select q).OrderByDescending(o => o.id).Take(100);
            int pageSize = 25;
            int pageNumber = (page ?? 1);
            return View(p.ToPagedList(pageNumber, pageSize));
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

        public ActionResult Create(int? id)
        {
            if (id == null) id = 0;           
            ViewBag.id = id;
            try
            {
                if (id != 0)
                {
                    golf_trophy gt = db.golf_trophy.Find(id);
                    ViewBag.name = gt.name;
                    ViewBag.des = gt.des;
                    ViewBag.image = gt.image;
                    ViewBag.banner = gt.banner;
                    ViewBag.price = gt.price;
                    ViewBag.golf_trophy_cat_id = gt.golf_trophy_cat_id;
                }
                else {
                    ViewBag.name = "";
                    ViewBag.des = "";
                    ViewBag.image = "";
                    ViewBag.banner = "";
                    ViewBag.price = 0;
                    ViewBag.golf_trophy_cat_id = 1;
                }
            }
            catch (Exception ex) { 
            }
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
        [HttpPost]
        [ValidateInput(false)]
        public string Update(string name, string des, decimal price, string image, string banner,int golf_trophy_cat_id, int id)
        {
            try
            {
                //add new
                if (id == 0)
                {
                    golf_trophy gt = new golf_trophy();
                    gt.name = name;
                    gt.des = des;
                    gt.price = price;
                    gt.image = image;
                    gt.banner = banner;
                    gt.golf_trophy_cat_id = golf_trophy_cat_id;
                    db.golf_trophy.Add(gt);
                    db.SaveChanges();
                    return gt.id.ToString();
                }
                else {
                    golf_trophy gt = db.golf_trophy.Find(id);
                    gt.name = name;
                    gt.des = des;
                    gt.price = price;
                    gt.image = image;
                    gt.banner = banner;
                    gt.golf_trophy_cat_id = golf_trophy_cat_id;
                    db.Entry(gt).State = EntityState.Modified;
                    db.SaveChanges();
                    return id.ToString();
                }

            }
            catch (Exception ex) { 
                return "0";
            }
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
        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
        public string autosearch(string keyword) {
            var p = (from q in db.golf_trophy where q.name.Contains(keyword) select q.name).ToList();
            return JsonConvert.SerializeObject(p);
        }
    }
}