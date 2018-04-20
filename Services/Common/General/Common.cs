using OpenHtmlToPdf;
using System.Configuration;
using System.IO;
using System.Web.Memory;

namespace System.Web
{
    public partial class Common
    {
        public static void Clear()
        {
            InSession.Clear();
        }
    
        public static void Enviroments(string User, string SubSeccao, string Departamento, string Modulo, string TipoProduto)
        {
            InSession.Entity<string>("intcod_LoginModulo", User);
            InSession.Entity<string>("intcod_SubseccaoModulo", SubSeccao);
            InSession.Entity<string>("intcod_DepartamentoModulo", Departamento);
            InSession.Entity<string>("strcod_IntranetModulo", Modulo);
            InSession.Entity<string>("intcod_TipoProdutoModulo", TipoProduto);
            InSession.Entity<String>("Ambient", ConfigurationManager.AppSettings["Ambient"]);
            InSession.Entity<String>("Enviroment", ConfigurationManager.AppSettings["Enviroment"]);
        }

        public static byte[] HtmlToPdf(string html, string title)
        {
            return Pdf.From(html)
                        .OfSize(PaperSize.A4)
                        .WithObjectSetting("web.background", "true")
                        .WithObjectSetting("web.loadImages", "true")
                        .WithObjectSetting("web.enableJavascript", "true")
                        .WithObjectSetting("web.enableIntelligentShrinking", "false")
                        .WithObjectSetting("web.minimumFontSize", "9")
                        .WithObjectSetting("web.printMediaType", "true")
                        .WithObjectSetting("web.defaultEncoding", "utf-8")
                        .WithObjectSetting("web.enablePlugins", "true")
                        .WithTitle(title)
                        .WithoutOutline()
                        .WithMargins(0.25.Centimeters())
                        .Portrait()
                        .Content();
        }

        public static void LoadApplication(object application)
        {
            InSession.Entity<Object>("Application", application);
        }

        public static void LoadOperations(object operations)
        {
            InSession.Entity<Object>("Operations", operations);
        }

        public static void LoadMenu(object menu)
        {
            InSession.Entity<Object>("Menu", menu);
        }

        public static string CreateFileIfNeeded(string path, byte[] file)
        {
            bool _result = true;

            try
            {
                int _count = 0;
                string _filepath = Path.GetDirectoryName(path);
                string _filename = Path.GetFileNameWithoutExtension(path);
                string _fileextension = Path.GetExtension(path);
                CreateFolderIfNeeded(_filepath);

                while (_result)
                {
                    if (!File.Exists(path))
                    {
                        File.WriteAllBytes(path, file);  
                        _result = false;
                    } else
                    {
                        _count++;
                        path = string.Format("{0}\\{1}_{2}{3}", _filepath, _filename, _count, _fileextension);
                    }
                }
            }
            catch (Exception ex)
            {
                Common.LogError(ex);
            }
            return path;
        }

        public static void CreateFolderIfNeeded(string path)
        {

            if (!Directory.Exists(path))
            {
                try
                {
                    Directory.CreateDirectory(path);
                }
                catch (Exception ex)
                {
                    Common.LogError(ex);
                }
            }
        }

        public static void DeleteFolderIfNeeded(string path)
        {
            try
            {
                Directory.Delete(path);
            }
            catch (Exception ex)
            {
                Common.LogError(ex);
            }
        }

        public static void DeleteFolderIfEmpty(string path)
        {
            try
            {
                foreach (var directory in Directory.GetDirectories(path))
                {

                    if (Directory.GetFiles(directory).Length == 0 &&
                        Directory.GetDirectories(directory).Length == 0)
                    {
                        Directory.Delete(directory, false);
                    }
                }
            }
            catch (Exception ex)
            {
                Common.LogError(ex);
            }
        }

        public static void RenameFolderIfNeeded(string path_Original, string path_Rename)
        {
            try
            {
                Directory.Move(path_Original.Replace("\\\\", "\\"), path_Rename.Replace("\\\\", "\\"));
            }
            catch (Exception ex)
            {
                Common.LogError(ex);
            }
        }
    }
}