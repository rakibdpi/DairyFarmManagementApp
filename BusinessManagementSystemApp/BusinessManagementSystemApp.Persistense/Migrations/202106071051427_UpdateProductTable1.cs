namespace BusinessManagementSystemApp.Persistense.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateProductTable1 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Products", "CategoryId", "dbo.Categories");
            DropIndex("dbo.Products", new[] { "CategoryId" });
            AddColumn("dbo.Products", "Color", c => c.String(nullable: false));
            AddColumn("dbo.Products", "Weight", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.Products", "Age", c => c.String());
            AddColumn("dbo.Products", "Status", c => c.String());
            DropColumn("dbo.Products", "Name");
            DropColumn("dbo.Products", "CategoryId");
            DropColumn("dbo.Products", "ReorderLevel");
            DropColumn("dbo.Products", "Description");
            DropColumn("dbo.Products", "ImagePath");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Products", "ImagePath", c => c.String());
            AddColumn("dbo.Products", "Description", c => c.String());
            AddColumn("dbo.Products", "ReorderLevel", c => c.Int(nullable: false));
            AddColumn("dbo.Products", "CategoryId", c => c.Int(nullable: false));
            AddColumn("dbo.Products", "Name", c => c.String(nullable: false));
            DropColumn("dbo.Products", "Status");
            DropColumn("dbo.Products", "Age");
            DropColumn("dbo.Products", "Weight");
            DropColumn("dbo.Products", "Color");
            CreateIndex("dbo.Products", "CategoryId");
            AddForeignKey("dbo.Products", "CategoryId", "dbo.Categories", "Id", cascadeDelete: true);
        }
    }
}
