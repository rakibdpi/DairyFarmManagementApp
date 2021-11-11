namespace BusinessManagementSystemApp.Persistense.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddPaymetsTableToDatabase : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Payments",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Month = c.String(nullable: false),
                        Year = c.String(nullable: false),
                        AreaId = c.Int(nullable: false),
                        ClientInfoId = c.Int(nullable: false),
                        BillAmount = c.Decimal(nullable: false, precision: 18, scale: 2),
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
            DropForeignKey("dbo.Payments", "ClientInfoId", "dbo.ClientInfoes");
            DropForeignKey("dbo.Payments", "AreaId", "dbo.Areas");
            DropIndex("dbo.Payments", new[] { "ClientInfoId" });
            DropIndex("dbo.Payments", new[] { "AreaId" });
            DropTable("dbo.Payments");
        }
    }
}
