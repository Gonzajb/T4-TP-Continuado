using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TallerIV.Dominio;

namespace TallerIV.Negocio.Servicios
{
    public class AvisosService : BaseService<Aviso>
    {
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

        public void ReasignarAviso(long idAviso, string idReclutador)
        {
            Aviso aviso = this.GetById(idAviso);
            aviso.UsuarioReclutador_Id = idReclutador;
            this.UpdateEntity(aviso);
        }
    }
}
