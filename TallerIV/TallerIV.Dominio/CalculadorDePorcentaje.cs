using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TallerIV.Dominio
{
    class CalculadorDePorcentaje
    {
        public CalculadorDePorcentaje() { }
        public CalculadorDePorcentaje (Aviso aviso, UsuarioEmpleado empleado)
        {
            this.Aviso = aviso;
            this.Empleado = empleado;
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
        public static int PrioridadBaja = 1;
        public static int PrioridadNormal = 2;
    }

    public enum Puntos
    {
        PrioridadBaja =2,

    }
}
