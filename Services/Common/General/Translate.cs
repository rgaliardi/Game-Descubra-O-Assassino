using System.Web.Memory;

namespace System.Web
{
    public partial class Common
    {
        public static void Languages(string culture)
        {
            string _language = null;

            switch (culture)
            {
                case "pt-BR":
                case "pt":
                    _language = "pt";
                    break;
                case "en-US":
                case "en-GB":
                    _language = "en";
                    break;
                default:
                    break;
            }
            InSession.Entity<string>("Language", _language);
        }


        public static string Previous_Pagination
        {
            get
            {
                string _value;

                switch (Common.Language)
                {
                    case "en":
                        _value = "Previous";
                        break;
                    default:
                        _value = "Anterior";
                        break;
                }
                return _value;
            }
        }

        public static string Display_Pagination
        {
            get
            {
                string _value;

                switch (Common.Language)
                {
                    case "en":
                        _value = "Page {0} to {1} (Total {2} entries)";
                        break;
                    default:
                        _value = "Página {0} de {1} (Total {2} registros)";
                        break;
                }
                return _value;
            }
        }
    }
}
