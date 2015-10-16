﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace GolfBooking
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            
            //define 1 cai route o day
            routes.MapRoute(
                "viewpacket",
                "viewpacket/{id}",
                new { controller = "PackageStay", action = "View", id = UrlParameter.Optional }
            );

            //Trophy/category
            // them route cho trophy category
            routes.MapRoute(
                "trophycategory",
                "golftrophycat",
                new { controller = "Trophy", action = "category"}
            );


            //Trophy/list/?catid=4
            // them route cho trophy list category
            routes.MapRoute(
                "trophylist",
                "golftrophy/{catid}/{page}",
                new { controller = "Trophy", action = "list", catid = UrlParameter.Optional, page = UrlParameter.Optional }
            );


            //Trophy/view/?id=4
            // them route cho trophy view detail
            routes.MapRoute(
                "trophyview",
                "golftrophydetail/{id}",
                new { controller = "Trophy", action = "view", id = UrlParameter.Optional}
            );



            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}