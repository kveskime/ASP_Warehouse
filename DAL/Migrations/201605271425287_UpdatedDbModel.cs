namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdatedDbModel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ProductInPurchases", "CreatedAtDT", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
            AddColumn("dbo.ProductInPurchases", "CreatedBy", c => c.String(maxLength: 256));
            AddColumn("dbo.ProductInPurchases", "ModifiedAtDT", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
            AddColumn("dbo.ProductInPurchases", "ModifiedBy", c => c.String(maxLength: 256));
            AddColumn("dbo.Products", "CreatedAtDT", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
            AddColumn("dbo.Products", "CreatedBy", c => c.String(maxLength: 256));
            AddColumn("dbo.Products", "ModifiedAtDT", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
            AddColumn("dbo.Products", "ModifiedBy", c => c.String(maxLength: 256));
            AddColumn("dbo.ProductInWarehouses", "CreatedAtDT", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
            AddColumn("dbo.ProductInWarehouses", "CreatedBy", c => c.String(maxLength: 256));
            AddColumn("dbo.ProductInWarehouses", "ModifiedAtDT", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
            AddColumn("dbo.ProductInWarehouses", "ModifiedBy", c => c.String(maxLength: 256));
            AddColumn("dbo.Warehouses", "CreatedAtDT", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
            AddColumn("dbo.Warehouses", "CreatedBy", c => c.String(maxLength: 256));
            AddColumn("dbo.Warehouses", "ModifiedAtDT", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
            AddColumn("dbo.Warehouses", "ModifiedBy", c => c.String(maxLength: 256));
            AddColumn("dbo.ProductInWorks", "CreatedAtDT", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
            AddColumn("dbo.ProductInWorks", "CreatedBy", c => c.String(maxLength: 256));
            AddColumn("dbo.ProductInWorks", "ModifiedAtDT", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
            AddColumn("dbo.ProductInWorks", "ModifiedBy", c => c.String(maxLength: 256));
            AddColumn("dbo.WorkTypes", "CreatedAtDT", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
            AddColumn("dbo.WorkTypes", "CreatedBy", c => c.String(maxLength: 256));
            AddColumn("dbo.WorkTypes", "ModifiedAtDT", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
            AddColumn("dbo.WorkTypes", "ModifiedBy", c => c.String(maxLength: 256));
            AddColumn("dbo.ProductTypes", "CreatedAtDT", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
            AddColumn("dbo.ProductTypes", "CreatedBy", c => c.String(maxLength: 256));
            AddColumn("dbo.ProductTypes", "ModifiedAtDT", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
            AddColumn("dbo.ProductTypes", "ModifiedBy", c => c.String(maxLength: 256));
            AddColumn("dbo.Purchases", "CreatedAtDT", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
            AddColumn("dbo.Purchases", "CreatedBy", c => c.String(maxLength: 256));
            AddColumn("dbo.Purchases", "ModifiedAtDT", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
            AddColumn("dbo.Purchases", "ModifiedBy", c => c.String(maxLength: 256));
            AddColumn("dbo.Suppliers", "CreatedAtDT", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
            AddColumn("dbo.Suppliers", "CreatedBy", c => c.String(maxLength: 256));
            AddColumn("dbo.Suppliers", "ModifiedAtDT", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
            AddColumn("dbo.Suppliers", "ModifiedBy", c => c.String(maxLength: 256));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Suppliers", "ModifiedBy");
            DropColumn("dbo.Suppliers", "ModifiedAtDT");
            DropColumn("dbo.Suppliers", "CreatedBy");
            DropColumn("dbo.Suppliers", "CreatedAtDT");
            DropColumn("dbo.Purchases", "ModifiedBy");
            DropColumn("dbo.Purchases", "ModifiedAtDT");
            DropColumn("dbo.Purchases", "CreatedBy");
            DropColumn("dbo.Purchases", "CreatedAtDT");
            DropColumn("dbo.ProductTypes", "ModifiedBy");
            DropColumn("dbo.ProductTypes", "ModifiedAtDT");
            DropColumn("dbo.ProductTypes", "CreatedBy");
            DropColumn("dbo.ProductTypes", "CreatedAtDT");
            DropColumn("dbo.WorkTypes", "ModifiedBy");
            DropColumn("dbo.WorkTypes", "ModifiedAtDT");
            DropColumn("dbo.WorkTypes", "CreatedBy");
            DropColumn("dbo.WorkTypes", "CreatedAtDT");
            DropColumn("dbo.ProductInWorks", "ModifiedBy");
            DropColumn("dbo.ProductInWorks", "ModifiedAtDT");
            DropColumn("dbo.ProductInWorks", "CreatedBy");
            DropColumn("dbo.ProductInWorks", "CreatedAtDT");
            DropColumn("dbo.Warehouses", "ModifiedBy");
            DropColumn("dbo.Warehouses", "ModifiedAtDT");
            DropColumn("dbo.Warehouses", "CreatedBy");
            DropColumn("dbo.Warehouses", "CreatedAtDT");
            DropColumn("dbo.ProductInWarehouses", "ModifiedBy");
            DropColumn("dbo.ProductInWarehouses", "ModifiedAtDT");
            DropColumn("dbo.ProductInWarehouses", "CreatedBy");
            DropColumn("dbo.ProductInWarehouses", "CreatedAtDT");
            DropColumn("dbo.Products", "ModifiedBy");
            DropColumn("dbo.Products", "ModifiedAtDT");
            DropColumn("dbo.Products", "CreatedBy");
            DropColumn("dbo.Products", "CreatedAtDT");
            DropColumn("dbo.ProductInPurchases", "ModifiedBy");
            DropColumn("dbo.ProductInPurchases", "ModifiedAtDT");
            DropColumn("dbo.ProductInPurchases", "CreatedBy");
            DropColumn("dbo.ProductInPurchases", "CreatedAtDT");
        }
    }
}
