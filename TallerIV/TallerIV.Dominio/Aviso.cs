using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TallerIV.Dominio.Usuarios;

namespace TallerIV.Dominio
{
    public class Aviso
    {
        public Aviso()
        {
        }
        public Aviso(string titulo, string descripcion, DateTime fechaInicio, UsuarioReclutador usuarioReclutador, List<Tag> tagsBuscados, TipoRelacionDeTrabajo relacionTrabajo, Prioridad relacionTrabajoPrioridad, int horasTrabajo, Prioridad horasTrabajoPrioridad, string usuarioEmpresaId)
        {
            this.Titulo = titulo;
            this.Descripcion = descripcion;
            this.FechaInicio = FechaInicio;
            //this.UsuarioReclutador_Id = usuarioreclutador_id;
            this.UsuarioReclutador = usuarioReclutador;
            this.TagsBuscados = tagsBuscados;
            this.TipoRelacionDeTrabajo = relacionTrabajo;
            this.TipoRelacionDeTrabajoPrioridad = relacionTrabajoPrioridad;
            this.HorasTrabajo = horasTrabajo;
            this.HorasTrabajoPrioridad = HorasTrabajoPrioridad;
            this.UsuarioEmpresa_Id = usuarioEmpresaId;

        }
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Descripcion { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime? FechaFin { get; set; }
        public string UsuarioReclutador_Id { get; set; }
        public virtual UsuarioReclutador UsuarioReclutador { get; set; }
        public virtual List<Tag> TagsBuscados { get; set; }
        public float? SueldoOfrecido { get; set; }
        public Prioridad? SueldoOfrecidoPrioridad { get; set; }
        public TipoRelacionDeTrabajo TipoRelacionDeTrabajo { get; set; }
        public Prioridad TipoRelacionDeTrabajoPrioridad { get; set; }
        public int HorasTrabajo { get; set; }
        public Prioridad HorasTrabajoPrioridad { get; set; }
        public string UsuarioEmpresa_Id { get; set; }
    }
}
