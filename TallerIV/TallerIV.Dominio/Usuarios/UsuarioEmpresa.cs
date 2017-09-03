using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TallerIV.Dominio
{
    public class UsuarioEmpresa: Usuario
    {
        public string Cuit { get; set; }
        public string RazonSocial { get; set; }
        public virtual List<Aviso> Avisos { get; set; }
        public virtual List <UsuarioReclutador>Reclutadores { get; set; }
    }
}
