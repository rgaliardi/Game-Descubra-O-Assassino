using System;
using System.Data.Entity;
using System.Data.Entity.Migrations;

namespace System.Web.Mvc
{
    /// <summary>
    /// Operações de Insert, Update and Delete
    /// </summary>
    public partial class DataLoad<TContext> : IDisposable where TContext : DbContext, new()
    {
        /// <summary>
        /// Inclui um objeto
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="TEntity"></param>
        /// <returns></returns>
        public virtual void Insert<T>(T TEntity) where T : class
        {
            if (TEntity == null) throw new ArgumentNullException("Insert with TEntity null.");

            try
            {
                dataContext.Set<T>().Add(TEntity);
                dataContext.Entry(TEntity).State = EntityState.Added;
            }
            catch {
                Update<T>(TEntity);
            }
        }

        /// <summary>
        /// Atualiza um objeto
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="TEntity"></param>
        /// <returns></returns>
        public virtual void Update<T>(T TEntity) where T : class
        {
            if (TEntity == null) throw new ArgumentNullException("Update with TEntity null.");

            T _entity = Exists<T>(TEntity);
            if (_entity == null)
            {
                this.Insert<T>(TEntity);
            }
            else if (dataContext.Entry(_entity).State == EntityState.Added)
            {
                dataContext.Entry(_entity).State = EntityState.Detached;
                this.Insert<T>(TEntity);
            }
            else
            {
                dataContext.Entry(_entity).CurrentValues.SetValues(TEntity);
                dataContext.Entry(_entity).State = EntityState.Modified;
            }
        }

        /// <summary>
        /// Excluí um objeto pela entidade
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="TEntity"></param>
        public virtual void Delete<T>(T TEntity) where T : class
        {
            if (TEntity == null) throw new ArgumentNullException("Delete with TEntity null.");

            T _entity = Exists<T>(TEntity);
            dataContext.Entry(_entity).State = EntityState.Detached;
            _entity = Exists<T>(TEntity);

            if (_entity != null)
                dataContext.Entry(_entity).State = EntityState.Deleted;
        }

        private void ApplyStateChanges<T>(T TEntity) where T : class
        {
            DbSet<T> _dbSet = dataContext.Set<T>();

            if (dataContext.Entry(TEntity).State == EntityState.Added || dataContext.Entry(TEntity).State == EntityState.Modified)
            {
                _dbSet.AddOrUpdate(TEntity);
            }
            else if (dataContext.Entry(TEntity).State == EntityState.Deleted)
            {
                _dbSet.Remove(TEntity);
            }
        }
    }
}