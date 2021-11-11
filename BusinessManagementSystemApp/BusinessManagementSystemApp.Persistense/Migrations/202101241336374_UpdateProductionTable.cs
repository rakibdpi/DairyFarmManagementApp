namespace BusinessManagementSystemApp.Persistense.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateProductionTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Productions", "ProductionMonth", c => c.String());
            AddColumn("dbo.Productions", "Year", c => c.String());
            AddColumn("dbo.Productions", "MorningQuantity", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.Productions", "AfterNoonQuantity", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.Productions", "NightQuantity", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.Productions", "OtherTime", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.Productions", "DayNumber", c => c.Int(nullable: false));
            DropColumn("dbo.Productions", "Quantity");
            DropColumn("dbo.Productions", "ProductionDate");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Productions", "ProductionDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.Productions", "Quantity", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            DropColumn("dbo.Productions", "DayNumber");
            DropColumn("dbo.Productions", "OtherTime");
            DropColumn("dbo.Productions", "NightQuantity");
            DropColumn("dbo.Productions", "AfterNoonQuantity");
            DropColumn("dbo.Productions", "MorningQuantity");
            DropColumn("dbo.Productions", "Year");
            DropColumn("dbo.Productions", "ProductionMonth");
        }
    }
}
