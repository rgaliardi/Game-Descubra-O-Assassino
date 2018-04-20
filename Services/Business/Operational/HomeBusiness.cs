using System.Configuration;
using System.IO;
using System.Xml.Serialization;

namespace System.Web.Mvc
{
    public static class HomeBusiness
    {
        public static string Modulo
        {
            get { return ConfigurationManager.AppSettings["Module"].ToStringNullSafe(); }
        }

        public static String Ambient
        {
            get { return ConfigurationManager.AppSettings["Ambient"].ToStringNullSafe(); }
        }

        public static String EmailFrom
        {
            get { return ConfigurationManager.AppSettings["EmailFrom"].ToStringNullSafe(); }
        }

        public static String EmailCopy
        {
            get { return ConfigurationManager.AppSettings["EmailCopy"].ToStringNullSafe(); }
        }

        public static String IPClient
        {
            get { return Common.GetIpAddress; }
        }

        public static String HostClient
        {
            get { return Common.GetCompCode; }
        }  

        public static string ConvertXml<T>(object xml)
        {
            var _serializer = new XmlSerializer(typeof(T));
            return _serializer.Deserialize(new StringReader(xml.ToString())).ToString();
        }
    }
}