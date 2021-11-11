namespace BusinessManagementSystemApp.Persistense.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateCustomerTable : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Customers", "Contact", c => c.String(nullable: false));
            DropColumn("dbo.Customers", "Email");
            DropColumn("dbo.Customers", "LoyaltyPoint");
            DropColumn("dbo.Customers", "ImagePath");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Customers", "ImagePath", c => c.String());
            AddColumn("dbo.Customers", "LoyaltyPoint", c => c.Int(nullable: false));
            AddColumn("dbo.Customers", "Email", c => c.String(nullable: false));
            AlterColumn("dbo.Customers", "Contact", c => c.String());
        }
    }
}
