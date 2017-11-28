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

        public void ReasignarAviso(long idAviso, UsuarioReclutador reclutador)
        {
            
            Aviso aviso = this.GetById(idAviso);
            //UsuarioReclutadorService reclutadorService = new UsuarioReclutadorService();
            //UsuarioReclutador Reclutador = reclutadorService.GetById(Aid);
            //Aviso nuevoAviso = new Aviso(aviso.Titulo, aviso.Descripcion, aviso.FechaInicio, idReclutador, aviso.SueldoOfrecidoPrioridad, aviso.TipoRelacionDeTrabajo, aviso.TipoRelacionDeTrabajoPrioridad,aviso.HorasTrabajo,aviso.HorasTrabajoPrioridad, aviso.UsuarioEmpresa_Id, aivo)
            //aviso.UsuarioReclutador_Id = reclutador.Id;
            aviso.UsuarioReclutador = reclutador;
            this.UpdateEntity(aviso);
        }
    }
}
