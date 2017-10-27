using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TallerIV.Dominio.Avisos;

namespace TallerIV.Dominio.Coincidencias.CalculadoresDePorcentaje
{
    public abstract class CalcularPorcentaje
    {
        protected int CalcularPuntajeMaximo(Aviso aviso)
        {
            int PuntajeMaximo = 0;
            foreach (AptitudPorAviso aptitud in aviso.AptitudesBuscadas)
            {
                PuntajeMaximo += (int)aptitud.Prioridad;
            }
            return PuntajeMaximo;
        }
        //public abstract Coincidencia GenerarCoincidencia(Aviso aviso);
        //public abstract Coincidencia GenerarCoicidencia(UsuarioEmpleado empleado);
    }
}
