using System.Linq.Expressions;
using System.Text;
using System.Web.Mvc.Html;
using System.Web.Routing;

namespace System.Web.Mvc
{
    public static partial class Extensions
    {
        public static MvcHtmlString Menu2(this HtmlHelper helper, string text, string title, string href = null, object htmlAttributes = null, object iconAttributes = null, bool disabled = false, bool permission = false, bool appNew = false)
        {
            TagBuilder _ibuilder = null;
            TagBuilder _sbuilder = null;
            TagBuilder _builder = new TagBuilder("a");
            TagBuilder _pbuilder = new TagBuilder("span");
            UrlHelper _urlHelper = new UrlHelper(helper.ViewContext.RequestContext);
            string _controller = Extensions.NullToString(helper.ViewContext.RouteData.GetRequiredString("controller"));

            _builder.GenerateId("btn" + (string.IsNullOrEmpty(text) ? title : text));
            _builder.Attributes["title"] = title;
            _builder.Attributes["data-widget"] = "view";
            _builder.Attributes["href"] = "#";
            _builder.Attributes["data-href"] = (href != null ? href : "#");

            if (disabled || permission)
                _builder.Attributes["disabled"] = "disabled";

            if (permission)
                _builder.AddCssClass("text-disable");

            if (appNew)
            {
                _sbuilder = new TagBuilder("span");
                _sbuilder.AddCssClass("badge pull-right bg-green");
                _sbuilder.InnerHtml = "new";
                _builder.InnerHtml = string.Format("{0}{1}", _builder.InnerHtml, _sbuilder.ToString());
            }

            if (iconAttributes != null)
            {
                _ibuilder = new TagBuilder("i");
                _ibuilder.MergeAttributes(new RouteValueDictionary(HtmlHelper.AnonymousObjectToHtmlAttributes(iconAttributes)));
                _builder.InnerHtml = string.Format("{0}{1}", _builder.InnerHtml, _ibuilder.ToString());
            }
            _pbuilder.InnerHtml = text;
            _builder.InnerHtml = string.Format("{0} {1}", _builder.InnerHtml, _pbuilder.ToString());
            _builder.MergeAttributes(new RouteValueDictionary(HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes)));
            return MvcHtmlString.Create(_builder.ToString());

        }

        public static MvcHtmlString Menu(this HtmlHelper helper, string text, string title, string href = null, object htmlAttributes = null, object iconAttributes = null, bool disabled = false, bool permission = false, bool appNew = false)
        {
            TagBuilder _builder = null;
            TagBuilder _ibuilder = null;
            TagBuilder _abuilder = null;
            TagBuilder _sbuilder = null;
            TagBuilder _pbuilder = null;

            _builder = new TagBuilder("a");
            _builder.Attributes["href"] = (href != null ? href : "#");
            _builder.Attributes["title"] = title;

            if (disabled || permission)
            {
                _builder.Attributes["disabled"] = "disabled";
                _builder.AddCssClass("nocursor");
                _builder.Attributes["href"] = "#";
                _builder.Attributes["title"] = "Função indisponível - " + title;
            }

            if (permission)
            {
                _builder.AddCssClass("text-disable");
                _builder.Attributes["title"] = "Acesso não liberado - " + title;
            }

            _pbuilder = new TagBuilder("span");
            _pbuilder.InnerHtml = text;

            if (!_builder.Attributes["href"].Equals("#"))
            {
                _pbuilder.AddCssClass("font");
            }

            if (iconAttributes != null)
            {
                _ibuilder = new TagBuilder("i");
                _ibuilder.MergeAttributes(new RouteValueDictionary(HtmlHelper.AnonymousObjectToHtmlAttributes(iconAttributes)));
                _builder.InnerHtml = string.Format("{0}{1}", _builder.InnerHtml, _ibuilder.ToString());
            }
            _builder.InnerHtml = string.Format("{0}{1}", _builder.InnerHtml, _pbuilder.ToString());

            if (appNew)
            {
                _sbuilder = new TagBuilder("span");
                _sbuilder.AddCssClass("badge pull-right bg-green");
                _sbuilder.InnerHtml = "new";
                _builder.InnerHtml = string.Format("{0}{1}", _builder.InnerHtml, _sbuilder.ToString());
            }

            if (htmlAttributes != null)
            {
                _abuilder = new TagBuilder("i");
                _abuilder.MergeAttributes(new RouteValueDictionary(HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes)));
                _builder.InnerHtml = string.Format("{0}{1}", _builder.InnerHtml, _abuilder.ToString());
            }
            return MvcHtmlString.Create(_builder.ToString());
        }

        public static MvcHtmlString Menu1(this HtmlHelper helper, string text, string title, string href = null, object htmlAttributes = null, object iconAttributes = null, bool disabled = false, bool permission = false, bool appNew = false)
        {
            UrlHelper _urlHelper = new UrlHelper(helper.ViewContext.RequestContext);
            TagBuilder _builder = null;
            TagBuilder _ibuilder = null;
            TagBuilder _abuilder = null;
            TagBuilder _sbuilder = null;
            TagBuilder _pbuilder = null;

            _builder = new TagBuilder("a");
            _builder.Attributes["href"] = (href != null ? href : "#");
            _builder.Attributes["title"] = title;
            _pbuilder = new TagBuilder("span");
            _pbuilder.InnerHtml = text;

            if (!_builder.Attributes["href"].Equals("#"))
            {
                _pbuilder.AddCssClass("font");
            }

            if (iconAttributes != null)
            {
                _ibuilder = new TagBuilder("i");
                _ibuilder.MergeAttributes(new RouteValueDictionary(HtmlHelper.AnonymousObjectToHtmlAttributes(iconAttributes)));
                _builder.InnerHtml = string.Format("{0}{1}", _builder.InnerHtml, _ibuilder.ToString());
            }
            _builder.InnerHtml = string.Format("{0}{1}", _builder.InnerHtml, _pbuilder.ToString());

            if (appNew)
            {
                _sbuilder = new TagBuilder("span");
                _sbuilder.AddCssClass("badge pull-right bg-green");
                _sbuilder.InnerHtml = "new";
                _builder.InnerHtml = string.Format("{0}{1}", _builder.InnerHtml, _sbuilder.ToString());
            }

            if (htmlAttributes != null)
            {
                _abuilder = new TagBuilder("i");
                _abuilder.MergeAttributes(new RouteValueDictionary(HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes)));
                _builder.InnerHtml = string.Format("{0}{1}", _builder.InnerHtml, _abuilder.ToString());
            }
            return MvcHtmlString.Create(_builder.ToString());
        }

        public static MvcHtmlString MenuItem(this HtmlHelper helper, string text, string action, string controller, object route = null, object htmlAttributes = null)
        {
            TagBuilder _libuilder = new TagBuilder("li");
            var _routeData = helper.ViewContext.RouteData;
            var _currentAction = _routeData.GetRequiredString("action");
            var _currentController = _routeData.GetRequiredString("controller");

            if (string.Equals(_currentAction, action, StringComparison.OrdinalIgnoreCase) &&
                string.Equals(_currentController, controller, StringComparison.OrdinalIgnoreCase))
            {
                _libuilder.AddCssClass("active");
            }
            if (route != null)
            {
                _libuilder.InnerHtml = (htmlAttributes != null)
                    ? helper.ActionLink(text, action, controller, route, htmlAttributes).ToHtmlString()
                    : helper.ActionLink(text, action, controller, route).ToHtmlString();
            }
            else
            {
                _libuilder.InnerHtml = (htmlAttributes != null)
                    ? helper.ActionLink(text, action, controller, null, htmlAttributes).ToHtmlString()
                    : helper.ActionLink(text, action, controller).ToHtmlString();
            }
            return MvcHtmlString.Create(_libuilder.ToString());
        }

        public static MvcHtmlString MyDisplayFor(this HtmlHelper helper, string value, string small, object htmlAttributes = null, bool disabled = false)
        {
            TagBuilder _builder = null;
            TagBuilder _pbuilder = null;

            _builder = new TagBuilder("p");
            _builder.InnerHtml = value;
            _pbuilder = new TagBuilder("small");
            _pbuilder.InnerHtml = small;

            if (disabled)
                _builder.Attributes["disabled"] = "disabled";

            _builder.InnerHtml = string.Format("{0} {1}", _builder.InnerHtml, _pbuilder.ToString());
            _builder.MergeAttributes(new RouteValueDictionary(HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes)));
            return MvcHtmlString.Create(_builder.ToString());
        }

        public static MvcHtmlString Button(this HtmlHelper helper, string text, string title, object htmlAttributes = null, object iconAttributes = null, bool disabled = false)
        {
            TagBuilder _ibuilder = null;
            TagBuilder _builder = new TagBuilder("button");
            TagBuilder _pbuilder = new TagBuilder("span");
            UrlHelper _urlHelper = new UrlHelper(helper.ViewContext.RequestContext);

            _builder.GenerateId("btn" + (string.IsNullOrEmpty(title) ? text : title));
            _builder.Attributes["title"] = title;
            _builder.Attributes["type"] = "submit";
            _builder.Attributes["value"] = text;
            _builder.Attributes["name"] = "command";

            if (disabled)
                _builder.Attributes["disabled"] = "disabled";

            if (iconAttributes != null)
            {
                _ibuilder = new TagBuilder("i");
                _ibuilder.MergeAttributes(new RouteValueDictionary(HtmlHelper.AnonymousObjectToHtmlAttributes(iconAttributes)));
                _builder.InnerHtml = string.Format("{0}{1}", _builder.InnerHtml, _ibuilder.ToString());
            }
            _pbuilder.InnerHtml = text;
            _builder.InnerHtml = string.Format("{0} {1}", _builder.InnerHtml, _pbuilder.ToString());
            _builder.MergeAttributes(new RouteValueDictionary(HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes)));
            return MvcHtmlString.Create(_builder.ToString());
        }

        public static MvcHtmlString Button(this HtmlHelper helper, string text, string title, string href, object htmlAttributes = null, object iconAttributes = null, bool disabled = false)
        {
            TagBuilder _ibuilder = null;
            TagBuilder _builder = new TagBuilder("a");
            TagBuilder _pbuilder = new TagBuilder("span");
            UrlHelper _urlHelper = new UrlHelper(helper.ViewContext.RequestContext);

            _builder.GenerateId("btn" + (string.IsNullOrEmpty(text) ? title : text));
            _builder.Attributes["title"] = title;
            _builder.Attributes["href"] = (string.IsNullOrEmpty(href) ? "#" : href);

            if (disabled)
                _builder.Attributes["disabled"] = "disabled";

            if (iconAttributes != null)
            {
                _ibuilder = new TagBuilder("i");
                _ibuilder.MergeAttributes(new RouteValueDictionary(HtmlHelper.AnonymousObjectToHtmlAttributes(iconAttributes)));
                _builder.InnerHtml = string.Format("{0}{1}", _builder.InnerHtml, _ibuilder.ToString());
            }
            _pbuilder.InnerHtml = text;
            _builder.InnerHtml = string.Format("{0} {1}", _builder.InnerHtml, _pbuilder.ToString());
            _builder.MergeAttributes(new RouteValueDictionary(HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes)));
            return MvcHtmlString.Create(_builder.ToString());
        }

        public static MvcHtmlString Button(this HtmlHelper helper, string text, string title, Action modal, object htmlAttributes = null, object iconAttributes = null, bool disabled = false)
        {
            TagBuilder _ibuilder = null;
            TagBuilder _builder = new TagBuilder("a");
            TagBuilder _pbuilder = new TagBuilder("span");
            UrlHelper _urlHelper = new UrlHelper(helper.ViewContext.RequestContext);

            _builder.GenerateId("btn" + (string.IsNullOrEmpty(title) ? text : title));
            _builder.Attributes["title"] = title;
            _builder.Attributes["href"] = "#";

            if (modal.Equals(Action.Dismiss))
                _builder.Attributes["data-dismiss"] = modal.GetValueAsString();
            else
                _builder.Attributes["data-widget"] = modal.GetValueAsString();

            if (disabled)
                _builder.Attributes["disabled"] = "disabled";

            if (iconAttributes != null)
            {
                _ibuilder = new TagBuilder("i");
                _ibuilder.MergeAttributes(new RouteValueDictionary(HtmlHelper.AnonymousObjectToHtmlAttributes(iconAttributes)));
                _builder.InnerHtml = string.Format("{0}{1}", _builder.InnerHtml, _ibuilder.ToString());
            }
            _pbuilder.InnerHtml = text;
            _builder.InnerHtml = string.Format("{0} {1}", _builder.InnerHtml, _pbuilder.ToString());
            _builder.MergeAttributes(new RouteValueDictionary(HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes)));
            return MvcHtmlString.Create(_builder.ToString());
        }

        public static MvcHtmlString Button(this HtmlHelper helper, string text, string title, Action modal, string action, object route = null, object htmlAttributes = null, object iconAttributes = null, bool disabled = false)
        {
            TagBuilder _builder = new TagBuilder("a");
            TagBuilder _pbuilder = new TagBuilder("span");
            UrlHelper _urlHelper = new UrlHelper(helper.ViewContext.RequestContext);
            string _controller = Extensions.NullToString(helper.ViewContext.RouteData.GetRequiredString("controller"));

            _builder.GenerateId("btn" + (string.IsNullOrEmpty(text) ? title : text));
            _builder.Attributes["title"] = title;

            if (modal.Equals(Action.Dismiss))
            {
                _builder.Attributes["data-dismiss"] = modal.GetValueAsString();
            }
            else
            {
                _builder.Attributes["data-widget"] = modal.GetValueAsString();
            }
            _builder.Attributes["href"] = "#";
            _builder.Attributes["data-href"] = (action != null ? _urlHelper.Action(action, _controller, route) : "#");

            if (disabled)
                _builder.Attributes["disabled"] = "disabled";

            if (iconAttributes != null)
            {
                TagBuilder ibuilder = new TagBuilder("i");
                ibuilder.MergeAttributes(new RouteValueDictionary(HtmlHelper.AnonymousObjectToHtmlAttributes(iconAttributes)));
                _builder.InnerHtml = string.Format("{0}{1}", _builder.InnerHtml, ibuilder.ToString());
            }
            _pbuilder.InnerHtml = text;
            _builder.InnerHtml = string.Format("{0} {1}", _builder.InnerHtml, _pbuilder.ToString());

            if (modal.Equals(Action.Upload))
            {
                TagBuilder xbuilder = new TagBuilder("input");
                xbuilder.Attributes["id"] = "getfile";
                xbuilder.Attributes["name"] = "getfile";
                xbuilder.Attributes["type"] = "file";
                xbuilder.Attributes["enctype"] = "multipart/form-data";
                _builder.InnerHtml = string.Format("{0} {1}", _builder.InnerHtml, xbuilder.ToString());
            }
            _builder.MergeAttributes(new RouteValueDictionary(HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes)));
            return MvcHtmlString.Create(_builder.ToString());
        }

        public static MvcHtmlString ButtonAlert(this HtmlHelper helper, string text, string title, Action modal, string action, object htmlAttributes = null, object iconAttributes = null, string value = null, bool disabled = false)
        {
            TagBuilder _ibuilder = null;
            TagBuilder _builder = new TagBuilder("a");
            TagBuilder _pbuilder = new TagBuilder("span");
            UrlHelper _urlHelper = new UrlHelper(helper.ViewContext.RequestContext);
            string _controller = Extensions.NullToString(helper.ViewContext.RouteData.GetRequiredString("controller"));

            _builder.GenerateId("btn" + (string.IsNullOrEmpty(text) ? title : text));
            _builder.Attributes["title"] = title;
            _builder.Attributes["data-widget"] = modal.GetValueAsString();
            _builder.Attributes["href"] = "#";
            _builder.Attributes["data-href"] = (action != null ? _urlHelper.Action(action, _controller) : "#");

            if (disabled)
                _builder.Attributes["disabled"] = "disabled";

            if (iconAttributes != null)
            {
                _ibuilder = new TagBuilder("i");
                _ibuilder.MergeAttributes(new RouteValueDictionary(HtmlHelper.AnonymousObjectToHtmlAttributes(iconAttributes)));
                _builder.InnerHtml = string.Format("{0}{1}", _builder.InnerHtml, _ibuilder.ToString());
            }

            if (value != null)
            {
                _pbuilder.InnerHtml = value;
                _pbuilder.MergeAttributes(new RouteValueDictionary(HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes)));
            }
            else
            {
                _pbuilder.InnerHtml = text;
            }
            _builder.InnerHtml = string.Format("{0} {1}", _builder.InnerHtml, _pbuilder.ToString());
            return MvcHtmlString.Create(_builder.ToString());
        }

        public static MvcHtmlString ImageLink(this HtmlHelper helper, string text, string title, string action, string controller, object route = null, object htmlAttributes = null, object iconAttributes = null, bool disabled = false)
        {
            UrlHelper _urlHelper = new UrlHelper(helper.ViewContext.RequestContext);
            TagBuilder _builder = null;
            TagBuilder _pbuilder = null;

            _builder = new TagBuilder("img");
            _pbuilder = new TagBuilder("span");
            _pbuilder.InnerHtml = text;

            if (disabled)
                _builder.Attributes["disabled"] = "disabled";

            _builder.InnerHtml = _pbuilder.ToString();
            _builder.Attributes["title"] = title;
            _builder.Attributes["href"] = (action != null ? _urlHelper.Action(action, controller, route) : "#");
            _builder.MergeAttributes(new RouteValueDictionary(HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes)));
            return MvcHtmlString.Create(_builder.ToString());
        }

        public static MvcHtmlString Label(this HtmlHelper html, string expression, string id = "", bool generatedId = false)
        {
            return LabelHelper(html, ModelMetadata.FromStringExpression(expression, html.ViewData), expression, id, generatedId);
        }

        public static MvcHtmlString LabelFor<TModel, TValue>(this HtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression, string id = "", bool generatedId = false)
        {
            return LabelHelper(html, ModelMetadata.FromLambdaExpression(expression, html.ViewData), ExpressionHelper.GetExpressionText(expression), id, generatedId);
        }

        internal static MvcHtmlString LabelHelper(HtmlHelper html, ModelMetadata metadata, string htmlFieldName, string id, bool generatedId)
        {
            string _labelText = metadata.DisplayName ?? metadata.PropertyName;
            if (string.IsNullOrEmpty(_labelText))
            {
                return MvcHtmlString.Empty;
            }
            var _sb = new StringBuilder();
            _sb.Append(_labelText);
            if (metadata.IsRequired)
                _sb.Append("*");

            var _tag = new TagBuilder("label");
            if (!string.IsNullOrWhiteSpace(id))
            {
                _tag.Attributes.Add("id", id);
            }
            else if (generatedId)
            {
                _tag.Attributes.Add("id", html.ViewContext.ViewData.TemplateInfo.GetFullHtmlFieldId(htmlFieldName) + "_Label");
            }

            _tag.Attributes.Add("for", html.ViewContext.ViewData.TemplateInfo.GetFullHtmlFieldId(htmlFieldName));
            _tag.SetInnerText(_sb.ToString());

            return MvcHtmlString.Create(_tag.ToString(TagRenderMode.Normal));
        }

        public static String DataWidget(this HtmlHelper helper, Action modal)
        {
            return modal.GetValueAsString();
        }

        public static String DataHref(this HtmlHelper helper, string action, object route = null)
        {
            UrlHelper _urlHelper = new UrlHelper(helper.ViewContext.RequestContext);
            string _controller = Extensions.NullToString(helper.ViewContext.RouteData.GetRequiredString("controller"));

            return (_urlHelper.Action(action, _controller, route));
        }
    }
}   