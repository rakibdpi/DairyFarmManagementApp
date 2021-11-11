namespace BusinessManagementSystemApp.Persistense.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AAA : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ClientInfoes", "AreaId", "dbo.Areas");
            DropIndex("dbo.ClientInfoes", new[] { "AreaId" });
            AlterColumn("dbo.ClientInfoes", "AreaId", c => c.Int(nullable: false));
            CreateIndex("dbo.ClientInfoes", "AreaId");
            AddForeignKey("dbo.ClientInfoes", "AreaId", "dbo.Areas", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ClientInfoes", "AreaId", "dbo.Areas");
            DropIndex("dbo.ClientInfoes", new[] { "AreaId" });
            AlterColumn("dbo.ClientInfoes", "AreaId", c => c.Int());
            CreateIndex("dbo.ClientInfoes", "AreaId");
            AddForeignKey("dbo.ClientInfoes", "AreaId", "dbo.Areas", "Id");
        }
    }
}
