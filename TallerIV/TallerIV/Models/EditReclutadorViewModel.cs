using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using TallerIV.Dominio;

namespace TallerIV.Models
{
    public class EditReclutadorViewModel
    {
        public string Id { get; set; }
        public string Email { get; set; }

        [Required]
        [Display(Name = "Nombre")]
        public string Nombre { get; set; }

        [Required]
        [Display(Name = "Apellido")]
        public string Apellido { get; set; }

        [Display(Name = "Fecha de nacimiento")]
        public DateTime? FechaDeNacimiento { get; set; }
    }
}