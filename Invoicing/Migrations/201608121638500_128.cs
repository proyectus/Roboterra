namespace Invoicing.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _128 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Tracings",
                c => new
                    {
                        ID = c.Long(nullable: false, identity: true),
                        DateTime = c.DateTime(nullable: false),
                        Message = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Tracings");
        }
    }
}
