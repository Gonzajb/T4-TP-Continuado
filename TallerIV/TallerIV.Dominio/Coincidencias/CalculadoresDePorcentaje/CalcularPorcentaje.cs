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
            int puntajeMaximo = 0;
            foreach (AptitudPorAviso aptitud in aviso.AptitudesBuscadas)
            {
                //PuntajeMaximo += (int)aviso.TagsBuscadosPrioridad;
            }
            return puntajeMaximo;
        }
        //public abstract Coincidencia GenerarCoincidencia(Aviso aviso);
        //public abstract Coincidencia GenerarCoicidencia(UsuarioEmpleado empleado);
    }
}
