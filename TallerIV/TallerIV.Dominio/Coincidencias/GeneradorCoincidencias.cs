using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TallerIV.Dominio.Usuarios;
using TallerIV.Dominio.Coincidencias.CalculadoresDePorcentaje;

namespace TallerIV.Dominio.Coincidencias
{
    public class GeneradorCoincidencias
    {
        /// <summary>
        /// Genera listado de coincidencias desde una instancia de un Aviso
        /// </summary>
        /// <param name="avisoOrigen"></param>
        /// <param name="usuariosEmpleado"></param>
        /// <returns>Listado de coincidencias</returns>
        public List<Coincidencia> GenerarListadoCoincidencias(Aviso avisoOrigen, IQueryable<UsuarioEmpleado> usuariosEmpleado) {
            //Inicialización
            List<Coincidencia> listCoincidencias = new List<Coincidencia>();
            CalculadorDePorcentajeAviso calculadorDePorcentaje = new CalculadorDePorcentajeAviso(avisoOrigen);

            //Se filtran todos los empleados que cumplan con los parámetros excluyentes
            List<UsuarioEmpleado> listCandidatos = usuariosEmpleado.Where(usEmp =>
                usEmp.Busqueda_Id.HasValue
                && (avisoOrigen.HorasTrabajoPrioridad == Prioridad.Excluyente ? avisoOrigen.HorasTrabajo == usEmp.Busqueda.HorasTrabajo : true)
                && (avisoOrigen.SueldoOfrecidoPrioridad == Prioridad.Excluyente ? avisoOrigen.SueldoOfrecido >= usEmp.Busqueda.SueldoMinimo : true)
                && (avisoOrigen.TipoRelacionDeTrabajoPrioridad == Prioridad.Excluyente ? avisoOrigen.TipoRelacionDeTrabajo == usEmp.Busqueda.TipoRelacionDeTrabajo : true)
                && !avisoOrigen.UsuariosEmpleadoAprobados.Any(aviso => aviso.Id == usEmp.Id)
                && !avisoOrigen.UsuariosEmpleadoDesaprobados.Any(aviso => aviso.Id == usEmp.Id)
            ).ToList();
            
            //Se generan las coincidencias
            foreach (var empleado in listCandidatos)
            {
                Coincidencia coincidencia = calculadorDePorcentaje.GenerarCoincidencia(empleado);
                listCoincidencias.Add(coincidencia);
            }

            //Se ordenan las coincidencias
            listCoincidencias = listCoincidencias.OrderBy(x => x.Porcentaje).ToList();

            return listCoincidencias;
        }
        /// <summary>
        /// Genera listado de coincidencias desde una instancia de un UsuarioEmpleadoOrigen
        /// </summary>
        /// <param name="usuarioEmpleadoOrigen"></param>
        /// <param name="usuariosEmpleado"></param>
        /// <returns>Listado de coincidencias</returns>
        public List<Coincidencia> GenerarListadoCoincidencias(UsuarioEmpleado usuarioEmpleadoOrigen, IQueryable<Aviso> avisos)
        {
            //Inicialización
            DateTime fechaActual = DateTime.Now;
            List<Coincidencia> listCoincidencias = new List<Coincidencia>();
            CalculadorDePorcentajeEmpleado calculadorDePorcentaje = new CalculadorDePorcentajeEmpleado(usuarioEmpleadoOrigen);
            BusquedaUsuarioPostulante busqueda = usuarioEmpleadoOrigen.Busqueda;

            //Se filtran todos los avisos que cumplan con los parámetros excluyentes
            List<Aviso> listCandidatos = avisos.Where(aviso =>
                aviso.FechaInicio <= fechaActual
                && (aviso.FechaFin.HasValue ? aviso.FechaFin >= fechaActual : true)
                && (busqueda.HorasTrabajoPrioridad == Prioridad.Excluyente ? busqueda.HorasTrabajo == aviso.HorasTrabajo : true)
                && (busqueda.SueldoMinimoPrioridad == Prioridad.Excluyente ? busqueda.SueldoMinimo <= aviso.SueldoOfrecido : true)
                && (busqueda.TipoRelacionDeTrabajoPrioridad == Prioridad.Excluyente ? busqueda.TipoRelacionDeTrabajo == aviso.TipoRelacionDeTrabajo : true)
                && !usuarioEmpleadoOrigen.AvisosAprobados.Any(usEmp => usEmp.Id == aviso.Id)
                && !usuarioEmpleadoOrigen.AvisosDesaprobados.Any(usEmp => usEmp.Id == aviso.Id)
                ).ToList();

            //Se generan las coincidencias
            foreach (var aviso in listCandidatos)
            {                
                Coincidencia coincidencia = calculadorDePorcentaje.GenerarCoincidencia(aviso);
                listCoincidencias.Add(coincidencia);
            }

            //Se ordenan las coincidencias
            listCoincidencias = listCoincidencias.OrderBy(x => x.Porcentaje).ToList();

            return listCoincidencias;
        }
    }
}
