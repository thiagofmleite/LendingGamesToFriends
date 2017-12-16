namespace LendGamesToMyFriends.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class VinculoUserGame : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.UserReferences",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Email = c.String(),
                        Name = c.String(),
                        LastName = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Games", "Status", c => c.Boolean(nullable: false));
            AddColumn("dbo.Games", "User_Id", c => c.Int());
            CreateIndex("dbo.Games", "User_Id");
            AddForeignKey("dbo.Games", "User_Id", "dbo.UserReferences", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Games", "User_Id", "dbo.UserReferences");
            DropIndex("dbo.Games", new[] { "User_Id" });
            DropColumn("dbo.Games", "User_Id");
            DropColumn("dbo.Games", "Status");
            DropTable("dbo.UserReferences");
        }
    }
}
