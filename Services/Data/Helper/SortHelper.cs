using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Web;

namespace Services.Data.Helper
{
    public static class SortHelper
    {
        public static IEnumerable<T> Search<T>(object source, string searchString, out int total) where T : class
        {
            IEnumerable<T> _entities = (IEnumerable<T>)source;

            if (!string.IsNullOrEmpty(searchString))
            {
                //var stringProperties = typeof(T).GetProperties().Where(prop => prop.PropertyType == searchString.GetType() && !prop.Name.StartsWith("cod_"));
                //var stringProperties = typeof(T).GetProperties().Where(prop => prop.PropertyType == searchString.GetType());
                var _stringProperties = typeof(T).GetProperties();

                Regex _reNum = new Regex(@"^\d+$");
                if (_reNum.Match(searchString.ToString()).Success)
                {
                    _entities = _entities.Where(c => _stringProperties.Any(prop => ((prop.GetValue(c, null) == null) ? "" : prop.GetValue(c, null).ToString()) == searchString.ToString()));
                }
                else
                {
                    _entities = _entities.Where(c => _stringProperties.Any(prop => ((prop.GetValue(c, null) == null) ? "" : prop.GetValue(c, null).ToString().ToLower()).Contains(searchString.ToString().ToLower())));
                }
            }
            total = _entities.Count();
            return _entities;
        }

        public static IEnumerable<T> Sort<T>(object source, string sortBy, string direction) where T : class
        {
            if (!string.IsNullOrEmpty(sortBy) && !string.IsNullOrEmpty(direction))
            {
                if (direction.Trim().ToLower() == "asc")
                {
                    source = OrderBy<T>(source, sortBy);
                }
                else
                {
                    source = OrderByDescending<T>(source, sortBy);
                }
            }
            return (IEnumerable<T>)source;
        }

        public static IEnumerable<T> Page<T>(object source, int? page, int? limit) where T : class
        {
            IEnumerable<T> _entities = (IEnumerable<T>)source;

            if (page.HasValue && limit.HasValue)
            {
                int _start = (page.Value - 1) * limit.Value;
                _entities = _entities.Skip(_start).Take(limit.Value);
            }
            return _entities;
        }

        static IOrderedQueryable<T> OrderBy<T>(object source, string property) where T : class
        {
            return ApplyOrder<T>(source, property, "OrderBy");
        }
        static IOrderedQueryable<T> OrderByDescending<T>(object source, string property) where T : class
        {
            return ApplyOrder<T>(source, property, "OrderByDescending");
        }
        static IOrderedQueryable<T> ThenBy<T>(object source, string property) where T : class
        {
            return ApplyOrder<T>(source, property, "ThenBy");
        }
        static IOrderedQueryable<T> ThenByDescending<T>(object source, string property) where T : class
        {
            return ApplyOrder<T>(source, property, "ThenByDescending");
        }
        static IOrderedQueryable<T> ApplyOrder<T>(object source, string property, string methodName) where T : class
        {
            string[] _props = property.Split('.');
            Type _type = typeof(T);
            object _result = null;

            ParameterExpression _arg = Expression.Parameter(_type, "x");
            Expression _expr = _arg;
            foreach (string prop in _props)
            {
                // use reflection (not ComponentModel) to mirror LINQ
                PropertyInfo _pi = _type.GetProperty(prop);
                _expr = Expression.Property(_expr, _pi);
                _type = _pi.PropertyType;
            }
            Type _delegateType = typeof(Func<,>).MakeGenericType(typeof(T), _type);
            LambdaExpression _lambda = Expression.Lambda(_delegateType, _expr, _arg);

            try
            {
                _result = typeof(Queryable).GetMethods().Single(
                        method => method.Name == methodName
                                && method.IsGenericMethodDefinition
                                && method.GetGenericArguments().Length == 2
                                && method.GetParameters().Length == 2)
                        .MakeGenericMethod(typeof(T), _type)
                        .Invoke(null, new object[] { source, _lambda });
            }catch (Exception ex)
            {
                Common.LogError(ex);
            }
            return (IOrderedQueryable<T>)_result;
        }
    }
}