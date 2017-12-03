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
    public class ChatController : Controller
    {
        // GET: Chat
        public ActionResult Index(long encuentro_id)
        {
            TallerIVDbContext db = new TallerIVDbContext();
            BaseService<Encuentro> encuentroService = new BaseService<Encuentro>(db);
            BaseService<UsuarioPersona> usuariosService = new BaseService<UsuarioPersona>(db);
            Encuentro encuentro = encuentroService.GetById(encuentro_id);
            string userid = this.User.Identity.GetUserId();
            UsuarioPersona usuario = usuariosService.GetAll().FirstOrDefault(x => x.Id == userid);
            ViewBag.Name = usuario.Nombre;
            ViewBag.UserId = userid;
            return View(encuentro);
        }
    }
}