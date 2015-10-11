using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GolfBooking.Models;
using Newtonsoft.Json;
using PagedList;
namespace GolfBooking.Controllers
{
    public class NewsController : Controller
    {
        private golfbookingEntities db = new golfbookingEntities();

        //
        // GET: /News/

        public ActionResult Index(string name, int? page)
        {
            //return View(db.provinces.Where(o=>o.deleted==0).OrderBy(o=>o.country_id).ThenBy(o=>o.name).ToList());
            if (name == null) name = "";
            name = name.Replace("%20", " ");
            name = name.Trim();
            ViewBag.name = name;            
            var p = (from q in db.news where q.title.Contains(name) select q).OrderByDescending(o => o.id).Take(100);
            int pageSize = 25;
            int pageNumber = (page ?? 1);
            return View(p.ToPagedList(pageNumber, pageSize));
        }

        //
        // GET: /News/Details/5

        public ActionResult Details(int id = 0)
        {
            news news = db.news.Find(id);
            if (news == null)
            {
                return HttpNotFound();
            }
            return View(news);
        }

        //
        // GET: /News/Create

        public ActionResult Create(int? id)
        {
            if (id == null) id = 0;
            ViewBag.id = id;
            if (id == 0)
            {
                ViewBag.name = "";
                ViewBag.image = "";
                ViewBag.full_details = "";
                ViewBag.type = 1;
                ViewBag.des = "";
            }
            else {
                news n = db.news.Find(id);
                ViewBag.name = n.title;
                ViewBag.image = n.image;
                ViewBag.full_details = n.full_details;
                ViewBag.type = n.type;
                ViewBag.des = n.des;
            }
            return View();
        }

        //
        // POST: /News/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(news news)
        {
            if (ModelState.IsValid)
            {
                db.news.Add(news);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(news);
        }

        //
        // GET: /News/Edit/5

        public ActionResult Edit(int id = 0)
        {
            news news = db.news.Find(id);
            if (news == null)
            {
                return HttpNotFound();
            }
            return View(news);
        }

        //
        // POST: /News/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(news news)
        {
            if (ModelState.IsValid)
            {
                db.Entry(news).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(news);
        }

        //
        // GET: /News/Delete/5

        public ActionResult Delete(int id = 0)
        {
            news news = db.news.Find(id);
            if (news == null)
            {
                return HttpNotFound();
            }
            return View(news);
        }

        //
        // POST: /News/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            news news = db.news.Find(id);
            db.news.Remove(news);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
        [HttpPost]
        [ValidateInput(false)]
        public string Update(string title, string des, string full_details, string image, byte type, int id)
        {
            try
            {
                if (id == 0)
                {
                    news n = new news();
                    n.title = title;
                    n.des = des;
                    n.full_details = full_details;
                    n.image = image;
                    n.type = type;
                    db.news.Add(n);
                    db.SaveChanges();
                    return n.id.ToString();
                }
                else {
                    news n = db.news.Find(id);
                    n.title = title;
                    n.des = des;
                    n.full_details = full_details;
                    n.image = image;
                    n.type = type;
                    db.Entry(n).State = EntityState.Modified;
                    db.SaveChanges();
                    return id.ToString();
                }
            }
            catch (Exception ex) {
                return "0";
            }
        }
        [HttpPost]
        [AcceptVerbs(HttpVerbs.Post)]
        public string UploadImageProcess(HttpPostedFileBase file)
        {
            string physicalPath = HttpContext.Server.MapPath("../" + Config.NewsGolfImagePath + "\\");
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
            return Config.NewsGolfImagePath + "/" + nameFile;
        }
        public string autosearch(string keyword)
        {
            var p = (from q in db.news where q.title.Contains(keyword) select q.title).Take(10);
            return JsonConvert.SerializeObject(p.ToList());
        }
    }
}