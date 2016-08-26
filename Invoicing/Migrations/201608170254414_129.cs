namespace Invoicing.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _129 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Orders", "Extension", c => c.String());
            AddColumn("dbo.Orders", "OccupationTitle", c => c.String());
            AddColumn("dbo.Orders", "OrganizationType", c => c.String());
            AddColumn("dbo.Orders", "SchoolState", c => c.String());
            AddColumn("dbo.Orders", "SchoolCity", c => c.String());
            AddColumn("dbo.Orders", "OfficeClassRoomLocation", c => c.String());
            AddColumn("dbo.Orders", "FD_OccupationTitle", c => c.String());
            AddColumn("dbo.Orders", "FD_Extension", c => c.String());
            AddColumn("dbo.Orders", "FD_OfficeLocation", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Orders", "FD_OfficeLocation");
            DropColumn("dbo.Orders", "FD_Extension");
            DropColumn("dbo.Orders", "FD_OccupationTitle");
            DropColumn("dbo.Orders", "OfficeClassRoomLocation");
            DropColumn("dbo.Orders", "SchoolCity");
            DropColumn("dbo.Orders", "SchoolState");
            DropColumn("dbo.Orders", "OrganizationType");
            DropColumn("dbo.Orders", "OccupationTitle");
            DropColumn("dbo.Orders", "Extension");
        }
    }
}
