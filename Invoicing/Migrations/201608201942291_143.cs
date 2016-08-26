namespace Invoicing.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _143 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.OrderDetails", "Name", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.OrderDetails", "Name");
        }
    }
}
