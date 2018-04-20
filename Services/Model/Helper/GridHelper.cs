using System.Collections.Generic;
using System.Linq;

namespace Services.Model.Helper
{
    public static class GridHelper
    {
        public static IEnumerable<T> GridData<T>(object source, int? page, int? limit, string sortBy, string direction, string searchString, out int total) where T : class
        {
            IList<T> _list = null;

            try
            {
                source = SortHelper.Sort<T>(source, sortBy, direction);
                source = SortHelper.Search<T>(source, searchString, out total);
                source = SortHelper.Page<T>(source, page, limit);
                var _entities = (IEnumerable<T>)source;
                _list = _entities.ToList();
            }
            catch
            {
                total = 0;
            }
            return _list;
        }
    }
}
