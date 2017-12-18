namespace LendGamesToMyFriends.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EstruturaBase : DbMigration
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
                .ForeignKey("dbo.UserReferences", t => t.User_Id)
                .Index(t => t.User_Id);
            
            CreateTable(
                "dbo.UserReferences",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Email = c.String(),
                        Name = c.String(),
                        LastName = c.String(),
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
                "dbo.Lendings",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        LendDate = c.DateTime(nullable: false),
                        Friend_Id = c.Guid(),
                        Game_Id = c.Guid(),
                        User_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Friends", t => t.Friend_Id)
                .ForeignKey("dbo.Games", t => t.Game_Id)
                .ForeignKey("dbo.UserReferences", t => t.User_Id)
                .Index(t => t.Friend_Id)
                .Index(t => t.Game_Id)
                .Index(t => t.User_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Lendings", "User_Id", "dbo.UserReferences");
            DropForeignKey("dbo.Lendings", "Game_Id", "dbo.Games");
            DropForeignKey("dbo.Lendings", "Friend_Id", "dbo.Friends");
            DropForeignKey("dbo.Games", "User_Id", "dbo.Users");
            DropForeignKey("dbo.Friends", "User_Id", "dbo.UserReferences");
            DropIndex("dbo.Lendings", new[] { "User_Id" });
            DropIndex("dbo.Lendings", new[] { "Game_Id" });
            DropIndex("dbo.Lendings", new[] { "Friend_Id" });
            DropIndex("dbo.Games", new[] { "User_Id" });
            DropIndex("dbo.Friends", new[] { "User_Id" });
            DropTable("dbo.Lendings");
            DropTable("dbo.Users");
            DropTable("dbo.Games");
            DropTable("dbo.UserReferences");
            DropTable("dbo.Friends");
        }
    }
}
