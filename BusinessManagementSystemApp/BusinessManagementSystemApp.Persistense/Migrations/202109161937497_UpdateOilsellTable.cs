namespace BusinessManagementSystemApp.Persistense.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateOilsellTable : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.OilSells", "DayNumber", c => c.Int());
            AlterColumn("dbo.OilSells", "OneKg", c => c.Int());
            AlterColumn("dbo.OilSells", "TwoKg", c => c.Int());
            AlterColumn("dbo.OilSells", "FiveKg", c => c.Int());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.OilSells", "FiveKg", c => c.Int(nullable: false));
            AlterColumn("dbo.OilSells", "TwoKg", c => c.Int(nullable: false));
            AlterColumn("dbo.OilSells", "OneKg", c => c.Int(nullable: false));
            AlterColumn("dbo.OilSells", "DayNumber", c => c.Int(nullable: false));
        }
    }
}
