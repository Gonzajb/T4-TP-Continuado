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
            if (Empleado.Tags != null && aviso.AptitudesBuscadas != null)
            {
                foreach (AptitudPorAviso aptitud in aviso.AptitudesBuscadas)
                {
                    int i = 0;
                    while (aptitud.Aptitud.Titulo != Empleado.Tags[i].Titulo && i < Empleado.Tags.Count)
                    {
                        i++;
                    }
                    if (aptitud.Aptitud.Titulo == Empleado.Tags[i].Titulo)
                    {
                        //PuntajeEmpleado += tag.Prioridad;
                    }
                }
                coincidencia = new Coincidencia(PuntajeMaximo / PuntajeEmpleado, Empleado, aviso);
            }
            else
            {
                coincidencia = null;
            }
            return coincidencia;
        }
        public UsuarioEmpleado Empleado { get; set; }
    }
}
