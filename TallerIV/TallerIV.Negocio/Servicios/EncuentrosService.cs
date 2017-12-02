using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TallerIV.Datos;
using TallerIV.Dominio;

namespace TallerIV.Negocio.Servicios
{
    public class EncuentrosService : BaseService<Encuentro>
    {
        BaseService<UsuarioPersona> usuarioPersonaService;
        public EncuentrosService(TallerIVDbContext db) : base(db) {
            usuarioPersonaService = new BaseService<UsuarioPersona>(db);
        }
        public EncuentrosService() : base() { usuarioPersonaService = new BaseService<UsuarioPersona>(); }
        public List<Encuentro> GetEncuentrosConcretadosPorUsuario(string userid) {
            List<Encuentro> listadoEncuentros;
            IQueryable<Encuentro> queryEncuentros = this.GetAll().Where(x => x.Mensajes.Any());
            UsuarioPersona usuario = usuarioPersonaService.GetAll().FirstOrDefault(x => x.Id == userid);
            if (usuario is UsuarioEmpleado)
            {
                listadoEncuentros = queryEncuentros.Where(x => x.UsuarioEmpleado_Id == userid).ToList();
            }
            else {
                listadoEncuentros = queryEncuentros.Where(x => x.UsuarioReclutador_Id == userid).ToList();
            }
            return listadoEncuentros;
        }
    }
}
