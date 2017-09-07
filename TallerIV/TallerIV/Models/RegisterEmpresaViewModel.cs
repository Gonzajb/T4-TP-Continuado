using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TallerIV.Models
{
    public class RegisterEmpresaViewModel : RegisterViewModel
    {
        [Required]
        public string Cuit { get; set; }
        [Required]
        public string RazonSocial { get; set; }
    }
}