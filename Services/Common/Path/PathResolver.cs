namespace System.Web.Mvc
{
    public class PathResolver
    {
        private readonly PathSettings settings;

        public PathResolver(PathSettings settings)
        {
            this.settings = settings;
        }

        public string GetPath(Type controllerType)
        {
            string _directoryPath = GetDirectoryPath(controllerType);
            string _controllerName = GetControllerName(controllerType);

            bool excludeName = settings.MergeNameIntoNamespace && CanMerge(_directoryPath, _controllerName);
            
            if (excludeName)
                return _directoryPath;
            else if (_directoryPath == "")
                return _controllerName;
            else
                return string.Format("{0}/{1}", _directoryPath, _controllerName);
        }

        private static bool CanMerge(string directoryPath, string controllerName)
        {
            return (directoryPath == controllerName || directoryPath.EndsWith(string.Format("/{0}", controllerName)));
        }

        private string GetDirectoryPath(Type controllerType)
        {
            string _subPath = controllerType.Namespace ?? "";
            
            if (_subPath == settings.RootNamespace)
                return "";

            if (_subPath.StartsWith(settings.RootNamespace))
            {
                _subPath = _subPath.Substring(settings.RootNamespace.Length + 1);
            }
            string directoryPath = _subPath.Replace(".", "/");
            return directoryPath;
        }

        private string GetControllerName(Type controllerType)
        {
            string _typeName = controllerType.Name;
            if (_typeName.EndsWith("Controller", StringComparison.OrdinalIgnoreCase))
            {
                return _typeName.Substring(0, _typeName.Length - "Controller".Length);
            }
            return _typeName;
        }
    }
}