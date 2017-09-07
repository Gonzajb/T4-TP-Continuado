﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Objects.DataClasses;
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
        public void UpdateEntity(T entity) {
            db.Entry(entity).State = EntityState.Modified;
            db.SaveChanges();
        }
    }
}
