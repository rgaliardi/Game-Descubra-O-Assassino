using System.Web.Mvc;

namespace System.Web
{
    public partial class Component
    {
        public static bool ValidaAcesso(string funcao, string operacao = null)
        {
            object[] _parametersNF = { Common.UserName, Common.SubSeccao, Common.Modulo, funcao, operacao };
            return !Invoker.Instance.Boolean("Seguranca.Bus_Relatorio", "ValidarAcesso", _parametersNF);
        }
    }
}
