namespace BusinessManagementSystemApp.Persistense.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateOilSalesTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.OilSells",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        AreaId = c.Int(nullable: false),
                        ClientInfoId = c.Int(nullable: false),
                        SalesMonth = c.String(),
                        Year = c.String(),
                        DayNumber = c.Int(nullable: false),
                        OneKg = c.Int(nullable: false),
                        TwoKg = c.Int(nullable: false),
                        FiveKg = c.Int(nullable: false),
                        IsDelete = c.Boolean(nullable: false),
                        CreateBy = c.String(),
                        CreateDate = c.DateTime(nullable: false),
                        UpdateBy = c.String(),
                        UpdateDate = c.DateTime(),
                        DeleteBy = c.String(),
                        DeleteDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Areas", t => t.AreaId, cascadeDelete: false)
                .ForeignKey("dbo.ClientInfoes", t => t.ClientInfoId, cascadeDelete: false)
                .Index(t => t.AreaId)
                .Index(t => t.ClientInfoId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.OilSells", "ClientInfoId", "dbo.ClientInfoes");
            DropForeignKey("dbo.OilSells", "AreaId", "dbo.Areas");
            DropIndex("dbo.OilSells", new[] { "ClientInfoId" });
            DropIndex("dbo.OilSells", new[] { "AreaId" });
            DropTable("dbo.OilSells");
        }
    }
}
