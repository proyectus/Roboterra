namespace Invoicing.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _138 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CityTables", "New", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.CityTables", "New");
        }
    }
}
