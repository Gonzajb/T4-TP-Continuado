using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TallerIV.Dominio.Avisos;
using TallerIV.Dominio.Usuarios;

namespace TallerIV.Dominio
{
    public class Aviso
    {
        public Aviso()
        {
        }
        public Aviso(string titulo, string descripcion, DateTime fechaInicio, UsuarioReclutador usuarioReclutador, List<AptitudPorAviso> aptitudesBuscados, TipoRelacionDeTrabajo relacionTrabajo, Prioridad relacionTrabajoPrioridad, int horasTrabajo, Prioridad horasTrabajoPrioridad, string usuarioEmpresaId, string usuarioNombre)
        {
            this.Titulo = titulo;
            this.Descripcion = descripcion;
            this.FechaInicio = fechaInicio;
            //this.UsuarioReclutador_Id = usuarioreclutador_id;
            this.UsuarioReclutador = usuarioReclutador;
            this.AptitudesBuscadas = aptitudesBuscados;
            this.TipoRelacionDeTrabajo = relacionTrabajo;
            this.TipoRelacionDeTrabajoPrioridad = relacionTrabajoPrioridad;
            this.HorasTrabajo = horasTrabajo;
            this.HorasTrabajoPrioridad = HorasTrabajoPrioridad;
            this.UsuarioEmpresa_Id = usuarioEmpresaId;
            this.UsuarioEmpresa_Nombre = usuarioNombre;

        }
        public long Id { get; set; }
        public string Titulo { get; set; }
        public string Descripcion { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime? FechaFin { get; set; }
        public string UsuarioReclutador_Id { get; set; }
        public virtual UsuarioReclutador UsuarioReclutador { get; set; }
        public virtual List<AptitudPorAviso> AptitudesBuscadas { get; set; }
        public float? SueldoOfrecido { get; set; }
        public Prioridad? SueldoOfrecidoPrioridad { get; set; }
        public TipoRelacionDeTrabajo TipoRelacionDeTrabajo { get; set; }
        public Prioridad TipoRelacionDeTrabajoPrioridad { get; set; }
        public int HorasTrabajo { get; set; }
        public Prioridad HorasTrabajoPrioridad { get; set; }
        public string UsuarioEmpresa_Id { get; set; }
        public string UsuarioEmpresa_Nombre { get; set; }
        public virtual List<UsuarioEmpleado> UsuariosEmpleadoAprobados { get; set; }
        public virtual List<UsuarioEmpleado> UsuariosEmpleadoDesaprobados { get; set; }
        public bool ComprobarUsuarioAprobado(UsuarioEmpleado usuarioEmpleado) {
            return this.UsuariosEmpleadoAprobados.Contains(usuarioEmpleado);
        }
    }
}
