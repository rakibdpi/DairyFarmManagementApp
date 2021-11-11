namespace BusinessManagementSystemApp.Persistense.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ModifyPacketSalesTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PacketSales", "DayNumber", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.PacketSales", "DayNumber");
        }
    }
}
