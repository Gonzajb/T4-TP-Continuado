using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TallerIV.Dominio
{
    public abstract class UsuarioPersona: Usuario
    {
        public UsuarioPersona() { }
        public UsuarioPersona(DateTime fechaDeResgistro, string email, string telefono, string userName, string nombre, string apellido, DateTime? fechaDeNacimiento) : base(fechaDeResgistro, email,userName) {
            Nombre = nombre;
            Apellido = apellido;
            FechaDeNacimiento = fechaDeNacimiento;
            Telefono = telefono;
        }
        public string Telefono { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public DateTime? FechaDeNacimiento { get; set; }
        public int Edad
        {
            get
            {
                DateTime zeroTime = new DateTime(1, 1, 1);
                int years = 0;
                if (this.FechaDeNacimiento.HasValue)
                {
                    years = (zeroTime + (DateTime.Now - FechaDeNacimiento.Value)).Year - 1;
                }
                else
                {
                }
                return years;
            }
            private set { }
        }

    }
}
