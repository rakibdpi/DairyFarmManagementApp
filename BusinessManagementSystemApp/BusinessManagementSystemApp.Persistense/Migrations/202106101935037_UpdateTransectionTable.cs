namespace BusinessManagementSystemApp.Persistense.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateTransectionTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Transactions", "ReferenceTableId", c => c.Int());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Transactions", "ReferenceTableId");
        }
    }
}
