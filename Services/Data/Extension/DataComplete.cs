using System;
using System.Data.Entity;
using System.Web;

namespace System.Web.Mvc
{
    /// <summary>
    /// Operação controle da Transação
    /// </summary>
    /// <typeparam name="TContext"></typeparam>
    public partial class DataLoad<TContext> : IDisposable where TContext : DbContext, new()
    {
        public void Configuration(bool enabled)
        {
            dataContext.Configuration.LazyLoadingEnabled = enabled;
            dataContext.Configuration.ProxyCreationEnabled = enabled;
        }

        /// <summary>
        /// Aplicar as Alterações realizadas e aplica-lás no Database
        /// </summary>
        public bool Commit()
        {
            bool _status = false;

            try
            {
                dataContext.SaveChanges();
                Dispose();
                _status = true;
            }
            catch (Exception ex)
            {
                Common.LogError(ex);
            }
            return _status;
        }

        /// <summary>
        /// Reverter as Alterações realizadas
        /// </summary>
        public void Rollback()
        {
            try
            {
                Dispose();
            }
            catch (Exception ex)
            {
                Common.LogError(ex);
            }
        }
    }
}
