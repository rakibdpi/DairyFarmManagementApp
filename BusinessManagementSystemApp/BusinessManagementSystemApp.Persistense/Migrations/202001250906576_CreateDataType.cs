namespace BusinessManagementSystemApp.Persistense.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateDataType : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.DataTypes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.DataTypes");
        }
    }
}
