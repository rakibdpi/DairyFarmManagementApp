namespace BusinessManagementSystemApp.Persistense.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateGheeTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.GheeSales",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        AreaId = c.Int(nullable: false),
                        ClientInfoId = c.Int(nullable: false),
                        SalesMonth = c.String(),
                        Year = c.String(),
                        OneFourthKg = c.Int(nullable: false),
                        HalfKg = c.Int(nullable: false),
                        OneKg = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Areas", t => t.AreaId, cascadeDelete: false)
                .ForeignKey("dbo.ClientInfoes", t => t.ClientInfoId, cascadeDelete: false)
                .Index(t => t.AreaId)
                .Index(t => t.ClientInfoId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.GheeSales", "ClientInfoId", "dbo.ClientInfoes");
            DropForeignKey("dbo.GheeSales", "AreaId", "dbo.Areas");
            DropIndex("dbo.GheeSales", new[] { "ClientInfoId" });
            DropIndex("dbo.GheeSales", new[] { "AreaId" });
            DropTable("dbo.GheeSales");
        }
    }
}
