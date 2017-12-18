namespace LendGamesToMyFriends.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ForeignKeyLending : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Lendings", "Friend_Id", "dbo.Friends");
            DropForeignKey("dbo.Lendings", "Game_Id", "dbo.Games");
            DropForeignKey("dbo.Lendings", "User_Id", "dbo.UserReferences");
            DropIndex("dbo.Lendings", new[] { "Friend_Id" });
            DropIndex("dbo.Lendings", new[] { "Game_Id" });
            DropIndex("dbo.Lendings", new[] { "User_Id" });
            RenameColumn(table: "dbo.Lendings", name: "Friend_Id", newName: "FriendId");
            RenameColumn(table: "dbo.Lendings", name: "Game_Id", newName: "GameId");
            RenameColumn(table: "dbo.Lendings", name: "User_Id", newName: "UserId");
            AlterColumn("dbo.Lendings", "FriendId", c => c.Guid(nullable: false));
            AlterColumn("dbo.Lendings", "GameId", c => c.Guid(nullable: false));
            AlterColumn("dbo.Lendings", "UserId", c => c.Guid(nullable: false));
            CreateIndex("dbo.Lendings", "FriendId");
            CreateIndex("dbo.Lendings", "GameId");
            CreateIndex("dbo.Lendings", "UserId");
            AddForeignKey("dbo.Lendings", "FriendId", "dbo.Friends", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Lendings", "GameId", "dbo.Games", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Lendings", "UserId", "dbo.Users", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Lendings", "UserId", "dbo.Users");
            DropForeignKey("dbo.Lendings", "GameId", "dbo.Games");
            DropForeignKey("dbo.Lendings", "FriendId", "dbo.Friends");
            DropIndex("dbo.Lendings", new[] { "UserId" });
            DropIndex("dbo.Lendings", new[] { "GameId" });
            DropIndex("dbo.Lendings", new[] { "FriendId" });
            AlterColumn("dbo.Lendings", "UserId", c => c.Guid());
            AlterColumn("dbo.Lendings", "GameId", c => c.Guid());
            AlterColumn("dbo.Lendings", "FriendId", c => c.Guid());
            RenameColumn(table: "dbo.Lendings", name: "UserId", newName: "User_Id");
            RenameColumn(table: "dbo.Lendings", name: "GameId", newName: "Game_Id");
            RenameColumn(table: "dbo.Lendings", name: "FriendId", newName: "Friend_Id");
            CreateIndex("dbo.Lendings", "User_Id");
            CreateIndex("dbo.Lendings", "Game_Id");
            CreateIndex("dbo.Lendings", "Friend_Id");
            AddForeignKey("dbo.Lendings", "User_Id", "dbo.UserReferences", "Id");
            AddForeignKey("dbo.Lendings", "Game_Id", "dbo.Games", "Id");
            AddForeignKey("dbo.Lendings", "Friend_Id", "dbo.Friends", "Id");
        }
    }
}
