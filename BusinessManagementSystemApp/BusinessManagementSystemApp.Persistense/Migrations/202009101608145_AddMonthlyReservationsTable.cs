namespace BusinessManagementSystemApp.Persistense.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddMonthlyReservationsTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.MonthlyReservations",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        ClientInfoId = c.Int(nullable: false),
                        HalfKg = c.Int(),
                        SevenAndHalfGm = c.Int(),
                        OneKg = c.Int(),
                        IsDelete = c.Boolean(nullable: false),
                        CreateBy = c.String(),
                        CreateDate = c.DateTime(nullable: false),
                        UpdateBy = c.String(),
                        UpdateDate = c.DateTime(),
                        DeleteBy = c.String(),
                        DeleteDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ClientInfoes", t => t.ClientInfoId, cascadeDelete: false)
                .Index(t => t.ClientInfoId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.MonthlyReservations", "ClientInfoId", "dbo.ClientInfoes");
            DropIndex("dbo.MonthlyReservations", new[] { "ClientInfoId" });
            DropTable("dbo.MonthlyReservations");
        }
    }
}
