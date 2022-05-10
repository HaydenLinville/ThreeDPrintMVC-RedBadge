namespace Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Material",
                c => new
                    {
                        MaterialId = c.Int(nullable: false, identity: true),
                        UserId = c.Guid(nullable: false),
                        MaterialBrand = c.String(nullable: false),
                        MaterialType = c.Int(nullable: false),
                        Color = c.String(nullable: false),
                        Image = c.Binary(),
                    })
                .PrimaryKey(t => t.MaterialId);
            
            CreateTable(
                "dbo.Printer",
                c => new
                    {
                        PrinterId = c.Int(nullable: false, identity: true),
                        UserId = c.Guid(nullable: false),
                        PrinterBrand = c.String(nullable: false),
                        PrinterModel = c.String(nullable: false),
                        HasDualExtruder = c.Boolean(nullable: false),
                        CanAutoLevel = c.Boolean(nullable: false),
                        HasHeatedBed = c.Boolean(nullable: false),
                        HasWifi = c.Boolean(nullable: false),
                        CanUpgrade = c.Boolean(nullable: false),
                        HasCamera = c.Boolean(nullable: false),
                        Image = c.Binary(),
                    })
                .PrimaryKey(t => t.PrinterId);
            
            CreateTable(
                "dbo.Setting",
                c => new
                    {
                        SettingId = c.Int(nullable: false, identity: true),
                        UserId = c.Guid(nullable: false),
                        CustomSettingName = c.String(nullable: false),
                        MaterialId = c.Int(nullable: false),
                        PrinterId = c.Int(nullable: false),
                        MaterialTemp = c.Double(nullable: false),
                        BedTemp = c.Double(nullable: false),
                        Speed = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.SettingId)
                .ForeignKey("dbo.Material", t => t.MaterialId, cascadeDelete: true)
                .ForeignKey("dbo.Printer", t => t.PrinterId, cascadeDelete: true)
                .Index(t => t.MaterialId)
                .Index(t => t.PrinterId);
            
            CreateTable(
                "dbo.IdentityRole",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.IdentityUserRole",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(),
                        IdentityRole_Id = c.String(maxLength: 128),
                        ApplicationUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.UserId)
                .ForeignKey("dbo.IdentityRole", t => t.IdentityRole_Id)
                .ForeignKey("dbo.ApplicationUser", t => t.ApplicationUser_Id)
                .Index(t => t.IdentityRole_Id)
                .Index(t => t.ApplicationUser_Id);
            
            CreateTable(
                "dbo.ApplicationUser",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Email = c.String(),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.IdentityUserClaim",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                        ApplicationUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ApplicationUser", t => t.ApplicationUser_Id)
                .Index(t => t.ApplicationUser_Id);
            
            CreateTable(
                "dbo.IdentityUserLogin",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        LoginProvider = c.String(),
                        ProviderKey = c.String(),
                        ApplicationUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.UserId)
                .ForeignKey("dbo.ApplicationUser", t => t.ApplicationUser_Id)
                .Index(t => t.ApplicationUser_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.IdentityUserRole", "ApplicationUser_Id", "dbo.ApplicationUser");
            DropForeignKey("dbo.IdentityUserLogin", "ApplicationUser_Id", "dbo.ApplicationUser");
            DropForeignKey("dbo.IdentityUserClaim", "ApplicationUser_Id", "dbo.ApplicationUser");
            DropForeignKey("dbo.IdentityUserRole", "IdentityRole_Id", "dbo.IdentityRole");
            DropForeignKey("dbo.Setting", "PrinterId", "dbo.Printer");
            DropForeignKey("dbo.Setting", "MaterialId", "dbo.Material");
            DropIndex("dbo.IdentityUserLogin", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.IdentityUserClaim", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.IdentityUserRole", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.IdentityUserRole", new[] { "IdentityRole_Id" });
            DropIndex("dbo.Setting", new[] { "PrinterId" });
            DropIndex("dbo.Setting", new[] { "MaterialId" });
            DropTable("dbo.IdentityUserLogin");
            DropTable("dbo.IdentityUserClaim");
            DropTable("dbo.ApplicationUser");
            DropTable("dbo.IdentityUserRole");
            DropTable("dbo.IdentityRole");
            DropTable("dbo.Setting");
            DropTable("dbo.Printer");
            DropTable("dbo.Material");
        }
    }
}
