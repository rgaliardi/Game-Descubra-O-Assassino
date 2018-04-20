using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace System.Web
{
    public static class StringExtensions
    {
        public static IEnumerable<T> PadRight<T>(this IEnumerable<T> source, int length)
        {
            int _i = 0;
            // use "Take" in case "length" is smaller than the source's length.
            foreach (var item in source.Take(length))
            {
                yield return item;
                _i++;
            }
            for (; _i < length; _i++)
                yield return default(T);
        }

        public static string ToTitleCase(this string value)
        {
            return CultureInfo.CurrentCulture.TextInfo.ToTitleCase(value.ToLower().Trim());
        }

        public static string ToLowerCase(this string value)
        {
            return CultureInfo.CurrentCulture.TextInfo.ToLower(value);
        }

        public static string ToEmailSafe(this string value)
        {
            return CultureInfo.CurrentCulture.TextInfo.ToLower(value.Replace(",", ";").Replace(";", "; ").Trim());
        }

        public static bool ToBoolNullSafe(this object value)
        {
            bool _ret = false;
            bool.TryParse(value.ToStringNullSafe(), out _ret);
            return _ret;
        }

        public static short ToShortNullSafe(this object value)
        {
            short _ret = 0;
            short.TryParse(value.ToStringNullSafe().Replace(".", ","), out _ret);
            return _ret;
        }

        public static short ToShortSafe(this object value)
        {
            short _ret = -1;

            if (value != null)
                short.TryParse(value.ToStringNullSafe().Replace(".", ","), out _ret);
            return _ret;
        }

        public static int ToIntNullSafe(this object value)
        {
            int _ret = 0;
            int.TryParse(value.ToStringNullSafe().Replace(".", ","), out _ret);
            return _ret;
        }

        public static int ToIntSafe(this object value)
        {
            int _ret = -1;

            if (value != null)
                int.TryParse(value.ToStringNullSafe().Replace(".", ","), out _ret);
            return _ret;
        }

        public static long ToLongNullSafe(this object value)
        {
            long _ret = 0;
            long.TryParse(value.ToStringNullSafe().Replace(".", ","), out _ret);
            return _ret;
        }

        public static decimal ToDecimalNullSafe(this object value)
        {
            decimal _ret = 0;
            decimal.TryParse(value.ToStringNullSafe().Replace(".", ","), out _ret);
            return _ret;
        }

        public static string ToStringNumer(this object value, int len, char chr)
        {
            return (value ?? string.Empty).ToString().Trim().PadLeft(len, chr);
        }

        public static string ToStringPad(this object value)
        {
            return (value ?? string.Empty).ToString().Trim().PadRight(6);
        }

        public static string ToStringNoCrLf(this object value)
        {
            var _result = (value ?? string.Empty).ToString().Trim();

            if (String.IsNullOrEmpty(_result))
            {
                return _result;
            }
            string _lineSeparator = ((char)0x2028).ToString();
            string _paragraphSeparator = ((char)0x2029).ToString();
            return _result.Replace("\r\n", string.Empty).Replace("\n", string.Empty).Replace("\r", string.Empty).Replace(_lineSeparator, string.Empty).Replace(_paragraphSeparator, string.Empty);
        }

        public static string ToLowerNullSafe(this object value)
        {
            return (value ?? string.Empty).ToString().Trim().ToLower();
        }

        public static string ToUpperNullSafe(this object value)
        {
            return (value ?? string.Empty).ToString().Trim().ToUpper();
        }

        public static string ToStringNullSafe(this object value)
        {
            return (value ?? string.Empty).ToString().Trim();
        }

        public static string ToCurrencySafe(this object value)
        {
            return value == null ? "null" : string.Format("{0:C}", value);
        }

        public static string ToStringPad(this object value, Int16 pad)
        {
            return (value ?? string.Empty).ToString().Trim().PadRight(pad);
        }        

        public static bool Contains(this string source, string toCheck, StringComparison comp)
        {
            return source.IndexOf(toCheck, comp) >= 0;
        }

        public static bool Boolean(this string source)
        {
            switch (source)
            {
                case "S":
                case "Y":
                case "1":
                    return true;
                default:
                    return false;
            }
        }

        public static bool IsNumeric(this object value) {
            long _ret;
            bool _isNum = long.TryParse(value.ToStringNullSafe(), out _ret); //c is your variable
            return _isNum;
        }

        public static bool Boolean(this Func<bool> source)
        {
            switch (source.ToString())
            {
                case "S":
                case "Y":
                case "1":
                    return true;
                default:
                    return false;
            }
        }

        public static Predicate<T> ToPredicate<T>(this Func<T, bool> source)
        {
            return x => source(x);
        }
    }
}
