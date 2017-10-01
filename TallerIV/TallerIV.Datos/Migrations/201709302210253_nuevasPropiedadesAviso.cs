namespace TallerIV.Datos.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class nuevasPropiedadesAviso : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Avisos", "SueldoOfrecido", c => c.Single());
            AddColumn("dbo.Avisos", "SueldoOfrecidoPrioridad", c => c.Int(nullable: false));
            AddColumn("dbo.Avisos", "TipoRelacionDeTrabajo", c => c.Int(nullable: false));
            AddColumn("dbo.Avisos", "TipoRelacionDeTrabajoPrioridad", c => c.Int(nullable: false));
            AddColumn("dbo.Avisos", "HorasTrabajo", c => c.Int(nullable: false));
            AddColumn("dbo.Avisos", "HorasTrabajoPrioridad", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Avisos", "HorasTrabajoPrioridad");
            DropColumn("dbo.Avisos", "HorasTrabajo");
            DropColumn("dbo.Avisos", "TipoRelacionDeTrabajoPrioridad");
            DropColumn("dbo.Avisos", "TipoRelacionDeTrabajo");
            DropColumn("dbo.Avisos", "SueldoOfrecidoPrioridad");
            DropColumn("dbo.Avisos", "SueldoOfrecido");
        }
    }
}
