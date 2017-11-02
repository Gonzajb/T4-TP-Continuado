using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TallerIV.Dominio
{
    public class Aptitud
    {
        public Aptitud() { }
        public Aptitud (long Id, string Titulo)
        {
            this.Id = Id;
            this.Titulo = Titulo;
        }
        public long Id { get; set; }
        public string Titulo { get; set; }
    }
}
