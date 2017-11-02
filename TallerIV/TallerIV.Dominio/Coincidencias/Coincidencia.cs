using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TallerIV.Dominio
{
    public class Coincidencia
    {
        public Coincidencia()
        {

        }
        public Coincidencia(float porcentaje, UsuarioEmpleado empleado, Aviso aviso)
        {
            this.Porcentaje = porcentaje;
            this.Empleado = empleado;
            this.Aviso = aviso;
        }
        public float Porcentaje { get; set; }
        public Aviso Aviso { get; set; }
        public UsuarioEmpleado Empleado { get; set; }
    }
}
