using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
        public string UsuarioEmpresa_Id { get; set; }
        public string UsuarioReclutadorAsignado_Id { get; set; }
    }
}
