namespace Invoicing.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Invoices",
                c => new
                    {
                        ID = c.Long(nullable: false),
                        FirstName = c.String(),
                        LastName = c.String(),
                        Email = c.String(),
                        Phone = c.String(),
                        Mobile = c.String(),
                        ShippingAddress = c.String(),
                        ShippingCountry = c.String(),
                        ShippingCity = c.String(),
                        ShippingState = c.String(),
                        ShippingZip = c.String(),
                        FD_FirstName = c.String(),
                        FD_LastName = c.String(),
                        FD_Email = c.String(),
                        FD_Phone = c.String(),
                        FD_Mobile = c.String(),
                        FD_ShippingAddress = c.String(),
                        FD_ShippingCountry = c.String(),
                        FD_ShippingCity = c.String(),
                        FD_ShippingState = c.String(),
                        FD_ShippingZip = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Invoices");
        }
    }
}
