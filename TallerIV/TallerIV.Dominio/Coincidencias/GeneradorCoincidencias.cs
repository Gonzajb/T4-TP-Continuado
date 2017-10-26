using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TallerIV.Dominio.Usuarios;

namespace TallerIV.Dominio.Coincidencias
{
    public class GeneradorCoincidencias
    {
        public List<UsuarioEmpleado> GenerarListadoCoincidencias(Aviso aviso, IQueryable<UsuarioEmpleado> usuariosEmpleado) {
            //Se filtran todos los empleados que cumplan con los parámetros excluyentes
            List<UsuarioEmpleado> listUsuariosEmpleados = usuariosEmpleado.Where(x =>
                x.Busqueda_Id.HasValue
                && (aviso.HorasTrabajoPrioridad == Prioridad.Excluyente ? aviso.HorasTrabajo == x.Busqueda.HorasTrabajo : true)
                && (aviso.SueldoOfrecidoPrioridad == Prioridad.Excluyente ? aviso.SueldoOfrecido >= x.Busqueda.SueldoMinimo : true)
                && (aviso.TipoRelacionDeTrabajoPrioridad == Prioridad.Excluyente ? aviso.TipoRelacionDeTrabajo == x.Busqueda.TipoRelacionDeTrabajo : true)
            ).ToList();

            //Se generan las coincidencias

            //Se ordenan las coincidencias

            return listUsuariosEmpleados;
        }
    }
}
