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
        /// <summary>  
        ///  Esta función agrega a la base de datos el listado de entidades de tipo genérico recibido por parámetro.
        /// </summary>  
        public void AddEntityRange(List<T> entities)
        {
            dbSet.AddRange(entities);
            db.SaveChanges();
        }
        /// <summary>  
        ///  Esta función elimina de la base de datos la entidad de tipo genérico recibida por parámetro.
        /// </summary>  
        public void RemoveEntity(T entity)
        {
            dbSet.Remove(entity);
            db.SaveChanges();
        }
        /// <summary>  
        ///  Esta función retorna una query de base de datos que selecciona todas las entidades del tipo genérico.
        /// </summary>  
        public IQueryable<T> GetAll()
        {
            return dbSet;
        }
        /// <summary>  
        ///  Esta función retorna la entidad genérica obtenida de la base de datos relacionada al id que recibe por parámetro.
        /// </summary>  
        public T GetById(long id) {
            return dbSet.Find(id);
        }
        /// <summary>  
        ///  Esta función actualiza en la base de datos la entidad que recibe por parámetro del tipo genérico.
        /// </summary>  
        public void UpdateEntity(T entity) {
            dbSet.AddOrUpdate(entity);
            db.SaveChanges();
        }
        public TallerIVDbContext GetDbContext()
        {
            return db;
        }
    }
}
