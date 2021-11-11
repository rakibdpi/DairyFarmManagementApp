namespace BusinessManagementSystemApp.Persistense.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateMilkPurchaseTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.MilkPurchases", "PurchaseDate", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.MilkPurchases", "PurchaseDate");
        }
    }
}
