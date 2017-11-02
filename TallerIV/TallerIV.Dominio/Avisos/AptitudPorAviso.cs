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
        public AptitudPorAviso(long Aviso_Id, long Aptitud_Id, Aptitud Aptitud, Prioridad Prioridad)
        {
            this.Aviso_Id = Aviso_Id;
            this.Aptitud_Id = Aptitud_Id;
            this.Aptitud = Aptitud;
            this.Prioridad = Prioridad; 
        }
        public long Aviso_Id { get; set; }
        public long Aptitud_Id { get; set; }
        public virtual Aptitud Aptitud { get; set; }
        public Prioridad Prioridad { get; set; }
    }
}
