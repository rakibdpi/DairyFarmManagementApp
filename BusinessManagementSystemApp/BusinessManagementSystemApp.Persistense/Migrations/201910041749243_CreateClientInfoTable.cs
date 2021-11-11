namespace BusinessManagementSystemApp.Persistense.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateClientInfoTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ClientInfoes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        PhoneNo = c.String(nullable: false, maxLength: 11),
                        MilkQuantity = c.Single(nullable: false),
                        Address = c.String(),
                        IsActive = c.Boolean(nullable: false),
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
            DropTable("dbo.ClientInfoes");
        }
    }
}
