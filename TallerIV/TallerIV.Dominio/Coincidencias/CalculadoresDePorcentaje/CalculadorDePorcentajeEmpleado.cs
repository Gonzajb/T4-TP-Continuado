using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TallerIV.Dominio.Avisos;

namespace TallerIV.Dominio.Coincidencias.CalculadoresDePorcentaje
{
    public class CalculadorDePorcentajeEmpleado : CalcularPorcentaje
    {
        public CalculadorDePorcentajeEmpleado() { }
        public CalculadorDePorcentajeEmpleado(UsuarioEmpleado empleado)
        {
            this.Empleado = empleado;
        }
        public Coincidencia GenerarCoincidencia(Aviso aviso)
        {
            int PuntajeMaximo = CalcularPuntajeMaximo(aviso);
            int PuntajeEmpleado = 0;
            Coincidencia coincidencia;
            if (Empleado.Aptitud != null && aviso.AptitudesBuscadas != null)
            {
                foreach (AptitudPorAviso aptitudAviso in aviso.AptitudesBuscadas)
                {
                    int i = 0;
                    while (i < Empleado.Aptitud.Count && aptitudAviso.Aptitud.Id != Empleado.Aptitud[i].Id)
                    {
                        i++;
                    }
                    if (i != Empleado.Aptitud.Count)
                    {
                        PuntajeEmpleado += (int)aptitudAviso.Prioridad;
                    }
                }
                coincidencia = new Coincidencia((float)PuntajeEmpleado / (float)PuntajeMaximo * 100, Empleado, aviso);
            }
            else
            {
                coincidencia = null;
            }
            return coincidencia;
        }
        public UsuarioEmpleado Empleado { get; }
    }
}
