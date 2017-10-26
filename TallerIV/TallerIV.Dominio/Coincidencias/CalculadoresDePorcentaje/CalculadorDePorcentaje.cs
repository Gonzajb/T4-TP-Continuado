using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TallerIV.Dominio
{
    public class CalculadorDePorcentaje
    {
        public CalculadorDePorcentaje() { }
        public CalculadorDePorcentaje(Aviso aviso)
        {
            this.Aviso = aviso;
            CalcularPuntaje(aviso);
        }
        public CalculadorDePorcentaje(UsuarioEmpleado usuarioEmpleado)
        {
            this.Empleado = usuarioEmpleado;
        }
        private void CalcularPuntaje(Aviso aviso)
        {
            float puntajeMaximo = 0;
            foreach(Tag tag in aviso.TagsBuscados)
            {
                //PuntajeMaximo += (int)aviso.TagsBuscadosPrioridad;
            }
            this.PuntajeMaximo = puntajeMaximo;
        }
        public Coincidencia GenerarCoincidencia(UsuarioEmpleado empleado)
        {            
            int PuntajeEmpleado = 0;
            foreach(Tag tag in Aviso.TagsBuscados)
            {
                int i = 0;
                while (tag != empleado.Tags[i] || i < empleado.Tags.Count)
                {
                    i++;
                }
                if (tag == empleado.Tags[1])
                {
                    //PuntajeEmpleado += tag.Prioridad;
                }                
            }
            Coincidencia coincidencia = new Coincidencia(PuntajeMaximo / PuntajeEmpleado, empleado, Aviso);

            return coincidencia;
        }
        public Coincidencia GenerarCoincidencia()
        {
            Coincidencia coincidencia = null;
            float PuntosAviso = 0;
            foreach (Tag tag in Aviso.TagsBuscados)
            {
                
            }            
            return coincidencia;
        }
        public Aviso Aviso { get; set; }
        public UsuarioEmpleado Empleado { get; set; }
        public float PuntajeMaximo { get; set; }
    }

}
