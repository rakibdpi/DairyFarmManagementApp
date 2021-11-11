namespace BusinessManagementSystemApp.Persistense.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AgainUpdateProductionTable : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Productions", "MilkingTime");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Productions", "MilkingTime", c => c.Int(nullable: false));
        }
    }
}
