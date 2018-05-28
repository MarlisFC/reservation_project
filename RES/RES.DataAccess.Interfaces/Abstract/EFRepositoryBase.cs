using System;
using System.Linq;
using System.Linq.Expressions;
using System.Data.Entity;
using RES.BusinessLogic.Core.Data;
using RES.DataAccess.Interfaces.Base;

namespace RES.DataAccess.Interfaces.Abstract
{
    public abstract class EFRepositoryBase<TEntity> : IRepositoryBase where TEntity : class
    {
        protected ResEntities Context;
        protected EFRepositoryBase(ResEntities dbCtx)
        {
            Context = dbCtx;
        }

        public virtual TEntity Create()
        {
            return Context.Set<TEntity>().Create();
        }

        public virtual TEntity Create(TEntity entity)
        {
            return Context.Set<TEntity>().Add(entity);
        }

        public virtual TEntity Update(TEntity entity)
        {
            Context.Entry(entity).State = EntityState.Modified;
            return entity;
        }

        public virtual void Delete(long id)
        {
            var item = Context.Set<TEntity>().Find(id);
            Context.Set<TEntity>().Remove(item);
        }

        public virtual void Delete(TEntity entity)
        {
            Context.Set<TEntity>().Remove(entity);
        }

        public virtual void Delete(Expression<Func<TEntity, bool>> where)
        {
            var objects = Context.Set<TEntity>().Where(where).AsEnumerable();
            foreach (var item in objects)
            {
                Context.Set<TEntity>().Remove(item);
            }
        }

        public virtual TEntity FindById(long id)
        {
            return Context.Set<TEntity>().Find(id);
        }

        public virtual TEntity FindOne(Expression<Func<TEntity, bool>> where = null)
        {
            return FindAll(where).FirstOrDefault();
        }

        public IQueryable<T> Set<T>() where T : class
        {
            return Context.Set<T>();
        }

        public virtual IQueryable<TEntity> FindAll(Expression<Func<TEntity, bool>> where = null)
        {
            return null != where ? Context.Set<TEntity>().Where(where) : Context.Set<TEntity>();
        }

        public virtual bool SaveChanges()
        {
            return 0 < Context.SaveChanges();
        }

        #region IDisposable

        public bool CanDispose { get; set; }

        public void Dispose(bool force)
        {
            this.CanDispose = true;
            Dispose();
        }

        public void Dispose()
        {
            if (Context != null && this.CanDispose)
                Context.Dispose();
        }

        #endregion
    }
}
