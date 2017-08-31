using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TallerIV.Negocio
{
    public class UsuarioReclutador: UsuarioPersona
    {
        public List<Aviso>Avisos { get; set; }
    }
}
