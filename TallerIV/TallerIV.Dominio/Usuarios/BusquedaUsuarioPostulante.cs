using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TallerIV.Dominio.Usuarios
{
    public class BusquedaUsuarioPostulante
    {
        public long Id { get; set; }
        [Display(Name = "Sueldo mínimo")]
        [Required]
        public double SueldoMinimo { get; set; }
        [Required]
        public Prioridad SueldoMinimoPrioridad { get; set; }
        [Display(Name = "Tipo de relación de trabajo")]
        [Required]
        public TipoRelacionDeTrabajo TipoRelacionDeTrabajo { get; set; }
        [Required]
        public Prioridad TipoRelacionDeTrabajoPrioridad { get; set; }
        [Display(Name = "Horas de trabajo")]
        [Required]
        public int HorasTrabajo { get; set; }
        [Required]
        public Prioridad HorasTrabajoPrioridad { get; set; }
    }
    public enum Prioridad
    {
        [Display(Name = "Prioridad baja")]
        Baja = 1,
        [Display(Name = "Prioridad normal")]
        Normal = 2,
        [Display(Name = "Prioridad excluyente")]
        Excluyente = 3,
    }
}
