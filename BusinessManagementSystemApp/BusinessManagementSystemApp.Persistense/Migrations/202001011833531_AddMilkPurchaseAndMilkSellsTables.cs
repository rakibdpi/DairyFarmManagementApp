namespace BusinessManagementSystemApp.Persistense.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddMilkPurchaseAndMilkSellsTables : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.MilkPurchases",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        MilkSuppliersId = c.Int(nullable: false),
                        MilkQuantity = c.Decimal(nullable: false, precision: 18, scale: 2),
                        IsDelete = c.Boolean(nullable: false),
                        CreateBy = c.String(),
                        CreateDate = c.DateTime(nullable: false),
                        UpdateBy = c.String(),
                        UpdateDate = c.DateTime(),
                        DeleteBy = c.String(),
                        DeleteDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.MilkSuppliers", t => t.MilkSuppliersId, cascadeDelete: false)
                .Index(t => t.MilkSuppliersId);
            
            CreateTable(
                "dbo.MilkSuppliers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        PhoneNo = c.String(),
                        Address = c.String(),
                        IsDelete = c.Boolean(nullable: false),
                        CreateBy = c.String(),
                        CreateDate = c.DateTime(nullable: false),
                        UpdateBy = c.String(),
                        UpdateDate = c.DateTime(),
                        DeleteBy = c.String(),
                        DeleteDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.PacketSales",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        AreaId = c.Int(nullable: false),
                        ClientInfoId = c.Int(nullable: false),
                        SalesDate = c.DateTime(nullable: false),
                        OneKg = c.Single(nullable: false),
                        HalfKg = c.Single(nullable: false),
                        SevenAndHalfGm = c.Single(nullable: false),
                        IsDelete = c.Boolean(nullable: false),
                        CreateBy = c.String(),
                        CreateDate = c.DateTime(nullable: false),
                        UpdateBy = c.String(),
                        UpdateDate = c.DateTime(),
                        DeleteBy = c.String(),
                        DeleteDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Areas", t => t.AreaId, cascadeDelete: true)
                .ForeignKey("dbo.ClientInfoes", t => t.ClientInfoId, cascadeDelete: false)
                .Index(t => t.AreaId)
                .Index(t => t.ClientInfoId);
            
            CreateTable(
                "dbo.RowSales",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CustomerName = c.String(),
                        MobileNo = c.String(),
                        Quantity = c.Decimal(nullable: false, precision: 18, scale: 2),
                        SalesDate = c.DateTime(nullable: false),
                        IsDelete = c.Boolean(nullable: false),
                        CreateBy = c.String(),
                        CreateDate = c.DateTime(nullable: false),
                        UpdateBy = c.String(),
                        UpdateDate = c.DateTime(),
                        DeleteBy = c.String(),
                        DeleteDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PacketSales", "ClientInfoId", "dbo.ClientInfoes");
            DropForeignKey("dbo.PacketSales", "AreaId", "dbo.Areas");
            DropForeignKey("dbo.MilkPurchases", "MilkSuppliersId", "dbo.MilkSuppliers");
            DropIndex("dbo.PacketSales", new[] { "ClientInfoId" });
            DropIndex("dbo.PacketSales", new[] { "AreaId" });
            DropIndex("dbo.MilkPurchases", new[] { "MilkSuppliersId" });
            DropTable("dbo.RowSales");
            DropTable("dbo.PacketSales");
            DropTable("dbo.MilkSuppliers");
            DropTable("dbo.MilkPurchases");
        }
    }
}
