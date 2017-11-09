using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TallerIV.Dominio.Coincidencias
{
    public class AprobadorAviso : IAprobador
    {
        public Encuentro Aprobar(UsuarioEmpleado postulante, Aviso avisoAAprobar)
        {
            //Se agrega el aviso a aprobar

            Encuentro encuentro = null;
            bool huboEncuentro = avisoAAprobar.ComprobarUsuarioAprobado(postulante);
            if (huboEncuentro)
                encuentro = new Encuentro(avisoAAprobar.UsuarioReclutador, postulante, avisoAAprobar);

            return encuentro;
        }
    }
}
