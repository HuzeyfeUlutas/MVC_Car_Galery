namespace Car_Galery.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v4 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.UserRequests",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Location = c.String(),
                        UserName = c.Int(nullable: false),
                        UserPhoneNumber = c.String(),
                        VehicleId = c.Int(nullable: false),
                        RequestDateTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Vehicles", "UserRequest_Id", c => c.Int());
            CreateIndex("dbo.Vehicles", "UserRequest_Id");
            AddForeignKey("dbo.Vehicles", "UserRequest_Id", "dbo.UserRequests", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Vehicles", "UserRequest_Id", "dbo.UserRequests");
            DropIndex("dbo.Vehicles", new[] { "UserRequest_Id" });
            DropColumn("dbo.Vehicles", "UserRequest_Id");
            DropTable("dbo.UserRequests");
        }
    }
}
