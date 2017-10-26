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
        /// <summary>
        /// Genera listado de coincidencias desde una instancia de un Aviso
        /// </summary>
        /// <param name="avisoOrigen"></param>
        /// <param name="usuariosEmpleado"></param>
        /// <returns>Listado de coincidencias</returns>
        public List<Coincidencia> GenerarListadoCoincidencias(Aviso avisoOrigen, IQueryable<UsuarioEmpleado> usuariosEmpleado) {
            //Inicialización
            List<Coincidencia> listCoincidencias = new List<Coincidencia>();
            CalculadorDePorcentaje calculadorDePorcentaje = new CalculadorDePorcentaje();
            calculadorDePorcentaje.Aviso = avisoOrigen;

            //Se filtran todos los empleados que cumplan con los parámetros excluyentes
            List<UsuarioEmpleado> listCandidatos = usuariosEmpleado.Where(x =>
                x.Busqueda_Id.HasValue
                && (avisoOrigen.HorasTrabajoPrioridad == Prioridad.Excluyente ? avisoOrigen.HorasTrabajo == x.Busqueda.HorasTrabajo : true)
                && (avisoOrigen.SueldoOfrecidoPrioridad == Prioridad.Excluyente ? avisoOrigen.SueldoOfrecido >= x.Busqueda.SueldoMinimo : true)
                && (avisoOrigen.TipoRelacionDeTrabajoPrioridad == Prioridad.Excluyente ? avisoOrigen.TipoRelacionDeTrabajo == x.Busqueda.TipoRelacionDeTrabajo : true)
            ).ToList();
            
            //Se generan las coincidencias
            foreach (var empleado in listCandidatos)
            {
                calculadorDePorcentaje.Empleado = empleado;
                Coincidencia coincidencia = calculadorDePorcentaje.GenerarCoincidencia();
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
            CalculadorDePorcentaje calculadorDePorcentaje = new CalculadorDePorcentaje();
            calculadorDePorcentaje.Empleado = usuarioEmpleadoOrigen;
            BusquedaUsuarioPostulante busqueda = usuarioEmpleadoOrigen.Busqueda;

            //Se filtran todos los avisos que cumplan con los parámetros excluyentes
            List<Aviso> listCandidatos = avisos.Where(aviso =>
                aviso.FechaInicio <= fechaActual
                && (aviso.FechaFin.HasValue ? aviso.FechaFin >= fechaActual : true)
                && (busqueda.HorasTrabajoPrioridad == Prioridad.Excluyente ? busqueda.HorasTrabajo == aviso.HorasTrabajo : true)
                && (busqueda.SueldoMinimoPrioridad == Prioridad.Excluyente ? busqueda.SueldoMinimo <= aviso.SueldoOfrecido : true)
                && (busqueda.TipoRelacionDeTrabajoPrioridad == Prioridad.Excluyente ? busqueda.TipoRelacionDeTrabajo == aviso.TipoRelacionDeTrabajo : true)
            ).ToList();

            //Se generan las coincidencias
            foreach (var aviso in listCandidatos)
            {
                calculadorDePorcentaje.Aviso = aviso;
                Coincidencia coincidencia = calculadorDePorcentaje.GenerarCoincidencia();
                listCoincidencias.Add(coincidencia);
            }

            //Se ordenan las coincidencias
            listCoincidencias = listCoincidencias.OrderBy(x => x.Porcentaje).ToList();

            return listCoincidencias;
        }
    }
}
