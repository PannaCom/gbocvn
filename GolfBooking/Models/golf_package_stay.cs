//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace GolfBooking.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class golf_package_stay
    {
        public int id { get; set; }
        public string golf_id { get; set; }
        public string name { get; set; }
        public string des { get; set; }
        public string full_detail { get; set; }
        public string image { get; set; }
        public Nullable<decimal> min_price { get; set; }
        public Nullable<byte> deleted { get; set; }
        public Nullable<byte> type { get; set; }
    }
}
