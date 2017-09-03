using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TallerIV.Dominio
{
    public class UsuarioEmpleado: Usuario
    {
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public virtual List<Tag> Tags { get; set; }
    }
}
