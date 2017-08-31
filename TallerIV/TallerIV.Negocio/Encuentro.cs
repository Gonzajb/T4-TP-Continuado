using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TallerIV.Negocio
{
    public class Encuentro
    {
        public int Id { get; set; }
        public UsuarioReclutador UsuarioReclutador { get; set; }
        public UsuarioEmpleado UsuarioEmpleado { get; set; }

    }
}
