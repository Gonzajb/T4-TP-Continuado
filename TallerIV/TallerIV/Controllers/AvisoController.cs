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

namespace TallerIV.Controllers
{
    public class AvisoController : Controller
    {
        private AvisosService avisoService;
        public AvisoController()
        {
            avisoService = new AvisosService();
        }

        // GET: Aviso
        public ActionResult Index()
        {
            string id = this.User.Identity.GetUserId();
            return View(this.avisoService.GetAllByEmpresa(id, false));
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

        // POST: Aviso/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Titulo,Descripcion,FechaInicio,FechaFin,UsuarioReclutador_Id,UsuarioEmpresa_Id,UsuarioReclutadorAsignado_Id")] Aviso aviso)
        {
            if (ModelState.IsValid)
            {
                aviso.UsuarioEmpresa_Id = this.User.Identity.GetUserId();
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

        // POST: Aviso/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Aviso aviso = avisoService.GetById(id);
            this.avisoService.RemoveEntity(aviso);
            return RedirectToAction("Index");
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
