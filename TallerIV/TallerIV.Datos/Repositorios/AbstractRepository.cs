using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Objects.DataClasses;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TallerIV.Datos.Repositorios
{
    public abstract class AbstractRepository<T> where T : EntityObject
    {
        DbSet<T> dbSet;
        public AbstractRepository(){
            TallerIVContext db = new TallerIVContext();
            dbSet = db.Set<T>();
        }
        public void AddEntity(T entity) {
            dbSet.Add(entity);
        }
        public void RemoveEntity(T entity)
        {
            dbSet.Remove(entity);
        }
        public IQueryable<T> GetAll()
        {
            return dbSet;
        }
        public T GetById(long id) {
            return dbSet.Find(id);
        }
    }
}
