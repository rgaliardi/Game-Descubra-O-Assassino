using System.Configuration;
using System.IO;
using System.Net;

namespace System.Web
{
    public static partial class Common
    {
        /// <summary>
        /// Get IP Address
        /// </summary>
        /// <returns></returns>
        public static string GetIpAddress
        {
            get
            {
                string _ipaddress = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];

                if (string.IsNullOrEmpty(_ipaddress))
                    _ipaddress = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];

                if (_ipaddress.Length < 7)
                    _ipaddress = GetIpAddressExtra();

                return _ipaddress;
            }
        }

        /// <summary>
        /// Get IP Address
        /// </summary>
        /// <returns></returns>
        public static string GetIpAddressExtra()
        {
            string _ip = "";
            IPHostEntry _ipEntry = Dns.GetHostEntry(GetCompCode);
            IPAddress[] _addr = _ipEntry.AddressList;

            if (_addr[1].ToStringNullSafe().Length > 15)
                _ip = _addr[2].ToString();
            else
                _ip = _addr[1].ToString();
            return _ip;
        }

        /// <summary>
        /// Get Computer Name 
        /// </summary>
        /// <returns></returns>
        public static string GetCompCode
        {
            get
            {
                string _strHostName = "";
                _strHostName = Dns.GetHostName();
                return _strHostName;
            }
        }

        public static void LogDetail(string description, string page)
        {
            string _message = string.Format("Time: {0}", DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss tt"));

            _message += Environment.NewLine;
            _message += "-----------------------------------------------------------";
            _message += Environment.NewLine;
            _message += string.Format("Message:      {0}", "Detalhes - Ponto de Referência");
            _message += Environment.NewLine;
            _message += string.Format("Descrição:    {0}", description.ToStringNullSafe());
            _message += Environment.NewLine;
            _message += string.Format("Página:       {0}", page.ToString());
            _message += Environment.NewLine;
            _message += "-----------------------------------------------------------";
            _message += Environment.NewLine;
            _message += Environment.NewLine;
            
            string _file = string.Concat(CheckFileDetail, string.Format("Detail_{0:yyyyMMdd}.txt", DateTime.Now));
            using (StreamWriter writer = new StreamWriter(_file, true))
            {
                writer.WriteLine(_message);
                writer.Close();
            }
        }

        public static void LogError(Exception ex)
        {
#if (!DEBUG)
            string _message = string.Format("Time: {0}", DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss tt"));

            _message += Environment.NewLine;
            _message += "-----------------------------------------------------------";
            _message += Environment.NewLine;
            _message += string.Format("Message:      {0}", ex.Message);
            _message += Environment.NewLine;
            _message += string.Format("StackTrace:   {0}", ex.StackTrace);
            _message += Environment.NewLine;
            _message += string.Format("Source:       {0}", ex.Source);
            _message += Environment.NewLine;

            if (ex.InnerException != null)
            {
                _message += string.Format("InnerDetails: {0}", ex.InnerException.Message);
                _message += Environment.NewLine;
            }
            _message += string.Format("TargetSite:   {0}", ex.TargetSite.ToString());
            _message += Environment.NewLine;
            _message += "-----------------------------------------------------------";
            _message += Environment.NewLine;
            _message += Environment.NewLine;

            try
            {
                string _subject = string.Empty;
                string _body = string.Empty;
                string _fromEmail = ConfigurationManager.AppSettings["email_from"].ToString();
                string _toAddress = ConfigurationManager.AppSettings["email_support"];
                string _ccAddress = string.Empty;

                _subject = "Erro no Sistema";
                _body += string.Format("Ocorrreu um erro no sistema! O as informações desse erro encontram-se na pasta interna da aplicação no IIS '/App_Data/Erro'.");
                SendEmail(Common.Ambient, _fromEmail, _toAddress, _subject, _body, _ccAddress);
            }
            finally
            {
                string file = string.Concat(CheckFileError, string.Format("Error_{0:yyyyMMdd}.txt", DateTime.Now));
                using (StreamWriter writer = new StreamWriter(file, true))
                {
                    writer.WriteLine(_message);
                    writer.Close();
                }
            }
#endif
        }

        private static string CheckFileError
        {
            get
            {
                string _path = HttpContext.Current.Server.MapPath("~/App_Data/Error/");

                if (!Directory.Exists(_path))
                    Directory.CreateDirectory(_path);

                return _path;
            }
        }

        private static string CheckFileDetail
        {
            get
            {
                string _path = HttpContext.Current.Server.MapPath("~/App_Data/Detail/");

                if (!Directory.Exists(_path))
                    Directory.CreateDirectory(_path);

                return _path;
            }
        }
    }
}