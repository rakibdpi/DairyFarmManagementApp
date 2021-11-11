namespace BusinessManagementSystemApp.Persistense.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class chagesGheeTable : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.GheeSales");
            AlterColumn("dbo.GheeSales", "Id", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.GheeSales", "Id");
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.GheeSales");
            AlterColumn("dbo.GheeSales", "Id", c => c.Long(nullable: false, identity: true));
            AddPrimaryKey("dbo.GheeSales", "Id");
        }
    }
}
