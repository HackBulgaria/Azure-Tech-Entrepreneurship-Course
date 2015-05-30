namespace TwitterApi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Friendships",
                c => new
                    {
                        FollowerId = c.Long(nullable: false),
                        FollowedId = c.Long(nullable: false),
                        Date = c.DateTimeOffset(nullable: false, precision: 7),
                    })
                .PrimaryKey(t => new { t.FollowerId, t.FollowedId })
                .ForeignKey("dbo.Users", t => t.FollowedId)
                .ForeignKey("dbo.Users", t => t.FollowerId)
                .Index(t => t.FollowerId)
                .Index(t => t.FollowedId);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        FirstName = c.String(),
                        LastName = c.String(),
                        Birthday = c.DateTimeOffset(nullable: false, precision: 7),
                        Email = c.String(),
                        Password = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Tweets",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Text = c.String(),
                        Date = c.DateTimeOffset(nullable: false, precision: 7),
                        UserId = c.Long(nullable: false),
                        ParentId = c.Long(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Tweets", t => t.ParentId)
                .ForeignKey("dbo.Users", t => t.UserId)
                .Index(t => t.UserId)
                .Index(t => t.ParentId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Tweets", "UserId", "dbo.Users");
            DropForeignKey("dbo.Tweets", "ParentId", "dbo.Tweets");
            DropForeignKey("dbo.Friendships", "FollowerId", "dbo.Users");
            DropForeignKey("dbo.Friendships", "FollowedId", "dbo.Users");
            DropIndex("dbo.Tweets", new[] { "ParentId" });
            DropIndex("dbo.Tweets", new[] { "UserId" });
            DropIndex("dbo.Friendships", new[] { "FollowedId" });
            DropIndex("dbo.Friendships", new[] { "FollowerId" });
            DropTable("dbo.Tweets");
            DropTable("dbo.Users");
            DropTable("dbo.Friendships");
        }
    }
}
