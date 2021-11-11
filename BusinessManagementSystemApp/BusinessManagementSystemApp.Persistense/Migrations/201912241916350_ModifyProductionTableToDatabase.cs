namespace BusinessManagementSystemApp.Persistense.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ModifyProductionTableToDatabase : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Productions", "ProductionDate", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Productions", "ProductionDate");
        }
    }
}
