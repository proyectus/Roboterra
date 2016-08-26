using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Data.Entity.Infrastructure.Annotations;
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
            modelBuilder.Entity<School>().Property(c => c.Type).HasColumnAnnotation("Index", new IndexAnnotation(new IndexAttribute("IX_School_Type")));
            modelBuilder.Entity<School>().Property(c => c.LocationState).HasColumnAnnotation("Index", new IndexAnnotation(new IndexAttribute("IX_School_State")));
            modelBuilder.Entity<School>().Property(c => c.LocationCity).HasColumnAnnotation("Index", new IndexAnnotation(new IndexAttribute("IX_School_City")));
            modelBuilder.Entity<Order>().Property(x => x.TaxPercentage).HasPrecision(16, 3);
        }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<Tracing> Tracings { get; set; }
        public DbSet<School> Schools { get; set; }
        public DbSet<School2> Schools2 { get; set; }
        public DbSet<Ubigeo> Ubigeos { get; set; }
        public DbSet<StateTable> StateTables { get; set; }
        public DbSet<CityTable> CityTables { get; set; }
        public DbSet<Parameter> Parameters { get; set; }
    }

}   