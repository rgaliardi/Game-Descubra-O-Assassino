using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;

namespace System.Web.Mvc
{
    public static partial class Extensions
    {
        public static bool ArraysEqual(Array a, Array b)
        {
            IStructuralEquatable se1 = a;
            //Next returns True
            return se1.Equals(b, StructuralComparisons.StructuralEqualityComparer); 
        }

        public static string NullToString(object Value)
        {
            // Value.ToString() allows for Value being DBNull, but will also convert int, double, etc.
            return Value == null ? string.Empty : Value.ToString();
        }

        public static string GetValueAsString(this Action environment)
        {
            // get the field 
            var _field = environment.GetType().GetField(environment.ToString());
            var _customAttributes = _field.GetCustomAttributes(typeof(DescriptionAttribute), false);

            if (_customAttributes.Length > 0)
            {
                return (_customAttributes[0] as DescriptionAttribute).Description;
            }
            else
            {
                return environment.ToString();
            }
        }

        public static HashSet<T> ToHashSet<T>(this IEnumerable<T> source)
        {
            return new HashSet<T>(source);
        }
    }
}