using Services.Data.Models;
using System.Linq;
using System.Web.Mvc;
using static Services.Data.Models.Places;
using static Services.Data.Models.Suspects;

namespace Services.Business
{
    public class PlacesBusiness : Singleton<PlacesBusiness>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Singleton"/> class.
        /// </summary>
        private PlacesBusiness() { }

        public Places Find(int code)
        {
            var _entity = Connection.Default.Places.Where(o => o.Code == code).FirstOrDefault();
            return _entity;
        }

        public SelectList Dropdown(object select = null)
        {
            var _list = Connection.Transaction.List<Places>().
                Select(s => new PlacesDropdown
                {
                    value = s.Code,
                    text = s.Name
                }).ToList().AsQueryable();

            return new SelectList(_list, "value", "text", select);
        }
    }
}
