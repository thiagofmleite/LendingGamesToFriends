namespace LendGamesToMyFriends.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FriendsCRUD : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Friends",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        LastName = c.String(),
                        User_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.UserReferences", t => t.User_Id)
                .Index(t => t.User_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Friends", "User_Id", "dbo.UserReferences");
            DropIndex("dbo.Friends", new[] { "User_Id" });
            DropTable("dbo.Friends");
        }
    }
}
