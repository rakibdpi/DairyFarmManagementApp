namespace BusinessManagementSystemApp.Persistense.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddAmountTypeInDueBillsTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.DueBills", "AmountType", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.DueBills", "AmountType");
        }
    }
}
