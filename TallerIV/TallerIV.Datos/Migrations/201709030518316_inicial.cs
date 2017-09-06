namespace TallerIV.Datos.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class inicial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Avisos",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Titulo = c.String(),
                        Descripcion = c.String(),
                        FechaInicio = c.DateTime(nullable: false),
                        FechaFin = c.DateTime(nullable: false),
                        UsuarioReclutador_Id = c.String(nullable: false, maxLength: 128),
                        UsuarioEmpresa_Id = c.String(nullable: false, maxLength: 128),
                        UsuarioReclutadorAsignado_Id = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.IdentityUser", t => t.UsuarioReclutador_Id, cascadeDelete: true)
                .ForeignKey("dbo.Usuarios", t => t.UsuarioEmpresa_Id)
                .Index(t => t.UsuarioReclutador_Id)
                .Index(t => t.UsuarioEmpresa_Id);
            
            CreateTable(
                "dbo.Tag",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Titulo = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.IdentityUser",
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
                        FechaRegistro = c.DateTime(),
                        Nombre = c.String(),
                        Apellido = c.String(),
                        FechaDeNacimiento = c.DateTime(),
                        Edad = c.Int(),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                        UsuarioEmpresa_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Usuarios", t => t.UsuarioEmpresa_Id)
                .Index(t => t.UsuarioEmpresa_Id);
            
            CreateTable(
                "dbo.IdentityUserClaim",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                        IdentityUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.IdentityUser", t => t.IdentityUser_Id)
                .Index(t => t.IdentityUser_Id);
            
            CreateTable(
                "dbo.IdentityUserLogin",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        LoginProvider = c.String(),
                        ProviderKey = c.String(),
                        IdentityUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.UserId)
                .ForeignKey("dbo.IdentityUser", t => t.IdentityUser_Id)
                .Index(t => t.IdentityUser_Id);
            
            CreateTable(
                "dbo.IdentityUserRole",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                        IdentityRole_Id = c.String(maxLength: 128),
                        IdentityUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.IdentityRole", t => t.IdentityRole_Id)
                .ForeignKey("dbo.IdentityUser", t => t.IdentityUser_Id)
                .Index(t => t.IdentityRole_Id)
                .Index(t => t.IdentityUser_Id);
            
            CreateTable(
                "dbo.Encuentros",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UsuarioReclutador_Id = c.String(nullable: false, maxLength: 128),
                        UsuarioEmpleado_Id = c.String(),
                        UsuarioEmpleado_Id1 = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Usuarios", t => t.UsuarioEmpleado_Id1)
                .ForeignKey("dbo.IdentityUser", t => t.UsuarioReclutador_Id, cascadeDelete: true)
                .Index(t => t.UsuarioReclutador_Id)
                .Index(t => t.UsuarioEmpleado_Id1);
            
            CreateTable(
                "dbo.Likes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        LikeEmpleado = c.Boolean(nullable: false),
                        LikeReclutador = c.Boolean(nullable: false),
                        UsuarioReclutador_Id = c.String(nullable: false, maxLength: 128),
                        UsuarioEmpleado_Id = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Usuarios", t => t.UsuarioEmpleado_Id)
                .ForeignKey("dbo.IdentityUser", t => t.UsuarioReclutador_Id, cascadeDelete: true)
                .Index(t => t.UsuarioReclutador_Id)
                .Index(t => t.UsuarioEmpleado_Id);
            
            CreateTable(
                "dbo.IdentityRole",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AvisoTag",
                c => new
                    {
                        Aviso_Id = c.Int(nullable: false),
                        Tag_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Aviso_Id, t.Tag_Id })
                .ForeignKey("dbo.Avisos", t => t.Aviso_Id, cascadeDelete: true)
                .ForeignKey("dbo.Tag", t => t.Tag_Id, cascadeDelete: true)
                .Index(t => t.Aviso_Id)
                .Index(t => t.Tag_Id);
            
            CreateTable(
                "dbo.UsuarioEmpleadoTag",
                c => new
                    {
                        UsuarioEmpleado_Id = c.String(nullable: false, maxLength: 128),
                        Tag_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.UsuarioEmpleado_Id, t.Tag_Id })
                .ForeignKey("dbo.Usuarios", t => t.UsuarioEmpleado_Id, cascadeDelete: true)
                .ForeignKey("dbo.Tag", t => t.Tag_Id, cascadeDelete: true)
                .Index(t => t.UsuarioEmpleado_Id)
                .Index(t => t.Tag_Id);
            
            CreateTable(
                "dbo.Usuarios",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Nombre = c.String(),
                        Apellido = c.String(),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                        Cuit = c.String(),
                        RazonSocial = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.IdentityUser", t => t.Id)
                .Index(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Usuarios", "Id", "dbo.IdentityUser");
            DropForeignKey("dbo.IdentityUserRole", "IdentityUser_Id", "dbo.IdentityUser");
            DropForeignKey("dbo.IdentityUserLogin", "IdentityUser_Id", "dbo.IdentityUser");
            DropForeignKey("dbo.IdentityUserClaim", "IdentityUser_Id", "dbo.IdentityUser");
            DropForeignKey("dbo.IdentityUser", "UsuarioEmpresa_Id", "dbo.Usuarios");
            DropForeignKey("dbo.Avisos", "UsuarioEmpresa_Id", "dbo.Usuarios");
            DropForeignKey("dbo.IdentityUserRole", "IdentityRole_Id", "dbo.IdentityRole");
            DropForeignKey("dbo.Likes", "UsuarioReclutador_Id", "dbo.IdentityUser");
            DropForeignKey("dbo.Likes", "UsuarioEmpleado_Id", "dbo.Usuarios");
            DropForeignKey("dbo.Encuentros", "UsuarioReclutador_Id", "dbo.IdentityUser");
            DropForeignKey("dbo.Encuentros", "UsuarioEmpleado_Id1", "dbo.Usuarios");
            DropForeignKey("dbo.UsuarioEmpleadoTag", "Tag_Id", "dbo.Tag");
            DropForeignKey("dbo.UsuarioEmpleadoTag", "UsuarioEmpleado_Id", "dbo.Usuarios");
            DropForeignKey("dbo.Avisos", "UsuarioReclutador_Id", "dbo.IdentityUser");
            DropForeignKey("dbo.AvisoTag", "Tag_Id", "dbo.Tag");
            DropForeignKey("dbo.AvisoTag", "Aviso_Id", "dbo.Avisos");
            DropIndex("dbo.Usuarios", new[] { "Id" });
            DropIndex("dbo.UsuarioEmpleadoTag", new[] { "Tag_Id" });
            DropIndex("dbo.UsuarioEmpleadoTag", new[] { "UsuarioEmpleado_Id" });
            DropIndex("dbo.AvisoTag", new[] { "Tag_Id" });
            DropIndex("dbo.AvisoTag", new[] { "Aviso_Id" });
            DropIndex("dbo.Likes", new[] { "UsuarioEmpleado_Id" });
            DropIndex("dbo.Likes", new[] { "UsuarioReclutador_Id" });
            DropIndex("dbo.Encuentros", new[] { "UsuarioEmpleado_Id1" });
            DropIndex("dbo.Encuentros", new[] { "UsuarioReclutador_Id" });
            DropIndex("dbo.IdentityUserRole", new[] { "IdentityUser_Id" });
            DropIndex("dbo.IdentityUserRole", new[] { "IdentityRole_Id" });
            DropIndex("dbo.IdentityUserLogin", new[] { "IdentityUser_Id" });
            DropIndex("dbo.IdentityUserClaim", new[] { "IdentityUser_Id" });
            DropIndex("dbo.IdentityUser", new[] { "UsuarioEmpresa_Id" });
            DropIndex("dbo.Avisos", new[] { "UsuarioEmpresa_Id" });
            DropIndex("dbo.Avisos", new[] { "UsuarioReclutador_Id" });
            DropTable("dbo.Usuarios");
            DropTable("dbo.UsuarioEmpleadoTag");
            DropTable("dbo.AvisoTag");
            DropTable("dbo.IdentityRole");
            DropTable("dbo.Likes");
            DropTable("dbo.Encuentros");
            DropTable("dbo.IdentityUserRole");
            DropTable("dbo.IdentityUserLogin");
            DropTable("dbo.IdentityUserClaim");
            DropTable("dbo.IdentityUser");
            DropTable("dbo.Tag");
            DropTable("dbo.Avisos");
        }
    }
}
