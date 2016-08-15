namespace Invoicing.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _4 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Invoices", "FirstName", c => c.String(nullable: false, maxLength: 60));
            AlterColumn("dbo.Invoices", "LastName", c => c.String(nullable: false, maxLength: 60));
            AlterColumn("dbo.Invoices", "Email", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Invoices", "Email", c => c.String());
            AlterColumn("dbo.Invoices", "LastName", c => c.String());
            AlterColumn("dbo.Invoices", "FirstName", c => c.String(maxLength: 60));
        }
    }
}
