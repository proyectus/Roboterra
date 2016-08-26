namespace Invoicing.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _134 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Schools", "Type", c => c.String(maxLength: 50));
            AlterColumn("dbo.School2", "Type", c => c.String(maxLength: 50));
            CreateIndex("dbo.Schools", "Type", name: "IX_School_Type");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Schools", "IX_School_Type");
            AlterColumn("dbo.School2", "Type", c => c.String());
            AlterColumn("dbo.Schools", "Type", c => c.String());
        }
    }
}
