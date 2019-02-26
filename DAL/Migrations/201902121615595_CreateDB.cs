namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateDB : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        UserID = c.Int(nullable: false, identity: true),
                        Username = c.String(nullable: false, maxLength: 15),
                        Password = c.String(nullable: false, maxLength: 20),
                        ConfirmPassword = c.String(nullable: false, maxLength: 20),
                        FirstName = c.String(nullable: false, maxLength: 20),
                        LastName = c.String(nullable: false, maxLength: 20),
                        Address = c.String(nullable: false, maxLength: 50),
                        Email = c.String(nullable: false, maxLength: 50),
                        BirthDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.UserID);
            
            CreateTable(
                "dbo.Videos",
                c => new
                    {
                        VideoID = c.Int(nullable: false, identity: true),
                        OwnerID = c.Int(),
                        CurrentUserID = c.Int(),
                        Title = c.String(nullable: false, maxLength: 30),
                        ShortDescription = c.String(nullable: false, maxLength: 50),
                        LongDescription = c.String(nullable: false, maxLength: 100),
                        Price = c.Single(nullable: false),
                        ReleaseYear = c.Int(nullable: false),
                        Image1 = c.Binary(),
                        Image2 = c.Binary(),
                        Image3 = c.Binary(),
                        CurrentUser_UserID = c.Int(),
                        Owner_UserID = c.Int(),
                    })
                .PrimaryKey(t => t.VideoID)
                .ForeignKey("dbo.Users", t => t.CurrentUser_UserID)
                .ForeignKey("dbo.Users", t => t.Owner_UserID)
                .Index(t => t.CurrentUser_UserID)
                .Index(t => t.Owner_UserID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Videos", "Owner_UserID", "dbo.Users");
            DropForeignKey("dbo.Videos", "CurrentUser_UserID", "dbo.Users");
            DropIndex("dbo.Videos", new[] { "Owner_UserID" });
            DropIndex("dbo.Videos", new[] { "CurrentUser_UserID" });
            DropTable("dbo.Videos");
            DropTable("dbo.Users");
        }
    }
}
