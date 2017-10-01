namespace TallerIV.Datos.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class cartaDePresentacionPostulante : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Usuarios", "CartaDePresentacion", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Usuarios", "CartaDePresentacion");
        }
    }
}
