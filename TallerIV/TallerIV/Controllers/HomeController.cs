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
using System.Configuration;
using System.Data.SqlClient;
using System.Data;

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
            GenerarStoredProcedure();
            InicializarTags(db);
            InicializarUsuarios(db);
            InicializarAvisos(db);
            InicializacionParaEntrega(db);
        }

        protected static void GenerarStoredProcedure()
        {
            try
            {
                string cs = ConfigurationManager.ConnectionStrings["TallerIVContext"].ConnectionString;

                //string store = "CREATE PROCEDURE [spInsertADAuthorization] @AD_Account varchar(255),@AD_SID varchar(255),@AD_EmailAddress varchar(255),@DateImported datetime,@Active bit AS BEGIN SET NOCOUNT ON; INSERT INTO AD_Authorization (AD_Account, AD_SID, AD_EmailAddress, DateImported, Active) VALUES (@AD_Account,@AD_SID,@AD_EmailAddress,@DateImported,@Active) END";

                using (SqlConnection connection = new SqlConnection(cs))
                {
                    string store1 = "CREATE PROCEDURE [AvisoDesaprobadosPorUsuario] @avisoId INT AS BEGIN SELECT COUNT(UsuarioEmpleado_Id) as TOTAL from UsuarioEmpleadoAviso1 WHERE Aviso_Id = @avisoId END";

                    using (SqlCommand cmd = new SqlCommand(store1, connection))
                    {
                        connection.Open();
                        cmd.CommandType = CommandType.Text;
                        cmd.ExecuteNonQuery();
                        string store2 = "CREATE PROCEDURE [AvisoAprobadosPorUsuario] @avisoId INT AS BEGIN SELECT COUNT(UsuarioEmpleado_Id) as TOTAL from UsuarioEmpleadoAviso WHERE Aviso_Id = @avisoId END";
                        cmd.CommandText = store2;
                        cmd.ExecuteNonQuery();
                        string store3 = "create PROCEDURE EstadisticaPorcentajeAViso (@avisoId INT) as begin select Porcentaje, count(1) as Cantidad, maximo as MaximoAviso from(SELECT cast(CAST(SUM(ap.Prioridad) as float) / cast(Maximo as float) as decimal(10, 1)) as Porcentaje, maximo from(select SUM(ap1.Prioridad) AS maximo, ap1.Aviso_Id from AptitudesPorAviso ap1 where ap1.Aviso_Id = @avisoId GROUP BY ap1.Aviso_Id) a1 inner join AptitudesPorAviso ap ON ap.Aviso_Id = a1.Aviso_Id inner join UsuarioEmpleadoAptitud uea on ap.Aptitud_Id = uea.Aptitud_Id WHERE ap.Aviso_Id = @avisoId GROUP BY UsuarioEmpleado_Id, maximo) as temp group by temp.Porcentaje, temp.maximo order by temp.Porcentaje END";
                        cmd.CommandText = store3;
                        cmd.ExecuteNonQuery();
                        connection.Close();
                    }
                }
            } catch (SqlException e)
            {

            }
            
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
            UsuarioReclutador Reclutador = db.Users.OfType<UsuarioReclutador>().FirstOrDefault();        
            string EmpresaId = db.Users.OfType<UsuarioEmpresa>().FirstOrDefault().Id;
            string usuarioNombre = db.Users.OfType<UsuarioEmpresa>().FirstOrDefault().RazonSocial;

            

            //List<AptitudPorAviso> aptitudes = new List<AptitudPorAviso>();

            //aptitudes.Add(aptitud);

            Aviso aviso = new Aviso(
               "Este es un Aviso de .Net",
               "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Suspendisse ornare, lectus vitae rutrum blandit, tortor velit auctor ipsum, nec pretium dui lorem ac neque. Pellentesque fringilla diam vitae mi tempor elementum. Quisque cursus felis odio, eu ornare lacus malesuada ac. Maecenas rhoncus eros nec imperdiet rutrum. Curabitur id ipsum ac eros varius hendrerit non at erat. Phasellus eget massa finibus, imperdiet odio at, fringilla ex. Nulla pretium, dolor eu viverra efficitur, felis nisi lacinia erat, aliquet consectetur massa lorem viverra lectus. Etiam consectetur mi arcu, eget convallis erat egestas sed. Duis vehicula lacus sed orci rhoncus, vitae rhoncus lectus viverra. Sed in varius neque, at elementum ex.",
               DateTime.Now,
               Reclutador,
               HomeController.GenerarAptitudes(db),
               1000,
               Dominio.Usuarios.Prioridad.Normal,
               TipoRelacionDeTrabajo.Monotributo,
               Dominio.Usuarios.Prioridad.Baja,
               8,
               Dominio.Usuarios.Prioridad.Baja,
               EmpresaId,
               usuarioNombre);

            Aviso avisoOtro = new Aviso(
               "Ee .Net",
               "Ldipiscing elit. Suspendisse ornare, lectus vitae rutrum blandit, tortor velit auctor ipsum, nec pretium dui lorem ac neque. Pellentesque fringilla diam vitae mi tempor elementum. Quisque cursus felis odio, eu ornare lacus malesuada ac. Maecenas rhoncus eros nec imperdiet rutrum. Curabitur id ipsum ac eros varius hendrerit non at erat. Phasellus eget massa finibus, imperdiet odio at, fringilla ex. Nulla pretium, dolor eu viverra efficitur, felis nisi lacinia erat, aliquet consectetur massa lorem viverra lectus. Etiam consectetur mi arcu, eget convallis erat egestas sed. Duis vehicula lacus sed orci rhoncus, vitae rhoncus lectus viverra. Sed in varius neque, at elementum ex.",
               DateTime.Now,
               Reclutador,
               HomeController.GenerarAptitudes(db),
               1000,
               Dominio.Usuarios.Prioridad.Normal,
               TipoRelacionDeTrabajo.Monotributo,
               Dominio.Usuarios.Prioridad.Baja,
               8,
               Dominio.Usuarios.Prioridad.Baja,
               EmpresaId,
               usuarioNombre);

            Aviso avisoOtroOtro = new Aviso(
               "AVISO 3",
               "Ldipiscing elit. Suspendisse ornare, lectus vitae rutrum blandit, tortor velit auctor ipsum, nec pretium dui lorem ac neque. Pellentesque fringilla diam vitae mi tempor elementum. Quisque cursus felis odio, eu ornare lacus malesuada ac. Maecenas rhoncus eros nec imperdiet rutrum. Curabitur id ipsum ac eros varius hendrerit non at erat. Phasellus eget massa finibus, imperdiet odio at, fringilla ex. Nulla pretium, dolor eu viverra efficitur, felis nisi lacinia erat, aliquet consectetur massa lorem viverra lectus. Etiam consectetur mi arcu, eget convallis erat egestas sed. Duis vehicula lacus sed orci rhoncus, vitae rhoncus lectus viverra. Sed in varius neque, at elementum ex.",
               DateTime.Now,
               Reclutador,
               HomeController.GenerarAptitudes(db),
               1000,
               Dominio.Usuarios.Prioridad.Normal,
               TipoRelacionDeTrabajo.Monotributo,
               Dominio.Usuarios.Prioridad.Baja,
               8,
               Dominio.Usuarios.Prioridad.Baja,
               EmpresaId,
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
                var Empleado = new UsuarioEmpleado(DateTime.Now, "nsabaj@hotmail.com", "1150437641", "nsabaj@hotmail.com", "Nicolas", "Sabaj", new DateTime(1995, 9, 23), "Mi nombre es Nicolás.");
                userManager.Create(Empleado, "Ns12345!");
                userManager.AddToRole(Empleado.Id, "Empleado");
            }

            var userGoyano = userManager.FindByName("ngoyano@gmail.com");

            if (userGoyano == null)
            {
                var Empleado = new UsuarioEmpleado(DateTime.Now, "ngoyano@gmail.com", "1163656732", "ngoyano@gmail.com", "Nicolas", "Goyano", new DateTime(1996, 6, 29), "Mi nombre es Nicolás Goyano.");
                userManager.Create(Empleado, "Ng12345!");
                userManager.AddToRole(Empleado.Id, "Empleado");
            }

            var userBangueses = userManager.FindByName("gonzajb@gmail.com");

            if (userBangueses == null)
            {
                var Empleado = new UsuarioEmpleado(DateTime.Now, "gonzajb@gmail.com","1153258745", "gonzajb@gmail.com", "Gonzalo", "Bangueses", new DateTime(1985, 6, 20), "Mi nombre es Gonza.");
                userManager.Create(Empleado, "Gb12345!");
                userManager.AddToRole(Empleado.Id, "Empleado");
            }

            var userFraiman = userManager.FindByName("brailf@gmail.com");

            if (userBangueses == null)
            {
                var Empleado = new UsuarioEmpleado(DateTime.Now, "brailf@gmail.com", "1136456567", "brailf@gmail.com", "Brian", "Fraiman", new DateTime(1995, 5, 3), "Mi nombre es Brian.");
                userManager.Create(Empleado, "Bf12345!");
                userManager.AddToRole(Empleado.Id, "Empleado");
            }

            #endregion

            #region Empresa
            var Empresa = userManager.FindByName("laempresa1@hotmail.com");
            if (Empresa == null)
            {
                Empresa = new UsuarioEmpresa("1234321", "La Empresa 1", DateTime.Now, "laempresa1@hotmail.com", "laempresa1@hotmail.com");
                userManager.Create(Empresa, "Le12345!");
                userManager.AddToRole(Empresa.Id, "Empresa");
            }

            #endregion

            var user = userManager.FindByName("rec1@gmail.com");

            if (user == null)
            {
                var Reclutador = new UsuarioReclutador(DateTime.Now, "rec1@gmail.com", "1151235476", "rec1@gmail.com", "Rec1", "TE", DateTime.Now, Empresa.Id);
                userManager.Create(Reclutador, "Le12345!");
                userManager.AddToRole(Reclutador.Id, "Reclutador");

            }            
            //}
        }
        public static void InicializacionParaEntrega(TallerIVDbContext db)
        {
            var userStore = new UserStore<IdentityUser>(db);
            var userManager = new UserManager<IdentityUser>(userStore);
            Aptitud Apt = db.Aptitudes.FirstOrDefault();
            List<Aptitud> ListaApt = new List<Aptitud>();
            ListaApt.Add(Apt);
            if (!db.Users.Any(x => x.Email == "prueba@gmail.com"))
            {
                var Us = new UsuarioEmpleado(DateTime.Now, "prueba@gmail.com", "1151234576", "prueba@gmail.com", "Jose", "Perez", DateTime.Now, null, ListaApt);
                userManager.Create(Us, "Le12345!");
                userManager.AddToRole(Us.Id, "Empleado");
            }
            if (!db.Users.Any(x => x.Email == "softa@gmail.com"))
            {
                var Emp = new UsuarioEmpresa("30112233446", "Soft algo SA", DateTime.Now, "softa@gmail.com", "softa@gmail.com");
                userManager.Create(Emp, "Le12345!");
                userManager.AddToRole(Emp.Id, "Empresa");
            }
            if (!db.Users.Any(x => x.Email == "esteban@gmail.com"))
            {
                var Rec = new UsuarioReclutador(DateTime.Now, "esteban@gmail.com", "1151234576", "esteban@gmail.com", "Esteban", "Gonzalez", DateTime.Now, db.Users.First(x => x.Email == "softa@gmail.com").Id);
                userManager.Create(Rec, "Le12345!");
                userManager.AddToRole(Rec.Id, "Reclutador");
            }
            if (!db.Aptitudes.Any(x => x.Titulo == "C++"))
            {
                db.Aptitudes.Add(new Aptitud { Titulo = "C++" });
            }
            if (!db.Aptitudes.Any(x => x.Titulo == "PHP"))
            {
                db.Aptitudes.Add(new Aptitud { Titulo = "PHP" });
            }
            if (!db.Aptitudes.Any(x => x.Titulo == "RUBY"))
            {
                db.Aptitudes.Add(new Aptitud { Titulo = "RUBY" });
            }
            //UsuarioReclutador Reclutador2 = db.Users.OfType<UsuarioReclutador>().First(x => x.Email == "esteban@gmail.com");
            //Aptitud AptitudNueva= db.Aptitudes.First(x => x.Titulo == "RUBY";

            db.SaveChanges();
        }
    }
}