namespace MyTrashCollector.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedBooleanPickedUpConfirmation : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Customers", "isPickedUp", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Customers", "isPickedUp");
        }
    }
}
