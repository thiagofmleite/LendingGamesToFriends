namespace LendGamesToMyFriends.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class CorrigidaReferenciaAmigoUsuario : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Friends",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(),
                        LastName = c.String(),
                        User_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.User_Id)
                .Index(t => t.User_Id);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(nullable: false),
                        LastName = c.String(nullable: false),
                        Email = c.String(nullable: false),
                        Password = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Games",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Title = c.String(),
                        Cover = c.String(),
                        Status = c.Boolean(nullable: false),
                        User_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.User_Id)
                .Index(t => t.User_Id);
            
            CreateTable(
                "dbo.Lendings",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        FriendId = c.Guid(nullable: false),
                        GameId = c.Guid(nullable: false),
                        UserId = c.Guid(nullable: false),
                        LendDate = c.DateTime(nullable: false),
                        ReturnDate = c.DateTime(),
                        Status = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Friends", t => t.FriendId, cascadeDelete: true)
                .ForeignKey("dbo.Games", t => t.GameId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.FriendId)
                .Index(t => t.GameId)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Lendings", "UserId", "dbo.Users");
            DropForeignKey("dbo.Lendings", "GameId", "dbo.Games");
            DropForeignKey("dbo.Lendings", "FriendId", "dbo.Friends");
            DropForeignKey("dbo.Games", "User_Id", "dbo.Users");
            DropForeignKey("dbo.Friends", "User_Id", "dbo.Users");
            DropIndex("dbo.Lendings", new[] { "UserId" });
            DropIndex("dbo.Lendings", new[] { "GameId" });
            DropIndex("dbo.Lendings", new[] { "FriendId" });
            DropIndex("dbo.Games", new[] { "User_Id" });
            DropIndex("dbo.Friends", new[] { "User_Id" });
            DropTable("dbo.Lendings");
            DropTable("dbo.Games");
            DropTable("dbo.Users");
            DropTable("dbo.Friends");
        }
    }
}
