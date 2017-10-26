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
        public Aviso(string titulo, string descripcion, DateTime fechainicio, UsuarioReclutador usuarioreclutador, List<AptitudPorAviso> aptitudesbuscadas, TipoRelacionDeTrabajo relaciontrabajo, Prioridad relaciontrabajop, int horastrabajo, Prioridad horastrabajop, string usuarioempresa_id)
        {
            this.Titulo = titulo;
            this.Descripcion = descripcion;
            this.FechaInicio = fechainicio;
            //this.UsuarioReclutador_Id = usuarioreclutador_id;
            this.UsuarioReclutador = usuarioreclutador;
            this.AptitudesBuscadas = aptitudesbuscadas;
            this.TipoRelacionDeTrabajo = relaciontrabajo;
            this.TipoRelacionDeTrabajoPrioridad = relaciontrabajop;
            this.HorasTrabajo = horastrabajo;
            this.HorasTrabajoPrioridad = horastrabajop;
            this.UsuarioEmpresa_Id = usuarioempresa_id;

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
    }
}
