using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GolfBooking.Models;
using Newtonsoft.Json;
using System.Collections;
using PagedList;
namespace GolfBooking.Controllers
{
    public class PackageStayController : Controller
    {
        private golfbookingEntities db = new golfbookingEntities();

        //
        // GET: /PackageStay/

        public ActionResult Index(string name,int? type, int? page)
        {
            //return View(db.provinces.Where(o=>o.deleted==0).OrderBy(o=>o.country_id).ThenBy(o=>o.name).ToList());
            if (name == null) name = "";
            name = name.Replace("%20", " ");
            name = name.Trim();
            if (type == null) type = 1;
            ViewBag.name = name;
            ViewBag.type = type;
            var p = (from q in db.golf_package_stay where q.name.Contains(name) && q.deleted == 0 && q.type==type select q).OrderByDescending(o=>o.id).Take(100);
            int pageSize = 25;
            int pageNumber = (page ?? 1);
            return View(p.ToPagedList(pageNumber, pageSize));
        }
        public ActionResult list(string name, int? type, int? page)
        {
            if (name == null) name = "";
            name = name.Replace("%20", " ");
            if (type == null) type = 1;
            ViewBag.name = name;
            ViewBag.type = type;
            var p = (from q in db.golf_package_stay where q.name.Contains(name) && q.deleted == 0 && q.type == type select q).OrderByDescending(o => o.id).Take(100);
            int pageSize = 25;
            int pageNumber = (page ?? 1);
            return View(p.ToPagedList(pageNumber, pageSize));
        }
        

        //  Tuyenvv - View a packet
        public ActionResult View(int id = 0)
        {

            var q = (from pack in db.golf_package_stay where pack.id == id
                     select new ModelPacketStayItem
                     {
                         id = pack.id,
                         golf_id = pack.golf_id,
                         name = pack.name,
                         des = pack.des,
                         full_detail = pack.full_detail,
                         image = pack.image,
                         min_price = pack.min_price,
                         deleted = pack.deleted,
                         type = pack.type,
                         listImages = db.golf_package_stay_image.Where( o => o.golf_package_id == pack.id ).Select(x => x.image).Take(3)
                     }).FirstOrDefault();

            return View(q);
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
            if (id != 0)
            {
                golf_package_stay p = db.golf_package_stay.Find(id);
                ViewBag.golf_id = p.golf_id;
                ViewBag.name = p.name;
                ViewBag.des = p.des;
                ViewBag.full_detail = p.full_detail;
                ViewBag.image = p.image;
                ViewBag.min_price = p.min_price;//.ToString().Replace(".", "").Replace(",", "")
                ViewBag.type = p.type;
                ViewBag.autogolfname = "";
                //golf g = db.golves.Find(p.golf_id); 
                if (p.golf_id!=null && p.golf_id!=""){
                    string[] list_golf_id = p.golf_id.Split(',');
                    string golf_list_table = "";
                    int countGolfList = 0;
                    for (int k = 0; k < list_golf_id.Length; k++)
                    {

                        int index=0;
                        if (list_golf_id[k].Trim()!="") index=int.Parse(list_golf_id[k]);
                        var gl = (from q in db.golves where q.deleted == 0 && q.id==index select q).ToList();                        
                        for (int i = 0; i < gl.Count; i++)
                        {
                            countGolfList++;
                            golf_list_table += "<tr class=\"odd gradeX\" id=trg_" + countGolfList + "><td>" + gl[i].name + "</td><td><a onclick=\"delGolf(" + countGolfList + "," + gl[i].id + ");\" style=\"cursor:pointer;\">Xóa</a></td></tr>";
                        }
                    }
                    ViewBag.golf_list_table = golf_list_table;
                    ViewBag.countGolfList = countGolfList;
                }
            }
            else {
                ViewBag.name = "";
                ViewBag.des = "";
                ViewBag.full_detail = "";
                ViewBag.image = "";
                ViewBag.min_price = 0;
                ViewBag.countGolfList = 0;
                ViewBag.type = 1;
                ViewBag.autogolfname = "";
                ViewBag.golf_id = "";
            }

            return View();
        }
        [HttpPost]
        [ValidateInput(false)]
        public string Update(string name, string des, string full_detail, string golf_id, decimal min_price, string image,byte type,int id)
        {
            try
            {
                //add new
                if (id == 0)
                {
                    golf_package_stay gps = new golf_package_stay();
                    gps.name = name;
                    gps.des = des;
                    gps.full_detail = full_detail;
                    gps.golf_id = golf_id;
                    gps.min_price = min_price;
                    gps.image = image;
                    gps.type = type;
                    gps.deleted = 0;
                    db.golf_package_stay.Add(gps);
                    db.SaveChanges();
                    return gps.id.ToString();
                }
                else
                {
                    golf_package_stay gps = db.golf_package_stay.Find(id);
                    gps.name = name;
                    gps.des = des;
                    gps.full_detail = full_detail;
                    gps.golf_id = golf_id;
                    gps.min_price = min_price;
                    gps.image = image;
                    gps.type = type;
                    gps.deleted = 0;
                    db.Entry(gps).State = EntityState.Modified;
                    db.SaveChanges();
                    return id.ToString();
                }
                
            }
            catch (Exception ex) {
                return "0";
            }
        }
        public string getGI(int id)
        {
            var p = (from q in db.golf_package_stay_image where q.golf_package_id == id select q.image);
            return JsonConvert.SerializeObject(p.ToList());
        }
        public string updateGI(int count, int id)
        {
            try
            {
                db.Database.ExecuteSqlCommand("delete from golf_package_stay_image where golf_package_id=" + id);
                if (id != 0)
                {
                    for (int i = 0; i < count; i++)
                    {
                        string name = Request.Form["name_" + i].ToString();
                        if (name != "" && name != null)
                        {
                            golf_package_stay_image gi = new golf_package_stay_image();
                            gi.golf_package_id = id;
                            gi.image = name;
                            db.golf_package_stay_image.Add(gi);
                            db.SaveChanges();
                        }
                    }
                }
                return "1";
            }
            catch (Exception ex)
            {
                return "0";
            }
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
        [HttpPost]
        [AcceptVerbs(HttpVerbs.Post)]
        public string UploadImageProcess(HttpPostedFileBase file)
        {
            string physicalPath = HttpContext.Server.MapPath("../" + Config.PackageGolfImagePath + "\\");
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
            return Config.PackageGolfImagePath + "/" + nameFile;
        }
        [HttpPost]
        [AcceptVerbs(HttpVerbs.Post)]
        public string UploadImageProcessGi(HttpPostedFileBase file)
        {
            string physicalPath = HttpContext.Server.MapPath("../" + Config.PackageGolfImagePath + "\\");
            string nameFile = String.Format("{0}.jpg", Guid.NewGuid().ToString());
            int countFile = Request.Files.Count;
            string fullPath = physicalPath + System.IO.Path.GetFileName(nameFile);
            ArrayList list = new ArrayList();
            for (int i = 0; i < countFile; i++)
            {
                nameFile = String.Format("{0}.jpg", Guid.NewGuid().ToString());
                fullPath = physicalPath + System.IO.Path.GetFileName(nameFile);
                if (System.IO.File.Exists(fullPath))
                {
                    System.IO.File.Delete(fullPath);
                }
                Request.Files[i].SaveAs(fullPath);
                list.Add(Config.PackageGolfImagePath + "/" + nameFile);
            }
            return JsonConvert.SerializeObject(list);
        }
        public string autosearch(string keyword)
        {
            var p = (from q in db.golf_package_stay where q.deleted == 0 && q.name.Contains(keyword) select q.name).Take(10);
            return JsonConvert.SerializeObject(p.ToList());
        }
    }
}