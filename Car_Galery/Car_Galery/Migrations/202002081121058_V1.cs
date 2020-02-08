namespace Car_Galery.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class V1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Brands",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        BrandImgUrl = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Models",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        BrandId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Brands", t => t.BrandId)
                .Index(t => t.BrandId);
            
            CreateTable(
                "dbo.Vehicles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        ImageUrl = c.String(),
                        Year = c.String(maxLength: 4),
                        Km = c.Int(nullable: false),
                        Color = c.String(),
                        Price = c.Int(nullable: false),
                        Fuel = c.String(),
                        Transmission = c.String(),
                        BrandId = c.Int(nullable: false),
                        ModelId = c.Int(nullable: false),
                        TypeId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Types", t => t.TypeId)
                .ForeignKey("dbo.Models", t => t.ModelId)
                .ForeignKey("dbo.Brands", t => t.BrandId)
                .Index(t => t.BrandId)
                .Index(t => t.ModelId)
                .Index(t => t.TypeId);
            
            CreateTable(
                "dbo.Types",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.TypeBrands",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        BrandId = c.Int(nullable: false),
                        TypeId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Types", t => t.TypeId)
                .ForeignKey("dbo.Brands", t => t.BrandId)
                .Index(t => t.BrandId)
                .Index(t => t.TypeId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Vehicles", "BrandId", "dbo.Brands");
            DropForeignKey("dbo.TypeBrands", "BrandId", "dbo.Brands");
            DropForeignKey("dbo.Models", "BrandId", "dbo.Brands");
            DropForeignKey("dbo.Vehicles", "ModelId", "dbo.Models");
            DropForeignKey("dbo.Vehicles", "TypeId", "dbo.Types");
            DropForeignKey("dbo.TypeBrands", "TypeId", "dbo.Types");
            DropIndex("dbo.TypeBrands", new[] { "TypeId" });
            DropIndex("dbo.TypeBrands", new[] { "BrandId" });
            DropIndex("dbo.Vehicles", new[] { "TypeId" });
            DropIndex("dbo.Vehicles", new[] { "ModelId" });
            DropIndex("dbo.Vehicles", new[] { "BrandId" });
            DropIndex("dbo.Models", new[] { "BrandId" });
            DropTable("dbo.TypeBrands");
            DropTable("dbo.Types");
            DropTable("dbo.Vehicles");
            DropTable("dbo.Models");
            DropTable("dbo.Brands");
        }
    }
}
