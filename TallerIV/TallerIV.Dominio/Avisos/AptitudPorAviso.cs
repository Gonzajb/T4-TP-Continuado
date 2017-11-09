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
        public AptitudPorAviso() { }
        public AptitudPorAviso(long AvisoId, long AptitudId, Aptitud aptitud, Prioridad p)
        {
            this.Aviso_Id = AvisoId;
            this.Aptitud_Id = AptitudId;
            this.Aptitud = aptitud;
            this.Prioridad = p;

        }
        public long Aviso_Id { get; set; }
        public long Aptitud_Id { get; set; }
        public virtual Aptitud Aptitud { get; set; }
        public Prioridad Prioridad { get; set; }
    }
}
