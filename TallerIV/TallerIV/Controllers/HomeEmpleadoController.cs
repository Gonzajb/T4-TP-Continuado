using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TallerIV.Datos;
using TallerIV.Dominio;
using TallerIV.Dominio.Coincidencias;
using TallerIV.Negocio.Servicios;

namespace TallerIV.Controllers
{
    public class HomeEmpleadoController : Controller
    {

        // GET: HomeEmpleado
        private AvisosService avisoService;
        public ActionResult Index()
        {
            TallerIVDbContext db = new TallerIVDbContext();
            BaseService<Aviso> avisosService = new BaseService<Aviso>(db);
            IQueryable<Aviso> queryAvisos = avisoService.GetAll();
            string uid = this.User.Identity.GetUserId();
            UsuarioEmpleado usuario = db.Users.OfType<UsuarioEmpleado>().Where(x => x.Id == uid).FirstOrDefault();
            GeneradorCoincidencias generadorCoincidencias = new GeneradorCoincidencias();
            List<Coincidencia> coincidenciasList = generadorCoincidencias.GenerarListadoCoincidencias(usuario, queryAvisos);
            return View(coincidenciasList);
        }
        
        public JsonResult Like(int id)
        {
            try
            {
                string uid = this.User.Identity.GetUserId();
                TallerIVDbContext db = new TallerIVDbContext();
                AprobadorAviso avisoAprobado = new AprobadorAviso();
                UsuarioEmpleado empleado = db.Users.OfType<UsuarioEmpleado>().Where(x => x.Id == uid).FirstOrDefault();
                Aviso aviso = db.Avisos.Where(x => x.Id == id).FirstOrDefault();
                avisoAprobado.Aprobar(empleado, aviso);
                return Json(new { error = false, message = "Aprobación exitosa" });
            }
            catch (Exception e) {
                return Json(new { error = true, message = "No pudo aprobarse el aviso. Vuelva a intentarlo." });
            }

        }
        public JsonResult DisLike(int id)
        {
            try
            {
                string uid = this.User.Identity.GetUserId();
                TallerIVDbContext db = new TallerIVDbContext();
                AprobadorAviso avisoAprobado = new AprobadorAviso();

                UsuarioEmpleado empleado = db.Users.OfType<UsuarioEmpleado>().Where(x => x.Id == uid).FirstOrDefault();
                Aviso aviso = db.Avisos.Where(x => x.Id == id).FirstOrDefault();

                empleado.AvisosDesaprobados.Add(aviso);
                db.SaveChanges();
                return Json(new { error = false, message = "Desaprobación exitosa" });
            }
            catch (Exception e)
            {
                return Json(new { error = true, message = "No pudo desaprobar el aviso. Vuelva a intentarlo." });
            }

        }

    }
}