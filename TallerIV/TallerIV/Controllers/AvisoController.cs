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
using TallerIV.Dominio.RangoEstadisitica;
using TallerIV.Models;

namespace TallerIV.Controllers
{
    [Authorize(Roles = "Empresa,Reclutador")]
    public class AvisoController : Controller
    {
        private AvisosService avisoService;
        private UsuarioReclutadorService reclutadorService;
        private AvisoSPService avisoSPService;
        public AvisoController()
        {
            avisoService = new AvisosService();
            reclutadorService = new UsuarioReclutadorService();
            avisoSPService = new AvisoSPService();
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
        // GET: Aviso/ReasignarAviso/5
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
            //OPCION 1
            //TallerIVDbContext db = new TallerIVDbContext();
            //UsuarioReclutador reclutador = db.IdentityUsers.Find(Aid);
            //Aviso aviso = db.Avisos.Find(id);
            //avisoService.ReasignarAviso(aviso, reclutador);


            //OPCION 3
            avisoService.ReasignarAviso(id, Aid);

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
            Aviso aviso = this.avisoService.GetById(id);

            int PostulantesQueAprobaron = avisoSPService.AprobacionesDeUsuarios((int)id);
            int PostulantesQueDesaprobaron = avisoSPService.DesaprobacionesDeUsuarios((int)id);
            float PorcentajeAprobadosPostulante = avisoSPService.CalcularPorcentaje(PostulantesQueAprobaron, PostulantesQueDesaprobaron);
            float PorcentajeDesaprobadosPostulante = avisoSPService.CalcularPorcentaje(PostulantesQueDesaprobaron, PostulantesQueAprobaron);

            PorcentajeAprobadosPostulante = (float)Math.Round(PorcentajeAprobadosPostulante, 4);
            PorcentajeDesaprobadosPostulante = (float)Math.Round(PorcentajeDesaprobadosPostulante, 4);


            //if (TotalPostulantes == 0)
            //{
            //    PorcentajeAprobadosPostulante = 0;
            //    PorcentajeDesaprobadosPostulante = 0;
            //} else
            //{
            //    PorcentajeAprobadosPostulante = PostulantesQueAprobaron / TotalPostulantes;
            //    PorcentajeDesaprobadosPostulante = PostulantesQueDesaprobaron / TotalPostulantes;
            //}


            //Consulta para la cantidad de aprobaciones que tiene un Aviso.
            //No hace falta, actualmente Estadistica calculo esto apartir del Aviso.
            //cmd.CommandText = "SELECT COUNT(UsuarioEmpleado_Id) as TOTAL from AvisoUsuariosEmpleadosAprobados WHERE Aviso_Id = @query";
            //Consulta para cantidad de Postulantes que aprobaron el aviso.
            //cmd.CommandText = "SELECT COUNT(UsuarioEmpleado_Id) as TOTAL from UsuarioEmpleadoAviso WHERE Aviso_Id = @query";
            float PorcentajeAprobadosReclutador = avisoSPService.CalcularPorcentaje(aviso.UsuariosEmpleadoAprobados.Count(), aviso.UsuariosEmpleadoDesaprobados.Count());
            float PorcentajeDesaprobadosReclutador = avisoSPService.CalcularPorcentaje(aviso.UsuariosEmpleadoDesaprobados.Count(), aviso.UsuariosEmpleadoAprobados.Count());

            if (float.IsNaN(PorcentajeAprobadosReclutador))
            {
                PorcentajeAprobadosReclutador = 0;
            } else
            {
                PorcentajeAprobadosReclutador = (float)Math.Round(PorcentajeAprobadosReclutador, 4);
            }
            if (float.IsNaN(PorcentajeDesaprobadosReclutador))
            {
                PorcentajeDesaprobadosReclutador = 0;
            } else
            {
                PorcentajeDesaprobadosReclutador = (float)Math.Round(PorcentajeDesaprobadosReclutador, 4);
            }

            RangoEstadistica[] rangoEstadistica = avisoSPService.DevolverRangoEstadisticaOrdenado((int)id);

            EstadisticaViewModel vista = new EstadisticaViewModel { TituloAviso = aviso.Titulo, PorcentajeApPost = PorcentajeAprobadosPostulante*100, PorcentajeDesPost = PorcentajeDesaprobadosPostulante*100, PorcentajeApRec = PorcentajeAprobadosReclutador*100, PorcentajeDesRec = PorcentajeDesaprobadosReclutador*100, RangosEstadistica = rangoEstadistica };

            return View(vista);
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
