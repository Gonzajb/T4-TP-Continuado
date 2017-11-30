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
            Encuentro encuentro = encuentroService.GetById(encuentro_id);
            ViewBag.UserName = this.User.Identity.GetUserName();
            ViewBag.UserId = this.User.Identity.GetUserId();
            return View(encuentro);
        }
    }
}