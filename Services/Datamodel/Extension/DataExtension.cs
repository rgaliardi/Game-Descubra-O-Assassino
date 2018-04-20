using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace Services.Model
{
    /// <summary>
    /// Operações de Extensão
    /// </summary>
    public partial class DataLoad<TContext> : IDisposable where TContext : DbContext, new()
    {
        public class Entity
        {
            public int Id { get; set; }
        }

        /// <summary>
        /// Retorna os itens da classe pela PK
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual T Find<T>(object id) where T : class
        {
            Configuration(true);
            return dataContext.Set<T>().Find(id);
        }

        public virtual T Find<T>(object id, object tp) where T : class
        {
            Configuration(true);
            return dataContext.Set<T>().Find(id, tp);
        }

        public virtual T Find<T>(object id, object tp, object cs) where T : class
        {
            Configuration(true);
            return dataContext.Set<T>().Find(id, tp, cs);
        }

        public virtual T Find<T>(object id, object tp, object cs, object cd) where T : class
        {
            Configuration(true);
            return dataContext.Set<T>().Find(id, tp, cs, cd);
        }

        public virtual IList<T> List<T>() where T : class
        {
            Configuration(true);
            return dataContext.Set<T>().ToList();
        }

        public virtual IEnumerable<T> Query<T>() where T : class
        {
            Configuration(false);
            return dataContext.Set<T>().ToList().AsQueryable();
        }

        public virtual IEnumerable<T> Get<T>(Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, string includeProperties = "") where T : class
        {
            Configuration(true);
            IQueryable<T> _query = dataContext.Set<T>();

            if (filter != null)
            {
                _query = _query.Where(filter);
            }

            foreach (var includeProperty in includeProperties.Split
                (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                _query = _query.Include(includeProperty);
            }

            if (orderBy != null)
            {
                return orderBy(_query).ToList();
            }
            else
            {
                return _query.ToList();
            }
        }

        /// <summary>
        /// Retorna um número válido para uso temporário de ID da entidade
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public int NewID<T>() where T : class
        {
            DbSet<T> _set = dataContext.Set<T>();

            if (_set == null) throw new ArgumentNullException("Identity with TEntity null.");

            int ret = (_set.Local.Count() * -1);

            if (ret < 0)
            {
                T find = null;

                do
                {
                    --ret;
                    find = Find<T>(ret);
                } while (find != null);
            } else
            {
                ret = -1;
            }
            return ret;
        }

        public T Get<T>(object id) where T : class
        {
            Configuration(true);
            DbSet<T> _set = dataContext.Set<T>();

            if (_set == null) throw new ArgumentNullException("Identity with TEntity null.");

            var _entity = ((IList<T>)_set).SingleOrDefault();
            if (_entity == null) throw new ArgumentNullException("This item doesn't exist anymore");
            return _entity;
        }

        public ObservableCollection<T> Reference<T>(IEnumerable<T> TEntity, string REntity) where T : class
        {
            Configuration(true);
            DbSet<T> _set = dataContext.Set<T>();
            
            foreach(var item in _set.Local)
            {
                dataContext.Entry(item).Reference(REntity).Load();
            }
            return _set.Local;
        }

        public ObservableCollection<T> Reference<T>(IEnumerable<T> TEntity, string[] REntity) where T : class
        {
            Configuration(true);
            DbSet<T> _set = dataContext.Set<T>();

            foreach(var refe in REntity)
            {
                foreach (var item in _set.Local)
                {
                    dataContext.Entry(item).Reference(refe).Load();
                }
            }
            return _set.Local;
        }

        public T Exists<T>(T TEntity) where T : class
        {
            var _objectset = objectContext.CreateObjectSet<T>();
            var _entityKey = objectContext.CreateEntityKey(_objectset.EntitySet.Name, TEntity);

            DbSet<T> set = dataContext.Set<T>();
            var keys = (from x in _entityKey.EntityKeyValues
                        select x.Value).ToArray();
            return set.Find(keys);
        }
    }
}