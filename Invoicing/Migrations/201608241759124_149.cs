namespace Invoicing.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _149 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Orders", "ZohoInvoiceID", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Orders", "ZohoInvoiceID");
        }
    }
}
