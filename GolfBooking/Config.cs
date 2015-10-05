using System;
using System.Collections.Generic;
using System.Data.Spatial;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace GolfBooking
{
    public class Config
    {
        public static string GolfImagePath = "/Images/Golf/";
        public static string PackageGolfImagePath = "/Images/PackageGolf/";
        public static string domain = "http://golf.binhyen.net/";
        //convert longitude latitude to geography
        public static DbGeography CreatePoint(double? latitude, double? longitude)
        {
            if (latitude == null || longitude == null) return null;
            latitude = (double)latitude;
            longitude = (double)longitude;
            return DbGeography.FromText(String.Format("POINT({1} {0})", latitude, longitude));
        }
        public static string smoothDes(string des) {
            Regex.Replace(des, "<.*?>", string.Empty);
            string[] arDes=des.Split(' ');
            int l = arDes.Length > 60 ? 60 : arDes.Length;
            string rs = "";
            for (int i = 0; i < l; i++) {
                rs += arDes[i] + " ";
            }
            return rs;
        }
        public static string smoothDesSmall(string des)
        {
            Regex.Replace(des, "<.*?>", string.Empty);
            string[] arDes = des.Split(' ');
            int l = arDes.Length > 20 ? 20 : arDes.Length;
            string rs = "";
            for (int i = 0; i < l; i++)
            {
                rs += arDes[i] + " ";
            }
            return rs;
        }
        public static string getRegionById(int id) {
            if (id < 0 || id == null) return "";
            string[] region = new string[] { "", "North", "Central", "South", "Western", "Eastern" };
            return region[id];
        }
        public static string convertToDateTimeId(string d)
        {
            DateTime d1;
            try
            {
                d1 = DateTime.Parse(d);
                return d1.Year.ToString() + d1.Month.ToString("00") + d1.Day.ToString("00");
            }
            catch (Exception ex)
            {
                d1 = DateTime.Now;
                return d1.Year.ToString() + d1.Month.ToString("00") + d1.Day.ToString("00");
            }
            d1 = DateTime.Now;
            return d1.Year.ToString() + d1.Month.ToString("00") + d1.Day.ToString("00");
        }
    }
}