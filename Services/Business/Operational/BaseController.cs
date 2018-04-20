using System.Collections.Generic;
using System.IO;
using System.Web.Routing;

namespace System.Web.Mvc
{
    public abstract class SimpleController : Controller
    {
        public SimpleController()
        {
            ViewBag.Page = "Sistema Externo";
            ViewBag.UsersOnline = "User(s) online ";
        }

        protected override JsonResult Json(object data, string contentType, System.Text.Encoding contentEncoding, JsonRequestBehavior behavior)
        {
            return new CustomJsonResult
            {
                Data = data,
                ContentType = contentType,
                ContentEncoding = contentEncoding,
                JsonRequestBehavior = behavior
            };
        }
    }

    public abstract class BaseController : Controller
    {
        private ITempDataProvider _tempDataProvider;

        public new ITempDataProvider TempDataProvider
        {
            get
            {
                if (_tempDataProvider == null)
                {
                    _tempDataProvider = CreateTempDataProvider();
                }
                return _tempDataProvider;
            }
            set { _tempDataProvider = value; }
        }

        protected void LoadSession()
        {
            string _module = Common.Modulo;
            string _subseccao = Common.SubSeccao;
            string _department = Common.Departmento;
            string _tipoProduto = Common.TipoProduto.ToStringNullSafe();
            string _user = Common.UserName;

            if (string.IsNullOrEmpty(Common.UserName))
            {
                _module = Request["strcod_IntranetModulo"].ToStringNullSafe();
                _subseccao = Request["intcod_SubseccaoModulo"].ToStringNullSafe();
                _department = Request["intcod_DepartamentoModulo"].ToStringNullSafe();
                _tipoProduto = Request["intcod_TipoProdutoModulo"].ToStringNullSafe();
                _user = Request["intcod_LoginModulo"].ToStringNullSafe();

                Common.Enviroments(_user, _subseccao, _department, _module, _tipoProduto);
            }
        }

        protected override ITempDataProvider CreateTempDataProvider()
        {
            return base.CreateTempDataProvider();
        }

        protected override void Initialize(RequestContext requestContext)
        {
            try
            {
                LoadSession();

                var _controller = requestContext.RouteData.Values["controller"].ToStringNullSafe();
                var _action = requestContext.RouteData.Values["action"].ToStringNullSafe();

                if (_action.ToLower() == "reprint") _action = "Separate";
                if (_action.ToLower() == "reconference") _action = "Separate";

                if (_action.ToLower() == "index" && requestContext.HttpContext.Request.AppRelativeCurrentExecutionFilePath.Split('/').Length.Equals(2))
                {
                    Connection.Clean(this);
                    base.Initialize(requestContext);
                }
                else if (_action.StartsWith("_", StringComparison.InvariantCulture) || _action.ToLower() == "cancel" || _action.ToLower() == "close")
                {
                    base.Initialize(requestContext);
                }
                else if (Common.UserName == null)
                {
                    Common.Error(requestContext, "Sua sessão foi expirada!");
                }
                else if (Component.ValidaAcesso(_controller, _action))
                {
                    Common.Error(requestContext, "Usuário sem permissão de acesso!");
                }
                else
                {
                    base.Initialize(requestContext);
                }
            }
            catch (Exception ex)
            {
                Common.LogError(ex);
                Common.Error(requestContext, ("Sua sessão foi expirada!"));
            }
        }
        
        public BaseController()
        {
            ViewBag.Page = "Central de Aplicativos";
            ViewBag.UsersOnline = "User(s) online ";
        }

        // GET: Close
        public ActionResult Close(long id)
        {
            Connection.Transaction.Rollback();
            return RazorViewToJson("Index", Extensions.Result.Sucsess);
        }

        // GET: Cancel
        public ActionResult Cancel()
        {
            return RazorViewToJson("Index");
        }

        public ActionResult RazorViewToJson(bool result = true)
        {
            var _view = Extensions.NullToString(this.ControllerContext.RouteData.GetRequiredString("action"));
            return RazorViewToJson(_view, null, null, result);
        }

        public ActionResult RazorViewToJson(string view, Extensions.Result result, string file)
        {
            return RazorViewToJson(view, null, null, true, null, result, file);
        }

        public ActionResult RazorViewToJson(string view, Extensions.Result result)
        {
            return RazorViewToJson(view, null, null, true, null, result);
        }

        public ActionResult RazorViewToJson(string view, bool result = true)
        {
            return RazorViewToJson(view, null, null, result);
        }

        public ActionResult RazorViewToJson(object model, bool result = true, string title = null)
        {
            var _view = Extensions.NullToString(this.ControllerContext.RouteData.GetRequiredString("action"));
            return RazorViewToJson(_view, model, null, result, title);
        }

        public ActionResult RazorViewToJson(string view, object model, bool result = true)
        {
            return RazorViewToJson(view, model, null, result);
        }

        public ActionResult RazorViewToJson(string view, string partial, bool result = true)
        {
            return RazorViewToJson(view, null, partial, result);
        }

        public ActionResult RazorViewToJson(string view, string title, Extensions.Result alert)
        {
            this.ViewData.Model = null;
            string _partial = "#container";

            using (var swc = new StringWriter())
            {
                var _viewResult = ViewEngines.Engines.FindPartialView(this.ControllerContext, view);
                var _viewContext = new ViewContext(this.ControllerContext, _viewResult.View, this.ViewData, this.TempData, swc);
                _viewResult.View.Render(_viewContext, swc);
                return Json(new { success = true, url = swc.GetStringBuilder().ToString(), partial = _partial, title = title, alert = alert }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult RazorViewToJsonRedirect(string view, Extensions.Result redirect, string link)
        {
            this.ViewData.Model = null;
            string _partial = "#container";

            using (var swc = new StringWriter())
            {
                var _viewResult = ViewEngines.Engines.FindPartialView(this.ControllerContext, view);
                var _viewContext = new ViewContext(this.ControllerContext, _viewResult.View, this.ViewData, this.TempData, swc);
                _viewResult.View.Render(_viewContext, swc);
                return Json(new { success = redirect, url = swc.GetStringBuilder().ToString(), partial = _partial, redirect = link }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult RazorViewToJson(string view, object model, string partial, bool result = true, string title = null, Extensions.Result alert = Extensions.Result.None, string file = null)
        {
            this.ViewData.Model = model;

            if (!string.IsNullOrEmpty(partial))
            {
                if (!partial.StartsWith("#"))
                    partial = string.Concat("#", partial);
            }
            else
            {
                partial = "#container";
            }
            using (var swc = new StringWriter())
            {
                if (!result) returnTitle(view, out title); else title = string.Empty;
                var _viewResult = ViewEngines.Engines.FindPartialView(this.ControllerContext, view);
                var _viewContext = new ViewContext(this.ControllerContext, _viewResult.View, this.ViewData, this.TempData, swc);
                _viewResult.View.Render(_viewContext, swc);
                return Json(new { success = result, url = swc.GetStringBuilder().ToString(), partial = partial, title = title, alert = alert, file = file}, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult RazorViewToJson(string view, bool result, Extensions.Result alert, string partialUrl, string callbackUrl)
        {
            this.ViewData.Model = null;
            string _partial = "#container";
            string _title = string.Empty;

            using (var swc = new StringWriter())
            {
                if (!result) returnTitle(view, out _title); else _title = string.Empty;
                var _viewResult = ViewEngines.Engines.FindPartialView(this.ControllerContext, view);
                var _viewContext = new ViewContext(this.ControllerContext, _viewResult.View, this.ViewData, this.TempData, swc);
                _viewResult.View.Render(_viewContext, swc);
                return Json(new { success = result, url = swc.GetStringBuilder().ToString(), partial = _partial, title = _title, alert = alert, partialUrl = partialUrl, callbackUrl = callbackUrl }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult RazorViewToJsonFailure(string title)
        {
            var _view = Extensions.NullToString(this.ControllerContext.RouteData.GetRequiredString("action"));
            return RazorViewToJsonFailure(_view, null, null, title);
        }

        public ActionResult RazorViewToJsonFailure(object model, string title = null)
        {
            var _view = Extensions.NullToString(this.ControllerContext.RouteData.GetRequiredString("action"));
            return RazorViewToJsonFailure(_view, model, null, title);
        }

        public ActionResult RazorViewToJsonFailure(string view, object model, string partial = null, string title = null)
        {
            this.ViewData.Model = model;

            if (!string.IsNullOrEmpty(partial))
            {
                if (!partial.StartsWith("#"))
                    partial = string.Concat("#", partial);
            }
            else
            {
                partial = "#container";
            }

            using (var swc = new StringWriter())
            {
                var _viewResult = ViewEngines.Engines.FindPartialView(this.ControllerContext, view);
                var _viewContext = new ViewContext(this.ControllerContext, _viewResult.View, this.ViewData, this.TempData, swc);
                _viewResult.View.Render(_viewContext, swc);
                return Json(new { success = false, url = swc.GetStringBuilder().ToString(), partial = partial, title = title }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult RazorViewToJson(bool result, Extensions.Result alert)
        {
            return Json(new { success = result, alert = alert }, JsonRequestBehavior.AllowGet);
        }

        protected override ViewResult View(string viewName, string masterName, object model)
        {
            ViewBag.Name = string.Empty;

            if (!string.IsNullOrEmpty(ControllerContext.RouteData.GetRequiredString("controller")))
            {
                ViewBag.Name = Extensions.NullToString(ControllerContext.RouteData.GetRequiredString("controller"));
            }

            //if (InSession.Difference<String>("View", ViewBag.Name))
            //HomeBusiness.Dispose();

            //if (model is Page)
            //{
            //    ViewBag.Title = ((Page)model).Title;
            //    //HACK: SEO
            //    //TODO: SEO
            //}
            return base.View(viewName, masterName, model);
        }

        void returnTitle(string view, out string title)
        {
            switch (view)
            {
                case "Index":
                    title = "Lista";
                    break;
                case "Details":
                    title = "Detalhes";
                    break;
                case "Create":
                    title = "Inclusão";
                    break;
                case "Edit":
                    title = "Edição";
                    break;
                default:
                    title = "";
                    break;
            }
        }

        void DisplaySuccessMessage(string msgText)
        {
            TempData["SuccessMessage"] = msgText;
        }

        void DisplayErrorMessage()
        {
            TempData["ErrorMessage"] = "Save changes was unsuccessful.";
        }
    }

    public class NullTempDataProvider : ITempDataProvider
    {

        public IDictionary<string, object> LoadTempData(ControllerContext controllerContext)
        {
            return new TempDataDictionary();
        }

        public void SaveTempData(ControllerContext controllerContext, IDictionary<string, object> values)
        {

        }
    }
}