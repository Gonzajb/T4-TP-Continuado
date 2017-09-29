using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects.DataClasses;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TallerIV.Datos;
using TallerIV.Datos.Repositorios;

namespace TallerIV.Negocio.Servicios
{
    public class BaseService<T> where T : class
    {
        protected BaseRepository<T> repository;
        public BaseService(BaseRepository<T> repository)
        {
            this.repository = repository;
        }
        public BaseService(TallerIVDbContext db)
        {
            this.repository = new BaseRepository<T>(db);
        }
        public BaseService() {
            this.repository = new BaseRepository<T>();
        }
        public void AddEntity(T entity)
        {
            this.repository.AddEntity(entity);
        }
        public void RemoveEntity(T entity)
        {
            this.repository.RemoveEntity(entity);
        }
        public IQueryable<T> GetAll()
        {
            return this.repository.GetAll();
        }
        public T GetById(long id)
        {
            return this.repository.GetById(id);
        }
        public void UpdateEntity(T entity) {
            this.repository.UpdateEntity(entity);
        }
    }
}
