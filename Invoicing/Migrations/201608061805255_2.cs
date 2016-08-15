namespace Invoicing.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _2 : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.Invoices");
            CreateTable(
                "dbo.InvoiceDetails",
                c => new
                    {
                        ID = c.Long(nullable: false),
                        Item = c.Int(nullable: false),
                        ZohoIdProduct = c.Long(nullable: false),
                        Description = c.String(),
                    })
                .PrimaryKey(t => new { t.ID, t.Item })
                .ForeignKey("dbo.Invoices", t => t.ID)
                .Index(t => t.ID);
            
            AlterColumn("dbo.Invoices", "ID", c => c.Long(nullable: false, identity: true));
            AddPrimaryKey("dbo.Invoices", "ID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.InvoiceDetails", "ID", "dbo.Invoices");
            DropIndex("dbo.InvoiceDetails", new[] { "ID" });
            DropPrimaryKey("dbo.Invoices");
            AlterColumn("dbo.Invoices", "ID", c => c.Long(nullable: false));
            DropTable("dbo.InvoiceDetails");
            AddPrimaryKey("dbo.Invoices", "ID");
        }
    }
}
