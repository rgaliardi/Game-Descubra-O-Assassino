namespace System.Web.Mvc
{
    public class PathSettings
    {
        public string RootNamespace { get; private set; }
        public bool MergeNameIntoNamespace { get; private set; }

        public PathSettings(string rootNamespace, bool mergeNameIntoNamespace = false)
        {
            RootNamespace = rootNamespace;
            MergeNameIntoNamespace = mergeNameIntoNamespace;
        }
    }
}