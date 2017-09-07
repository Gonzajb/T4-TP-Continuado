using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TallerIV.Dominio
{
    public class UsuarioEmpresa : Usuario
    {
        public UsuarioEmpresa() { }
        public UsuarioEmpresa(string cuit, string razonSocial, DateTime fechaRegistro, string email, string userName) : base(fechaRegistro, email, userName) {
            this.Cuit = cuit;
            this.RazonSocial = razonSocial;
        }
        public string Cuit { get; set; }
        public string RazonSocial { get; set; }
        public virtual List<Aviso> Avisos { get; set; }
        public virtual List <UsuarioReclutador>Reclutadores { get; set; }
    }
}
