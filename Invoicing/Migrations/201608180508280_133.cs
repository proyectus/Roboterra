namespace Invoicing.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _133 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Schools", "Type", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Schools", "Type");
        }
    }
}
