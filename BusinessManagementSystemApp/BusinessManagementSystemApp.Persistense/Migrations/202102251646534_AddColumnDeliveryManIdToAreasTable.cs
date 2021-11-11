namespace BusinessManagementSystemApp.Persistense.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddColumnDeliveryManIdToAreasTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Areas", "DeliveryManId", c => c.Int(nullable: true));
            CreateIndex("dbo.Areas", "DeliveryManId");
            AddForeignKey("dbo.Areas", "DeliveryManId", "dbo.DeliveryMen", "Id", cascadeDelete: false);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Areas", "DeliveryManId", "dbo.DeliveryMen");
            DropIndex("dbo.Areas", new[] { "DeliveryManId" });
            DropColumn("dbo.Areas", "DeliveryManId");
        }
    }
}
