namespace BusinessManagementSystemApp.Persistense.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ModifyColumnInPacketSalesTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PacketSales", "SalesMonth", c => c.String());
            DropColumn("dbo.PacketSales", "SalesDate");
        }
        
        public override void Down()
        {
            AddColumn("dbo.PacketSales", "SalesDate", c => c.DateTime(nullable: false));
            DropColumn("dbo.PacketSales", "SalesMonth");
        }
    }
}
