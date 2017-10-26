using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TallerIV.Dominio.Coincidencias.CalculadoresDePorcentaje
{
    public abstract class CalcularPorcentaje
    {
        protected int CalcularPuntajeMaximo(Aviso aviso)
        {
            int puntajeMaximo = 0;
            foreach (Tag tag in aviso.TagsBuscados)
            {
                //PuntajeMaximo += (int)aviso.TagsBuscadosPrioridad;
            }
            return puntajeMaximo;
        }
        //public abstract Coincidencia GenerarCoincidencia(Aviso aviso);
        //public abstract Coincidencia GenerarCoicidencia(UsuarioEmpleado empleado);
    }
}
