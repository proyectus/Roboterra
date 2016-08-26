namespace Invoicing.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _140 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Orders", "OrganizationType", c => c.String(nullable: false));
            AlterColumn("dbo.Orders", "SchoolState", c => c.String(nullable: false));
            AlterColumn("dbo.Orders", "SchoolCity", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Orders", "SchoolCity", c => c.String());
            AlterColumn("dbo.Orders", "SchoolState", c => c.String());
            AlterColumn("dbo.Orders", "OrganizationType", c => c.String());
        }
    }
}
