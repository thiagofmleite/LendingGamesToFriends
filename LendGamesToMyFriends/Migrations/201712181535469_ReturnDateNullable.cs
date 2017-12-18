namespace LendGamesToMyFriends.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ReturnDateNullable : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Lendings", "ReturnDate", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Lendings", "ReturnDate", c => c.DateTime(nullable: false));
        }
    }
}
