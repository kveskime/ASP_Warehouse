namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedIdentitySupport : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.RoleInts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.UserRoleInts",
                c => new
                    {
                        UserId = c.Int(nullable: false),
                        RoleId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.RoleInts", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.UserInts", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.UserInts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(precision: 7, storeType: "datetime2"),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                        FirstName = c.String(maxLength: 128),
                        LastName = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.UserClaimInts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.UserInts", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.UserLoginInts",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.UserInts", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserRoleInts", "UserId", "dbo.UserInts");
            DropForeignKey("dbo.UserLoginInts", "UserId", "dbo.UserInts");
            DropForeignKey("dbo.UserClaimInts", "UserId", "dbo.UserInts");
            DropForeignKey("dbo.UserRoleInts", "RoleId", "dbo.RoleInts");
            DropIndex("dbo.UserLoginInts", new[] { "UserId" });
            DropIndex("dbo.UserClaimInts", new[] { "UserId" });
            DropIndex("dbo.UserInts", "UserNameIndex");
            DropIndex("dbo.UserRoleInts", new[] { "RoleId" });
            DropIndex("dbo.UserRoleInts", new[] { "UserId" });
            DropIndex("dbo.RoleInts", "RoleNameIndex");
            DropTable("dbo.UserLoginInts");
            DropTable("dbo.UserClaimInts");
            DropTable("dbo.UserInts");
            DropTable("dbo.UserRoleInts");
            DropTable("dbo.RoleInts");
        }
    }
}
