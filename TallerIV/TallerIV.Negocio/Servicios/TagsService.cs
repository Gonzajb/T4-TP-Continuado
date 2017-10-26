using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TallerIV.Datos;
using TallerIV.Dominio;

namespace TallerIV.Negocio.Servicios
{
    public class TagsService : BaseService<Aptitud>
    {
        public TagsService(TallerIVDbContext db) : base(db) {
        }
        public TagsService() : base() { }
        public List<Aptitud> GetTagsByString(string tagsString) {
            //Resuelve una lista de tags separados por comas (",") agregando a la base de datos los inexistentes.
            List<Aptitud> tagsAAgregar = new List<Aptitud>();
            List<Aptitud> tagsADevolver = new List<Aptitud>();
            string[] tagsAProcesar = tagsString.Split(',');

            tagsADevolver.AddRange(this.GetAll().Where(x => tagsAProcesar.Any(t => t.ToUpper() == x.Titulo.ToUpper())).ToList());
            tagsAAgregar.AddRange(tagsAProcesar.Where(t => !tagsADevolver.Any(x => x.Titulo.ToUpper() == t.ToUpper())).Select(t => new Aptitud { Titulo = t }).ToList());

            repository.AddEntityRange(tagsAAgregar);
            tagsADevolver.AddRange(tagsAAgregar);

            return tagsADevolver;
        }
        public List<Aptitud> GetTagsByTitulo(string term) {
            return this.GetAll().Where(x => x.Titulo.ToUpper().Contains(term.ToUpper())).ToList();
        }
    }
}
