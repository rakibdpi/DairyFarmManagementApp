namespace BusinessManagementSystemApp.Persistense.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateClientInfoTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ClientInfoes", "HalfKg", c => c.Int());
            AddColumn("dbo.ClientInfoes", "SevenAndHalfGm", c => c.Int());
            AddColumn("dbo.ClientInfoes", "OneKg", c => c.Int());
            DropColumn("dbo.ClientInfoes", "MilkQuantity");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ClientInfoes", "MilkQuantity", c => c.Single(nullable: false));
            DropColumn("dbo.ClientInfoes", "OneKg");
            DropColumn("dbo.ClientInfoes", "SevenAndHalfGm");
            DropColumn("dbo.ClientInfoes", "HalfKg");
        }
    }
}
