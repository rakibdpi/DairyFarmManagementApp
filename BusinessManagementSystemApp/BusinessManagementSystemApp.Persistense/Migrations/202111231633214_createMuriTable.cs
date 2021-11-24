namespace BusinessManagementSystemApp.Persistense.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class createMuriTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.MuriSales",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        AreaId = c.Int(nullable: false),
                        ClientInfoId = c.Int(nullable: false),
                        SalesMonth = c.String(),
                        Year = c.String(),
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
            DropForeignKey("dbo.MuriSales", "ClientInfoId", "dbo.ClientInfoes");
            DropForeignKey("dbo.MuriSales", "AreaId", "dbo.Areas");
            DropIndex("dbo.MuriSales", new[] { "ClientInfoId" });
            DropIndex("dbo.MuriSales", new[] { "AreaId" });
            DropTable("dbo.MuriSales");
        }
    }
}
