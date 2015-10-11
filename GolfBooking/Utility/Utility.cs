using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text.RegularExpressions;
namespace GolfBooking.Utility
{
    public class Utility
    {

        public static string stripTagHtml(string str)
        {
            return Regex.Replace(str, @"<[^>]*>", String.Empty);
        }
    }
}