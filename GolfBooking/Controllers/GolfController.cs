using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using Newtonsoft.Json;
using GolfBooking.Models;
using System.Data;
using System.Collections;

namespace GolfBooking.Controllers
{
    public class GolfController : Controller
    {
        //
        // GET: /Golf/
        private golfbookingEntities db = new golfbookingEntities();
        public ActionResult Index(string name, int? page)
        {
            //return View(db.provinces.Where(o=>o.deleted==0).OrderBy(o=>o.country_id).ThenBy(o=>o.name).ToList());
            if (name == null) name = "";
            ViewBag.name = name;
            var p = (from q in db.golves where q.name.Contains(name) && q.deleted == 0 select q).OrderBy(o => o.name).Take(100);
            int pageSize = 25;
            int pageNumber = (page ?? 1);
            return View(p.ToPagedList(pageNumber, pageSize));
        }
        public ActionResult List(string name,int? region,int? page)
        {
            //return View(db.provinces.Where(o=>o.deleted==0).OrderBy(o=>o.country_id).ThenBy(o=>o.name).ToList());
            if (name == null) name = "";
            if (region == null) region = -1;
            ViewBag.name = name;
            ViewBag.region = region;

            var isWeeken = DateTime.Now.DayOfWeek == DayOfWeek.Sunday || DateTime.Now.DayOfWeek == DayOfWeek.Saturday ? true : false;

            var p = (from g in db.golves
                     select new GolfItemViewModel
                     {
                         id = g.id,
                         name = g.name,
                         des = g.des,
                         tech_des = g.tech_des,
                         score_board_image = g.score_board_image,
                         image = g.image,
                         country_id = g.country_id,
                         province_id = g.province_id,
                         region_id = g.region_id,
                         address = g.address,
                         lon = g.lon,
                         lat = g.lat,
                         geo = g.geo,
                         deleted = g.deleted,
                         priceInfo = !isWeeken ? //how??
                                    db.golf_price.Where(o => o.golf_id == g.id)
                                        .Select(x =>
                                            new extentdVal
                                            {
                                                cart = x.cart.HasValue ? x.cart.Value : false,
                                                minPrice = x.normal_day_price.HasValue ? x.normal_day_price.Value : 0
                                            }).OrderBy(z => z.minPrice).FirstOrDefault() :
                                    db.golf_price.Where(o => o.golf_id == g.id)
                                        .Select(x =>
                                            new extentdVal
                                            {
                                                cart = x.cart.HasValue ? x.cart.Value : false,
                                                minPrice = x.weekend_day_price.HasValue ? x.weekend_day_price.Value : 0
                                            }).OrderBy(z => z.minPrice).FirstOrDefault()

                     });

            p = p.Where(o=>o.name.Contains(name)).Where(o=>o.deleted==0);
            //(from q in db.golves where q.name.Contains(name) && q.deleted == 0 select q).OrderBy(o => o.name).Take(100);
            if (region != -1) p = p.Where(o => o.region_id == region);

            p = p.OrderBy(o => o.name).Take(100);

            int pageSize = 8;
            int pageNumber = (page ?? 1);
            ViewBag.page = pageNumber;
            ViewBag.header_course_list = Config.getRegionById((int)region) + " Golf Course";
            
            //tao viet 1 cai query o day
            


            return View(p.ToPagedList(pageNumber, pageSize));
        }
        public ActionResult View(int id) {
            golf golf = db.golves.Find(id);
            if (golf == null)
            {
                return HttpNotFound();
            }
            ViewBag.sdes = Config.smoothDes(golf.des);
            ViewBag.des = golf.des;
            string gallery = "";
            var p = (from q in db.golf_image where q.golf_id == id select q).Take(3).ToList();
            for (int i = 0; i < p.Count; i++) { 
                gallery+="<li><a href=\""+Config.domain+p[i].image+"\" rel=\"lightbox\" class=\"link-image\"><img class=\"wp-image\" src=\""+Config.domain+p[i].image+"\" alt=\""+golf.name+"\" style=\"width:70px;height:70px;\"/></a></li>";
            }
            ViewBag.gallery = gallery;
            string golf_course_list = "";
            var p2 = (from q in db.golves where q.region_id == golf.region_id && q.deleted==0 select q).Take(6).ToList();
            for (int j = 0; j < p2.Count; j++) { 
                golf_course_list+="<div class=\"panel panel-default\">";
				golf_course_list+=" <div class=\"panel-heading\">";
				golf_course_list+="<h6 class=\"panel-title\" style=\"font-size:12px;\">";
				golf_course_list+="<a class=\"accordion-toggle\" data-toggle=\"collapse\" data-parent=\"#accordion\" href=\"#collapseOne\">"+p2[j].name+"</a>";
				golf_course_list+=" </h6>";
				golf_course_list+="	</div>";
				golf_course_list+=" <div id=\"collapseOne\" class=\"panel-collapse collapse in\">";
				golf_course_list+="		<div class=\"panel-body\" style=\"font-size:12px;\">";
                golf_course_list += Config.smoothDesSmall(p2[j].des) + "..<a href=\"/golf/view/" + p2[j].id + "\">đọc tiếp</a>";
				golf_course_list+="			    	</div>";
				golf_course_list+="			    </div>";
                golf_course_list += "		</div>";
            }
            ViewBag.golf_course_list = golf_course_list;
            ViewBag.header_course_list = Config.getRegionById((int)golf.region_id) + " Golf";
            return View(golf);
        }
        public ActionResult Create(int? id) {
            ViewBag.id = id;
            if (id == null)
            {
                ViewBag.id = 0;
                ViewBag.country_id = -1;
                ViewBag.province_id = -1;
                ViewBag.region_id = 1;
                ViewBag.name = "";
                ViewBag.des = "";
                ViewBag.image = "";
                ViewBag.address = "";
                ViewBag.lon = "";
                ViewBag.lat = "";
                ViewBag.tech_des = "";
                ViewBag.score_board_image = "";
                ViewBag.rowsPrice = 0;
            }
            else
            {
                var p = db.golves.Where(o => o.id == id).FirstOrDefault();
                if (p != null) {
                    ViewBag.name = p.name;
                    ViewBag.des = p.des;
                    ViewBag.country_id = p.country_id;
                    ViewBag.province_id = p.province_id;
                    ViewBag.region_id = p.region_id;
                    ViewBag.image = p.image;
                    ViewBag.address = p.address;
                    ViewBag.lon = p.lon;
                    ViewBag.lat = p.lat;
                    ViewBag.tech_des = p.tech_des;
                    ViewBag.score_board_image = p.score_board_image;
                    
                }
                try
                {
                    int rowsPrice = (int)db.golf_price.Count(o => o.golf_id == id);
                    if (rowsPrice != null)
                        ViewBag.rowsPrice = rowsPrice;
                    else ViewBag.rowsPrice = 0;
                }
                catch (Exception ex) {
                    ViewBag.rowsPrice = 0;
                }
            }
            DateTime fromDate = DateTime.Now;
            DateTime toDate = DateTime.Now.AddDays(30);
            ViewBag.fromDate = String.Format("{0:yyyy-MM-dd}", fromDate);
            ViewBag.toDate = String.Format("{0:yyyy-MM-dd}", toDate);  
            return View();
        }
        public ActionResult Test() {

            return View();
        }
        [HttpPost]
        [AcceptVerbs(HttpVerbs.Post)]
        public string UploadImageProcess(HttpPostedFileBase file)
        {
            string physicalPath = HttpContext.Server.MapPath("../" + Config.GolfImagePath + "\\");
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
            return Config.GolfImagePath + "/" + nameFile;
        }
        [HttpPost]
        [AcceptVerbs(HttpVerbs.Post)]
        public string UploadImageProcessGi(HttpPostedFileBase file)
        {
            string physicalPath = HttpContext.Server.MapPath("../" + Config.GolfImagePath + "\\");
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
                list.Add(Config.GolfImagePath + "/" + nameFile);
            }
            return JsonConvert.SerializeObject(list);
        }
        [HttpPost]
        [ValidateInput(false)]
        public string Update_Golf(string name, string des,int country_id,int province_id,int region_id, string image, string address, double? lon, double? lat, string tech_des, string score_board_image,int id)
        {
            try
            {
                if (id == 0)
                {
                    golf gl = new golf();
                    gl.name = name;
                    gl.des = des;
                    gl.country_id = country_id;
                    gl.province_id = province_id;
                    gl.region_id = region_id;
                    gl.image = image;
                    gl.address = address;
                    gl.lon = lon;
                    gl.lat = lat;
                    gl.tech_des = tech_des;
                    gl.score_board_image = score_board_image;
                    gl.geo = Config.CreatePoint(lat, lon);
                    gl.deleted = 0;
                    db.golves.Add(gl);
                    db.SaveChanges();
                    return gl.id.ToString();
                }
                else
                {
                    golf gl = db.golves.Find(id);
                    gl.name = name;
                    gl.des = des;
                    gl.country_id = country_id;
                    gl.province_id = province_id;
                    gl.region_id = region_id;
                    gl.image = image;
                    gl.address = address;
                    gl.lon = lon;
                    gl.lat = lat;
                    gl.tech_des = tech_des;
                    gl.score_board_image = score_board_image;
                    gl.geo = Config.CreatePoint(lat, lon);
                    gl.deleted = 0;
                    db.Entry(gl).State = EntityState.Modified;
                    db.SaveChanges();
                    return id.ToString();
                }
            }
            catch (Exception ex) {
                return "0";
            }
            //return "1";
        }
        public string getGI(int id) {
            var p = (from q in db.golf_image where q.golf_id == id select q.image).ToList();
            ArrayList list=new ArrayList();
            try
            {
                int i;
                if (p.Count > 0)
                {
                    for(i=0;i<p.Count;i++)
                    {
                        list.Add(p[i]);
                    }
                }
                return JsonConvert.SerializeObject(list);
            }
            catch (Exception ex) {
                return "0";
            }
        }
        public string getGP(int id)
        {
            var p = (from q in db.golf_price where q.golf_id == id select q).ToList();            
            try
            {
                return JsonConvert.SerializeObject(p);
            }
            catch (Exception ex)
            {
                return "0";
            }
        }
        public string updateGI(int count, int id) {
            try
            {
                db.Database.ExecuteSqlCommand("delete from golf_image where golf_id=" + id);
                if (id != 0)
                {
                    for (int i = 0; i < count; i++)
                    {
                        string name = Request.Form["name_" + i].ToString();
                        if (name != "" && name != null) {
                            golf_image gi = new golf_image();
                            gi.golf_id = id;
                            gi.image = name;
                            db.golf_image.Add(gi);
                            db.SaveChanges();
                        }
                    }
                }
                return "1";
            }
            catch (Exception ex) {
                return "0";
            }
        }
        public string updatePrice(int count, int id)
        {
            try
            {
                db.Database.ExecuteSqlCommand("delete from golf_price where golf_id=" + id);
                if (id != 0)
                {
                    for (int i = 1; i <= count; i++)
                    {
                        try
                        {
                            string from_date = Request.Form["from_date_id_" + i].ToString();
                            string to_date = Request.Form["to_date_id_" + i].ToString();
                            int from_date_id = int.Parse(Config.convertToDateTimeId(from_date));
                            int to_date_id = int.Parse(Config.convertToDateTimeId(to_date));
                            decimal normal_day_price = decimal.Parse(Request.Form["normal_day_price_" + i].ToString());
                            decimal weekend_day_price = decimal.Parse(Request.Form["weekend_day_price_" + i].ToString());
                            TimeSpan from_time = TimeSpan.Parse(Request.Form["from_time_" + i].ToString());
                            TimeSpan to_time = TimeSpan.Parse(Request.Form["to_time_" + i].ToString());
                            bool cart = false;
                            if (Request.Form["cart_" + i].ToString() == "true") cart = true;
                            if (from_date_id != null && from_date_id != 0)
                            {
                                golf_price gi = new golf_price();
                                gi.golf_id = id;
                                gi.from_date_id = from_date_id;
                                gi.from_time = from_time;
                                gi.normal_day_price = normal_day_price;
                                gi.to_date_id = to_date_id;
                                gi.to_time = to_time;
                                gi.weekend_day_price = weekend_day_price;
                                gi.cart = cart;
                                db.golf_price.Add(gi);
                                db.SaveChanges();
                            }
                        }
                        catch (Exception ex) { 

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
        public string autosearch(string keyword) {
            var p = (from q in db.golves where q.deleted == 0 && q.name.Contains(keyword) select q.name).Take(10);
            return JsonConvert.SerializeObject(p.ToList());
        }
        public string getgolfid(string keyword)
        {
            try
            {
                var p = (from q in db.golves where q.deleted == 0 && q.name.Contains(keyword) select q.id).FirstOrDefault();
                if (p == null) return "0";
                return p.ToString();
            }
            catch (Exception ex) {
                return "0";
            }
        }
        public class searchTeeTime {
            public int id { get; set; }
            public string name { get; set; }
            public string image { get; set; }
            public int country { get; set; }
            public int province { get; set; }
            public decimal nprice { get; set; }
            public decimal wprice { get; set; }
            public bool cart { get; set; }
        }
        public ActionResult Search(string name,int? country,int? province,int date_id,string time) {

            if (country==null) country=0;
            if (province==null) province=0;
            string query = "select id,name,image,country,province,nprice,wprice,cart from ";
            query += "(select name,image,id,country_id as country,province_id as province from golf where name like N'%" + name + "%') as A inner join ";
            query += "(select golf_id,normal_day_price as nprice,weekend_day_price as wprice,cart from golf_price where from_date_id<=" + date_id + " and to_date_id>=" + date_id + " and from_time<='"+time+"' and to_time>='"+time+"') as B ";
            query += " on A.id=B.golf_id where 1=1 ";
            if (country != 0) query += " and country=" + country;
            if (province != 0) query += " and province=" + province;
            var p = db.Database.SqlQuery<searchTeeTime>(query);
            return View(p.ToList());
        }
    }
}
