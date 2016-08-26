namespace Invoicing.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _142 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Orders", "SchoolID", c => c.Long(nullable: false));
            AlterColumn("dbo.Orders", "SchoolName", c => c.String(maxLength: 60));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Orders", "SchoolName", c => c.String(nullable: false, maxLength: 60));
            DropColumn("dbo.Orders", "SchoolID");
        }
    }
}
