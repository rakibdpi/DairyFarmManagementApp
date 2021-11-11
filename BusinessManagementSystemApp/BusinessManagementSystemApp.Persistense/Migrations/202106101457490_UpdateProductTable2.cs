namespace BusinessManagementSystemApp.Persistense.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateProductTable2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Products", "Type", c => c.Int());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Products", "Type");
        }
    }
}
