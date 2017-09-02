using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects.DataClasses;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TallerIV.Datos.Repositorios;

namespace TallerIV.Negocio.Servicios
{
    public abstract class AbstractService<T> where T : class
    {
        AbstractRepository<T> repository;
        public AbstractService(AbstractRepository<T> repository)
        {
            this.repository = repository;
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
    }
}
