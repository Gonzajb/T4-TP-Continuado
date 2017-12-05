using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TallerIV.Datos;
using TallerIV.Dominio;

namespace TallerIV.Negocio.Servicios
{
    public class AvisosService : BaseService<Aviso>
    {
        public AvisosService(TallerIVDbContext db) : base(db)
        {
        }
        public AvisosService() : base()
        {
        }
        public IEnumerable<Aviso> GetAllByEmpresa(string usuarioEmpresaId, bool filtrarFinalizados) {
            IQueryable<Aviso> avisos = this.GetAll().Where(a => a.UsuarioEmpresa_Id == usuarioEmpresaId);
            if (filtrarFinalizados)
          avisos = avisos.Where(a => !a.FechaFin.HasValue);

            return avisos.AsEnumerable();      
        }
        public IEnumerable<Aviso> GetAllByReclutador(string IdReclutador, bool filtrarFinalizados)
        {
            IQueryable<Aviso> avisos = this.GetAll().Where(a => a.UsuarioReclutador_Id == IdReclutador);
            if (filtrarFinalizados)
                avisos = avisos.Where(a => !a.FechaFin.HasValue);

            return avisos.AsEnumerable();
        
        }
        public IEnumerable<Aviso> GetAll(bool filtrarFinalizados)
        {
            IQueryable<Aviso> avisos = this.GetAll();
            if (filtrarFinalizados)
                avisos = avisos.Where(a => !a.FechaFin.HasValue);

            return avisos.AsEnumerable();

        }

        public void ReasignarAviso(Aviso aviso, UsuarioReclutador reclutador)
        {            
            //Aviso aviso = this.GetById(idAviso);
            //UsuarioReclutadorService reclutadorService = new UsuarioReclutadorService();
            //UsuarioReclutador Reclutador = reclutadorService.GetById(Aid);           
            //aviso.UsuarioReclutador_Id = reclutador.Id;
            aviso.UsuarioReclutador = reclutador;
            aviso.UsuarioReclutador_Id = reclutador.Id;
            this.UpdateEntity(aviso);
        }
        public void ReasignarAviso(long idAviso, string Aid)
        {
            Aviso aviso = this.GetById(idAviso);
            UsuarioReclutadorService reclutadorService = new UsuarioReclutadorService(this.repository.GetDbContext());
            UsuarioReclutador reclutador = reclutadorService.GetAll().FirstOrDefault(r => r.Id == Aid);
            aviso.UsuarioReclutador_Id = reclutador.Id;
            this.UpdateEntity(aviso);
        }
    }
}
