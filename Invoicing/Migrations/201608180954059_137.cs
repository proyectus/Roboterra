namespace Invoicing.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _137 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Ubigeos",
                c => new
                    {
                        ID = c.Long(nullable: false),
                        zip = c.String(),
                        state = c.String(),
                        city = c.String(),
                        lat = c.String(),
                        lng = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Ubigeos");
        }
    }
}
