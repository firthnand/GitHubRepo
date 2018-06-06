using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace HouseWaterMeters.Models
{
    public class HouseManagerContext : DbContext
    {
        // You can add custom code to this file. Changes will not be overwritten.
        // 
        // If you want Entity Framework to drop and regenerate your database
        // automatically whenever you change your model schema, add the following
        // code to the Application_Start method in your Global.asax file.
        // Note: this will destroy and re-create your database with every model change.
        // 
        // System.Data.Entity.Database.SetInitializer(new System.Data.Entity.DropCreateDatabaseIfModelChanges<HouseManager.Models.HouseManagerContext>());

        public HouseManagerContext() : base("name=HouseManagerContext")
        {

        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<House>().HasRequired(p => p.WaterMeter).WithOptional();
            //modelBuilder.Entity<WaterMeter>().HasRequired(p => p.WMHouse).WithOptional();
            modelBuilder.Entity<House>()
                 .HasOptional(s => s.WaterMeter) // Mark Address property optional in Student entity
                 .WithRequired(ad => ad.WMHouse); // mark Student property as required in StudentAddress entity. C  
            base.OnModelCreating(modelBuilder);
        }


        public DbSet<House> Houses { get; set; }

        public DbSet<WaterMeter> WaterMeters { get; set; }

        public DbSet<WaterValue> WaterValues { get; set; }

    }
}