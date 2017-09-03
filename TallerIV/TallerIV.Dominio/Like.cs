using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TallerIV.Dominio
{
    public class Like
    {
        public int Id { get; set; }
        public bool LikeEmpleado { get; set; }
        public bool LikeReclutador { get; set; }
        public virtual UsuarioReclutador UsuarioReclutador { get; set; }
        public string UsuarioReclutador_Id { get; set; }
        public virtual UsuarioEmpleado UsuarioEmpleado { get; set; }
        public string UsuarioEmpleado_Id { get; set; }
    }
}
