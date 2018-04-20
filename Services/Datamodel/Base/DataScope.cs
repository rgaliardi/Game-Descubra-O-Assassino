using Services.Model.Models;
using System;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Threading;
using System.Web.Memory;

namespace Services.Model
{
    /// <summary>
    /// Operação com controle do Scopo da Transação
    /// </summary>
    /// <typeparam name="TContext"></typeparam>
    public partial class DataLoad<TContext> : IDisposable where TContext : DbContext, new()
    {
        internal TContext dataContext;
        internal int referenceCount;
        internal bool disposed = false;
        internal ObjectContext objectContext;
        internal static LocalDataStoreSlot slot = Thread.AllocateNamedDataSlot("Load");

        #region Initialize

        /// <summary>
        /// Inicialização
        /// </summary>
        /// <param name="dbContext"></param>
        public DataLoad(TContext dataContext)
        {
            this.referenceCount = 0;
            this.dataContext = dataContext;
            this.objectContext = ((IObjectContextAdapter)dataContext).ObjectContext;
            Thread.SetData(slot, this);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="isolation">Define o tipo de cursor de transação quando o Transaction está TRUE</param>
        /// <param name="configuration">TRUE - carregamento lento (carrega todas as entidades agregadas)</param>
        /// <param name="transaction">TRUE - define como cursor de transações</param>
        /// <returns></returns>
        public static DataLoad<TContext> Require()
        {
            return DataLoad<TContext>.Requires(() => { return new TContext(); });
        }

        /// <summary>
        /// Definição e abertura do Scopo padrão Factory
        /// </summary>
        /// <param name="factory"></param>
        /// <returns></returns>
        private static DataLoad<TContext> Requires(Func<TContext> factory)
        {
            DataLoad<TContext> _scope = (DataLoad<TContext>)Thread.GetData(slot);

            try
            {
                if (_scope == null)
                    throw new System.ArgumentException("Connection is disposed!");

                _scope.DbContext.Database.Exists();
            }
            catch
            {
                _scope = new DataLoad<TContext>(factory());
                _scope.DbContext.Configuration.AutoDetectChangesEnabled = false;
                _scope.DbContext.Configuration.EnsureTransactionsForFunctionsAndCommands = false;
                _scope.DbContext.Configuration.LazyLoadingEnabled = true;
                _scope.DbContext.Configuration.ProxyCreationEnabled = true;
                _scope.DbContext.Configuration.UseDatabaseNullSemantics = false;
                _scope.DbContext.Configuration.ValidateOnSaveEnabled = false;
                _scope.referenceCount++;
            }
            return _scope;
        }

        /// <summary>
        /// Descarte
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.WaitForPendingFinalizers();
            GC.SuppressFinalize(this);
            GC.Collect();
        }

        // Protected implementation of Dispose pattern.
        protected virtual void Dispose(bool disposing)
        {
            if (disposed)
                return;

            if (disposing)
            {
                dataContext.Dispose();
                referenceCount--;
                InSession.Scope<DataLoad<Entities_Principal>>("DBLoad", true);
            }
            disposed = true;
        }
        #endregion

        #region Scope

        /// <summary>
        /// Contexto da Aplicação
        /// </summary>
        public TContext DbContext
        {
            get { return dataContext; }
        }
        #endregion
    }
}
