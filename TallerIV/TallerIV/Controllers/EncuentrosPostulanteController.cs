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

namespace TallerIV.Controllers
{
    [Authorize(Roles = "Empleado")]
    public class EncuentrosPostulanteController : Controller
    {
        private TallerIVDbContext db = new TallerIVDbContext();

        // GET: EncuentrosPostulante
        public ActionResult Index()
        {
            string userid = this.User.Identity.GetUserId();
            var encuentros = db.Encuentros.Include(e => e.Aviso).Include(e => e.UsuarioEmpleado).Include(e => e.UsuarioReclutador).Where(x => x.UsuarioEmpleado_Id == userid);
            return View(encuentros.ToList());
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
