using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TallerIV.Dominio.Usuarios;

namespace TallerIV.Dominio.Avisos
{
    public class AptitudPorAviso
    {
        public long Aviso_Id { get; set; }
        public long Aptitud_Id { get; set; }
        public virtual Aptitud Aptitud { get; set; }
        public Prioridad Prioridad { get; set; }
    }
}
