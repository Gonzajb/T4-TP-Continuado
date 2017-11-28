using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TallerIV.Dominio;


namespace TallerIV.Negocio.Servicios
{
    public class UsuarioReclutadorService : BaseService<UsuarioReclutador>
    {
        public IEnumerable<UsuarioReclutador> GetAllByEmpresa(string usuarioEmpresaId)
        {
            IQueryable<UsuarioReclutador> Reclutadores = this.GetAll().Where(a => a.UsuarioEmpresa_Id == usuarioEmpresaId);
            return Reclutadores.ToList();
        }       
    }
}
