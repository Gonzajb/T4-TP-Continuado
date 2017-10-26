using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TallerIV.Dominio.Avisos;
using TallerIV.Dominio.Coincidencias;

namespace TallerIV.Dominio.Coincidencias.CalculadoresDePorcentaje
{
    public class CalculadorDePorcentajeAviso : CalcularPorcentaje
    {
        public CalculadorDePorcentajeAviso() { }
        public CalculadorDePorcentajeAviso(Aviso aviso)
        {
            this.Aviso = aviso;
            this.PuntajeMaximo = CalcularPuntajeMaximo(aviso);
        }
        //public CalculadorDePorcentaje(UsuarioEmpleado usuarioEmpleado)
        //{
        //    this.Empleado = usuarioEmpleado;
        //}
        //private void CalcularPuntajeMaximo(Aviso aviso)
        //{
        //    int puntajeMaximo = 0;
        //    foreach(Tag tag in aviso.TagsBuscados)
        //    {
        //        //PuntajeMaximo += (int)aviso.TagsBuscadosPrioridad;
        //    }
        //    this.PuntajeMaximo = puntajeMaximo;
        //}
        public Coincidencia GenerarCoincidencia(UsuarioEmpleado empleado)
        {
            int PuntajeEmpleado = 0;
            Coincidencia coincidencia;
            if (empleado.Tags != null && Aviso.AptitudesBuscadas != null)
            {
                foreach (AptitudPorAviso aptitud in Aviso.AptitudesBuscadas)
                {
                    int i = 0;
                    while (aptitud.Aptitud.Titulo != empleado.Tags[i].Titulo && i < empleado.Tags.Count)
                    {
                        i++;
                    }
                    if (aptitud.Aptitud.Titulo == empleado.Tags[i].Titulo)
                    {
                        //PuntajeEmpleado += tag.Prioridad;
                    }
                }
                coincidencia = new Coincidencia(PuntajeMaximo / PuntajeEmpleado, empleado, Aviso);
            }
            else
            {
                coincidencia = null;
            }
            return coincidencia;
        }
        public Aviso Aviso { get; set; }
        public int PuntajeMaximo { get; set; }
    }

}
