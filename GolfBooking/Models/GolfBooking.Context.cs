﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class golfbookingEntities : DbContext
    {
        public golfbookingEntities()
            : base("name=golfbookingEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public DbSet<golf_atribute_value> golf_atribute_value { get; set; }
        public DbSet<golf_atributes> golf_atributes { get; set; }
        public DbSet<golf_customize_package> golf_customize_package { get; set; }
        public DbSet<golf_order_course> golf_order_course { get; set; }
        public DbSet<golf_order_customize_package> golf_order_customize_package { get; set; }
        public DbSet<golf_order_trophy> golf_order_trophy { get; set; }
        public DbSet<golf_package_stay_image> golf_package_stay_image { get; set; }
        public DbSet<golf_review> golf_review { get; set; }
        public DbSet<golf_trophy_image> golf_trophy_image { get; set; }
        public DbSet<news> news { get; set; }
        public DbSet<country> countries { get; set; }
        public DbSet<province> provinces { get; set; }
        public DbSet<golf> golves { get; set; }
        public DbSet<golf_image> golf_image { get; set; }
        public DbSet<golf_price> golf_price { get; set; }
        public DbSet<golf_package_stay> golf_package_stay { get; set; }
        public DbSet<golf_trophy> golf_trophy { get; set; }
        public DbSet<golf_trophy_category> golf_trophy_category { get; set; }
        public DbSet<slide> slides { get; set; }
        public DbSet<golf_order> golf_order { get; set; }
        public DbSet<region> regions { get; set; }
        public DbSet<golf_order_package_stay> golf_order_package_stay { get; set; }
    }
}
