namespace Invoicing.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _139 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Schools", "LocationCity", c => c.String(maxLength: 50));
            AlterColumn("dbo.Schools", "LocationState", c => c.String(maxLength: 50));
            CreateIndex("dbo.Schools", "LocationCity", name: "IX_School_City");
            CreateIndex("dbo.Schools", "LocationState", name: "IX_School_State");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Schools", "IX_School_State");
            DropIndex("dbo.Schools", "IX_School_City");
            AlterColumn("dbo.Schools", "LocationState", c => c.String());
            AlterColumn("dbo.Schools", "LocationCity", c => c.String());
        }
    }
}
