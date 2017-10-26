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
            InicializarTags(db);
            InicializarUsuarios(db);
            InicializarAvisos(db);
            base.Seed(db);
        }
        public static void InicializarAvisos(TallerIVDbContext db) {
            UsuarioReclutador usuarioReclutador = db.Users.OfType<UsuarioReclutador>().FirstOrDefault();
            UsuarioEmpresa usuarioEmpresa = db.Users.OfType<UsuarioEmpresa>().FirstOrDefault();

            Aviso aviso1 = new Aviso(
                "primer aviso",
                "es tu primer trabajo",
                DateTime.Now,
                usuarioReclutador, null,
                TipoRelacionDeTrabajo.Dependencia,
                Dominio.Usuarios.Prioridad.Baja,
                8,
                Dominio.Usuarios.Prioridad.Baja,
                usuarioEmpresa.Id);


            Aviso aviso2 = new Aviso(
               "segundo aviso",
               "es tu primer trabajo",
               DateTime.Now,
               usuarioReclutador, null,
               TipoRelacionDeTrabajo.Dependencia,
               Dominio.Usuarios.Prioridad.Baja,
               8,
               Dominio.Usuarios.Prioridad.Baja,
               usuarioEmpresa.Id);

            if (!db.Avisos.Any(X=> X.Titulo == "primer aviso"))
            {
                db.Avisos.Add(aviso1);
            }
            if (!db.Avisos.Any(X => X.Titulo == "segundo aviso"))
            {
                db.Avisos.Add(aviso2);
            }

        }
        public static void InicializarTags(TallerIVDbContext db) {
            if (!db.Aptitudes.Any(x => x.Titulo == "ASP.NET"))
            {
                db.Aptitudes.Add(new Aptitud { Titulo = "ASP.NET" });
            }
            if (!db.Aptitudes.Any(x => x.Titulo == "SQL SERVER"))
            {
                db.Aptitudes.Add(new Aptitud { Titulo = "SQL SERVER" });
            }
            if (!db.Aptitudes.Any(x => x.Titulo == "JAVA"))
            {
                db.Aptitudes.Add(new Aptitud { Titulo = "JAVA" });
            }
            db.SaveChanges();
        }
        public static void InicializarUsuarios(TallerIVDbContext db)
        {

            //if (!db.Users.Any())
            //{
                var roleStore = new RoleStore<IdentityRole>(db);
                var roleManager = new RoleManager<IdentityRole>(roleStore);
                var userStore = new UserStore<IdentityUser>(db);
                var userManager = new UserManager<IdentityUser>(userStore);

                // Add missing roles
                var role = roleManager.FindByName("Admin");
                if (role == null)
                {
                    role = new IdentityRole("Admin");
                    roleManager.Create(role);
                }
                role = roleManager.FindByName("Empleado");
                if (role == null)
                {
                    role = new IdentityRole("Empleado");
                    roleManager.Create(role);
                }
                role = roleManager.FindByName("Empresa");
                if (role == null)
                {
                    role = new IdentityRole("Empresa");
                    roleManager.Create(role);
                }
            role = roleManager.FindByName("Reclutador");
            if (role == null)
            {
                role = new IdentityRole("Reclutador");
                roleManager.Create(role);
            }

                // Create test users
                var user = userManager.FindByName("nsabaj@hotmail.com");
                if (user == null)
                {
                    var usuarioEmpleado = new UsuarioEmpleado(DateTime.Now, "nsabaj@hotmail.com", "nsabaj@hotmail.com", "Nicolas", "Sabaj", new DateTime(1995, 9, 23), "Mi nombre es Nicolás.");
                    //var result = await UserManager.CreateAsync(user, model.Password);
                    userManager.Create(usuarioEmpleado, "Ns12345!");
                    //userManager.SetLockoutEnabled(usuarioEmpleado.Id, false);
                    userManager.AddToRole(usuarioEmpleado.Id, "Empleado");
                }

                var usuarioEmpresa = userManager.FindByName("laempresa1");
                if (usuarioEmpresa == null)
                {
                    usuarioEmpresa = new UsuarioEmpresa("1234321", "La Empresa 1", DateTime.Now, "laempresa1@hotmail.com", "laempresa1@hotmail.com");
                    userManager.Create(usuarioEmpresa, "Le12345!");
                    //userManager.SetLockoutEnabled(usuarioEmpresa.Id, false);
                    userManager.AddToRole(usuarioEmpresa.Id, "Empresa");
                }
                user = userManager.FindByName("Relutador1");
                if (user == null)
                {
                    var usuarioReclutador = new UsuarioReclutador(DateTime.Now, "rec1@gmail.com", "rec1@gmail.com", "Rec1", "TE", DateTime.Now,usuarioEmpresa.Id);
                    userManager.Create(usuarioReclutador, "Le12345!");
                    userManager.AddToRole(usuarioReclutador.Id, "Reclutador");

                }

            //}
        }
    }
}
