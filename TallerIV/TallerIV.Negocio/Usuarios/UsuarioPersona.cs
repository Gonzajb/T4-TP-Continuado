using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TallerIV.Negocio
{
    public abstract class UsuarioPersona: Usuario
    {
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public DateTime FechaDeNacimiento { get; set; }
        public int Edad
        {
            get
            {
                DateTime zeroTime = new DateTime(1, 1, 1);
                int years = (zeroTime + (DateTime.Now - this.FechaDeNacimiento)).Year - 1;
                return years;
            }
            private set { }
        }

    }
}
