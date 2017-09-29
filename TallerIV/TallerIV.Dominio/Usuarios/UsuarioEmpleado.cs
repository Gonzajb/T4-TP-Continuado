using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TallerIV.Dominio.Usuarios;

namespace TallerIV.Dominio
{
    public class UsuarioEmpleado: UsuarioPersona
    {
        public UsuarioEmpleado(){}

        public UsuarioEmpleado(DateTime fechaDeResgistro, string email, string userName, string nombre, string apellido, DateTime? fechaDeNacimiento, List<Tag> tags = null) : base(fechaDeResgistro, email, userName,nombre,apellido,fechaDeNacimiento)
        {
            this.Tags = new List<Tag>();

            this.Nombre = nombre;
            this.Apellido = apellido;
            this.FechaDeNacimiento = fechaDeNacimiento;

            if (tags != null)
                this.Tags.AddRange(tags);
        }
        public virtual List<Tag> Tags { get; set; }
        //public virtual ParametrosBusquedaUsuario ParametrosBusqueda { get; set; }
        //public long? ParametrosBusqueda_Id { get; set; }
    }
}
