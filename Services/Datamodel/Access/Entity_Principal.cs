using System.Web.Memory;

namespace Services.Model.Models
{
    public static class Corporativo
    {
        public static Entities_Principal Entities
        {
            get { return new Entities_Principal(); }
        }

        /// <summary>
        /// Context utilizado para conexão e operação com banco de dados
        /// </summary>
        public static DataLoad<Entities_Principal> Load
        {
            get
            {
                var _context = InSession.Scope<DataLoad<Entities_Principal>>("DBLoad");

                if (_context == null)
                    _context = InSession.Scope<DataLoad<Entities_Principal>>("DBLoad", DataLoad<Entities_Principal>.Require());

                return _context;
            }
        }
    }
}