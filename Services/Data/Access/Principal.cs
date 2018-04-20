using System;
using System.Web.Memory;
using Services.Data.Models;

namespace System.Web.Mvc
{
    public static class Connection
    {
        /// <summary>
        /// Context utilizado como padrão sem transação no banco de dados.
        /// </summary>
        public static Entities_Principal Default
        {
            get { return new Entities_Principal(); }
        }

        /// <summary>
        /// Context utilizado com transação no banco de dados.
        /// </summary>
        public static DataLoad<Entities_Principal> Transaction
        {
            get
            {
                var _context = InSession.Scope<DataLoad<Entities_Principal>>("DBLoad");

                if (_context == null)
                    _context = InSession.Scope<DataLoad<Entities_Principal>>("DBLoad", DataLoad<Entities_Principal>.Require());

                return _context;
            }
        }

        /// <summary>
        /// Context utilizado com transação no banco de dados.
        /// </summary>
        public static void Clean(object obj)
        {
            InSession.Scope<DataLoad<Entities_Principal>>("DBLoad", true);
            GC.WaitForPendingFinalizers();
            GC.SuppressFinalize(obj);
            GC.Collect();
        }
    }
}