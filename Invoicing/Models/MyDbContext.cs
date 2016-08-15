using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace Invoicing.Models
{
    public class MyDbContext : DbContext
    {
        public MyDbContext() : base("DefaultConnection")
        {
            this.Database.CommandTimeout = 36000;
            this.Configuration.AutoDetectChangesEnabled = false;
            this.Configuration.ProxyCreationEnabled = false;
            /*
             If this is an Azure website, go to Portal, select your site, CONFIGURE tab and add an AppSettings:
             SCM_COMMAND_IDLE_TIMEOUT  to say 3600 (this is in sec - so it is an hour etc.).
            */
        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            modelBuilder.Conventions.Remove<StoreGeneratedIdentityKeyConvention>();

            modelBuilder.Entity<OrderDetail>().HasKey(c => new { c.ID, c.Item });
            modelBuilder.Entity<Order>().HasKey(c => c.ID);
            modelBuilder.Entity<Order>().HasMany(order => order.OrderDetails);
            modelBuilder.Entity<Order>().Property(c => c.ID).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
        }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<Tracing> Tracings { get; set; }
    }

}