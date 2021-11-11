namespace BusinessManagementSystemApp.Persistense.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class chagessGheeTable : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.GheeSales", "OneFourthKg", c => c.Int(nullable: false));
            AlterColumn("dbo.GheeSales", "HalfKg", c => c.Int(nullable: false));
            AlterColumn("dbo.GheeSales", "OneKg", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.GheeSales", "OneKg", c => c.Int());
            AlterColumn("dbo.GheeSales", "HalfKg", c => c.Int());
            AlterColumn("dbo.GheeSales", "OneFourthKg", c => c.Int());
        }
    }
}
