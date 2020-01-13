namespace MyTrashCollector.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedDesiredDayToViewComployeeModel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Employees", "DesiredDayToView", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Employees", "DesiredDayToView");
        }
    }
}
