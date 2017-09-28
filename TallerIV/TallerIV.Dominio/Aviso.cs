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
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Descripcion { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime? FechaFin { get; set; }
        public string UsuarioReclutador_Id { get; set; }
        public virtual UsuarioReclutador UsuarioReclutador { get; set; }
        public virtual List<Tag> TagsBuscados { get; set; }
        public float? SueldoOfrecido { get; set; }
        public Prioridad SueldoOfrecidoPrioridad { get; set; }
        public TipoRelacionDeTrabajo TipoRelacionDeTrabajo { get; set; }
        public Prioridad TipoRelacionDeTrabajoPrioridad { get; set; }
        public int HorasTrabajo { get; set; }
        public Prioridad HorasTrabajoPrioridad { get; set; }
        public string UsuarioEmpresa_Id { get; set; }
        public string UsuarioReclutadorAsignado_Id { get; set; }
    }
}
