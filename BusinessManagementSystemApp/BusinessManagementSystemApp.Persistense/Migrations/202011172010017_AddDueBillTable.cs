namespace BusinessManagementSystemApp.Persistense.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddDueBillTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.DueBills",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ClientInfoId = c.Int(nullable: false),
                        MonthId = c.String(),
                        Year = c.String(),
                        DueAmount = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ClientInfoes", t => t.ClientInfoId, cascadeDelete: true)
                .Index(t => t.ClientInfoId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.DueBills", "ClientInfoId", "dbo.ClientInfoes");
            DropIndex("dbo.DueBills", new[] { "ClientInfoId" });
            DropTable("dbo.DueBills");
        }
    }
}
