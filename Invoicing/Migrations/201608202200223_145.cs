namespace Invoicing.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _145 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Orders", "PaymentMethod");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Orders", "PaymentMethod", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
    }
}
