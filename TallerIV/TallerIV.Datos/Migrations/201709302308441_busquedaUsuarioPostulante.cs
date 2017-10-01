namespace TallerIV.Datos.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class busquedaUsuarioPostulante : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.BusquedaUsuarioPostulante",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        SueldoMinimo = c.Double(nullable: false),
                        SueldoMinimoPrioridad = c.Int(nullable: false),
                        TipoRelacionDeTrabajo = c.Int(nullable: false),
                        TipoRelacionDeTrabajoPrioridad = c.Int(nullable: false),
                        HorasTrabajo = c.Int(nullable: false),
                        HorasTrabajoPrioridad = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Usuarios", "Busqueda_Id", c => c.Long());
            CreateIndex("dbo.Usuarios", "Busqueda_Id");
            AddForeignKey("dbo.Usuarios", "Busqueda_Id", "dbo.BusquedaUsuarioPostulante", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Usuarios", "Busqueda_Id", "dbo.BusquedaUsuarioPostulante");
            DropIndex("dbo.Usuarios", new[] { "Busqueda_Id" });
            DropColumn("dbo.Usuarios", "Busqueda_Id");
            DropTable("dbo.BusquedaUsuarioPostulante");
        }
    }
}
