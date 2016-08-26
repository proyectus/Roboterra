namespace Invoicing.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _135 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CityTables",
                c => new
                    {
                        State = c.String(nullable: false, maxLength: 128),
                        City = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.State, t.City });
            
            CreateTable(
                "dbo.StateTables",
                c => new
                    {
                        State = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.State);
            
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
        
        public override void Down()
        {
            DropTable("dbo.Ubigeos");
            DropTable("dbo.StateTables");
            DropTable("dbo.CityTables");
        }
    }
}
