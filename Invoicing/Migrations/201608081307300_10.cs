namespace Invoicing.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _10 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.InvoiceDetails", "ZohoIdProduct", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.InvoiceDetails", "ZohoIdProduct", c => c.Long(nullable: false));
        }
    }
}
