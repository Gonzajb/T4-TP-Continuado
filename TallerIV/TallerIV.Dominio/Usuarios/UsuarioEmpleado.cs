using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TallerIV.Dominio.Usuarios;

namespace TallerIV.Dominio
{
    public class UsuarioEmpleado : UsuarioPersona
    {
        public UsuarioEmpleado(){}

        public UsuarioEmpleado(DateTime fechaDeResgistro, string email, string userName, string nombre, string apellido, DateTime? fechaDeNacimiento, string cartaDePresentacion, List<Aptitud> tags = null) : base(fechaDeResgistro, email, userName,nombre,apellido,fechaDeNacimiento)
        {
            this.Aptitud = new List<Aptitud>();

            this.Nombre = nombre;
            this.Apellido = apellido;
            this.FechaDeNacimiento = fechaDeNacimiento;
            this.CartaDePresentacion = cartaDePresentacion;

            if (tags != null)
                this.Aptitud.AddRange(tags);
        }
        public string CartaDePresentacion { get; set; }
        public virtual List<Aptitud> Aptitud { get; set; }
        public virtual BusquedaUsuarioPostulante Busqueda { get; set; }
        public long? Busqueda_Id { get; set; }
        public virtual List<Aviso> AvisosAprobados { get; set; }
        public virtual List<Aviso> AvisosDesaprobados { get; set; }
        [NotMapped]
        public string TagsText
        {
            get
            {
                return String.Join(",", this.Aptitud.Select(x => x.Titulo));
            }
        }
        public bool ComprobarAvisoAprobado(Aviso aviso) {
            return this.AvisosAprobados.Contains(aviso);
        }
        //public virtual ParametrosBusquedaUsuario ParametrosBusqueda { get; set; }
        //public long? ParametrosBusqueda_Id { get; set; }
    }
}
