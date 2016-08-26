namespace Invoicing.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _147 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Orders", "TaxPercentage", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Orders", "TaxPercentage");
        }
    }
}
