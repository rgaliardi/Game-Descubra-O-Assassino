using Services.Data.Models;
using System.Linq;
using System.Web.Mvc;
using static Services.Data.Models.Suspects;
using static Services.Data.Models.Weapons;

namespace Services.Business
{
    public class WeaponsBusiness : Singleton<WeaponsBusiness>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Singleton"/> class.
        /// </summary>
        private WeaponsBusiness() { }

        public Weapons Find(int code)
        {
            var _entity = Connection.Default.Weapons.Where(o => o.Code == code).FirstOrDefault();
            return _entity;
        }

        public SelectList Dropdown(object select = null)
        {
            var _list = Connection.Transaction.List<Weapons>().
                Select(s => new WeaponsDropdown
                {
                    value = s.Code,
                    text = s.Name
                }).ToList().AsQueryable();

            return new SelectList(_list, "value", "text", select);
        }
    }
}
