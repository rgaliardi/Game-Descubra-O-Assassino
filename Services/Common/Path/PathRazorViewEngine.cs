namespace System.Web.Mvc
{
    public class PathRazorViewEngine : RazorViewEngine
    {
        private readonly PathResolver controllerPathResolver;

        public PathRazorViewEngine(PathSettings settings)
            : this(null, settings)
        {
        }

        public PathRazorViewEngine(IViewPageActivator viewPageActivator, PathSettings settings)
            : base(viewPageActivator)
        {
            controllerPathResolver = new PathResolver(settings);
        }

        public override ViewEngineResult FindView(ControllerContext controllerContext, string viewName, string masterName, bool useCache)
        {
            return FindUsingControllerPath(controllerContext, () => base.FindView(controllerContext, viewName, masterName, useCache));
        }

        public override ViewEngineResult FindPartialView(ControllerContext controllerContext, string partialViewName, bool useCache)
        {
            return FindUsingControllerPath(controllerContext, () => base.FindPartialView(controllerContext, partialViewName, useCache));
        }

        /// <summary>
        /// Temporarily switches the "controller" route value within the current ControllerContext when calling the FindView/FindPartial methods.
        /// The "controller" value is changed to a path based on the controller name and namespace.
        /// 
        /// This allows us to use the existing view finding functionality, with multiple folders (based on controller namespace) instead
        /// of a single folder based on the controller name only.
        /// 
        /// Internally, ViewEngines that inherit from VirtualPathProviderViewEngine cache the result of finding views. The cache 
        /// key is generated from the "controller" value. Using the full controller path allows separate views to be cached, 
        /// supporting situations where we have the same controller name in different namespaces.
        /// </summary>
        private ViewEngineResult FindUsingControllerPath(ControllerContext controllerContext, Func<ViewEngineResult> func)
        {
            string _controllerName = controllerContext.RouteData.GetRequiredString("controller");
            string _controllerPath = controllerPathResolver.GetPath(controllerContext.Controller.GetType());
            controllerContext.RouteData.Values["controller"] = _controllerPath;
            var _result = func();
            controllerContext.RouteData.Values["controller"] = _controllerName;
            return _result;
        }
    }
}
