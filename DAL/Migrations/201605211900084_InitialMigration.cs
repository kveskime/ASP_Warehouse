namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ProductInPurchases",
                c => new
                    {
                        ProductInPurchaseId = c.Int(nullable: false, identity: true),
                        Description = c.String(maxLength: 128),
                        Quantity = c.Int(nullable: false),
                        ProductId = c.Int(nullable: false),
                        PurchaseId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ProductInPurchaseId)
                .ForeignKey("dbo.Products", t => t.ProductId, cascadeDelete: true)
                .ForeignKey("dbo.Purchases", t => t.PurchaseId, cascadeDelete: true)
                .Index(t => t.ProductId)
                .Index(t => t.PurchaseId);
            
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        ProductId = c.Int(nullable: false, identity: true),
                        Description = c.String(maxLength: 128),
                        Name = c.String(maxLength: 128),
                        ProductTypeId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ProductId)
                .ForeignKey("dbo.ProductTypes", t => t.ProductTypeId, cascadeDelete: true)
                .Index(t => t.ProductTypeId);
            
            CreateTable(
                "dbo.ProductInWarehouses",
                c => new
                    {
                        ProductInWarehouseId = c.Int(nullable: false, identity: true),
                        Description = c.String(maxLength: 128),
                        WarehouseId = c.Int(nullable: false),
                        ProductId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ProductInWarehouseId)
                .ForeignKey("dbo.Products", t => t.ProductId, cascadeDelete: true)
                .ForeignKey("dbo.Warehouses", t => t.WarehouseId, cascadeDelete: true)
                .Index(t => t.WarehouseId)
                .Index(t => t.ProductId);
            
            CreateTable(
                "dbo.Warehouses",
                c => new
                    {
                        WarehouseId = c.Int(nullable: false, identity: true),
                        Description = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.WarehouseId);
            
            CreateTable(
                "dbo.ProductInWorks",
                c => new
                    {
                        ProductInWorkId = c.Int(nullable: false, identity: true),
                        Description = c.String(maxLength: 128),
                        ProductId = c.Int(nullable: false),
                        WorkTypeId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ProductInWorkId)
                .ForeignKey("dbo.Products", t => t.ProductId, cascadeDelete: true)
                .ForeignKey("dbo.WorkTypes", t => t.WorkTypeId, cascadeDelete: true)
                .Index(t => t.ProductId)
                .Index(t => t.WorkTypeId);
            
            CreateTable(
                "dbo.WorkTypes",
                c => new
                    {
                        WorkTypeId = c.Int(nullable: false, identity: true),
                        Description = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.WorkTypeId);
            
            CreateTable(
                "dbo.ProductTypes",
                c => new
                    {
                        ProductTypeId = c.Int(nullable: false, identity: true),
                        Description = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.ProductTypeId);
            
            CreateTable(
                "dbo.Purchases",
                c => new
                    {
                        PurchaseId = c.Int(nullable: false, identity: true),
                        Description = c.String(maxLength: 128),
                        CreationDateTime = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        SupplierId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.PurchaseId)
                .ForeignKey("dbo.Suppliers", t => t.SupplierId, cascadeDelete: true)
                .Index(t => t.SupplierId);
            
            CreateTable(
                "dbo.Suppliers",
                c => new
                    {
                        SupplierId = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 128),
                        Rating = c.Int(),
                        Address = c.String(maxLength: 1000),
                        Country = c.String(maxLength: 128),
                        DeliverySpeed = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.SupplierId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Purchases", "SupplierId", "dbo.Suppliers");
            DropForeignKey("dbo.ProductInPurchases", "PurchaseId", "dbo.Purchases");
            DropForeignKey("dbo.Products", "ProductTypeId", "dbo.ProductTypes");
            DropForeignKey("dbo.ProductInWorks", "WorkTypeId", "dbo.WorkTypes");
            DropForeignKey("dbo.ProductInWorks", "ProductId", "dbo.Products");
            DropForeignKey("dbo.ProductInWarehouses", "WarehouseId", "dbo.Warehouses");
            DropForeignKey("dbo.ProductInWarehouses", "ProductId", "dbo.Products");
            DropForeignKey("dbo.ProductInPurchases", "ProductId", "dbo.Products");
            DropIndex("dbo.Purchases", new[] { "SupplierId" });
            DropIndex("dbo.ProductInWorks", new[] { "WorkTypeId" });
            DropIndex("dbo.ProductInWorks", new[] { "ProductId" });
            DropIndex("dbo.ProductInWarehouses", new[] { "ProductId" });
            DropIndex("dbo.ProductInWarehouses", new[] { "WarehouseId" });
            DropIndex("dbo.Products", new[] { "ProductTypeId" });
            DropIndex("dbo.ProductInPurchases", new[] { "PurchaseId" });
            DropIndex("dbo.ProductInPurchases", new[] { "ProductId" });
            DropTable("dbo.Suppliers");
            DropTable("dbo.Purchases");
            DropTable("dbo.ProductTypes");
            DropTable("dbo.WorkTypes");
            DropTable("dbo.ProductInWorks");
            DropTable("dbo.Warehouses");
            DropTable("dbo.ProductInWarehouses");
            DropTable("dbo.Products");
            DropTable("dbo.ProductInPurchases");
        }
    }
}
