using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GolfBooking.Models;
using Newtonsoft.Json;

namespace GolfBooking.Controllers
{
    public class HomeController : Controller
    {
        golfbookingEntities db = new golfbookingEntities();
        public class myoffer
        {
            public int id { get; set; }
            public string name { get; set; }
            public string image { get; set; }
            public decimal? minprice { get; set; }
        }
        public ActionResult Index()
        {
            ViewBag.Message = "Modify this template to jump-start your ASP.NET MVC application.";
            var caption = db.slides.OrderBy(o => o.type).ToList();
            //var slogan = db.slides.OrderBy(o => o.type).Select(o => o.slogan).ToList();
            ViewBag.caption = caption;
            //ViewBag.slogan = slogan;
            ViewBag.caption1 = caption[0].caption;
            ViewBag.slogan1 = caption[0].slogan;
            var offer = (from q in db.golves
                         where q.deleted == 0
                         select new myoffer
                         { 
                             id=q.id,
                             name=q.name,
                             image=q.image,
                             minprice = db.golf_price.Where(o => o.golf_id == q.id).Min(o => o.normal_day_price)
                         }).OrderByDescending(o => o.id).Take(6).ToList();
            ViewBag.offer = offer;
            var package = (from q in db.golf_package_stay where q.deleted == 0 && q.type == 1 select q).OrderByDescending(o => o.id).Take(6).ToList();
            ViewBag.package = package;
            var stay = (from q in db.golf_package_stay where q.deleted == 0 && q.type == 2 select q).OrderByDescending(o => o.id).Take(6).ToList();
            ViewBag.stay = stay;
            var news = (from q in db.news where q.type == 2 select q).OrderByDescending(o => o.id).Take(6).ToList();
            ViewBag.news = news;
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your app description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}
