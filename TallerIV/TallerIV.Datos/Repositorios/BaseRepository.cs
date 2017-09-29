using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Objects.DataClasses;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TallerIV.Datos.Repositorios
{
    public class BaseRepository<T> where T : class
    {
        protected DbSet<T> dbSet;
        protected TallerIVDbContext db;
        public BaseRepository(){
            this.db = TallerIVDbContext.Create();
            dbSet = db.Set<T>();
        }
        public BaseRepository(TallerIVDbContext db)
        {
            this.db = db;
            dbSet = db.Set<T>();
        }
        public void AddEntity(T entity) {
            dbSet.Add(entity);
            db.SaveChanges();
        }
        public void AddEntityRange(List<T> entities)
        {
            dbSet.AddRange(entities);
            db.SaveChanges();
        }
        public void RemoveEntity(T entity)
        {
            dbSet.Remove(entity);
            db.SaveChanges();
        }
        public IQueryable<T> GetAll()
        {
            return dbSet;
        }
        public T GetById(long id) {
            return dbSet.Find(id);
        }
        public void UpdateEntity(T entity) {
            dbSet.AddOrUpdate(entity);
            //db.Entry(entity).State = EntityState.Modified;
            db.SaveChanges();
        }
    }
}
