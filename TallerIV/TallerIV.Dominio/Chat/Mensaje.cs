using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TallerIV.Dominio.Chat
{
    public class Mensaje
    {
        public Mensaje() { }
        public Mensaje(string texto, string usuario_id, int encuentro_id) {
            this.Texto = texto;
            this.Usuario_Id = usuario_id;
            this.Encuentro_Id = encuentro_id;
        }
        public long Id { get; set; }
        public string Texto { get; set; }
        public string Usuario_Id { get; set; }
        public virtual UsuarioPersona Usuario { get; set; }
        public int Encuentro_Id { get; set; }
    }
}
