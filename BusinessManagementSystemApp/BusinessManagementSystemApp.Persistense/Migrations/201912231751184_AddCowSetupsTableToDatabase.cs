namespace BusinessManagementSystemApp.Persistense.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCowSetupsTableToDatabase : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CowSetups",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Number = c.String(),
                        Color = c.String(),
                        IsDelete = c.Boolean(nullable: false),
                        CreateBy = c.String(),
                        CreateDate = c.DateTime(nullable: false),
                        UpdateBy = c.String(),
                        UpdateDate = c.DateTime(),
                        DeleteBy = c.String(),
                        DeleteDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.ClientInfoes", "AreaId", c => c.Int(nullable: false));
            CreateIndex("dbo.ClientInfoes", "AreaId");
            AddForeignKey("dbo.ClientInfoes", "AreaId", "dbo.Areas", "Id", cascadeDelete: false);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ClientInfoes", "AreaId", "dbo.Areas");
            DropIndex("dbo.ClientInfoes", new[] { "AreaId" });
            DropColumn("dbo.ClientInfoes", "AreaId");
            DropTable("dbo.CowSetups");
        }
    }
}
