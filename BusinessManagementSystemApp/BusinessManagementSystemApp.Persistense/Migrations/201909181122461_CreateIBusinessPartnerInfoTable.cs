namespace BusinessManagementSystemApp.Persistense.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateIBusinessPartnerInfoTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.BusinessPartnerInfoes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Occupation = c.String(),
                        MobileNo = c.String(),
                        Email = c.String(),
                        Age = c.Int(),
                        NidNo = c.String(),
                        Education = c.String(),
                        PresentAddress = c.String(),
                        PermanentAddress = c.String(),
                        Gender = c.Boolean(),
                        FatherName = c.String(),
                        MotherName = c.String(),
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
            DropTable("dbo.BusinessPartnerInfoes");
        }
    }
}
