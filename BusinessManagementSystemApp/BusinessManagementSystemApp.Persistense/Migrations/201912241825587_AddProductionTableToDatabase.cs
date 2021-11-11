namespace BusinessManagementSystemApp.Persistense.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddProductionTableToDatabase : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Productions",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        MilkingTime = c.Int(nullable: false),
                        CowSetupId = c.Int(nullable: false),
                        Quantity = c.Decimal(nullable: false, precision: 18, scale: 2),
                        IsDelete = c.Boolean(nullable: false),
                        CreateBy = c.String(),
                        CreateDate = c.DateTime(nullable: false),
                        UpdateBy = c.String(),
                        UpdateDate = c.DateTime(),
                        DeleteBy = c.String(),
                        DeleteDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.CowSetups", t => t.CowSetupId, cascadeDelete: false)
                .Index(t => t.CowSetupId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Productions", "CowSetupId", "dbo.CowSetups");
            DropIndex("dbo.Productions", new[] { "CowSetupId" });
            DropTable("dbo.Productions");
        }
    }
}
