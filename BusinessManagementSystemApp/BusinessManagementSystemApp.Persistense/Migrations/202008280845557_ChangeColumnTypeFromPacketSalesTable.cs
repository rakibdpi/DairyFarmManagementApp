namespace BusinessManagementSystemApp.Persistense.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeColumnTypeFromPacketSalesTable : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.PacketSales", "OneKg", c => c.Int(nullable: false));
            AlterColumn("dbo.PacketSales", "HalfKg", c => c.Int(nullable: false));
            AlterColumn("dbo.PacketSales", "SevenAndHalfGm", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.PacketSales", "SevenAndHalfGm", c => c.Single(nullable: false));
            AlterColumn("dbo.PacketSales", "HalfKg", c => c.Single(nullable: false));
            AlterColumn("dbo.PacketSales", "OneKg", c => c.Single(nullable: false));
        }
    }
}
