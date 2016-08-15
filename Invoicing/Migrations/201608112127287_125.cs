namespace Invoicing.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _125 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Orders", "SubTotal", c => c.Decimal(precision: 18, scale: 2));
            AlterColumn("dbo.Orders", "ShippingCharges", c => c.Decimal(precision: 18, scale: 2));
            AlterColumn("dbo.Orders", "Tax", c => c.Decimal(precision: 18, scale: 2));
            AlterColumn("dbo.Orders", "Total", c => c.Decimal(precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Orders", "Total", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.Orders", "Tax", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.Orders", "ShippingCharges", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.Orders", "SubTotal", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
    }
}
