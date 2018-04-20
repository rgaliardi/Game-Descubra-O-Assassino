using Newtonsoft.Json;
using System.IO;
using System.Runtime.Serialization.Json;

namespace System.Web.Mvc
{
    public class ObjectFilter : ActionFilterAttribute
    {
        public string Param { get; set; }
        public Type RootType { get; set; }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if ((filterContext.HttpContext.Request.ContentType ?? string.Empty).Contains("application/json"))
            {
                object _object = new DataContractJsonSerializer(RootType).ReadObject(filterContext.HttpContext.Request.InputStream);
                filterContext.ActionParameters[Param] = _object;
            }
        }
    }

    public class JsonFilter : ActionFilterAttribute
    {
        public string Param { get; set; }
        public Type JsonDataType { get; set; }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (filterContext.HttpContext.Request.ContentType.Contains("application/json"))
            {
                string inputContent;
                using (var sr = new StreamReader(filterContext.HttpContext.Request.InputStream))
                {
                    inputContent = sr.ReadToEnd();
                }
                var result = JsonConvert.DeserializeObject(inputContent, JsonDataType);
                filterContext.ActionParameters[Param] = result;
            }
        }
    }

    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public sealed class NoCacheAttribute : ActionFilterAttribute
    {
        public override void OnResultExecuting(ResultExecutingContext filterContext)
        {
            filterContext.HttpContext.Response.Cache.SetExpires(DateTime.UtcNow.AddDays(-1));
            filterContext.HttpContext.Response.Cache.SetValidUntilExpires(false);
            filterContext.HttpContext.Response.Cache.SetRevalidation(HttpCacheRevalidation.AllCaches);
            filterContext.HttpContext.Response.Cache.SetCacheability(HttpCacheability.NoCache);
            filterContext.HttpContext.Response.Cache.SetNoStore();
            base.OnResultExecuting(filterContext);
        }
    } 

    public class PageTitleAttribute : ActionFilterAttribute
    {
        private readonly string _pageTitle = string.Empty;
        private readonly string _pageSubTitle = string.Empty;

        public PageTitleAttribute(string pageTitle, string subTitle = null)
        {
            _pageTitle = pageTitle;
            _pageSubTitle = subTitle;
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            //Log("OnActionExecuting", filterContext.RouteData);
            var _result = filterContext.Result as ViewResult;

            if (_result != null)
            {
                _result.ViewBag.Title = _pageTitle;
                _result.ViewBag.SubTitle = _pageSubTitle;
            }
        }

        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            //Log("OnActionExecuted", filterContext.RouteData);
            base.OnActionExecuted(filterContext);
            var _result = filterContext.Result as ViewResult;

            if (_result != null)
            {
                _result.ViewBag.Title = _pageTitle;
                _result.ViewBag.SubTitle = _pageSubTitle;
            }
        }

        public override void OnResultExecuting(ResultExecutingContext filterContext)
        {
            //Log("OnResultExecuting", filterContext.RouteData);
            var _result = filterContext.Result as ViewResult;

            if (_result != null)
            {
                _result.ViewBag.Title = _pageTitle;
                _result.ViewBag.SubTitle = _pageSubTitle;
            }
        }
    }
}