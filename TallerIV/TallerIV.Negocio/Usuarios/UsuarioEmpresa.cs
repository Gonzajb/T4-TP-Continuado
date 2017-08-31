using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TallerIV.Negocio
{
    public class UsuarioEmpresa: Usuario
    {
        public string Cuit { get; set; }
        public string RazonSocial { get; set; }
        public List<Aviso> Avisos { get; set; }
        public List <UsuarioReclutador>Reclutadores { get; set; }
    }
}
