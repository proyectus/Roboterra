namespace Invoicing.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _124 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Orders", "SchoolName", c => c.String(nullable: false, maxLength: 60));
            AddColumn("dbo.Orders", "SubTotal", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.Orders", "ShippingCharges", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.Orders", "Tax", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.Orders", "Total", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Orders", "Total");
            DropColumn("dbo.Orders", "Tax");
            DropColumn("dbo.Orders", "ShippingCharges");
            DropColumn("dbo.Orders", "SubTotal");
            DropColumn("dbo.Orders", "SchoolName");
        }
    }
}
