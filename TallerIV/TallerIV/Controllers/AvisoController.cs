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
    [Authorize(Roles = "Empresa,Reclutador")]
    public class AvisoController : Controller
    {
        private AvisosService avisoService;
        private UsuarioReclutadorService reclutadorService;
        public AvisoController()
        {
            avisoService = new AvisosService();
            reclutadorService = new UsuarioReclutadorService();
        }

        // GET: Aviso
        public ActionResult Index()
        {
            List<Aviso> avisos;
            string id = this.User.Identity.GetUserId();

            if (User.IsInRole("Empresa"))
            {
                avisos = this.avisoService.GetAllByEmpresa(id, false).ToList();
                ViewBag.UsuarioReclutador = reclutadorService.GetAllByEmpresa(id);
            }

            else
                avisos = this.avisoService.GetAllByReclutador(id, false).ToList();
            return View(avisos);
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
        [HttpPost]
        public ActionResult ReasignarAviso(long idAviso, string idReclutador){
           avisoService.ReasignarAviso(idAviso, idReclutador);
            return RedirectToAction("Index");
        }
        // POST: Aviso/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Aviso aviso)
        {
            if (ModelState.IsValid)
            {
                aviso.UsuarioReclutador_Id = this.User.Identity.GetUserId();

                BaseService<UsuarioEmpresa> usuarioEmpresaService = new BaseService<UsuarioEmpresa>();
                UsuarioEmpresa empresa = usuarioEmpresaService.GetAll().FirstOrDefault(x => x.Reclutadores.Any(r => r.Id == aviso.UsuarioReclutador_Id));
                aviso.UsuarioEmpresa_Id = empresa.Id;

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
        public JsonResult SearchTags(string term)
        {
            TagsService tagsService = new TagsService();
            var tags = tagsService.GetTagsByTitulo(term).Select(x => new { value = x.Id, label = x.Titulo });
            return Json(tags, JsonRequestBehavior.AllowGet);
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
