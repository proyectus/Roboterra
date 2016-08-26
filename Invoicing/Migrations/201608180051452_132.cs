namespace Invoicing.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _132 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.School2", "Type", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.School2", "Type");
        }
    }
}
