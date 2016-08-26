namespace Invoicing.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _130 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Schools",
                c => new
                    {
                        ID = c.Long(nullable: false, identity: true),
                        DistrictName = c.String(),
                        SchoolName = c.String(),
                        PrincipalName = c.String(),
                        FirstName = c.String(),
                        LastName = c.String(),
                        EmailAddress = c.String(),
                        Phone = c.String(),
                        Fax = c.String(),
                        LocationAddress = c.String(),
                        LocationCity = c.String(),
                        LocationState = c.String(),
                        LocationZip = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Schools");
        }
    }
}
