using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TallerIV.Negocio.Servicios;

namespace TallerIV.Controllers
{
    public class HomeEmpleadoController : Controller
    {

        // GET: HomeEmpleado
        private AvisosService avisoService;
        public ActionResult Index()
        {
            avisoService = new AvisosService();

            return View(avisoService.GetAll(false).ToList());
        }
        
        public string Like(int id)
        {
            return "hola";
        }    

    }
}