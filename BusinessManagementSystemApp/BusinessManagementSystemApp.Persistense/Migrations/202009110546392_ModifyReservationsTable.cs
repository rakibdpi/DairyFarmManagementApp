namespace BusinessManagementSystemApp.Persistense.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ModifyReservationsTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.MonthlyReservations", "DayNumber", c => c.Int());
        }
        
        public override void Down()
        {
            DropColumn("dbo.MonthlyReservations", "DayNumber");
        }
    }
}
