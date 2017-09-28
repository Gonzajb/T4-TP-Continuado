using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TallerIV.Dominio.Usuarios
{
    public class ParametrosBusquedaUsuario
    {
        public long Id { get; set; }
        public double SueldoMinimo { get; set; }
        public Prioridad SueldoMinimoPrioridad { get; set; }
        public TipoRelacionDeTrabajo TipoRelacionDeTrabajo { get; set; }
        public Prioridad TipoRelacionDeTrabajoPrioridad { get; set; }
        public int HorasTrabajo { get; set; }
        public Prioridad HorasTrabajoPrioridad { get; set; }
    }
    public enum Prioridad
    {
        Baja,
        Normal,
        Excluyente
    }
}
