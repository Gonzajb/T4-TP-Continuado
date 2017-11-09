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
        
        public string Like(int id)
        {
            string uid = this.User.Identity.GetUserId();
            TallerIVDbContext db = new TallerIVDbContext();

            db.Users.OfType<UsuarioEmpleado>().Where(x => x.Id == uid).FirstOrDefault().AvisosAprobados.Add(db.Avisos.Where(x => x.Id == id).FirstOrDefault());
            db.SaveChanges();

            return "Se guardo"  ;
        }    

    }
}