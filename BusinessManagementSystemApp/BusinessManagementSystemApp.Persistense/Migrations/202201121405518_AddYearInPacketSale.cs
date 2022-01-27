namespace BusinessManagementSystemApp.Persistense.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddYearInPacketSale : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PacketSales", "Year", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.PacketSales", "Year");
        }
    }
}
