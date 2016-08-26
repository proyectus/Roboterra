namespace Invoicing.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _136 : DbMigration
    {
        public override void Up()
        {
            DropTable("dbo.Ubigeos");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Ubigeos",
                c => new
                    {
                        ID = c.Long(nullable: false, identity: true),
                        zip = c.String(),
                        state = c.String(),
                        city = c.String(),
                        lat = c.String(),
                        lng = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
        }
    }
}
