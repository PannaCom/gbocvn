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
        public ActionResult Index()
        {
            ViewBag.Message = "Modify this template to jump-start your ASP.NET MVC application.";
            var caption = db.slides.OrderBy(o => o.type).ToList();
            //var slogan = db.slides.OrderBy(o => o.type).Select(o => o.slogan).ToList();
            ViewBag.caption = caption;
            //ViewBag.slogan = slogan;
            ViewBag.caption1 = caption[0].caption;
            ViewBag.slogan1 = caption[0].slogan;
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
