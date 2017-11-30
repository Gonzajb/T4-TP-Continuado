using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TallerIV.Datos;
using TallerIV.Dominio;
using TallerIV.Negocio.Servicios;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using TallerIV.Dominio.Coincidencias;
using TallerIV.Dominio.Usuarios;
using System.Data.SqlClient;
using System.Configuration;

namespace TallerIV.Controllers
{
    [Authorize(Roles = "Empresa,Reclutador")]
    public class AvisoController : Controller
    {
        private AvisosService avisoService;
        private UsuarioReclutadorService reclutadorService;
        public AvisoController()
        {
            avisoService = new AvisosService();
            reclutadorService = new UsuarioReclutadorService();
        }

        // GET: Aviso
        public ActionResult Index()
        {
            List<Aviso> avisos;
            string id = this.User.Identity.GetUserId();

            if (User.IsInRole("Empresa"))
            {
                avisos = this.avisoService.GetAllByEmpresa(id, false).ToList();
                ViewBag.UsuarioReclutador = reclutadorService.GetAllByEmpresa(id);
            }

            else
                avisos = this.avisoService.GetAllByReclutador(id, false).ToList();
            return View(avisos);
        }

        // GET: Aviso/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Aviso aviso = this.avisoService.GetById(id.Value);
            if (aviso == null)
            {
                return HttpNotFound();
            }
            return View(aviso);
        }

        // GET: Aviso/Create
        public ActionResult Create()
        {
            ViewBag.Nombre = "asda";
            return View();
        }
        // GET: Aviso/Edit/5
        public ActionResult ReasignarAviso(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Aviso aviso = this.avisoService.GetById(id.Value);
            if (aviso == null)
            {
                return HttpNotFound();
            }
            UsuarioReclutadorService servicioReclutador = new UsuarioReclutadorService();
            List<UsuarioReclutador> Reclutadores = servicioReclutador.GetAllByEmpresa(aviso.UsuarioEmpresa_Id).ToList();
            ViewBag.Aviso = aviso;
            if (Reclutadores == null)
            {
                return HttpNotFound();
            }
            return View(Reclutadores);
        }

        [HttpPost]
        public ActionResult ReasignarAviso(long id, string Aid){
            TallerIVDbContext db = new TallerIVDbContext();
            UsuarioReclutador reclutador = db.IdentityUsers.Find(Aid);
            Aviso aviso = db.Avisos.Find(id);
            //BaseService <UsuarioReclutador> reclutadorService = new BaseService<UsuarioReclutador>();
            //UsuarioReclutador reclutador = reclutadorService.GetAll().FirstOrDefault(r => r.Id == Aid);
            //avisoService.ReasignarAviso(id, reclutador);
            avisoService.ReasignarAviso(aviso, reclutador);
            return RedirectToAction("Index");
        }
        // POST: Aviso/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Aviso aviso)
        {
            if (ModelState.IsValid)
            {
                aviso.UsuarioReclutador_Id = this.User.Identity.GetUserId();

                BaseService<UsuarioEmpresa> usuarioEmpresaService = new BaseService<UsuarioEmpresa>();
                UsuarioEmpresa empresa = usuarioEmpresaService.GetAll().FirstOrDefault(x => x.Reclutadores.Any(r => r.Id == aviso.UsuarioReclutador_Id));
                aviso.UsuarioEmpresa_Id = empresa.Id;
                aviso.UsuarioEmpresa_Nombre = empresa.RazonSocial;
                this.avisoService.AddEntity(aviso);
                return RedirectToAction("Index");
            }
            return View(aviso);
        }

        // GET: Aviso/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Aviso aviso = this.avisoService.GetById(id.Value);
            if (aviso == null)
            {
                return HttpNotFound();
            }
            return View(aviso);
        }

        // POST: Aviso/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Titulo,Descripcion,FechaInicio,FechaFin,UsuarioReclutador_Id,UsuarioEmpresa_Id,UsuarioReclutadorAsignado_Id")] Aviso aviso)
        {
            if (ModelState.IsValid)
            {
                this.avisoService.UpdateEntity(aviso);
                return RedirectToAction("Index");
            }
            return View(aviso);
        }

        // GET: Aviso/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Aviso aviso = this.avisoService.GetById(id.Value);
            if (aviso == null)
            {
                return HttpNotFound();
            }
            return View(aviso);
        }
        //GET: Aviso/Estadisticas/5
        public ActionResult Estadisticas(long id)
        {
            // CONECCTION STRING PARA ORT
            //SqlConnection sqlConnection = new SqlConnection("Data Source=A-srv-bdinst;Initial Catalog=redsocialtinder;Integrated Security=False;User Id=redsocialtinder;Password=ort2017;MultipleActiveResultSets=True");

            // CONECCTION STRING PERSONAL
            string cs = ConfigurationManager.ConnectionStrings["TallerIVContext"].ConnectionString;
            SqlConnection sqlConnection = new SqlConnection(cs);
            SqlDataReader reader = null;

            SqlCommand cmd = new SqlCommand("AvisoAprobadosPorUsuario", sqlConnection);
            cmd.CommandType = CommandType.StoredProcedure;


            //Consulta para la cantidad de aprobaciones que tiene un Aviso.
            //No hace falta, actualmente Estadistica calculo esto apartir del Aviso.
            //cmd.CommandText = "SELECT COUNT(UsuarioEmpleado_Id) as TOTAL from AvisoUsuariosEmpleadosAprobados WHERE Aviso_Id = @query";
            //Consulta para cantidad de Postulantes que aprobaron el aviso.
            //cmd.CommandText = "SELECT COUNT(UsuarioEmpleado_Id) as TOTAL from UsuarioEmpleadoAviso WHERE Aviso_Id = @query";


            //Consulta para cantidad de Postulantes que desaprobaron el aviso
            //cmd.CommandText = "SELECT COUNT(UsuarioEmpleado_Id) as TOTAL from UsuarioEmpleadoAviso1 WHERE Aviso_Id = @query";

            cmd.Parameters.AddWithValue("@avisoId", id);
            cmd.Connection = sqlConnection;

            sqlConnection.Open();

            reader = cmd.ExecuteReader();
            reader.Read();
            int UsuariosQueAprobaron = reader.GetInt32(reader.GetOrdinal("TOTAL"));
            sqlConnection.Close();
            ViewBag.UsuariosQueAprobaron = UsuariosQueAprobaron;

            cmd.CommandText = "AvisoDesaprobadosPorUsuario";


            sqlConnection.Open();

            reader = cmd.ExecuteReader();
            reader.Read();
            int UsuariosQueDesaprobaron = 25;
            UsuariosQueDesaprobaron = reader.GetInt32(reader.GetOrdinal("TOTAL"));
            sqlConnection.Close();
            ViewBag.UsuariosQueDesaprobaron = UsuariosQueDesaprobaron;

            cmd.CommandText = "EstadisticaPorcentajeAViso";


            sqlConnection.Open();

            reader = cmd.ExecuteReader();
            reader.Read();
            List<double> Porcentaje = new List<double>();
            
            while (reader.Read())
            {
                Porcentaje.Add(reader.GetFloat(reader.GetOrdinal("Porcentaje")));
            }

            ViewBag.Porcentaje = Porcentaje;



            TallerIVDbContext db = new TallerIVDbContext();
            Aviso aviso = this.avisoService.GetById(id);
            ViewBag.Aviso = aviso;
            float Total = aviso.UsuariosEmpleadoAprobados.Count() + aviso.UsuariosEmpleadoDesaprobados.Count();
            if (Total == 0.0)
            {
                ViewBag.PorcentajeAp = 0;
                ViewBag.PorcentajeDes = 0;
            } else
            {
                ViewBag.PorcentajeAp = (aviso.UsuariosEmpleadoAprobados.Count()/Total) * 100;
                ViewBag.PorcentajeDes = (aviso.UsuariosEmpleadoDesaprobados.Count() / Total) * 100;
            }
            return View(aviso);
        }
        // GET: HomeEmpleado
        public ActionResult BuscarPostulantes(long id)
        {
            TallerIVDbContext db = new TallerIVDbContext();
            BaseService<UsuarioEmpleado> usuarioService = new BaseService<UsuarioEmpleado>(db);
            IQueryable<UsuarioEmpleado> queryEmpleados = usuarioService.GetAll();
            Aviso aviso = db.Avisos.Where(x => x.Id == id).FirstOrDefault();
            GeneradorCoincidencias generadorCoincidencias = new GeneradorCoincidencias();
            List<Coincidencia> coincidenciasList = generadorCoincidencias.GenerarListadoCoincidencias(aviso, queryEmpleados);
            ViewBag.Aviso = aviso;
            ViewBag.CantidadEncuentros = db.Encuentros.Count(x => x.Aviso_Id == aviso.Id);
            return View(coincidenciasList);
        }

        public JsonResult Like(string id, long aid)
        {
            try
            {
                bool huboEncuentro = false;
                string uid = this.User.Identity.GetUserId();
                TallerIVDbContext db = new TallerIVDbContext();
                AprobadorPostulante postulanteAprobado = new AprobadorPostulante();
                UsuarioEmpleado empleado = db.Users.OfType<UsuarioEmpleado>().Where(x => x.Id == id).FirstOrDefault();
                Aviso aviso = db.Avisos.Where(x => x.Id == aid).FirstOrDefault();
                Encuentro encuentro = postulanteAprobado.Aprobar(empleado, aviso);
                if (encuentro != null) {
                    db.Encuentros.Add(encuentro);
                    huboEncuentro = true;
                }
                db.SaveChanges();
                return Json(new { error = false, message = "Aprobación exitosa", encuentro = huboEncuentro }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(new { error = true, message = "No pudo aprobarse el aviso. Vuelva a intentarlo." }, JsonRequestBehavior.AllowGet);
            }

        }
        public JsonResult DisLike(string id, long aid)
        {
            try
            {
                string uid = this.User.Identity.GetUserId();
                TallerIVDbContext db = new TallerIVDbContext();
                AprobadorPostulante postulanteAprobado = new AprobadorPostulante();

                UsuarioEmpleado empleado = db.Users.OfType<UsuarioEmpleado>().Where(x => x.Id == id).FirstOrDefault();
                Aviso aviso = db.Avisos.Where(x => x.Id == aid).FirstOrDefault();

                aviso.UsuariosEmpleadoDesaprobados.Add(empleado);
                db.SaveChanges();
                return Json(new { error = false, message = "Desaprobación exitosa" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(new { error = true, message = "No pudo desaprobar el aviso. Vuelva a intentarlo." }, JsonRequestBehavior.AllowGet);
            }

        }


        // POST: Aviso/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Aviso aviso = avisoService.GetById(id);
            this.avisoService.RemoveEntity(aviso);
            return RedirectToAction("Index");
        }
        public JsonResult SearchTags(string term)
        {
            TagsService tagsService = new TagsService();
            var tags = tagsService.GetTagsByTitulo(term).Select(x => new { value = x.Id, label = x.Titulo });
            return Json(tags, JsonRequestBehavior.AllowGet);
        }
        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing)
        //    {
        //        db.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}
    }
}
