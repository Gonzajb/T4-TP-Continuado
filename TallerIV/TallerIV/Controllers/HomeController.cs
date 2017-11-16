using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TallerIV.Datos;
using TallerIV.Dominio;
using TallerIV.Dominio.Avisos;

namespace TallerIV.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public void GenerarEntidades() {
            var db = new TallerIVDbContext();
            InicializarTags(db);
            InicializarUsuarios(db);
            InicializarAvisos(db);
        }

        protected static List<AptitudPorAviso> GenerarAptitudes(TallerIVDbContext db)
        {
            AptitudPorAviso aptitud = new AptitudPorAviso(
                2,
                db.Aptitudes.FirstOrDefault().Id,
                db.Aptitudes.FirstOrDefault(),
                Dominio.Usuarios.Prioridad.Baja
            );
            var lista = new List<AptitudPorAviso>();
            lista.Add(aptitud);
            return lista ;
        }

        public static void InicializarAvisos(TallerIVDbContext db)
        {
            UsuarioReclutador usuarioReclutador = db.Users.OfType<UsuarioReclutador>().FirstOrDefault();
            string usuarioEmpresaId = db.Users.OfType<UsuarioEmpresa>().FirstOrDefault().Id;
            string usuarioNombre = db.Users.OfType<UsuarioEmpresa>().FirstOrDefault().RazonSocial;

            

            //List<AptitudPorAviso> aptitudes = new List<AptitudPorAviso>();

            //aptitudes.Add(aptitud);

            Aviso aviso = new Aviso(
               "Este es un Aviso de .Net",
               "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Suspendisse ornare, lectus vitae rutrum blandit, tortor velit auctor ipsum, nec pretium dui lorem ac neque. Pellentesque fringilla diam vitae mi tempor elementum. Quisque cursus felis odio, eu ornare lacus malesuada ac. Maecenas rhoncus eros nec imperdiet rutrum. Curabitur id ipsum ac eros varius hendrerit non at erat. Phasellus eget massa finibus, imperdiet odio at, fringilla ex. Nulla pretium, dolor eu viverra efficitur, felis nisi lacinia erat, aliquet consectetur massa lorem viverra lectus. Etiam consectetur mi arcu, eget convallis erat egestas sed. Duis vehicula lacus sed orci rhoncus, vitae rhoncus lectus viverra. Sed in varius neque, at elementum ex.",
               DateTime.Now,
               usuarioReclutador,
               HomeController.GenerarAptitudes(db),
               TipoRelacionDeTrabajo.Monotributo,
               Dominio.Usuarios.Prioridad.Baja,
               8,
               Dominio.Usuarios.Prioridad.Baja,
               usuarioEmpresaId,
               usuarioNombre);

            Aviso avisoOtro = new Aviso(
               "Ee .Net",
               "Ldipiscing elit. Suspendisse ornare, lectus vitae rutrum blandit, tortor velit auctor ipsum, nec pretium dui lorem ac neque. Pellentesque fringilla diam vitae mi tempor elementum. Quisque cursus felis odio, eu ornare lacus malesuada ac. Maecenas rhoncus eros nec imperdiet rutrum. Curabitur id ipsum ac eros varius hendrerit non at erat. Phasellus eget massa finibus, imperdiet odio at, fringilla ex. Nulla pretium, dolor eu viverra efficitur, felis nisi lacinia erat, aliquet consectetur massa lorem viverra lectus. Etiam consectetur mi arcu, eget convallis erat egestas sed. Duis vehicula lacus sed orci rhoncus, vitae rhoncus lectus viverra. Sed in varius neque, at elementum ex.",
               DateTime.Now,
               usuarioReclutador,
               HomeController.GenerarAptitudes(db),
               TipoRelacionDeTrabajo.Monotributo,
               Dominio.Usuarios.Prioridad.Baja,
               8,
               Dominio.Usuarios.Prioridad.Baja,
               usuarioEmpresaId,
               usuarioNombre);

            Aviso avisoOtroOtro = new Aviso(
               "AVISO 3",
               "Ldipiscing elit. Suspendisse ornare, lectus vitae rutrum blandit, tortor velit auctor ipsum, nec pretium dui lorem ac neque. Pellentesque fringilla diam vitae mi tempor elementum. Quisque cursus felis odio, eu ornare lacus malesuada ac. Maecenas rhoncus eros nec imperdiet rutrum. Curabitur id ipsum ac eros varius hendrerit non at erat. Phasellus eget massa finibus, imperdiet odio at, fringilla ex. Nulla pretium, dolor eu viverra efficitur, felis nisi lacinia erat, aliquet consectetur massa lorem viverra lectus. Etiam consectetur mi arcu, eget convallis erat egestas sed. Duis vehicula lacus sed orci rhoncus, vitae rhoncus lectus viverra. Sed in varius neque, at elementum ex.",
               DateTime.Now,
               usuarioReclutador,
               HomeController.GenerarAptitudes(db),
               TipoRelacionDeTrabajo.Monotributo,
               Dominio.Usuarios.Prioridad.Baja,
               8,
               Dominio.Usuarios.Prioridad.Baja,
               usuarioEmpresaId,
               usuarioNombre);

            if (!db.Avisos.Any(X => X.Titulo == aviso.Titulo))
            {

                db.Avisos.Add(aviso);
            }

            if (!db.Avisos.Any(X => X.Titulo == avisoOtro.Titulo))
            {

                db.Avisos.Add(avisoOtro);
            }

            if (!db.Avisos.Any(X => X.Titulo == avisoOtroOtro.Titulo))
            {

                db.Avisos.Add(avisoOtroOtro);
            }
            db.SaveChanges();
        }
        public static void InicializarTags(TallerIVDbContext db)
        {
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

            #region Usuarios Empleados
            var userSabaj = userManager.FindByName("nsabaj@hotmail.com");

            if (userSabaj == null)
            {
                var usuarioEmpleado = new UsuarioEmpleado(DateTime.Now, "nsabaj@hotmail.com", "nsabaj@hotmail.com", "Nicolas", "Sabaj", new DateTime(1995, 9, 23), "Mi nombre es Nicolás.");
                userManager.Create(usuarioEmpleado, "Ns12345!");
                userManager.AddToRole(usuarioEmpleado.Id, "Empleado");
            }

            var userGoyano = userManager.FindByName("ngoyano@gmail.com");

            if (userGoyano == null)
            {
                var usuarioEmpleado = new UsuarioEmpleado(DateTime.Now, "ngoyano@gmail.com", "ngoyano@gmail.com", "Nicolas", "Goyano", new DateTime(1996, 6, 29), "Mi nombre es Nicolás Goyano.");
                userManager.Create(usuarioEmpleado, "Ng12345!");
                userManager.AddToRole(usuarioEmpleado.Id, "Empleado");
            }

            var userBangueses = userManager.FindByName("gonzajb@gmail.com");

            if (userBangueses == null)
            {
                var usuarioEmpleado = new UsuarioEmpleado(DateTime.Now, "gonzajb@gmail.com", "gonzajb@gmail.com", "Gonzalo", "Bangueses", new DateTime(1985, 6, 20), "Mi nombre es Gonza.");
                userManager.Create(usuarioEmpleado, "Gb12345!");
                userManager.AddToRole(usuarioEmpleado.Id, "Empleado");
            }

            var userFraiman = userManager.FindByName("brailf@gmail.com");

            if (userBangueses == null)
            {
                var usuarioEmpleado = new UsuarioEmpleado(DateTime.Now, "brailf@gmail.com", "brailf@gmail.com", "Brian", "Fraiman", new DateTime(1995, 5, 3), "Mi nombre es Brian.");
                userManager.Create(usuarioEmpleado, "Bf12345!");
                userManager.AddToRole(usuarioEmpleado.Id, "Empleado");
            }

            #endregion

            #region Empresa
            var usuarioEmpresa = userManager.FindByName("laempresa1@hotmail.com");
            if (usuarioEmpresa == null)
            {
                usuarioEmpresa = new UsuarioEmpresa("1234321", "La Empresa 1", DateTime.Now, "laempresa1@hotmail.com", "laempresa1@hotmail.com");
                userManager.Create(usuarioEmpresa, "Le12345!");
                userManager.AddToRole(usuarioEmpresa.Id, "Empresa");
            }

            #endregion

            var user = userManager.FindByName("rec1@gmail.com");

            if (user == null)
            {
                var usuarioReclutador = new UsuarioReclutador(DateTime.Now, "rec1@gmail.com", "rec1@gmail.com", "Rec1", "TE", DateTime.Now, usuarioEmpresa.Id);
                userManager.Create(usuarioReclutador, "Le12345!");
                userManager.AddToRole(usuarioReclutador.Id, "Reclutador");

            }

            //}
        }
    }
}