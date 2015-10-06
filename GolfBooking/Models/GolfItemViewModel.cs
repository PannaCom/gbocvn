using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GolfBooking.Models;

namespace GolfBooking.Models
{
    public class GolfItemViewModel : golf
    {
        //define cac property may can hien thi moi cai item o cai list ay ra day
        //deo ai biet, tam thoi chac cu lay ben entity model san

        public extentdVal priceInfo {get; set;}
    }


    public class extentdVal
    {
        public decimal? minPrice { get; set; }
        public bool cart { get; set; }
    }
}