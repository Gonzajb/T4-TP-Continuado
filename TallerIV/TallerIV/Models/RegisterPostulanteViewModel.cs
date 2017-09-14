﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TallerIV.Models
{
    public class RegisterPostulanteViewModel : RegisterViewModel
    {
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