namespace Invoicing.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _148 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Orders", "TaxPercentage", c => c.Decimal(nullable: false, precision: 16, scale: 3));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Orders", "TaxPercentage", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
    }
}
