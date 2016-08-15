namespace Invoicing.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _8 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.InvoiceDetails", "ID", "dbo.Invoices");
            DropIndex("dbo.InvoiceDetails", new[] { "ID" });
            DropTable("dbo.InvoiceDetails");
            DropTable("dbo.Invoices");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Invoices",
                c => new
                    {
                        ID = c.Long(nullable: false, identity: true),
                        FirstName = c.String(nullable: false, maxLength: 60),
                        LastName = c.String(nullable: false, maxLength: 60),
                        Email = c.String(nullable: false),
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
                        AcceptTC = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.InvoiceDetails",
                c => new
                    {
                        ID = c.Long(nullable: false),
                        Item = c.Int(nullable: false),
                        ZohoIdProduct = c.Long(nullable: false),
                        Description = c.String(),
                        Qty = c.Int(nullable: false),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Total = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => new { t.ID, t.Item });
            
            CreateIndex("dbo.InvoiceDetails", "ID");
            AddForeignKey("dbo.InvoiceDetails", "ID", "dbo.Invoices", "ID");
        }
    }
}
