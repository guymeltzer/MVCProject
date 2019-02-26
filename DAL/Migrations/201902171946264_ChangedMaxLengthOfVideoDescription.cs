namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangedMaxLengthOfVideoDescription : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Videos", "ShortDescription", c => c.String(nullable: false, maxLength: 100));
            AlterColumn("dbo.Videos", "LongDescription", c => c.String(nullable: false, maxLength: 300));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Videos", "LongDescription", c => c.String(nullable: false, maxLength: 100));
            AlterColumn("dbo.Videos", "ShortDescription", c => c.String(nullable: false, maxLength: 50));
        }
    }
}
