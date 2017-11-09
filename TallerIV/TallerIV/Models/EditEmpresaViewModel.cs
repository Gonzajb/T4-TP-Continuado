using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using TallerIV.Dominio;

namespace TallerIV.Models
{
    public class EditEmpresaViewModel
    {
        public string Id { get; set; }
        public string Email { get; set; }
        [Required]
        [Display(Name = "Razon Social")]
        public string RazonSocial { get; set; }
        [Required]
        [Display(Name = "Cuit")]
        public string Cuit { get; set; }
        [Display(Name = "Reclutadores")]
        public virtual List<UsuarioReclutador> Reclutadores { get; set; }
        [Display(Name = "Avisos")]
        public virtual List<Aviso> Avisos { get; set; }

}
}