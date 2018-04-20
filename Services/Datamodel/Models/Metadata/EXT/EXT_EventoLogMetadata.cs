using System.ComponentModel.DataAnnotations;

namespace Services.Model.Models
{
    [MetadataType(typeof(EXT_EventoLogMetadata))]
    public partial class EXT_EventoLog
    {
        internal sealed class EXT_EventoLogMetadata
        {
            [Key]
            [Required(ErrorMessage = "Campo {0} é obrigatório")]
            [Display(Name = "Código", Order = 1)]
            public int cod_EventoLog { get; set; }
            [Required(ErrorMessage = "Campo {0} é obrigatório")]
            [Display(Name = "Solicitação", Order = 2)]
            public int num_Solicitacao { get; set; }
            [Required(ErrorMessage = "Campo {0} é obrigatório")]
            [Display(Name = "Fornecedor", Order = 3)]
            public int cod_Fornecedor { get; set; }
            [Required(ErrorMessage = "Campo {0} é obrigatório")]
            [Display(Name = "Data", Order = 4)]
            public System.DateTime dta_Evento { get; set; }
            [Required(ErrorMessage = "Campo {0} é obrigatório")]
            [Display(Name = "Descrição", Order = 5)]
            public string des_Evento { get; set; }
            [Display(Name = "Usuário", Order = 6)]
            public string des_Login { get; set; }
            [Display(Name = "Usuário", Order = 6)]
            public string id_AspNet { get; set; }
            [Display(Name = "Conteúdo", Order = 7)]
            public string xml_conteudo { get; set; }
        }
    }
}