using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GolfBooking.Models;

namespace GolfBooking.Models
{
    public class ModelPacketStayItem:golf_package_stay
    {
        public IEnumerable<string> listImages { get; set; }
    }
}