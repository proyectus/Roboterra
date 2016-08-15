namespace Invoicing.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _5 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.InvoiceDetails", "Qty", c => c.Int(nullable: false));
            AddColumn("dbo.Invoices", "AcceptTC", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Invoices", "AcceptTC");
            DropColumn("dbo.InvoiceDetails", "Qty");
        }
    }
}
