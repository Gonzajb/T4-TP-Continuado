using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TallerIV.Dominio.Coincidencias
{
    public interface IAprobador
    {
        Encuentro Aprobar(UsuarioEmpleado postulante, Aviso avisoAAprobar);
    }
}
