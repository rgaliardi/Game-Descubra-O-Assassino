using System.Text;
using System.Web.Memory;
using System.Web.Mvc;
using System.Web.Routing;

namespace System.Web
{
    public static partial class Common
    {
        public static object Application
        {
            get { return InSession.Entity<object>("Application"); }
        }

        public static object Operations
        {
            get { return InSession.Entity<object>("Operations"); }
        }

        public static object Menu
        {
            get { return InSession.Entity<object>("Menu"); }
        }

        public static string Ambient
        {
            get { return InSession.Entity<string>("Ambient"); }
        }

        public static string Enviroment
        {
            get { return InSession.Entity<string>("Enviroment"); }
        }

        public static string Modulo
        {
            get { return InSession.Entity<string>("strcod_IntranetModulo"); }
        }

        public static short TipoPedido
        {
            get {
                short _intcod_TipoProdutoModulo = 0;

                if (TipoProduto.ToShortNullSafe() == 6)
                    _intcod_TipoProdutoModulo = 2;
                else if (TipoProduto.ToShortNullSafe() == 7)
                    _intcod_TipoProdutoModulo = 1;

                return _intcod_TipoProdutoModulo; }
        }

        public static short TipoProduto
        {
            get { return InSession.Entity<string>("intcod_TipoProdutoModulo").ToShortNullSafe(); }
        }

        public static string UserName
        {
            get { return InSession.Entity<string>("intcod_LoginModulo"); }
        }

        public static string SubSeccao
        {
            get { return InSession.Entity<string>("intcod_SubseccaoModulo"); }
        }

        public static string Departmento
        {
            get { return InSession.Entity<string>("intcod_DepartamentoModulo"); }
        }

        public static string Fornecedor
        {
            get { return InSession.Entity<string>("Fornecedor"); }
            set {  InSession.Entity<string>("Fornecedor", value); }
        }

        public static string Language
        {
            get { return InSession.Entity<string>("Language"); }
        }

        public static string CustomRenderYesOrNo(object value)
        {
            string _true;
            string _false;
            string _value;

            switch (Language)
            {
                case "en":
                    _true = "Yes";
                    _false = "No";
                    break;
                default:
                    _true = "Sim";
                    _false = "Não";
                    break;
            }

            switch (Extensions.NullToString(value))
            {
                case "S":
                case "True":
                case "1":
                    _value = _true;
                    break;
                default:
                    _value = _false;
                    break;
            }
            return _value;
        }

        public static string Next_Pagination
        {
            get
            {
                string _value;

                switch (Language)
                {
                    case "en":
                        _value = "Next";
                        break;
                    default:
                        _value = "Próximo";
                        break;
                }
                return _value;
            }
        }

        public static string View
        {
            get { return InSession.Entity<string>("View"); }
        }

        public static MvcHtmlString CustomRenderYesOrNo(this HtmlHelper helper, object value)
        {
            string _value = CustomRenderYesOrNo(value);

            return MvcHtmlString.Create(_value);
        }

        public static void Error(RequestContext requestContext, string message)
        {
            requestContext.HttpContext.Response.Clear();
            requestContext.HttpContext.Response.Write(Common.Notification(message));
            requestContext.HttpContext.Response.End();
        }

        private static string Notification(string message = "Usuário sem permissão de acesso!")
        {
            StringBuilder _html = new StringBuilder();

            _html.AppendLine("<link href=\"~/content/site.css\" rel=\"stylesheet\" />");
            _html.AppendLine("<script type=\"text/javascript\" src=\"~/scripts/jquery-1.11.3.min.js\"></script>");
            _html.AppendLine("<script type=\"text/javascript\" src=\"~/scripts/button-1.0.0.js\"></script>");
            _html.AppendLine("<body>");
            _html.AppendLine("<div class='bb-alert alert alert-success' style='display:none;'></div>");
            _html.AppendLine("</body>");
            _html.AppendLine("<script>Notify.show('" + message + "');</script>");
            _html.AppendLine("</html>");

            return _html.ToString();
        }
    }
}
