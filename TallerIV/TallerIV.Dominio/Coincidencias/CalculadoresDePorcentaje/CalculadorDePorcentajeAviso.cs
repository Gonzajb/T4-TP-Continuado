using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            foreach(Tag tag in Aviso.TagsBuscados)
            {
                int i = 0;
                while (tag.Titulo != empleado.Tags[i].Titulo || i < empleado.Tags.Count)
                {
                    i++;
                }
                if (tag.Titulo == empleado.Tags[i].Titulo)
                {
                    //PuntajeEmpleado += tag.Prioridad;
                }                
            }
            Coincidencia coincidencia = new Coincidencia(PuntajeMaximo / PuntajeEmpleado, empleado, Aviso);

            return coincidencia;
        }
        public Aviso Aviso { get; set; }
        public int PuntajeMaximo { get; set; }
    }

}
