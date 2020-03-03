namespace Car_Galery.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v5 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.UserRequests", "UserName", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.UserRequests", "UserName", c => c.Int(nullable: false));
        }
    }
}
