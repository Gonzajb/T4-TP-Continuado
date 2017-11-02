namespace TallerIV.Datos.Migrations
{
    using Dominio;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<TallerIV.Datos.TallerIVDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
        }
        //var user = new UsuarioEmpleado(DateTime.Now, "nsabaj@hotmail.com", "nsabaj", "Nicolas", "Sabaj", new DateTime(1995, 9, 23));

        protected override void Seed(TallerIVDbContext db)
        {
            base.Seed(db);
        }
    }
}
