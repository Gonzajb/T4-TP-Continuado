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
using TallerIV.Dominio.Usuarios;
using TallerIV.Negocio.Servicios;

namespace TallerIV.Controllers
{
    public class BusquedaUsuarioPostulantesController : Controller
    {
        private BaseService<BusquedaUsuarioPostulante> busquedaService = new BaseService<BusquedaUsuarioPostulante>();
        private BaseService<UsuarioEmpleado> usuariosService = new BaseService<UsuarioEmpleado>();
        private TallerIVDbContext db = new TallerIVDbContext();

        // GET: BusquedaUsuarioPostulantes/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BusquedaUsuarioPostulante busquedaUsuarioPostulante = busquedaService.GetById(id.Value);
            if (busquedaUsuarioPostulante == null)
            {
                return HttpNotFound();
            }
            return View(busquedaUsuarioPostulante);
        }

        // GET: BusquedaUsuarioPostulantes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: BusquedaUsuarioPostulantes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,SueldoMinimo,SueldoMinimoPrioridad,TipoRelacionDeTrabajo,TipoRelacionDeTrabajoPrioridad,HorasTrabajo,HorasTrabajoPrioridad")] BusquedaUsuarioPostulante busquedaUsuarioPostulante)
        {
            if (ModelState.IsValid)
            {
                string userId = this.User.Identity.GetUserId();
                UsuarioEmpleado usuario = usuariosService.GetAll().FirstOrDefault(x => x.Id == userId);
                usuario.Busqueda = busquedaUsuarioPostulante;
                usuariosService.UpdateEntity(usuario);
                //busquedaService.AddEntity(busquedaUsuarioPostulante);
                return RedirectToAction("Index");
            }

            return View(busquedaUsuarioPostulante);
        }

        // GET: BusquedaUsuarioPostulantes/Edit/5
        public ActionResult Edit()
        {
            string userid = this.User.Identity.GetUserId();
            UsuarioEmpleado usuario = usuariosService.GetAll().FirstOrDefault(x => x.Id == userid);
            BusquedaUsuarioPostulante busquedaUsuarioPostulante = usuario.Busqueda;
            if (busquedaUsuarioPostulante == null)
            {
                return RedirectToAction("Create");
            }
            return View(busquedaUsuarioPostulante);
        }

        // POST: BusquedaUsuarioPostulantes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,SueldoMinimo,SueldoMinimoPrioridad,TipoRelacionDeTrabajo,TipoRelacionDeTrabajoPrioridad,HorasTrabajo,HorasTrabajoPrioridad")] BusquedaUsuarioPostulante busquedaUsuarioPostulante)
        {
            if (ModelState.IsValid)
            {
                busquedaService.UpdateEntity(busquedaUsuarioPostulante);
                return RedirectToAction("Index");
            }
            return View(busquedaUsuarioPostulante);
        }

        // GET: BusquedaUsuarioPostulantes/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BusquedaUsuarioPostulante busquedaUsuarioPostulante = db.BusquedaUsuarioPostulantes.Find(id);
            if (busquedaUsuarioPostulante == null)
            {
                return HttpNotFound();
            }
            return View(busquedaUsuarioPostulante);
        }

        // POST: BusquedaUsuarioPostulantes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            BusquedaUsuarioPostulante busquedaUsuarioPostulante = busquedaService.GetById(id);
            busquedaService.RemoveEntity(busquedaUsuarioPostulante);
            return RedirectToAction("Index");
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
