namespace BusinessManagementSystemApp.Persistense.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateDataTransectionTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TransectionDatas",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DataTypeId = c.Int(nullable: false),
                        DateTime = c.DateTime(nullable: false),
                        Value = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.DataTypes", t => t.DataTypeId, cascadeDelete: true)
                .Index(t => t.DataTypeId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TransectionDatas", "DataTypeId", "dbo.DataTypes");
            DropIndex("dbo.TransectionDatas", new[] { "DataTypeId" });
            DropTable("dbo.TransectionDatas");
        }
    }
}
