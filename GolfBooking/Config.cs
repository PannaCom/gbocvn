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
        public static string TrophyGolfImagePath = "/Images/Trophy/";
        public static string NewsGolfImagePath = "/Images/News/";
        public static string SlideImagePath = "/Images/Slide/";
        public static string fromEmail = "golfbookingvietnam@gmail.com";
        public static string passEmail = "ducta123";

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

        //convert tieng viet thanh khong dau va them dau -
        public static string unicodeToNoMark(string input)
        {
            input = input.ToLowerInvariant().Trim();
            if (input == null) return "";
            string noMark = "a,a,a,a,a,a,a,a,a,a,a,a,a,a,a,a,a,a,e,e,e,e,e,e,e,e,e,e,e,e,u,u,u,u,u,u,u,u,u,u,u,u,o,o,o,o,o,o,o,o,o,o,o,o,o,o,o,o,o,o,i,i,i,i,i,i,y,y,y,y,y,y,d,A,A,E,U,O,O,D";
            string unicode = "a,á,à,ả,ã,ạ,â,ấ,ầ,ẩ,ẫ,ậ,ă,ắ,ằ,ẳ,ẵ,ặ,e,é,è,ẻ,ẽ,ẹ,ê,ế,ề,ể,ễ,ệ,u,ú,ù,ủ,ũ,ụ,ư,ứ,ừ,ử,ữ,ự,o,ó,ò,ỏ,õ,ọ,ơ,ớ,ờ,ở,ỡ,ợ,ô,ố,ồ,ổ,ỗ,ộ,i,í,ì,ỉ,ĩ,ị,y,ý,ỳ,ỷ,ỹ,ỵ,đ,Â,Ă,Ê,Ư,Ơ,Ô,Đ";
            string[] a_n = noMark.Split(',');
            string[] a_u = unicode.Split(',');
            for (int i = 0; i < a_n.Length; i++)
            {
                input = input.Replace(a_u[i], a_n[i]);
            }
            input = input.Replace("  ", " ");
            input = Regex.Replace(input, "[^a-zA-Z0-9% ._]", string.Empty);
            input = removeSpecialChar(input);
            input = input.Replace(" ", "-");
            input = input.Replace("--", "-");
            return input;
        }
        public static string removeSpecialChar(string input)
        {
            input = input.Replace("-", "").Replace(":", "").Replace(",", "").Replace("_", "").Replace("'", "").Replace("\"", "").Replace(";", "").Replace("”", "").Replace(".", "").Replace("%", "").Replace("&", "");
            return input;
        }
        public static bool isNormalDay(string sDate)
        {
            if (sDate == null || sDate == "") return false;
            sDate = sDate.Substring(0, 4) + "-" + sDate.Substring(4, 2) + "-" + sDate.Substring(6, 2);
            try
            {
                DateTime d1 = DateTime.Parse(sDate);
                if (d1.DayOfWeek == DayOfWeek.Saturday || d1.DayOfWeek == DayOfWeek.Sunday) 
                    return false; 
                else 
                    return true;
            }
            catch (Exception ex) { 
            }
            return false;
        }
    }
}