namespace BusinessManagementSystemApp.Persistense.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ModifyClientInfosTable : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.ClientInfoes", "PhoneNo", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.ClientInfoes", "PhoneNo", c => c.String(nullable: false, maxLength: 11));
        }
    }
}
