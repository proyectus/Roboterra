namespace Invoicing.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _11 : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.InvoiceDetails", newName: "OrderDetails");
            RenameTable(name: "dbo.Invoices", newName: "Orders");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.Orders", newName: "Invoices");
            RenameTable(name: "dbo.OrderDetails", newName: "InvoiceDetails");
        }
    }
}
