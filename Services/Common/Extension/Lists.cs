using System.Collections.Generic;
using System.ComponentModel;

namespace System.Web.Mvc
{
    public static partial class Extensions
    {
        public enum Record
        {
            [Description("")]
            None,
            [Description("Inclusão")]
            Insert,
            [Description("Atualização")]
            Update,
            [Description("Exclusão")]
            Delete,
            [Description("Consulta")]
            Select,
        }

        public enum Action
        {
            [Description("")]
            None,
            [Description("modal")]
            Modal,
            [Description("dialog")]
            Dialog,
            [Description("observation")]
            DialogObservation,
            [Description("upload")]
            Upload,
            [Description("view")]
            Open,
            [Description("collapse")]
            Collapse,
            [Description("modal")]
            Dismiss,
            [Description("display")]
            Display,
        }

        public enum Result
        {
            None,
            Sucsess,
            Failure,
            Mistake,
            Email,
            File,
            Modal,
            Redirect,
        }

        public enum Change
        {
            None,
            Insert,
            Update,
            Delete,
            Approve,
            Status,
            Warehouse,
            Shopping,
            Evaluation,
            Dentistry,
            Disapproved,
            Requester,
            StockChange,
            Quotation,
            Devolution,
            Confirmation,
            Solicitation,
            Error,
        }

        public static List<SelectListItem> FlagCharSet = new List<SelectListItem>()
        {
            new SelectListItem() {Text="Selecione...", Value=null},
            new SelectListItem() {Text="Sim", Value="S", Selected=true},
            new SelectListItem() { Text="Não", Value="N"}
        };

        public static List<SelectListItem> FlagChar = new List<SelectListItem>()
        {
            new SelectListItem() {Text="Selecione...", Value=null, Selected=true},
            new SelectListItem() {Text="Sim", Value="S"},
            new SelectListItem() { Text="Não", Value="N"}
        };

        public static List<SelectListItem> FlagBool = new List<SelectListItem>()
        {
            new SelectListItem() {Text="Selecione...", Value=null, Selected=true},
            new SelectListItem() {Text="Sim", Value="True"},
            new SelectListItem() { Text="Não", Value="False"}
        };

        public static List<SelectListItem> PedidoUsuario = new List<SelectListItem>()
        {
            new SelectListItem() {Text="Selecione...", Value=null, Selected=true},
            new SelectListItem() {Text="Solicitante", Value="Usuário"},
            new SelectListItem() { Text="Aprovador", Value="Aprovador"},
            new SelectListItem() { Text="Almoxarifado", Value="Almoxarifado"},
            new SelectListItem() { Text="Compras", Value="Compras"}
        };
    }
}