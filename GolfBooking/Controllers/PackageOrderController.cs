using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using Newtonsoft.Json;
using GolfBooking.Models;
using System.Collections;
using System.Data;
using System.Data.Entity;

namespace GolfBooking.Controllers
{
    public class PackageOrderController : Controller
    {
        //
        // GET: /PackageOrder/
        private golfbookingEntities db = new golfbookingEntities();
        public ActionResult Index(string name, int? page)
        {
            //return View(db.provinces.Where(o=>o.deleted==0).OrderBy(o=>o.country_id).ThenBy(o=>o.name).ToList());
            if (name == null) name = "";
            ViewBag.name = name;
            var p = (from q in db.golf_order_package_stay where q.name.Contains(name) select q).OrderByDescending(o => o.id).Take(100);
            int pageSize = 25;
            int pageNumber = (page ?? 1);
            return View(p.ToPagedList(pageNumber, pageSize));
        }

    }
}
