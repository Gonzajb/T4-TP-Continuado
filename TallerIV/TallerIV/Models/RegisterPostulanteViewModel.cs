using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using TallerIV.Dominio;

namespace TallerIV.Models
{
    public class RegisterPostulanteViewModel : RegisterViewModel
    {
        public RegisterPostulanteViewModel() {
        }
        public RegisterPostulanteViewModel(string id, string nombre, string apellido, DateTime? fechaDeNacimiento, string tags, string email, string password, string cartaDePresentacion):base(email, password)
        {
            this.Id = id;
            this.Nombre = nombre;
            this.Apellido = apellido;
            this.FechaDeNacimiento = fechaDeNacimiento;
            this.Tags = tags;
        }
        public string Id { get; set; }
        [Required]
        [Display(Name = "Nombre")]
        public string Nombre { get; set; }
        [Required]
        [Display(Name = "Apellido")]
        public string Apellido { get; set; }

        [Display(Name = "Fecha de nacimiento")]
        public DateTime? FechaDeNacimiento { get; set; }
        [Display(Name = "Carta de presentación")]
        public string CartaDePresentacion  { get; set; }
        [Display(Name = "Aptitudes")]
        public string Tags { get; set; }
    }
}