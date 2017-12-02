using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TallerIV.Datos;
using TallerIV.Dominio;
using TallerIV.Negocio.Servicios;

namespace TallerIV.Controllers
{
    public class ContactosController : Controller
    {
        // GET: Contactos
        public ActionResult Index()
        {
            EncuentrosService encuentrosService = new EncuentrosService();
            string userid = this.User.Identity.GetUserId();
            List<Encuentro> encuentros = encuentrosService.GetEncuentrosConcretadosPorUsuario(userid);
            ViewBag.UserId = userid;
            return View(encuentros);
        }
    }
}