using Microsoft.AspNet.Identity;
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
using TallerIV.Dominio.Chat;
using TallerIV.Negocio.Servicios;

namespace TallerIV.Controllers
{
    [Authorize(Roles = "Reclutador")]
    public class EncuentrosReclutadorController : Controller
    {
        private TallerIVDbContext db = new TallerIVDbContext();

        // GET: EncuentrosaReclutador
        public ActionResult Index()
        {
            string userid = this.User.Identity.GetUserId();
            ViewBag.UserId = userid;
            var encuentros = db.Encuentros.Include(e => e.Aviso).Include(e => e.UsuarioEmpleado).Include(e => e.UsuarioReclutador).Where(x => x.UsuarioReclutador_Id == userid);
            return View(encuentros.ToList());
        }
        public JsonResult Descartar(long id)
        {
            try
            {
                TallerIVDbContext db = new TallerIVDbContext();
                Encuentro encuentro = db.Encuentros.FirstOrDefault(x => x.Id == id);
                db.Encuentros.Remove(encuentro);
                db.SaveChanges();
                return Json(new { error = false, message = "Descartado exitoso" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(new { error = true, message = "No pudo descartarse el encuentro. Vuelva a intentarlo." }, JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult IniciarConversacion(int encuentro_id, string usuario_id, string texto) {
            try
            {
                BaseService<Mensaje> mensajesService = new BaseService<Mensaje>();
                Mensaje mensaje = new Mensaje(texto, usuario_id, encuentro_id);
                mensajesService.AddEntity(mensaje);
                return Json(new { error = false, message = "Conversación iniciada correctamente." }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e) {
                return Json(new { error = true, message = "No se ha podido iniciar la conversación." }, JsonRequestBehavior.AllowGet);
            }
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
