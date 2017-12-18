namespace LendGamesToMyFriends.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class LendingReturn : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Lendings", "ReturnDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.Lendings", "Status", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Lendings", "Status");
            DropColumn("dbo.Lendings", "ReturnDate");
        }
    }
}
