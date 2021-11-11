namespace BusinessManagementSystemApp.Persistense.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateAreaTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Areas", "CodeNo", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Areas", "CodeNo");
        }
    }
}
