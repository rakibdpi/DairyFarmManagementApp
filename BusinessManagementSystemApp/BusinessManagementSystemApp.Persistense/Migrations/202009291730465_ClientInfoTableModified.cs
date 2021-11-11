namespace BusinessManagementSystemApp.Persistense.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ClientInfoTableModified : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ClientInfoes", "Code", c => c.String(nullable: false));
            AddColumn("dbo.ClientInfoes", "DayInterval", c => c.Int());
        }
        
        public override void Down()
        {
            DropColumn("dbo.ClientInfoes", "DayInterval");
            DropColumn("dbo.ClientInfoes", "Code");
        }
    }
}
