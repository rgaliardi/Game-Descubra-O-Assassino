using Services.Data.Models;
using System.Linq;
using System.Web.Mvc;
using static Services.Data.Models.Suspects;

namespace Services.Business
{
    public class SuspectsBusiness : Singleton<SuspectsBusiness>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Singleton"/> class.
        /// </summary>
        private SuspectsBusiness() { }

        public Suspects Find(int code)
        {
            var _entity = Connection.Default.Suspects.Where(o => o.Code == code).FirstOrDefault();
            return _entity;
        }

        public SelectList Dropdown(object select = null)
        {
            var _list = Connection.Transaction.List<Suspects>().
                Select(s => new SuspectsDropdown
                {
                    value = s.Code,
                    text = s.Name
                }).ToList().AsQueryable();

            return new SelectList(_list, "value", "text", select);
        }
    }
}
