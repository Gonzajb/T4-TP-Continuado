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
    public class UsuariosReclutadoresController : Controller
    {
        private TallerIVDbContext db = new TallerIVDbContext();

        // GET: UsuariosReclutadores
        public ActionResult Index()
        {
            var uid = this.User.Identity.GetUserId();
            return View(db.IdentityUsers.OfType<UsuarioReclutador>().Where(x => x.UsuarioEmpresa_Id == uid).ToList());
        }

        // GET: UsuariosReclutadores/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UsuarioReclutador usuarioReclutador = db.IdentityUsers.Find(id);
            if (usuarioReclutador == null)
            {
                return HttpNotFound();
            }
            return View(usuarioReclutador);
        }

        // GET: UsuariosReclutadores/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: UsuariosReclutadores/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Email,EmailConfirmed,PasswordHash,SecurityStamp,PhoneNumber,PhoneNumberConfirmed,TwoFactorEnabled,LockoutEndDateUtc,LockoutEnabled,AccessFailedCount,UserName,FechaRegistro,Telefono,Nombre,Apellido,FechaDeNacimiento,Edad,UsuarioEmpresa_Id")] UsuarioReclutador usuarioReclutador)
        {
            if (ModelState.IsValid)
            {
                db.IdentityUsers.Add(usuarioReclutador);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(usuarioReclutador);
        }

        // GET: UsuariosReclutadores/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UsuarioReclutador usuarioReclutador = db.IdentityUsers.Find(id);
            if (usuarioReclutador == null)
            {
                return HttpNotFound();
            }
            return View(usuarioReclutador);
        }

        // POST: UsuariosReclutadores/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Email,EmailConfirmed,PasswordHash,SecurityStamp,PhoneNumber,PhoneNumberConfirmed,TwoFactorEnabled,LockoutEndDateUtc,LockoutEnabled,AccessFailedCount,UserName,FechaRegistro,Telefono,Nombre,Apellido,FechaDeNacimiento,Edad,UsuarioEmpresa_Id")] UsuarioReclutador usuarioReclutador)
        {
            if (ModelState.IsValid)
            {
                db.Entry(usuarioReclutador).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(usuarioReclutador);
        }

        // GET: UsuariosReclutadores/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UsuarioReclutador usuarioReclutador = db.IdentityUsers.Find(id);
            if (usuarioReclutador == null)
            {
                return HttpNotFound();
            }
            return View(usuarioReclutador);
        }

        // POST: UsuariosReclutadores/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            UsuarioReclutador usuarioReclutador = db.IdentityUsers.Find(id);
            db.IdentityUsers.Remove(usuarioReclutador);
            db.SaveChanges();
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
